using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Employees.Factories;
using BusinessSafe.WebSite.Areas.Employees.ViewModels;
using BusinessSafe.WebSite.Areas.Users.Factories;
using BusinessSafe.WebSite.Areas.Users.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.EmployeeViewModelFactoryTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetViewModelTests
    {
        private Mock<IEmployeeService> _employeeService;
        private Mock<ISiteService> _siteService;
        private Mock<ILookupService> _lookupService;

        private Mock<ISiteGroupService> _siteGroupService;
        private Mock<IRolesService> _rolesService;
        private Mock<IRiskAssessorService> _riskAssessorService;
        
        private Mock<IAddUsersViewModelFactory> _addUsersViewModelFactory;

        private Guid? _employeeId;
        private const long _companyId = 0;

        [SetUp]
        public void Setup()
        {
            _employeeService = new Mock<IEmployeeService>();
            _siteService = new Mock<ISiteService>();
            _lookupService = new Mock<ILookupService>();
            _siteGroupService = new Mock<ISiteGroupService>();
            _rolesService = new Mock<IRolesService>();
            _riskAssessorService = new Mock<IRiskAssessorService>();

            var countries = new List<CountryDto>() { };
            _lookupService.Setup(x => x.GetCountries()).Returns(countries);

            var nationalities = new List<LookupDto>() { new LookupDto { Id = 1, Name = "British" }, new LookupDto { Id = 2, Name = "Irish" } };
            _lookupService.Setup(x => x.GetNationalities()).Returns(nationalities);

            
            var sites = new List<SiteDto>();
            sites.Add(new SiteDto(){IsMainSite = true});

            _siteService
                .Setup(x => x.GetAll( It.IsAny<long>() /*_companyId*/))
                .Returns(sites);

            _siteService
                .Setup(x => x.GetByCompanyId(It.IsAny<long>()))
                .Returns(sites);

            _addUsersViewModelFactory = new Mock<IAddUsersViewModelFactory>();
            _addUsersViewModelFactory
                .Setup( x => x.WithCompanyId(It.IsAny<long>()))
                .Returns( _addUsersViewModelFactory.Object );
            
            _addUsersViewModelFactory
                .Setup( x => x.GetViewModel())
                .Returns( new AddUsersViewModel() );


            _employeeService
                .Setup(x=>x.Search(It.IsAny<SearchEmployeesRequest>()))
                .Returns(new List<EmployeeDto>());

            _rolesService
                .Setup( x=>x.GetAllRoles(It.IsAny<long>()))
                .Returns( new List<RoleDto>() );

            _siteGroupService
                .Setup(x => x.GetByCompanyId(It.IsAny<long>()))
                .Returns( new List<SiteGroupDto>());
        }

        [Test]
        public void When_get_view_model_Then_return_correct_view_model()
        {
            //Given
            var target = CreateEmployeeViewModelFactory();

            var countries = new List<CountryDto>() { };
            _lookupService.Setup(x => x.GetCountries()).Returns(countries);

            var nationalities = new List<LookupDto>() { new LookupDto { Id = 1, Name = "British" }, new LookupDto { Id = 2, Name = "Irish" } };
            _lookupService.Setup(x => x.GetNationalities()).Returns(nationalities);

            var sites = new List<SiteDto>();
            _siteService.Setup(x => x.GetAll(_companyId)).Returns(sites);

            //When
            var result = target.WithEmployeeId(_employeeId).GetViewModel();

            //Then
            Assert.That(result, Is.TypeOf<EmployeeViewModel>());
            Assert.That(result.Countries.Count(), Is.EqualTo(countries.Count()+1));
            Assert.That(result.Nationalities.Count(), Is.EqualTo(nationalities.Count()+1));
            Assert.That(result.Sites.Count(), Is.EqualTo(sites.Count()+1));
        }

        [Test]
        public void When_get_view_model_with_employee_id_has_no_value_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateEmployeeViewModelFactory();

            //When
            target.WithEmployeeId(null).GetViewModel();

            //Then
            _employeeService.VerifyAll();
            _siteService.VerifyAll();
        }

        [Test]
        public void When_get_view_model_with_employee_id_has_value_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateEmployeeViewModelFactory();

            _employeeId = Guid.NewGuid();

            var employee = new EmployeeDto();
            _employeeService.Setup(x => x.GetEmployee(_employeeId.Value, _companyId))
                            .Returns(employee);

            //When
            target.WithEmployeeId(_employeeId).GetViewModel();

            //Then
            _employeeService.VerifyAll();
            _siteService.VerifyAll();
        }

        [Test]
        public void When_get_view_model_with_employee_id_has_value_Then_should_return_correct_view_model()
        {
            //Given
            var target = CreateEmployeeViewModelFactory();

            _employeeId = Guid.NewGuid();

            var employee = new EmployeeDto()
                               {
                                   EmployeeReference = "Test Me Man",
                                   Title = "Mr",
                                   Forename = "John",
                                   Surname = "Dee",
                                   MiddleName = "Lida",
                                   PreviousSurname = "Not Set",
                                   Sex = "Female",
                                   DateOfBirth = DateTime.Today.AddYears(-20),
                                   Nationality = new NationalityDto { Id = 5 },
                                   HasDisability = true,
                                   DisabilityDescription = "Some Description",
                                   NINumber = "JN12 12 12 2",
                                   PassportNumber = "ABC",
                                   PPSNumber = "TODO",
                                   WorkVisaNumber = "0123456789",
                                   WorkVisaExpirationDate = DateTime.Today.AddYears(1),
                                   DrivingLicenseNumber = "weewewe",
                                   DrivingLicenseExpirationDate = DateTime.Today.AddDays(1),
                                   HasCompanyVehicle = true,
                                   CompanyVehicleTypeId = 1,
                                   CompanyVehicleRegistration = "KK NN M,",
                                   SiteId = 1111,
                                   JobTitle = "Momom",
                                   EmploymentStatusId = 555,
                                   MainContactDetails = new EmployeeContactDetailDto
                                   {
                                       Address1 = "Address1",
                                       Address2 = "Address2",
                                       Address3 = "Address3",
                                       Town = "the town",
                                       County = "County",
                                       Country = new CountryDto { Id = 998 },
                                       PostCode = "w78 1tr",
                                       Telephone1 = "000",
                                       Telephone2 = "222",
                                       Email = "testing@hotmail.com"
                                   }
                               };
            _employeeService.Setup(x => x.GetEmployee(_employeeId.Value, _companyId))
                            .Returns(employee);

            //When
            var result = target.WithEmployeeId(_employeeId).GetViewModel();

            //Then
            Assert.That(result.EmployeeReference, Is.EqualTo(employee.EmployeeReference));
            Assert.That(result.NameTitle, Is.EqualTo(employee.Title));
            Assert.That(result.Forename, Is.EqualTo(employee.Forename));
            Assert.That(result.Surname, Is.EqualTo(employee.Surname));
            Assert.That(result.MiddleName, Is.EqualTo(employee.MiddleName));
            Assert.That(result.PreviousSurname, Is.EqualTo(employee.PreviousSurname));
            Assert.That(result.Sex, Is.EqualTo(employee.Sex));
            Assert.That(result.DateOfBirth, Is.EqualTo(employee.DateOfBirth));
            Assert.That(result.NationalityId, Is.EqualTo(employee.Nationality.Id));
            Assert.That(result.HasDisability, Is.EqualTo(employee.HasDisability));
            Assert.That(result.DisabilityDescription, Is.EqualTo(employee.DisabilityDescription));
            Assert.That(result.NINumber, Is.EqualTo(employee.NINumber));
            Assert.That(result.PassportNumber, Is.EqualTo(employee.PassportNumber));
            Assert.That(result.PPSNumber, Is.EqualTo(employee.PPSNumber));
            Assert.That(result.WorkVisaNumber, Is.EqualTo(employee.WorkVisaNumber));
            Assert.That(result.WorkVisaExpirationDate, Is.EqualTo(employee.WorkVisaExpirationDate));
            Assert.That(result.DrivingLicenseNumber, Is.EqualTo(employee.DrivingLicenseNumber));
            Assert.That(result.DrivingLicenseExpirationDate, Is.EqualTo(employee.DrivingLicenseExpirationDate));
            Assert.That(result.HasCompanyVehicle, Is.EqualTo(employee.HasCompanyVehicle));
            Assert.That(result.CompanyVehicleTypeId, Is.EqualTo(employee.CompanyVehicleTypeId));
            Assert.That(result.CompanyVehicleRegistration, Is.EqualTo(employee.CompanyVehicleRegistration));
            Assert.That(result.SiteId, Is.EqualTo(employee.SiteId));
            Assert.That(result.JobTitle, Is.EqualTo(employee.JobTitle));
            Assert.That(result.EmploymentStatusId, Is.EqualTo(employee.EmploymentStatusId));
            Assert.That(result.Address1, Is.EqualTo(employee.MainContactDetails.Address1));
            Assert.That(result.Address2, Is.EqualTo(employee.MainContactDetails.Address2));
            Assert.That(result.Address3, Is.EqualTo(employee.MainContactDetails.Address3));
            Assert.That(result.County, Is.EqualTo(employee.MainContactDetails.County));
            Assert.That(result.CountryId, Is.EqualTo(employee.MainContactDetails.Country.Id));
            Assert.That(result.Postcode, Is.EqualTo(employee.MainContactDetails.PostCode));
            Assert.That(result.Telephone, Is.EqualTo(employee.MainContactDetails.Telephone1));
            Assert.That(result.Mobile, Is.EqualTo(employee.MainContactDetails.Telephone2));
            Assert.That(result.Email, Is.EqualTo(employee.MainContactDetails.Email));
            Assert.That(result.Town,Is.EqualTo(employee.MainContactDetails.Town));
        }

        [Test]
        public void When_get_view_model_Then_should_have_british_as_the_first_item_nationalities_dropdownlist()
        {
            //Given
            var target = CreateEmployeeViewModelFactory();

            _employeeId = Guid.NewGuid();
            var employee = new EmployeeDto();
            _employeeService.Setup(x => x.GetEmployee(_employeeId.Value, _companyId))
                            .Returns(employee);

            //When
            var result = target.WithEmployeeId(_employeeId).GetViewModel();

            //Then
            Assert.That(result.Nationalities.Skip(1).Take(1).First().label, Is.EqualTo("British"));
        }

        [Test]
        public void When_get_view_model_Then_should_have_united_kingdom_and_ireland_as_the_first_couple_items_in_countries_dropdownlist()
        {
            //Given
            var target = CreateEmployeeViewModelFactory();

            _employeeId = Guid.NewGuid();
            var employee = new EmployeeDto();
            _employeeService.Setup(x => x.GetEmployee(_employeeId.Value, _companyId))
                            .Returns(employee);

            var countries = new List<CountryDto>()
                                {
                                    new CountryDto() {Name = "Afganstan", Id = 1},
                                    new CountryDto() {Name = "Spain", Id = 2},
                                    new CountryDto() {Name = "United Kingdom", Id = 3},
                                    new CountryDto() {Name = "Egypt", Id = 4},
                                    new CountryDto() {Name = "Ireland", Id = 5}
                                };

            _lookupService.Setup(x => x.GetCountries()).Returns(countries);

            //When
            var result = target.WithEmployeeId(_employeeId).GetViewModel();

            //Then
            Assert.That(result.Countries.Skip(1).Take(1).First().label, Is.EqualTo("United Kingdom"));
            Assert.That(result.Countries.Skip(2).Take(1).First().label, Is.EqualTo("Ireland"));
        }

        [Test]
        public void Given_employee_user_record_is_deleted_When_get_view_model_Then_IsPendingRegistration_is_false()
        {
            //Given
            var target = CreateEmployeeViewModelFactory();

            var employee = new EmployeeDto()
            {
                Id = Guid.NewGuid(),
                Surname = "tests",
                User = new UserDto() { Deleted = true, IsRegistered = false }
            };
            _employeeService.Setup(x => x.GetEmployee(It.IsAny<Guid>(), It.IsAny<long>()))
                            .Returns(() => employee);

            var countries = new List<CountryDto>() { };
            _lookupService.Setup(x => x.GetCountries()).Returns(countries);

            var nationalities = new List<LookupDto>() { new LookupDto { Id = 1, Name = "British" }, new LookupDto { Id = 2, Name = "Irish" } };
            _lookupService.Setup(x => x.GetNationalities()).Returns(nationalities);

            var sites = new List<SiteDto>();
            _siteService.Setup(x => x.GetAll(_companyId)).Returns(sites);

            //When
            var result = target.WithEmployeeId(employee.Id).GetViewModel();

            //Then
            Assert.IsFalse(result.IsPendingRegistration);
        }

        [Test]
        public void Given_employee_user_record_is_not_deleted_and_not_registered_When_get_view_model_Then_IsPendingRegistration_is_false()
        {
            //Given
            var target = CreateEmployeeViewModelFactory();

            var employee = new EmployeeDto()
            {
                Id = Guid.NewGuid(),
                Surname = "tests",
                User = new UserDto() { Deleted = false, IsRegistered = false }
            };
            _employeeService.Setup(x => x.GetEmployee(It.IsAny<Guid>(), It.IsAny<long>()))
                            .Returns(() => employee);

            //When
            var result = target.WithEmployeeId(employee.Id).GetViewModel();

            //Then
            Assert.IsTrue(result.IsPendingRegistration);
        }

        [Test]
        public void Given_employee_is_risk_assessor_when_getViewModel_then_IsRiskAssessor_is_true()
        {
            //Given
            var riskAssessor = new RiskAssessorDto(){DoNotSendReviewDueNotification = true, DoNotSendTaskCompletedNotifications = true,DoNotSendTaskOverdueNotifications =  true, DoNotSendDueTomorrowNotification = true};
            riskAssessor.Site = new SiteDto(){IsMainSite = false, Name = "Winterfell", Id = 453346};

            var employee = new EmployeeDto(){ RiskAssessor = riskAssessor};

            _employeeService.Setup(x => x.GetEmployee(It.IsAny<Guid>(),It.IsAny<long>()))
                            .Returns(() => employee);

            var target = CreateEmployeeViewModelFactory();
            //When
            var result = target
                .WithEmployeeId(Guid.NewGuid())
                .GetViewModel();

            //THEN
            Assert.That(result.IsRiskAssessor, Is.True);
            Assert.That(result.DoNotSendTaskOverdueNotifications, Is.True);
            Assert.That(result.DoNotSendTaskCompletedNotifications, Is.True);
            Assert.That(result.DoNotSendReviewDueNotification, Is.True);
            Assert.That(result.IsRiskAssessorAssignedToRiskAssessments, Is.False);
            Assert.That(result.RiskAssessorSite,Is.EqualTo(riskAssessor.Site.Name));
            Assert.That(result.RiskAssessorSiteId, Is.EqualTo(riskAssessor.Site.Id));
        }

        [Test]
        public void Given_employee_is_risk_assessor_and_assigned_to_risk_assessments_when_getViewModel_then_IsRiskAssessor_is_true()
        {
            //Given
            var riskAssessor = new RiskAssessorDto() { DoNotSendReviewDueNotification = true, DoNotSendTaskCompletedNotifications = true, DoNotSendTaskOverdueNotifications = true };

            var employee = new EmployeeDto() { RiskAssessor = riskAssessor };

            _employeeService.Setup(x => x.GetEmployee(It.IsAny<Guid>(), It.IsAny<long>()))
                            .Returns(() => employee);

            _riskAssessorService
                .Setup(x => x.HasRiskAssessorGotOutstandingRiskAssessments(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(true);

            var target = CreateEmployeeViewModelFactory();

            //When
            var result = target
                .WithEmployeeId(Guid.NewGuid())
                .GetViewModel();

            //THEN
            Assert.That(result.IsRiskAssessorAssignedToRiskAssessments,Is.True);
        }

        [Test]
        public void Given_employee_is_user_when_get_view_model_user_info_is_mapped()
        {
            //Given
            var target = CreateEmployeeViewModelFactory();

            var employee = new EmployeeDto()
            {
                Id = Guid.NewGuid(),
                Surname = "tests",
                User = new UserDto() { Deleted = false, IsRegistered = false, 
                    SiteStructureElement = new SiteDto()
                        {
                            Id = 111
                        }, Role = new RoleDto() { Id = Guid.NewGuid(), Name = "Role"}
                }
            };
            _employeeService.Setup(x => x.GetEmployee(It.IsAny<Guid>(), It.IsAny<long>()))
                            .Returns(() => employee);

            //When
            var result = target.WithEmployeeId(employee.Id).GetViewModel();

            //Then
            Assert.That(result.UserSiteId, Is.EqualTo(employee.User.SiteStructureElement.Id));
            Assert.That(result.UserRoleId, Is.EqualTo(employee.User.Role.Id));
            Assert.That(result.UserPermissionsApplyToAllSites, Is.False);
        }

        [Test]
        public void Given_employee_is_user_is_assigned_to_main_site_when_get_view_model_user_UserPermissionsApplyToAllSites_equals_true()
        {
            //Given
            var target = CreateEmployeeViewModelFactory();

            var employee = new EmployeeDto()
            {
                Id = Guid.NewGuid(),
                Surname = "tests",
                User = new UserDto()
                {
                    Deleted = false,
                    IsRegistered = false,
                    SiteStructureElement = new SiteDto()
                    {
                        Id = 111, IsMainSite = true
                    },
                    Role = new RoleDto() { Id = Guid.NewGuid(), Name = "Role" }
                    ,
                }
            };
            _employeeService.Setup(x => x.GetEmployee(It.IsAny<Guid>(), It.IsAny<long>()))
                            .Returns(() => employee);

            //When
            var result = target.WithEmployeeId(employee.Id).GetViewModel();

            //Then
            Assert.That(result.UserPermissionsApplyToAllSites, Is.True);
            Assert.That(result.UserSiteId, Is.Null);
        }

        [Test]
        public void Given_user_assigned_to_site_group_then_sites_and_allSites_settings_are_null()
        {
            //Given
            var target = CreateEmployeeViewModelFactory();

            var employee = new EmployeeDto()
            {
                Id = Guid.NewGuid(),
                Surname = "tests",
                User = new UserDto()
                {
                    Deleted = false,
                    IsRegistered = false,
                    
                    SiteStructureElement = new SiteGroupDto()
                    {
                        Id = 111,
                    },
                    Role = new RoleDto() { Id = Guid.NewGuid(), Name = "Role" }
                    ,
                }
            };
            _employeeService.Setup(x => x.GetEmployee(It.IsAny<Guid>(), It.IsAny<long>()))
                            .Returns(() => employee);

            //When
            var result = target.WithEmployeeId(employee.Id).GetViewModel();

            //Then
            Assert.That(result.UserPermissionsApplyToAllSites, Is.False);
            Assert.That(result.UserSiteId, Is.Null);
            Assert.That(result.UserSiteGroupId, Is.EqualTo(employee.User.SiteStructureElement.Id));
        }

        [Test]
        public void Given_user_assigned_to_site_then_sites_group_and_allSites_settings_are_null()
        {
            //Given
            var target = CreateEmployeeViewModelFactory();

            var employee = new EmployeeDto()
            {
                Id = Guid.NewGuid(),
                Surname = "tests",
                User = new UserDto()
                {
                    Deleted = false,
                    IsRegistered = false,

                    SiteStructureElement = new SiteDto()
                    {
                        Id = 111,
                    },
                    Role = new RoleDto() { Id = Guid.NewGuid(), Name = "Role" }
                }
            };
            _employeeService.Setup(x => x.GetEmployee(It.IsAny<Guid>(), It.IsAny<long>()))
                            .Returns(() => employee);

            //When
            var result = target.WithEmployeeId(employee.Id).GetViewModel();

            //Then
            Assert.That(result.UserPermissionsApplyToAllSites, Is.False);
            Assert.That(result.UserSiteGroupId, Is.Null);
            Assert.That(result.UserSiteId, Is.EqualTo(employee.User.SiteStructureElement.Id));
        }

        [Test]
        public void Given_user_is_admin_then_role_cannot_be_changed()
        {
            //Given
            var currentUser = new Mock<ICustomPrincipal>();

            currentUser
                .Setup(x => x.UserId /*Identity.Name*/)
                .Returns(new Guid());

            var target = CreateEmployeeViewModelFactory().WithCurrentUser(currentUser.Object);
         
            var employee = new EmployeeDto()
            {
                Id = Guid.NewGuid(),
                Surname = "tests",
                User = new UserDto()
                {
                    Deleted = false,
                    IsRegistered = false,

                    SiteStructureElement = new SiteDto()
                    {
                        Id = 111,
                    },
                    Role = new RoleDto() { Id = Guid.NewGuid(), Name = "Role", Description = "User Admin" }
                }
            };
            
            _employeeService.Setup(x => x.GetEmployee(It.IsAny<Guid>(), It.IsAny<long>()))
                            .Returns(() => employee);

            //When
            var result = target.WithEmployeeId(employee.Id).GetViewModel();

            //Then
            Assert.That(result.CanChangeRoleDdl, Is.False);
        }

        [Test]
        public void Given_user_is_not_admin_then_role_can_be_changed()
        {
            //Given
            var currentUser = new Mock<ICustomPrincipal>();

            currentUser
                .Setup(x => x.UserId /*Identity.Name*/)
                .Returns(new Guid());

            var target = CreateEmployeeViewModelFactory().WithCurrentUser(currentUser.Object);

            var employee = new EmployeeDto()
            {
                Id = Guid.NewGuid(),
                Surname = "tests",
                User = new UserDto()
                {
                    Deleted = false,
                    IsRegistered = false,

                    SiteStructureElement = new SiteDto()
                    {
                        Id = 111,
                    },
                    Role = new RoleDto() { Id = Guid.NewGuid(), Name = "Role"}
                }
            };

            _employeeService.Setup(x => x.GetEmployee(It.IsAny<Guid>(), It.IsAny<long>()))
                            .Returns(() => employee);

            //When
            var result = target.WithEmployeeId(employee.Id).GetViewModel();

            //Then
            Assert.That(result.CanChangeRoleDdl, Is.True);
        }

        [Test]
        public void Given_user_is_in_role_then_get_role_description()
        {
            //Given
            var currentUser = new Mock<ICustomPrincipal>();

            currentUser
                .Setup(x => x.UserId /*Identity.Name*/)
                .Returns(new Guid());

            var target = CreateEmployeeViewModelFactory().WithCurrentUser(currentUser.Object);

            var employee = new EmployeeDto()
            {
                Id = Guid.NewGuid(),
                Surname = "tests",
                User = new UserDto()
                {
                    Deleted = false,
                    IsRegistered = false,

                    SiteStructureElement = new SiteDto()
                    {
                        Id = 111,
                    },
                    Role = new RoleDto() { Id = Guid.NewGuid(), Name = "Role", Description = "role description"}
                }
            };

            _employeeService.Setup(x => x.GetEmployee(It.IsAny<Guid>(), It.IsAny<long>()))
                            .Returns(() => employee);

            //When
            var result = target.WithEmployeeId(employee.Id).GetViewModel();

            //Then
            Assert.That(result.UserRoleDescription, Is.EqualTo(employee.User.Role.Description));
        }

        [Test]
        public void Given_user_is_deleted_then_dont_return_user_details_in_viewModel()
        {
            //Given
            var currentUser = new Mock<ICustomPrincipal>();

            currentUser
                .Setup(x => x.UserId /*Identity.Name*/)
                .Returns(new Guid());

            var target = CreateEmployeeViewModelFactory().WithCurrentUser(currentUser.Object);

            var employee = new EmployeeDto()
            {
                Id = Guid.NewGuid(),
                Surname = "tests",
                User = new UserDto()
                {
                    Deleted = true,
                    IsRegistered = false,

                    SiteStructureElement = new SiteDto()
                    {
                        Id = 111,
                    },
                    Role = new RoleDto() { Id = Guid.NewGuid(), Name = "Role", Description = "role description" }
                }
            };

            _employeeService.Setup(x => x.GetEmployee(It.IsAny<Guid>(), It.IsAny<long>()))
                            .Returns(() => employee);

            //When
            var result = target.WithEmployeeId(employee.Id).GetViewModel();

            //Then
            Assert.That(result.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(result.UserPermissionsApplyToAllSites, Is.EqualTo(false));
            Assert.That(result.UserRoleDescription, Is.EqualTo(null));
            Assert.That(result.UserRoleId, Is.EqualTo(null));
            Assert.That(result.UserSiteGroupId, Is.EqualTo(null));
        }


        private EmployeeViewModelFactory CreateEmployeeViewModelFactory()
        {
            return new EmployeeViewModelFactory( _employeeService.Object, _siteService.Object, _lookupService.Object, _siteGroupService.Object, _rolesService.Object, _riskAssessorService.Object);
        }
    }
}