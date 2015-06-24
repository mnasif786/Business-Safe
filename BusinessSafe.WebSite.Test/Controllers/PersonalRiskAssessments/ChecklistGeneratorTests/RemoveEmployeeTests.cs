using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Controllers;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.PersonalRiskAssessments.ChecklistGeneratorTests
{
    [TestFixture]
    public class RemoveEmployeeTests
    {
        private Mock<IPersonalRiskAssessmentService> _personalRiskAssessmentService;

        [SetUp]
        public void Setup()
        {
            _personalRiskAssessmentService = new Mock<IPersonalRiskAssessmentService>();
        }

        [Test]
        public void given_valid_request_remove_employee_returns_success()
        {
            //given
            
            var controller = GetTarget();
            long riskAssessmentId = 1234L;
            Guid employeeId = Guid.NewGuid();
            
            //when

            var result = controller.RemoveEmployee(riskAssessmentId, employeeId);
            
            //then
            Assert.That(result.Data.ToString().Contains("Success"),Is.True);
        }

        [Test]
        public void given_riskassessment_and_employee_remove_employee_calls_service_with_correct_parameters()
        {
            //given

            var controller = GetTarget();
            long riskAssessmentId = 1234L;
            Guid employeeId = Guid.NewGuid();

            _personalRiskAssessmentService
                .Setup(x=>x.RemoveEmployeeFromCheckListGenerator(riskAssessmentId,TestControllerHelpers.CompanyIdAssigned, employeeId,TestControllerHelpers.UserIdAssigned))
                .Verifiable();
            //when

            controller.RemoveEmployee(riskAssessmentId, employeeId);

            //then
            _personalRiskAssessmentService.Verify();
            
        }

        private ChecklistGeneratorController GetTarget()
        {
            var controller = new ChecklistGeneratorController(
                null,
                null,
                _personalRiskAssessmentService.Object,
                null,
                null,
                null);

            return TestControllerHelpers.AddUserToController(controller);
        }
    }
}
