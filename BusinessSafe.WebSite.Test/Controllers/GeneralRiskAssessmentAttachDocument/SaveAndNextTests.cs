using System.Collections.Generic;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Controllers;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.AttachDocument
{
    [TestFixture]
    [Category("Unit")]
    public class SaveAndNextTests
    {
        private Mock<IRiskAssessmentAttachmentService> _attachmentService;
        private Mock<IGeneralRiskAssessmentService> _riskAssessmentService;
        private long _companyId = 100;
        private long _riskAssessmentId = 200;

        [SetUp]
        public void SetUp()
        {
            _attachmentService = new Mock<IRiskAssessmentAttachmentService>();
            _riskAssessmentService = new Mock<IGeneralRiskAssessmentService>();
        }
        
        [Test]
        public void When_post_to_save_next_Then_should_return_correct_result()
        {
            
            // Given
            var target = CreateController();
            
            var savingDocumentsViewModel = new DocumentsToSaveViewModel()
            {
                CreateDocumentRequests = new List<CreateDocumentRequest>() { new CreateDocumentRequest() },
                DeleteDocumentRequests = new List<long>() { 1, 2 }
            };

            // When
            var result = target.SaveAndNext(_companyId, _riskAssessmentId, savingDocumentsViewModel) as JsonResult;

            // Assert
            dynamic data = result.Data;
            Assert.That(data.ToString(), Contains.Substring("Success = True"));
        }
        
        [Test]
        public void Given_valid_request_When_post_save_and_next_Then_should_call_correct_methods()
        {
            // Given
            var controller = CreateController();

            var savingDocumentsViewModel = new DocumentsToSaveViewModel()
                                               {
                                                   CreateDocumentRequests = new List<CreateDocumentRequest>(){ new CreateDocumentRequest()},
                                                   DeleteDocumentRequests = new List<long>(){1,2}
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
            controller.SaveAndNext(_companyId, _riskAssessmentId, savingDocumentsViewModel);

            // Then
            _attachmentService.VerifyAll();
        }


        private DocumentsController CreateController()
        {
            var target = new DocumentsController(null, _riskAssessmentService.Object, _attachmentService.Object);
            return TestControllerHelpers.AddUserToController(target);
        }
    }
}