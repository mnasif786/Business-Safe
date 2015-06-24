using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Implementations.Company;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Response;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.WebSite.Areas.Company.Tasks;
using BusinessSafe.WebSite.Tests.Builder;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.CompanyDefaultsTaskFactoryTests
{
    [TestFixture]
    [Category("Unit")]
    public class HazardSaveTaskTests
    {
        private Mock<ICompanyDefaultService> _companyDefaultService;
        private Mock<IDoesCompanyDefaultAlreadyExistGuard> _doesCompanyDefaultAlreadyExistGuard;
        private Mock<IHazardRepository> _hazardRepository;
        private Guid _userId = Guid.NewGuid();

        [SetUp]
        public void Setup()
        {
            _companyDefaultService = new Mock<ICompanyDefaultService>();
            _doesCompanyDefaultAlreadyExistGuard = new Mock<IDoesCompanyDefaultAlreadyExistGuard>();
            _hazardRepository = new Mock<IHazardRepository>();
        }

        [Test]
        public void Given_that_peopleatrisksavetask_with_request_to_run_match_check_When_executes_Then_should_call_match_check_guard()
        {
            //Given
            var peopleAtRiskSaveTask = CreateHazardTask();
            var request = SaveCompanyDefaultViewModelBuilder
                                .Create()
                                .WithRunMatchCheck(true)
                                .Build();

            _doesCompanyDefaultAlreadyExistGuard
                                .Setup(x => x.Execute(It.IsAny<Func<IEnumerable<MatchingName>>>()))
                                .Returns(GuardDefaultExistsReponse.NoMatches);

            //When
            peopleAtRiskSaveTask.Execute(request, _userId);

            //Then
            _doesCompanyDefaultAlreadyExistGuard.Verify(x => x.Execute(It.IsAny<Func<IEnumerable<MatchingName>>>()), Times.Once());
        }

        [Test]
        public void Given_that_guard_finds_matches_When_executes_Then_should_return_correct_result()
        {
            //Given
            var peopleAtRiskSaveTask = CreateHazardTask();
            var request = SaveCompanyDefaultViewModelBuilder
                                .Create()
                                .WithRunMatchCheck(true)
                                .Build();

            var matches = new List<string>() { "Matching Name 1" };
            _doesCompanyDefaultAlreadyExistGuard
                                .Setup(x => x.Execute(It.IsAny<Func<IEnumerable<MatchingName>>>()))
                                .Returns(GuardDefaultExistsReponse.MatchesExist(matches));

            //When
            var result = peopleAtRiskSaveTask.Execute(request, _userId);

            //Then
            Assert.That(result.Success, Is.False);
            Assert.That(result.Matches.Count(), Is.EqualTo(matches.Count));
        }

        [Test]
        public void Given_that_hazardsavetask_When_executes_Then_should_call_SaveHazard_on_service()
        {
            //Given
            var hazardSaveTask = CreateHazardTask();

            var request = SaveCompanyDefaultViewModelBuilder.Create().Build();

            //When
            hazardSaveTask.Execute(request, _userId);

            //Then
            _companyDefaultService.Verify(cds => cds.SaveHazard(It.IsAny<SaveCompanyHazardDefaultRequest>()),Times.Once());
        }

      
        [Test]
        public void Given_that_hazardsavetask_When_executes_Then_should_return_correct_result()
        {
            //Given
            var hazardSaveTask = CreateHazardTask();

            const long expectedId = 2;
            _companyDefaultService.Setup(x => x.SaveHazard(It.IsAny<SaveCompanyHazardDefaultRequest>())).Returns(expectedId);

            var request = SaveCompanyDefaultViewModelBuilder.Create().Build();

            //When
            var result = hazardSaveTask.Execute(request, _userId);

            //Then
            Assert.That(result.Id, Is.EqualTo(2));
        }

        [Test]
        public void Given_that_there_are_no_RiskAssessmentTypeApplicable_in_viewmodel_Then_should_return_false()
        {
            //Given
            var hazardSaveTask = CreateHazardTask();
            var request = SaveCompanyDefaultViewModelBuilder
                                .Create()
                                .WithApplicableTypes(new int[] {})
                                .Build();

            _doesCompanyDefaultAlreadyExistGuard
                                .Setup(x => x.Execute(It.IsAny<Func<IEnumerable<MatchingName>>>()))
                                .Returns(GuardDefaultExistsReponse.NoMatches);

            //When
            var result = hazardSaveTask.Execute(request, _userId);

            //Then
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Contains.Substring("Applicable risk types required"));
        }

        private HazardSaveTask CreateHazardTask()
        {
            var hazardSaveTask = new HazardSaveTask(_companyDefaultService.Object, _hazardRepository.Object, _doesCompanyDefaultAlreadyExistGuard.Object);
            return hazardSaveTask;
        }
    }
}
