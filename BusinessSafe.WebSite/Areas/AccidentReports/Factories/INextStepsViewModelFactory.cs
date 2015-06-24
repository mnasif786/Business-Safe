using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;

namespace BusinessSafe.WebSite.Areas.AccidentReports.Factories
{
    public interface INextStepsViewModelFactory
    {
        INextStepsViewModelFactory WithCompanyId(long companyId);
        INextStepsViewModelFactory WithAccidentRecordId(long accidentRecordId);
        NextStepsViewModel GetViewModel();
    }
}