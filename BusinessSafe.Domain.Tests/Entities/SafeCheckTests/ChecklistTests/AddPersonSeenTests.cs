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
    public class AddPersonSeenTests
    {

        [Test]
        public void Given_person_seen_is_an_employee_and_not_in_list_when_AddPersonSeen_then_added_to_list()
        {
            //GIVEN
            var checklist = new BusinessSafe.Domain.Entities.SafeCheck.Checklist();

            var employee = new Employee();
            employee.Id = Guid.NewGuid();
            employee.Forename = "Alester";
            employee.Surname = "Oakheart";
            employee.SetEmail("text@testeros.com",null);

            var personSeen = ChecklistPersonSeen.Create(employee);

            //WHEN
            checklist.AddPersonSeen(personSeen);


            //THEN
            Assert.That(checklist.PersonsSeen.Count, Is.EqualTo(1));
            Assert.That(checklist.PersonsSeen[0].Id, Is.Not.EqualTo(string.Empty));
            Assert.That(checklist.PersonsSeen[0].Employee.Id, Is.EqualTo(employee.Id));
            Assert.That(checklist.PersonsSeen[0].FullName, Is.EqualTo(employee.FullName));
            Assert.That(checklist.PersonsSeen[0].EmailAddress, Is.EqualTo(employee.GetEmail()));
            Assert.That(checklist.PersonsSeen[0].Checklist, Is.EqualTo(checklist));

        }

        [Test]
        public void Given_person_seen_is_an_employee_and_is_in_list_when_AddPersonSeen_then_duplicate_not_added_to_list()
        {
            //GIVEN
            var checklist = new BusinessSafe.Domain.Entities.SafeCheck.Checklist();

            var employee = new Employee();
            employee.Id = Guid.NewGuid();
            employee.Forename = "Alester";
            employee.Surname = "Oakheart";
            employee.SetEmail("text@testeros.com", null);

            var personSeen = ChecklistPersonSeen.Create(employee);

            //WHEN
            checklist.AddPersonSeen(personSeen);
            checklist.AddPersonSeen(personSeen);

            //THEN
            Assert.That(checklist.PersonsSeen.Count, Is.EqualTo(1));

        }

        [Test]
        public void Given_person_seen_is_not_an_employee_and_not_in_list_when_AddPersonSeen_then_added_to_list()
        {
            //GIVEN
            var checklist = new BusinessSafe.Domain.Entities.SafeCheck.Checklist();

            var personSeen = ChecklistPersonSeen.Create(Guid.NewGuid(),"Alester Oakheart","test@tester.com");

            //WHEN
            checklist.AddPersonSeen(personSeen);

            //THEN
            Assert.That(checklist.PersonsSeen.Count, Is.EqualTo(1));
            Assert.That(checklist.PersonsSeen[0].Id, Is.Not.EqualTo(string.Empty));
            Assert.That(checklist.PersonsSeen[0].Checklist, Is.EqualTo(checklist));
        }

        [Test]
        public void Given_person_seen_is_not_an_employee_and_in_list_when_AddPersonSeen_then_not_duplicated()
        {
            //GIVEN
            var checklist = new BusinessSafe.Domain.Entities.SafeCheck.Checklist();

            var personSeen = ChecklistPersonSeen.Create(Guid.NewGuid(), "Alester Oakheart", "test@tester.com");

            //WHEN
            checklist.AddPersonSeen(personSeen);
            checklist.AddPersonSeen(personSeen);

            //THEN
            Assert.That(checklist.PersonsSeen.Count, Is.EqualTo(1));

        }
    }
}

