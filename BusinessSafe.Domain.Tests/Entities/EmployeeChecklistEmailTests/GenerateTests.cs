using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.ParameterClasses;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.EmployeeChecklistEmailTests
{
    [TestFixture]
    public class GenerateTests
    {

        [Test]
        public void GeneratesExpectedEmployeeChecklistsWhenEmployeeHasEmail()
        {
            var employeeId = Guid.NewGuid();
            const string employeeEmail = "percy.purple@purplecompany.com";
            const string message = "Test message";
            var generatingUser = new UserForAuditing();

            var employee = new Employee
                               {
                                   Id = employeeId,
                                   Surname = "Brown",
                                   ContactDetails =
                                       new List<EmployeeContactDetail>
                                           {
                                               new EmployeeContactDetail
                                                   {
                                                       Email = employeeEmail
                                                   }
                                           }
                               };

            var employeesParameters = new List<EmployeesWithNewEmailsParameters>
                                          {
                                              new EmployeesWithNewEmailsParameters
                                                  {
                                                      Employee = employee
                                                  }
                                          };

            var checklists = new List<Checklist>
                                 {
                                     new Checklist
                                         {
                                             Id = 1L,
                                             Title = "Test Checklists 01"
                                         },
                                     new Checklist
                                         {
                                             Id = 2L,
                                             Title = "Test Checklist 01"
                                         }
                                 };

            var existingReferenceParameters = new List<ExistingReferenceParameters>
                                                  {
                                                      new ExistingReferenceParameters
                                                          {
                                                              Prefix = "BROWN",
                                                              MaxIncremental = 9
                                                          }
                                                  };

            var riskAssessment = new PersonalRiskAssessment();

            var employeeChecklistEmails = EmployeeChecklistEmail.Generate(
                employeesParameters, 
                checklists, 
                message, 
                generatingUser, 
                riskAssessment,
                false, 
                null, 
                null,
                existingReferenceParameters);

            Assert.That(employeeChecklistEmails.Count(), Is.EqualTo(1));
            Assert.That(employeeChecklistEmails[0].Id, Is.Not.Null);
            Assert.That(employeeChecklistEmails[0].Id, Is.Not.EqualTo(default(Guid)));
            Assert.That(employeeChecklistEmails[0].RecipientEmail, Is.EqualTo(employeeEmail));
            Assert.That(employeeChecklistEmails[0].Message, Is.EqualTo(message));
            Assert.That(employeeChecklistEmails[0].CreatedOn, Is.Not.Null);
            Assert.That(employeeChecklistEmails[0].CreatedOn, Is.Not.EqualTo(default(DateTime)));
            Assert.That(employeeChecklistEmails[0].CreatedBy, Is.EqualTo(generatingUser));
            Assert.That(employeeChecklistEmails[0].EmployeeChecklists.Count(), Is.EqualTo(2));
            Assert.That(employeeChecklistEmails[0].EmployeeChecklists[0].Employee, Is.EqualTo(employee));
            Assert.That(employeeChecklistEmails[0].EmployeeChecklists[0].Checklist, Is.EqualTo(checklists[0]));
            Assert.That(employeeChecklistEmails[0].EmployeeChecklists[0].Id, Is.Not.Null);
            Assert.That(employeeChecklistEmails[0].EmployeeChecklists[0].Id, Is.Not.EqualTo(default(Guid)));
            Assert.That(employeeChecklistEmails[0].EmployeeChecklists[0].StartDate, Is.Null);
            Assert.That(employeeChecklistEmails[0].EmployeeChecklists[0].CompletedDate, Is.Null);
            Assert.That(employeeChecklistEmails[0].EmployeeChecklists[0].Answers, Is.Null.Or.Empty);
            Assert.That(employeeChecklistEmails[0].EmployeeChecklists[0].CreatedOn, Is.Not.Null);
            Assert.That(employeeChecklistEmails[0].EmployeeChecklists[0].CreatedOn, Is.Not.EqualTo(default(DateTime)));
            Assert.That(employeeChecklistEmails[0].EmployeeChecklists[0].CreatedBy, Is.EqualTo(generatingUser));
            Assert.That(employeeChecklistEmails[0].EmployeeChecklists[0].FriendlyReference, Is.EqualTo("BROWN0010"));
            Assert.That(employeeChecklistEmails[0].EmployeeChecklists[1].Employee, Is.EqualTo(employee));
            Assert.That(employeeChecklistEmails[0].EmployeeChecklists[1].Checklist, Is.EqualTo(checklists[1]));
            Assert.That(employeeChecklistEmails[0].EmployeeChecklists[1].Id, Is.Not.Null);
            Assert.That(employeeChecklistEmails[0].EmployeeChecklists[1].Id, Is.Not.EqualTo(default(Guid)));
            Assert.That(employeeChecklistEmails[0].EmployeeChecklists[1].StartDate, Is.Null);
            Assert.That(employeeChecklistEmails[0].EmployeeChecklists[1].CompletedDate, Is.Null);
            Assert.That(employeeChecklistEmails[0].EmployeeChecklists[1].Answers, Is.Null.Or.Empty);
            Assert.That(employeeChecklistEmails[0].EmployeeChecklists[1].CreatedOn, Is.Not.Null);
            Assert.That(employeeChecklistEmails[0].EmployeeChecklists[1].CreatedOn, Is.Not.EqualTo(default(DateTime)));
            Assert.That(employeeChecklistEmails[0].EmployeeChecklists[1].CreatedBy, Is.EqualTo(generatingUser));
            Assert.That(employeeChecklistEmails[0].EmployeeChecklists[1].PersonalRiskAssessment, Is.EqualTo(riskAssessment));
            Assert.That(employeeChecklistEmails[0].EmployeeChecklists[1].SendCompletedChecklistNotificationEmail, Is.EqualTo(false));
            Assert.That(employeeChecklistEmails[0].EmployeeChecklists[1].CompletionNotificationEmailAddress, Is.EqualTo(null));
            Assert.That(employeeChecklistEmails[0].EmployeeChecklists[1].DueDateForCompletion, Is.EqualTo(null));
            Assert.That(employeeChecklistEmails[0].EmployeeChecklists[1].FriendlyReference, Is.EqualTo("BROWN0011"));
        }

        [Test]
        public void GeneratesFriendlyReferenceWhenNoCurrentSimilarReferences()
        {
            var employeeId = Guid.NewGuid();
            const string employeeEmail = "percy.purple@purplecompany.com";
            const string message = "Test message";
            var generatingUser = new UserForAuditing();

            var employee = new Employee
            {
                Id = employeeId,
                Surname = "Brown",
                ContactDetails =
                    new List<EmployeeContactDetail>
                                           {
                                               new EmployeeContactDetail
                                                   {
                                                       Email = employeeEmail
                                                   }
                                           }
            };

            var employeesParameters = new List<EmployeesWithNewEmailsParameters>
                                          {
                                              new EmployeesWithNewEmailsParameters
                                                  {
                                                      Employee = employee
                                                  }
                                          };

            var checklists = new List<Checklist>
                                 {
                                     new Checklist
                                         {
                                             Id = 1L,
                                             Title = "Test Checklists 01"
                                         }
                                 };

            var existingReferenceParameters = new List<ExistingReferenceParameters>();

            var riskAssessment = new PersonalRiskAssessment();

            var employeeChecklistEmails = EmployeeChecklistEmail.Generate(
                employeesParameters,
                checklists,
                message,
                generatingUser,
                riskAssessment,
                false,
                null,
                null,
                existingReferenceParameters);

            Assert.That(employeeChecklistEmails[0].EmployeeChecklists[0].FriendlyReference, Is.EqualTo("BROWN0001"));
        }

        [Test]
        public void SetsEmployeeAndRecipientEmailWhenNewEmailSupplied()
        {

            const string employeeEmail = "geraldine.green@greencompany.com";
            const string message = "Test message";
            var generatingUser = new UserForAuditing();

            var employee = new Employee
                               {
                                   Id = Guid.NewGuid(),
                                   Surname = "White",
                                   ContactDetails = new List<EmployeeContactDetail>()
                                                        {
                                                            new EmployeeContactDetail()
                                                        }
                               };

            var employeesParameters = new List<EmployeesWithNewEmailsParameters>
                                          {
                                              new EmployeesWithNewEmailsParameters
                                                  {
                                                      Employee = employee,
                                                      NewEmail = employeeEmail
                                                  }
                                          };

            var checklists = new List<Checklist>
                                 {
                                     new Checklist
                                         {
                                             Id = 1L,
                                             Title = "Test Checklists 01"
                                         },
                                     new Checklist
                                         {
                                             Id = 2L,
                                             Title = "Test Checklist 01"
                                         }
                                 };

            var existingReferenceParameters = new List<ExistingReferenceParameters>
                                                  {
                                                      new ExistingReferenceParameters
                                                          {
                                                              Prefix = "WHITE",
                                                              MaxIncremental = 3
                                                          }
                                                  };

            var riskAssessment = new PersonalRiskAssessment();

            var employeeChecklistEmails = EmployeeChecklistEmail.Generate(
                employeesParameters, 
                checklists, message, 
                generatingUser, 
                riskAssessment, 
                null, 
                null,
                null,
                existingReferenceParameters);

            Assert.That(employeeChecklistEmails.Count(), Is.EqualTo(1));
            Assert.That(employee.ContactDetails[0].Email, Is.EqualTo(employeeEmail));
            Assert.That(employeeChecklistEmails[0].RecipientEmail, Is.EqualTo(employeeEmail));
            Assert.That(employee.ContactDetails[0].LastModifiedOn, Is.Not.Null);
            Assert.That(employee.ContactDetails[0].LastModifiedOn, Is.Not.EqualTo(default(DateTime)));
            Assert.That(employee.ContactDetails[0].LastModifiedBy, Is.EqualTo(generatingUser));
            
        }

        [Test]
        [ExpectedException(typeof(Exception), UserMessage = "Employee does not have an email and no new NewEmail supplied")]
        public void Given_an_employee_doesnt_have_an_email_address_and_new_email_not_specified_when_Generate_Checklist_emails_then_exception_is_thrown()
        {
            const string message = "Test message";
            var generatingUser = new UserForAuditing();

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                Surname = "Purple",
                ContactDetails = new List<EmployeeContactDetail>()
                                                        {
                                                            new EmployeeContactDetail
                                                            {
                                                                Email = string.Empty
                                                            }
                                                        }
            };

            var employeesParameters = new List<EmployeesWithNewEmailsParameters>
                                          {
                                              new EmployeesWithNewEmailsParameters
                                                  {
                                                      Employee = employee,
                                                      NewEmail = string.Empty
                                                  }
                                          };

            var checklists = new List<Checklist>
                                 {
                                     new Checklist
                                         {
                                             Id = 1L,
                                             Title = "Test Checklists 01"
                                         },
                                     new Checklist
                                         {
                                             Id = 2L,
                                             Title = "Test Checklist 01"
                                         }
                                 };

            var existingReferenceParameters = new List<ExistingReferenceParameters>
                                                  {
                                                      new ExistingReferenceParameters
                                                          {
                                                              Prefix = "PURPLE",
                                                              MaxIncremental = 4
                                                          }
                                                  };

            var riskAssessment = new PersonalRiskAssessment();
            EmployeeChecklistEmail.Generate(
                employeesParameters, 
                checklists, 
                message, 
                generatingUser, 
                riskAssessment, 
                null, 
                null, 
                null,
                existingReferenceParameters);
        }
    }
}

