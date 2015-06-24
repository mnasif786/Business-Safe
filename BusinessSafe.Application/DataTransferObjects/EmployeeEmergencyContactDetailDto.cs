namespace BusinessSafe.Application.DataTransferObjects
{
    public class EmployeeEmergencyContactDetailDto
    {
        public int EmergencyContactId { get; set; }
        //public bool SameEmployeeAddress { get; set; }
        public string Title { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Relationship { get; set; }
        public bool SameAddressAsEmployee { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public int CountryId { get; set; }
        public CountryDto Country { get; set; }
        public string PostCode { get; set; }
        public string WorkTelephone { get; set; }
        public string HomeTelephone { get; set; }
        public string MobileTelephone { get; set; }
        public int PreferredContactNumber { get; set; }
    }
}