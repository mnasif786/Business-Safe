using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels.Wizard;

namespace BusinessSafe.WebSite.Areas.Responsibilities.Factories
{
    public interface ISelectResponsibilitiesViewModelFactory
    {
        SelectResponsibilitiesViewModel GetViewModel();

        ISelectResponsibilitiesViewModelFactory WithSelectedResponsibilityTemplates(long[] selectedResponsibilityTemplateIds);
    }
}