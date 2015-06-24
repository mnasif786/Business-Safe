using System.Web;

namespace BusinessSafe.WebSite.ViewModels
{
    public static class SearchRiskAssessmentViewModelExtensions
    {
        private static HtmlString DeletedMessage = new HtmlString("<strong>Deleted</strong>");
        private static HtmlString ArchivedMessage = new HtmlString("<strong>Archived</strong>");
        private static HtmlString OpenMessage = new HtmlString("<strong>Open</strong>");

        public static HtmlString GetAdditionalTitle(this ISearchRiskAssessmentsViewModel viewModel)
        {
            if (viewModel.IsShowDeleted)
            {
                return DeletedMessage;
            }

            if (viewModel.IsShowArchived)
            {
                return ArchivedMessage;
            }

            return OpenMessage;
        }

        public static bool IsShowOpenVisible(this ISearchRiskAssessmentsViewModel viewModel)
        {
            if (viewModel.IsShowDeleted || viewModel.IsShowArchived)
            {
                return true;
            }

            return false;
        }
    }
}