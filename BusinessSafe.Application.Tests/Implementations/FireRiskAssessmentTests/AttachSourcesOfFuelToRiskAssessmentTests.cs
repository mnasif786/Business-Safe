using System;
using System.Collections.Generic;
using BusinessSafe.Application.Implementations.FireRiskAssessments;
using BusinessSafe.Application.Implementations.MultiHazardRiskAssessment;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.FireRiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class AttachSourcesOfFuelToRiskAssessmentTests
    {
        private Mock<IFireRiskAssessmentRepository> _riskAssessmentRepository;
        private Mock<ISourceOfFuelRepository> _sourceOfFuelRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private UserForAuditing _user;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentRepository = new Mock<IFireRiskAssessmentRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _sourceOfFuelRepository = new Mock<ISourceOfFuelRepository>();
            _user = new UserForAuditing();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_valid_request_When_AttachPeopleAtRisk_is_called_Then_should_call_appropiate_methods()
        {
            //Given
            var riskAssessmentService = CreateRiskAssessmentService();
            var request = new AttachSourceOfFuelToRiskAssessmentRequest()
                              {
                                  SourceOfFuelIds = new List<long> { 1, 2, 3 },
                                  RiskAssessmentId = 2,
                                  CompanyId = 1,
                                  UserId = Guid.NewGuid()
                              };

            var mockRiskAssessment = new Mock<FireRiskAssessment>();
            _riskAssessmentRepository
                .Setup(rr => rr.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId))
                .Returns(mockRiskAssessment.Object);

            var sourceOfFuels = new List<SourceOfFuel>()
                                        {
                                            new SourceOfFuel(),
                                            new SourceOfFuel(),
                                            new SourceOfFuel()
                                        };
            _sourceOfFuelRepository
                .Setup(x => x.GetByIds(request.SourceOfFuelIds))
                .Returns(sourceOfFuels);

            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId))
                .Returns(_user);

            //When
            riskAssessmentService.AttachSourcesOfFuelToRiskAssessment(request);

            //Then
            _riskAssessmentRepository.Verify(x => x.SaveOrUpdate(mockRiskAssessment.Object));
            _userRepository.Verify(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId));
            mockRiskAssessment.Verify(x => x.AttachSourceOfFuelsToRiskAssessment(sourceOfFuels, _user));
        }

        private FireRiskAssessmentAttachmentService CreateRiskAssessmentService()
        {
            var riskAssessmentService = new FireRiskAssessmentAttachmentService(_riskAssessmentRepository.Object, null, _userRepository.Object, null,null, _sourceOfFuelRepository.Object, _log.Object);
            return riskAssessmentService;
        }
    }
}