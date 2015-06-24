using System.Collections.Generic;
using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request.HazardousSubstanceInventory;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.ViewModels;
using BusinessSafe.WebSite.Factories.HazardousSubstances;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.HazardousSubstances
{
    [TestFixture]
    public class Inventory
    {
        private Mock<ISuppliersService> _supplierService;
        private Mock<IHazardousSubstancesService> _hazardousSubstanceService;
        private InventoryViewModelFactory _target;

        [SetUp]
        public void Setup()
        {
            _supplierService = new Mock<ISuppliersService>();
            _supplierService.Setup(x => x.GetForCompany(It.IsAny<long>())).Returns(new List<SupplierDto>());

            _hazardousSubstanceService = new Mock<IHazardousSubstancesService>();
            _hazardousSubstanceService.Setup(x => x.GetForCompany(It.IsAny<long>())).Returns(new List<HazardousSubstanceDto>());

            _target = new InventoryViewModelFactory(_hazardousSubstanceService.Object, _supplierService.Object);
        }

        [Test]
        public void GetViewModel_Returns_ViewModel()
        {
            // Given

            // When
            var result = _target.WithCompanyId(It.IsAny<long>()).GetViewModel();

            // Then
            Assert.That(result, Is.InstanceOf<InventoryViewModel>());
        }

        [Test]
        public void GetViewModel_Gets_HazardousSubstances_From_Service()
        {
            // Given
            var hazardousSubstances = new List<HazardousSubstanceDto>();
            _hazardousSubstanceService.Setup(x => x.GetForCompany(1234)).Returns(hazardousSubstances);

            // When
            var result = _target.WithCompanyId(1234).GetViewModel();

            // Then
            Assert.That(result.Substances, Is.EqualTo(hazardousSubstances));
        }

        [Test]
        public void Given_valid_criteria_entered_When_search_clicked_correct_methods_called_and_correct_result_returned()
        {
            //Given
            var now = DateTime.Now;

            var request = new SearchHazardousSubstancesRequest
                          {
                              CompanyId = 5678L,
                              SubstanceNameLike = "Test name like",
                              SupplierId = 2345L
                          };

            _hazardousSubstanceService
                .Setup(x => x.Search(It.Is<SearchHazardousSubstancesRequest>(
                    y => y.CompanyId == request.CompanyId
                    && y.SubstanceNameLike == request.SubstanceNameLike
                    && y.SupplierId == request.SupplierId)))
                .Returns(new List<HazardousSubstanceDto>
                         {
                             new HazardousSubstanceDto
                             {
                                 CompanyId = 2345,
                                 AssessmentRequired = true,
                                 CreatedOn = now,
                                 DetailsOfUse = "Test details of use 1",
                                 Id = 1,
                                 Name = "Test Substance 1",
                                 Reference = "ref01",
                                 RiskPhrases = new List<RiskPhraseDto>
                                               {
                                                   new RiskPhraseDto{ Id = 1, ReferenceNumber = "RY01" },
                                                   new RiskPhraseDto{ Id = 2, ReferenceNumber = "RY02" }
                                               },
                                 HazardousSubstanceSafetyPhrases = new List<HazardousSubstanceSafetyPhraseDto>
                                               {
                                                   new HazardousSubstanceSafetyPhraseDto{ SafetyPhase  = new SafetyPhraseDto{Id = 1, ReferenceNumber = "SY01" }},
                                                   new HazardousSubstanceSafetyPhraseDto{ SafetyPhase  = new SafetyPhraseDto{Id = 2, ReferenceNumber = "SY02" }}
                                               },
                                 SdsDate = now,
                                 Standard = HazardousSubstanceStandard.European,
                                 Supplier = new SupplierDto { Id = 2345L, Name = "Test Supplier 3" }
                             },
                             new HazardousSubstanceDto
                             {
                                 CompanyId = 2345,
                                 AssessmentRequired = true,
                                 CreatedOn = now,
                                 DetailsOfUse = "Test details of use 2",
                                 Id = 2,
                                 Name = "Test Substance 2",
                                 Reference = "ref02",
                                 RiskPhrases = new List<RiskPhraseDto>
                                               {
                                                   new RiskPhraseDto{ Id = 3, ReferenceNumber = "RY03" },
                                                   new RiskPhraseDto{ Id = 4, ReferenceNumber = "RY04" }
                                               },
                                 HazardousSubstanceSafetyPhrases = new List<HazardousSubstanceSafetyPhraseDto>
                                               {
                                                   new HazardousSubstanceSafetyPhraseDto{ SafetyPhase  = new SafetyPhraseDto{ Id = 3, ReferenceNumber = "SY03" }},
                                                   new HazardousSubstanceSafetyPhraseDto{ SafetyPhase  = new SafetyPhraseDto{ Id = 4, ReferenceNumber = "SY04" }}
                                               },
                                 SdsDate = now,
                                 Standard = HazardousSubstanceStandard.Global,
                                 Supplier = new SupplierDto { Id = 2345L, Name = "Test Supplier 3" }
                             }
                         });

            //When
            var result = _target
                .WithCompanyId(5678L)
                .WithSubstanceNameLike("Test name like")
                .WithSupplierId(2345L)
                .GetViewModel();

            var substances = result.Substances.ToList();

            //Then
            _hazardousSubstanceService.Verify(x => x.Search(It.Is<SearchHazardousSubstancesRequest>(
                y => y.CompanyId == request.CompanyId
                    && y.SubstanceNameLike == request.SubstanceNameLike
                        && y.SupplierId == request.SupplierId)));

            Assert.AreEqual(5678L, result.CompanyId);
            Assert.AreEqual(2345L, result.SupplierId);
            Assert.AreEqual("Test name like", result.SubstanceNameLike);
            Assert.AreEqual(2, substances.Count());
            Assert.AreEqual("Test Substance 1", substances[0].Name);
            Assert.AreEqual("Test Substance 2", substances[1].Name);
            Assert.AreEqual("Test details of use 1", substances[0].DetailsOfUse);
            Assert.AreEqual("Test details of use 2", substances[1].DetailsOfUse);
            Assert.AreEqual("Test Supplier 3", substances[0].Supplier);
            Assert.AreEqual("Test Supplier 3", substances[1].Supplier);
            Assert.AreEqual("RY01, RY02", substances[0].RiskPhraseReferences);
            Assert.AreEqual("RY03, RY04", substances[1].RiskPhraseReferences);
            Assert.AreEqual("SY01, SY02", substances[0].SafetyPhraseReferences);
            Assert.AreEqual("SY03, SY04", substances[1].SafetyPhraseReferences);
            Assert.AreEqual("European", substances[0].Standard);
            Assert.AreEqual("Global", substances[1].Standard);
        }
    }
}
