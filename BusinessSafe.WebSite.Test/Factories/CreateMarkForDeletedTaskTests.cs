using System;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.Application.Implementations.Company;
using BusinessSafe.WebSite.Areas.Company.Factories;
using BusinessSafe.WebSite.Areas.Company.Tasks;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Factories
{
    [TestFixture]
    [Category("Unit")]
    public class CreateMarkForDeletedTaskTests
    {
        private Mock<ICompanyDefaultService> _companyDefaultService;
        private Mock<IDoesCompanyDefaultAlreadyExistGuard> _doesCompanyDefaultAlreadyExistGuard;
        private Mock<ISuppliersService> _suppliersService;

        [SetUp]
        public void Setup()
        {
            _suppliersService = new Mock<ISuppliersService>();
            _companyDefaultService = new Mock<ICompanyDefaultService>();
            _doesCompanyDefaultAlreadyExistGuard = new Mock<IDoesCompanyDefaultAlreadyExistGuard>();
        }

        [Test]
        public void Given_that_hazards_type_is_passed_to_CompanyDefaultsTaskFactory_Then_returns_hazard_mark_as_deleted_task()
        {
            //Given
            const string companyDefaultType = "Hazards";
            //When
            var result = CreateCompanyDefaultTaskFactory().CreateMarkForDeletedTask(companyDefaultType);

            //Then
            Assert.That(result, Is.TypeOf<HazardMarkAsDeletedTask>());
        }

        [Test]
        public void Given_that_peopleatrisk_type_is_passed_to_CompanyDefaultsTaskFactory_Then_returns_peopleatrisk_mark_as_deleted_task()
        {
            //Given
            const string companyDefaultType = "PeopleAtRisk";
            //When
            var result = CreateCompanyDefaultTaskFactory().CreateMarkForDeletedTask(companyDefaultType);

            //Then
            Assert.That(result, Is.TypeOf<PersonAtRiskMarkAsDeletedTask>());
        }


        [Test]
        public void Given_that_invalid_type_is_passed_to_CompanyDefaultsTaskFactory_Then_throws_exception()
        {
            //Given
            const string companyDefaultType = "Invalid";
            //When
            //Than
            Assert.Throws<ArgumentException>(() => CreateCompanyDefaultTaskFactory().CreateMarkForDeletedTask(companyDefaultType));

        }

        [Test]
        public void Given_that_suppliers_type_is_passed_to_CompanyDefaultsTaskFactory_Then_returns_peopleatrisk_delete_task()
        {
            //Given
            const string companyDefaultType = "SpecialistSuppliers";
            //When
            var result = CreateCompanyDefaultTaskFactory().CreateMarkForDeletedTask(companyDefaultType);

            //Then
            Assert.That(result, Is.TypeOf<SuppliersMarkAsDeletedTask>());
        }

        private CompanyDefaultsTaskFactory CreateCompanyDefaultTaskFactory()
        {
            return new CompanyDefaultsTaskFactory(_companyDefaultService.Object, _suppliersService.Object, _doesCompanyDefaultAlreadyExistGuard.Object, null, null, null);
        }
    }
}