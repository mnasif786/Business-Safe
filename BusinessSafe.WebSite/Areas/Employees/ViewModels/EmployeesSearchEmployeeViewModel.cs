namespace BusinessSafe.WebSite.Areas.Employees.ViewModels
{
    public class EmployeesSearchEmployeeViewModel
    {
        public string Id { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string JobTitle { get; set; }
        public string OrgUnit { get; set; }
        public string Site { get; set; }
        public string Status { get; set; }
        public bool IsDeleted { get; set; }
        public bool ShowDeleteButton { get; set; }        
    }
}