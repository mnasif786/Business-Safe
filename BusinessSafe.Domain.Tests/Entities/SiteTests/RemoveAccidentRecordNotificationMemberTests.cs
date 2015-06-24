using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.SiteTests
{

    [TestFixture]
    [Category("Unit")]
    public class RemoveAccidentRecordNotificationMemberTests
    {
        [Test]
        public void Given_employee_is_not_deleted_in_list_when_RemoveEmployee_to_distribution_list_then_record_is_marked_as_delete()
        {
            //GIVEN
            var site = new SiteStructureElementForTesting() { Id = 123123 };
            var employee = new Employee() { Id = Guid.NewGuid() };
            var accidentRecordNotificationMember = new AccidentRecordNotificationEmployeeMember
            {
                Deleted = false,
                Site = site,
                Employee = employee
            };
            site.ProtectedAccidentRecordNotificationMembers = new List<AccidentRecordNotificationMember>() { accidentRecordNotificationMember };

            //WHEN
            site.RemoveAccidentRecordNotificationMember(employee, new UserForAuditing());

            //THEN
            Assert.That(site.ProtectedAccidentRecordNotificationMembers.Count, Is.EqualTo(1));
            Assert.That(site.ProtectedAccidentRecordNotificationMembers[0].Deleted, Is.True);
        }

        [Test]
        public void Given_employee_is_not_in_list_when_RemoveEmployee_to_distribution_list_then_list_is_not_changed()
        {
            //GIVEN
            var site = new SiteStructureElementForTesting() { Id = 123123 };
            var employee = new Employee() { Id = Guid.NewGuid() };
            var accidentRecordNotificationMember = new AccidentRecordNotificationEmployeeMember
            {
                Deleted = false,
                Site = site,
                Employee = employee
            };
            site.ProtectedAccidentRecordNotificationMembers = new List<AccidentRecordNotificationMember>() { accidentRecordNotificationMember };

            //WHEN
            site.RemoveAccidentRecordNotificationMember(new Employee(){Id = Guid.NewGuid()}, new UserForAuditing());

            //THEN
            Assert.That(site.AccidentRecordNotificationMembers.Count, Is.EqualTo(1));
        }
    }
}
