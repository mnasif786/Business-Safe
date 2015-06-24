using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.EmployeeChecklistsTests
{
    [TestFixture]
    public class ValidateCompleteTests
    {
        [Test]
        public void Given_an_EmployeeChecklist_is_already_completed_When_I_try_to_complete_it_again_Then_I_get_a_validation_error()
        {
            //Given
            var employeeChecklist = new EmployeeChecklist
                                        {
                                            CompletedDate = DateTime.Now
                                        };

            //When
            var messages = employeeChecklist.ValidateComplete();

            //Then
            Assert.That(messages.Count, Is.EqualTo(1));
            Assert.That(messages[0].Text, Is.EqualTo("This checklist has already been completed once and cannot be resubmitted."));
        }
    }
}
