using System;
using BusinessSafe.WebSite.Areas.Employees.ViewModels;

namespace BusinessSafe.WebSite.Tests.Builder
{
    public class EmployeeViewModelBuilder
    {
        private static EmployeeViewModel _viewModel;
        private string _title = "";
        private string _employeeReference;
        private string _forename;
        private string _surname;
        private string _sex;
        private int _nationalityId;
        private Guid _employeeId;
        private Guid _userRoleId;
        private long _userSiteId;
        private long _userSiteGroupId;
        private string _email;
        private string _mobile;

        public static EmployeeViewModelBuilder Create()
        {
            _viewModel = new EmployeeViewModel();
            return new EmployeeViewModelBuilder();
        }

        public EmployeeViewModel Build()
        {
            _viewModel.EmployeeReference = _employeeReference;
            _viewModel.NameTitle = _title;
            _viewModel.Forename = _forename;
            _viewModel.Surname = _surname;
            _viewModel.NationalityId = _nationalityId;
            _viewModel.Sex = _sex;
            _viewModel.EmployeeId = _employeeId;

            _viewModel.UserRoleId = _userRoleId;
            _viewModel.UserSiteId = _userSiteId;
            _viewModel.UserSiteGroupId = _userSiteGroupId;
            _viewModel.Email = _email;
            _viewModel.Mobile = _mobile;

           
            return _viewModel;
        }

        public EmployeeViewModelBuilder WithEmployeeReference(string siteId)
        {
            _employeeReference = siteId;
            return this;
        }

        public EmployeeViewModelBuilder WithTitle(string siteId)
        {
            _title = siteId;
            return this;
        }

        public EmployeeViewModelBuilder WithForename(string siteId)
        {
            _forename = siteId;
            return this;
        }

        public EmployeeViewModelBuilder WithSurname(string siteId)
        {
            _surname = siteId;
            return this;
        }

        public EmployeeViewModelBuilder WithSex(string siteId)
        {
            _sex = siteId;
            return this;
        }
        public EmployeeViewModelBuilder WithNationalityId(int siteId)
        {
            _nationalityId = siteId;
            return this;
        }

        public EmployeeViewModelBuilder WithEmployeeId(Guid employeeId)
        {
            _employeeId = employeeId;
            return this;
        }

        public EmployeeViewModelBuilder WithEmail(string email)
        {
            _email= email;
            return this;
        }

        public EmployeeViewModelBuilder WithMobile(string mobile)
        {
            _mobile = mobile;
            return this;
        }


        public EmployeeViewModelBuilder WithUserRoleId(Guid userRoleId)
        {
            _userRoleId = userRoleId;
            return this;
        }

        public EmployeeViewModelBuilder WithUserSiteId(long userSiteId)
        {
            _userSiteId = userSiteId;
            return this;
        }

        public EmployeeViewModelBuilder WithUserSiteGroupId(long userSiteGroupId)
        {
            _userSiteGroupId = userSiteGroupId;
            return this;
        }
    }

}
