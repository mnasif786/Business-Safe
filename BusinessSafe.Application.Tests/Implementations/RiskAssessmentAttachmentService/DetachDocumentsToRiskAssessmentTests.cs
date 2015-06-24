using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.RiskAssessmentAttachmentService
{
    [TestFixture]
    [Category("Unit")]
    public class DetachDocumentsToRiskAssessmentTests
    {
        private Mock<IRiskAssessmentRepository> _riskAssessmentRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private UserForAuditing _user;
        private Mock<IPeninsulaLog> _log;
        private Mock<IDocumentTypeRepository> _documentTypeRepository;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentRepository = new Mock<IRiskAssessmentRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _user = new UserForAuditing();
            _log = new Mock<IPeninsulaLog>();
            _documentTypeRepository = new Mock<IDocumentTypeRepository>();
            _documentTypeRepository.Setup(x => x.GetById(It.IsAny<long>())).Returns(new DocumentType());
        }

        [Test]
        public void Given_valid_request_When_DetachDocuments_is_called_Then_should_call_appropiate_methods()
        {
            //Given
            var riskAssessmentService = CreateRiskAssessmentService();
            var detachDocumentsFromRiskAssessmentRequest = new DetachDocumentsFromRiskAssessmentRequest()
                                                               {
                                                                   DocumentsToDetach = new List<long>()
                                                                                           {
                                                                                               1,
                                                                                               2
                                                                                           },
                                                                   RiskAssessmentId = 2,
                                                                   CompanyId = 1,
                                                                   UserId = Guid.NewGuid()
                                                               };

            var mockRiskAssessment = new Mock<GeneralRiskAssessment>();
            _riskAssessmentRepository
                .Setup(rr => rr.GetByIdAndCompanyId(detachDocumentsFromRiskAssessmentRequest.RiskAssessmentId, detachDocumentsFromRiskAssessmentRequest.CompanyId))
                .Returns(mockRiskAssessment.Object);

            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(detachDocumentsFromRiskAssessmentRequest.UserId, detachDocumentsFromRiskAssessmentRequest.CompanyId))
                .Returns(_user);

            //When
            riskAssessmentService.DetachDocumentsToRiskAssessment(detachDocumentsFromRiskAssessmentRequest);

            //Then
            _riskAssessmentRepository.Verify(x => x.SaveOrUpdate(mockRiskAssessment.Object));
            _userRepository.Verify(x => x.GetByIdAndCompanyId(detachDocumentsFromRiskAssessmentRequest.UserId, detachDocumentsFromRiskAssessmentRequest.CompanyId));
            mockRiskAssessment.Verify(x => x.DetachDocumentFromRiskAssessment(It.Is<IEnumerable<long>>(y => y.Count() == detachDocumentsFromRiskAssessmentRequest.DocumentsToDetach.Count), _user), Times.Once());
        }

        private Application.Implementations.RiskAssessments.RiskAssessmentAttachmentService CreateRiskAssessmentService()
        {
            var riskAssessmentService = new Application.Implementations.RiskAssessments.RiskAssessmentAttachmentService(_riskAssessmentRepository.Object, _userRepository.Object, _documentTypeRepository.Object, null, _log.Object, null);
            return riskAssessmentService;
        }
    }
}