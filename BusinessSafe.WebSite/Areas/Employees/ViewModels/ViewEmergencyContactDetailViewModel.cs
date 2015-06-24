using System.Web;
namespace BusinessSafe.WebSite.Areas.Employees.ViewModels
{
    public class ViewEmergencyContactDetailViewModel
    {
        public string Title { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Relationship { get; set; }
        public string WorkTelephone { get; set; }
        public string HomeTelephone { get; set; }
        public string MobileTelephone { get; set; }
        public bool SameAddressAsEmployee { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }

        ////todo: do this in a better way
        //public HtmlString IsPreferredContactNumber(string value)
        //{
        //    if (PreferredContactNumber.ToString() == value)
        //        return new HtmlString("checked='checked'");

        //    return new HtmlString("");
        //}
    }
}