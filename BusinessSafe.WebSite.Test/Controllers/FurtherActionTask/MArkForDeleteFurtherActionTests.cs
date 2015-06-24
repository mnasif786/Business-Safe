using System;
using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.TaskList.Controllers;
using BusinessSafe.WebSite.Areas.TaskList.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.FurtherActionTask
{
    [TestFixture]
    public class MarkForDeleteFurtherActionTests
    {
        private Mock<ITaskService> _taskService;
        long taskId;
        long companyId;

        [SetUp]
        public void Setup()
        {
            _taskService = new Mock<ITaskService>();

            taskId = 2;
            companyId = 10;
        }

        [Test]
        public void Given_valid_further_control_measure_id_and_company_id_When_mark_further_actionTask_for_delete_Then_should_call_appropiate_methods()
        {
            //Given
            var target = CreateController();

            var markTaskAsDeleteViewModel = new MarkTaskAsDeletedViewModel()
            {
                CompanyId = companyId,
                FurtherControlMeasureTaskId = taskId
            };

            //When
            target.MarkTaskAsDeleted(markTaskAsDeleteViewModel);

            //Then
            _taskService.Verify(x => x.MarkTaskAsDeleted(It.Is<MarkTaskAsDeletedRequest>(y => y.TaskId == taskId &&
                                                                                                                                   y.CompanyId == companyId &&
                                                                                                                                   y.UserId == target.CurrentUser.UserId)));
        }

        [Test]
        public void Given_invalid_further_control_measure_id_and_company_id_When_mark_further_actionTask_for_delete_Then_should_throw_correct_exception()
        {
            //Given
            taskId = 0;
            companyId = 0;
            var target = CreateController();

            var markTaskAsDeleteViewModel = new MarkTaskAsDeletedViewModel()
            {
                CompanyId = companyId,
                FurtherControlMeasureTaskId = taskId
            };

            target.ModelState.AddModelError("", "");

            //When
            //Then
            Assert.Throws<ArgumentException>(() => target.MarkTaskAsDeleted(markTaskAsDeleteViewModel));
        }

        [Test]
        public void Given_valid_further_control_measure_id_and_company_id_When_mark_further_actionTask_for_delete_Then_should_return_correct_result()
        {
            //Given
            var target = CreateController();
            var markTaskAsDeleteViewModel = new MarkTaskAsDeletedViewModel()
            {
                CompanyId = companyId,
                FurtherControlMeasureTaskId = taskId
            };

            //When
            var result = target.MarkTaskAsDeleted(markTaskAsDeleteViewModel);

            //Then
            dynamic data = result.Data;
            Assert.That(data.Success, Is.EqualTo(true));
        }

        private TaskListActionController CreateController()
        {
            var result = new TaskListActionController(_taskService.Object, null, null, null);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}