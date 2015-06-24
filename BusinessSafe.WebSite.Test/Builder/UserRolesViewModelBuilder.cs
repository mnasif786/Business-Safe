using BusinessSafe.WebSite.Areas.Users.ViewModels;

namespace BusinessSafe.WebSite.Tests.Builder
{
    public class UserRolesViewModelBuilder
    {
        private static UserRolesViewModel _viewModel;


        public static UserRolesViewModelBuilder Create()
        {
            _viewModel = new UserRolesViewModel();
            return new UserRolesViewModelBuilder();
        }

        public UserRolesViewModel Build()
        {
           
            return _viewModel;
        }
    }
}