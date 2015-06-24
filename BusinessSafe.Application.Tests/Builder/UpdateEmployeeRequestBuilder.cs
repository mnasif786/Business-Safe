using System;
using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Tests.Builder
{
    public class UpdateEmployeeRequestBuilder
    {
        private UpdateEmployeeRequest _request;
        private string _title;
        private string _forename;
        private string _surname;
        private string _employeeReference;
        private string _middleName;
        private string _previousSurname;
        private string _sex;
        private string _dob = "01/01/1920";
        private int _nationalityId;
        private bool _hasDisability;
        private string _disabilityDescription;
        private string _niNumber;
        private string _passportNumber;
        private string _pps;
        private string _workingVisaNumber;
        private DateTime? _workingVisaExpirationDate;
        private string _drivingLicenseNumber;
        private DateTime? _drivingLicenseExpirationDate;
        private bool _hasCompanyVehicle;
        private string _registration;
        private int _companyVehicleTypeId;
        private int _siteId;
        private string _jobTitle;
        private int _employmentStatusId;
        private string _postcode;
        private string _county;
        private string _address3;
        private string _address2;
        private string _address1;
        private int _countryId;
        private string _telephone;
        private string _mobile;
        private string _email;
        private Guid _userId;
        private Guid _employeeId;

        public static UpdateEmployeeRequestBuilder Create()
        {
            return new UpdateEmployeeRequestBuilder();
        }

        public UpdateEmployeeRequest Build()
        {
            _request = new UpdateEmployeeRequest
                           {
                               EmployeeReference = _employeeReference,
                               Title = _title,
                               Forename = _forename,
                               Surname = _surname,
                               MiddleName = _middleName,
                               PreviousSurname = _previousSurname,
                               Sex = _sex,
                               DateOfBirth = DateTime.Parse(_dob),
                               NationalityId = _nationalityId,
                               HasDisability = _hasDisability,
                               DisabilityDescription = _disabilityDescription,
                               NINumber = _niNumber,
                               PassportNumber = _passportNumber,
                               PPSNumber = _pps,
                               WorkVisaNumber = _workingVisaNumber,
                               WorkVisaExpirationDate = _workingVisaExpirationDate,
                               DrivingLicenseNumber = _drivingLicenseNumber,
                               DrivingLicenseExpirationDate = _drivingLicenseExpirationDate,
                               HasCompanyVehicle = _hasCompanyVehicle,
                               CompanyVehicleRegistration = _registration,
                               CompanyVehicleTypeId = _companyVehicleTypeId,
                               SiteId = _siteId,
                               JobTitle = _jobTitle,
                               EmploymentStatusId = _employmentStatusId,
                               Address1 = _address1,
                               Address2 = _address2,
                               Address3 = _address3,
                               County = _county,
                               Postcode = _postcode,
                               CountryId = _countryId,
                               Telephone = _telephone,
                               Mobile = _mobile,
                               Email = _email,
                               UserId = _userId,
                               EmployeeId = _employeeId
                           };
            return _request;
        }

        public UpdateEmployeeRequestBuilder WithEmployeeReference(string employeeReference)
        {
            _employeeReference = employeeReference;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithForename(string forname)
        {
            _forename = forname;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithSurname(string surname)
        {
            _surname = surname;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithMiddleName(string middleName)
        {
            _middleName = middleName;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithPreviousSurname(string previousSurname)
        {
            _previousSurname = previousSurname;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithSex(string sex)
        {
            _sex = sex;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithDOB(string dob)
        {
            _dob = dob;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithNationalityId(int nationalityId)
        {
            _nationalityId = nationalityId;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithHasDisability(bool hasDisability)
        {
            _hasDisability = hasDisability;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithDisabilityDescription(string disabilityDescription)
        {
            _disabilityDescription = disabilityDescription;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithNINumber(string niNumber)
        {
            _niNumber = niNumber;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithPassportNumber(string passportNumber)
        {
            _passportNumber = passportNumber;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithPPS(string pps)
        {
            _pps = pps;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithWorkingVisaNumber(string workingVisaNumber)
        {
            _workingVisaNumber = workingVisaNumber;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithWorkingVisaExpirationDate(DateTime? workingVisaExpirationDate)
        {
            _workingVisaExpirationDate = workingVisaExpirationDate;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithDrivingLicenseNumber(string drivingLicenseNumber)
        {
            _drivingLicenseNumber = drivingLicenseNumber;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithDrivingLicenseExpirationDate(DateTime? drivingLicenseExpirationDate)
        {
            _drivingLicenseExpirationDate = drivingLicenseExpirationDate;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithHasCompanyVehicle(bool hasCompanyVehicle)
        {
            _hasCompanyVehicle = hasCompanyVehicle;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithCompanyVehicleRegistration(string registration)
        {
            _registration = registration;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithCompanyVehicleTypeId(int vehicleId)
        {
            _companyVehicleTypeId = vehicleId;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithSiteId(int siteId)
        {
            _siteId = siteId;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithJobTitle(string jobTitle)
        {
            _jobTitle = jobTitle;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithEmploymentStatusId(int employmentStatusId)
        {
            _employmentStatusId = employmentStatusId;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithAddress1(string address1)
        {
            _address1 = address1;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithAddress2(string address2)
        {
            _address2 = address2;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithAddress3(string address3)
        {
            _address3 = address3;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithCounty(string county)
        {
            _county = county;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithPostcode(string postcode)
        {
            _postcode = postcode;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithCountryId(int countryId)
        {
            _countryId = countryId;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithTelephone(string telephone)
        {
            _telephone = telephone;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithMobile(string mobile)
        {
            _mobile = mobile;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithUserId(Guid userId)
        {
            _userId = userId;
            return this;
        }

        public UpdateEmployeeRequestBuilder WithEmployeeId(Guid employeeId)
        {
            _employeeId = employeeId;
            return this;
        }
    }
}