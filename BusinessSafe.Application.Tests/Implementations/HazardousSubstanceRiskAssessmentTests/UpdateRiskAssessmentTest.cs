using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.Implementations.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.Tests.Builder;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using Moq;
using NUnit.Framework;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Tests.Implementations.HazardousSubstanceRiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class UpdateRiskAssessmentTest
    {
        private Mock<IHazardousSubstanceRiskAssessmentRepository> _hazardousSubstanceAssessmentRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IPeninsulaLog> _log;
        private UserForAuditing _user;
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<IHazardousSubstancesRepository> _hazardousSubstanceRepository;
        
        [SetUp]
        public void Setup()
        {
            _hazardousSubstanceAssessmentRepository = new Mock<IHazardousSubstanceRiskAssessmentRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _user = new UserForAuditing();
            _log = new Mock<IPeninsulaLog>();
            _employeeRepository = new Mock<IEmployeeRepository>();
            _hazardousSubstanceRepository = new Mock<IHazardousSubstancesRepository>();
            
        }

        [Test]
        public void Given_valid_request_When_UpdateRiskAssessmentDescription_Then_should_call_Update_on_RiskAssessment()
        {
            //Given
            var riskAssessmentService = GetTarget();

            var request = new SaveHazardousSubstanceRiskAssessmentRequestBuilder()
                                .Build();

            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId))
                .Returns(_user);

            var mockRiskAssessment = new Mock<HazardousSubstanceRiskAssessment>();
            mockRiskAssessment.Setup(x => x.Update(
                It.IsAny<UserForAuditing>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<string>()
                ));

            _hazardousSubstanceAssessmentRepository
                .Setup(x => x.GetByIdAndCompanyId(request.Id, request.CompanyId)).
                Returns(() => mockRiskAssessment.Object);

            //When
            riskAssessmentService.UpdateRiskAssessmentDescription(request);

            //Then
            mockRiskAssessment.Verify(x => x.Update(
                _user,
                request.IsInhalationRouteOfEntry,
                request.IsIngestionRouteOfEntry,
                request.IsAbsorptionRouteOfEntry,
                request.WorkspaceExposureLimits
                ));
            
        }

        private IHazardousSubstanceRiskAssessmentService GetTarget()
        {
            return new HazardousSubstanceRiskAssessmentService(
                _hazardousSubstanceAssessmentRepository.Object,
                _userRepository.Object,
                _hazardousSubstanceRepository.Object,
                _log.Object,
                null,
                null,
                null,
                null
                );
        }
    }
}
