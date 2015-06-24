using System;
using BusinessSafe.Checklists.ViewModels;

namespace BusinessSafe.Checklists.ViewModelFactories
{
    public interface IEmployeeChecklistViewModelFactory
    {
        EmployeeChecklistViewModel GetViewModel();
        IEmployeeChecklistViewModelFactory WithEmployeeChecklistId(Guid employeeChecklistId);
        IEmployeeChecklistViewModelFactory WithCompletedOnEmployeesBehalf(bool completedOnEmployeesBehalf);
    }
}