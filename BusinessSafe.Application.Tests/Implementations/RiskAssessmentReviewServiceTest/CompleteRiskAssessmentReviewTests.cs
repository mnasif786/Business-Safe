using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.Application.Tests.Builder;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.RiskAssessmentReviewServiceTest
{
    [TestFixture]
    [Category("Unit")]
    public class CompleteRiskAssessmentReviewTests : BaseRiskAssessmentReviewTests
    {
        private User newEmptyUser;
        private RiskAssessmentReview newEmptyRiskAssessmentReview;

        [SetUp]
        public void SetUp()
        {
            base.SetUp();

            _documentParameterHelper.Setup(
                x => x.GetCreateDocumentParameters(It.IsAny<List<CreateDocumentRequest>>(), It.IsAny<long>())).Returns(new List<CreateDocumentParameters>()
                                                                                                                       {
                                                                                                                           new CreateDocumentParameters()
                                                                                                                           {
                                                                                                                               ClientId = 54321,
                                                                                                                               CreatedOn = DateTime.Today,
                                                                                                                               CreatedBy = new UserForAuditing(),
                                                                                                                               Description = "description",
                                                                                                                               DocumentLibraryId = 1,
                                                                                                                               DocumentOriginType = DocumentOriginType.TaskCreated,
                                                                                                                               DocumentType = new DocumentType(),
                                                                                                                               Extension = ".biz",
                                                                                                                               Filename = "filename",
                                                                                                                               FilesizeByte = 12345
                                                                                                                           }
                                                                                                                       });

            newEmptyUser = new User() { Employee = new Employee() };
            newEmptyRiskAssessmentReview = new RiskAssessmentReview() { RiskAssessmentReviewTask = new RiskAssessmentReviewTask() };

            _userRepo = new Mock<IUserRepository>();
            _userRepo.Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>())).Returns(newEmptyUser);

            _employeeRepo = new Mock<IEmployeeRepository>();
            _riskAssessmentRepo = new Mock<IRiskAssessmentRepository>();

            _riskAssessmentReviewRepo = new Mock<IRiskAssessmentReviewRepository>();
            _riskAssessmentReviewRepo.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>())).Returns(newEmptyRiskAssessmentReview);
            _riskAssessmentReviewRepo.Setup(x => x.SaveOrUpdate(It.IsAny<RiskAssessmentReview>()));

            _responsibilityTaskCategoryRepository = new Mock<ITaskCategoryRepository>();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_a_valid_request_When_CompleteRiskAssessmentReview_called_Then_review_repo_asked_to_retrieve_associated_RiskAssessmentReview()
        {
            //Given
            var target = CreateRiskAssessmentReviewService();
            var model = new CompleteRiskAssessmentReviewRequest()
                            {
                                RiskAssessmentReviewId = 1234,
                                ClientId = 5678,
                                NextReviewDate = DateTime.Now
                            };

            var riskAssessmentReview = new RiskAssessmentReview
            {
                RiskAssessmentReviewTask = new RiskAssessmentReviewTask(),
                RiskAssessment = new GeneralRiskAssessment()
            };

            _riskAssessmentReviewRepo
                .Setup(x => x.GetByIdAndCompanyId(1234, 5678))
                .Returns(riskAssessmentReview);

            //When
            target.CompleteRiskAssessementReview(model);

            //Then
            _riskAssessmentReviewRepo.Verify(x => x.GetByIdAndCompanyId(model.RiskAssessmentReviewId, model.ClientId), Times.Once());
        }

        [Test]
        public void Given_a_valid_request_When_CompleteRiskAssessmentReview_called_Then_review_repo_asked_to_update_associated_RiskAssessmentReview()
        {
            //Given
            var passedRiskAssessmentReview = new RiskAssessmentReview
                                                 {
                                                     RiskAssessmentReviewTask = new RiskAssessmentReviewTask(),
                                                     RiskAssessment = new GeneralRiskAssessment()
                                                 };

            _riskAssessmentReviewRepo
                .Setup(x => x.GetByIdAndCompanyId(1234, 5678))
                .Returns(passedRiskAssessmentReview);

            _riskAssessmentReviewRepo.Setup(x => x.SaveOrUpdate(It.IsAny<RiskAssessmentReview>())).Callback
                <RiskAssessmentReview>(y => passedRiskAssessmentReview = y);

            var target = CreateRiskAssessmentReviewService();

            var model = new CompleteRiskAssessmentReviewRequest()
            {
                RiskAssessmentReviewId = 1234,
                ClientId = 5678,
                CompletedComments = "some comments",
                IsComplete = true,
                ReviewingUserId = Guid.NewGuid(),
                NextReviewDate = DateTime.Now,
                Archive = false
            };

            //When
            target.CompleteRiskAssessementReview(model);

            //Then
            _riskAssessmentReviewRepo.Verify(x => x.SaveOrUpdate(It.IsAny<RiskAssessmentReview>()), Times.Once());
            Assert.That(passedRiskAssessmentReview.Comments, Is.EqualTo(model.CompletedComments));
        }

        [Test]
        public void Given_a_valid_request_When_CompleteRiskAssessmentReview_called_Then_entity_asked_to_complete_itself()
        {
            //Given
            var mockRiskAssessmentReview = new Mock<RiskAssessmentReview>();
            mockRiskAssessmentReview.Object.RiskAssessmentReviewTask = new RiskAssessmentReviewTask();
            mockRiskAssessmentReview.Setup(x => x.Complete(It.IsAny<string>(), It.IsAny<UserForAuditing>(), It.IsAny<DateTime?>(), It.IsAny<bool>(), null, It.IsAny<User>()));

            _riskAssessmentReviewRepo.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>())).Returns(mockRiskAssessmentReview.Object);

            var target = CreateRiskAssessmentReviewService();
            var model = new CompleteRiskAssessmentReviewRequest()
            {
                RiskAssessmentReviewId = 1234,
                ClientId = 5678,
                CompletedComments = "comments",
                NextReviewDate = DateTime.Now,
                Archive = false
            };

            //When
            target.CompleteRiskAssessementReview(model);

            //Then
            mockRiskAssessmentReview.Verify(x => x.Complete(model.CompletedComments, It.IsAny<UserForAuditing>(), model.NextReviewDate, model.Archive, It.IsAny<List<CreateDocumentParameters>>(),It.IsAny<User>()), Times.Once());
        }

        [Test]
        public void When_documents_attached_pass_to_entity()
        {
            //Given
            List<CreateDocumentParameters> passedCreateDocumentParameters = null;

            var mockRiskAssessmentReview = new Mock<RiskAssessmentReview>();
            mockRiskAssessmentReview.Object.RiskAssessmentReviewTask = new RiskAssessmentReviewTask();
            mockRiskAssessmentReview
                .Setup(x => x.Complete(
                    It.IsAny<string>(),
                    It.IsAny<UserForAuditing>(),
                    It.IsAny<DateTime?>(),
                    It.IsAny<bool>(),
                    It.IsAny<List<CreateDocumentParameters>>(),
                    It.IsAny<User>()
                    ))
                    .Callback<string, UserForAuditing, DateTime?, bool, IList<CreateDocumentParameters>,User>(
                        (a, b, c, d, e,f) => passedCreateDocumentParameters = e.ToList()
                    );

            _riskAssessmentReviewRepo
                .Setup(x => x.GetByIdAndCompanyId(
                    It.IsAny<long>(),
                    It.IsAny<long>()))
                .Returns(mockRiskAssessmentReview.Object);

            const long companyId = 61889L;

            var returnedCreateDocumentParams =
                new CreateDocumentParameters()
                {
                    ClientId = companyId,
                    CreatedOn = DateTime.Today,
                    CreatedBy = new UserForAuditing(),
                    Description = "description",
                    DocumentLibraryId = 1,
                    DocumentOriginType = DocumentOriginType.TaskCreated,
                    DocumentType = new DocumentType(),
                    Extension = ".biz",
                    Filename = "filename",
                    FilesizeByte = 12345
                };

            _documentParameterHelper.Setup(
                x => x.GetCreateDocumentParameters(It.IsAny<List<CreateDocumentRequest>>(), It.IsAny<long>()))
                .Returns(new List<CreateDocumentParameters>() { returnedCreateDocumentParams });

            var createDocumentsRequests = new List<CreateDocumentRequest>()
                                          {
                                              new CreateDocumentRequestBuilder()
                                              .WithCompanyId(companyId)
                                              .WithDocumentLibraryId(33L)
                                              .WithDocumentOriginTypeId(DocumentOriginType.TaskCompleted)
                                              .WithDocumentType(DocumentTypeEnum.GRAReview)
                                              .WithFilename("my filename.docx")
                                              .WithSiteId(12L)
                                              .Build()
                                          };

            var model = new CompleteRiskAssessmentReviewRequest()
            {
                RiskAssessmentReviewId = 1234L,
                ClientId = companyId,
                CompletedComments = "comments",
                NextReviewDate = DateTime.Now,
                Archive = false,
                CreateDocumentRequests = createDocumentsRequests
            };

            var target = CreateRiskAssessmentReviewService();

            //When
            target.CompleteRiskAssessementReview(model);

            //Then
            mockRiskAssessmentReview.Verify(x => x.Complete(model.CompletedComments, It.IsAny<UserForAuditing>(), model.NextReviewDate, model.Archive, It.IsAny<IList<CreateDocumentParameters>>(), It.IsAny<User>()), Times.Once());
            Assert.That(passedCreateDocumentParameters[0], Is.EqualTo(returnedCreateDocumentParams));
            
            _documentParameterHelper.Verify(x => x.GetCreateDocumentParameters(It.IsAny<List<CreateDocumentRequest>>(), It.IsAny<long>()));

        }

        [Test]
        public void Given_a_valid_request_When_CompleteRiskAssessmentReview_called_Then_user_repo_asked_to_retrieve_associated_User()
        {
            //Given
            var target = CreateRiskAssessmentReviewService();
            var model = new CompleteRiskAssessmentReviewRequest()
            {
                RiskAssessmentReviewId = 1234,
                ClientId = 5678,
                ReviewingUserId = Guid.NewGuid(),
                NextReviewDate = DateTime.Now,
            };

            var riskAssessmentReview = new RiskAssessmentReview
            {
                RiskAssessmentReviewTask = new RiskAssessmentReviewTask(),
                RiskAssessment = new GeneralRiskAssessment()
            };

            _riskAssessmentReviewRepo
                .Setup(x => x.GetByIdAndCompanyId(1234, 5678))
                .Returns(riskAssessmentReview);

            //When
            target.CompleteRiskAssessementReview(model);

            //Then
            _userForAuditingRepo.Verify(x => x.GetByIdAndCompanyId(model.ReviewingUserId, model.ClientId), Times.Once());
        }
    }
}