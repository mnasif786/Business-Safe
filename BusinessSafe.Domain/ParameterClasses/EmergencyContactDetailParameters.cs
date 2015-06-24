using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.ParameterClasses
{
    public class EmergencyContactDetailParameters
    {
        public long EmergencyContactId { get; set; }
        public virtual string Title { get; set; }
        public virtual string Forename { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Relationship { get; set; }
        public virtual bool SameAddressAsEmployee { get; set; }
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string Address3 { get; set; }
        public virtual string Town { get; set; }
        public virtual string County { get; set; }
        public virtual string PostCode { get; set; }
        public virtual string Telephone1 { get; set; }
        public virtual string Telephone2 { get; set; }
        public virtual string Telephone3 { get; set; }
        public virtual int PreferedTelephone { get; set; }
        public virtual Country Country { get; set; }
    }
}