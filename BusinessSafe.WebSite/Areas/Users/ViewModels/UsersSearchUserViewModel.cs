namespace BusinessSafe.WebSite.Areas.Users.ViewModels
{
    public class UsersSearchUserViewModel
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeReference { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string JobTitle { get; set; }
        public string SiteName { get; set; }
        public string Role { get; set; }
        public bool IsDeleted { get; set; }
        public int IsRegistered { get; set; }
        public bool ShowDeleteButton { get; set; }
        public bool ShowResetUserRegistrationButton { get; set; }

    }
}