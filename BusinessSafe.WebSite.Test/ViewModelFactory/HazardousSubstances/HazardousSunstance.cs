using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.ViewModels;
using BusinessSafe.WebSite.Factories.HazardousSubstances;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.HazardousSubstances
{
    [TestFixture]
    public class HazardousSunstance
    {
        private Mock<ISuppliersService> _supplierService;
        private Mock<IHazardousSubstancesService> _hazardousSubstanceService;
        private Mock<IPictogramService> _pictogramService;
        private Mock<IRiskPhraseService> _riskPhraseService;
        private Mock<ISafetyPhraseService> _safetyPhraseService;
        private HazardousSubstanceViewModelFactory _target;
        private long _companyId;
        private long _hazardousSubstanceId;
        private List<SupplierDto> _supplierDtos;
        private IEnumerable<PictogramDto> _pictograms;
        private IEnumerable<RiskPhraseDto> _riskPhrases;
        private IEnumerable<SafetyPhraseDto> _safetyPhrses;

        [SetUp]
        public void Setup()
        {
            _companyId = 200;
            _hazardousSubstanceId = 300;

            _supplierService = new Mock<ISuppliersService>();
            _supplierDtos = new List<SupplierDto>()
                                {
                                    new SupplierDto()
                                };
            _supplierService.Setup(x => x.GetForCompany(_companyId)).Returns(_supplierDtos);

            _hazardousSubstanceService = new Mock<IHazardousSubstancesService>();
            
            _pictogramService = new Mock<IPictogramService>();
            _pictograms = new List<PictogramDto>()
                              {
                                  new PictogramDto()
                              };
            _pictogramService.Setup(x => x.GetAll()).Returns(_pictograms);

            _riskPhraseService = new Mock<IRiskPhraseService>();
            _riskPhrases = new List<RiskPhraseDto>()
                               {
                                   new RiskPhraseDto()
                               };
            _riskPhraseService.Setup(x => x.GetAll()).Returns(_riskPhrases);

            _safetyPhraseService = new Mock<ISafetyPhraseService>();
            _safetyPhrses = new List<SafetyPhraseDto>()
                                {
                                    new SafetyPhraseDto()
                                };
            _safetyPhraseService.Setup(x => x.GetAll()).Returns(_safetyPhrses);

            _target = new HazardousSubstanceViewModelFactory(_hazardousSubstanceService.Object, _supplierService.Object, _pictogramService.Object, _riskPhraseService.Object, _safetyPhraseService.Object);
        }

        [Test]
        public void GetViewModel_Returns_ViewModel()
        {
            // Given

            // When
            var result = _target.WithCompanyId(_companyId).WithHazardousSubstanceId(0).GetViewModel();

            // Then
            Assert.That(result, Is.InstanceOf<AddEditHazardousSubstanceViewModel>());
        }

        [Test]
        public void GetViewModel_For_New_HazardousSunbstance_Call_Correct_Methods()
        {
            // Given
            _hazardousSubstanceId = 0;

            // When
            _target.WithCompanyId(_companyId).WithHazardousSubstanceId(_hazardousSubstanceId).GetViewModel();

            // Then
            _supplierService.Verify(x => x.GetForCompany(_companyId));
            _hazardousSubstanceService.Verify(x => x.GetByIdAndCompanyId(_companyId, _hazardousSubstanceId), Times.Never());
        }

        [Test]
        public void GetViewModel_For_Edit_HazardousSunbstance_Call_Correct_Methods()
        {
            // Given
            _hazardousSubstanceId = 500;

            _hazardousSubstanceService
                .Setup(x => x.GetByIdAndCompanyId(_hazardousSubstanceId, _companyId))
                .Returns(new HazardousSubstanceDto());

            // When
            _target.WithCompanyId(_companyId).WithHazardousSubstanceId(_hazardousSubstanceId).GetViewModel();

            // Then
            _supplierService.Verify(x => x.GetForCompany(_companyId));
            _hazardousSubstanceService.Verify(x => x.GetByIdAndCompanyId(_hazardousSubstanceId, _companyId));
        }

        [Test]
        public void GetViewModel_For_New_HazardousSunbstance_Returns_Correct_ViewModel()
        {
            // Given
            _hazardousSubstanceId = 0;

            // When
            var result = _target.WithCompanyId(_companyId).WithHazardousSubstanceId(_hazardousSubstanceId).GetViewModel();

            // Then
            Assert.That(result.CompanyId, Is.EqualTo(_companyId));
            Assert.That(result.Pictograms.Count(), Is.EqualTo(_pictograms.Count()));
            Assert.That(result.RiskPhrases.Count(), Is.EqualTo(_riskPhrases.Count()));
            Assert.That(result.SafetyPhrases.Count(), Is.EqualTo(_safetyPhrses.Count()));
            
        }

        [Test]
        public void GetViewModel_For_Edit_HazardousSunbstance_Returns_Correct_ViewModel()
        {
            // Given
            _hazardousSubstanceId = 200;


            var hazardousSubstance = new HazardousSubstanceDto()
                                         {
                                             Id = 200,
                                             Name = "Test Hazard",
                                             Reference = "Test Ref",
                                             SdsDate = DateTime.Now,
                                             Supplier = new SupplierDto() { Id = 1 },
                                             Standard = HazardousSubstanceStandard.European,
                                             Pictograms = new List<PictogramDto>()
                                                              {
                                                                  new PictogramDto()
                                                              },
                                             RiskPhrases = new List<RiskPhraseDto>()
                                                               {
                                                                   new RiskPhraseDto()
                                                               },
                                             HazardousSubstanceSafetyPhrases = new List<HazardousSubstanceSafetyPhraseDto>()
                                                                                   {
                                                                                       new HazardousSubstanceSafetyPhraseDto()
                                                                                           {
                                                                                               SafetyPhase = new SafetyPhraseDto()
                                                                                           }
                                                                                   },
                                            DetailsOfUse = "Test",
                                            AssessmentRequired = true
                                         };
            _hazardousSubstanceService
                .Setup(x => x.GetByIdAndCompanyId(_hazardousSubstanceId, _companyId))
                .Returns(hazardousSubstance);

            // When
            var result = _target.WithCompanyId(_companyId).WithHazardousSubstanceId(_hazardousSubstanceId).GetViewModel();

            // Then
            Assert.That(result.CompanyId, Is.EqualTo(_companyId));
            Assert.That(result.Pictograms.Count(), Is.EqualTo(_pictograms.Count()));
            Assert.That(result.RiskPhrases.Count(), Is.EqualTo(_riskPhrases.Count()));
            Assert.That(result.SafetyPhrases.Count(), Is.EqualTo(_safetyPhrses.Count()));

            Assert.That(result.Id, Is.EqualTo(hazardousSubstance.Id));
            Assert.That(result.Name, Is.EqualTo(hazardousSubstance.Name));
            Assert.That(result.Reference, Is.EqualTo(hazardousSubstance.Reference));
            Assert.That(result.SdsDate, Is.EqualTo(hazardousSubstance.SdsDate));
            Assert.That(result.SupplierId, Is.EqualTo(hazardousSubstance.Supplier.Id));

            Assert.That(result.HazardousSubstanceStandard, Is.EqualTo(hazardousSubstance.Standard));
            Assert.That(result.SelectedHazardousSubstanceSymbols.Count(), Is.EqualTo(hazardousSubstance.Pictograms.Count));
            Assert.That(result.SelectedRiskPhrases.Count(), Is.EqualTo(hazardousSubstance.RiskPhrases.Count));
            Assert.That(result.SafetyPhrases.Count(), Is.EqualTo(hazardousSubstance.HazardousSubstanceSafetyPhrases.Count()));
            Assert.That(result.DetailsOfUse, Is.EqualTo(hazardousSubstance.DetailsOfUse));
            Assert.That(result.AssessmentRequired, Is.EqualTo(hazardousSubstance.AssessmentRequired));
        }
    }
}