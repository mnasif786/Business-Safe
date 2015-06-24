using System.Web.Mvc;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.Implementations.RiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Filters;

namespace BusinessSafe.WebSite.Controllers
{
    public class RiskAssessmentStatusController : BaseController
    {
        private readonly IRiskAssessmentService _riskAssessmentService;

        public RiskAssessmentStatusController(IRiskAssessmentService riskAssessmentService)
        {
            _riskAssessmentService = riskAssessmentService;
        }

        [PermissionFilter(Permissions.DeleteGeneralandHazardousSubstancesRiskAssessments)]
        public JsonResult CheckRiskAssessmentCanBeDeleted(long companyId, long riskAssessmentId)
        {
            bool hasUndeletedTasks = _riskAssessmentService.HasUndeletedTasks(companyId, riskAssessmentId);

            return Json(new { hasUndeletedTasks = !hasUndeletedTasks }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public JsonResult MarkRiskAssessmentIsDraftStatus(MarkRiskAssessmentIsDraftStatusViewModel viewModel)
        {
            if (viewModel.IsDraft)
            {
                _riskAssessmentService.MarkRiskAssessmentAsDraft(new MarkRiskAssessmentAsDraftRequest()
                                                                     {
                                                                         CompanyId = viewModel.CompanyId,
                                                                         RiskAssessmentId = viewModel.RiskAssessmentId,
                                                                         UserId = CurrentUser.UserId
                                                                     });
            }
            else
            {
                var canMarkRiskAssessmentAsLive = _riskAssessmentService.CanMarkRiskAssessmentAsLive(viewModel.CompanyId, viewModel.RiskAssessmentId);

                if (canMarkRiskAssessmentAsLive)
                {
                    _riskAssessmentService.MarkRiskAssessmentAsLive(new MarkRiskAssessmentAsLiveRequest()
                    {
                        CompanyId = viewModel.CompanyId,
                        RiskAssessmentId = viewModel.RiskAssessmentId,
                        UserId = CurrentUser.UserId
                    });
                }
                else
                {
                    return Json(new { Success = false, Message = "Please set a Review Date." });
                }
            }

            return Json(new { Success = true });
        }

        [HttpPost]
        [PermissionFilter(Permissions.DeleteGeneralandHazardousSubstancesRiskAssessments)]
        public JsonResult MarkRiskAssessmentAsDeletedOrUnDeleted(long companyId, long riskAssessmentId, bool deleted)
        {
            if (deleted)
            {
                _riskAssessmentService.MarkRiskAssessmentAsDeleted(new MarkRiskAssessmentAsDeletedRequest()
                {
                    CompanyId = companyId,
                    RiskAssessmentId = riskAssessmentId,
                    UserId = CurrentUser.UserId
                });

                _riskAssessmentService.MarkAllRealtedUncompletedTasksAsNoLongerRequired(new MarkRiskAssessmentTasksAsNoLongerRequiredRequest()
                {
                    CompanyId = companyId,
                    RiskAssessmentId = riskAssessmentId,
                    UserId = CurrentUser.UserId
                }); 
            }
            else
            {
                _riskAssessmentService.ReinstateRiskAssessmentAsNotDeleted(new ReinstateRiskAssessmentAsDeletedRequest()
                {
                    CompanyId = companyId,
                    RiskAssessmentId = riskAssessmentId,
                    UserId = CurrentUser.UserId
                });
            }

            return Json(new { Success = true });
        }
    }
}