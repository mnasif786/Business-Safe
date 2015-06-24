using System;
using System.Web.Mvc;

using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Employees.Controllers;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Employees.EmployeeControllerTests
{
    [TestFixture]
    [Category("Unit")]
    public class MarkEmployeeAsDeletedTests
    {
        private long _companyId;
        private Mock<IEmployeeService> _employeeService;
        private Guid _employeeId;


        [SetUp]
        public void SetUp()
        {
            _companyId = 1;
            _employeeId = Guid.NewGuid();
            _employeeService = new Mock<IEmployeeService>();
        }

        [Test]
        public void Given_invalid_request_companyId_not_set_When_Delete_Then_should_throw_correct_exception()
        {
            //Given
            var controller = CreateAddedDocumentsLibraryController();

            _companyId = 0;

            //Get
            //Then
            Assert.Throws<ArgumentException>(() => controller.MarkEmployeeAsDeleted(_companyId, _employeeId.ToString()));

        }

        [Test]
        public void Given_invalid_request_documentId_not_set_When_Delete_Then_should_throw_correct_exception()
        {
            //Given
            var controller = CreateAddedDocumentsLibraryController();

            //Get
            //Then
            Assert.Throws<ArgumentException>(() => controller.MarkEmployeeAsDeleted(_companyId, string.Empty));

        }

        [Test]
        public void Given_valid_request_When_Delete_Then_should_return_correct_result()
        {
            //Given
            var controller = CreateAddedDocumentsLibraryController();

            //Get
            var result = controller.MarkEmployeeAsDeleted(_companyId, _employeeId.ToString()) as JsonResult;

            //Then
            Assert.That(result.Data.ToString(), Contains.Substring("Success = True"));
        }


        [Test]
        public void Given_valid_request_When_Delete_Then_should_call_correct_methods()
        {
            // Given
            var controller = CreateAddedDocumentsLibraryController();

            // When
            controller.MarkEmployeeAsDeleted(_companyId, _employeeId.ToString());

            // Then
            _employeeService.Verify(x => x.MarkEmployeeAsDeleted(It.Is<MarkEmployeeAsDeletedRequest>(r => r.CompanyId == _companyId &&
                                                                                                          r.EmployeeId == _employeeId &&
                                                                                                          r.UserId == controller.CurrentUser.UserId)));
        }


        private EmployeeController CreateAddedDocumentsLibraryController()
        {
            var result = new EmployeeController(_employeeService.Object, null, null, null);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}