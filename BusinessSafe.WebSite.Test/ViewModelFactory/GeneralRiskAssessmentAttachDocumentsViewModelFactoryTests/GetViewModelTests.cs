using System;
using System.Linq;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.GeneralRiskAssessmentAttachDocumentsViewModelFactoryTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetViewModelTests
    {
        private Mock<IRiskAssessmentAttachmentService> _riskAssessmentAttachmentService;
        private const long _companyId = 1234;
        private const long _riskAssessmentId = 5678;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentAttachmentService = new Mock<IRiskAssessmentAttachmentService>();
        }

        [Test]
        public void When_get_view_model_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateViewModelFactory();


            var documents = new DocumentDto[] { };
            _riskAssessmentAttachmentService.Setup(
                x => x.GetRiskAssessmentAttachedDocuments(_riskAssessmentId, _companyId)).Returns(documents);

            //When
            target
                .WithCompanyId(_companyId)
                .WithRiskAssessmentId(_riskAssessmentId)
                .GetViewModel();

            //Then
            _riskAssessmentAttachmentService.VerifyAll();
        }

        [Test]
        public void When_get_view_model_Then_return_correct_view_model()
        {
            //Given
            var target = CreateViewModelFactory();


            var firstDocument = new DocumentDto()
            {
                Id = 1,
                Description = "1",
                DocumentLibraryId = 1,
                DocumentType = new DocumentTypeDto { Name = "1" },
                Filename = "1"
            };

            var secondDocument = new DocumentDto()
            {
                Id = 2,
                Description = "2",
                DocumentLibraryId = 2,
                DocumentType = new DocumentTypeDto { Name = "1" },
                Filename = "2"
            };

            var documents = new DocumentDto[] { firstDocument, secondDocument };
            _riskAssessmentAttachmentService.Setup(
                x => x.GetRiskAssessmentAttachedDocuments(_riskAssessmentId, _companyId)).Returns(documents);

            //When
            var result = target
                            .WithCompanyId(_companyId)
                            .WithRiskAssessmentId(_riskAssessmentId)
                            .GetViewModel();

            //Then
            Assert.That(result, Is.TypeOf<DocumentsViewModel>());
            Assert.That(result.CompanyId, Is.EqualTo(_companyId));
            Assert.That(result.RiskAssessmentId, Is.EqualTo(_riskAssessmentId));
            Assert.That(result.ExistingDocumentsViewModel.PreviouslyAddedDocuments.Count, Is.EqualTo(documents.Length));

            var returnedFirstDocument = result.ExistingDocumentsViewModel.PreviouslyAddedDocuments.First();
            Assert.That(returnedFirstDocument.Id, Is.EqualTo(firstDocument.Id));
            Assert.That(returnedFirstDocument.Description, Is.EqualTo(firstDocument.Description));
            Assert.That(returnedFirstDocument.DocumentLibraryId, Is.EqualTo(firstDocument.DocumentLibraryId));
            Assert.That(returnedFirstDocument.DocumentTypeName, Is.EqualTo(firstDocument.DocumentType.Name));
            Assert.That(returnedFirstDocument.Filename, Is.EqualTo(firstDocument.Filename));

            var returnedSecondDocument = result.ExistingDocumentsViewModel.PreviouslyAddedDocuments.Skip(1).Take(1).First();
            Assert.That(returnedSecondDocument.Id, Is.EqualTo(secondDocument.Id));
            Assert.That(returnedSecondDocument.Description, Is.EqualTo(secondDocument.Description));
            Assert.That(returnedSecondDocument.DocumentLibraryId, Is.EqualTo(secondDocument.DocumentLibraryId));
            Assert.That(returnedSecondDocument.DocumentTypeName, Is.EqualTo(secondDocument.DocumentType.Name));
            Assert.That(returnedSecondDocument.Filename, Is.EqualTo(secondDocument.Filename));
        }

        private DocumentsViewModelFactory CreateViewModelFactory()
        {
            return new DocumentsViewModelFactory(_riskAssessmentAttachmentService.Object);
        }
    }
}