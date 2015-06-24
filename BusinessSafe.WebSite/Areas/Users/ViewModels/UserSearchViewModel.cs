using System.Collections.Generic;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Users.ViewModels
{
    public class UserSearchViewModel
    {
        public long CompanyId { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public IEnumerable<AutoCompleteViewModel> Sites { get; set; }
        public List<UsersSearchUserViewModel> Users { get; set; }
        public long SiteId { get; set; }
        public long GroupSiteId { get; set; }
        public bool IsRegistered { get; set; }
    }
}