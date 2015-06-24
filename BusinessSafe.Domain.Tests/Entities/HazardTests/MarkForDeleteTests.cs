using System;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.HazardTests
{
    [TestFixture]
    [Category("Unit")]
    public class MarkForDeleteTests
    {
        [Test]
        public void Given_mark_for_delete_Then_hazard_updates_all_required_fields()
        {
            //Given
            var user  = new UserForAuditing();
            var result = Hazard.Create("original name", 100, user,null, null);

            //When
            result.MarkForDelete(user);

            //Then
            Assert.That(result.LastModifiedBy, Is.EqualTo(user));
            Assert.That(result.Deleted, Is.True);
            Assert.That(result.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
        }

        [Test]
        public void Given_system_default_hazard_When_mark_for_delete_Then_should_throw_the_correct_exception()
        {
            //Given
            var result = new MyHazard();
            result.MarkHazardAsSystemDefault();

            //When
            //Then
            Assert.Throws<AttemptingToDeleteSystemDefaultException>(() => result.MarkForDelete(It.IsAny<UserForAuditing>()));
        }
    }
}