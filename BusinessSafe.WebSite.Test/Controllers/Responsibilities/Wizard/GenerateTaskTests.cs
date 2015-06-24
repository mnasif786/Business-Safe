using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.Request.Responsibilities;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Responsibilities.Controllers;
using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels.Wizard;
using Moq;
using NServiceBus;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Tests.Controllers.Responsibilities.Wizard
{
    [TestFixture]
    public class GenerateTaskTests
    {
        private Mock<IResponsibilitiesService> _responsibilitiesService;
        private Mock<IBusinessSafeSessionManager> _businessSafeSessionManager;
        private Mock<IBus> _bus;

        private GenerateResponsibilityTaskViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            _businessSafeSessionManager = new Mock<IBusinessSafeSessionManager>();
            _bus = new Mock<IBus>();

            _responsibilitiesService = new Mock<IResponsibilitiesService>();
            _responsibilitiesService.Setup(
                x => x.CreateResponsibilityTaskFromWizard(It.IsAny<CreateResponsibilityTasksFromWizardRequest>()));

            _viewModel = new GenerateResponsibilityTaskViewModel
            {
                SiteId = 1L,
                TaskId = 1L,
                ResponsibilityId = 1L,
                Frequency = TaskReoccurringType.Weekly,
                Owner = Guid.NewGuid(),
                StartDate = DateTime.Now.ToShortDateString(),
                EndDate = DateTime.Now.AddDays(1).ToShortDateString()
            };
        }

        [Test]
        public void Given_valid_model_When_generate_task_Then_return_json_success_equals_true()
        {
            //given
            var target = GetTarget();
            
            //when
            dynamic result = target.GenerateTask(_viewModel);
            //then
            Assert.That(result.Data.Success, Is.True);

        }

        [Test]
        public void Given_a_model_with_invalid_frequencty_When_generate_task_Then_add_error_to_modelstate()
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
            target.GenerateTask(viewModel);
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
            target.GenerateTask(viewModel);
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
            target.GenerateTask(viewModel);
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
            target.GenerateTask(viewModel);
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
            target.GenerateTask(viewModel);
            //then
            Assert.That(target.ModelState.IsValid == false);
        }

        [Test]
        public void Given_a_model_without_an_enddate_When_generate_task_Then_do_not_add_error_to_modelstate()
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
                EndDate = null,
            };

            //when
            target.GenerateTask(viewModel);

            //then
            Assert.That(target.ModelState.IsValid);
        }

        [Test]
        public void Given_a_model_with_startdate_before_today_When_generate_task_Then_add_error_to_modelstate()
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
                StartDate = DateTime.Now.AddDays(-1).ToShortDateString(),
                EndDate = DateTime.Now.AddDays(5).ToShortDateString(),
            };

            //when
            target.GenerateTask(viewModel);

            //then
            Assert.That(target.ModelState.IsValid == false);
        }

        [Test]
        public void Given_valid_model_When_GenerateResponsibilityTask_Then_map_save_request()
        {
            // Given
            var target = GetTarget();

            // When
            target.GenerateTask(_viewModel);

            // Then
            _responsibilitiesService.Verify(x => x.CreateResponsibilityTaskFromWizard(It.Is<CreateResponsibilityTasksFromWizardRequest>(y =>
                y.CompanyId == TestControllerHelpers.CompanyIdAssigned &&
                y.SiteId == _viewModel.SiteId &&
                y.TaskTemplateId == _viewModel.TaskId &&
                y.Frequency == _viewModel.Frequency &&
                y.AssigneeId == _viewModel.Owner &&
                y.StartDate == DateTime.Parse(_viewModel.StartDate) &&
                y.EndDate == DateTime.Parse(_viewModel.EndDate) &&
                y.UserId == TestControllerHelpers.UserIdAssigned
            )));
        }

        [Test]
        public void Given_valid_model_When_GenerateResponsibilityTask_Then_call_service()
        {
            // Given
            var target = GetTarget();

            // When
            target.GenerateTask(_viewModel);

            // Then
            _responsibilitiesService.Verify(x => x.CreateResponsibilityTaskFromWizard(It.IsAny<CreateResponsibilityTasksFromWizardRequest>()));
        }


        private WizardController GetTarget()
        {
            var controller = new WizardController(null, null, null, _responsibilitiesService.Object, _bus.Object, _businessSafeSessionManager.Object);
            return TestControllerHelpers.AddUserToController(controller);
        }
    }
}
