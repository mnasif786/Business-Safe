using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessSafe.Checklists.CustomModelBinders;
using BusinessSafe.Checklists.ViewModelFactories;
using BusinessSafe.Checklists.ViewModels;
using BusinessSafe.Domain.Entities;

using Moq;
using NUnit.Framework;

namespace BusinessSafe.Checklists.Test.CustomModelBinders
{
    [TestFixture]
    public class EmployeeChecklistViewModelBinderTests
    {
        private Mock<EmployeeChecklistViewModelBinder> _target;
        private Mock<ControllerContext> _controllerContext;
        private Mock<ModelBindingContext> _modelBindingContext;
        private Mock<HttpContextBase> _httpContext;
        private Mock<HttpRequestBase> _httpRequestBase;
        private Mock<IEmployeeChecklistViewModelFactory> _employeeChecklistViewModelFactory;
        private EmployeeChecklistViewModel _employeeChecklistViewModel;
        private NameValueCollection form;

        [SetUp]
        public void Setup()
        {
            _employeeChecklistViewModelFactory = new Mock<IEmployeeChecklistViewModelFactory>();
            form = new NameValueCollection();

            _controllerContext = new Mock<ControllerContext>();
            _httpContext = new Mock<HttpContextBase>();
            _httpRequestBase = new Mock<HttpRequestBase>();
            _controllerContext.Setup(x => x.HttpContext).Returns(_httpContext.Object);
            _httpContext.Setup(x => x.Request).Returns(_httpRequestBase.Object);
            _httpRequestBase.Setup(x => x.Form).Returns(form);

            _modelBindingContext = new Mock<ModelBindingContext>();
        }

        [TestCase("Yes", true)]
        [TestCase("No", false)]
        public void When_answering_a_yesno_question_answer_is_set_correctly(string nameValue, bool answer)
        {
            // Given
            var questions = new List<QuestionViewModel>()
                            {
                                new QuestionViewModel() { Id = 1, QuestionType = QuestionType.YesNo }
                            };

            _employeeChecklistViewModel = new EmployeeChecklistViewModel()
            {
                Sections = new List<SectionViewModel>()
                                                         {
                                                             new SectionViewModel()
                                                             {
                                                                 Questions = questions
                                                             }
                                                         }
            };

            _target = GetTarget(_employeeChecklistViewModel);

            form = new NameValueCollection()
                   {
                       { "YesNo_1", nameValue }
                   };
            _httpRequestBase.Setup(x => x.Form).Returns(form);


            // When
            var result = _target.Object.BindModel(_controllerContext.Object, _modelBindingContext.Object) as EmployeeChecklistViewModel;

            // Then
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Sections);
            Assert.IsNotNull(result.Sections[0].Questions);
            Assert.That(result.Sections[0].Questions[0].Answer.BooleanResponse == answer);
        }

        [TestCase("Yes", "this is the text blah", true)]
        [TestCase("No", "some more text", false)]
        public void When_answering_a_yesno_question_with_text_answer_is_set_accordingly_with_the_entered_text(string nameValue, string enteredText, bool answer)
        {
            // Given
            var questions = new List<QuestionViewModel>()
                            {
                                new QuestionViewModel() { Id = 1, QuestionType = QuestionType.YesNoWithAdditionalInfo }
                            };

            _employeeChecklistViewModel = new EmployeeChecklistViewModel()
            {
                Sections = new List<SectionViewModel>()
                                                         {
                                                             new SectionViewModel()
                                                             {
                                                                 Questions = questions
                                                             }
                                                         }
            };

            _target = GetTarget(_employeeChecklistViewModel);

            form = new NameValueCollection()
                   {
                       { "YesNo_1", nameValue }
                       ,{"AdditionalInfo_1", enteredText}
                   };
            _httpRequestBase.Setup(x => x.Form).Returns(form);


            // When
            var result = _target.Object.BindModel(_controllerContext.Object, _modelBindingContext.Object) as EmployeeChecklistViewModel;

            // Then
            Assert.That(result.Sections[0].Questions[0].Answer.BooleanResponse == answer);
            Assert.That(result.Sections[0].Questions[0].Answer.AdditionalInfo == enteredText);
        }

        [TestCase(32, "this is the text blah")]
        [TestCase(33, "some more text")]
        public void When_answering_a_text_question_answer_is_set_accordingly_with_the_entered_text(int questionId, string enteredText)
        {
            // Given
            var questions = new List<QuestionViewModel>()
                            {
                                new QuestionViewModel() { Id = 45, QuestionType = QuestionType.AdditionalInfo }, 
                                new QuestionViewModel() { Id = questionId, QuestionType = QuestionType.AdditionalInfo }
                            };

            _employeeChecklistViewModel = new EmployeeChecklistViewModel()
            {
                Sections = new List<SectionViewModel>()
                                                         {
                                                             new SectionViewModel()
                                                             {
                                                                 Questions = questions
                                                             }
                                                         }
            };

            _target = GetTarget(_employeeChecklistViewModel);

            form = new NameValueCollection()
                   {
                       {"AdditionalInfo_" + questionId.ToString(), enteredText}
                   };
            _httpRequestBase.Setup(x => x.Form).Returns(form);


            // When
            var result = _target.Object.BindModel(_controllerContext.Object, _modelBindingContext.Object) as EmployeeChecklistViewModel;

            // Then
            Assert.AreEqual(enteredText, result.Sections[0].Questions.First(x => x.Id == questionId).Answer.AdditionalInfo);

        }

        [TestCase("2F5D7B9F-E0CD-4E60-B7DA-8106701FA18E")]
        [TestCase("F53EF447-B2C9-4354-A0F0-FEA71895732F")]
        [TestCase("A07FAF97-4CE0-43E4-A187-C029993E9274")]
        public void requests_correct_EmployeeChecklist(string checklistId)
        {
            // Given
            _employeeChecklistViewModel = new EmployeeChecklistViewModel()
            {
                Sections = new List<SectionViewModel>()
            };

            _target = GetTarget(_employeeChecklistViewModel);

            form = new NameValueCollection()
                   {
                       { "EmployeeChecklistId", checklistId }
                   };
            _httpRequestBase.Setup(x => x.Form).Returns(form);


            // When
            var result = _target.Object.BindModel(_controllerContext.Object, _modelBindingContext.Object) as EmployeeChecklistViewModel;

            // Then
            _employeeChecklistViewModelFactory.Verify(x => x.WithEmployeeChecklistId(Guid.Parse(checklistId)), Times.Once());
        }

        private Mock<EmployeeChecklistViewModelBinder> GetTarget(EmployeeChecklistViewModel employeeChecklistViewModel)
        {
            _employeeChecklistViewModelFactory
                .Setup(x => x.WithEmployeeChecklistId(It.IsAny<Guid>()))
                .Returns(_employeeChecklistViewModelFactory.Object);

            _employeeChecklistViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(employeeChecklistViewModel);

            var target = new Mock<EmployeeChecklistViewModelBinder>() { CallBase = true };
            target.Setup(x => x.EmployeeChecklistViewModelFactory).Returns(_employeeChecklistViewModelFactory.Object);

            return target;
        }
    }
}

