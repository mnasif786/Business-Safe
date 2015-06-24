using System;
using BusinessSafe.Application.Implementations;
using BusinessSafe.Application.Implementations.HazardousSubstanceInventory;
using BusinessSafe.Application.Request.HazardousSubstanceInventory;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.HazardousSubstances
{
    [TestFixture]
    public class MarkForDeleteTests
    {
        private Mock<IHazardousSubstanceRiskAssessmentRepository> hazardousSubstanceRiskAssessmentRepository;
        private Mock<IHazardousSubstancesRepository> hazardousSubstanceRepository;
        private Mock<IUserForAuditingRepository> userRepository;
        private HazardousSubstancesService target;
        private Mock<IPeninsulaLog> log;
        
        [SetUp]
        public void Setup()
        {
            hazardousSubstanceRepository = new Mock<IHazardousSubstancesRepository>();
            userRepository = new Mock<IUserForAuditingRepository>();
            log = new Mock<IPeninsulaLog>();
            hazardousSubstanceRiskAssessmentRepository = new Mock<IHazardousSubstanceRiskAssessmentRepository>();

            target = new HazardousSubstancesService(
                hazardousSubstanceRepository.Object, 
                userRepository.Object, 
                null,
                null,
                null,
                null, 
                hazardousSubstanceRiskAssessmentRepository.Object, log.Object);
        }

        [Test]
        public void When_MarkForDelete_Then_should_call_correct_methods()
        {
            // Given
            var request = new MarkHazardousSubstanceAsDeleteRequest()
                              {
                                  CompanyId = 1,
                                  HazardousSubstanceId = 200,
                                  UserId = Guid.NewGuid()
                              };

            hazardousSubstanceRiskAssessmentRepository
                .Setup(x => x.DoesRiskAssessmentsExistForHazardousSubstance(request.HazardousSubstanceId, request.CompanyId))
                .Returns(false);


            var user = new UserForAuditing();
            userRepository
                .Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId))
                .Returns(user);

            var hazardousSubstance = new Mock<HazardousSubstance>();
            hazardousSubstanceRepository
                .Setup(x => x.GetByIdAndCompanyId(request.HazardousSubstanceId, request.CompanyId))
                .Returns(hazardousSubstance.Object);

            // When
            target.MarkForDelete(request);

            // Then
            hazardousSubstanceRiskAssessmentRepository.VerifyAll();
            hazardousSubstance.Verify(x => x.MarkForDelete(user));
            hazardousSubstanceRepository.Verify(x => x.SaveOrUpdate(hazardousSubstance.Object));
        }

        [Test]
        public void Given_hazardous_substance_used_in_risk_assessments_When_MarkForDelete_Then_should_throw_correct_exception()
        {
            // Given
            var request = new MarkHazardousSubstanceAsDeleteRequest()
            {
                CompanyId = 1,
                HazardousSubstanceId = 200,
                UserId = Guid.NewGuid()
            };

            hazardousSubstanceRiskAssessmentRepository
                .Setup(x => x.DoesRiskAssessmentsExistForHazardousSubstance(request.HazardousSubstanceId, request.CompanyId))
                .Returns(true);

            // When
            // Then
            Assert.Throws<TryingToDeleteHazardousSubstanceThatUsedByRiskAssessmentsException>(() => target.MarkForDelete(request));
            
        }
        
    }
}
