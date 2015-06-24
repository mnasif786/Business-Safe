using BusinessSafe.Application.Implementations.GeneralRiskAssessments;
using BusinessSafe.Application.Implementations.RiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.GeneralRiskAssessmentLookupServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetNonEmployeesTests
    {
        private Mock<INonEmployeeRepository> _nonEmployeeRepository;
        private Mock<IRiskAssessmentRepository> _riskAssessmentRepo;
        private Mock<IPeninsulaLog> _log;
        private string searchTerm = "testing";

        [SetUp]
        public void SetUp()
        {
            _nonEmployeeRepository = new Mock<INonEmployeeRepository>();
            _riskAssessmentRepo = new Mock<IRiskAssessmentRepository>();
            _log = new Mock<IPeninsulaLog>();
        }

        
        [Test]
        public void Given_that_get_non_employee_is_called_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateRiskAssessmentLookupService();

            var riskAssessment = new GeneralRiskAssessment();
            var companyId = 1;
            var riskAssessmentId = 2;

            _riskAssessmentRepo
                .Setup(x => x.GetByIdAndCompanyId(riskAssessmentId, companyId))
                .Returns(riskAssessment);

            var noneEmployees = new NonEmployee[]{ new NonEmployee(), };
            _nonEmployeeRepository.Setup(x => x.GetByTermSearch(searchTerm, companyId, 20)).Returns(noneEmployees);

            //When
            target.SearchForNonEmployeesNotAttachedToRiskAssessment(
                new NonEmployeesNotAttachedToRiskAssessmentSearchRequest()
                    {
                        SearchTerm = searchTerm,
                        CompanyId = companyId,
                        RiskAssessmentId = riskAssessmentId,
                        PageLimit = 20
                    }
                );
            
            //Then
            _riskAssessmentRepo.VerifyAll();
            _nonEmployeeRepository.VerifyAll();
        }
      
        private RiskAssessmentLookupService CreateRiskAssessmentLookupService()
        {
            var target = new RiskAssessmentLookupService(_riskAssessmentRepo.Object, _log.Object, null, _nonEmployeeRepository.Object);
            return target;
        }
    }
}