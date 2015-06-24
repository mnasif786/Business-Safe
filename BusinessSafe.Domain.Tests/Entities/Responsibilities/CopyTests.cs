using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.Responsibilities
{

    [TestFixture]
    public class CopyTests
    {
        private Employee _employee;
        private const long _originalResponsibilityId = 1234L;
        private const long _newResponsibilityId = 5555L;
        private const long _companyId = 5678L;
        private Responsibility _originalResponibility;
        private Site _site;
        private readonly DateTime _createdOn = DateTime.Now;
        private UserForAuditing _user;

        [SetUp]
        public void Setup()
        {
            _site = new Site() { Id = 311 };
            _employee = new Employee() { Id = Guid.NewGuid() };

            _originalResponibility = new Responsibility()
            {
                CompanyId = _companyId,
                ResponsibilityCategory = ResponsibilityCategory.Create(1, "Category"),
                Title = "Title Orig",
                Description = "Description",
                Site = new Site() { Id = 1 },
                ResponsibilityReason = new ResponsibilityReason() { Id = 1 },
                Owner = _employee,
                InitialTaskReoccurringType = TaskReoccurringType.ThreeMonthly,
                ResponsibilityTasks = new List<ResponsibilityTask>() { 
                    new ResponsibilityTask() { Id = 1, Title = "1", Site = new Site() { Id = 1}, TaskAssignedTo = new Employee() { Id = Guid.NewGuid()}},
                    new ResponsibilityTask() { Id = 2, Title = "1", Site = new Site() { Id = 1}, TaskAssignedTo = new Employee() { Id = Guid.NewGuid()}},
                    new ResponsibilityTask() { Id = 3, Title = "1", Site = new Site() { Id = 1}, TaskAssignedTo = new Employee() { Id = Guid.NewGuid()}}},
                CreatedOn = _createdOn,
                StatutoryResponsibilityTemplateCreatedFrom = new StatutoryResponsibilityTemplate() { Id = 2 },
                Id = _originalResponsibilityId
            };
            
            _user = new UserForAuditing() {CompanyId = _companyId, Id = Guid.NewGuid()};
        }

        [Test]
        public void Given_ResponsibilityId_Found_When_Copy_Responsibility_Then_Check_New_Responsibility_Created_With_Originals_Parameters()
        {
            // When
            var newResponsibility = _originalResponibility.CopyWithoutSiteAndOwner("Title", _user);

            // Then
            Assert.True(newResponsibility.CompanyId.Equals(_originalResponibility.CompanyId));
            Assert.True(newResponsibility.ResponsibilityCategory.Equals(_originalResponibility.ResponsibilityCategory));
            Assert.False(newResponsibility.Title.Equals(_originalResponibility.Title));
            Assert.True(newResponsibility.Description.Equals(_originalResponibility.Description));
            Assert.IsNull(newResponsibility.Site);
            Assert.IsNull(newResponsibility.Owner);
            Assert.True(newResponsibility.InitialTaskReoccurringType.Equals(_originalResponibility.InitialTaskReoccurringType));
            Assert.True(newResponsibility.ResponsibilityTasks.Count.Equals(_originalResponibility.ResponsibilityTasks.Count));
            Assert.True(newResponsibility.StatutoryResponsibilityTemplateCreatedFrom.Equals(_originalResponibility.StatutoryResponsibilityTemplateCreatedFrom));

            for (int i = 0; i < newResponsibility.ResponsibilityTasks.Count; i++ )
            {
                var task = newResponsibility.ResponsibilityTasks[i];
                var origTask = _originalResponibility.ResponsibilityTasks[i];
                
                Assert.IsTrue(task.Title.Equals(origTask.Title));
                Assert.IsNull(task.TaskAssignedTo);
                Assert.IsNull(task.Site);
            }
        }

    }
}
