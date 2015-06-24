using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Checklists;
using BusinessSafe.Application.Request.Checklists;
using BusinessSafe.Checklists.Controllers;
using BusinessSafe.Checklists.ViewModelFactories;
using BusinessSafe.Checklists.ViewModels;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Messages.Commands;
using Moq;
using NUnit.Framework;
using NServiceBus;

namespace BusinessSafe.Checklists.Test.Controllers.Home
{
    [TestFixture]
    public class CompleteTest
    {
        private HomeController _target;
        private Mock<IEmployeeChecklistService>  _employeeChecklistService;
        private Mock<IEmployeeChecklistViewModelFactory>  _employeeChecklistViewModelFactory;
        private Mock<IBus> _bus;

        [SetUp]
        public void Setup()
        {
            _employeeChecklistService = new Mock<IEmployeeChecklistService>();
            _employeeChecklistService.Setup(x => x.Complete(It.IsAny<CompleteEmployeeChecklistRequest>()));
            _employeeChecklistService.Setup(x => x.Save(It.IsAny<SaveEmployeeChecklistRequest>()));
            _employeeChecklistViewModelFactory = new Mock<IEmployeeChecklistViewModelFactory>();
            _bus = new Mock<IBus>();
        }

        [Test]
        public void Given_i_have_answered_all_question_When_I_click_on_complete_Then_sned_complete_checklist_command()
        {
            // Given
             var questions = new List<QuestionViewModel>()
                            {
                                new QuestionViewModel() { Id = 1, QuestionType = QuestionType.YesNo, Answer = new AnswerViewModel()}
                            };

            var model = new EmployeeChecklistViewModel()
            {
                EmployeeChecklistId = Guid.NewGuid(),
                Sections = new List<SectionViewModel>()
                                                         {
                                                             new SectionViewModel()
                                                             {
                                                                 Questions = questions
                                                             }
                                                         }
            };

            CompleteEmployeeChecklistRequest passedCompleteEmployeeChecklistRequest = null;
            _employeeChecklistService
                .Setup(x => x.Complete(It.IsAny<CompleteEmployeeChecklistRequest>()))
                .Callback<CompleteEmployeeChecklistRequest>(z => passedCompleteEmployeeChecklistRequest = z);

            _employeeChecklistService
                .Setup(x => x.ValidateComplete(It.IsAny<CompleteEmployeeChecklistRequest>()))
                .Returns(new ValidationMessageCollection());

            _target = GetTarget();

            // When
            _target.Complete(model, new FormCollection());

            // Then
            _bus.Verify(x => x.Send(It.Is<CompleteEmployeeChecklist>(y => y.EmployeeChecklistId == model.EmployeeChecklistId)));
        }

        [Test]
        [Ignore("Complete is no longer individually calling Save")]
        public void Given_i_have_answered_all_question_When_I_click_on_complete_Then_checklist_service_save_is_called()
        {
            // Given
            var questions = new List<QuestionViewModel>()
                            {
                                new QuestionViewModel() { Id = 1, QuestionType = QuestionType.YesNo, Answer = new AnswerViewModel() }
                            };

            var model = new EmployeeChecklistViewModel()
            {
                EmployeeChecklistId = Guid.NewGuid(),
                Sections = new List<SectionViewModel>()
                                                         {
                                                             new SectionViewModel()
                                                             {
                                                                 Questions = questions
                                                             }
                                                         }
            };

            _target = GetTarget();

            // When
            _target.Complete(model, null);

            // Then
            _employeeChecklistService.Verify(x => x.Save(It.IsAny<SaveEmployeeChecklistRequest>()));

        }

        [Test]
        public void Given_the_EmployeeChecklist_has_already_been_completed_When_I_try_to_complete_it_again_Then_complete_is_not_called()
        {
            // Given
            var questions = new List<QuestionViewModel>()
                            {
                                new QuestionViewModel() { Id = 1, QuestionType = QuestionType.YesNo, Answer = new AnswerViewModel()}
                            };

            var model = new EmployeeChecklistViewModel()
            {
                EmployeeChecklistId = Guid.NewGuid(),
                Sections = new List<SectionViewModel>()
                                                         {
                                                             new SectionViewModel()
                                                             {
                                                                 Questions = questions
                                                             }
                                                         }
            };

            CompleteEmployeeChecklistRequest passedCompleteEmployeeChecklistRequest = null;

            _employeeChecklistService
                .Setup(x => x.Complete(It.IsAny<CompleteEmployeeChecklistRequest>()))
                .Callback<CompleteEmployeeChecklistRequest>(z => passedCompleteEmployeeChecklistRequest = z);

            _employeeChecklistService
                .Setup(x => x.ValidateComplete(It.IsAny<CompleteEmployeeChecklistRequest>()))
                .Returns(new ValidationMessageCollection
                             {
                                 new ValidationMessage(MessageType.Error, "This checklist has already been completed once and cannot be resubmitted.")
                             });

            _target = GetTarget();

            // When
            var result = _target.Complete(model, new FormCollection()) as ViewResult;

            // Then
            var errors = result.ViewData.ModelState.Values.ToList();
            _employeeChecklistService.Verify(x => x.Complete(It.IsAny<CompleteEmployeeChecklistRequest>()), Times.Never());
            Assert.That(errors.Count, Is.EqualTo(1));
            Assert.That(errors[0].Errors[0].ErrorMessage, Is.EqualTo("This checklist has already been completed once and cannot be resubmitted."));
        }

        private HomeController GetTarget()
        {
            return new HomeController(_employeeChecklistService.Object, _employeeChecklistViewModelFactory.Object, _bus.Object);
        }
    }
}
