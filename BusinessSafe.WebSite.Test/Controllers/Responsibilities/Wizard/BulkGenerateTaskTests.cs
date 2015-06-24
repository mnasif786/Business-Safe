using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.Request.Responsibilities;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Messages.Events;
using BusinessSafe.WebSite.Areas.Responsibilities.Controllers;
using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels.Wizard;
using Moq;

using NServiceBus;

using NUnit.Framework;

using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Tests.Controllers.Responsibilities.Wizard
{
    [TestFixture]
    public class BulkGenerateTaskTests
    {
        private Mock<IResponsibilitiesService> _responsibilitiesService;
        private List<GenerateResponsibilityTaskViewModel> _viewModel;
        private Mock<IBus> _bus;
        private Mock<IBusinessSafeSessionManager> _businessSafeSessionManager;

        [SetUp]
        public void Setup()
        {
            _responsibilitiesService = new Mock<IResponsibilitiesService>();
            _responsibilitiesService.Setup(
                x => x.CreateManyResponsibilityTaskFromWizard(It.IsAny<CreateManyResponsibilityTaskFromWizardRequest>()));

            _bus = new Mock<IBus>();
            _bus.Setup(x => x.Publish(It.IsAny<TaskAssigned>()));

            _businessSafeSessionManager = new Mock<IBusinessSafeSessionManager>();

            _viewModel = new List<GenerateResponsibilityTaskViewModel>
            {
                new GenerateResponsibilityTaskViewModel {
                    SiteId = 123L,
                    TaskId = 456L,
                    ResponsibilityId = 789L,
                    Frequency = TaskReoccurringType.Weekly,
                    Owner = Guid.NewGuid(),
                    StartDate = DateTime.Now.ToShortDateString(),
                    EndDate = DateTime.Now.AddDays(1).ToShortDateString()
                },
                new GenerateResponsibilityTaskViewModel {
                    SiteId = 57856774L,
                    TaskId = 456747467L,
                    ResponsibilityId = 7846746749L,
                    Frequency = TaskReoccurringType.Monthly,
                    Owner = Guid.NewGuid(),
                    StartDate = DateTime.Now.ToShortDateString(),
                    EndDate = DateTime.Now.AddDays(123).ToShortDateString()
                }
            };
        }

        [Test]
        public void Given_valid_model_When_BulkGenerateTasks_Then_return_json_success_equals_true()
        {
            // Given
            var target = GetTarget();

            // When
            dynamic result = target.BulkGenerateTasks(_viewModel);

            // Then
            Assert.That(result.Data.Success, Is.True);
        }

        [Test]
        public void Given_valid_model_When_BulkGenerateTasks_Then_pass_collection_of_requests_to_service()
        {
            // Given
            var target = GetTarget();

            // When
            target.BulkGenerateTasks(_viewModel);

            // Then
            _responsibilitiesService.Verify(
                x => x.CreateManyResponsibilityTaskFromWizard(It.IsAny<CreateManyResponsibilityTaskFromWizardRequest>()));
        }

        [Test]
        public void Given_valid_model_When_BulkGenerateTasks_Then_attach_logged_in_user_id_to_request()
        {
            // Given
            CreateManyResponsibilityTaskFromWizardRequest passedRequest = null;
            _responsibilitiesService
                .Setup(x => x.CreateManyResponsibilityTaskFromWizard(It.IsAny<CreateManyResponsibilityTaskFromWizardRequest>()))
                .Callback<CreateManyResponsibilityTaskFromWizardRequest>(y => passedRequest = y);

            var target = GetTarget();

            // When
            target.BulkGenerateTasks(_viewModel);

            // Then
            Assert.That(passedRequest.CreatingUserId, Is.EqualTo(TestControllerHelpers.UserIdAssigned));
        }

        [Test]
        public void Given_valid_model_When_BulkGenerateTasks_Then_attach_logged_in_users_company_id_to_request()
        {
            // Given
            CreateManyResponsibilityTaskFromWizardRequest passedRequest = null;
            _responsibilitiesService
                .Setup(x => x.CreateManyResponsibilityTaskFromWizard(It.IsAny<CreateManyResponsibilityTaskFromWizardRequest>()))
                .Callback<CreateManyResponsibilityTaskFromWizardRequest>(y => passedRequest = y);

            var target = GetTarget();

            // When
            target.BulkGenerateTasks(_viewModel);

            // Then
            Assert.That(passedRequest.CompanyId, Is.EqualTo(TestControllerHelpers.CompanyIdAssigned));
        }

        [Test]
        public void Given_invalid_model_When_BulkGenerateTasks_Then_do_not_pass_collection_of_requests_to_service()
        {
            // Given
            var target = GetTarget();
            target.ModelState.AddModelError("error", "an error");

            // When
            target.BulkGenerateTasks(_viewModel);

            // Then
            _responsibilitiesService.Verify(x =>
                x.CreateManyResponsibilityTaskFromWizard(It.IsAny<CreateManyResponsibilityTaskFromWizardRequest>()), Times.Never());
        }

        [Test]
        public void Given_valid_model_When_BulkGenerateTasks_Then_map_requests()
        {
            // Given
            CreateManyResponsibilityTaskFromWizardRequest passedRequest = null;
            _responsibilitiesService
                .Setup(x => x.CreateManyResponsibilityTaskFromWizard(It.IsAny<CreateManyResponsibilityTaskFromWizardRequest>()))
                .Callback<CreateManyResponsibilityTaskFromWizardRequest>(y => passedRequest = y);
            var target = GetTarget();

            // When
            target.BulkGenerateTasks(_viewModel);

            // Then
            Assert.That(passedRequest.TaskDetails.Count, Is.EqualTo(_viewModel.Count));
            foreach (var model in _viewModel)
            {
                var currRequest = passedRequest.TaskDetails.SingleOrDefault(x => x.TaskTemplateId == model.TaskId);
                Assert.NotNull(currRequest);
                Assert.That(currRequest.CompanyId, Is.EqualTo(TestControllerHelpers.CompanyIdAssigned));
                Assert.That(currRequest.EndDate, Is.EqualTo(DateTime.Parse(model.EndDate)));
                Assert.That(currRequest.Frequency, Is.EqualTo(model.Frequency));
                Assert.That(currRequest.AssigneeId, Is.EqualTo(model.Owner));
                Assert.That(currRequest.ResponsibilityId, Is.EqualTo(model.ResponsibilityId));
                Assert.That(currRequest.SiteId, Is.EqualTo(model.SiteId));
                Assert.That(currRequest.StartDate, Is.EqualTo(DateTime.Parse(model.StartDate)));
                Assert.That(currRequest.UserId, Is.EqualTo(TestControllerHelpers.UserIdAssigned));
            }
        }

        [Test]
        public void Given_invalid_model_When_BulkGenerateTasks_Then_return_errors_in_json()
        {
            // Given
            var target = GetTarget();
            target.ModelState.AddModelError("error", "an error");

            // When
            dynamic result = target.BulkGenerateTasks(_viewModel);

            // Then
            Assert.That(result.Data.Success, Is.EqualTo("false"));
            Assert.That(result.Data.Errors[0], Is.EqualTo("an error"));
        }

        [Test]
        public void Given_a_model_with_invalid_frequency_When_generate_task_Then_add_error_to_modelstate()
        {
            //given
            var target = GetTarget();

            var viewModel = new GenerateResponsibilityTaskViewModel
            {
                SiteId = 1L,
                TaskId = 1L,
                ResponsibilityId = 1L,
                Frequency = TaskReoccurringType.None,
                Owner = Guid.NewGuid(),
                StartDate = DateTime.Now.ToShortDateString(),
                EndDate = DateTime.Now.AddDays(1).ToShortDateString()
            };

            //when
            target.BulkGenerateTasks(new List<GenerateResponsibilityTaskViewModel> {viewModel});
            //then
            Assert.That(target.ModelState.IsValid == false);

        }

        [Test]
        public void Given_a_model_with_invalid_owner_When_generate_task_Then_add_error_to_modelstate()
        {
            //given
            var target = GetTarget();

            var viewModel = new GenerateResponsibilityTaskViewModel
            {
                SiteId = 1L,
                TaskId = 1L,
                ResponsibilityId = 1L,
                Frequency = TaskReoccurringType.Weekly,
                StartDate = DateTime.Now.ToShortDateString(),
                EndDate = DateTime.Now.AddDays(1).ToShortDateString()
            };

            //when
            target.BulkGenerateTasks(new List<GenerateResponsibilityTaskViewModel> { viewModel });
            //then
            Assert.That(target.ModelState.IsValid == false);

        }

        [Test]
        public void Given_a_model_with_invalid_startdate_When_generate_task_Then_add_error_to_modelstate()
        {
            //given
            var target = GetTarget();

            var viewModel = new GenerateResponsibilityTaskViewModel
            {
                SiteId = 1L,
                TaskId = 1L,
                ResponsibilityId = 1L,
                Frequency = TaskReoccurringType.Weekly,
                Owner = Guid.NewGuid(),
                StartDate = string.Empty,
                EndDate = DateTime.Now.AddDays(1).ToShortDateString()
            };

            //when
            target.BulkGenerateTasks(new List<GenerateResponsibilityTaskViewModel> { viewModel });
            //then
            Assert.That(target.ModelState.IsValid == false);

        }

        [Test]
        public void Given_a_model_with_invalid_enddate_When_generate_task_Then_add_error_to_modelstate()
        {
            //given
            var target = GetTarget();

            var viewModel = new GenerateResponsibilityTaskViewModel
            {
                SiteId = 1L,
                TaskId = 1L,
                ResponsibilityId = 1L,
                Frequency = TaskReoccurringType.Weekly,
                Owner = Guid.NewGuid(),
                StartDate = DateTime.Now.ToShortDateString(),
                EndDate = string.Empty
            };

            //when
            target.BulkGenerateTasks(new List<GenerateResponsibilityTaskViewModel> { viewModel });
            //then
            Assert.That(target.ModelState.IsValid == false);

        }

        [Test]
        public void Given_a_model_with_enddate_before_startdate_When_generate_task_Then_add_error_to_modelstate()
        {
            //given
            var target = GetTarget();

            var viewModel = new GenerateResponsibilityTaskViewModel
            {
                SiteId = 1L,
                TaskId = 1L,
                ResponsibilityId = 1L,
                Frequency = TaskReoccurringType.Weekly,
                Owner = Guid.NewGuid(),
                StartDate = DateTime.Now.ToShortDateString(),
                EndDate = DateTime.Now.AddDays(-1).ToShortDateString(),
            };

            //when
            target.BulkGenerateTasks(new List<GenerateResponsibilityTaskViewModel> { viewModel });
            //then
            Assert.That(target.ModelState.IsValid == false);

        }

        [Test]
        public void Given_valid_model_When_BulkGenerateTasks_Then_pass_generated_task_guid_to_bus()
        {
            // Given
            CreateManyResponsibilityTaskFromWizardRequest passedRequest = null;
            _responsibilitiesService
                .Setup(x => x.CreateManyResponsibilityTaskFromWizard(It.IsAny<CreateManyResponsibilityTaskFromWizardRequest>()))
                .Callback<CreateManyResponsibilityTaskFromWizardRequest>(y => passedRequest = y);

            var target = GetTarget();

            // When
            target.BulkGenerateTasks(_viewModel);

            // Then
            _bus.Verify(x => x.Publish(It.Is<TaskAssigned>(y => y.TaskGuid == passedRequest.TaskDetails.ElementAt(0).TaskGuid)));
            _bus.Verify(x => x.Publish(It.Is<TaskAssigned>(y => y.TaskGuid == passedRequest.TaskDetails.ElementAt(1).TaskGuid)));
        }

        private WizardController GetTarget()
        {
            var controller = new WizardController(null, null, null, _responsibilitiesService.Object, _bus.Object, _businessSafeSessionManager.Object);
            return TestControllerHelpers.AddUserToController(controller);
        }
    }
}
