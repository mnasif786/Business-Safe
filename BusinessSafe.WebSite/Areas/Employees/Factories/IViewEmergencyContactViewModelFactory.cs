using BusinessSafe.WebSite.Areas.Employees.ViewModels;
using System;

namespace BusinessSafe.WebSite.Areas.Employees.Factories
{
    public interface IViewEmergencyContactViewModelFactory
    {
        IViewEmergencyContactViewModelFactory WithCompanyId(long companyId);
        IViewEmergencyContactViewModelFactory WithEmployeeEmergencyContactDetailsId(int employeeEmergencyContactDetailsId);
        IViewEmergencyContactViewModelFactory WithEmployeeId(Guid employeeId);
        ViewEmergencyContactDetailViewModel GetViewModel();
    }
}