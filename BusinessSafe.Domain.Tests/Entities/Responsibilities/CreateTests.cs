using System;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.ParameterClasses;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.Responsibilities
{
    [TestFixture]
    public class CreateTests
    {

        [Test]
        public void When_Create_Then_Return_Responsibility()
        {
            // Given
            var category = new ResponsibilityCategory();
            const string title = "R1";
            const string description = "R Test";
            var site = new Site();
            var reason = new ResponsibilityReason();
            var owner = new Employee ();
            const TaskReoccurringType frequency = TaskReoccurringType.Weekly;
            var user = new UserForAuditing();
            var template = new StatutoryResponsibilityTemplate();
        
            // When
            var result = Responsibility.Create(default(long),
                category,
                title,
                description,
                site,
                reason,
                owner,
                frequency, template,
                user
                );

            // Then

            Assert.AreEqual(category,result.ResponsibilityCategory);
            Assert.AreEqual(title,result.Title);
            Assert.AreEqual(description,result.Description);
            Assert.AreEqual(site,result.Site);
            Assert.AreEqual(reason,result.ResponsibilityReason);
            Assert.AreEqual(owner,result.Owner);
            Assert.AreEqual(frequency,result.InitialTaskReoccurringType);
            Assert.AreEqual(user,result.CreatedBy);
            Assert.AreEqual(template, result.StatutoryResponsibilityTemplateCreatedFrom);
            Assert.That(result, Is.InstanceOf<Responsibility>());
        }

    }
}