using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.WebSite.Areas.Employees.ViewModels;
using System;

namespace BusinessSafe.WebSite.Areas.Employees.Factories
{
    public class ViewEmergencyContactViewModelFactory : IViewEmergencyContactViewModelFactory
    {
        private readonly IEmployeeEmergencyContactDetailService _employeeEmergencyContactDetailService;
        private readonly IEmployeeService _employeeService;
        private long _companyId;
        private int _employeeEmergencyContactDetailsId;
        private Guid? _employeeId;

        public ViewEmergencyContactViewModelFactory(IEmployeeEmergencyContactDetailService employeeEmergencyContactDetailService, IEmployeeService employeeService)
        {
            _employeeEmergencyContactDetailService = employeeEmergencyContactDetailService;
            _employeeService = employeeService;
        }

        public IViewEmergencyContactViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IViewEmergencyContactViewModelFactory WithEmployeeEmergencyContactDetailsId(int employeeEmergencyContactDetailsId)
        {
            _employeeEmergencyContactDetailsId = employeeEmergencyContactDetailsId;
            return this;
        }

        public IViewEmergencyContactViewModelFactory WithEmployeeId(Guid employeeId)
        {
            _employeeId = employeeId;
            return this;
        }

        public ViewEmergencyContactDetailViewModel GetViewModel()
        {
            var employeeEmergencyContactDetail =
                _employeeEmergencyContactDetailService.GetByIdAndCompanyId(_employeeEmergencyContactDetailsId,
                                                                           _companyId);

            var employee = _employeeService.GetEmployee(_employeeId.Value, _companyId);

            var viewModel = new ViewEmergencyContactDetailViewModel
                       {
                           Title = employeeEmergencyContactDetail.Title,
                           Forename = employeeEmergencyContactDetail.Forename,
                           Surname = employeeEmergencyContactDetail.Surname,
                           Relationship = employeeEmergencyContactDetail.Relationship,
                           WorkTelephone = employeeEmergencyContactDetail.WorkTelephone + (employeeEmergencyContactDetail.PreferredContactNumber == 1 ? " (Preferred)" : ""),
                           HomeTelephone = employeeEmergencyContactDetail.HomeTelephone + (employeeEmergencyContactDetail.PreferredContactNumber == 2 ? " (Preferred)" : ""),
                           MobileTelephone = employeeEmergencyContactDetail.MobileTelephone + (employeeEmergencyContactDetail.PreferredContactNumber == 3 ? " (Preferred)" : ""),
                           SameAddressAsEmployee = employeeEmergencyContactDetail.SameAddressAsEmployee
                       };

            if(employeeEmergencyContactDetail.SameAddressAsEmployee)
            {
                viewModel.Address1 = employee.MainContactDetails != null ? employee.MainContactDetails.Address1 : null;
                viewModel.Address2 = employee.MainContactDetails != null ? employee.MainContactDetails.Address2 : null;
                viewModel.Address3 = employee.MainContactDetails != null ? employee.MainContactDetails.Address3 : null;
                viewModel.Town = employee.MainContactDetails != null ? employee.MainContactDetails.Town : null;
                viewModel.County = employee.MainContactDetails != null ? employee.MainContactDetails.County : null;
                viewModel.Country = employee.MainContactDetails != null && employee.MainContactDetails.Country != null ? employee.MainContactDetails.Country.Name : null;
                viewModel.PostCode = employee.MainContactDetails != null ? employee.MainContactDetails.PostCode : null; 
            }
            else
            {
                viewModel.Address1 = employeeEmergencyContactDetail.Address1;
                viewModel.Address2 = employeeEmergencyContactDetail.Address2;
                viewModel.Address3 = employeeEmergencyContactDetail.Address3;
                viewModel.Town = employeeEmergencyContactDetail.Town;
                viewModel.County = employeeEmergencyContactDetail.County;
                viewModel.Country =
                    employeeEmergencyContactDetail.Country != null
                        ? employeeEmergencyContactDetail.Country.Name
                        : "";
                viewModel.PostCode = employeeEmergencyContactDetail.PostCode;
            }
            return viewModel;
        }
    }
}