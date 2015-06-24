using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.Contracts.MultiHazardRiskAssessment;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Controllers;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.PremisesInformation
{
    [TestFixture]
    [Category("Unit")]
    public class BasePremisesInformationControllerTests
    {
       protected Mock<IGeneralRiskAssessmentService> RiskAssessmentService;
        protected Mock<IMultiHazardRiskAssessmentService> MultiHazardRiskAssessmentSerivce;

        [SetUp]
        public void SetUp()
        {
            RiskAssessmentService = new Mock<IGeneralRiskAssessmentService>();
            MultiHazardRiskAssessmentSerivce = new Mock<IMultiHazardRiskAssessmentService>();
        }

        internal PremisesInformationController GetTarget()
        {
            var result = new PremisesInformationController(RiskAssessmentService.Object, MultiHazardRiskAssessmentSerivce.Object);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}