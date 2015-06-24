using BusinessSafe.Application.Implementations.Checklists;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.Checklists.EmployeeChecklistServiceTests
{
    [TestFixture]
    public class GetByPersonalRiskAssessmentIdTests : BaseEmployeeServiceTests
    {
        private Mock<IEmployeeChecklistRepository> _employeeChecklistRepository;
        private Mock<IPeninsulaLog> _log;
        private const long riskAssessmentId = 200;

        [SetUp]
        public void Setup()
        {
            _employeeChecklistRepository = new Mock<IEmployeeChecklistRepository>();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void When_GetByPersonalRiskAssessmentIdAndCompanyId_Then_should_call_correct_methods()
        {
            // Given
            var target = GetTarget();

            var employeeChecklists = new[]{new EmployeeChecklist() };
            _employeeChecklistRepository
                .Setup(x => x.GetByPersonalRiskAssessmentId(riskAssessmentId))
                .Returns(employeeChecklists);

            // When
            target.GetByPersonalRiskAssessmentId(riskAssessmentId);

            // Then
            _employeeChecklistRepository.VerifyAll();    
        }

        protected EmployeeChecklistService GetTarget()
        {
            return new EmployeeChecklistService(null, _employeeChecklistRepository.Object, null, _log.Object,null);
        }
    }
}
