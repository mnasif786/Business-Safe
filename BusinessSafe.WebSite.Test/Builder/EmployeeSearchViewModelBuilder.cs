using BusinessSafe.WebSite.Areas.Employees.ViewModels;

namespace BusinessSafe.WebSite.Tests.Builder
{
    public class EmployeeSearchViewModelBuilder
    {
        private static EmployeeSearchViewModel _viewModel;

        public static EmployeeSearchViewModelBuilder Create()
        {
            _viewModel = new EmployeeSearchViewModel();
            return new EmployeeSearchViewModelBuilder();
        }

        public EmployeeSearchViewModel Build()
        {
            return _viewModel;
        } 
    }
}