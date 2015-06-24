using System;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.WebSite.Controllers;
using Moq;
using NUnit.Framework;
using StructureMap;

namespace BusinessSafe.WebSite.Tests.Controllers.NonEmployee
{
    [TestFixture]
    [Category("Unit")]
    public class AttachNonEmployeeToRiskAssessmentTests
    {
        private Mock<IRiskAssessmentAttachmentService> _riskAssessmentAttachmentService;
        private const long NonEmployeeId = 1;
        private const long RiskAssessmentId = 2;
        private const long CompanyId = 3;

        [SetUp]
        public void SetUp()
        {
            _riskAssessmentAttachmentService = new Mock<IRiskAssessmentAttachmentService>();
            ObjectFactory.Inject<IPeninsulaLog>(new StubPeninsulaLog());
        }

        [Test]
        public void Given_that_json_get_request_to_attach_non_employee_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateNonEmployeeController();
            

            //When
            var result = target.AttachNonEmployeeToRiskAssessment(NonEmployeeId, RiskAssessmentId, CompanyId);

            //Then
            _riskAssessmentAttachmentService.Verify(x => x.AttachNonEmployeeToRiskAssessment(It.Is<AttachNonEmployeeToRiskAssessmentRequest>(y => y.NonEmployeeToAttachId == NonEmployeeId && y.RiskAssessmentId == RiskAssessmentId)));
            Assert.That(result, Is.TypeOf<JsonResult>());
        }

        [Test]
        [ExpectedException]
        public void Given_that_risk_assessment_service_encounters_exception_Then_should_return_correct_methods()
        {
            //Given
            var target = CreateNonEmployeeController();
            

            _riskAssessmentAttachmentService
                .Setup(x => x.AttachNonEmployeeToRiskAssessment(It.IsAny<AttachNonEmployeeToRiskAssessmentRequest>()))
                .Throws(new Exception());

            //When
            var result = target.AttachNonEmployeeToRiskAssessment(NonEmployeeId, RiskAssessmentId, CompanyId);
        }


        [Test]
        public void Given_that_risk_assessment_service_process_ok_Then_should_return_correct_methods()
        {
            //Given
            var target = CreateNonEmployeeController();

            _riskAssessmentAttachmentService
                .Setup(x => x.AttachNonEmployeeToRiskAssessment(It.IsAny<AttachNonEmployeeToRiskAssessmentRequest>()));
                

            //When
            var result = target.AttachNonEmployeeToRiskAssessment(NonEmployeeId, RiskAssessmentId, CompanyId);

            //Then
            Assert.That(result.Data.ToString(), Is.StringContaining("{ Success = True }"));
        }

        private RiskAssessmentNonEmployeeController CreateNonEmployeeController()
        {
            var target = new RiskAssessmentNonEmployeeController(null, _riskAssessmentAttachmentService.Object);
            return TestControllerHelpers.AddUserToController(target);
        }
    }
}