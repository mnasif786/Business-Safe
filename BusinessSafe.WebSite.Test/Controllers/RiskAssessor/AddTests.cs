using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.WebSite.Areas.Company.Controllers;
using BusinessSafe.WebSite.Areas.Company.Factories;
using BusinessSafe.WebSite.Areas.Company.ViewModels;
using NUnit.Framework;
using Moq;

namespace BusinessSafe.WebSite.Tests.Controllers.RiskAssessor
{
    [TestFixture]
    public class AddTests
    {
        private Mock<IAddEditRiskAssessorViewModelFactory> _addEditRiskAssessorViewModelFactory;
        private AddEditRiskAssessorViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            _addEditRiskAssessorViewModelFactory = new Mock<IAddEditRiskAssessorViewModelFactory>();

            _viewModel = new AddEditRiskAssessorViewModel
                             {
                                 SaveButtonEnabled = true
                             };

            _addEditRiskAssessorViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(_viewModel);
        }

        [Test]
        public void When_Add_Then_calls_correct_methods()
        {
            // Given
            var target = GetTarget();

            // When
            target.Add();

            // Then
            _addEditRiskAssessorViewModelFactory.VerifyAll();
        }

        [Test]
        public void When_Add_Then_returns_correct_view()
        {
            // Given
            var target = GetTarget();

            // When
            var result = target.Add();

            // Then
            Assert.That(result.ViewName, Is.EqualTo("_AddForm"));
        }

        [Test]
        public void When_Add_Then_returns_correct_view_model()
        {
            // Given
            var target = GetTarget();

            // When
            var result = target.Add();
            var model = result.Model as AddEditRiskAssessorViewModel;

            // Then
            Assert.IsTrue(model.SaveButtonEnabled);
        }

        private RiskAssessorController GetTarget()
        {
            var controller = new RiskAssessorController(
                _addEditRiskAssessorViewModelFactory.Object,
                null,
                null,
                null
                );
            return TestControllerHelpers.AddUserToController(controller);
        }
    }
}

