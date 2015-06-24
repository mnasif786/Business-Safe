using System;
using System.Web.Mvc;

using BusinessSafe.Application.Contracts.Checklists;
using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Controllers;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.PersonalRiskAssessments.ChecklistManagerCommandsTests
{
    [TestFixture]
    public class ToggleFurtherActionRequiredTests
    {
        private ChecklistManagerCommandsController _target;
        private Mock<IEmployeeChecklistService> _employeeChecklistService;
        private Mock<IPersonalRiskAssessmentService> _personalRiskAssessmentService;

        private Guid employeeChecklistId;

        [SetUp]
        public void Setup()
        {
            _employeeChecklistService = new Mock<IEmployeeChecklistService>();
            _personalRiskAssessmentService = new Mock<IPersonalRiskAssessmentService>();
            employeeChecklistId = Guid.NewGuid();
        }

        [Test]
        public void Given_id_When_ToggleFurtherActionRequired_Then_return_json_success_true()
        {
            // Given
            _target = GetTarget();

            // When
            dynamic result = _target.ToggleFurtherActionRequired(employeeChecklistId, true);

            // Then
            Assert.That(result, Is.InstanceOf<JsonResult>());
            Assert.That(result.Data.Success, Is.True);
        }

        [Test]
        public void Given_id_and_action_required_When_ToggleFurtherActionRequired_Then_tell_service_to_mark_it_as_further_action_required()
        {
            // Given
            _target = GetTarget();

            // When
            dynamic result = _target.ToggleFurtherActionRequired(employeeChecklistId, true);

            // Then
            _employeeChecklistService.Verify(x => x.ToggleFurtherActionRequired(employeeChecklistId, true, TestControllerHelpers.UserIdAssigned));
        }

        [Test]
        public void Given_id_and_action_not_required_When_ToggleFurtherActionRequired_Then_tell_service_to_mark_it_as_further_action_required()
        {
            // Given
            _target = GetTarget();

            // When
            dynamic result = _target.ToggleFurtherActionRequired(employeeChecklistId, false);

            // Then
            _employeeChecklistService.Verify(x => x.ToggleFurtherActionRequired(employeeChecklistId, false, TestControllerHelpers.UserIdAssigned));
        }

        [Test]
        public void Given_a_checklist_when_UpdateEmployeeChecklistFurtherRequiredValue_to_true_then_new_risk_assessment_created()
        {
            var checkListId = Guid.NewGuid();
            var target = GetTarget();
            target.UpdateEmployeeChecklistFurtherRequired(checkListId, true);

            var userId = target.CurrentUser.UserId;
            _personalRiskAssessmentService.Verify(x => x.CreateRiskAssessmentFromChecklist(checkListId, userId),Times.Once());
        }

        [Test]
        public void Given_a_checklist_when_UpdateEmployeeChecklistFurtherRequiredValue_to_false_then_new_risk_assessment_not_created()
        {
            var checkListId = Guid.NewGuid();
            var target = GetTarget();
            target.UpdateEmployeeChecklistFurtherRequired(checkListId, false);

            var userId = target.CurrentUser.UserId;
            _personalRiskAssessmentService.Verify(x => x.CreateRiskAssessmentFromChecklist(checkListId, userId),Times.Never());

        }

        private ChecklistManagerCommandsController GetTarget()
        {
            return TestControllerHelpers.AddUserToController(new ChecklistManagerCommandsController(_employeeChecklistService.Object, _personalRiskAssessmentService.Object));
        }
    }
}
