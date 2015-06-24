using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
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
    public class SupplierSaveTaskTests
    {
        private Mock<ISupplierRepository> _suppliersRepository;
        private Mock<ISuppliersService> _suppliersService;
        private Mock<IDoesCompanyDefaultAlreadyExistGuard> _doesCompanyDefaultAlreadyExistGuard;
        private Guid _userId = Guid.NewGuid();

        [SetUp]
        public void Setup()
        {
            _suppliersService = new Mock<ISuppliersService>();
            _doesCompanyDefaultAlreadyExistGuard = new Mock<IDoesCompanyDefaultAlreadyExistGuard>();
            _suppliersRepository = new Mock<ISupplierRepository>();
        }

        [Test]
        public void Given_that_suppliersavetask_with_request_to_run_match_check_When_executes_Then_should_call_match_check_guard()
        {
            //Given
            var suppliersSaveTask = CreateSuppliersSaveTask();
            
            var request = SaveCompanyDefaultViewModelBuilder
                .Create()
                .WithRunMatchCheck(true)
                .Build();

            _doesCompanyDefaultAlreadyExistGuard
                .Setup(x => x.Execute(It.IsAny<Func<IEnumerable<MatchingName>>>()))
                .Returns(GuardDefaultExistsReponse.NoMatches);

            //When
            suppliersSaveTask.Execute(request, _userId);

            //Then
            _doesCompanyDefaultAlreadyExistGuard.Verify(x => x.Execute(It.IsAny<Func<IEnumerable<MatchingName>>>()), Times.Once());
        }

        [Test]
        public void Given_that_guard_finds_matches_When_executes_Then_should_return_correct_result()
        {
            //Given
            var suppliersSaveTask = CreateSuppliersSaveTask();
            var request = SaveCompanyDefaultViewModelBuilder
                .Create()
                .WithRunMatchCheck(true)
                .Build();

            var matches = new List<string>() { "Matching Name 1" };
            _doesCompanyDefaultAlreadyExistGuard
                .Setup(x => x.Execute(It.IsAny<Func<IEnumerable<MatchingName>>>()))
                .Returns(GuardDefaultExistsReponse.MatchesExist(matches));

            //When
            var result = suppliersSaveTask.Execute(request, _userId);

            //Then
            Assert.That(result.Success, Is.False);
            Assert.That(result.Matches.Count(), Is.EqualTo(matches.Count));
        }

        [Test]
        public void Given_that_creating_a_new_supplier_When_executes_Then_should_call_CreateSupplier_on_service()
        {
            //Given
            var peopleAtRiskSaveTask = CreateSuppliersSaveTask();
            var request = SaveCompanyDefaultViewModelBuilder.Create().Build();

            //When
            peopleAtRiskSaveTask.Execute(request, _userId);

            //Then
            _suppliersService.Verify(cds => cds.CreateSupplier(It.IsAny<SaveSupplierRequest>()), Times.Once());
        }

        [Test]
        public void Given_that_updating_supplier_When_executes_Then_should_call_UpdateSupplier_on_service()
        {
            //Given
            var peopleAtRiskSaveTask = CreateSuppliersSaveTask();
            var request = SaveCompanyDefaultViewModelBuilder.Create().WithDefaultId(1).Build();

            //When
            peopleAtRiskSaveTask.Execute(request, _userId);

            //Then
            _suppliersService.Verify(cds => cds.UpdateSupplier(It.IsAny<SaveSupplierRequest>()), Times.Once());
        }

        [Test]
        public void Given_that_creating_a_new_supplier_When_executes_Then_should_return_correct_result()
        {
            //Given
            var peopleAtRiskSaveTask = CreateSuppliersSaveTask();

            const long expectedId = 5;
            _suppliersService.Setup(x => x.CreateSupplier(It.IsAny<SaveSupplierRequest>())).Returns(expectedId);

            //When
            var request = SaveCompanyDefaultViewModelBuilder.Create().Build();
            var result = peopleAtRiskSaveTask.Execute(request, _userId);

            //Then
            Assert.That(result.Id, Is.EqualTo(5));
        }


        [Test]
        public void Given_that_updating_a_supplier_When_executes_Then_should_return_correct_result()
        {
            //Given
            var peopleAtRiskSaveTask = CreateSuppliersSaveTask();

            const int expectedId = 5;
            
            //When
            var request = SaveCompanyDefaultViewModelBuilder.Create().WithDefaultId(expectedId).Build();
            var result = peopleAtRiskSaveTask.Execute(request, _userId);

            //Then
            Assert.That(result.Id, Is.EqualTo(expectedId));
        }

        private SuppliersSaveTask CreateSuppliersSaveTask()
        {
            var peopleAtRiskSaveTask = new SuppliersSaveTask(_suppliersService.Object, 
                                                                _suppliersRepository.Object,
                                                                _doesCompanyDefaultAlreadyExistGuard.Object);
            return peopleAtRiskSaveTask;
        }
    }
}