using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Contracts.MultiHazardRiskAssessment;
using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Controllers
{
    [PersonalRiskAssessmentCurrentTabActionFilter(PersonalRiskAssessmentTabs.Hazards)]
    [PersonalRiskAssessmentContextFilter]
    public class HazardsController : BaseController
    {
        private readonly ICompanyDefaultService _companyDefaultService;
        private readonly IPersonalRiskAssessmentService _riskAssessmentService;
        private readonly IMultiHazardRiskAssessmentAttachmentService _multiHazardRiskAssessmentAttachmentService;
        private readonly IMultiHazardRiskAssessmentService _multiHazardRiskAssessmentService;

        public HazardsController(
            ICompanyDefaultService companyDefaultService,
            IPersonalRiskAssessmentService riskAssessmentService,
            IMultiHazardRiskAssessmentAttachmentService multiHazardRiskAssessmentAttachmentService,
            IMultiHazardRiskAssessmentService multiHazardRiskAssessmentService)
        {
            _companyDefaultService = companyDefaultService;
            _riskAssessmentService = riskAssessmentService;
            _multiHazardRiskAssessmentAttachmentService = multiHazardRiskAssessmentAttachmentService;
            _multiHazardRiskAssessmentService = multiHazardRiskAssessmentService;
        }

        [PermissionFilter(Permissions.ViewPersonalRiskAssessments)]
        public ActionResult View(long companyId, long riskAssessmentId)
        {
            IsReadOnly = true;
            return Index(companyId, riskAssessmentId);
        }

        [PermissionFilter(Permissions.ViewPersonalRiskAssessments)]
        public ActionResult Index(long companyId, long riskAssessmentId)
        {
            var riskAssessmentDto = _riskAssessmentService.GetRiskAssessmentWithHazards(riskAssessmentId, companyId, CurrentUser.UserId);
            var allHazardsForCompany = _companyDefaultService.GetAllMultiHazardRiskAssessmentHazardsForCompany(companyId, HazardTypeEnum.Personal, riskAssessmentId);
            var allSelectedHazardsIds = riskAssessmentDto.Hazards.ToList().Select(hazard => hazard.Id).ToArray();
            var availableHazards = allHazardsForCompany
               .Where(x => !allSelectedHazardsIds.Contains(x.Id));

            var viewModel = new HazardsViewModel()
            {
                RiskAssessmentId = riskAssessmentId,
                CompanyId = companyId,
                Hazards = availableHazards.Select(x => new LookupDto() { Id = x.Id, Name = x.Name }),
                SelectedHazards = riskAssessmentDto.Hazards.Select(x => new LookupDto() { Id = x.Id, Name = x.Name }),
            };

            return View("Index", viewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditPersonalRiskAssessments, Permissions.EditPersonalRiskAssessments)]
        public JsonResult SaveAndNext(SaveHazardsViewModel viewModel)
        {
            UpdateRiskAssessmentHazards(viewModel);
            return Json(new { Success = true });
        }

        [HttpPost]
        [PermissionFilter(Permissions.AddPersonalRiskAssessments, Permissions.EditPersonalRiskAssessments)]
        public ActionResult Save(SaveHazardsViewModel viewModel)
        {
            UpdateRiskAssessmentHazards(viewModel);
            return RedirectToAction("Index", new { companyId = viewModel.CompanyId, riskAssessmentId = viewModel.RiskAssessmentId });
        }

        private void UpdateRiskAssessmentHazards(SaveHazardsViewModel viewModel)
        {
            _multiHazardRiskAssessmentAttachmentService.AttachHazardsToRiskAssessment(new AttachHazardsToRiskAssessmentRequest
            {
                CompanyId = viewModel.CompanyId,
                UserId = CurrentUser.UserId,
                RiskAssessmentId = viewModel.RiskAssessmentId,
                Hazards = viewModel.HazardIds.Select((hazardId,index) => new AttachHazardsToRiskAssessmentHazardDetail() { Id = hazardId, OrderNumber = index+1}).ToList()
            });
        }

        [PermissionFilter(Permissions.DeletePersonalRiskAssessments)]
        public JsonResult CheckRiskAssessmentHazardCanBeRemoved(long companyId, long riskAssessmentId, long riskAssessmentHazardId)
        {
            bool canBeRemoved;
            canBeRemoved = _multiHazardRiskAssessmentService.CanRemoveRiskAssessmentHazard(companyId, riskAssessmentId, riskAssessmentHazardId);
            return Json(new { CanBeRemoved = canBeRemoved }, JsonRequestBehavior.AllowGet);
        }
    }
}
