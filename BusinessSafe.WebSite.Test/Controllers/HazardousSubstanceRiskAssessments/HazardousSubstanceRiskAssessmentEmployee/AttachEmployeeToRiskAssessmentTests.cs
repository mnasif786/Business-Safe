using System;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Controllers;
using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.HazardousSubstanceRiskAssessments.HazardousSubstanceRiskAssessmentEmployee
{
    [TestFixture]
    [Category("Unit")]
    public class AttachEmployeeToRiskAssessmentTests
    {
        private Mock<IRiskAssessmentAttachmentService> _riskAssessmentAttachmentService;
        private Guid _employeeId;
        private long _riskAssessmentId;
        private long _companyId;

        [SetUp]
        public void SetUp()
        {
            _riskAssessmentAttachmentService = new Mock<IRiskAssessmentAttachmentService>();
            _employeeId = Guid.NewGuid();
            _riskAssessmentId = 1;
            _companyId = 2;
        }

        [Test]
        public void Given_valid_json_post_request_to_attach_employees_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateNonEmployeeController();

            //When
            target.AttachEmployeeToRiskAssessment(_employeeId, _riskAssessmentId, _companyId);

            //Then
            _riskAssessmentAttachmentService
                .Verify(x => x.AttachEmployeeToRiskAssessment(It.Is<AttachEmployeeRequest>(r => 
                                                                                           r.RiskAssessmentId == _riskAssessmentId && 
                                                                                           r.EmployeeId == _employeeId && 
                                                                                           r.CompanyId == _companyId && 
                                                                                           r.UserId == target.CurrentUser.UserId)));
        }

        [Test]
        public void Given_valid_json_post_request_to_attach_employees_Then_should_return_correct_results()
        {
            //Given
            var target = CreateNonEmployeeController();


            _riskAssessmentAttachmentService
                .Setup(x => x.AttachEmployeeToRiskAssessment(It.IsAny<AttachEmployeeRequest>()));

            //When
            var result = target.AttachEmployeeToRiskAssessment(_employeeId, _riskAssessmentId, _companyId);

            //Then
            dynamic data = result.Data;
            Assert.That(data.ToString(), Contains.Substring("Success = True"));
        }

        private RiskAssessmentEmployeeController CreateNonEmployeeController()
        {
            var target = new RiskAssessmentEmployeeController(null, _riskAssessmentAttachmentService.Object);
            return TestControllerHelpers.AddUserToController(target);
        }
    }
}
