using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.ParameterClasses.SafeCheck;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.SafeCheckTests.ChecklistTests
{
    [TestFixture]
    class CreateChecklistTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void When_Create_Then_Return_Checklist()
        {
            // Given
            var parameters = new CreateUpdateChecklistParameters()
            {
                SiteId = 11,
                ClientId = 22,
                CoveringLetterContent = "coveringLetterContent",
                Status = "status",
                Jurisdiction = "UK"
            };


            // When
            var result = Checklist.Create(parameters);

            // Then
            Assert.That(result, Is.InstanceOf<Checklist>());
            Assert.That(result.SiteId, Is.EqualTo(11));
            Assert.That(result.ClientId, Is.EqualTo(22));
            Assert.That(result.CoveringLetterContent, Is.EqualTo("coveringLetterContent"));
            Assert.That(result.Status, Is.EqualTo("status"));
            Assert.That(result.Jurisdiction, Is.EqualTo("UK"));
        }
    }
}
