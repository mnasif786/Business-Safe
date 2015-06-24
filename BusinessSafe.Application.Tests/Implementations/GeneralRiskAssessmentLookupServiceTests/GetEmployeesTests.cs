using System.Collections.Generic;
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
    public class GetEmployeesTests
    {
        private Mock<IRiskAssessmentRepository> _riskAssessmentRepo;
        private Mock<IPeninsulaLog> _log;
        private Mock<IEmployeeRepository> _employeeRepository;
        private string searchTerm;

        [SetUp]
        public void SetUp()
        {
            _riskAssessmentRepo = new Mock<IRiskAssessmentRepository>();
            _employeeRepository = new Mock<IEmployeeRepository>();
            _log = new Mock<IPeninsulaLog>();
            searchTerm = string.Empty;
        }

       
        [Test]
        public void Given_that_get_employee_is_called_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateRiskAssessmentLookupService();

            var riskAssessment = new GeneralRiskAssessment();
            _riskAssessmentRepo
                .Setup(x => x.GetByIdAndCompanyId(1, 10))
                .Returns(riskAssessment);

            var employees = new Employee[]
                                {
                                    new Employee(),
                                };
            _employeeRepository
                .Setup(x => x.GetByTermSearch(searchTerm, 10, 10)).Returns(employees);


            //When
            target.SearchForEmployeesNotAttachedToRiskAssessment(new EmployeesNotAttachedToRiskAssessmentSearchRequest(){
                SearchTerm = searchTerm,
                CompanyId = 10,
                RiskAssessmentId = 1,
                PageLimit = 10});

            //Then
            _riskAssessmentRepo.VerifyAll();
            _employeeRepository.VerifyAll();
        }
       
        private RiskAssessmentLookupService CreateRiskAssessmentLookupService()
        {
            var target = new RiskAssessmentLookupService(_riskAssessmentRepo.Object,_log.Object, _employeeRepository.Object, null);
            return target;
        }
    }
}