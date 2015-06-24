using System;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.Users.ViewModels;

namespace BusinessSafe.WebSite.Areas.Users.Factories
{
    public class ViewUserViewModelFactory : IViewUserViewModelFactory
    {
        private readonly IEmployeeService _employeeService;
        private readonly IRolesService _rolesService;
        private long _companyId;
        private Guid _employeeId;

        public ViewUserViewModelFactory(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public IViewUserViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;

            return this;
        }

        public IViewUserViewModelFactory WithEmployeeId(Guid employeeId)
        {
            _employeeId = employeeId;

            return this;
        }

        public ViewUserViewModel GetViewModel()
        {
            var viewModel = new ViewUserViewModel();
            var employee = _employeeService.GetEmployee(_employeeId, _companyId);

            viewModel.CompanyId = _companyId;

            if (employee != null)
            {
                viewModel.Name = employee.FullName;
                viewModel.EmployeeReference = employee.EmployeeReference;
                viewModel.EmployeeId = _employeeId;
                viewModel.JobTitle = employee.JobTitle;
                viewModel.Department = "NOT AVAILABLE";
                viewModel.ManagerName = "NOT AVAILABLE";

                if (employee.User != null)
                {
                    if (employee.User.Role != null)
                    {
                        viewModel.RoleId = employee.User.Role.Id;
                        viewModel.Role = employee.User.Role.Description;
                    }

                    if (employee.User.SiteStructureElement.IsMainSite)
                        viewModel.PermissionLevel = "All Sites";
                    else
                    {
                        if (employee.User.SiteStructureElement != null)
                        {
                            if (employee.User.SiteStructureElement.GetType() == typeof(SiteGroupDto))
                                viewModel.PermissionLevel = "Site Group - " + employee.User.SiteStructureElement.Name;
                            else if (employee.User.SiteStructureElement.GetType() == typeof(SiteDto))
                                viewModel.PermissionLevel = "Site - " + employee.User.SiteStructureElement.Name;
                        }
                    }
                }
            }

            return viewModel;
        }
    }
}