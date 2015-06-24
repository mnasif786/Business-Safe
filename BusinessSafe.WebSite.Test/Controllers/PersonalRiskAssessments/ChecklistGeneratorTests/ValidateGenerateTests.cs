using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;
using NUnit.Framework;
using Moq;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Tests.Controllers.PersonalRiskAssessments.ChecklistGeneratorTests
{
    [TestFixture]
    public class ValidateGenerateTests
    {
        private Mock<EmployeeChecklistGeneratorViewModel> _passedViewModel;
        private List<ValidationResult> _validationResultWithErrors;
        private Mock<IBusinessSafeSessionManager> _businessSafeSessionManager;

        [SetUp]
        public void Setup()
        {
            _passedViewModel = new Mock<EmployeeChecklistGeneratorViewModel>();
            _passedViewModel
                .Setup(x => x.Validate(It.IsAny<ValidationContext>(), It.IsAny<ModelStateDictionary>()))
                .Returns(new List<ValidationResult>());

            _validationResultWithErrors = new List<ValidationResult>
            {
                new ValidationResult("some message")
            };

            _businessSafeSessionManager = new Mock<IBusinessSafeSessionManager>();
        }

        [Test]
        public void correct_Json_is_returned()
        {
            // Given
            var target = GetTarget();

            // When
            var result = target.ValidateGenerate(_passedViewModel.Object);

            // Then
            Assert.IsInstanceOf<JsonResult>(result);
        }

        [Test]
        public void correct_Json_data_is_not_null()
        {
            // Given
            var target = GetTarget();

            // When
            var result = target.ValidateGenerate(_passedViewModel.Object);

            // Then
            Assert.That(result.Data, Is.Not.Null);
        }

        [Test]
        public void viewmodel_validation_is_called()
        {
            // Given
            var target = GetTarget();

            // When
            var result = target.ValidateGenerate(_passedViewModel.Object);

            // Then
            _passedViewModel.VerifyAll();
        }

        [Test]
        public void When_viewmodel_is_valid_return_success_json()
        {
            // Given
            var target = GetTarget();

            // When
            dynamic result = target.ValidateGenerate(_passedViewModel.Object);

            // Then
            Assert.IsTrue(result.Data.success);
        }

        [Test]
        public void When_viewmodel_is_invalid_return_failed_json()
        {
            // Given
            var target = GetTarget();

            _passedViewModel
                .Setup(x => x.Validate(It.IsAny<ValidationContext>(), It.IsAny<ModelStateDictionary>()))
                .Returns(_validationResultWithErrors);

            // When
            dynamic result = target.ValidateGenerate(_passedViewModel.Object);

            // Then
            Assert.IsFalse(result.Data.success);
        }

        [Test]
        public void When_viewmodel_has_errors_return_them_in_json()
        {
            // Given
            var target = GetTarget();

            _passedViewModel
                .Setup(x => x.Validate(It.IsAny<ValidationContext>(), It.IsAny<ModelStateDictionary>()))
                .Returns(_validationResultWithErrors);

            // When
            dynamic result = target.ValidateGenerate(_passedViewModel.Object);

            // Then
            Assert.That(result.Data.errors.Count, Is.EqualTo(_validationResultWithErrors.Count()));
            for(var i = 0; i < _validationResultWithErrors.Count(); i++)
            {
                Assert.That(result.Data.errors[i], Is.EqualTo(_validationResultWithErrors[i].ErrorMessage));
            }
        }

        private ChecklistGeneratorController GetTarget()
        {
            var controller = new ChecklistGeneratorController(null, null, null, null, null, _businessSafeSessionManager.Object);
            return TestControllerHelpers.AddUserToController(controller);
        }
    }
}
