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
    public class AddOtherEmailTests
    {
        [Test]
        public void Given_checklist_has_no_other_email_address_when_AddOtherEmails_then_added_to_list()
        {
            //Given
            var checklist = new BusinessSafe.Domain.Entities.SafeCheck.Checklist();

            var otherEmail = ChecklistOtherEmail.Create("abc@xyz.com", "name1");

            //When
            checklist.AddOtherEmailAddresses(otherEmail);

            //Then
            Assert.That(checklist.OtherEmails.Count(), Is.EqualTo(1));
            Assert.That(checklist.OtherEmails[0].Id, Is.Not.EqualTo(string.Empty));
            Assert.That(checklist.OtherEmails[0].Checklist, Is.EqualTo(checklist));
            Assert.That(checklist.OtherEmails[0].EmailAddress, Is.EqualTo(otherEmail.EmailAddress));
            Assert.That(checklist.OtherEmails[0].Name, Is.EqualTo(otherEmail.Name));
        }

        [Test]
        public void Given_checklist_has_other_email_address_when_AddOtherEmails_with_same_email_address_then_duplicate_not_added_to_list()
        {
            //Given
            var checklist = new BusinessSafe.Domain.Entities.SafeCheck.Checklist();

            var otherEmail = ChecklistOtherEmail.Create("abc@xyz.com", "name1");
         

            //When
            checklist.AddOtherEmailAddresses(otherEmail);
            checklist.AddOtherEmailAddresses(otherEmail);

            //Then
            Assert.That(checklist.OtherEmails.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Given_checklist_has_other_email_address_when_modify_and_AddOtherEmails_then_other_email_address_is_modified_in_list()
        {
            //Given
            var checklist = new BusinessSafe.Domain.Entities.SafeCheck.Checklist();
            
            var originalEmailAddress = "abc@xyz.com";
            var modifiedEmailAddress = "qwe@qwe.com";
            var name = "name1";

            var originalOtherEmail = ChecklistOtherEmail.Create(originalEmailAddress, name);
            
            var guid = originalOtherEmail.Id;
            var modifiedOtherEmail = ChecklistOtherEmail.Create(guid, modifiedEmailAddress, name);

            //When
            checklist.AddOtherEmailAddresses(originalOtherEmail);
            checklist.AddOtherEmailAddresses(modifiedOtherEmail);


            //Then
            Assert.That(checklist.OtherEmails.Count(), Is.EqualTo(1));
            Assert.That(checklist.OtherEmails[0].EmailAddress, Is.EqualTo(modifiedEmailAddress));
            Assert.That(checklist.OtherEmails[0].Name, Is.EqualTo(name));
        }

        [Test]
        public void Given_checklist_has_other_email_address_when_AddOtherEmails_where_email_exists_then_check_detatils_changed()
        {
            //Given
            var checklist = new BusinessSafe.Domain.Entities.SafeCheck.Checklist();

            var otherEmail = ChecklistOtherEmail.Create("abc@xyz.com", "name1");
            
            //When
            checklist.AddOtherEmailAddresses(otherEmail);

            var newEmail = "def@xyz.com";
            var newName = "Name2";
            
            // create new object to stop test refering to new object
            var newOtherEmail = ChecklistOtherEmail.Create(newEmail, newName);
            newOtherEmail.Id = otherEmail.Id;

            checklist.AddOtherEmailAddresses(newOtherEmail);

            //Then
            Assert.That(checklist.OtherEmails.Count(), Is.EqualTo(1));
            Assert.That(checklist.OtherEmails[0].EmailAddress, Is.EqualTo(newEmail));
            Assert.That(checklist.OtherEmails[0].Name, Is.EqualTo(newName));
        }
    }
}
