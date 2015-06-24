using System;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.UserTests
{
    [TestFixture]
    [Category("Unit")]
    public class DeleteUserTests
    {
       
        [Test]
        public void Given_user_trying_to_delete_themselves_When_Delete_Then_should_throw_correct_exception()
        {
            // Given
            var user = new User {Id = Guid.NewGuid()};

            // When
            // Then
            Assert.Throws<UserAttemptingToDeleteSelfException>(() => user.Delete(new UserForAuditing()
                                                                                     {
                                                                                         Id = user.Id
                                                                                     }));
        }

        [Test]
        public void When_Delete_Then_should_set_correct_properties()
        {
            // Given
            var user = new User { Id = Guid.NewGuid() };
            var userDeleting = new UserForAuditing();

            // When
            user.Delete(userDeleting);

            // Then
            Assert.That(user.Deleted, Is.True);
            Assert.That(user.LastModifiedBy, Is.EqualTo(userDeleting));
            Assert.That(user.LastModifiedOn.Value.ToShortDateString(), Is.EqualTo(DateTime.Now.ToShortDateString()));
            Assert.That(user.DateDeleted.Value.ToShortDateString(), Is.EqualTo(DateTime.Now.ToShortDateString()));
        }
    }
}