using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.SiteTests
{
    public class SiteStructureElementForTesting : Site
    {
        public IList<AccidentRecordNotificationMember> ProtectedAccidentRecordNotificationMembers
        {
            get { return _accidentRecordNotificationMembers; }
            set { _accidentRecordNotificationMembers = value; }
        }
    }

    [TestFixture]
    [Category("Unit")]
    public class AddAccidentRecordNotificationMemberTests
    {

        [Test]
        public void Given_employee_not_in_list_when_AddEmployee_to_distribution_list_then_employee_is_added()
        {
            //GIVEN
            var site = new Site(){Id= 123123};
            var employee = new Employee() {Id = Guid.NewGuid()};

            //WHEN
            site.AddAccidentRecordNotificationMember(employee, new UserForAuditing() );

            //THEN
            Assert.That(site.AccidentRecordNotificationMembers.Count,Is.EqualTo(1));
            Assert.That(site.AccidentRecordNotificationMembers[0].Site, Is.EqualTo(site));
            Assert.That( ( (AccidentRecordNotificationEmployeeMember)site.AccidentRecordNotificationMembers[0] ).Employee, Is.EqualTo(employee));
        }

        [Test]
        public void Given_employee_is_in_list_when_AddEmployee_to_distribution_list_then_employee_is_not_duplicated()
        {
            //GIVEN
            var site = new Site() { Id = 123123 };
            var employee = new Employee() { Id = Guid.NewGuid() };

            //WHEN
            site.AddAccidentRecordNotificationMember(employee, new UserForAuditing());
            site.AddAccidentRecordNotificationMember(employee, new UserForAuditing());

            //THEN
            Assert.That(site.AccidentRecordNotificationMembers.Count, Is.EqualTo(1));
        }

        [Test]
        public void Given_employee_is_deleted_in_list_when_AddEmployee_to_distribution_list_then_employee_is_not_duplicated_and_undeleted()
        {
            //GIVEN
            var site = new SiteStructureElementForTesting() {Id = 123123};
            var employee = new Employee() {Id = Guid.NewGuid()};
            var accidentRecordNotificationMember = new AccidentRecordNotificationEmployeeMember
                                                       {
                                                           Deleted = true,
                                                           Site = site,
                                                           Employee = employee
                                                       };
            site.ProtectedAccidentRecordNotificationMembers = new List<AccidentRecordNotificationMember>(){accidentRecordNotificationMember};
            
            //WHEN
            site.AddAccidentRecordNotificationMember(employee, new UserForAuditing());

            //THEN
            Assert.That(site.ProtectedAccidentRecordNotificationMembers.Count, Is.EqualTo(1));
            Assert.That(site.AccidentRecordNotificationMembers[0].Deleted, Is.False);
        }
    }
}
