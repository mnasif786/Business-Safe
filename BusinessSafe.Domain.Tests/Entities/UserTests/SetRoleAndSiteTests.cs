using System;
using System.Collections.Generic;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;


namespace BusinessSafe.Domain.Tests.Entities.UserTests
{
    [TestFixture]
    [Category("Unit")]
    public class SetRoleAndSiteTests
    {
        [Test]
        public void Given_all_required_fields_are_available_When_Set_role_and_sites_is_called_Then_create_user_method_creates_an_object()
        {
            //Given
            long companyId = 999L;

            var oldRole = new Role
            {
                Id = Guid.NewGuid(),
                CompanyId = companyId
            };

            var newRole = new Role
            {
                Id = Guid.NewGuid(),
                CompanyId = companyId
            };

            var user = new UserForAuditing
                           {
                               Id = new Guid("B03C83EE-39F2-4F88-B4C4-7C276B1AAD99"),
                               CompanyId = companyId
                           };

            var employeeContactDetail = new EmployeeContactDetail { Email = "gary@green.com" };
            var employee = new Employee { Forename = "Gary", Surname = "Green", ContactDetails = new List<EmployeeContactDetail> { employeeContactDetail } };
            var userToUpdate = User.CreateUser(Guid.NewGuid(), companyId, oldRole, null, employee, user);
            var actioningUser = new UserForAuditing()
                                    {
                                        Id= Guid.NewGuid(),
                                        CompanyId = companyId
                                    };

            var site = new Site
                           {
                               Id = 1,
                               ClientId = companyId
                           };

            //When
            userToUpdate.SetRoleAndSite(newRole, site, actioningUser);

            //Then
            Assert.AreEqual(newRole, userToUpdate.Role);
            Assert.AreEqual(site, userToUpdate.Site);
            Assert.AreEqual(actioningUser, userToUpdate.LastModifiedBy);
        }

        [Test]
        [ExpectedException(typeof(CompanyMismatchException<User, Role>))]
        public void Given_role_is_not_same_company_as_user_When_Set_role_and_sites_is_called_Then_correct_excpetion_is_thrown()
        {
            //Given
            long companyId = 999L;

            var oldRole = new Role
            {
                Id = Guid.NewGuid(),
                CompanyId = companyId
            };

            var newRole = new Role
            {
                Id = Guid.NewGuid(),
                CompanyId = 333L
            };

            var user = new UserForAuditing
                           {
                               Id = new Guid("B03C83EE-39F2-4F88-B4C4-7C276B1AAD99"),
                               CompanyId = companyId
                           };

            var employeeContactDetail = new EmployeeContactDetail { Email = "gary@green.com" };
            var employee = new Employee { Forename = "Gary", Surname = "Green", ContactDetails = new List<EmployeeContactDetail> { employeeContactDetail } };
            var userToUpdate = User.CreateUser(Guid.NewGuid(), companyId, oldRole, null, employee, user);
            var actioningUser = new UserForAuditing()
                                    {
                                        Id = Guid.NewGuid(),
                                        CompanyId = companyId
                                    };

            //Todo: finish when sites are refactored.
            var site = new Site
                           {
                               Id = 1,
                               ClientId = companyId
                           };

            //When
            userToUpdate.SetRoleAndSite(newRole, site, actioningUser);

            //Then exception is thrown.
        }

        [Test]
        [ExpectedException(typeof(CompanyMismatchException<User, SiteStructureElement>))]
        public void Given_site_is_not_same_company_as_user_When_Set_role_and_sites_is_called_Then_correct_excpetion_is_thrown()
        {
            //Given
            long companyId = 999L;

            var oldRole = new Role
            {
                Id = Guid.NewGuid(),
                CompanyId = companyId
            };

            var newRole = new Role
            {
                Id = Guid.NewGuid(),
                CompanyId = companyId
            };

            var user = new UserForAuditing
            {
                Id = new Guid("B03C83EE-39F2-4F88-B4C4-7C276B1AAD99"),
                CompanyId = companyId
            };

            var employeeContactDetail = new EmployeeContactDetail { Email = "gary@green.com" };
            var employee = new Employee { Forename = "Gary", Surname = "Green", ContactDetails = new List<EmployeeContactDetail> { employeeContactDetail } };
            var userToUpdate = User.CreateUser(Guid.NewGuid(), companyId, oldRole, null, employee, user);
            var actioningUser = new UserForAuditing()
                                    {
                                        Id = Guid.NewGuid(),
                                        CompanyId = companyId
                                    };

            //Todo: finish when sites are refactored.
            var site = new Site
            {
                Id = 1,
                ClientId = 333L
            };

            //When
            userToUpdate.SetRoleAndSite(newRole, site, actioningUser);

            //Then exception is thrown.
        }
    }
}
