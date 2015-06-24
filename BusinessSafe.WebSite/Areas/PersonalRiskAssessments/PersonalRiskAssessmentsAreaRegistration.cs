using System.Web.Mvc;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments
{
    public class PersonalRiskAssessmentsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "PersonalRiskAssessments"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("PersonalRiskAssessments_default", "PersonalRiskAssessments/{controller}/{action}/{id}", new { action = "Index", controller = "RiskAssessment", id = UrlParameter.Optional });
        }
    }
}