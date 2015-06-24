using System;
using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Response;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.WebSite.Areas.Company.Tasks;
using BusinessSafe.WebSite.Controllers;
using Moq;
using NUnit.Framework;
using StructureMap;

namespace BusinessSafe.WebSite.Tests.Controllers.NonEmployee
{
    [TestFixture]
    [Category("Unit")]
    public class SaveNonEmployeeTests
    {
        private Mock<INonEmployeeService> _nonEmployeeSerive;
        private Mock<INonEmployeeSaveTask> _nonEmployeeSaveTask;
        private const long companyIdToLinkNonEmployeeTo = 1234;
        private const string name = "Bob";
        private const string position = "lying down";
        private const string nonEmployeeCompanyName = "Bob's Bits";

        [SetUp]
        public void SetUp()
        {
            _nonEmployeeSerive = new Mock<INonEmployeeService>();
            _nonEmployeeSaveTask = new Mock<INonEmployeeSaveTask>();
            ObjectFactory.Inject<IPeninsulaLog>(new StubPeninsulaLog());
        }

        [Test]
        public void Given_that_json_post_request_to_create_non_employee_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateNonEmployeeController();

            _nonEmployeeSaveTask
                .Setup(x => x.Execute(It.IsAny<SaveNonEmployeeRequest>()))
                .Returns(CompanyDefaultSaveResponse.CreateSavedSuccessfullyResponse(1));

            //When
            target.CreateNonEmployee(companyIdToLinkNonEmployeeTo, name, position, nonEmployeeCompanyName, true);

            //Then
            _nonEmployeeSaveTask.Verify(x => x.Execute(It.Is<SaveNonEmployeeRequest>(y => y.CompanyId == companyIdToLinkNonEmployeeTo && y.Name == name && y.NonEmployeeCompanyName == nonEmployeeCompanyName && y.Position == position)));
        }

        [Test]
        public void Given_that_json_post_request_to_create_non_employee_Then_should_return_correct_result()
        {
            //Given
            var target = CreateNonEmployeeController();

            _nonEmployeeSaveTask
                .Setup(x => x.Execute(It.IsAny<SaveNonEmployeeRequest>()))
                .Returns(CompanyDefaultSaveResponse.CreateSavedSuccessfullyResponse(1));

            //When
            var result = target.CreateNonEmployee(companyIdToLinkNonEmployeeTo, name, position, nonEmployeeCompanyName, true);

            //Then
            Assert.That(result.Data.ToString(), Is.StringContaining("{ Success = True, NonEmployeeId = 1 }"));
        }
        
        [Test]
        [ExpectedException()]
        public void Given_non_employee_service_encounters_exception_When_create_non_employee_Then_should_return_correct_result()
        {
            //Given
            var target = CreateNonEmployeeController();

            _nonEmployeeSaveTask
                .Setup(x => x.Execute(It.IsAny<SaveNonEmployeeRequest>()))
                .Throws<Exception>();

            //When
            var result = target.CreateNonEmployee(companyIdToLinkNonEmployeeTo, name, position, nonEmployeeCompanyName, true);

            //Then
            //Assert.That(result.Data.ToString(), Is.StringContaining("{ Success = False, Message = Unfortunately we encountered an error creating non employee"));
        }

        private NonEmployeeDefaultsController CreateNonEmployeeController()
        {
            var target = new NonEmployeeDefaultsController(_nonEmployeeSerive.Object, _nonEmployeeSaveTask.Object);
            return TestControllerHelpers.AddUserToController(target);
        }
    }
}