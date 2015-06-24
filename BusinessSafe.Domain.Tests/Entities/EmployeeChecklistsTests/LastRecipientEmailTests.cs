using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.EmployeeChecklistsTests
{
    [TestFixture]
    public class LastRecipientEmailTests
    {
        [Test]
        public void Given_only_one_recipient_When_LastRecipientEmail_called_returns_expected_email()
        {
            var emailAddress = "Test1@test.com";

            var employeeChecklist = new EmployeeChecklist
                                        {
                                            EmployeeChecklistEmails = new List<EmployeeChecklistEmail>
                                                                          {
                                                                              new EmployeeChecklistEmail
                                                                                  {
                                                                                      RecipientEmail = emailAddress
                                                                                  }
                                                                          }
                                        };

            Assert.That(employeeChecklist.LastRecipientEmail, Is.EqualTo(emailAddress));
        }

        [Test]
        public void Given_many_recipient_When_LastRecipientEmail_called_returns_latest_email()
        {
            var emailAddress1 = "Test1@test.com";
            var emailAddress2 = "frank.sinatra@gmail.com";
            var emailAddress3 = "dean.martin@hotmail.co.uk";

            var now = DateTime.Now;

            var employeeChecklist = new EmployeeChecklist
                                        {
                                            EmployeeChecklistEmails = new List<EmployeeChecklistEmail>
                                                                          {
                                                                              new EmployeeChecklistEmail
                                                                                  {
                                                                                      RecipientEmail = emailAddress1,
                                                                                      CreatedOn = now.AddDays(-7)
                                                                                  },
                                                                              new EmployeeChecklistEmail
                                                                                  {
                                                                                      RecipientEmail = emailAddress2,
                                                                                      CreatedOn = now.AddDays(-3)
                                                                                  },
                                                                              new EmployeeChecklistEmail
                                                                                  {
                                                                                      RecipientEmail = emailAddress3,
                                                                                      CreatedOn = now.AddDays(-1)
                                                                                  }
                                                                          }
                                        };

            Assert.That(employeeChecklist.LastRecipientEmail, Is.EqualTo(emailAddress3));
        }

        [Test]
        public void Given_null_recipients_When_LastRecipientEmail_called_Then_returns_null()
        {
            var employeeChecklist = new EmployeeChecklist();
            Assert.That(employeeChecklist.LastRecipientEmail, Is.Null);
        }

        [Test]
        public void Given_no_recipients_When_LastRecipientEmail_called_Then_returns_null()
        {
            var employeeChecklist = new EmployeeChecklist()
                                        {
                                            EmployeeChecklistEmails = new List<EmployeeChecklistEmail>()
                                        };

            Assert.That(employeeChecklist.LastRecipientEmail, Is.Null);
        }
    }
}
