using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request.HazardousSubstanceInventory;
using BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.ViewModels;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.HazardousSubstanceInventory.Inventory
{
    [TestFixture]
    public class SubstanceDetailsForRiskAssessment : HazardousSubtanceInventoryTest
    {

        [SetUp]
        public new void Setup()
        {
            base.Setup();
        }

        [Test]
        public void When_get_SubstanceDetailsForRiskAssessment_Then_requested_hazardous_substance_fills_in_view_model()
        {
            // Given 
            var hazardousSubstanceDto = new HazardousSubstanceDto()
                                        {
                                            Name = "a haz sub",
                                            Pictograms = new List<PictogramDto>()
                                                         {
                                                             new PictogramDto() { Title = "hazard a"},
                                                             new PictogramDto() { Title = "hazard b"},
                                                             new PictogramDto() { Title = "hazard c"}
                                                         },
                                            RiskPhrases = new List<RiskPhraseDto>()
                                                          {
                                                              new RiskPhraseDto() { ReferenceNumber = "RP_01", Title = "Risk Phrase 1"},
                                                              new RiskPhraseDto() { ReferenceNumber = "RP_02", Title = "Risk Phrase 2"},
                                                              new RiskPhraseDto() { ReferenceNumber = "RP_03", Title = "Risk Phrase 3"},
                                                              new RiskPhraseDto() { ReferenceNumber = "RP_04", Title = "Risk Phrase 4"},
                                                          },
                                            HazardousSubstanceSafetyPhrases = new List<HazardousSubstanceSafetyPhraseDto>()
                                                                                  {
                                                                                      new HazardousSubstanceSafetyPhraseDto()
                                                                                          {
                                                                                              SafetyPhase = new SafetyPhraseDto() { ReferenceNumber = "SP_01", Title = "Safety Phrase 1"},
                                                                                              AdditionalInformation = "Additional Information 1"
                                                                                          },
                                                                                      new HazardousSubstanceSafetyPhraseDto()
                                                                                          {
                                                                                              SafetyPhase = new SafetyPhraseDto() { ReferenceNumber = "SP_02", Title = "Safety Phrase 2"},
                                                                                              AdditionalInformation = "Additional Information 2"
                                                                                          },
                                                                                      new HazardousSubstanceSafetyPhraseDto()
                                                                                          {
                                                                                              SafetyPhase = new SafetyPhraseDto() { ReferenceNumber = "SP_03", Title = "Safety Phrase 3"}
                                                                                          }
                                                                                  }
                                        };
            hazardousSubstancesService
                .Setup(x => x.GetByIdAndCompanyId(1234, It.IsAny<long>()))
                .Returns(hazardousSubstanceDto);

            // When
            var result = target.SubstanceDetailsForRiskAssessment(1234);
            var model = result.Model as HazardousSubstanceSummaryViewModel;

            // Then
            Assert.That(model.Name, Is.EqualTo(hazardousSubstanceDto.Name));
            Assert.That(model.RiskPhrases.Length, Is.EqualTo(4));
            Assert.That(model.RiskPhrases.Contains("RP_01 Risk Phrase 1"));
            Assert.That(model.RiskPhrases.Contains("RP_02 Risk Phrase 2"));
            Assert.That(model.RiskPhrases.Contains("RP_03 Risk Phrase 3"));
            Assert.That(model.RiskPhrases.Contains("RP_04 Risk Phrase 4"));
            Assert.That(model.SafetyPhrases.Length, Is.EqualTo(3));
            Assert.That(model.SafetyPhrases.Contains("SP_01 Safety Phrase 1"));
            Assert.That(model.SafetyPhrases.Contains("SP_02 Safety Phrase 2"));
            Assert.That(model.SafetyPhrases.Contains("SP_03 Safety Phrase 3"));
            Assert.That(model.AdditionalInformationRecords.Length, Is.EqualTo(2));
            Assert.That(model.AdditionalInformationRecords.Contains("SP_01: Additional Information 1"));
            Assert.That(model.AdditionalInformationRecords.Contains("SP_02: Additional Information 2"));
        }
    }
}
