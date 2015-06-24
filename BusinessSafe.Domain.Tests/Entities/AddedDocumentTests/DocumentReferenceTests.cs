using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.AddedDocumentTests
{
    [TestFixture]
    [Category("Unit")]
    public class DocumentReferenceTests
    {
        [Test]
        public void When_GetDocumentReference_Then_should_return_correct_result()
        {
            //Given
            //When
            var result = new AddedDocument().DocumentReference;

            //Then
            Assert.That(result, Is.EqualTo(string.Empty));
            
        }
    }
}