using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.Employees.ViewModels;
using BusinessSafe.WebSite.Areas.Users.Factories;
using BusinessSafe.WebSite.Areas.Users.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Employees.Factories
{
    public class EmployeeViewModelFactory : IEmployeeViewModelFactory
    {
        private Guid? _employeeId;
        private readonly IEmployeeService _employeeService;
        private readonly ISiteService _siteService;
        private readonly IRolesService _rolesService;
        private readonly ISiteGroupService _siteGroupService;
        private readonly ILookupService _lookupService;
        private readonly IAddUsersViewModelFactory _addUsersViewModelFactory;
        private readonly IRiskAssessorService _riskAssessorService;
        private long _companyId;
        private ICustomPrincipal _currentUser;

        public EmployeeViewModelFactory(IEmployeeService employeeService, ISiteService siteService, ILookupService lookupService,
                                        ISiteGroupService siteGroupService, IRolesService rolesService, IRiskAssessorService riskAssessorService)
        {
            _employeeService = employeeService;
            _siteService = siteService;
            _siteGroupService = siteGroupService;
            _lookupService = lookupService;
            _rolesService = rolesService;
            _riskAssessorService = riskAssessorService;

            _addUsersViewModelFactory = new AddUsersViewModelFactory(_employeeService, _rolesService, _siteService, _siteGroupService);
        }

        public EmployeeViewModel GetViewModel()
        {
            var employeeDto = GetEmployee();
            var countries = GetCountries(employeeDto);
            var nationalities = GetNationalities(employeeDto);
            var sites = GetSites();
           
            var sexes = GetSexes();
            var titles = GetTitles();
            var employmentStatuses = GetEmploymentStatuses();
            var companyVehicleTypes = GetCompanyVehicleTypes();
     
            AddUsersViewModel addUsersViewModel = _addUsersViewModelFactory
                .WithCompanyId(_companyId)                
                .GetViewModel();
            
            var viewModel = new EmployeeViewModel
                       {
                           EmployeeReference = employeeDto.EmployeeReference,
                           NameTitle = employeeDto.Title,
                           Forename = employeeDto.Forename,
                           Surname = employeeDto.Surname,
                           MiddleName = employeeDto.MiddleName,
                           PreviousSurname = employeeDto.PreviousSurname,
                           Sex = employeeDto.Sex,
                           DateOfBirth = employeeDto.DateOfBirth,
                           NationalityId = employeeDto.Nationality != null ? employeeDto.Nationality.Id : default(int),
                           HasDisability = employeeDto.HasDisability,
                           DisabilityDescription = employeeDto.DisabilityDescription,
                           NINumber = employeeDto.NINumber,
                           PassportNumber = employeeDto.PassportNumber,
                           PPSNumber = employeeDto.PPSNumber,
                           WorkVisaNumber = employeeDto.WorkVisaNumber,
                           WorkVisaExpirationDate = employeeDto.WorkVisaExpirationDate,
                           DrivingLicenseNumber = employeeDto.DrivingLicenseNumber,
                           DrivingLicenseExpirationDate = employeeDto.DrivingLicenseExpirationDate,
                           HasCompanyVehicle = employeeDto.HasCompanyVehicle,
                           CompanyVehicleTypeId = employeeDto.CompanyVehicleTypeId,
                           CompanyVehicleRegistration = employeeDto.CompanyVehicleRegistration,
                           SiteId = employeeDto.SiteId,
                           JobTitle = employeeDto.JobTitle,
                           EmploymentStatusId = employeeDto.EmploymentStatusId,
                           CompanyId = _companyId,
                           EmployeeId = _employeeId,
                           Countries = countries,
                           Nationalities = nationalities,
                           Sites = sites,
                           Sexes = sexes,
                           Titles = titles,
                           EmploymentStatuses = employmentStatuses,
                           CompanyVehicleTypes = companyVehicleTypes,
                           Address1 = employeeDto.MainContactDetails != null ? employeeDto.MainContactDetails.Address1 : null,
                           Address2 = employeeDto.MainContactDetails != null ? employeeDto.MainContactDetails.Address2 : null,
                           Address3 = employeeDto.MainContactDetails != null ? employeeDto.MainContactDetails.Address3 : null,
                           Town = employeeDto.MainContactDetails != null ? employeeDto.MainContactDetails.Town : null,
                           County = employeeDto.MainContactDetails != null ? employeeDto.MainContactDetails.County : null,
                           CountryId = employeeDto.MainContactDetails != null && employeeDto.MainContactDetails.Country != null ? (int?)employeeDto.MainContactDetails.Country.Id : null,
                           Postcode = employeeDto.MainContactDetails != null ? employeeDto.MainContactDetails.PostCode : null,
                           Telephone = employeeDto.MainContactDetails != null ? employeeDto.MainContactDetails.Telephone1 : null,
                           Mobile = employeeDto.MainContactDetails != null ? employeeDto.MainContactDetails.Telephone2 : null,
                           Email = employeeDto.MainContactDetails != null ? employeeDto.MainContactDetails.Email : null,
                           IsExistingUser = employeeDto.User != null,
                           CanChangeEmail = employeeDto.CanChangeEmail,
                           ContactDetailId = employeeDto.MainContactDetails != null ? employeeDto.MainContactDetails.Id : 0,
                          // UserId = employeeDto.User != null ? employeeDto.User.Id : Guid.Empty,
                           IsPendingRegistration = employeeDto.User != null && employeeDto.User.Deleted == false && employeeDto.User.IsRegistered == false,
                           EmergencyContactDetails = MapEmergencyContactDetails(employeeDto.EmergencyContactDtos),
                           UserSites = addUsersViewModel.Sites,
                           UserSiteGroups = addUsersViewModel.SiteGroups,
                           UserRoles = addUsersViewModel.Roles,
                           NotificationType = MapNotificationType(employeeDto.NotificationType),
                           NotificationFrequency = employeeDto.NotificationFrequency
                       };

            if (employeeDto.User != null && employeeDto.User.Deleted == false)
            {
                viewModel.UserId = employeeDto.User.Id;
                viewModel.UserRoleId = employeeDto.User.Role != null ? employeeDto.User.Role.Id : (Guid?)null;
                viewModel.UserRoleDescription = employeeDto.User.Role != null ? employeeDto.User.Role.Description : null;

                if (employeeDto.User.SiteStructureElement != null)
                {
                    viewModel.UserPermissionsApplyToAllSites =  employeeDto.User.SiteStructureElement.IsMainSite;

                    if (viewModel.UserPermissionsApplyToAllSites == false)
                    {
                        if (employeeDto.User.SiteStructureElement.GetType() == typeof (SiteGroupDto))
                        {
                            viewModel.UserSiteGroupId = employeeDto.User.SiteStructureElement.Id;
                        }
                        else if (employeeDto.User.SiteStructureElement.GetType() == typeof (SiteDto))
                        {
                            viewModel.UserSiteId = employeeDto.User.SiteStructureElement.Id;
                        }
                    }
                }

                if (_currentUser != null
                     && employeeDto.User.Role.Description == "User Admin"
                     && _currentUser.UserId == employeeDto.User.Id)
                {
                    viewModel.CanChangeRoleDdl = false;
                }
            }

            if (employeeDto.RiskAssessor == null) return viewModel;
            viewModel.IsRiskAssessor = true;
            viewModel.DoNotSendTaskOverdueNotifications = employeeDto.RiskAssessor.DoNotSendTaskOverdueNotifications;
            viewModel.DoNotSendTaskCompletedNotifications = employeeDto.RiskAssessor.DoNotSendTaskCompletedNotifications;
            viewModel.DoNotSendReviewDueNotification = employeeDto.RiskAssessor.DoNotSendReviewDueNotification;
            viewModel.IsRiskAssessorAssignedToRiskAssessments = _riskAssessorService.HasRiskAssessorGotOutstandingRiskAssessments(employeeDto.RiskAssessor.Id, viewModel.CompanyId);
            viewModel.RiskAssessorHasAccessToAllSites = employeeDto.RiskAssessor.HasAccessToAllSites;
            if (employeeDto.RiskAssessor.Site == null) return viewModel;
            viewModel.RiskAssessorSite = employeeDto.RiskAssessor.Site.Name;
            viewModel.RiskAssessorSiteId = employeeDto.RiskAssessor.Site.Id;
          
            return viewModel;
        }

        private NotificationTypeViewModel MapNotificationType(NotificationTypeDto notification)
        {
            switch (notification)
            {
                case NotificationTypeDto.Daily: return NotificationTypeViewModel.Daily;
                case NotificationTypeDto.Weekly: return NotificationTypeViewModel.Weekly;
                case NotificationTypeDto.Monthly: return NotificationTypeViewModel.Monthly;
                default: return NotificationTypeViewModel.Daily;
            }
        }
        
        private IEnumerable<EmergencyContactViewModel> MapEmergencyContactDetails(IEnumerable<EmployeeEmergencyContactDetailDto> emergencyContactDtos)
        {
            if (emergencyContactDtos == null)
                return new List<EmergencyContactViewModel>();

            return emergencyContactDtos.Select(emergencyContact => new EmergencyContactViewModel()
            {
                EmergencyContactId = emergencyContact.EmergencyContactId,
                Title = emergencyContact.Title,
                Forename = emergencyContact.Forename,
                Surname = emergencyContact.Surname,
                Relationship = emergencyContact.Relationship,
                Address1 = emergencyContact.Address1,
                Address2 = emergencyContact.Address2,
                Address3 = emergencyContact.Address3,
                PostCode = emergencyContact.PostCode,
                Town = emergencyContact.Town,
                County = emergencyContact.County,
                EmergencyContactCountryId = emergencyContact.CountryId,
                WorkTelephone = emergencyContact.WorkTelephone,
                HomeTelephone = emergencyContact.HomeTelephone,
                MobileTelephone = emergencyContact.MobileTelephone,
                PreferredContactNumber = emergencyContact.PreferredContactNumber,
                SameAddressAsEmployee = emergencyContact.SameAddressAsEmployee
            }).ToList();
        }

        private static IEnumerable<AutoCompleteViewModel> GetCompanyVehicleTypes()
        {
            var companyVehicleTypes = new List<AutoCompleteViewModel>
                                          {
                                              new AutoCompleteViewModel("Car", "1")
                                          };

            return companyVehicleTypes.AddDefaultOption();
        }

        private static IEnumerable<AutoCompleteViewModel> GetEmploymentStatuses()
        {
            var employmentStatuses = new List<AutoCompleteViewModel>
                                                        {
                                                            new AutoCompleteViewModel ("Employed","1")
                                                        };
            return employmentStatuses.AddDefaultOption();
        }

        private static IEnumerable<AutoCompleteViewModel> GetTitles()
        {
            var titles = new List<AutoCompleteViewModel>
                             {
                                 new AutoCompleteViewModel ("Mr", "Mr"),
                                 new AutoCompleteViewModel ("Mrs", "Mrs"),
                                 new AutoCompleteViewModel ("Ms", "Ms"),
                                 new AutoCompleteViewModel ("Miss", "Miss"),
                                 new AutoCompleteViewModel ("Dr", "Dr")
                             };

            return titles.AddDefaultOption();
        }

        private static IEnumerable<AutoCompleteViewModel> GetSexes()
        {
            var sexes = new List<AutoCompleteViewModel>()
                            {
                                new AutoCompleteViewModel("Male", "Male"),
                                new AutoCompleteViewModel("Female", "Female")
                            };

            return sexes.AddDefaultOption();
        }

        private EmployeeDto GetEmployee()
        {
            var employeeDto = new EmployeeDto();
            if (_employeeId.HasValue)
            {
                employeeDto = _employeeService.GetEmployee(_employeeId.Value, _companyId);
            }
            return employeeDto;
        }

        private IEnumerable<AutoCompleteViewModel> GetSites()
        {
            return _siteService
                .GetAll(_companyId)
                .OrderBy(x => x.Name)
                .Select(AutoCompleteViewModel.ForSite)
                .AddDefaultOption();
        }    
     
        private IEnumerable<AutoCompleteViewModel> GetNationalities(EmployeeDto employeeDto)
        {
            var nationalities = _lookupService
                                    .GetNationalities()
                                    .OrderBy(x => x.Name);

            if (!nationalities.Any())
                return new List<AutoCompleteViewModel>().AddDefaultOption();

            var result = new List<LookupDto>
                             {
                                 nationalities.First(x => x.Name == "British"),
                                 nationalities.First(x => x.Name == "Irish")
                             };
            result.AddRange(nationalities.Where(x => x.Name != "British" && x.Name!="Irish"));

            return result.Select(AutoCompleteViewModel.ForLookUp).AddDefaultOption();
        }

        private IEnumerable<AutoCompleteViewModel> GetCountries(EmployeeDto employeeDto)
        {
            var countries = _lookupService.GetCountries();
            return AutoCompleteViewModel.ForCountries(countries);
        }

        public IEmployeeViewModelFactory WithCurrentUser(ICustomPrincipal currentUser)
        {
            _currentUser = currentUser;
            return this;
        }

        public IEmployeeViewModelFactory WithEmployeeId(Guid? employeeId)
        {
            _employeeId = employeeId;
            return this;
        }

        public IEmployeeViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }
    }
}