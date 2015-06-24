using System.Linq;
using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels.Wizard;

namespace BusinessSafe.WebSite.Areas.Responsibilities.Factories
{
    public class SelectResponsibilitiesViewModelFactory : ISelectResponsibilitiesViewModelFactory
    {
        private readonly IStatutoryResponsibilityTemplateService _statutoryResponsibilityTemplateService;
        private readonly IResponsibilitiesService _responsibilitiesService;
        private long _companyId;
        private long[] _selectedResponsibilityTemplateIds;

        public SelectResponsibilitiesViewModelFactory(
            IStatutoryResponsibilityTemplateService statutoryResponsibilityTemplateService, 
            IResponsibilitiesService responsibilitiesService)
        {
            _statutoryResponsibilityTemplateService = statutoryResponsibilityTemplateService;
            _responsibilitiesService = responsibilitiesService;
        }

        public ISelectResponsibilitiesViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public ISelectResponsibilitiesViewModelFactory WithSelectedResponsibilityTemplates(long[] selectedResponsibilityTemplateIds)
        {
            _selectedResponsibilityTemplateIds = selectedResponsibilityTemplateIds;
            return this;
        }

        public SelectResponsibilitiesViewModel GetViewModel()
        {
            var templates = _statutoryResponsibilityTemplateService.GetStatutoryResponsibilityTemplates();

            return new SelectResponsibilitiesViewModel()
                       {
                     
                           Responsibilities =
                               templates
                               .OrderBy(x=>x.ResponsibilityCategory.Category)
                               .Select(
                                   t =>
                                   new StatutoryResponsibilityViewModel
                                       {
                                           Id = (int)t.Id,
                                           Category = t.ResponsibilityCategory.Category,
                                           Title = t.Title,
                                           Description = t.Description,
                                           ResponsibilityReason = t.ResponsibilityReason.Reason,
                                           IsSelected = _selectedResponsibilityTemplateIds != null && _selectedResponsibilityTemplateIds.Contains(t.Id)
                                       })
                       };
        }
    }
}