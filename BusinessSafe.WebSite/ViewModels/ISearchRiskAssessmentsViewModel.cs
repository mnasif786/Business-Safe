namespace BusinessSafe.WebSite.ViewModels
{
    public interface ISearchRiskAssessmentsViewModel
    {
        bool IsShowDeleted { get; set; }
        bool IsShowArchived { get; set; }
    }
}