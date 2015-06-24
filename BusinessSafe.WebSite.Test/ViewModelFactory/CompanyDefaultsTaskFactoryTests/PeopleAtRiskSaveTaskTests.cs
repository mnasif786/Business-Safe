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
    public class PeopleAtRiskSaveTaskTests
    {
        private Mock<IPeopleAtRiskRepository> _peopleAtRiskRepository;
        private Mock<ICompanyDefaultService> _companyDefaultService;
        private Mock<IDoesCompanyDefaultAlreadyExistGuard> _doesCompanyDefaultAlreadyExistGuard;
        private Guid _userId = Guid.NewGuid();

        [SetUp]
        public void Setup()
        {
            _companyDefaultService = new Mock<ICompanyDefaultService>();
            _doesCompanyDefaultAlreadyExistGuard = new Mock<IDoesCompanyDefaultAlreadyExistGuard>();
            _peopleAtRiskRepository = new Mock<IPeopleAtRiskRepository>();
        }

        [Test]
        public void Given_that_peopleatrisksavetask_with_request_to_run_match_check_When_executes_Then_should_call_match_check_guard()
        {
            //Given
            var peopleAtRiskSaveTask = CreatePeopleAtRiskSaveTask();
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
            var peopleAtRiskSaveTask = CreatePeopleAtRiskSaveTask();
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
        public void Given_that_peopleatrisksavetask_When_executes_Then_should_call_SavePeopleAtRisk_on_service()
        {
            //Given
            var peopleAtRiskSaveTask = CreatePeopleAtRiskSaveTask();
            var request = SaveCompanyDefaultViewModelBuilder.Create().Build();

            //When
            peopleAtRiskSaveTask.Execute(request, _userId);

            //Then
            _companyDefaultService.Verify(cds => cds.SavePeopleAtRisk(It.IsAny<SaveCompanyDefaultRequest>()), Times.Once());
        }

        [Test]
        public void Given_that_peopleatrisksavetask_When_executes_Then_should_return_correct_result()
        {
            //Given
            var peopleAtRiskSaveTask = CreatePeopleAtRiskSaveTask();

            const long expectedId = 5;
            _companyDefaultService.Setup(x => x.SavePeopleAtRisk(It.IsAny<SaveCompanyDefaultRequest>())).Returns(expectedId);

            //When
            var request = SaveCompanyDefaultViewModelBuilder.Create().Build();
            var result = peopleAtRiskSaveTask.Execute(request, _userId);

            //Then
            Assert.That(result.Id, Is.EqualTo(5));
        }

        private PeopleAtRiskSaveTask CreatePeopleAtRiskSaveTask()
        {
            var peopleAtRiskSaveTask = new PeopleAtRiskSaveTask(_companyDefaultService.Object,
                                                                _peopleAtRiskRepository.Object,
                                                                _doesCompanyDefaultAlreadyExistGuard.Object);
            return peopleAtRiskSaveTask;
        }
    }
}