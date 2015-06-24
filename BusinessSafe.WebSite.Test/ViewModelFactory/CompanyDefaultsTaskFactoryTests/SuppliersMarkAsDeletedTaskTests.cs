using System;
using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Company.Tasks;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.CompanyDefaultsTaskFactoryTests
{
    [TestFixture]
    [Category("Unit")]
    public class SuppliersMarkAsDeletedTaskTests
    {
        private Mock<ISuppliersService> _suppliersService;

        [SetUp]
        public void Setup()
        {
            _suppliersService = new Mock<ISuppliersService>();
        }

        [Test]
        public void Given_that_suppliersmarkasdeltedtask_When_executes_Then_should_call_MarkAsDeleted_on_service()
        {
            //Given
            var personAtRiskMarkAsDeletedTask = CreatePersonAtRiskTask();

            //When
            personAtRiskMarkAsDeletedTask.Execute(1, 2, Guid.NewGuid());

            //Then
            _suppliersService.Verify(cds => cds.MarkSupplierAsDeleted(It.IsAny<MarkCompanyDefaultAsDeletedRequest>()), Times.Once());
        }

        private SuppliersMarkAsDeletedTask CreatePersonAtRiskTask()
        {
            var suppliersMarkAsDeletedTask = new SuppliersMarkAsDeletedTask(_suppliersService.Object);
            return suppliersMarkAsDeletedTask;
        }
    }
}