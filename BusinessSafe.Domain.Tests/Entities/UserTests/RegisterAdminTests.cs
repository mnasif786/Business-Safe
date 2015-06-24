using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Entities;

using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.UserTests
{
    [TestFixture]
    [Category("Unit")]
    public class RegisterAdminTests
    {
        [Test]
        public void Given_all_required_fields_are_available_Then_create_user_method_creates_an_object()
        {
            //Given
            Guid userId = Guid.NewGuid();
            const long companyId = 33749;
            const long siteId = 324234L;
            var role = new Role() { Id = new Guid("BACF7C01-D210-4DBC-942F-15D8456D3B92") };
            var user = new UserForAuditing() { Id = new Guid("B03C83EE-39F2-4F88-B4C4-7C276B1AAD99") };
            var site = new Site() {Id = siteId};
            var employeeContactDetail = new EmployeeContactDetail {Email = "gary@green.com"};
            var employee = new Employee {Forename = "Gary", Surname = "Green",ContactDetails = new List<EmployeeContactDetail> {employeeContactDetail}};

            //When
            var result = User.CreateUser(userId, companyId, role, site, employee, user);

            //Then
            Assert.That(result.Id, Is.EqualTo(userId));
            Assert.That(result.CompanyId, Is.EqualTo(companyId));
            Assert.That(result.Site.Id, Is.EqualTo(siteId));
            Assert.That(result.Employee, Is.Not.Null);
            Assert.That(result.Employee.Forename, Is.EqualTo("Gary"));
            Assert.That(result.Employee.Surname, Is.EqualTo("Green"));
            Assert.That(result.Employee.ContactDetails, Is.Not.Null);
            Assert.That(result.Employee.ContactDetails.Count, Is.EqualTo(1));
            Assert.That(result.Employee.ContactDetails[0].Email, Is.EqualTo("gary@green.com"));
        }
    }
}
