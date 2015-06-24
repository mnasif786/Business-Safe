using BusinessSafe.WebSite.Areas.Sites.ViewModels;

namespace BusinessSafe.WebSite.Tests.Builder
{
    public class DeleteSiteGroupViewModelBuilder
    {
        private static DeleteSiteGroupViewModel _deleteSiteGroupViewModel;
        private long _groupId = 1;
        private long _companyId = 1;

        public static DeleteSiteGroupViewModelBuilder Create()
        {
            _deleteSiteGroupViewModel = new DeleteSiteGroupViewModel();
            return new DeleteSiteGroupViewModelBuilder();
        }

        public DeleteSiteGroupViewModel Build()
        {
            _deleteSiteGroupViewModel.ClientId = _companyId;
            _deleteSiteGroupViewModel.GroupId = _groupId;
            return _deleteSiteGroupViewModel;
        }

        public DeleteSiteGroupViewModelBuilder WithCompanyId(int companyId)
        {
            _companyId = companyId;
            return this;
        }

        public DeleteSiteGroupViewModelBuilder WithSiteGroupId(int groupId)
        {
            _groupId = groupId;
            return this;
        }
    }
}