using System.Collections.Generic;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Controllers;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.FireRiskAssessmentAttachDocument
{
    [TestFixture]
    [Category("Unit")]
    public class SaveTests
    {
        private Mock<IRiskAssessmentAttachmentService> _attachmentService;
        private long _companyId = 100;
        private long _riskAssessmentId = 200;

        [SetUp]
        public void SetUp()
        {
            _attachmentService = new Mock<IRiskAssessmentAttachmentService>();
        }

        [Test]
        public void Given_valid_request_with_documents_to_attach_and_detach_When_save_Then_should_call_correct_methods()
        {
            // Given
            var controller = GetTarget();
            
            var savingDocumentsViewModel = new DocumentsToSaveViewModel()
            {
                CreateDocumentRequests = new List<CreateDocumentRequest>() { new CreateDocumentRequest() },
                DeleteDocumentRequests = new List<long>() { 1, 2 }
            };

            _attachmentService
                .Setup(x => x.AttachDocumentsToRiskAssessment(It.Is<AttachDocumentsToRiskAssessmentRequest>
                                                                  (y => y.CompanyId == _companyId &&
                                                                        y.RiskAssessmentId == _riskAssessmentId &&
                                                                        y.UserId == controller.CurrentUser.UserId &&
                                                                        y.DocumentsToAttach == savingDocumentsViewModel.CreateDocumentRequests)));

            _attachmentService
                .Setup(x => x.DetachDocumentsToRiskAssessment(It.Is<DetachDocumentsFromRiskAssessmentRequest>
                                                                  (y => y.CompanyId == _companyId &&
                                                                        y.RiskAssessmentId == _riskAssessmentId &&
                                                                        y.UserId == controller.CurrentUser.UserId &&
                                                                        y.DocumentsToDetach == savingDocumentsViewModel.DeleteDocumentRequests)));

            // When
            controller.Save(_companyId, _riskAssessmentId, savingDocumentsViewModel);

            // Then
            _attachmentService.VerifyAll();
        }

        [Test]
        public void Given_valid_request_with_no_documents_to_attach_or_detach_When_save_Then_should_call_correct_methods()
        {
            // Given
            var controller = GetTarget();

            var savingDocumentsViewModel = new DocumentsToSaveViewModel()
            {
                CreateDocumentRequests = new List<CreateDocumentRequest>() {  },
                DeleteDocumentRequests = new List<long>() { }
            };
            
            // When
            controller.Save(_companyId, _riskAssessmentId, savingDocumentsViewModel);

            // Then
            _attachmentService.Verify(x => x.AttachDocumentsToRiskAssessment(It.IsAny<AttachDocumentsToRiskAssessmentRequest>()), Times.Never());
            _attachmentService.Verify(x => x.DetachDocumentsToRiskAssessment(It.IsAny<DetachDocumentsFromRiskAssessmentRequest>()), Times.Never());
        }

        [Test]
        public void Given_valid_request_When_save_Then_should_return_correct_action_result()
        {
            // Given
            var controller = GetTarget();

            var savingDocumentsViewModel = new DocumentsToSaveViewModel()
            {
                CreateDocumentRequests = new List<CreateDocumentRequest>() { new CreateDocumentRequest() },
                DeleteDocumentRequests = new List<long>() { 1, 2 }
            };

            // When
            var result = controller.Save(_companyId, _riskAssessmentId, savingDocumentsViewModel) as RedirectToRouteResult;

            // Then
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(result.RouteValues["controller"], Is.EqualTo("Documents"));
            Assert.That(result.RouteValues["companyId"], Is.EqualTo(_companyId));
            Assert.That(result.RouteValues["riskAssessmentId"], Is.EqualTo(_riskAssessmentId));
        }

        private DocumentsController GetTarget()
        {
            var target = new DocumentsController( null, _attachmentService.Object);
            return TestControllerHelpers.AddUserToController(target);
        }
    }
}