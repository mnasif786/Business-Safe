using System;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class EmployeeContactDetailDto
    {
        public long Id { get; set; }
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string Address3 { get; set; }
        public virtual string Town { get; set; }
        public virtual string County { get; set; }
        public virtual string PostCode { get; set; }
        public virtual string Telephone1 { get; set; }
        public virtual string Telephone2 { get; set; }
        public virtual int PreferedTelephone { get; set; }
        public virtual string Email { get; set; }
        public virtual CountryDto Country { get; set; }
    }
}
