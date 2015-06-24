using System.Web.Mvc;
using BusinessSafe.WebSite.Areas.Company.Controllers;
using BusinessSafe.WebSite.Areas.Company.Factories;
using BusinessSafe.WebSite.Areas.Company.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.RiskAssessor
{
    [TestFixture]
    public class EditTests
    {
        private Mock<IAddEditRiskAssessorViewModelFactory> _addEditRiskAssessorViewModelFactory;
        private AddEditRiskAssessorViewModel _viewModel;
        private long _companyId;
        private long _riskAssessorId;

        [SetUp]
        public void Setup()
        {
            _companyId = 88888L;
            _riskAssessorId = 3L;

            _addEditRiskAssessorViewModelFactory = new Mock<IAddEditRiskAssessorViewModelFactory>();

            _viewModel = new AddEditRiskAssessorViewModel
                             {
                                 SaveButtonEnabled = true
                             };

            _addEditRiskAssessorViewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_addEditRiskAssessorViewModelFactory.Object);

            _addEditRiskAssessorViewModelFactory
                .Setup(x => x.WithRiskAssessorId(It.IsAny<long>()))
                .Returns(_addEditRiskAssessorViewModelFactory.Object);

            _addEditRiskAssessorViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(_viewModel);
        }

        [Test]
        public void When_Edit_Then_returns_instance_of_partial_view_result()
        {
            // Given
            var target = GetTarget();

            // When
            var result = target.Edit(_companyId, _riskAssessorId);

            // Then
            Assert.IsInstanceOf<PartialViewResult>(result);
        }

        [Test]
        public void When_Add_Then_returns_correct_view()
        {
            // Given
            var target = GetTarget();

            // When
            var result = target.Edit(_companyId, _riskAssessorId);

            // Then
            Assert.That(result.ViewName, Is.EqualTo("_EditForm"));
        }

        [Test]
        public void When_Edit_Then_returns_AddEditRiskAssessorViewModel()
        {
            // Given
            var target = GetTarget();

            // When
            var result = target.Edit(_companyId, _riskAssessorId);

            // Then
            Assert.IsInstanceOf<AddEditRiskAssessorViewModel>(result.Model);
        }

        [Test]
        public void When_Edit_Then_return_model_retrieved_from_view_model_factory()
        {
            // Given
            var target = GetTarget();

            // When
            var result = target.Edit(_companyId, _riskAssessorId);

            // Then
            _addEditRiskAssessorViewModelFactory.Verify(x => x.WithCompanyId(_companyId));
            _addEditRiskAssessorViewModelFactory.Verify(x => x.WithRiskAssessorId(_riskAssessorId));
            _addEditRiskAssessorViewModelFactory.Verify(x => x.GetViewModel());
            Assert.That(result.Model, Is.EqualTo(_viewModel));
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