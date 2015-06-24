using System;
using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Responsibilities.Controllers;
using BusinessSafe.WebSite.Areas.TaskList.Controllers;
using BusinessSafe.WebSite.Areas.TaskList.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Responsibilities.ResponsibilityTasks
{
    [TestFixture]
    public class MarkAsNoLongerRequiredTests
    {
        private Mock<ITaskService> _taskService;
        private long taskId;
        private long companyId;

        [SetUp]
        public void Setup()
        {
            _taskService = new Mock<ITaskService>();

            taskId = 2;
            companyId = 10;
        }

        [Test]
        public void Given_valid_request_When_mark_for_no_longer_required_Then_should_call_appropiate_methods()
        {
            //Given
            var target = GetTarget();

            var viewModel = new MarkResponsibilityTaskAsNoLongerRequiredViewModel()
                                {
                                    CompanyId = companyId,
                                    TaskId = taskId
                                };

            //When
            target.MarkTaskAsNoLongerRequired(viewModel);

            //Then
            _taskService.Verify(
                x => x.MarkTaskAsNoLongerRequired(It.Is<MarkTaskAsNoLongerRequiredRequest>(y => y.TaskId == taskId &&
                                                                                                y.CompanyId == companyId &&
                                                                                                y.UserId ==
                                                                                                target.CurrentUser.
                                                                                                    UserId)));
        }

        [Test]
        public void
            Given_invalid_request_When_mark_responsibility_task_as_no_longer_required_Then_should_throw_correct_exception
            ()
        {
            //Given
            taskId = 0;
            companyId = 0;
            var target = GetTarget();

            var viewModel = new MarkResponsibilityTaskAsNoLongerRequiredViewModel()
                                {
                                    CompanyId = companyId,
                                    TaskId = taskId
                                };

            target.ModelState.AddModelError("", "");

            //When
            //Then
            Assert.Throws<ArgumentException>(() => target.MarkTaskAsNoLongerRequired(viewModel));
        }

        [Test]
        public void Given_valid_request_When_mark_responsibility_task_for_delete_Then_should_return_correct_result()
        {
            //Given
            var target = GetTarget();
            var viewModel = new MarkResponsibilityTaskAsNoLongerRequiredViewModel()
                                {
                                    CompanyId = companyId,
                                    TaskId = taskId
                                };

            //When
            var result = target.MarkTaskAsNoLongerRequired(viewModel);

            //Then
            dynamic data = result.Data;
            Assert.That(data.Success, Is.EqualTo(true));
        }

        private ResponsibilityController GetTarget()
        {
            var result = new ResponsibilityController(null, _taskService.Object, null, null, null, null, null, null, null, 
                                                      null, null);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}