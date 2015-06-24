using System;
using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Tests.Builder
{
    public class AddEmployeeRequestBuilder
    {
        private AddEmployeeRequest _request;
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

        public static AddEmployeeRequestBuilder Create()
        {
            return new AddEmployeeRequestBuilder();
        }

        public AddEmployeeRequest Build()
        {
            _request = new AddEmployeeRequest
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
                               UserId = _userId
                           };
            return _request;
        }

        public AddEmployeeRequestBuilder WithEmployeeReference(string employeeReference)
        {
            _employeeReference = employeeReference;
            return this;
        }

        public AddEmployeeRequestBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public AddEmployeeRequestBuilder WithForename(string forname)
        {
            _forename = forname;
            return this;
        }

        public AddEmployeeRequestBuilder WithSurname(string surname)
        {
            _surname = surname;
            return this;
        }

        public AddEmployeeRequestBuilder WithMiddleName(string middleName)
        {
            _middleName = middleName;
            return this;
        }

        public AddEmployeeRequestBuilder WithPreviousSurname(string previousSurname)
        {
            _previousSurname = previousSurname;
            return this;
        }

        public AddEmployeeRequestBuilder WithSex(string sex)
        {
            _sex = sex;
            return this;
        }

        public AddEmployeeRequestBuilder WithDOB(string dob)
        {
            _dob = dob;
            return this;
        }

        public AddEmployeeRequestBuilder WithNationalityId(int nationalityId)
        {
            _nationalityId = nationalityId;
            return this;
        }

        public AddEmployeeRequestBuilder WithHasDisability(bool hasDisability)
        {
            _hasDisability = hasDisability;
            return this;
        }

        public AddEmployeeRequestBuilder WithDisabilityDescription(string disabilityDescription)
        {
            _disabilityDescription = disabilityDescription;
            return this;
        }

        public AddEmployeeRequestBuilder WithNINumber(string niNumber)
        {
            _niNumber = niNumber;
            return this;
        }

        public AddEmployeeRequestBuilder WithPassportNumber(string passportNumber)
        {
            _passportNumber = passportNumber;
            return this;
        }

        public AddEmployeeRequestBuilder WithPPS(string pps)
        {
            _pps = pps;
            return this;
        }

        public AddEmployeeRequestBuilder WithWorkingVisaNumber(string workingVisaNumber)
        {
            _workingVisaNumber = workingVisaNumber;
            return this;
        }

        public AddEmployeeRequestBuilder WithWorkingVisaExpirationDate(DateTime? workingVisaExpirationDate)
        {
            _workingVisaExpirationDate = workingVisaExpirationDate;
            return this;
        }

        public AddEmployeeRequestBuilder WithDrivingLicenseNumber(string drivingLicenseNumber)
        {
            _drivingLicenseNumber = drivingLicenseNumber;
            return this;
        }

        public AddEmployeeRequestBuilder WithDrivingLicenseExpirationDate(DateTime? drivingLicenseExpirationDate)
        {
            _drivingLicenseExpirationDate = drivingLicenseExpirationDate;
            return this;
        }

        public AddEmployeeRequestBuilder WithHasCompanyVehicle(bool hasCompanyVehicle)
        {
            _hasCompanyVehicle = hasCompanyVehicle;
            return this;
        }

        public AddEmployeeRequestBuilder WithCompanyVehicleRegistration(string registration)
        {
            _registration = registration;
            return this;
        }

        public AddEmployeeRequestBuilder WithCompanyVehicleTypeId(int vehicleId)
        {
            _companyVehicleTypeId = vehicleId;
            return this;
        }

        public AddEmployeeRequestBuilder WithSiteId(int siteId)
        {
            _siteId = siteId;
            return this;
        }

        public AddEmployeeRequestBuilder WithJobTitle(string jobTitle)
        {
            _jobTitle = jobTitle;
            return this;
        }

        public AddEmployeeRequestBuilder WithEmploymentStatusId(int employmentStatusId)
        {
            _employmentStatusId = employmentStatusId;
            return this;
        }

        public AddEmployeeRequestBuilder WithAddress1(string address1)
        {
            _address1 = address1;
            return this;
        }

        public AddEmployeeRequestBuilder WithAddress2(string address2)
        {
            _address2 = address2;
            return this;
        }

        public AddEmployeeRequestBuilder WithAddress3(string address3)
        {
            _address3 = address3;
            return this;
        }

        public AddEmployeeRequestBuilder WithCounty(string county)
        {
            _county = county;
            return this;
        }

        public AddEmployeeRequestBuilder WithPostcode(string postcode)
        {
            _postcode = postcode;
            return this;
        }

        public AddEmployeeRequestBuilder WithCountryId(int countryId)
        {
            _countryId = countryId;
            return this;
        }

        public AddEmployeeRequestBuilder WithTelephone(string telephone)
        {
            _telephone = telephone;
            return this;
        }

        public AddEmployeeRequestBuilder WithMobile(string mobile)
        {
            _mobile = mobile;
            return this;
        }

        public AddEmployeeRequestBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public AddEmployeeRequestBuilder WithUserId(Guid userId)
        {
            _userId = userId;
            return this;
        }
    }
}