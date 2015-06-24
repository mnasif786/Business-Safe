using System;

namespace BusinessSafe.Application.Request
{
    public class EmployeeEmergencyContactRequest
    {
        public string EmployeeId { get; set; }
        public bool SameEmployeeAddress { get; set; }
        public int EmergencyContactId { get; set; }
        public virtual string Title { get; set; }
        public virtual string Forename { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Relationship { get; set; }
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string Address3 { get; set; }
        public virtual string Town { get; set; }
        public virtual string County { get; set; }
        public int CountryId { get; set; }
        public virtual string PostCode { get; set; }
        public virtual string Telephone1 { get; set; }
        public virtual string Telephone2 { get; set; }
        public virtual string Telephone3 { get; set; }
        public virtual int PreferedTelephone { get; set; }
        public Guid UserAddingOrUpdatingId { get; set; }
    }
}