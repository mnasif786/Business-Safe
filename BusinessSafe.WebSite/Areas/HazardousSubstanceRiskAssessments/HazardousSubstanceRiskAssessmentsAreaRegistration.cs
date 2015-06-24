using System.Web.Mvc;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments
{
    public class HazardousSubstanceRiskAssessmentsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "HazardousSubstanceRiskAssessments";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "HazardousSubstanceRiskAssessments_default",
                "HazardousSubstanceRiskAssessments/{controller}/{action}/{id}",
                new { action = "Index", controller = "RiskAssessment", id = UrlParameter.Optional }
            );
        }
    }
}
