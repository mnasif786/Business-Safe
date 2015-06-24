using System;
using System.Web.Mvc;

using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.WebSite.Areas.Employees.Controllers;
using BusinessSafe.WebSite.Areas.Employees.ViewModels;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Employees.EmployeeControllerTests
{
    [TestFixture]
    [Category("Unit")]
    public class CanDeleteEmployeeTests
    {
        private Mock<ITaskService> _taskService;
        private Guid _employeeId;
        private int _companyId;

        [SetUp]
        public void Setup()
        {
            _taskService = new Mock<ITaskService>();
            _employeeId = Guid.NewGuid();
            _companyId = 100;
        }

        [Test]
        public void When_CanDeleteEmployee_Then_should_call_correct_methods()
        {
            // Given
            var controller = CreateEmployeeController();

            // When
            controller.CanDeleteEmployee(new CanDeleteEmployeeViewModel()
                                             {
                                                 CompanyId = _companyId,
                                                 EmployeeId = _employeeId
                                             }); 

            // Then
            _taskService.Verify(x => x.HasEmployeeGotOutstandingTasks(_employeeId, _companyId));
        }

        [Test]
        public void Given_invalid_request_companyId_not_set_When_CanDeleteEmployee_Then_should_throw_correct_exception()
        {
            // Given
            var controller = CreateEmployeeController();

            _companyId = 0;

            // When
            // Then
            Assert.Throws<ArgumentException>(() => controller.CanDeleteEmployee(new CanDeleteEmployeeViewModel()
            {
                CompanyId = _companyId,
                EmployeeId = _employeeId
            }));

        }

        [Test]
        public void Given_invalid_request_employeeId_not_set_When_CanDeleteEmployee_Then_should_throw_correct_exception()
        {
            // Given
            var controller = CreateEmployeeController();

            _employeeId = Guid.Empty;

            // When
            // Then
            Assert.Throws<ArgumentException>(() => controller.CanDeleteEmployee(new CanDeleteEmployeeViewModel()
            {
                CompanyId = _companyId,
                EmployeeId = _employeeId
            }));

        }

        [Test]
        public void Given_can_delete_employee_When_CanDeleteEmployee_Then_should_return_correct_result()
        {
            // Given
            var controller = CreateEmployeeController();

            _taskService
                .Setup(x => x.HasEmployeeGotOutstandingTasks(_employeeId, _companyId))
                .Returns(false);

            // When
            var result = controller.CanDeleteEmployee(new CanDeleteEmployeeViewModel()
            {
                CompanyId = _companyId,
                EmployeeId = _employeeId
            }) as JsonResult;

            // Then
            Assert.That(result.Data.ToString(), Contains.Substring("CanDeleteEmployee = True"));
        }

        [Test]
        public void Given_cant_delete_employee_When_CanDeleteEmployee_Then_should_return_correct_result()
        {
            // Given
            var controller = CreateEmployeeController();

            _taskService
                .Setup(x => x.HasEmployeeGotOutstandingTasks(_employeeId, _companyId))
                .Returns(true);

            // When
            var result = controller.CanDeleteEmployee(new CanDeleteEmployeeViewModel()
            {
                CompanyId = _companyId,
                EmployeeId = _employeeId
            }) as JsonResult;

            // Then
            Assert.That(result.Data.ToString(), Contains.Substring("CanDeleteEmployee = False"));
        }

        private EmployeeSearchController CreateEmployeeController()
        {
            return new EmployeeSearchController(null, _taskService.Object);
        }
    }
}