using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Domain.Entities;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.HazardousSubstanceRiskAssessmentTests
{
    [TestFixture]
    public class CopyTests
    {
        [Test]
        public void Given_a_HSRA_when_copy_then_summary_information_is_copied()
        {
            //given
            var originalCreatingUser = new UserForAuditing { Id = Guid.NewGuid() };
            var currentUser = new UserForAuditing { Id = Guid.NewGuid() };
            var hazsub = new HazardousSubstance() { Id = 1234L };
            var hsraToCopy = HazardousSubstanceRiskAssessment.Create("this is the title", "the ref", 1312, originalCreatingUser, hazsub);
            hsraToCopy.AssessmentDate = DateTime.Now.Date.AddDays(-5);
            hsraToCopy.RiskAssessor = new RiskAssessor() { Id = 255L };

            //when
            var copiedHsra = hsraToCopy.Copy("new title", "new ref", currentUser) as HazardousSubstanceRiskAssessment;

            //then
            Assert.AreEqual("new title", copiedHsra.Title);
            Assert.AreEqual("new ref", copiedHsra.Reference);
            Assert.AreEqual(hsraToCopy.AssessmentDate, copiedHsra.AssessmentDate);
            Assert.That(copiedHsra.RiskAssessor, Is.Null);
            Assert.AreEqual(hsraToCopy.HazardousSubstance.Id, copiedHsra.HazardousSubstance.Id);
        }

        [Test]
        public void Given_a_HSRA_when_copy_then_createdby_and_createdDate_are_set()
        {
            //given
            var originalCreatingUser = new UserForAuditing { Id = Guid.NewGuid() };
            var currentUser = new UserForAuditing { Id = Guid.NewGuid() };
            var hazsub = new HazardousSubstance() { Id = 1234L };
            var hsraToCopy = HazardousSubstanceRiskAssessment.Create("this is the title", "the ref", 1312, originalCreatingUser, hazsub);

            //when
            var copiedFra = hsraToCopy.Copy("new title", "new ref", currentUser) as HazardousSubstanceRiskAssessment;

            //then
            Assert.AreEqual(currentUser.Id, copiedFra.CreatedBy.Id);
            Assert.AreEqual(copiedFra.CreatedOn.Value.Date, DateTime.Now.Date);
        }

        [Test]
        public void Given_a_HSRA_when_copy_then_employees_are_set()
        {
            //Given
            var user = new UserForAuditing();
            var riskAssessment = HazardousSubstanceRiskAssessment.Create("new title", "new reference", 1, user, new HazardousSubstance());

            var employee1 = new Mock<Employee>();
            var employee1Id = Guid.NewGuid();
            employee1.Setup(x => x.Id).Returns(employee1Id);
            riskAssessment.AttachEmployeeToRiskAssessment(employee1.Object, user);

            var employee2 = new Mock<Employee>();
            var employee2Id = Guid.NewGuid();
            employee2.Setup(x => x.Id).Returns(employee2Id);
            riskAssessment.AttachEmployeeToRiskAssessment(employee2.Object, user);

            //When
            var result = riskAssessment.Copy("Test Clone Reference", "", user) as HazardousSubstanceRiskAssessment;

            //Then
            Assert.That(result.Employees.Count, Is.EqualTo(riskAssessment.Employees.Count));
            Assert.That(result.Employees.Select(x => x.Id).First(), Is.EqualTo(riskAssessment.Employees.Select(x => x.Id).First()));
            Assert.That(result.Employees.Select(x => x.Id).Last(), Is.EqualTo(riskAssessment.Employees.Select(x => x.Id).Last()));
        }

        [Test]
        public void Given_a_HSRA_when_copy_then_non_employees_are_set()
        {
            //Given
            var user = new UserForAuditing();
            var riskAssessment = HazardousSubstanceRiskAssessment.Create("new title", "new reference", 1, user, new HazardousSubstance());

            var nonEmployee1 = new Mock<NonEmployee>();
            var nonEmployee1Id = 100;
            nonEmployee1.Setup(x => x.Id).Returns(nonEmployee1Id);
            riskAssessment.AttachNonEmployeeToRiskAssessment(nonEmployee1.Object, user);

            var nonEmployee2 = new Mock<NonEmployee>();
            var nonEmployee2Id = 200;
            nonEmployee2.Setup(x => x.Id).Returns(nonEmployee2Id);
            riskAssessment.AttachNonEmployeeToRiskAssessment(nonEmployee2.Object, user);

            //When
            var result = riskAssessment.Copy("Test Clone Reference", "", user) as HazardousSubstanceRiskAssessment;

            //Then
            Assert.That(result.NonEmployees.Count, Is.EqualTo(riskAssessment.NonEmployees.Count));
            Assert.That(result.NonEmployees.Select(x => x.Id).First(), Is.EqualTo(riskAssessment.NonEmployees.Select(x => x.Id).First()));
            Assert.That(result.NonEmployees.Select(x => x.Id).Last(), Is.EqualTo(riskAssessment.NonEmployees.Select(x => x.Id).Last()));
        }

        [Test]
        public void Given_a_HSRA_when_copy_then_work_exposure_limit_is_set()
        {
            //Given
            const string wel = "WEL";
            var user = new UserForAuditing();
            var riskAssessment = HazardousSubstanceRiskAssessment.Create("new title", "new reference", 1, user, new HazardousSubstance());
            riskAssessment.WorkspaceExposureLimits = wel;

            //When
            var result = riskAssessment.Copy("Test Clone Reference", "", user) as HazardousSubstanceRiskAssessment;

            //Then
            Assert.That(result.WorkspaceExposureLimits, Is.EqualTo(wel));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Given_a_HSRA_when_copy_then_inhalation_is_set(bool isInhalationRouteOfEntry)
        {
            //Given
            var user = new UserForAuditing();
            var riskAssessment = HazardousSubstanceRiskAssessment.Create("new title", "new reference", 1, user, new HazardousSubstance());
            riskAssessment.IsInhalationRouteOfEntry = isInhalationRouteOfEntry;

            //When
            var result = riskAssessment.Copy("Test Clone Reference", "", user) as HazardousSubstanceRiskAssessment;

            //Then
            Assert.That(result.IsInhalationRouteOfEntry, Is.EqualTo(isInhalationRouteOfEntry));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Given_a_HSRA_when_copy_then_ingestion_is_set(bool isIngestionRouteOfEntry)
        {
            //Given
            var user = new UserForAuditing();
            var riskAssessment = HazardousSubstanceRiskAssessment.Create("new title", "new reference", 1, user, new HazardousSubstance());
            riskAssessment.IsIngestionRouteOfEntry = isIngestionRouteOfEntry;

            //When
            var result = riskAssessment.Copy("Test Clone Reference", "", user) as HazardousSubstanceRiskAssessment;

            //Then
            Assert.That(result.IsIngestionRouteOfEntry, Is.EqualTo(isIngestionRouteOfEntry));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Given_a_HSRA_when_copy_then_absorption_is_set(bool isAbsorptionRouteOfEntry)
        {
            //Given
            var user = new UserForAuditing();
            var riskAssessment = HazardousSubstanceRiskAssessment.Create("new title", "new reference", 1, user, new HazardousSubstance());
            riskAssessment.IsAbsorptionRouteOfEntry = isAbsorptionRouteOfEntry;

            //When
            var result = riskAssessment.Copy("Test Clone Reference", "", user) as HazardousSubstanceRiskAssessment;

            //Then
            Assert.That(result.IsAbsorptionRouteOfEntry, Is.EqualTo(isAbsorptionRouteOfEntry));
        }

        [TestCase(Quantity.Small)]
        [TestCase(Quantity.Medium)]
        [TestCase(Quantity.Large)]
        public void Given_a_HSRA_when_copy_then_quantity_is_set(Quantity quantity)
        {
            //Given
            var user = new UserForAuditing();
            var riskAssessment = HazardousSubstanceRiskAssessment.Create("new title", "new reference", 1, user, new HazardousSubstance());
            riskAssessment.Quantity = quantity;

            //When
            var result = riskAssessment.Copy("Test Clone Reference", "", user) as HazardousSubstanceRiskAssessment;

            //Then
            Assert.That(result.Quantity, Is.EqualTo(quantity));
        }

        [TestCase(MatterState.Liquid)]
        [TestCase(MatterState.Solid)]
        public void Given_a_HSRA_when_copy_then_matter_state_is_set(MatterState state)
        {
            //Given
            var user = new UserForAuditing();
            var riskAssessment = HazardousSubstanceRiskAssessment.Create("new title", "new reference", 1, user, new HazardousSubstance());
            riskAssessment.MatterState = state;

            //When
            var result = riskAssessment.Copy("Test Clone Reference", "", user) as HazardousSubstanceRiskAssessment;

            //Then
            Assert.That(result.MatterState, Is.EqualTo(state));
        }

        [TestCase(DustinessOrVolatility.Low)]
        [TestCase(DustinessOrVolatility.Medium)]
        [TestCase(DustinessOrVolatility.High)]
        public void Given_a_HSRA_when_copy_then_DustinessOrVolatility_is_set(DustinessOrVolatility dustinessOrVolatility)
        {
            //Given
            var user = new UserForAuditing();
            var riskAssessment = HazardousSubstanceRiskAssessment.Create("new title", "new reference", 1, user, new HazardousSubstance());
            riskAssessment.DustinessOrVolatility = dustinessOrVolatility;

            //When
            var result = riskAssessment.Copy("Test Clone Reference", "", user) as HazardousSubstanceRiskAssessment;

            //Then
            Assert.That(result.DustinessOrVolatility, Is.EqualTo(dustinessOrVolatility));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Given_a_HSRA_when_copy_then_health_surveilance_is_set(bool healthSurveillanceRequired)
        {
            //Given
            var user = new UserForAuditing();
            var riskAssessment = HazardousSubstanceRiskAssessment.Create("new title", "new reference", 1, user, new HazardousSubstance());
            riskAssessment.HealthSurveillanceRequired = healthSurveillanceRequired;

            //When
            var result = riskAssessment.Copy("Test Clone Reference", "", user) as HazardousSubstanceRiskAssessment;

            //Then
            Assert.That(result.HealthSurveillanceRequired, Is.EqualTo(healthSurveillanceRequired));
        }

        [Test]
        public void Given_a_HSRA_when_copy_then_attached_documents_are_cloned()
        {

            var currentUser = new UserForAuditing { Id = Guid.NewGuid() };
            IEnumerable<RiskAssessmentDocument> documents = new List<RiskAssessmentDocument>()
                                                     {
                                                          new RiskAssessmentDocument()
                                                             {
                                                                 Id = 1, DocumentLibraryId = 1, CreatedBy = new UserForAuditing { Id = Guid.NewGuid() }, CreatedOn = DateTime.Now.AddDays(-1234),Description = "doc description",
                                                             },
                                                         new RiskAssessmentDocument()
                                                             {
                                                                 Id = 2, DocumentLibraryId = 2, CreatedBy = new UserForAuditing { Id = Guid.NewGuid() }, CreatedOn = DateTime.Now.AddDays(-1234)
                                                             }
                                                         ,
                                                         new RiskAssessmentDocument()
                                                             {
                                                                 Id = 3, DocumentLibraryId = 3, CreatedBy = new UserForAuditing { Id = Guid.NewGuid() }, CreatedOn = DateTime.Now.AddDays(-1234) 
                                                             }
                                                     };
            var hsraToCopy = HazardousSubstanceRiskAssessment.Create("this is the title", "the ref", 1312, new UserForAuditing { Id = Guid.NewGuid() }, new HazardousSubstance());
            hsraToCopy.Documents = documents.ToList();

            //when
            var copiedHsra = hsraToCopy.Copy("new title", "new ref", currentUser);

            //then
            Assert.AreEqual(hsraToCopy.Documents.Count(), copiedHsra.Documents.Count);

            Assert.IsTrue(copiedHsra.Documents.All(x => x.Id == 0));
            Assert.IsTrue(copiedHsra.Documents.All(x => x.CreatedBy.Id == currentUser.Id)); //ensure that all cloned are new entities
            Assert.IsTrue(copiedHsra.Documents.All(x => x.CreatedOn.Value.Date == DateTime.Now.Date)); //ensure that all cloned are new entities
            Assert.IsTrue(copiedHsra.Documents.Any(x => x.Description == documents.First().Description));
        }

        [Test]
        public void Given_a_HRSA_when_copy_then_Control_Measures_are_cloned()
        {
            var currentUser = new UserForAuditing { Id = Guid.NewGuid() };

            IEnumerable<HazardousSubstanceRiskAssessmentControlMeasure> controlMeasures = new List<HazardousSubstanceRiskAssessmentControlMeasure>()
            {
                HazardousSubstanceRiskAssessmentControlMeasure.Create("controlMeasure 1", new HazardousSubstanceRiskAssessment() { CreatedBy = currentUser, Id = 1},currentUser),
                HazardousSubstanceRiskAssessmentControlMeasure.Create("controlMeasure 2", new HazardousSubstanceRiskAssessment() { CreatedBy = currentUser, Id = 2},currentUser),
                HazardousSubstanceRiskAssessmentControlMeasure.Create("controlMeasure 3", new HazardousSubstanceRiskAssessment() { CreatedBy = currentUser, Id = 3},currentUser),
            };

            var hsraToCopy = HazardousSubstanceRiskAssessment.Create("this is the title", "the ref", 1312,
                new UserForAuditing {Id = Guid.NewGuid()}, new HazardousSubstance());

            foreach (var controlMeasure in controlMeasures)
            {
                hsraToCopy.AddControlMeasure(controlMeasure, currentUser);
            }

            //when
            var copiedHsra = (HazardousSubstanceRiskAssessment)hsraToCopy.Copy("new title", "new ref", currentUser);

            Assert.That(copiedHsra.ControlMeasures.Count, Is.EqualTo(controlMeasures.Count()));
            Assert.That(copiedHsra.ControlMeasures.Any(x => x.ControlMeasure == controlMeasures.First().ControlMeasure));
        }
    }
}
