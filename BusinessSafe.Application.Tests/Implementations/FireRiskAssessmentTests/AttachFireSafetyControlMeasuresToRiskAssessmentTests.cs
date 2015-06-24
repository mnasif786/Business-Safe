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
    public class AttachFireSafetyControlMeasuresToRiskAssessmentTests
    {
        private Mock<IFireRiskAssessmentRepository> _riskAssessmentRepository;
        private Mock<IFireSafetyControlMeasureRepository> _fireSafetyControlMeasureRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private UserForAuditing _user;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentRepository = new Mock<IFireRiskAssessmentRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _fireSafetyControlMeasureRepository = new Mock<IFireSafetyControlMeasureRepository>();
            _user = new UserForAuditing();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_valid_request_When_AttachPeopleAtRisk_is_called_Then_should_call_appropiate_methods()
        {
            //Given
            var riskAssessmentService = CreateRiskAssessmentService();
            var attachPeopleAtRiskToRiskAssessmentRequest = new AttachFireSafetyControlMeasuresToRiskAssessmentRequest()
                                                                {
                                                                    FireSafetyControlMeasureIds = new List<long> { 1, 2, 3 },
                                                                    RiskAssessmentId = 2,
                                                                    CompanyId = 1,
                                                                    UserId = Guid.NewGuid()
                                                                };

            var mockRiskAssessment = new Mock<FireRiskAssessment>();
            _riskAssessmentRepository
                .Setup(rr => rr.GetByIdAndCompanyId(attachPeopleAtRiskToRiskAssessmentRequest.RiskAssessmentId, attachPeopleAtRiskToRiskAssessmentRequest.CompanyId))
                .Returns(mockRiskAssessment.Object);

            var fireSafetyControlMeasures = new List<FireSafetyControlMeasure>()
                                    {
                                        new FireSafetyControlMeasure(),
                                        new FireSafetyControlMeasure(),
                                        new FireSafetyControlMeasure()
                                    };
            _fireSafetyControlMeasureRepository
                .Setup(x => x.GetByIds(attachPeopleAtRiskToRiskAssessmentRequest.FireSafetyControlMeasureIds))
                .Returns(fireSafetyControlMeasures);

            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(attachPeopleAtRiskToRiskAssessmentRequest.UserId, attachPeopleAtRiskToRiskAssessmentRequest.CompanyId))
                .Returns(_user);

            //When
            riskAssessmentService.AttachFireSafetyControlMeasuresToRiskAssessment(attachPeopleAtRiskToRiskAssessmentRequest);

            //Then
            _riskAssessmentRepository.Verify(x => x.SaveOrUpdate(mockRiskAssessment.Object));
            _userRepository.Verify(x => x.GetByIdAndCompanyId(attachPeopleAtRiskToRiskAssessmentRequest.UserId, attachPeopleAtRiskToRiskAssessmentRequest.CompanyId));
            mockRiskAssessment.Verify(x => x.AttachFireSafetyControlMeasuresToRiskAssessment(fireSafetyControlMeasures, _user));
        }

        private FireRiskAssessmentAttachmentService CreateRiskAssessmentService()
        {
            var riskAssessmentService = new FireRiskAssessmentAttachmentService(_riskAssessmentRepository.Object, null, _userRepository.Object, _fireSafetyControlMeasureRepository.Object, null, null, _log.Object);
            return riskAssessmentService;
        }
    }
}