using System;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.PeopleAtRiskTests
{
    [TestFixture]
    [Category("Unit")]
    public class MarkForDeleteTests
    {
        UserForAuditing _user = new UserForAuditing();

        [Test]
        public void Given_mark_for_delete_Then_hazard_updates_all_required_fields()
        {
            //Given
            var result = PeopleAtRisk.Create("original name", 100, null, new UserForAuditing());

            //When
            result.MarkForDelete(_user);

            //Then
            Assert.That(result.Deleted, Is.True);
            Assert.That(result.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
            Assert.That(result.LastModifiedBy, Is.EqualTo(_user));
        }

        [Test]
        public void Given_system_default_hazard_When_mark_for_delete_Then_should_throw_the_correct_exception()
        {
            //Given
            var result = new MyPeopleAtRisk();
            result.MarkPeopleAtRiskAsSystemDefault();

            //When
            //Then
            Assert.Throws<AttemptingToDeleteSystemDefaultException>(() => result.MarkForDelete(_user));
        }
    }
}