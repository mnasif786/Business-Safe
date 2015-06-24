using System.Web.Mvc;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments
{
    public class FireRiskAssessmentsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "FireRiskAssessments";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
              "FireRiskAssessment_DefaultToPremisesInformation",
              "FireRiskAssessments/",
              new { action = "Index", controller = "RiskAssessment" }
              );

            context.MapRoute(
                "FireRiskAssessments_default",
                "FireRiskAssessments/{controller}/{action}/{id}",
                new { action = "Index", controller = "RiskAssessment", id = UrlParameter.Optional }
            );
        }
    }
}
