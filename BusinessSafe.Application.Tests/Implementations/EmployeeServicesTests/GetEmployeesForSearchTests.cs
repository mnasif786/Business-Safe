using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Implementations.Employees;
using BusinessSafe.Application.Tests.Builder;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.EmployeeServicesTests
{
    [TestFixture]
    public class GetEmployeesForSearchTests
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
        public void Given_valid_search_request_When_get_employees_Then_should_call_correct_methods()
        {

            // Given
            var request = EmployeeSearchRequestBuilder.Create().Build();


            var target = CreateEmployeeSearchService();

            // When
            target.Search(request);

            // Then
            _employeeRepository.Verify(x => x.Search(
                It.IsAny<long>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<long[]>(),
                It.IsAny<bool>(),
                It.IsAny<int>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<string>(),
                It.IsAny<bool>()));
        }

        // Should return the correct results
        [Test]
        public void Given_valid_search_request_When_get_employees_Then_should_return_correct_results()
        {

            // Given
            var request = EmployeeSearchRequestBuilder
                .Create()
                .WithCompanyId(1001)
                .WithEmployeeReference("111")
                .WithForname("Bob")
                .WithSurname("Hon")
                .WithSiteId(5050)
                .Build();


            var target = CreateEmployeeSearchService();

            var firstEmployee = new Employee()
                                    {
                                        EmployeeReference = "Test"
                                    };
            var employeesList = new List<Employee>()
                                    {
                                        firstEmployee, 
                                        new Employee(), 
                                        new Employee()
                                    };

            _employeeRepository
                .Setup(x => x.Search(
                    It.IsAny<long>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<long[]>(),
                    It.IsAny<bool>(),
                    It.IsAny<int>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>()))
                .Returns(employeesList);

            // When
            var result = target.Search(request);

            // Then
            Assert.That(result.Count, Is.EqualTo(employeesList.Count));
            Assert.That(result.First().EmployeeReference, Is.EqualTo(firstEmployee.EmployeeReference));
        }

        private EmployeeService CreateEmployeeSearchService()
        {
            return new EmployeeService(_employeeRepository.Object, null, null, null, null, null, _log.Object, null, null, null);
        }
    }
}