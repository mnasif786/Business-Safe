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
    public class RemovePersonSeenTests
    {

        [Test]
        public void Given_person_seen_is_an_employee_and_in_list_when_RemovePersonSeen_then_removed_from_list()
        {
            //GIVEN
            var checklist = new BusinessSafe.Domain.Entities.SafeCheck.Checklist();

            var employee = new Employee();
            employee.Id = Guid.NewGuid();
            employee.Forename = "Alester";
            employee.Surname = "Oakheart";
            employee.SetEmail("text@testeros.com",null);

            var personSeenToRemove = ChecklistPersonSeen.Create(employee);
            var personSeen = ChecklistPersonSeen.Create(employee);
            checklist.AddPersonSeen(personSeen);

            //WHEN
            checklist.RemovePersonSeen(personSeenToRemove);


            //THEN
            Assert.That(checklist.PersonsSeen.Count, Is.EqualTo(0));
        }


        [Test]
        public void Given_person_seen_is_not_an_employee_and_not_in_list_when_AddPersonSeen_then_added_to_list()
        {
            //GIVEN
            var checklist = new BusinessSafe.Domain.Entities.SafeCheck.Checklist();
            var personSeen = ChecklistPersonSeen.Create(Guid.NewGuid(), "Alester Oakheart", "test@tester.com");
            var personSeenToRemove = ChecklistPersonSeen.Create(personSeen.Id, "Alester Oakheart", "test@tester.com");
            checklist.AddPersonSeen(personSeen);

            //WHEN
            checklist.RemovePersonSeen(personSeenToRemove);

            //THEN
            Assert.That(checklist.PersonsSeen.Count, Is.EqualTo(0));
        }
    
    }
}

