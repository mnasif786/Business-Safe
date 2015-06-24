using System.Web.Mvc;

namespace BusinessSafe.WebSite.Areas.GeneralRiskAssessments
{
    public class GeneralRiskAssessmentAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "GeneralRiskAssessments";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
             "GeneralRiskAssessment_DefaultToPremisesInformation",
             "GeneralRiskAssessments/",
             new { action = "Index", controller = "RiskAssessment" }
             );

            context.MapRoute(
                "GeneralRiskAssessments_default",
                "GeneralRiskAssessments/{controller}/{action}/{id}",
                new { action = "Index", controller = "RiskAssessment", id = UrlParameter.Optional }
            );
        }
    }
}

