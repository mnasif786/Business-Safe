using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.Implementations.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.Request.HazardousSubstanceRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using NUnit.Framework;
using Moq;

namespace BusinessSafe.Application.Tests.Implementations.HazardousSubstanceRiskAssessmentTests
{
    [TestFixture]
    public class SaveLastRecommendedControlSystemTests
    {
        private Mock<IHazardousSubstanceRiskAssessmentRepository> _hazardousSubstanceAssessmentRepository;
        private Mock<IControlSystemRepository> _controlSystemRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IPeninsulaLog> _log;
        private UserForAuditing _userForAuditing;

        [SetUp]
        public void Setup()
        {
            _hazardousSubstanceAssessmentRepository = new Mock<IHazardousSubstanceRiskAssessmentRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _controlSystemRepository = new Mock<IControlSystemRepository>();

            _userForAuditing = new UserForAuditing();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void When_SaveLastRecommendedControlSystem_calls_correct_methods()
        {
            // Given
            var riskAssessment = new Mock<HazardousSubstanceRiskAssessment>();
            var target = GetTarget();
            var request = new SaveLastRecommendedControlSystemRequest
                              {
                                  Id = 10L,
                                  CompanyId = 100L,
                                  ControlSystemId = 1000L,
                                  UserId = Guid.NewGuid()
                              };

            _hazardousSubstanceAssessmentRepository
                .Setup(x => x.GetByIdAndCompanyId(
                    request.Id,
                    request.CompanyId))
                .Returns(riskAssessment.Object);

            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(
                    request.UserId,
                    request.CompanyId))
                .Returns(_userForAuditing);

            var controlSystem = new ControlSystem();

            _controlSystemRepository
                .Setup(x => x.LoadById(request.ControlSystemId))
                .Returns(controlSystem);

            // When
            target.SaveLastRecommendedControlSystem(request);

            // Then
            _hazardousSubstanceAssessmentRepository.Verify(x => x.SaveOrUpdate(riskAssessment.Object));
            riskAssessment.Verify(x => x.SetLastRecommendedControlSystem(controlSystem, _userForAuditing));
            _controlSystemRepository.VerifyAll();
        }

        private IHazardousSubstanceRiskAssessmentService GetTarget()
        {
            return new HazardousSubstanceRiskAssessmentService(
                _hazardousSubstanceAssessmentRepository.Object,
                _userRepository.Object,
                null,
                _log.Object,
                null,
                _controlSystemRepository.Object,
                null,
                null
                );
        }
    }
}
