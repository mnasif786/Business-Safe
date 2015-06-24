using System.Linq;
using BusinessSafe.Application.Implementations.Employees;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.EmployeeServicesTests
{
    [TestFixture]
    public class GetEmployeesTests
    {
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void SetUp()
        {
            _employeeRepository = new Mock<IEmployeeRepository>();
            _log = new Mock<IPeninsulaLog>();

        }

        [Test]
        public void Given_valid_request_When_mark_employee_as_deleted_Then_should_call_correct_methods()
        {

            // Given
            var request = new SearchEmployeesRequest()
                              {
                                  CompanyId = 1
                              };


            var target = CreateEmployeeService();

            var employees = new Employee[] { };
            _employeeRepository
                .Setup(x => x.Search(
                    request.CompanyId, 
                    request.EmployeeReferenceLike, 
                    request.ForenameLike, 
                    request.SurnameLike, 
                    request.SiteIds, 
                    request.ShowDeleted, 
                    request.MaximumResults,
                    request.IncludeSiteless, 
                    request.ExcludeWithActiveUser,
                    null,
                    true))
                .Returns(employees);

            // When
            target.Search(request);

            // Then
            _employeeRepository.VerifyAll();

        }

        [Test]
        public void Given_valid_request_When_mark_employee_as_deleted_Then_should_return_correct_result()
        {

            // Given
            var request = new SearchEmployeesRequest()
                              {
                                  CompanyId = 1
                              };

            var target = CreateEmployeeService();

            var employees = new Employee[]
                                {
                                    new Employee()
                                        {
                                            EmployeeReference = "1"
                                        }, 
                                    new Employee()
                                        {
                                            EmployeeReference = "2"
                                        }, 
                                    new Employee()
                                        {
                                            EmployeeReference = "3"
                                        },
                                };
            _employeeRepository
                .Setup(x => x.Search(
                    request.CompanyId, 
                    request.EmployeeReferenceLike, 
                    request.ForenameLike, 
                    request.SurnameLike, 
                    request.SiteIds, 
                    request.ShowDeleted, 
                    request.MaximumResults,
                    request.IncludeSiteless, 
                    request.ExcludeWithActiveUser,
                    null,
                    true))
                .Returns(employees);

            // When
            var result = target.Search(request);

            // Then
            Assert.That(result.Count, Is.EqualTo(employees.Length));
            Assert.That(result.Count(x => x.EmployeeReference == "1"), Is.EqualTo(1));
            Assert.That(result.Count(x => x.EmployeeReference == "2"), Is.EqualTo(1));
            Assert.That(result.Count(x => x.EmployeeReference == "3"), Is.EqualTo(1));
        }

        private EmployeeService CreateEmployeeService()
        {
            return new EmployeeService(_employeeRepository.Object, null, null, null, null, null, _log.Object, null, null, null);
        }
    }
}