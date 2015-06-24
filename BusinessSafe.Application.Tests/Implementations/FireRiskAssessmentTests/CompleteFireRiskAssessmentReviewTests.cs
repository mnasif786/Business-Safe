using System;
using System.Collections.Generic;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Application.Implementations.FireRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.RepositoryContracts;
using NUnit.Framework;
using Moq;

namespace BusinessSafe.Application.Tests.Implementations.FireRiskAssessmentTests
{
    [TestFixture]
    public class CompleteFireRiskAssessmentReviewTests
    {

        private Mock<IFireRiskAssessmentRepository> _riskAssessmentRepo;
        private Mock<IUserForAuditingRepository> _userForAuditingRepo;
        private Mock<IUserRepository> _userRepo;
        private Mock<IPeninsulaLog> _log;
        private Mock<IChecklistRepository> _checklistRepository;
        private Mock<IEmployeeRepository> _employeeRepo;
        private Mock<IQuestionRepository> _questionRepo;
        private Mock<IDocumentParameterHelper> _documentParameterHelper;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentRepo = new Mock<IFireRiskAssessmentRepository>();
            _userForAuditingRepo = new Mock<IUserForAuditingRepository>();
            _userRepo = new Mock<IUserRepository>();
            _log = new Mock<IPeninsulaLog>();
            _checklistRepository = new Mock<IChecklistRepository>();
            _employeeRepo = new Mock<IEmployeeRepository>();
            _questionRepo = new Mock<IQuestionRepository>();
            _documentParameterHelper = new Mock<IDocumentParameterHelper>();
        }

        [Test]
        public void Given_FRA_review_when_completed_then_review_is_Completed()
        {
            // Given
            var fra = new FireRiskAssessment();
            var fraReview = new Mock<RiskAssessmentReview>();
            fra.AddReview( fraReview.Object);
            fra.FireRiskAssessmentChecklists.Add(new FireRiskAssessmentChecklist());

            _userRepo.Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>())).Returns(new User {Id = Guid.NewGuid()});
            _riskAssessmentRepo.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>())).Returns(fra);

            var target = new FireRiskAssessmentService(_riskAssessmentRepo.Object
                                                       , _userForAuditingRepo.Object
                                                       ,  _checklistRepository.Object
                                                       , _questionRepo.Object
                                                       , _documentParameterHelper.Object, _log.Object, null,_userRepo.Object, null, null);


            var request = new CompleteRiskAssessmentReviewRequest {ReviewingUserId = Guid.NewGuid(), ClientId = 123};

            // When
            target.CompleteFireRiskAssessementReview(request);


            // Then
            fraReview.Verify(x => x.Complete(It.IsAny<string>(), It.IsAny<UserForAuditing>()
                                             , It.IsAny<DateTime?>()
                                             , It.IsAny<bool>()
                                             , It.IsAny<IList<CreateDocumentParameters>>(),It.IsAny<User>()));
        }

        [Test]
        public void Given_FRA_review_when_completed_then_FRA_is_saved()
        {
            // Given
            var fra = new FireRiskAssessment();
            var fraReview = new Mock<RiskAssessmentReview>();
            fra.AddReview(fraReview.Object);
            fra.FireRiskAssessmentChecklists.Add(new FireRiskAssessmentChecklist());

            _userRepo.Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>())).Returns(new User { Id = Guid.NewGuid() });
            _riskAssessmentRepo.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>())).Returns(fra);

            var target = new FireRiskAssessmentService(_riskAssessmentRepo.Object
                                                       , _userForAuditingRepo.Object
                                                       , _checklistRepository.Object
                                                       , _questionRepo.Object
                                                       , _documentParameterHelper.Object, _log.Object, null,_userRepo.Object, null, null
                );

            var request = new CompleteRiskAssessmentReviewRequest { ReviewingUserId = Guid.NewGuid(), ClientId = 123 };

            // When
            target.CompleteFireRiskAssessementReview(request);

            // Then
            _riskAssessmentRepo.Verify(x => x.SaveOrUpdate(fra), Times.Once());
        }

    }
}
