using System;
using System.Web.Mvc;

using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.WebSite.Areas.Employees.Controllers;
using BusinessSafe.WebSite.Areas.Employees.Factories;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Tests.Builder;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Employees.EmployeeControllerTests
{
    [TestFixture]
    [Category("Unit")]
    public class IndexEmployeeTests
    {
        private Mock<IEmployeeService> _employeeService;
        private Mock<IEmployeeViewModelFactory> _employeeViewModelFactory;
        private Mock<ICustomPrincipal> _currentUser;
        private Guid _employeeId = Guid.Empty;
        private int _companyId = 100;
        
        [SetUp]
        public void Setup()
        {
            var currentUser = new Mock<ICustomPrincipal>();

            currentUser
                .Setup(x => x.UserId /*Identity.Name*/)
                .Returns(new Guid());

            _employeeService = new Mock<IEmployeeService>();
            _employeeViewModelFactory = new Mock<IEmployeeViewModelFactory>();
            _employeeViewModelFactory
                .Setup(x => x.WithEmployeeId(It.IsAny<Guid?>()))
                .Returns(_employeeViewModelFactory.Object);

            _employeeViewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_employeeViewModelFactory.Object);

            _employeeViewModelFactory
                .Setup(x => x.WithCurrentUser(It.IsAny<ICustomPrincipal>()))
                .Returns(_employeeViewModelFactory.Object);

        }

        [Test]
        public void Given_get_When_index_employee_Then_should_return_correct_view()
        {
            // Given
            var controller = CreateEmployeeController();

            // When
            var result = controller.Index(_employeeId, _companyId) as ViewResult;

            // Then
            Assert.That(result.ViewName, Is.EqualTo(""));
        }

        [Test]
        public void Given_get_When_index_employee_Then_should_return_correct_viewmodel()
        {
            // Given
            var controller = CreateEmployeeController();

            var expectedViewModel = EmployeeViewModelBuilder
                .Create()
                .Build();

            _employeeViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(expectedViewModel);

            // When
            var result = controller.Index(_employeeId, _companyId) as ViewResult;

            // Then
            Assert.That(result.ViewData.Model, Is.EqualTo(expectedViewModel));
        }

        [Test]
        public void Given_get_When_index_employee_Then_should_call_correct_methods()
        {
            // Given
            var controller = CreateEmployeeController();

            var expectedViewModel = EmployeeViewModelBuilder
                .Create()
                .Build();
            
            _employeeViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(expectedViewModel);
            
            // When
            controller.Index(_employeeId, _companyId);

            // Then
            _employeeViewModelFactory.VerifyAll();
        }

        private EmployeeController CreateEmployeeController()
        {
            var employeeController = new EmployeeController(_employeeService.Object, _employeeViewModelFactory.Object, null, null);
            return TestControllerHelpers.AddUserToController(employeeController);       
        }
    }
}