using System;
using System.Collections.Generic;

using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.Application.Request.Responsibilities;
using BusinessSafe.WebSite.Areas.Responsibilities.Controllers;
using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.ViewModels;

using Moq;
using NUnit.Framework;

using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Tests.Controllers.Responsibilities.ResponsibilityTasks
{
    [TestFixture]
    [Category("Unit")]
    public class POST_CompleteTest
    {
        private CompleteResponsibilityTaskViewModel _completeViewModel;
        private DocumentsToSaveViewModel _documentsToSaveViewModel;
        private Mock<IResponsibilityTaskService> _responsibilityTaskService;
        private Mock<IBusinessSafeSessionManager> _businessSafeSessionManager;
        private const long _responsibilityTaskId = 79214L;

        [SetUp]
        public void Setup()
        {
            _businessSafeSessionManager = new Mock<IBusinessSafeSessionManager>();
            _businessSafeSessionManager.Setup(x => x.CloseSession());
           _responsibilityTaskService = new Mock<IResponsibilityTaskService>();
            _completeViewModel = new CompleteResponsibilityTaskViewModel
                                {
                                    CompanyId = 1234L,
                                    CompletedComments = "comments",
                                    ResponsibilityId = 4653L,
                                    ResponsibilitySummary = new ResponsibilitySummaryViewModel
                                                            {
                                                                Description = "description",
                                                                Id = 45763L,
                                                                Title = "title"
                                                            },
                                                            ResponsibilityTask = new ViewResponsibilityTaskViewModel
                                                                                 {
                                                                                     ResponsibilityTaskId = _responsibilityTaskId
                                                                                 },
                                    ResponsibilityTaskId = _responsibilityTaskId
                                };
            _documentsToSaveViewModel = new DocumentsToSaveViewModel
                                        {
                                            CreateDocumentRequests = new List<CreateDocumentRequest>
                                                                     {
                                                                         new CreateDocumentRequest() { }
                                                                     },
                                            DeleteDocumentRequests = new List<long> { 1234L, 3573L }
                                        };
        }

        [Test]
        public void Given_invalid_view_model_When_Complete_Then_throw_exception()
        {
            // Given
            var target = GetTarget();

            // When
            target.ModelState.AddModelError("error", "an error");

            // Then
            Assert.Throws<ArgumentException>(() => target.Complete(_completeViewModel, _documentsToSaveViewModel));
        }

        [Test]
        public void When_Complete_Then_pass_request_to_service()
        {
            // Given
            var target = GetTarget();

            // When
            target.Complete(_completeViewModel, _documentsToSaveViewModel);

            // Then
            _responsibilityTaskService.Verify(x => x.Complete(It.Is<CompleteResponsibilityTaskRequest>(y => 
                y.CompanyId == _completeViewModel.CompanyId &&
                y.ResponsibilityTaskId == _completeViewModel.ResponsibilityTaskId &&
                y.UserId == TestControllerHelpers.UserIdAssigned &&
                y.CreateDocumentRequests == _documentsToSaveViewModel.CreateDocumentRequests &&
                y.DocumentLibraryIdsToDelete == _documentsToSaveViewModel.DeleteDocumentRequests &&
                y.CompletedDate.ToLocalShortDateString() == DateTime.Now.ToShortDateString()
            )));
        }

        [Test]
        public void When_Complete_Then_return_json_object_with_responsibility_id()
        {
            // Given
            var target = GetTarget();

            // When
            dynamic result = target.Complete(_completeViewModel, _documentsToSaveViewModel);

            // Then
            Assert.That(result.Data.Success, Is.True);
            Assert.That(result.Data.Id, Is.EqualTo(_responsibilityTaskId));
        }

        [Test]
        public void When_Complete_Then_use_seperate_sessions_from_complete_and_send_notification()
        {
            // Given
            var target = GetTarget();

            // When
            target.Complete(_completeViewModel, _documentsToSaveViewModel);

            // Then
            _businessSafeSessionManager.Verify(x => x.CloseSession(), Times.Exactly(2));
        }

        [Test]
        public void When_Complete_Then_tell_service_to_send_notification_email()
        {
            // Given
            var target = GetTarget();

            // When
            target.Complete(_completeViewModel, _documentsToSaveViewModel);

            // Then
            _responsibilityTaskService.Verify(x => x.SendTaskCompletedNotificationEmail(It.Is<CompleteResponsibilityTaskRequest>(y => 
                y.CompanyId == _completeViewModel.CompanyId &&
                y.ResponsibilityTaskId == _completeViewModel.ResponsibilityTaskId &&
                y.UserId == TestControllerHelpers.UserIdAssigned &&
                y.CreateDocumentRequests == _documentsToSaveViewModel.CreateDocumentRequests &&
                y.DocumentLibraryIdsToDelete == _documentsToSaveViewModel.DeleteDocumentRequests &&
                y.CompletedDate.ToLocalShortDateString() == DateTime.Now.ToShortDateString())));
        }

        private ResponsibilityController GetTarget()
        {
            return TestControllerHelpers.AddUserToController(new ResponsibilityController(null,
                null,
                _responsibilityTaskService.Object,
                null,
                null,
                null,
                null, null,
                null, 
                null,
                _businessSafeSessionManager.Object));
        }
    }
}
