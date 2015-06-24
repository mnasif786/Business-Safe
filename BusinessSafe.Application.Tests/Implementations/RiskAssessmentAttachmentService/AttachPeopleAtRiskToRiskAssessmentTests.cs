using System;
using System.Collections.Generic;

using BusinessSafe.Application.Implementations.GeneralRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;
using StructureMap;

namespace BusinessSafe.Application.Tests.Implementations.RiskAssessmentAttachmentService
{
    [TestFixture]
    [Category("Unit")]
    public class AttachPeopleAtRiskToRiskAssessmentTests
    {
        private Mock<IGeneralRiskAssessmentRepository> _riskAssessmentRepository;
        private Mock<IPeopleAtRiskRepository> _peopleAtRiskRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private UserForAuditing _user;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentRepository = new Mock<IGeneralRiskAssessmentRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _peopleAtRiskRepository = new Mock<IPeopleAtRiskRepository>();
            _user = new UserForAuditing();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_valid_request_When_AttachPeopleAtRisk_is_called_Then_should_call_appropiate_methods()
        {
            //Given
            var riskAssessmentService = CreateRiskAssessmentService();
            var attachPeopleAtRiskToRiskAssessmentRequest = new AttachPeopleAtRiskToRiskAssessmentRequest()
                                           {
                                               PeopleAtRiskIds = new List<long> { 1, 2, 3 },
                                               RiskAssessmentId = 2,
                                               CompanyId = 1,
                                               UserId = Guid.NewGuid()
                                           };

            var mockRiskAssessment = new Mock<GeneralRiskAssessment>();
            _riskAssessmentRepository
                .Setup(rr => rr.GetByIdAndCompanyId(attachPeopleAtRiskToRiskAssessmentRequest.RiskAssessmentId, attachPeopleAtRiskToRiskAssessmentRequest.CompanyId))
                .Returns(mockRiskAssessment.Object);

            var peopleAtRisks = new List<PeopleAtRisk>()
                              {
                                  new PeopleAtRisk(),
                                  new PeopleAtRisk(),
                                  new PeopleAtRisk()
                              };
            _peopleAtRiskRepository
                .Setup(x => x.GetByIds(attachPeopleAtRiskToRiskAssessmentRequest.PeopleAtRiskIds))
                .Returns(peopleAtRisks);

            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(attachPeopleAtRiskToRiskAssessmentRequest.UserId, attachPeopleAtRiskToRiskAssessmentRequest.CompanyId))
                .Returns(_user);

            //When
            riskAssessmentService.AttachPeopleAtRiskToRiskAssessment(attachPeopleAtRiskToRiskAssessmentRequest);

            //Then
            _riskAssessmentRepository.Verify(x => x.SaveOrUpdate(mockRiskAssessment.Object));
            _userRepository.Verify(x => x.GetByIdAndCompanyId(attachPeopleAtRiskToRiskAssessmentRequest.UserId, attachPeopleAtRiskToRiskAssessmentRequest.CompanyId));
            mockRiskAssessment.Verify(x => x.AttachPeopleAtRiskToRiskAssessment(peopleAtRisks, _user));
        }

        private GeneralRiskAssessmentAttachmentService CreateRiskAssessmentService()
        {
            var riskAssessmentService = new GeneralRiskAssessmentAttachmentService(_riskAssessmentRepository.Object, _userRepository.Object, _peopleAtRiskRepository.Object,  _log.Object);
            return riskAssessmentService;
        }
    }
}