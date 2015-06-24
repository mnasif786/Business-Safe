using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request.HazardousSubstanceInventory;
using BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.Factories;
using BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.ViewModels;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Factories.HazardousSubstances
{
    public class HazardousSubstanceViewModelFactory: IHazardousSubstanceViewModelFactory
    {
        private readonly IHazardousSubstancesService _hazardousSubstancesService;
        private readonly ISuppliersService _suppliersService;
        private readonly IPictogramService _pictogramService;
        private readonly IRiskPhraseService _riskPhraseService;
        private readonly ISafetyPhraseService _safetyPhraseService;
        private long _companyId;
        private long _hazardousSubstanceId;

        public HazardousSubstanceViewModelFactory(IHazardousSubstancesService hazardousSubstancesService, ISuppliersService suppliersService, IPictogramService pictogramService, IRiskPhraseService riskPhraseService, ISafetyPhraseService safetyPhraseService)
        {
            _hazardousSubstancesService = hazardousSubstancesService;
            _suppliersService = suppliersService;
            _pictogramService = pictogramService;
            _riskPhraseService = riskPhraseService;
            _safetyPhraseService = safetyPhraseService;
        }

        public IHazardousSubstanceViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IHazardousSubstanceViewModelFactory WithHazardousSubstanceId(long hazardousSubstanceId)
        {
            _hazardousSubstanceId = hazardousSubstanceId;
            return this;
        }

        public AddEditHazardousSubstanceViewModel GetViewModel()
        {
            var suppliers =_suppliersService.GetForCompany(_companyId);
            var hazardousSubstanceDto = new HazardousSubstanceDto();
            if(_hazardousSubstanceId > 0)
            {
                hazardousSubstanceDto = _hazardousSubstancesService.GetByIdAndCompanyId(_hazardousSubstanceId, _companyId);
            }

            return new AddEditHazardousSubstanceViewModel()
                       {
                           Id = hazardousSubstanceDto.Id,
                           Name = hazardousSubstanceDto.Name,
                           CompanyId = _companyId,
                           Reference = hazardousSubstanceDto.Reference,
                           SdsDate = hazardousSubstanceDto.SdsDate != DateTime.MinValue ? hazardousSubstanceDto.SdsDate: (DateTime?) null,
                           HazardousSubstanceStandard = hazardousSubstanceDto.Standard,
                           SupplierId = hazardousSubstanceDto.Supplier != null? hazardousSubstanceDto.Supplier.Id: 0,
                           SupplierName = hazardousSubstanceDto.Supplier != null ? hazardousSubstanceDto.Supplier.Name: string.Empty,
                           Suppliers = suppliers.Select(AutoCompleteViewModel.ForSupplier).AddDefaultOption(),
                           Pictograms = _pictogramService.GetAll(),
                           RiskPhrases = _riskPhraseService.GetAll(),
                           SafetyPhrases = _safetyPhraseService.GetAll(),
                           SelectedHazardousSubstanceSymbols = hazardousSubstanceDto.Pictograms.Select(p => p.Id).ToList(),
                           SelectedRiskPhrases = hazardousSubstanceDto.RiskPhrases.Select(r => r.Id).ToList(),
                           SelectedSafetyPhrases = hazardousSubstanceDto.HazardousSubstanceSafetyPhrases.Select(r => r.SafetyPhase.Id).ToList(),
                           DetailsOfUse = hazardousSubstanceDto.DetailsOfUse,
                           AssessmentRequired = _hazardousSubstanceId > 0 ? hazardousSubstanceDto.AssessmentRequired: (bool?) null,
                           HasLinkedRiskAssessments = hazardousSubstanceDto.LinkedRiskAsessments.Any(),
                           SafetyPhrasesAdditionalInformation = GetSafetyPhrasesAdditionalInformation(hazardousSubstanceDto)
                       };
        }

        private static IEnumerable<SafetyPhraseAdditionalInformationViewModel> GetSafetyPhrasesAdditionalInformation(HazardousSubstanceDto hazardousSubstanceDto)
        {
            if (hazardousSubstanceDto.HazardousSubstanceSafetyPhrases.Any() == false)
            {
                return new List<SafetyPhraseAdditionalInformationViewModel>();
            }

            return hazardousSubstanceDto
                .HazardousSubstanceSafetyPhrases
                .Where(x => x.SafetyPhase.RequiresAdditionalInformation)
                .Select(x => new SafetyPhraseAdditionalInformationViewModel
                                 {
                                     AdditionalInformation = x.AdditionalInformation,
                                     ReferenceNumber = x.SafetyPhase.ReferenceNumber,
                                     SafetyPhaseId = x.SafetyPhase.Id
                                 }).ToList();
        }

        public AddEditHazardousSubstanceViewModel GetViewModel(AddHazardousSubstanceRequest model)
        {
            var suppliers = _suppliersService.GetForCompany(_companyId);
            var viewModel = new AddEditHazardousSubstanceViewModel();
            viewModel.Id = model.Id;
            viewModel.Name = model.Name;
            viewModel.CompanyId = _companyId;
            viewModel.Reference = model.Reference;
            viewModel.SdsDate = model.SdsDate;
            viewModel.HazardousSubstanceStandard = model.HazardousSubstanceStandard;
            viewModel.SupplierId = model.SupplierId;
            viewModel.Suppliers = suppliers.Select(AutoCompleteViewModel.ForSupplier).AddDefaultOption();
            viewModel.Pictograms = _pictogramService.GetAll();
            viewModel.RiskPhrases = _riskPhraseService.GetAll();
            viewModel.SafetyPhrases = _safetyPhraseService.GetAll();

            viewModel.SelectedHazardousSubstanceSymbols = model.PictogramIds == null
                                                              ? new List<long>()
                                                              : model.PictogramIds.ToList();

            viewModel.SelectedRiskPhrases = model.RiskPhraseIds == null
                                                ? new List<long>()
                                                : model.RiskPhraseIds.ToList();

            viewModel.SelectedSafetyPhrases = model.SafetyPhraseIds == null
                                                  ? new List<long>()
                                                  : model.SafetyPhraseIds.ToList();

            viewModel.DetailsOfUse = model.DetailsOfUse;
            viewModel.AssessmentRequired = model.AssessmentRequired;

            viewModel.SafetyPhrasesAdditionalInformation =
                model.AdditionalInformation.Select(x => new SafetyPhraseAdditionalInformationViewModel()
                                                            {
                                                                AdditionalInformation = x.AdditionalInformation,
                                                                ReferenceNumber = x.ReferenceNumber,
                                                                SafetyPhaseId = x.SafetyPhaseId
                                                            });

            return viewModel;
        }
    }
}