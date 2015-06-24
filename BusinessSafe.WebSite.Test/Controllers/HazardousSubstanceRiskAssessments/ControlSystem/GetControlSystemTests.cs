using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Implementations.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.Request.HazardousSubstanceRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Controllers;
using NUnit.Framework;
using Moq;

namespace BusinessSafe.WebSite.Tests.Controllers.HazardousSubstanceRiskAssessments.ControlSystem
{
    [TestFixture]
    public class GetControlSystemTests
    {
        private Mock<IControlSystemService> _controlSystemService;
        private Mock<IHazardousSubstanceRiskAssessmentService> _hazardousSubstanceRiskAssessmentService;

        [SetUp]
        public void Setup()
        {
            _controlSystemService = new Mock<IControlSystemService>();
            _hazardousSubstanceRiskAssessmentService = new Mock<IHazardousSubstanceRiskAssessmentService>();
        }

        [Test]
        public void When_GetControlSystem_should_call_correct_methods()
        {
            // Given
            string hazardousSubstanceGroupCode = "AbcDef";
            MatterState? matterState = MatterState.Liquid;
            Quantity? quantity = Quantity.Medium;
            DustinessOrVolatility? dustinessOrVolatility = DustinessOrVolatility.High;
            var riskAssessmentId = 100L;
            var controlSystemDto = new ControlSystemDto
                                       {
                                           Id = 4L
                                       };

            var target = GetTarget();

            _controlSystemService
                .Setup(x => x.Calculate(hazardousSubstanceGroupCode, matterState, quantity, dustinessOrVolatility))
                .Returns(controlSystemDto);

            // When
            target.GetControlSystem(riskAssessmentId, hazardousSubstanceGroupCode, matterState, quantity, dustinessOrVolatility);

            // Then
            _controlSystemService.VerifyAll();
            _hazardousSubstanceRiskAssessmentService.Verify(x => x.SaveLastRecommendedControlSystem(It.Is<SaveLastRecommendedControlSystemRequest>(
                y => y.Id == riskAssessmentId &&
                     y.ControlSystemId == controlSystemDto.Id &&
                     y.UserId == target.CurrentUser.UserId
                )));
        }

        private ControlSystemController GetTarget()
        {
            var controller = new ControlSystemController(_controlSystemService.Object, _hazardousSubstanceRiskAssessmentService.Object);

            var routes = new RouteCollection();

            MvcApplication.RegisterRoutes(routes);

            var requestContextMock = new Mock<HttpRequestBase>();
            requestContextMock.Setup(r => r.AppRelativeCurrentExecutionFilePath).Returns("/");
            requestContextMock.Setup(r => r.ApplicationPath).Returns("/");

            var responseMock = new Mock<HttpResponseBase>();
            responseMock.Setup(s => s.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);


            var httpContextMock = new Mock<HttpContextBase>();
            httpContextMock.Setup(h => h.Request).Returns(requestContextMock.Object);
            httpContextMock.Setup(h => h.Response).Returns(responseMock.Object);

            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);

            controller.Url = new UrlHelper(new RequestContext(httpContextMock.Object, new RouteData()), routes);

            return TestControllerHelpers.AddUserToController(controller);
        }


    }


}
