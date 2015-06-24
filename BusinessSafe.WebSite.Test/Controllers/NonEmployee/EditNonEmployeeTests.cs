using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.WebSite.Areas.Company.ViewModels;
using BusinessSafe.WebSite.Controllers;
using Moq;
using NUnit.Framework;
using StructureMap;

namespace BusinessSafe.WebSite.Tests.Controllers.NonEmployee
{
    [TestFixture]
    [Category("Unit")]
    public class EditNonEmployeeTests
    {
        private Mock<INonEmployeeService> nonEmployeeSerive;
        private const long companyId = 1;
        private const long nonEmployeeId = 2;

        [SetUp]
        public void SetUp()
        {
            nonEmployeeSerive = new Mock<INonEmployeeService>();
            ObjectFactory.Inject<IPeninsulaLog>(new StubPeninsulaLog());
        }

        [Test]
        public void Given_that_get_non_employee_for_edit_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateNonEmployeeController();

            var nonEmployee = new Mock< Domain.Entities.NonEmployee>();
            nonEmployee.SetupGet(x => x.Id).Returns( nonEmployeeId);
            var nonEmployeeDto = new NonEmployeeDtoMapper().Map(nonEmployee.Object);


            nonEmployeeSerive.Setup(x => x.GetNonEmployee(nonEmployeeId, companyId)).Returns(nonEmployeeDto);

            //When
            target.EditNonEmployee(nonEmployeeId, companyId);

            //Then
            nonEmployeeSerive.Verify(x => x.GetNonEmployee(nonEmployeeId, companyId));
        }

        [Test] public void Given_that_get_non_employee_for_edit_Then_should_return_the_correct_result()
        {
            //Given
            var target = CreateNonEmployeeController();

            var nonEmployee = new Mock<Domain.Entities.NonEmployee>();
            nonEmployee.SetupGet(x => x.Id).Returns(nonEmployeeId);
            var nonEmployeeDto = new NonEmployeeDtoMapper().Map(nonEmployee.Object);


            nonEmployeeSerive.Setup(x => x.GetNonEmployee(nonEmployeeId, companyId)).Returns(nonEmployeeDto);

            //When
            var result = target.EditNonEmployee(nonEmployeeId, companyId);

            //Then
            Assert.That(result.ViewName, Is.EqualTo("_AddNonEmployee"));
            Assert.That(result.Model, Is.TypeOf<NonEmployeeViewModel>());
        }

        private NonEmployeeDefaultsController CreateNonEmployeeController()
        {
            var target = new NonEmployeeDefaultsController(nonEmployeeSerive.Object, null);
            return target;
        }
    }
}