using System;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.TaskDocumentTests
{
    [TestFixture]
    [Category("Unit")]
    public class MarkForDeleteTests
    {
        [Test]
        public void When_mark_for_delete_Then_should_set_properties_correctly()
        {
            //Given
            var task = new Domain.Entities.TaskDocument();
            var user = new UserForAuditing();

            //When
            task.MarkForDelete(user);

            //Then
            Assert.That(task.Deleted, Is.True);
            Assert.That(task.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
            Assert.That(task.LastModifiedBy, Is.EqualTo(user));
        }

    }
}