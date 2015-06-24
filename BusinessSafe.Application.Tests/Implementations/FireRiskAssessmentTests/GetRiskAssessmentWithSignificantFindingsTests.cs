using System.Collections.Generic;
using BusinessSafe.Application.Implementations.FireRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.FireRiskAssessmentTests
{
    [TestFixture]
    public class GetRiskAssessmentWithSignificantFindingsTests
    {
        private Mock<IFireRiskAssessmentRepository> _riskAssessmentRepo;
        private Mock<IPeninsulaLog> _log;
        private long _riskAssessmentId;
        private long _companyId;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentId = 123L;
            _companyId = 345L;

            _log = new Mock<IPeninsulaLog>();
            _log.Setup(x => x.Add(It.IsAny<object>()));

            _riskAssessmentRepo = new Mock<IFireRiskAssessmentRepository>();
            
        }

        [Test]
        public void When_GetRiskAssessmentWithSignificantFindings_called_Then_correct_methods_are_called()
        {
            // Given
            var target = GetTarget();

            var fireRiskAssessment = new Mock<FireRiskAssessment>();
            _riskAssessmentRepo
                .Setup(x => x.GetByIdAndCompanyId(_riskAssessmentId, _companyId))
                .Returns(fireRiskAssessment.Object);

            var fireCheckList = new Mock<FireRiskAssessmentChecklist>();
            var significantFindings = new List<SignificantFinding>()
                                                                {
                                                                     new SignificantFinding()
                                                                };
            fireCheckList
                .Setup(x => x.SignificantFindings)
                .Returns(significantFindings);

            fireRiskAssessment
                .Setup(x => x.Self)
                .Returns(new FireRiskAssessment());

            fireRiskAssessment
                .Setup(x => x.LatestFireRiskAssessmentChecklist)
                .Returns(fireCheckList.Object);

            fireRiskAssessment
                .Setup(x => x.Reviews)
                .Returns(new List<RiskAssessmentReview>());

            // When
            target.GetRiskAssessmentWithSignificantFindings(_riskAssessmentId, _companyId);

            // Then
            _riskAssessmentRepo.Verify(x => x.GetByIdAndCompanyId(_riskAssessmentId, _companyId));
        }
        
        private FireRiskAssessmentService GetTarget()
        {
            return new FireRiskAssessmentService(
                _riskAssessmentRepo.Object,
                null,
                null,
                null,
                null, _log.Object, null, null, null, null);
        }
    }
}