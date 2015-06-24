using System;
using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.WebSite.Controllers;
using Moq;
using NUnit.Framework;
using StructureMap;

namespace BusinessSafe.WebSite.Tests.Controllers.NonEmployee
{
    [TestFixture]
    [Category("Unit")]
    public class MarkNonEmployeeAsDeletedTests
    {
        private Mock<INonEmployeeService> nonEmployeeSerive;
        private long nonEmployeeId = 200;
        private long companyId = 100;

        [SetUp]
        public void SetUp()
        {
            nonEmployeeSerive = new Mock<INonEmployeeService>();
            
            ObjectFactory.Inject<IPeninsulaLog>(new StubPeninsulaLog());
        }

        [Test]
        public void Given_that_json_post_request_to_mark_non_employee_as_deleted_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateNonEmployeeController();

            //When
            target.MarkNonEmployeeAsDeleted(nonEmployeeId, companyId);

            //Then
            nonEmployeeSerive.Verify(x => x.MarkNonEmployeeAsDeleted(It.Is<MarkNonEmployeeAsDeletedRequest>(y => y.NonEmployeeId == nonEmployeeId)));
        }

        [Test]
        public void Given_that_json_post_request_to_mark_non_employee_as_deleted_Then_should_return_correct_result()
        {
            //Given
            var target = CreateNonEmployeeController();

            //When
            var result = target.MarkNonEmployeeAsDeleted(nonEmployeeId, companyId);

            //Then
            Assert.That(result.Data.ToString(), Is.StringContaining("{ Success = True, Id = 200 }"));
        }
        
        private NonEmployeeDefaultsController CreateNonEmployeeController()
        {
            var target = new NonEmployeeDefaultsController(nonEmployeeSerive.Object, null);
            return TestControllerHelpers.AddUserToController(target);
        }
    }
}