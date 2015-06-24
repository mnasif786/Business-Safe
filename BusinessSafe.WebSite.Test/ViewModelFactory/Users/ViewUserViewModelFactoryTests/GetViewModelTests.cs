using System;

using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.Users.Factories;
using BusinessSafe.WebSite.Areas.Users.ViewModels;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.Users.ViewUserViewModelFactoryTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetViewModelTests
    {
        private Mock<IEmployeeService> _employeeService;
        private long _companyId = 0;
        private Guid _employeeId = Guid.NewGuid();

        [SetUp]
        public void Setup()
        {
            _employeeService = new Mock<IEmployeeService>();
        }

        [Test]
        public void When_get_view_model_Then_return_correct_view_model()
        {
            // Given
            var target = CreateViewUserViewModelFactory();
            var employeeDto = new EmployeeDto()
                               {
                                   FullName = "Test Name",
                                   EmployeeReference = "Test Reference",
                                   JobTitle = "Test JobTitle",
                                   User = new UserDto()
                                              {
                                                  Role = new RoleDto()
                                                             {
                                                                 Name = "TestRole",
                                                                 Description = "Test Role"
                                                             },
                                                  SiteStructureElement = new SiteGroupDto()
                                                                             {
                                                                                 Name = "Test Site"
                                                                             }
                                              },
                               };

            _employeeService
                .Setup(x => x.GetEmployee(_employeeId, _companyId))
                .Returns(employeeDto);

            // When
            var result = target
                .WithCompanyId(_companyId)
                .WithEmployeeId(_employeeId)
                .GetViewModel();

            // Then
            Assert.That(result, Is.TypeOf<ViewUserViewModel>());
            Assert.That(result.Name, Is.EqualTo("Test Name"));
            Assert.That(result.EmployeeReference, Is.EqualTo("Test Reference"));
            Assert.That(result.JobTitle, Is.EqualTo("Test JobTitle"));
            Assert.That(result.Role, Is.EqualTo("Test Role"));
            Assert.That(result.PermissionLevel, Is.EqualTo("Site Group - Test Site"));
        }

        private ViewUserViewModelFactory CreateViewUserViewModelFactory()
        {
            return new ViewUserViewModelFactory(_employeeService.Object);
        }
    }
}