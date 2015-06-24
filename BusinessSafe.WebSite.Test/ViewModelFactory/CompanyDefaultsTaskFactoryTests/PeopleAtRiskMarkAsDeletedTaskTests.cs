using System;
using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Company.Tasks;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.CompanyDefaultsTaskFactoryTests
{
    [TestFixture]
    [Category("Unit")]
    public class PeopleAtRiskMarkAsDeletedTaskTests
    {
        private Mock<ICompanyDefaultService> _companyDefaultService;

        [SetUp]
        public void Setup()
        {
            _companyDefaultService = new Mock<ICompanyDefaultService>();
        }

        [Test]
        public void Given_that_personatriskmarkasdeltedtask_When_executes_Then_should_call_MarkAsDeleted_on_service()
        {
            //Given
            var personAtRiskMarkAsDeletedTask = CreatePersonAtRiskTask();

            //When
            personAtRiskMarkAsDeletedTask.Execute(1,2, Guid.NewGuid());

            //Then
            _companyDefaultService.Verify(cds => cds.MarkPersonAtRiskAsDeleted(It.IsAny<MarkCompanyDefaultAsDeletedRequest>()), Times.Once());
        }

        private PersonAtRiskMarkAsDeletedTask CreatePersonAtRiskTask()
        {
            var personAtRiskMarkAsDeletedTask = new PersonAtRiskMarkAsDeletedTask(_companyDefaultService.Object);
            return personAtRiskMarkAsDeletedTask;
        }
    }
}