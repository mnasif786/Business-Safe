using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.ParameterClasses;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.PersonalRiskAssessmentTests
{
    [TestFixture]
    public class SaveChecklistGeneratorTests
    {
        [Test]
        public void CanSaveForSingleEmployeeWhenEmployeeHasEmail()
        {
            const bool hasMultipleChecklistRecipients = false;
            var employeeId = Guid.NewGuid();
            const string employeeEmail = "percy.purple@purplecompany.com";
            const string message = "Test message";
            var currentUser = new UserForAuditing();

            var employee = new Employee
            {
                Id = employeeId,
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

            var personalRiskAssessment = new PersonalRiskAssessment()
                                             {
                                                 ChecklistGeneratorEmployees = new List<ChecklistGeneratorEmployee>(),
                                                 Checklists = new List<PersonalRiskAssessmentChecklist>()
                                             };

            bool? sendCompletedChecklistNotificationEmail = true;
            DateTime? completionDueDateForChecklists = DateTime.Now;
            string completionNotificationEmailAddress = "test@hotmail.com";

            personalRiskAssessment.SaveChecklistGenerator(
                hasMultipleChecklistRecipients,
                employeesParameters,
                checklists,
                message,
                currentUser,
                sendCompletedChecklistNotificationEmail,
                completionDueDateForChecklists,
                completionNotificationEmailAddress);

            Assert.That(personalRiskAssessment.HasMultipleChecklistRecipients, Is.EqualTo(hasMultipleChecklistRecipients));
            Assert.That(personalRiskAssessment.ChecklistGeneratorEmployees.Count(), Is.EqualTo(1));
            Assert.That(personalRiskAssessment.ChecklistGeneratorEmployees[0].Employee.Id, Is.EqualTo(employeeId));
            Assert.That(personalRiskAssessment.ChecklistGeneratorEmployees[0].Employee.ContactDetails[0].Email, Is.EqualTo(employeeEmail));
            Assert.That(personalRiskAssessment.Checklists.Count(), Is.EqualTo(2));
            Assert.That(personalRiskAssessment.Checklists.Select(x => x.Checklist).Contains(checklists[0]));
            Assert.That(personalRiskAssessment.Checklists.Select(x => x.Checklist).Contains(checklists[1]));
            Assert.That(personalRiskAssessment.ChecklistGeneratorMessage, Is.EqualTo(message));
            Assert.That(personalRiskAssessment.LastModifiedBy, Is.EqualTo(currentUser));
            Assert.That(personalRiskAssessment.LastModifiedOn, Is.Not.Null);
            Assert.That(personalRiskAssessment.LastModifiedOn, Is.Not.EqualTo(default(DateTime)));
            Assert.That(personalRiskAssessment.SendCompletedChecklistNotificationEmail, Is.EqualTo(sendCompletedChecklistNotificationEmail));
            Assert.That(personalRiskAssessment.CompletionDueDateForChecklists, Is.EqualTo(completionDueDateForChecklists));
            Assert.That(personalRiskAssessment.CompletionNotificationEmailAddress, Is.EqualTo(completionNotificationEmailAddress));
        }

        [Test]
        public void CanSaveForSingleEmployeeWhenNewEmailSuppliedForEmployee()
        {
            const bool hasMultipleChecklistRecipients = false;
            var employeeId = Guid.NewGuid();
            const string employeeEmail = "percy.purple@purplecompany.com";
            const string message = "Test message";
            var currentUser = new UserForAuditing();

            var employee = new Employee
            {
                Id = employeeId
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

            var personalRiskAssessment = new PersonalRiskAssessment()
            {
                ChecklistGeneratorEmployees = new List<ChecklistGeneratorEmployee>(),
                Checklists = new List<PersonalRiskAssessmentChecklist>()
            };

            personalRiskAssessment.SaveChecklistGenerator(
                hasMultipleChecklistRecipients,
                employeesParameters,
                checklists,
                message,
                currentUser, 
                null,
                null,
                string.Empty);

            Assert.That(personalRiskAssessment.HasMultipleChecklistRecipients, Is.EqualTo(hasMultipleChecklistRecipients));
            Assert.That(personalRiskAssessment.ChecklistGeneratorEmployees.Count(), Is.EqualTo(1));
            Assert.That(personalRiskAssessment.ChecklistGeneratorEmployees[0].Employee.Id, Is.EqualTo(employeeId));
            Assert.That(personalRiskAssessment.ChecklistGeneratorEmployees[0].Employee.ContactDetails[0].Email, Is.EqualTo(employeeEmail));
            Assert.That(personalRiskAssessment.Checklists.Count(), Is.EqualTo(2));
            Assert.That(personalRiskAssessment.Checklists.Select(x => x.Checklist).Contains(checklists[0]));
            Assert.That(personalRiskAssessment.Checklists.Select(x => x.Checklist).Contains(checklists[1]));
            Assert.That(personalRiskAssessment.ChecklistGeneratorMessage, Is.EqualTo(message));
            Assert.That(personalRiskAssessment.LastModifiedBy, Is.EqualTo(currentUser));
            Assert.That(personalRiskAssessment.LastModifiedOn, Is.Not.Null);
            Assert.That(personalRiskAssessment.LastModifiedOn, Is.Not.EqualTo(default(DateTime)));
        }

        
    }
}
