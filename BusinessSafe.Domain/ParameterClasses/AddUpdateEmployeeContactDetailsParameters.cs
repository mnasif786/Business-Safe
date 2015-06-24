using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.ParameterClasses
{
    public class AddUpdateEmployeeContactDetailsParameters
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
        public string Telephone1 { get; set; }
        public string Telephone2 { get; set; }
        public int PreferedTelephone { get; set; }
        public string Email { get; set; }
        public Country Country { get; set; }
        public long Id { get; set; }
    }
}