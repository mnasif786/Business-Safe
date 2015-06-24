using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.Employees.Controllers;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Employees.EmployeesControllerTests
{
    [TestFixture]
    public class IsEmployeeAbleToCompleteReviewTaskTests
    {
        private EmployeesController _target;
        private Mock<IEmployeeService> _employeeService;
        private Guid _employeeId;
        private long _companyId;

        [SetUp]
        public void Setup()
        {
            _employeeId = Guid.NewGuid();
            _companyId = 1234L;

            _employeeService = new Mock<IEmployeeService>();
            _employeeService
                .Setup(x => x.GetEmployee(_employeeId, _companyId))
                .Returns(new EmployeeDto()
                {
                    User = new UserDto()
                    {
                        Id = Guid.NewGuid(),
                        Role = new RoleDto() { Name = "GeneralUser" }
                    }
                });
        }

        [Test]
        public void Given_When_IsEmployeeAbleToCompleteReviewTask_Then_returns_json()
        {
            // Given
            _target = GetTarget();

            // When
            var result = _target.IsEmployeeAbleToCompleteReviewTask(_employeeId, _companyId);

            // Then
            Assert.That(result, Is.InstanceOf<JsonResult>());
            Assert.That(result.JsonRequestBehavior, Is.EqualTo(JsonRequestBehavior.AllowGet));
        }

        [Test]
        public void Given_Employee_is_GeneralUser_When_IsEmployeeAbleToCompleteReviewTask_Then_CanCompleteReviewTask_is_false()
        {
            // Given
            _target = GetTarget();

            // When
            dynamic result = _target.IsEmployeeAbleToCompleteReviewTask(_employeeId, _companyId);

            // Then
            Assert.IsFalse(result.Data.CanCompleteReviewTask);
        }

        [Test]
        public void Given_Employee_is_not_GeneralUser_When_IsEmployeeAbleToCompleteReviewTask_Then__CanCompleteReviewTask_is_true()
        {
            // Given
            _employeeService = new Mock<IEmployeeService>();
            _employeeService
                .Setup(x => x.GetEmployee(_employeeId, _companyId))
                .Returns(new EmployeeDto()
                {
                    User = new UserDto()
                    {
                        Id = Guid.NewGuid(),
                        Role = new RoleDto() { Name = "LordOfAllThings" }
                    }
                });
            _target = GetTarget();

            // When
            dynamic result = _target.IsEmployeeAbleToCompleteReviewTask(_employeeId, _companyId);

            // Then
            Assert.IsTrue(result.Data.CanCompleteReviewTask);
        }

        [Test]
        public void Given_Employee_is_not_a_User_When_IsEmployeeAbleToCompleteReviewTask_Then_CanCompleteReviewTask_is_false()
        {
            // Given
            _employeeService = new Mock<IEmployeeService>();
            _employeeService
                .Setup(x => x.GetEmployee(_employeeId, _companyId))
                .Returns(new EmployeeDto());
            _target = GetTarget();

            // When
            dynamic result = _target.IsEmployeeAbleToCompleteReviewTask(_employeeId, _companyId);

            // Then
            Assert.IsFalse(result.Data.CanCompleteReviewTask);
        }

        [Test]
        public void Given_Employee_is_not_a_User_When_IsEmployeeAbleToCompleteReviewTask_Then_IsUser_is_false()
        {
            // Given
            _employeeService = new Mock<IEmployeeService>();
            _employeeService
                .Setup(x => x.GetEmployee(_employeeId, _companyId))
                .Returns(new EmployeeDto());
            _target = GetTarget();

            // When
            dynamic result = _target.IsEmployeeAbleToCompleteReviewTask(_employeeId, _companyId);

            // Then
            Assert.IsFalse(result.Data.IsUser);
        }

        [Test]
        public void Given_Employee_is_a_User_When_IsEmployeeAbleToCompleteReviewTask_Then_IsUser_is_true()
        {
            // Given
            _target = GetTarget();

            // When
            dynamic result = _target.IsEmployeeAbleToCompleteReviewTask(_employeeId, _companyId);

            // Then
            Assert.IsTrue(result.Data.IsUser);
        }

        [Test]
        public void Given_EmployeeId_empty_Guid_When_IsEmployeeAbleToCompleteReviewTask_Then_IsUser_is_false_and_CanCompleteReviewTask_is_false()
        {
            // Given
            _target = GetTarget();

            // When
            dynamic result = _target.IsEmployeeAbleToCompleteReviewTask(Guid.Empty, _companyId);
            
            // Then
            Assert.IsFalse(result.Data.IsUser);
            Assert.IsFalse(result.Data.CanCompleteReviewTask);
        }

        private EmployeesController GetTarget()
        {
            return new EmployeesController(_employeeService.Object);
        }
    }
}
