using System;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Constants;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Mappers
{
    public interface IEmployeeParametersMapper
    {
        AddUpdateEmployeeParameters Map(AddEmployeeRequest request);
        AddUpdateEmployeeParameters Map(UpdateEmployeeRequest request);
    }

    public class EmployeeParametersMapper : IEmployeeParametersMapper
    {
        private readonly INationalityRepository _nationalityRepository;
        private readonly ICompanyVehicleTypeRepository _companyVehicleTypeRepository;
        private readonly IEmploymentStatusRepository _employmentStatusRepository;
        private readonly ISiteRepository _siteRepository;
        
        public EmployeeParametersMapper(INationalityRepository nationalityRepository,
                                        ICompanyVehicleTypeRepository companyVehicleTypeRepository,
                                        IEmploymentStatusRepository employmentStatusRepository, 
                                        ISiteRepository siteRepository)
        {
            _nationalityRepository = nationalityRepository;
            _companyVehicleTypeRepository = companyVehicleTypeRepository;
            _employmentStatusRepository = employmentStatusRepository;
            _siteRepository = siteRepository;
        }

        public AddUpdateEmployeeParameters Map(AddEmployeeRequest request)
        {
            var employeeParameters = CreateEmployeeParameters(request);
            return employeeParameters;
        }

        public AddUpdateEmployeeParameters Map(UpdateEmployeeRequest request)
        {
            var employeeParameters = CreateEmployeeParameters(request);
            return employeeParameters;
        }

        private AddUpdateEmployeeParameters CreateEmployeeParameters(SaveEmployeeRequest request)
        {
            var result = new AddUpdateEmployeeParameters()
                                                        {
                                                            EmployeeReference = request.EmployeeReference,
                                                            Title = request.Title,
                                                            Forename = request.Forename,
                                                            Surname = request.Surname,
                                                            MiddleName = request.MiddleName,
                                                            PreviousSurname = request.PreviousSurname,
                                                            Sex = request.Sex,
                                                            HasDisability = request.HasDisability,
                                                            DisabilityDescription = request.DisabilityDescription,
                                                            SiteId = request.SiteId,
                                                            JobTitle = request.JobTitle,
                                                            NINumber = request.NINumber,
                                                            PassportNumber = request.PassportNumber,
                                                            PPSNumber = request.PPSNumber,
                                                            WorkVisaNumber = request.WorkVisaNumber,
                                                            DrivingLicenseNumber = request.DrivingLicenseNumber,
                                                            HasCompanyVehicle = request.HasCompanyVehicle,
                                                            CompanyVehicleRegistration = request.CompanyVehicleRegistration,
                                                            ClientId = request.CompanyId,
                                                            NotificationType = MapNotificationType(request.NotificationType),
                                                            NotificationFrequency =  request.NotificiationFrequency
                                                        };
            if (request.DateOfBirth.HasValue && request.DateOfBirth.Value != DateTime.MinValue)
            {
                result.DateOfBirth = request.DateOfBirth.Value;
            }
            if (request.WorkVisaExpirationDate.HasValue && request.WorkVisaExpirationDate.Value != DateTime.MinValue)
            {
                result.WorkVisaExpirationDate = request.WorkVisaExpirationDate.Value;
            }
            if (request.DrivingLicenseExpirationDate.HasValue && request.DrivingLicenseExpirationDate.Value != DateTime.MinValue)
            {
                result.DrivingLicenseExpirationDate = request.DrivingLicenseExpirationDate.Value;
            }
            if (request.NationalityId.HasValue)
            {
                result.Nationality = _nationalityRepository.LoadById(request.NationalityId.Value);
            }
            if (request.CompanyVehicleTypeId.HasValue && request.CompanyVehicleTypeId.Value > 0)
            {
                result.CompanyVehicleType = _companyVehicleTypeRepository.LoadById(request.CompanyVehicleTypeId.Value);
            }
            if (request.EmploymentStatusId.HasValue)
            {
                result.EmploymentStatus = _employmentStatusRepository.LoadById(request.EmploymentStatusId.Value);
            }
            if (request.SiteId.HasValue)
            {
                result.Site = _siteRepository.GetById(request.SiteId.Value);
            }
            return result;
        }

        private NotificationTypeParameters MapNotificationType(NotificationTypeDto notification)
        {
            switch (notification)
            {
                case NotificationTypeDto.Daily: return NotificationTypeParameters.Daily;
                case NotificationTypeDto.Weekly: return NotificationTypeParameters.Weekly;
                case NotificationTypeDto.Monthly: return NotificationTypeParameters.Monthly;
                default: return NotificationTypeParameters.Daily;
            }
        }
    }
}