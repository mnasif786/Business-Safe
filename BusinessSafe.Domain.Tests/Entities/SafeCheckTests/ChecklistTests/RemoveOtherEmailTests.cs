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
    public class RemoveOtherEmailTests
    {
        [Test]
        public void Given_checklist_contain_Other_Email_to_be_removed_when_UpdateOtherEmailsList_then_removed_from_the_list()
        {
            //Given
            var checklist = new BusinessSafe.Domain.Entities.SafeCheck.Checklist();

            var otherEmail1 = ChecklistOtherEmail.Create(Guid.NewGuid(), "abc@xyz.com", "name1");
            var otherEmail2 = ChecklistOtherEmail.Create(Guid.NewGuid(), "abc@sky.com", "name1");
            var otherEmail3 = ChecklistOtherEmail.Create(Guid.NewGuid(), "abc@bbc.com", "name1");

            //When
            checklist.AddOtherEmailAddresses(otherEmail1);
            checklist.AddOtherEmailAddresses(otherEmail2);
            checklist.AddOtherEmailAddresses(otherEmail3);

            checklist.UpdateOtherEmailsList(new List<ChecklistOtherEmail>() { otherEmail1 });


            //Then
            Assert.That(checklist.OtherEmails.Count(), Is.EqualTo(1));
        }
    }
}
