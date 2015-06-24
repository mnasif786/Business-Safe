using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.FurtherActionTaskDetailsSummary
{
    [TestFixture]
    public class IndexTests
    {
        private const long _riskAssessmentFurtherControlMeasureId = 500;
        private Mock<ITaskService> _taskService;

        [SetUp]
        public void SetUp()
        {
            _taskService = new Mock<ITaskService>();
        }

        [Test]
        public void When_get_new_Then_should_return_the_correct_view()
        {
            // Given
            var target = CreateController();

            var taskDetailsSummaryDto = new TaskDetailsSummaryDto();
            _taskService
                .Setup(x => x.GetTaskDetailsSummary(It.Is<TaskDetailsSummaryRequest>(y => y.RiskAssessmentFurtherControlMeasureId == _riskAssessmentFurtherControlMeasureId)))
                .Returns(taskDetailsSummaryDto);

            // When
            var result = target.Index(_riskAssessmentFurtherControlMeasureId);

            // Then
            Assert.That(result.ViewName, Is.EqualTo("_TaskDetailsSummary"));
        }

        [Test]
        public void When_get_new_Then_should_return_the_correct_viewmodel()
        {
            // Given
            var target = CreateController();

            var taskDetailsSummaryDto = new TaskDetailsSummaryDto();
            _taskService
                .Setup(x => x.GetTaskDetailsSummary(It.Is<TaskDetailsSummaryRequest>(y => y.RiskAssessmentFurtherControlMeasureId == _riskAssessmentFurtherControlMeasureId)))
                .Returns(taskDetailsSummaryDto);

            // When
            var result = target.Index(_riskAssessmentFurtherControlMeasureId);

            // Then
            Assert.That(result.Model, Is.TypeOf<TaskDetailsSummaryViewModel>());
        }

        [Test]
        public void When_get_new_Then_should_call_the_correct_methods()
        {
            // Given
            var target = CreateController();

            var taskDetailsSummaryDto = new TaskDetailsSummaryDto();
            _taskService
                .Setup(x => x.GetTaskDetailsSummary(It.Is<TaskDetailsSummaryRequest>(y => y.RiskAssessmentFurtherControlMeasureId == _riskAssessmentFurtherControlMeasureId)))
                .Returns(taskDetailsSummaryDto);

            // When
            target.Index(_riskAssessmentFurtherControlMeasureId);

            // Then
            _taskService.VerifyAll();

        }

        private FurtherControlMeasureTaskDetailsSummaryController CreateController()
        {
            var result = new FurtherControlMeasureTaskDetailsSummaryController(_taskService.Object);
            return result;
        }
    }
}