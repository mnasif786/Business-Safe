using System;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.AnswerTests
{
    [TestFixture]
    [Category("Unit")]
    public class UpdateTests
    {
        [Test]
        public void Given_all_required_fields_are_available_Then_update_answer_method_updates_object()
        {
            //Given
            var employeeChecklist = new EmployeeChecklist();
            var question = new Question();
            bool? booleanResponse = false;
            var user = new UserForAuditing();
            string additionalInfo = "Additional Info";

            var answer = PersonalAnswer.Create(employeeChecklist, question, booleanResponse, additionalInfo, user);

            //When
            answer.Update(true,"New Additional Info", user);

            //Then
            Assert.That(answer.BooleanResponse, Is.EqualTo(true));
            Assert.That(answer.AdditionalInfo, Is.EqualTo("New Additional Info"));
            Assert.That(answer.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
            Assert.That(answer.LastModifiedBy, Is.EqualTo(user));
        }
    }
}