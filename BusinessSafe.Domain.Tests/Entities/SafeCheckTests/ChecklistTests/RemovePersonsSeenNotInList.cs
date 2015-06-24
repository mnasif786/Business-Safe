using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.Entities.SafeCheck;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.SafeCheckTests.ChecklistTests
{
    [TestFixture]
    public class RemovePersonsSeenNotInList
    {
        [Test]
        public void Given_a_non_employee_is_a_persons_seen_to_be_removed_when_RemovePersonsSeenNotInList_then_removed_from_persons_seen_list()
        {
            //GIVEN
            var personSeenToRemove = ChecklistPersonSeen.Create(Guid.NewGuid(),"Diana Smith","ds@test.com");
            var personSeen = ChecklistPersonSeen.Create(Guid.NewGuid(),"Petyr Littlefinger","pl@test.com");
            var personSeenEmployee = ChecklistPersonSeen.Create(new Employee(){Id = Guid.NewGuid()});
            var checklist = new BusinessSafe.Domain.Entities.SafeCheck.Checklist();

            checklist.AddPersonSeen(personSeen);
            checklist.AddPersonSeen(personSeenToRemove);
            checklist.AddPersonSeen(personSeenEmployee);

            //WHEN
            checklist.RemovePersonsSeenNotInList(new List<ChecklistPersonSeen>() { personSeen, personSeenEmployee });

            //THEN
            Assert.That(checklist.PersonsSeen.Count, Is.EqualTo(2));

        }

        [Test]
        public void Given_an_employee_is_a_persons_seen_to_be_removed_when_RemovePersonsSeenNotInList_then_removed_from_persons_seen_list()
        {
            //GIVEN
            var personSeen1 = ChecklistPersonSeen.Create(Guid.NewGuid(), "Diana Smith", "ds@test.com");
            var personSeen2 = ChecklistPersonSeen.Create(Guid.NewGuid(), "Petyr Littlefinger", "pl@test.com");
            var personSeenEmployee = ChecklistPersonSeen.Create(new Employee() { Id = Guid.NewGuid() });
            var checklist = new BusinessSafe.Domain.Entities.SafeCheck.Checklist();

            checklist.AddPersonSeen(personSeen1);
            checklist.AddPersonSeen(personSeen2);
            checklist.AddPersonSeen(personSeenEmployee);

            //WHEN
            checklist.RemovePersonsSeenNotInList(new List<ChecklistPersonSeen>() { personSeen1, personSeen2 });

            //THEN
            Assert.That(checklist.PersonsSeen.Count, Is.EqualTo(2));

        }


        
    
    }
}

