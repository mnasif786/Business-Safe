using BusinessSafe.Application.Implementations.GeneralRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.RiskAssessmentServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetRiskAssessmentTests
    {
        private Mock<IGeneralRiskAssessmentRepository> _riskAssessmentRepository;
        private Mock<IPeninsulaLog> _log;
        const long companyId = 500;
        const long riskAssessmentId = 600;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentRepository = new Mock<IGeneralRiskAssessmentRepository>();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_Task_Service_is_initialized_with_spy_repository_When_GetRiskAssessment_is_called_Then_get_by_Id_is_called()
        {
            //Given
            var riskAssessmentService = CreateRiskAssessmentService();



            _riskAssessmentRepository
                .Setup(rr => rr.GetByIdAndCompanyId(riskAssessmentId, companyId))
                .Returns(new GeneralRiskAssessment()
                {
                    CreatedBy = new UserForAuditing{ Employee = new EmployeeForAuditing()}
                                                                                                                                      
                });

            //When
            riskAssessmentService.GetRiskAssessment(riskAssessmentId, companyId);

            //Then
            _riskAssessmentRepository.VerifyAll();
        }
        
        private GeneralRiskAssessmentService CreateRiskAssessmentService()
        {
            var riskAssessmentService = new GeneralRiskAssessmentService(_riskAssessmentRepository.Object, null, null, null, _log.Object, null, null);
            return riskAssessmentService;
        }
    }
}
