using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class EmployeeDtoMapper
    {
        public EmployeeDto Map(Employee employee)
        {
            if (employee == null)
            {
                return null;
            }

            var employeeDto = new EmployeeDto
            {
                Id = employee.Id,
                EmployeeReference = employee.EmployeeReference,
                Title = employee.Title,
                Forename = employee.Forename,
                Surname = employee.Surname,
                MiddleName = employee.MiddleName,
                PreviousSurname = employee.PreviousSurname,
                Sex = employee.Sex,
                DateOfBirth = employee.DateOfBirth.GetValueOrDefault(),
                HasDisability = employee.HasDisability.GetValueOrDefault(),
                DisabilityDescription = employee.DisabilityDescription,
                SiteId = employee.Site != null ? employee.Site.Id : 0,
                SiteName = employee.Site != null ? employee.Site.Name : string.Empty,
                JobTitle = employee.JobTitle,
                EmploymentStatusId = employee.EmploymentStatus != null ? employee.EmploymentStatus.Id : 0,
                NINumber = employee.NINumber,
                PassportNumber = employee.PassportNumber,
                PPSNumber = employee.PPSNumber,
                WorkVisaNumber = employee.WorkVisaNumber,
                WorkVisaExpirationDate = employee.WorkVisaExpirationDate.HasValue ? employee.WorkVisaExpirationDate.Value : (DateTime?)null,
                DrivingLicenseNumber = employee.DrivingLicenseNumber,
                DrivingLicenseExpirationDate = employee.DrivingLicenseExpirationDate.HasValue ? employee.DrivingLicenseExpirationDate.Value : (DateTime?)null,
                HasCompanyVehicle = employee.HasCompanyVehicle.GetValueOrDefault(),
                CompanyVehicleTypeId = employee.CompanyVehicleType != null ? employee.CompanyVehicleType.Id : 0,
                CompanyVehicleType = employee.CompanyVehicleType != null ? employee.CompanyVehicleType.Name : string.Empty,
                CompanyVehicleRegistration = employee.CompanyVehicleRegistration,
                Deleted = employee.Deleted,
                FullName = employee.FullName,
                CanChangeEmail = employee.CanChangeEmail,
                NotificationFrequency = employee.NotificationFrequecy,
                NotificationType = MapNotificationType(employee.NotificationType)
            };

            return employeeDto;
        }

        private NotificationTypeDto MapNotificationType(NotificationType notification)
        {
            switch (notification)
            {
                case NotificationType.Daily: return NotificationTypeDto.Daily;
                case NotificationType.Weekly: return NotificationTypeDto.Weekly;
                case NotificationType.Monthly: return NotificationTypeDto.Monthly;
                default: return NotificationTypeDto.Daily;
            }
        }


        public EmployeeDto MapWithUser(Employee employee)
        {
            if (employee == null)
            {
                return new EmployeeDto();
            }
            var dto = Map(employee);
            dto.User = new UserDtoMapper().Map(employee.User);
            return dto;
        }

        public EmployeeDto MapWithNationality(Employee employee)
        {
            var employeeDto = Map(employee);

            employeeDto.Nationality = employee.Nationality != null
                                          ? new NationalityDtoMapper().Map(employee.Nationality)
                                          : null;

            return employeeDto;
        }

        public EmployeeDto MapWithNationalityAndContactDetails(Employee employee)
        {
            var employeeDto = MapWithNationality(employee);

            employeeDto.MainContactDetails = employee.MainContactDetails != null
                                                 ? new EmployeeContactDetailDtoMapper().MapWithCountry(employee.MainContactDetails)
                                                 : null;

            return employeeDto;
        }

        public EmployeeDto MapWithNationalityAndContactDetailsAndEmergencyContactDetails(Employee employee)
        {
            var dto = MapWithNationalityAndContactDetails(employee);
            dto.EmergencyContactDtos = MapEmergencyContactDetails(employee.EmergencyContactDetails);           
            return dto;
        }


        public EmployeeDto MapWithNationalityAndContactDetailsAndEmergencyContactDetailsAndUser(Employee employee)
        {
            var dto = MapWithNationalityAndContactDetailsAndEmergencyContactDetails(employee);
            if (employee.User != null)
            {
                dto.User = new UserDtoMapper().Map(employee.User);
            }
            return dto;
        }

        public EmployeeDto MapWithContactDetailsAndEmergencyContactDetailsAndUser(Employee employee)
        {
            var employeeDto = MapWithNationalityAndContactDetailsAndEmergencyContactDetails(employee);
            if (employee.User != null) employeeDto.User = new UserDtoMapper().MapIncludingRoleAndSite(employee.User);
            employeeDto.RiskAssessor = employee.RiskAssessor !=null && !employee.RiskAssessor.Deleted ? new RiskAssessorDtoMapper().Map(employee.RiskAssessor): null;
            return employeeDto;
        }

        public IEnumerable<EmployeeDto> Map(IEnumerable<Employee> employees)
        {
            return employees.Select(Map).ToList();
        }

        public IEnumerable<EmployeeDto> MapWithNationality(IEnumerable<Employee> employees)
        {
            return employees.Select(MapWithNationality).ToList();
        }

        public IEnumerable<EmployeeDto> MapWithNationalityAndContactDetails(IEnumerable<Employee> employees)
        {
            return employees.Select(MapWithNationalityAndContactDetails).ToList();
        }

        public IEnumerable<EmployeeDto> MapWithNationalityAndContactDetailsAndEmergencyContactDetails(IEnumerable<Employee> employees)
        {
            return employees.Select(MapWithNationalityAndContactDetailsAndEmergencyContactDetails).ToList();
        }

        public IEnumerable<EmployeeDto> MapWithNationalityAndContactDetailsAndEmergencyContactDetailsAndUser(IEnumerable<Employee> employees)
        {
            return employees.Select(MapWithNationalityAndContactDetailsAndEmergencyContactDetailsAndUser).ToList();
        }
        

        //ToDo: refactor this into its own mapper.
        private List<EmployeeEmergencyContactDetailDto> MapEmergencyContactDetails(IEnumerable<EmployeeEmergencyContactDetail> emergencyContactDetails)
        {
            return emergencyContactDetails.Select(emergencyContact => new EmployeeEmergencyContactDetailDto
                                                                          {
                                                                              EmergencyContactId = emergencyContact.Id, 
                                                                              Title = emergencyContact.Title, 
                                                                              Forename = emergencyContact.Forename, 
                                                                              Surname = emergencyContact.Surname, 
                                                                              Relationship = emergencyContact.Relationship, 
                                                                              SameAddressAsEmployee = emergencyContact.SameAddressAsEmployee,
                                                                              Address1 = emergencyContact.Address1, 
                                                                              Address2 = emergencyContact.Address2, 
                                                                              Address3 = emergencyContact.Address3, 
                                                                              Town = emergencyContact.Town,
                                                                              PostCode = emergencyContact.PostCode, 
                                                                              County = emergencyContact.County, 
                                                                              CountryId = emergencyContact.Country != null ? emergencyContact.Country.Id : 0, 
                                                                              WorkTelephone = emergencyContact.Telephone1, 
                                                                              HomeTelephone = emergencyContact.Telephone2, 
                                                                              MobileTelephone = emergencyContact.Telephone3,
                                                                              PreferredContactNumber = emergencyContact.PreferedTelephone
                                                                          }).ToList();
        }
           
    }

    /// <summary>
    /// Als experiment
    ///  new EmployeeMapperFactory(employee)
                //.WithNationality()
                //.WithContactDetails()
                //.Map();
    /// </summary>
    public class EmployeeMapperFactory
    {
        private EmployeeDto _employeeDto;
        private Employee _employee;
        public EmployeeMapperFactory(Employee employee)
        {
            _employee = employee;
            _employeeDto = new EmployeeDto
                               {
                                   Id = _employee.Id,
                                   EmployeeReference = _employee.EmployeeReference,
                                   Title = _employee.Title,
                                   Forename = _employee.Forename,
                                   Surname = _employee.Surname,
                                   MiddleName = _employee.MiddleName,
                                   PreviousSurname = _employee.PreviousSurname,
                                   Sex = _employee.Sex,
                                   DateOfBirth = _employee.DateOfBirth.GetValueOrDefault(),
                                   HasDisability = _employee.HasDisability.GetValueOrDefault(),
                                   DisabilityDescription = _employee.DisabilityDescription,
                                   SiteId = _employee.Site != null ? _employee.Site.Id : 0,
                                   SiteName = _employee.Site != null ? _employee.Site.Name : string.Empty,
                                   JobTitle = _employee.JobTitle,
                                   EmploymentStatusId = _employee.EmploymentStatus != null ? _employee.EmploymentStatus.Id : 0,
                                   NINumber = _employee.NINumber,
                                   PassportNumber = _employee.PassportNumber,
                                   PPSNumber = _employee.PPSNumber,
                                   WorkVisaNumber = _employee.WorkVisaNumber,
                                   WorkVisaExpirationDate = _employee.WorkVisaExpirationDate.HasValue ? _employee.WorkVisaExpirationDate.Value : (DateTime?) null,
                                   DrivingLicenseNumber = _employee.DrivingLicenseNumber,
                                   DrivingLicenseExpirationDate = _employee.DrivingLicenseExpirationDate.HasValue ? _employee.DrivingLicenseExpirationDate.Value : (DateTime?) null,
                                   HasCompanyVehicle = _employee.HasCompanyVehicle.GetValueOrDefault(),
                                   CompanyVehicleTypeId = _employee.CompanyVehicleType != null ? _employee.CompanyVehicleType.Id : 0,
                                   CompanyVehicleType = _employee.CompanyVehicleType != null ? _employee.CompanyVehicleType.Name : string.Empty,
                                   CompanyVehicleRegistration = _employee.CompanyVehicleRegistration,
                                   Deleted = _employee.Deleted,
                                   FullName = _employee.FullName,
                                   CanChangeEmail = _employee.CanChangeEmail,
                               };

        }

        public EmployeeDto Map()
        {
            return _employeeDto;
        }

        public EmployeeMapperFactory WithNationality()
        {

            _employeeDto.Nationality = _employee.Nationality != null
                                          ? new NationalityDtoMapper().Map(_employee.Nationality)
                                          : null;

            return this;
        }

        public EmployeeMapperFactory WithContactDetails()
        {
            _employeeDto.MainContactDetails = _employee.MainContactDetails != null
                                                 ? new EmployeeContactDetailDtoMapper().MapWithCountry(_employee.MainContactDetails)
                                                 : null;

            return this;
        }


    }
}