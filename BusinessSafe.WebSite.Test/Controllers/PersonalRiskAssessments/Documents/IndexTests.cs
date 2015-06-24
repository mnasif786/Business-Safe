using System;
using System.Collections.Generic;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.ViewModels;
using NUnit.Framework;
using Moq;
using System.Web.Mvc;


namespace BusinessSafe.WebSite.Tests.Controllers.PersonalRiskAssessments.Documents
{
    [TestFixture]
    [Category("Unit")]
    public class IndexTests
    {
        private Mock<IDocumentsViewModelFactory> _viewModelFactory;
        private long _companyId = 100;
        private long _riskAssessmentId = 200;
        
        [SetUp]
        public void SetUp()
        {
            
            _viewModelFactory = new Mock<IDocumentsViewModelFactory>();
            
            _viewModelFactory
                .Setup(x => x.WithCompanyId(_companyId))
                .Returns(_viewModelFactory.Object);

            _viewModelFactory
                .Setup(x => x.WithRiskAssessmentId(_riskAssessmentId))
                .Returns(_viewModelFactory.Object);
            
            _viewModelFactory
                .Setup(x => x.WithDocumentDefaultType(DocumentTypeEnum.PRADocumentType))
                .Returns(_viewModelFactory.Object);
        }

        [Test]
        public void When_get_index_Then_should_call_appropiate_methods()
        {
            // Given
            var controller = GetTarget();

            var viewModel = new DocumentsViewModel();
            _viewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(viewModel);

            // When
            controller.Index(_riskAssessmentId, _companyId);

            // Then
            _viewModelFactory.VerifyAll();
        }

        [Test]
        public void When_get_index_Then_should_return_correct_view()
        {
            // Given
            var controller = GetTarget();

            _viewModelFactory
                .Setup(x => x.WithRiskAssessmentId(_riskAssessmentId))
                .Returns(_viewModelFactory.Object);

            var viewModel = new DocumentsViewModel();
            _viewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(viewModel);

            // When
            var result = controller.Index(_riskAssessmentId, _companyId) as ViewResult;

            // Then
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void When_get_index_Then_should_return_correct_viewmodel_type()
        {
            // Given
            var controller = GetTarget();

            var viewModel = new DocumentsViewModel();
            _viewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(viewModel);

            // When
            var result = controller.Index(_riskAssessmentId, _companyId) as ViewResult;

            // Then
            Assert.That(result.Model, Is.TypeOf<DocumentsViewModel>());
        }

        [Test]
        public void When_get_index_Then_should_allow_you_to_change_document_type_when_you_upload_document()
        {
            // Given
            var mockRiskAssessmentAttachmentService = new Mock<IRiskAssessmentAttachmentService>();
            var mockRiskAssessmentService = new Mock<IRiskAssessmentService>();

            mockRiskAssessmentAttachmentService
                .Setup(x => x.GetRiskAssessmentAttachedDocuments(_riskAssessmentId, _companyId))
                .Returns(new List<DocumentDto>());

            mockRiskAssessmentService
                .Setup(x => x.GetByIdAndCompanyId(_riskAssessmentId, _companyId))
                .Returns(new RiskAssessmentDto
                {
                    CreatedOn = DateTime.Now
                });

            var factory = new DocumentsViewModelFactory(mockRiskAssessmentAttachmentService.Object);

            // When
            var model = factory
                .WithCompanyId(_companyId)
                .WithRiskAssessmentId(_riskAssessmentId)
                .GetViewModel();

            // Then
            Assert.That(model.ExistingDocumentsViewModel.CanEditDocumentType, Is.EqualTo(true));
        }

        private WebSite.Areas.PersonalRiskAssessments.Controllers.DocumentsController GetTarget()
        {
            return new WebSite.Areas.PersonalRiskAssessments.Controllers.DocumentsController(_viewModelFactory.Object, null);
        }
    }
}
