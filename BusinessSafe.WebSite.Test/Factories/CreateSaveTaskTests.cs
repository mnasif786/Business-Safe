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
    public class CreateSaveTaskTests
    {
        private Mock<ISuppliersService> _suppliersService;
        private Mock<ICompanyDefaultService> _companyDefaultService;
        private Mock<IDoesCompanyDefaultAlreadyExistGuard> _doesCompanyDefaultAlreadyExistGuard;

        [SetUp]
        public void Setup()
        {
            _companyDefaultService = new Mock<ICompanyDefaultService>();
           _doesCompanyDefaultAlreadyExistGuard = new Mock<IDoesCompanyDefaultAlreadyExistGuard>();
            _suppliersService = new Mock<ISuppliersService>();
        }

            [Test]
        public void Given_that_hazards_type_is_passed_to_CompanyDefaultsTaskFactory_Then_returns_correct_save_task()
        {
            //Given
            const string companyDefaultType = "Hazards";
            //When
            var result = CreateCompanyDefaultTaskFactory().CreateSaveTask(companyDefaultType);

            //Then
            Assert.That(result, Is.TypeOf<HazardSaveTask>());
        }

        [Test]
        public void Given_that_peopleatrisk_type_is_passed_to_CompanyDefaultsTaskFactory_Then_returns_correct_save_task()
        {
            //Given
            const string companyDefaultType = "PeopleAtRisk";
            //When
            var result = CreateCompanyDefaultTaskFactory().CreateSaveTask(companyDefaultType);

            //Then
            Assert.That(result, Is.TypeOf<PeopleAtRiskSaveTask>());
        }


        [Test]
        public void Given_that_suppliers_type_is_passed_to_CompanyDefaultsTaskFactory_Then_returns_correct_save_task()
        {
            //Given
            const string companyDefaultType = "SpecialistSuppliers";
            //When
            var result = CreateCompanyDefaultTaskFactory().CreateSaveTask(companyDefaultType);

            //Then
            Assert.That(result, Is.TypeOf<SuppliersSaveTask>());
        }

        [Test]
        public void Given_that_fire_safety_control_measure_type_is_passed_to_CompanyDefaultsTaskFactory_Then_returns_correct_save_task()
        {
            //Given
            const string companyDefaultType = "FireSafetyControlMeasure";
            //When
            var result = CreateCompanyDefaultTaskFactory().CreateSaveTask(companyDefaultType);

            //Then
            Assert.That(result, Is.TypeOf<FireSafetyControlMeasureSaveTask>());
        }

        [Test]
        public void Given_that_source_of_fuel_type_is_passed_to_CompanyDefaultsTaskFactory_Then_returns_correct_save_task()
        {
            //Given
            const string companyDefaultType = "SourceOfFuel";
            //When
            var result = CreateCompanyDefaultTaskFactory().CreateSaveTask(companyDefaultType);

            //Then
            Assert.That(result, Is.TypeOf<SourceOfFuelSaveTask>());
        }

        [Test]
        public void Given_that_source_of_ignition_type_is_passed_to_CompanyDefaultsTaskFactory_Then_returns_correct_save_task()
        {
            //Given
            const string companyDefaultType = "SourceOfIgnition";
            //When
            var result = CreateCompanyDefaultTaskFactory().CreateSaveTask(companyDefaultType);

            //Then
            Assert.That(result, Is.TypeOf<SourceOfIgnitionSaveTask>());
        }
        [Test]
        public void Given_that_injury_type_is_passed_to_CompanyDefaultsTaskFactory_Then_returns_correct_save_task()
        {
            //Given
            const string companyDefaultType = "Injury";
            //When
            var result = CreateCompanyDefaultTaskFactory().CreateSaveTask(companyDefaultType);

            //Then
            Assert.That(result, Is.TypeOf<InjurySaveTask>());
        }
       
        [Test]
        public void Given_that_invalid_type_is_passed_to_CompanyDefaultsTaskFactory_Then_throws_exception()
        {
            //Given
            const string companyDefaultType = "Invalid";
            //When
            //Than
            Assert.Throws<ArgumentException>(() => CreateCompanyDefaultTaskFactory().CreateSaveTask(companyDefaultType));
        
        }


        private CompanyDefaultsTaskFactory CreateCompanyDefaultTaskFactory()
        {
            return new CompanyDefaultsTaskFactory(_companyDefaultService.Object, _suppliersService.Object, _doesCompanyDefaultAlreadyExistGuard.Object, null, null, null);
        }
    }


}
