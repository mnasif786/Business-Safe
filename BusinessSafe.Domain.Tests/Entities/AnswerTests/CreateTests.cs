using System;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.AnswerTests
{
    [TestFixture]
    [Category("Unit")]
    public class CreateTests
    {
        [Test]
        public void Given_all_required_fields_are_available_Then_create_answer_method_creates_an_object()
        {
            //Given
            var employeeChecklist = new EmployeeChecklist();
            var question = new Question();
            bool? booleanResponse = false;
            var user = new UserForAuditing();
            string additionalInfo = "Additional Info";

            //When
            var result = PersonalAnswer.Create(employeeChecklist, question, booleanResponse,additionalInfo, user);

            //Then
            Assert.That(result.BooleanResponse, Is.EqualTo(booleanResponse));
            Assert.That(result.AdditionalInfo, Is.EqualTo(additionalInfo));
            Assert.That(result.CreatedOn.Value.Date, Is.EqualTo(DateTime.Today));
            Assert.That(result.CreatedBy, Is.EqualTo(user));
            Assert.That(result.EmployeeChecklist, Is.EqualTo(employeeChecklist));
            Assert.That(result.Question, Is.EqualTo(question));
        }
    }
}
