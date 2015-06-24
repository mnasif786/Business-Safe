using System;
using System.Collections.Generic;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RoleTests
{
    [TestFixture]
    [Category("Unit")]
    public class MarkAsDeleteTests
    {
        [Test]
        public void Given_userrole_When_mark_for_delete_Then_employee_properties_should_be_correct()
        {
            //Given
            var role = new Role(){CompanyId = 1};
            var userDeletingEmployee = new UserForAuditing();

            //When
            role.MarkForDelete(userDeletingEmployee);

            //Then
            Assert.That(role.Deleted, Is.True);
            Assert.That(role.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
            Assert.That(role.LastModifiedBy, Is.EqualTo(userDeletingEmployee));
        }

        [Test]
        public void Given_system_role_When_mark_for_delete_Then_should_throw_the_correct_exception()
        {
            //Given
            var result = new Role {CompanyId = 0};

            //When
            //Then
            Assert.Throws<AttemptingToDeleteSystemRoleException>(() => result.MarkForDelete(It.IsAny<UserForAuditing>()));
        }

        [Test]
        public void Given_role_is_currently_being_used_by_users_When_mark_for_delete_Then_should_throw_the_correct_exception()
        {
            //Given
            var result = new Role { CompanyId = 1 };
            result.Users = new List<User>()
                               {
                                   new User()
                                       {
                                           Role = result
                                       }
                               };


            //When
            //Then
            Assert.Throws<AttemptingToDeleteRoleCurrentlyUsedByUsersException>(() => result.MarkForDelete(It.IsAny<UserForAuditing>()));
        }

        [Test]
        public void Given_role_is_currently_being_used_by_a_deleted_users_When_mark_for_delete_Then_should_be_marked_for_deleted()
        {
            //Given
            var result = new Role { CompanyId = 1 };
            result.Users = new List<User>(){new User(){Role = result, Deleted = true}};

            //When
            Assert.DoesNotThrow(() => result.MarkForDelete(It.IsAny<UserForAuditing>()));

            //Then
            Assert.That(result.Deleted, Is.True);

        }
    }
}