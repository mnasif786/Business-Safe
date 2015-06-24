using System;
using System.Web.Mvc;

using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Response;
using BusinessSafe.WebSite.Areas.Employees.Controllers;
using BusinessSafe.WebSite.Areas.Employees.Factories;
using BusinessSafe.WebSite.Areas.Employees.ViewModels;
using BusinessSafe.WebSite.PeninsulaOnline;
using BusinessSafe.WebSite.Tests.Builder;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Employees.EmployeeControllerTests
{
    [TestFixture]
    [Category("Unit")]
    public class CreateEmployeeTests
    {
        private Mock<IEmployeeService> _employeeService;
        private Mock<IEmployeeViewModelFactory> _employeeViewModelFactory;
        private Mock<INewRegistrationRequestService> _newRegistrationRequestService;

        [SetUp]
        public void Setup()
        {
            _employeeService = new Mock<IEmployeeService>();    
            _employeeViewModelFactory = new Mock<IEmployeeViewModelFactory>();
            _newRegistrationRequestService = new Mock<INewRegistrationRequestService>();

            _employeeViewModelFactory
                .Setup(x => x.WithEmployeeId(It.IsAny<Guid?>()))
                .Returns(_employeeViewModelFactory.Object);

            _employeeViewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_employeeViewModelFactory.Object);
        }



        [Test]
        public void Given_invalid_viewmodel_post_When_create_employee_Then_should_return_to_same_view_with_same_viewmodel()
        {
            // Given
            var controller = CreateEmployeeController();

            var request = EmployeeViewModelBuilder
                                    .Create()
                                    .Build();

            controller.ModelState.AddModelError("Forname", "Forename is required");

            _employeeViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(request);

            // When
            var result = controller.Create(request) as ViewResult;

            // Then
            Assert.That(result.ViewName, Is.EqualTo("Index"));
            Assert.That(result.ViewData.Model, Is.EqualTo(request));
        }

        [Test]
        public void Given_valid_viewmodel_post_When_create_employee_Then_should_call_the_correct_methods()
        {
            // Given
            var controller = CreateEmployeeController();
            var employeeViewModel = EmployeeViewModelBuilder
                                    .Create()
                                    .WithEmployeeReference("Tester")
                                    .WithTitle("Mr")
                                    .WithForename("Barry")
                                    .WithSurname("Scott")
                                    .WithNationalityId(1)
                                    .WithSex("Male")
                                    .Build();


            var response = new AddEmployeeResponse{Success = true};

            _employeeService
                    .Setup(x => x.Add(It.IsAny<AddEmployeeRequest>()))
                    .Returns(response);

            // When
            controller.Create(employeeViewModel);

            // Then
            _employeeService.Verify(x => x.Add(MatchesEmployeeViewModelProperties(employeeViewModel)));
            

        }

        private static AddEmployeeRequest MatchesEmployeeViewModelProperties(EmployeeViewModel employeeViewModel)
        {
            return It.Is<AddEmployeeRequest>(y =>
                                                 
                                                     y.EmployeeReference == employeeViewModel.EmployeeReference &&
                                                     y.Title == employeeViewModel.NameTitle &&
                                                     y.Forename == employeeViewModel.Forename &&
                                                     y.Surname == employeeViewModel.Surname &&
                                                     y.Sex == employeeViewModel.Sex &&
                                                     y.NationalityId == employeeViewModel.NationalityId

                                                 );
        }


        [Test]
        public void Given_valid_viewmodel_post_When_create_employee_Then_should_return_correct_result()
        {
            // Given
            var controller = CreateEmployeeController();
            var request = EmployeeViewModelBuilder
                                    .Create()
                                    .Build();

            var response = new AddEmployeeResponse()
                               {
                                   EmployeeId = Guid.NewGuid(), Success = true
                               };

            _employeeService.Setup(x => x.Add(It.IsAny<AddEmployeeRequest>())).Returns(response);

            // When
            var result = controller.Create(request) as RedirectToRouteResult;

            // Then
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(result.RouteValues["controller"], Is.EqualTo("Employee"));
            Assert.That(result.RouteValues["employeeId"], Is.EqualTo(response.EmployeeId));
        }

        private EmployeeController CreateEmployeeController()
        {
            var result =new EmployeeController(_employeeService.Object, _employeeViewModelFactory.Object, _newRegistrationRequestService.Object, null);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}
