using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class CloneRiskAssessmentTest
    {
        [Test]
        public void Given_basic_riskassessment_details_Then_should_clone_correctly()
        {
            //Given
            var user = new UserForAuditing();
            var riskAssessment = GeneralRiskAssessment.Create("Test Title", "Test Reference", 1, user);

            //When
            var result = riskAssessment.Copy("new title", "Test Clone Reference", user);

            //Then
            Assert.That(result.CompanyId, Is.EqualTo(riskAssessment.CompanyId));
            Assert.That(result.Title, Is.EqualTo("new title"));
            Assert.That(result.Reference, Is.EqualTo("Test Clone Reference"));
            Assert.That(result.CreatedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
            Assert.That(result.CreatedBy, Is.EqualTo(user));
            Assert.That(result.AssessmentDate, Is.EqualTo(null));
            Assert.That(result.Status, Is.EqualTo(RiskAssessmentStatus.Draft));
        }

        [Test]
        public void Given_riskassessment_with_site_location_and_task_description_Then_should_clone_correctly()
        {
            //Given
            var user = new UserForAuditing();
            var riskAssessment = GeneralRiskAssessment.Create("", "", 1, user);

            riskAssessment.UpdatePremisesInformation("Test Location", "Test Process Dewscription",user);
            

            //When
            var result = riskAssessment.Copy("", "Test Clone Reference", user) as GeneralRiskAssessment;

            //Then
            Assert.That(result.Location, Is.EqualTo(riskAssessment.Location));
            Assert.That(result.TaskProcessDescription, Is.EqualTo(riskAssessment.TaskProcessDescription));
        }

        [Test]
        public void Given_riskassessment_with_employees_and_non_employees_Then_should_clone_correctly()
        {
            //Given
            var user = new UserForAuditing();
            var riskAssessment = GeneralRiskAssessment.Create("", "", 1, user);

            var employee1 = new Mock<Employee>();
            var employee1Id = Guid.NewGuid();
            employee1.Setup(x => x.Id).Returns(employee1Id);
            riskAssessment.AttachEmployeeToRiskAssessment(employee1.Object, user);  

            var employee2 = new Mock<Employee>();
            var employee2Id = Guid.NewGuid();
            employee2.Setup(x => x.Id).Returns(employee2Id);
            riskAssessment.AttachEmployeeToRiskAssessment(employee2.Object, user);

            var nonEmployee1 = new Mock<NonEmployee>();
            var nonEmployee1Id = 100;
            nonEmployee1.Setup(x => x.Id).Returns(nonEmployee1Id);
            riskAssessment.AttachNonEmployeeToRiskAssessment(nonEmployee1.Object, user);

            var nonEmployee2 = new Mock<NonEmployee>();
            var nonEmployee2Id = 200;
            nonEmployee2.Setup(x => x.Id).Returns(nonEmployee2Id);
            riskAssessment.AttachNonEmployeeToRiskAssessment(nonEmployee2.Object, user);

            //When
            var result = riskAssessment.Copy("","Test Clone Reference",user);

            //Then
            Assert.That(result.Employees.Count, Is.EqualTo(riskAssessment.Employees.Count));
            Assert.That(result.Employees.Select(x => x.Id).First(), Is.EqualTo(riskAssessment.Employees.Select(x => x.Id).First()));
            Assert.That(result.Employees.Select(x => x.Id).Last(), Is.EqualTo(riskAssessment.Employees.Select(x => x.Id).Last()));


            Assert.That(result.NonEmployees.Count, Is.EqualTo(riskAssessment.NonEmployees.Count));
            Assert.That(result.NonEmployees.Select(x => x.Id).First(), Is.EqualTo(riskAssessment.NonEmployees.Select(x => x.Id).First()));
            Assert.That(result.NonEmployees.Select(x => x.Id).Last(), Is.EqualTo(riskAssessment.NonEmployees.Select(x => x.Id).Last()));
        }

        [Test]
        public void Given_riskassessment_with_people_at_risk_Then_should_clone_correctly()
        {
            //Given
            var user = new UserForAuditing();
            var riskAssessment = GeneralRiskAssessment.Create("", "", 1, user);

            var personAtRisk1 = new Mock<PeopleAtRisk>();
            var personAtRisk1Id = 200;
            personAtRisk1.Setup(x => x.Id).Returns(personAtRisk1Id);
            riskAssessment.AttachPersonAtRiskToRiskAssessment(personAtRisk1.Object, user);

            var personAtRisk2 = new Mock<PeopleAtRisk>();
            var personAtRisk2Id = 400;
            personAtRisk2.Setup(x => x.Id).Returns(personAtRisk2Id);
            riskAssessment.AttachPersonAtRiskToRiskAssessment(personAtRisk2.Object, user);

            //When
            var result = riskAssessment.Copy("", "Test Clone Reference", user) as GeneralRiskAssessment;

            //Then
            Assert.That(result.PeopleAtRisk.Count, Is.EqualTo(riskAssessment.PeopleAtRisk.Count));
            Assert.That(result.PeopleAtRisk.Select(x => x.Id).First(), Is.EqualTo(riskAssessment.PeopleAtRisk.Select(x => x.Id).First()));
            Assert.That(result.PeopleAtRisk.Select(x => x.Id).Last(), Is.EqualTo(riskAssessment.PeopleAtRisk.Select(x => x.Id).Last()));

        }

        [Test]
        public void Given_riskassessment_with_hazards_Then_should_clone_correctly()
        {
            //Given
            var user = new UserForAuditing();
            var riskAssessment = GeneralRiskAssessment.Create("", "", 1, user);

            var hazard1 = new Mock<Hazard>();
            var hazard1Id = 200;
            hazard1.Setup(x => x.Id).Returns(hazard1Id);
            riskAssessment.AttachHazardsToRiskAssessment(new[]{ hazard1.Object}, user);
            riskAssessment.Hazards.First().UpdateDescription("Test Description 1", user);

            
            var hazard2 = new Mock<Hazard>();
            var hazard2Id = 400;
            hazard2.Setup(x => x.Id).Returns(hazard2Id);
            riskAssessment.AttachHazardsToRiskAssessment(new[] { hazard2.Object }, user);
            riskAssessment.Hazards.Last().UpdateDescription("Test Description 2", user);

            //When
            var result = riskAssessment.Copy("", "Test Clone Reference", user) as GeneralRiskAssessment;

            //Then
            Assert.That(result.Hazards.Count, Is.EqualTo(riskAssessment.Hazards.Count));
            Assert.That(result.Hazards.Select(x => x.Hazard.Id).First(), Is.EqualTo(riskAssessment.Hazards.Select(x => x.Hazard.Id).First()));
            Assert.That(result.Hazards.Select(x => x.Description).First(), Is.EqualTo(riskAssessment.Hazards.Select(x => x.Description).First()));
            Assert.That(result.Hazards.Select(x => x.Hazard.Id).Last(), Is.EqualTo(riskAssessment.Hazards.Select(x => x.Hazard.Id).Last()));
            Assert.That(result.Hazards.Select(x => x.Description).Last(), Is.EqualTo(riskAssessment.Hazards.Select(x => x.Description).Last()));

        }

        [Test]
        public void Given_riskassessment_with_documents_attached_Then_should_clone_correctly()
        {
            //Given
            var user = new UserForAuditing();
            var riskAssessment = GeneralRiskAssessment.Create("", "", 1, user);


            var document1 = new Mock<RiskAssessmentDocument>();
            document1.Setup(x => x.DocumentLibraryId).Returns(1);
            document1.Setup(x => x.CloneForRiskAssessmentTemplating(user)).Returns(document1.Object);

            var document2 = new Mock<RiskAssessmentDocument>();
            document2.Setup(x => x.DocumentLibraryId).Returns(2);
            document2.Setup(x => x.CloneForRiskAssessmentTemplating(user)).Returns(document2.Object);

            var documents = new List<RiskAssessmentDocument>
                                {
                                    document1.Object,
                                    document2.Object
                                };
            riskAssessment.AttachDocumentToRiskAssessment(documents, user);

            //When
            var result = riskAssessment.Copy("", "Test Clone Reference", user);

            //Then
            Assert.That(result.Documents.Count, Is.EqualTo(riskAssessment.Documents.Count));
            Assert.That(result.Documents.Select(x => x.DocumentLibraryId).First(), Is.EqualTo(riskAssessment.Documents.Select(x => x.DocumentLibraryId).First()));
            Assert.That(result.Documents.Select(x => x.DocumentLibraryId).Last(), Is.EqualTo(riskAssessment.Documents.Select(x => x.DocumentLibraryId).Last()));

        }
    }
}