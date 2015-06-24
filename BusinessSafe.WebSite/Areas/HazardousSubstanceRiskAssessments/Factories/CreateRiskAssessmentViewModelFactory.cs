using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.Application.Request.HazardousSubstanceInventory;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories
{
    public class CreateRiskAssessmentViewModelFactory : ICreateRiskAssessmentViewModelFactory
    {
        private readonly IHazardousSubstancesService hazardousSubstancesService;
        private long _companyId;
        private long _newHazardousSubstanceId;
        
        public CreateRiskAssessmentViewModelFactory(IHazardousSubstancesService hazardousSubstancesService)
        {
            this.hazardousSubstancesService = hazardousSubstancesService;
        }

        public ICreateRiskAssessmentViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public ICreateRiskAssessmentViewModelFactory WithNewHazardousSubstanceId(long? newHazardousSubstanceId)
        {
            if (newHazardousSubstanceId.HasValue)
                _newHazardousSubstanceId = (long)newHazardousSubstanceId;
            return this;
        }

        public CreateRiskAssessmentViewModel GetViewModel()
        {
            var viewModel = new CreateRiskAssessmentViewModel
                            {
                                NewHazardousSubstanceId = _newHazardousSubstanceId,
                                Title = (_newHazardousSubstanceId > 0) && (_companyId > 0)
                                    ? hazardousSubstancesService.GetByIdAndCompanyId(_newHazardousSubstanceId, _companyId).Name
                                    : string.Empty,
                                CompanyId = _companyId,
                                HazardousSubstances = GetHazardousSubstances()
                            };


            return viewModel;
        }

        public CreateRiskAssessmentViewModel GetViewModel(CreateRiskAssessmentViewModel createRiskAssessmentViewModel)
        {
            createRiskAssessmentViewModel.HazardousSubstances = GetHazardousSubstances();
            return createRiskAssessmentViewModel;
        }

        private IEnumerable<AutoCompleteViewModel> GetHazardousSubstances()
        {
            var hazardousSubstances = hazardousSubstancesService.Search(new SearchHazardousSubstancesRequest
                                                                            {
                                                                                CompanyId = _companyId
                                                                            });

            return hazardousSubstances.Select(AutoCompleteViewModel.ForHazardousSubstance).AddDefaultOption();
        }
    }
}