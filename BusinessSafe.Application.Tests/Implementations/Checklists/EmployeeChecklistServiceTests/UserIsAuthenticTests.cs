using System;

using BusinessSafe.Application.Request.Checklists;
using BusinessSafe.Domain.Entities;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.Checklists.EmployeeChecklistServiceTests
{
    [TestFixture]
    public class UserIsAuthenticTests : BaseEmployeeServiceTests
    {
        [SetUp]
        public void Setup()
        {
            base.Setup();
        }

        [Test]
        [TestCase("password")]
        [TestCase("abc")]
        [TestCase("def")]
        public void return_true_if_password_matches(string password)
        {
            // Given
            _target = GetTarget();
            _employeeChecklistrepo
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(new EmployeeChecklist()
                         {
                             Password = password
                         }
                 );

            // When
            var result = _target.UserIsAuthentic(new AuthenticateUserRequest()
            {
                ChecklistId = It.IsAny<Guid>(),
                Password = password
            });

            // Then
            Assert.IsTrue(result);
        }

        [Test]
        [TestCase("password")]
        [TestCase("abc")]
        [TestCase("def")]
        public void return_false_if_password_doesnt_match(string password)
        {
            // Given
            _target = GetTarget();
            _employeeChecklistrepo
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(new EmployeeChecklist()
                {
                    Password = "password that you'll never match"
                }
                 );

            // When
            var result = _target.UserIsAuthentic(new AuthenticateUserRequest()
            {
                ChecklistId = It.IsAny<Guid>(),
                Password = password
            });

            // Then
            Assert.IsFalse(result);
        }
    }
}
