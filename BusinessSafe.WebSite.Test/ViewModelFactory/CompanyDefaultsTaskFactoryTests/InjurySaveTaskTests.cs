using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Implementations.Company;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.WebSite.Areas.Company.Tasks;
using BusinessSafe.WebSite.Tests.Builder;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.CompanyDefaultsTaskFactoryTests
{
    [TestFixture]
    public class InjurySaveTaskTests
    {

        private Mock<ICompanyDefaultService> _companyDefaultService;
        private Mock<IDoesCompanyDefaultAlreadyExistGuard> _doesCompanyDefaultAlreadyExistGuard;
        private Guid _userId = Guid.NewGuid();

        [SetUp]
        public void Setup()
        {
            _companyDefaultService = new Mock<ICompanyDefaultService>();
            _doesCompanyDefaultAlreadyExistGuard = new Mock<IDoesCompanyDefaultAlreadyExistGuard>();
        }


        [Test]
        public void Given_that_hazardsavetask_When_executes_Then_should_call_SaveHazard_on_service()
        {
            //Given
            var injurySaveTask = CreateInjuryTask();

            var request = SaveCompanyDefaultViewModelBuilder.Create().Build();

            //When
            injurySaveTask.Execute(request, _userId);

            //Then
            _companyDefaultService.Verify(cds => cds.SaveInjury(It.IsAny<SaveInjuryRequest>()), Times.Once());
        }

        private InjurySaveTask CreateInjuryTask()
        {
            var hazardSaveTask = new InjurySaveTask(_companyDefaultService.Object, _doesCompanyDefaultAlreadyExistGuard.Object);
            return hazardSaveTask;
        }
    }
}
