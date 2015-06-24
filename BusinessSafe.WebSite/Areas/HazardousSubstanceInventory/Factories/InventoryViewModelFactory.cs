using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.Application.Request.HazardousSubstanceInventory;
using BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.Factories;
using BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.ViewModels;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Factories.HazardousSubstances
{
    public class InventoryViewModelFactory : IInventoryViewModelFactory
    {
        private long _companyId;
        private long? _supplierId;
        private string _substanceNameLike;
        private bool _showDeleted;
        private readonly IHazardousSubstancesService _inventoryService;
        private readonly ISuppliersService _suppliersService;

        public InventoryViewModelFactory(IHazardousSubstancesService inventoryService, ISuppliersService suppliersService)
        {
            _inventoryService = inventoryService;
            _suppliersService = suppliersService;
        }

        public IInventoryViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IInventoryViewModelFactory WithSupplierId(long? supplierId)
        {
            _supplierId = supplierId;
            return this;
        }

        public IInventoryViewModelFactory WithSubstanceNameLike(string substanceNameLike)
        {
            _substanceNameLike = substanceNameLike;
            return this;
        }

        public IInventoryViewModelFactory WithShowDeleted(bool showDeleted)
        {
            _showDeleted = showDeleted;
            return this;
        }

        public InventoryViewModel GetViewModel()
        {
            var hazardousSubstances = _inventoryService.Search(new SearchHazardousSubstancesRequest
                                                                   {
                                                                       CompanyId = _companyId,
                                                                       SupplierId = _supplierId,
                                                                       SubstanceNameLike = _substanceNameLike,
                                                                       ShowDeleted = _showDeleted
                                                                   });

            var suppliers = _suppliersService.GetForCompany(_companyId);

            return new InventoryViewModel()
                   {
                       CompanyId = _companyId,
                       SubstanceNameLike = _substanceNameLike,
                       SupplierId = _supplierId,
                       ShowDeleted = _showDeleted,
                       ShowDeletedButtonText = _showDeleted ? "Hide Deleted" : "Show Deleted",
                       Substances = hazardousSubstances.Select(hazardousSubstance =>
                           new InventorySubstanceViewModel
                           {
                               Id = hazardousSubstance.Id,
                               Name = hazardousSubstance.Name,
                               Reference = hazardousSubstance.Reference,
                               Supplier = hazardousSubstance.Supplier != null ? hazardousSubstance.Supplier.Name : string.Empty,
                               Standard = (int)hazardousSubstance.Standard > 0 ? hazardousSubstance.Standard.ToString() : string.Empty,
                               DetailsOfUse = hazardousSubstance.DetailsOfUse,
                               AssessmentRequired = hazardousSubstance.AssessmentRequired,
                               CompanyId = hazardousSubstance.CompanyId,
                               CreatedOn = hazardousSubstance.CreatedOn.Value.ToShortDateString(),
                               SdsDate = hazardousSubstance.SdsDate.ToShortDateString(),
                               RiskPhraseReferences =
                                   hazardousSubstance.RiskPhrases != null
                                       ? CommaSeparatedStringHelper.Construct(
                                           hazardousSubstance.RiskPhrases.Select(rf => rf.ReferenceNumber).ToList())
                                       : "",
                               SafetyPhraseReferences =
                                   hazardousSubstance.HazardousSubstanceSafetyPhrases != null
                                       ? CommaSeparatedStringHelper.Construct(
                                           hazardousSubstance.HazardousSubstanceSafetyPhrases.Select(x => x.SafetyPhase.ReferenceNumber).ToList())
                                       : "",
                             LinkedRiskAssessmentsIds = hazardousSubstance.LinkedRiskAsessments != null ? hazardousSubstance.LinkedRiskAsessments.Select(x => x.Id) : new long[]{}
                           }),
                       Suppliers = suppliers.Select(AutoCompleteViewModel.ForSupplier).AddDefaultOption()
                   };
        }
    }
}