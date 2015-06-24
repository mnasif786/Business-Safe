using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.UserTests
{
    [TestFixture]
    [Category("Unit")]
    public class CreateTests
    {
        [Test]
        public void Given_employee_when_create_user_then_returned_user_employee_are_equal()
        {
            //given
            var employee = new Employee
                               {
                                   Id = new Guid()
                               };

            //when

            var user = User.CreateUser(new Guid(),
                                       default(long),
                                       new Role(),
                                       new Site(),
                                       employee,
                                       new UserForAuditing()
                );


            //then
            Assert.AreSame(user.Employee, employee);
        }

        [Test]
        public void Given_employee_has_user_when_create_user_then_returned_user_employee_are_not_equal()
        {
            //given
            var employee = new Employee
            {
                Id = new Guid()
            };

            //when
            var user = User.CreateUser(new Guid(),
                                       default(long),
                                       new Role(),
                                       new Site(),
                                       new Employee{Id = new Guid()},
                                       new UserForAuditing()
                );


            //then
            Assert.AreNotSame(user.Employee, employee);
        }
    }
}
