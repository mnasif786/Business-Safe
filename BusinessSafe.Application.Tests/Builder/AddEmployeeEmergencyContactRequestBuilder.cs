using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Tests.Builder
{
    public class AddEmployeeEmergencyContactRequestBuilder
    {
        private int _emergencyContactId;
        private bool _sameEmployeeAddress;
        private string _title;
        private string _forename;
        private string _surname;
        private string _relationship;
        private string _address1 = default(string);
        private string _address2 = default(string);
        private string _address3 = default(string);
        private string _town;
        private string _county;
        private int _countryId;
        private string _postCode;
        private string _workTelephone;
        private string _homeTelephone;
        private string _mobileTelephone;
        private int _preferredContactNumber;
        private string _employeeId;

        public static AddEmployeeEmergencyContactRequestBuilder Create()
        {
            return new AddEmployeeEmergencyContactRequestBuilder();
        }

        public EmployeeEmergencyContactRequest Build()
        {
            return new EmployeeEmergencyContactRequest
                       {
                           EmergencyContactId = _emergencyContactId,
                           Address1 = _address1,
                           Address2 = _address2,
                           Address3 = _address3,
                           Title = _title,
                           Forename = _forename,
                           Surname = _surname,
                           Town = _town,
                           County = _county,
                           CountryId = _countryId,
                           Relationship = _relationship,
                           PostCode = _postCode,
                           Telephone1 = _workTelephone,
                           Telephone2 = _homeTelephone,
                           Telephone3 = _mobileTelephone,
                           PreferedTelephone = _preferredContactNumber,
                           SameEmployeeAddress = _sameEmployeeAddress,
                           EmployeeId = _employeeId
                       };
        }

        public AddEmployeeEmergencyContactRequestBuilder WithEmergencyContactId(int emergencyContactId)
        {
            _emergencyContactId = emergencyContactId;
            return this;
        }

        public AddEmployeeEmergencyContactRequestBuilder WithEmployeeAddress(bool sameEmployeeAddress)
        {
            _sameEmployeeAddress = sameEmployeeAddress;
            return this;
        }

        public AddEmployeeEmergencyContactRequestBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public AddEmployeeEmergencyContactRequestBuilder WithForename(string emergencyContactForename)
        {
            _forename = emergencyContactForename;
            return this;
        }

        public AddEmployeeEmergencyContactRequestBuilder WithSurname(string emergencyContactSurname)
        {
            _surname = emergencyContactSurname;
            return this;
        }

        public AddEmployeeEmergencyContactRequestBuilder WithRelationship(string relationship)
        {
            _relationship = relationship;
            return this;
        }

        public AddEmployeeEmergencyContactRequestBuilder WithAddress1(string address1)
        {
            _address1 = address1;
            return this;
        }

        public AddEmployeeEmergencyContactRequestBuilder WithAddress2(string address2)
        {
            _address2 = address2;
            return this;
        }

        public AddEmployeeEmergencyContactRequestBuilder WithAddress3(string address3)
        {
            _address3 = address3;
            return this;
        }

        public AddEmployeeEmergencyContactRequestBuilder WithTown(string town)
        {
            _town = town;
            return this;
        }

        public AddEmployeeEmergencyContactRequestBuilder WithCounty(string county)
        {
            _county = county;
            return this;
        }

        public AddEmployeeEmergencyContactRequestBuilder WithCountryId(int countryId)
        {
            _countryId = countryId;
            return this;
        }

        public AddEmployeeEmergencyContactRequestBuilder WithPostCode(string postCode)
        {
            _postCode = postCode;
            return this;
        }

        public AddEmployeeEmergencyContactRequestBuilder WithWorkTelephone(string workTelephone)
        {
            _workTelephone = workTelephone;
            return this;
        }

        public AddEmployeeEmergencyContactRequestBuilder WithHomeTelephone(string homeTelephone)
        {
            _homeTelephone = homeTelephone;
            return this;
        }

        public AddEmployeeEmergencyContactRequestBuilder WithMobileTelephone(string mobileTelephone)
        {
            _mobileTelephone = mobileTelephone;
            return this;
        }

        public AddEmployeeEmergencyContactRequestBuilder WithPreferredContactNumber(int preferredContactNumber)
        {
            _preferredContactNumber = preferredContactNumber;
            return this;
        }

        public AddEmployeeEmergencyContactRequestBuilder WithEmployeeId(string employeeId)
        {
            _employeeId = employeeId;
            return this;
        }
    }
}