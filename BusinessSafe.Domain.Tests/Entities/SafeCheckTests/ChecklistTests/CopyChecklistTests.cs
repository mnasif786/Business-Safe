using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.ParameterClasses.SafeCheck;
//using EvaluationChecklist.Models;
using NUnit.Framework;
using Checklist = BusinessSafe.Domain.Entities.SafeCheck.Checklist;
using Question = BusinessSafe.Domain.Entities.SafeCheck.Question;

namespace BusinessSafe.Domain.Tests.Entities.SafeCheckTests.ChecklistTests
{
    [TestFixture]
    internal class CopyChecklistTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void given_checklist_when_copyWithoutResponses_then_site_set()
        {
            //given
            var userForAuditing = new UserForAuditing() {Id = Guid.NewGuid()};
            var copiedByUserName = "Abc";
            var checklist = new Checklist() { Id = Guid.NewGuid(), ChecklistCreatedBy = "previousCreatedBy", CreatedOn = DateTime.Now.AddDays(-10), ChecklistCreatedOn = DateTime.Now.AddDays(-10) };


            var siteId = 213123;
            var clientID = 54321;

            //when
            var result = checklist.CopyToSiteWithoutResponses(siteId, clientID, userForAuditing, copiedByUserName, false);

            //then
            Assert.That(result.Id, Is.Not.EqualTo(checklist.Id));
            Assert.That(result.SiteId, Is.EqualTo(siteId));
        }

        [Test]
        public void given_checklist_when_copyWithoutResponses_then_new_client_id_set()
        {
            //given
            var userForAuditing = new UserForAuditing() { Id = Guid.NewGuid() };
            var copiedByUserName = "Abc";
            var checklist = new Checklist() { Id = Guid.NewGuid(), ClientId = 11111, ChecklistCreatedBy = "previousCreatedBy", CreatedOn = DateTime.Now.AddDays(-10), ChecklistCreatedOn = DateTime.Now.AddDays(-10) };


            var siteId = 213123;
            var clientID = 99999;

            //when
            var result = checklist.CopyToSiteWithoutResponses(siteId, clientID, userForAuditing, copiedByUserName, false);

            //then
            Assert.That(result.Id, Is.Not.EqualTo(checklist.Id));
            Assert.That(result.ClientId, Is.EqualTo(clientID));
        }


        [Test]
        public void given_checklist_when_copyWithResponses_then_new_client_id_set()
        {
            //given
            var userForAuditing = new UserForAuditing() { Id = Guid.NewGuid() };
            var copiedByUserName = "Abc";
            var checklist = new Checklist() { Id = Guid.NewGuid(), ClientId = 11111, ChecklistCreatedBy = "previousCreatedBy", CreatedOn = DateTime.Now.AddDays(-10), ChecklistCreatedOn = DateTime.Now.AddDays(-10) };


            var siteId = 213123;
            var clientID = 99999;

            //when
            var result = checklist.CopyToSiteWithResponses(siteId, clientID, userForAuditing, copiedByUserName, false);

            //then
            Assert.That(result.Id, Is.Not.EqualTo(checklist.Id));
            Assert.That(result.ClientId, Is.EqualTo(clientID));
        }
    
        [Test]
        public void given_checklist_when_copyWithoutResponses_then_initial_Values_set()
        {
            //given
            var clientId = 123;
            var userForAuditing = new UserForAuditing() { Id = Guid.NewGuid() };
            var copiedByUserName = "Abc";
            var checklist = new Checklist()
            {
                Id = Guid.NewGuid(), 
                ChecklistCreatedBy = "previousCreatedBy", 
                CreatedOn = DateTime.Now.AddDays(-10), 
                ChecklistCreatedOn = DateTime.Now.AddDays(-10),
                Jurisdiction = "UK",
                Status = "Submitted",
                ClientId = clientId,
                ChecklistTemplate = new ChecklistTemplate() { Id=Guid.NewGuid()}
            };

            var siteId = 213123;            

            //when
            var result = checklist.CopyToSiteWithoutResponses(siteId, clientId, userForAuditing, copiedByUserName, false);

            //then
            Assert.That(result.Id, Is.Not.EqualTo(checklist.Id));
            Assert.That(result.ClientId, Is.EqualTo(clientId));
            Assert.That(result.Jurisdiction, Is.EqualTo(checklist.Jurisdiction));
            Assert.That(result.Status, Is.EqualTo("Draft"));
            Assert.That(result.ChecklistCreatedBy, Is.EqualTo(copiedByUserName));
            Assert.That(result.ChecklistCreatedOn.Value.Date, Is.Not.EqualTo(checklist.CreatedOn.Value.Date));

        }

        [Test]
        public void given_checklist_when_copyWithoutResponses_then_mandatory_questions_set()
        {
            //given
            var siteId = 213123;
            var clientId = 123;
            var userForAuditing = new UserForAuditing() { Id = Guid.NewGuid() };
            var copiedByUserName = "Abc";
            var question = Question.Create(Guid.NewGuid(), "Title",
                new Category() {Id = Guid.NewGuid(), Title = "Care"}, false, userForAuditing);
            

            var checklist = new Checklist()
            {
                Id = Guid.NewGuid(),
                ChecklistCreatedBy = "previousCreatedBy",
                CreatedOn = DateTime.Now.AddDays(-10),
                ChecklistCreatedOn = DateTime.Now.AddDays(-10),
                Jurisdiction = "UK",
                Status = "Submitted",
                ClientId = clientId,
                ChecklistTemplate = new ChecklistTemplate() { Id = Guid.NewGuid() }
            };

            checklist.Questions.Add(new ChecklistQuestion() { Id = Guid.NewGuid(), Question = question, Checklist=checklist, CategoryNumber = 1, QuestionNumber = 2});
            
            //when
            var result = checklist.CopyToSiteWithoutResponses(siteId, clientId, userForAuditing, copiedByUserName, false);

            //then
            Assert.That(result.Questions.Count, Is.EqualTo(checklist.Questions.Count));
            Assert.That(result.Questions[0].Id, Is.Not.EqualTo(checklist.Questions[0].Id));
            Assert.That(result.Questions[0].Question.Id, Is.EqualTo(checklist.Questions[0].Question.Id));
            Assert.That(result.Questions[0].CategoryNumber, Is.EqualTo(checklist.Questions[0].CategoryNumber));
            Assert.That(result.Questions[0].QuestionNumber, Is.EqualTo(checklist.Questions[0].QuestionNumber));
            Assert.That(result.Questions[0].Question.CustomQuestion, Is.EqualTo(checklist.Questions[0].Question.CustomQuestion));
            Assert.That(result.Questions[0].Question.Deleted, Is.EqualTo(checklist.Questions[0].Question.CustomQuestion));
            Assert.That(result.Questions[0].Question.Category.Id, Is.EqualTo(checklist.Questions[0].Question.Category.Id));
        }

        [Test]
        public void given_checklist_when_copyWithoutResponses_then_non_mandatory_questions_set()
        {
            //given
            var siteId = 213123;
            var clientId = 123;
            var userForAuditing = new UserForAuditing() { Id = Guid.NewGuid() };
            var copiedByUserName = "Abc";
            var question = Question.Create(Guid.NewGuid(), "Title",
                new Category() { Id = Guid.NewGuid(), Title = "Care" }, true, userForAuditing);

            var checklist = new Checklist()
            {
                Id = Guid.NewGuid(),
                ChecklistCreatedBy = "previousCreatedBy",
                CreatedOn = DateTime.Now.AddDays(-10),
                ChecklistCreatedOn = DateTime.Now.AddDays(-10),
                Jurisdiction = "UK",
                Status = "Submitted",
                ClientId = clientId,
                ChecklistTemplate = new ChecklistTemplate() { Id = Guid.NewGuid() }
            };

            checklist.Questions.Add(new ChecklistQuestion() { Id = Guid.NewGuid(), Question = question, Checklist = checklist, CategoryNumber = 1, QuestionNumber = 2 });

            //when
            var result = checklist.CopyToSiteWithoutResponses(siteId, clientId, userForAuditing, copiedByUserName, false);

            //then
            Assert.That(result.Questions.Count, Is.EqualTo(checklist.Questions.Count));
            Assert.That(result.Questions[0].Id, Is.Not.EqualTo(checklist.Questions[0].Id));
            Assert.That(result.Questions[0].Question.Id, Is.Not.EqualTo(checklist.Questions[0].Question.Id));
            Assert.That(result.Questions[0].CategoryNumber, Is.EqualTo(checklist.Questions[0].CategoryNumber));
            Assert.That(result.Questions[0].QuestionNumber, Is.EqualTo(checklist.Questions[0].QuestionNumber));
            Assert.That(result.Questions[0].Question.CustomQuestion, Is.EqualTo(checklist.Questions[0].Question.CustomQuestion));
            Assert.That(result.Questions[0].Question.Category.Id, Is.EqualTo(checklist.Questions[0].Question.Category.Id));
        }

        [Test]
        public void given_checklist_when_copyWithoutResponses_then_deleted_questions_not_set()
        {
            //given
            var siteId = 213123;
            var clientId = 123;
            var userForAuditing = new UserForAuditing() { Id = Guid.NewGuid() };
            var copiedByUserName = "Abc";

            var question1 = Question.Create(Guid.NewGuid(), "Title",
                new Category() { Id = Guid.NewGuid(), Title = "Care" }, false, userForAuditing);

            var question2 = Question.Create(Guid.NewGuid(), "Title",
                new Category() { Id = Guid.NewGuid(), Title = "Care" }, true, userForAuditing);

            var question3 = Question.Create(Guid.NewGuid(), "Title",
               new Category() { Id = Guid.NewGuid(), Title = "Care" }, false, userForAuditing);
            question3.Deleted = true;

            var question4 = Question.Create(Guid.NewGuid(), "Title",
               new Category() { Id = Guid.NewGuid(), Title = "Care" }, true, userForAuditing);
            question4.Deleted = true;

            var checklist = new Checklist()
            {
                Id = Guid.NewGuid(),
                ChecklistCreatedBy = "previousCreatedBy",
                CreatedOn = DateTime.Now.AddDays(-10),
                ChecklistCreatedOn = DateTime.Now.AddDays(-10),
                Jurisdiction = "UK",
                Status = "Submitted",
                ClientId = clientId,
                ChecklistTemplate = new ChecklistTemplate() { Id = Guid.NewGuid() }
            };

            checklist.Questions.Add(new ChecklistQuestion() { Id = Guid.NewGuid(), Question = question1, Checklist = checklist, CategoryNumber = 1, QuestionNumber = 4 });
            checklist.Questions.Add(new ChecklistQuestion() { Id = Guid.NewGuid(), Question = question2, Checklist = checklist, CategoryNumber = 2, QuestionNumber = 3 });
            checklist.Questions.Add(new ChecklistQuestion() { Id = Guid.NewGuid(), Question = question3, Checklist = checklist, CategoryNumber = 3, QuestionNumber = 2 });
            checklist.Questions.Add(new ChecklistQuestion() { Id = Guid.NewGuid(), Question = question4, Checklist = checklist, CategoryNumber = 4, QuestionNumber = 1});

            //when
            var result = checklist.CopyToSiteWithoutResponses(siteId, clientId, userForAuditing, copiedByUserName, false);

            //then
            Assert.That(result.Questions.Count, Is.EqualTo(2));
        }

        [Test]
        public void given_checklist_when_copyWithResponses_then_Answers_to_all_mandatory_questions_are_set()
        {
            //given
            var siteId = 213123;
            var clientId = 54321;
            var userForAuditing = new UserForAuditing() { Id = Guid.NewGuid() };
            var copiedByUserName = "Abc";

            var question = Question.Create(Guid.NewGuid(), "Title",
              new Category() { Id = Guid.NewGuid(), Title = "Care" }, false, userForAuditing);

            var answer = new ChecklistAnswer()
            {
                Id = Guid.NewGuid(),
                AssignedTo = new BusinessSafe.Domain.Entities.Employee() { Id = Guid.NewGuid() },
                EmployeeNotListed = "employee not listed",
                Timescale = new Timescale() { Id = 2L },
                Question = question,
                Response = new QuestionResponse()
                {
                    Id = Guid.NewGuid(),
                    SupportingEvidence = "sp",
                    GuidanceNotes = "the guidance notes",
                    ActionRequired = "the action required",
                    ResponseType = "Positive",
                    Question = question,
                },
                SupportingDocumentationDate = DateTime.Now.Date,
                SupportingDocumentationStatus = "SS",
                SupportingEvidence = "se"
            };

            var answersList = new List<ChecklistAnswer>() { answer };
            var checklist = new Checklist() { Id = Guid.NewGuid()};

            checklist.Questions.Add(new ChecklistQuestion() { Id = Guid.NewGuid(), Question = question, Checklist = checklist, CategoryNumber = 1, QuestionNumber = 4 });
            checklist.UpdateAnswers(answersList, new UserForAuditing());
           
            //when
            var result = checklist.CopyToSiteWithResponses(siteId, clientId, userForAuditing, copiedByUserName, false);

            //then
            Assert.That(result.Answers.Count, Is.EqualTo(1));
            Assert.That(result.Answers[0].Checklist.Id, Is.Not.EqualTo(checklist.Id)); //copied answer Should be assiciated with the new checklist 
            Assert.That(result.Answers[0].Id, Is.Not.EqualTo(checklist.Answers[0].Id)); //copied answer id should be different than orignal answer id
            Assert.That(result.Answers[0].Question.Id, Is.EqualTo(checklist.Answers[0].Question.Id));
            Assert.That(result.Answers[0].Timescale, Is.EqualTo(null));
            Assert.That(result.Answers[0].AssignedTo, Is.EqualTo(null));
            Assert.That(result.Answers[0].EmployeeNotListed, Is.EqualTo(checklist.Answers[0].EmployeeNotListed));


            Assert.That(result.Answers[0].Response.Id, Is.EqualTo(checklist.Answers[0].Response.Id));
            Assert.That(result.Answers[0].Response.Question.Id, Is.EqualTo(checklist.Answers[0].Response.Question.Id));
            Assert.That(result.Answers[0].Response.ActionRequired, Is.EqualTo(checklist.Answers[0].Response.ActionRequired));
            Assert.That(result.Answers[0].Response.Id, Is.EqualTo(checklist.Answers[0].Response.Id));
            Assert.That(result.Answers[0].Response.GuidanceNotes, Is.EqualTo(checklist.Answers[0].Response.GuidanceNotes));
            Assert.That(result.Answers[0].Response.SupportingEvidence, Is.EqualTo(checklist.Answers[0].Response.SupportingEvidence));
        }

        [Test]
        public void given_checklist_when_copyWithResponses_then_Answers_to_all_non_mandatory_questions_are_set()
        {
            //given
            var siteId = 213123;
            var clientId = 54321;
            var userForAuditing = new UserForAuditing() { Id = Guid.NewGuid() };
            var copiedByUserName = "Abc";
            

            var question1 = Question.Create(Guid.NewGuid(), "Title",
               new Category() { Id = Guid.NewGuid(), Title = "Care" }, true, userForAuditing);

            var responseId = Guid.NewGuid();
            var questionResponse1 = new QuestionResponse() {
                    Id = responseId,
                    Title = "Acceptable",
                    SupportingEvidence = "sp",
                    GuidanceNotes = "the guidance notes",
                    ActionRequired = "the action required",
                    ResponseType = "Positive",
                    Question = question1,
                };
            var questionResponse2 = new QuestionResponse() { Id = Guid.NewGuid(), Question = question1, Title = "Unacceptable", ResponseType = "Negative"};

            question1.PossibleResponses.Add(questionResponse1);
            question1.PossibleResponses.Add(questionResponse2);

           
            var answer = new ChecklistAnswer()
            {
                Id = Guid.NewGuid(),
                AssignedTo = new BusinessSafe.Domain.Entities.Employee() { Id = Guid.NewGuid() },
                EmployeeNotListed = "employee not listed",
                Timescale = new Timescale() { Id = 2L },
                Question = question1,
                Response = new QuestionResponse()
                {
                    Id = responseId,
                    Title = "Acceptable",
                    SupportingEvidence = "sp",
                    GuidanceNotes = "the guidance notes",
                    ActionRequired = "the action required",
                    ResponseType = "Positive",
                    Question = question1,
                },
                SupportingDocumentationDate = DateTime.Now.Date,
                SupportingDocumentationStatus = "SS",
                SupportingEvidence = "se"
            };

            
            var answersList = new List<ChecklistAnswer>() { answer };
            var checklist = new Checklist() { Id = Guid.NewGuid() };

            checklist.Questions.Add(new ChecklistQuestion() { Id = Guid.NewGuid(), Question = question1, Checklist = checklist, CategoryNumber = 1, QuestionNumber = 4 });
            
            checklist.UpdateAnswers(answersList, new UserForAuditing());

            //when
            var result = checklist.CopyToSiteWithResponses(siteId, clientId, userForAuditing, copiedByUserName, false);

            Assert.That(result.Answers.Count, Is.EqualTo(checklist.Answers.Count()));

            Assert.That(result.Answers[0].Id, Is.Not.EqualTo(checklist.Answers[0].Id));
            Assert.That(result.Answers[0].Question.Id, Is.Not.EqualTo(checklist.Answers[0].Question.Id));
            Assert.That(result.Answers[0].Timescale, Is.EqualTo(null));
            Assert.That(result.Answers[0].AssignedTo, Is.EqualTo(null));
            Assert.That(result.Answers[0].EmployeeNotListed, Is.EqualTo(checklist.Answers[0].EmployeeNotListed));


            Assert.That(result.Answers[0].Response.Question.Id, Is.Not.EqualTo(checklist.Answers[0].Question.Id));
            Assert.That(result.Answers[0].Response.Title, Is.EqualTo(checklist.Answers[0].Response.Title));
            Assert.That(result.Answers[0].Response.ActionRequired, Is.EqualTo(checklist.Answers[0].Response.ActionRequired));
            Assert.That(result.Answers[0].Response.GuidanceNotes, Is.EqualTo(checklist.Answers[0].Response.GuidanceNotes));
            Assert.That(result.Answers[0].Response.SupportingEvidence, Is.EqualTo(checklist.Answers[0].Response.SupportingEvidence));
            Assert.That(result.Answers[0].Response.SupportingEvidence, Is.EqualTo(checklist.Answers[0].Response.SupportingEvidence));
            Assert.That(result.Answers[0].Response.ResponseType, Is.EqualTo(checklist.Answers[0].Response.ResponseType));
            
        }

        [Test]
        public void given_checklist_when_copyWithResponses_then_IRNs_are_copied()
        {
            //given
            var siteId = 213123;
            var clientId = 54321;
            var userForAuditing = new UserForAuditing() { Id = Guid.NewGuid() };
            var copiedByUserName = "Abc";

            var checklist = new Checklist() { Id = Guid.NewGuid() };

            Guid IRN1Id = Guid.NewGuid();
            var irn1 = ImmediateRiskNotification.Create( IRN1Id, 
                                                        "IRN 1 Some reference", 
                                                        "IRN 1 Title",
                                                        "IRN 1 Significant hazard identifed",
                                                        "IRN 1 Recommended immediate action",
                                                        checklist,
                                                        userForAuditing);

            Guid IRN2Id = Guid.NewGuid();
            var irn2 = ImmediateRiskNotification.Create(IRN2Id,
                                                        "IRN 2 Some reference",
                                                        "IRN 2 Title",
                                                        "IRN 2 Significant hazard identifed",
                                                        "IRN 2 Recommended immediate action",
                                                        checklist,
                                                        userForAuditing);

            checklist.AddImmediateRiskNotification(irn1);
            checklist.AddImmediateRiskNotification(irn2);
            
            //when
            var result = checklist.CopyToSiteWithResponses(siteId, clientId, userForAuditing, copiedByUserName, false);

            Assert.That(result.ImmediateRiskNotifications.Count, Is.EqualTo(checklist.ImmediateRiskNotifications.Count()));

            Assert.That(result.ImmediateRiskNotifications[0].Id, Is.Not.EqualTo(checklist.ImmediateRiskNotifications[0].Id));
            Assert.That(result.ImmediateRiskNotifications[0].Reference, Is.EqualTo(checklist.ImmediateRiskNotifications[0].Reference));
            Assert.That(result.ImmediateRiskNotifications[0].Title, Is.EqualTo(checklist.ImmediateRiskNotifications[0].Title));
            Assert.That(result.ImmediateRiskNotifications[0].SignificantHazardIdentified, Is.EqualTo(checklist.ImmediateRiskNotifications[0].SignificantHazardIdentified));
            Assert.That(result.ImmediateRiskNotifications[0].RecommendedImmediateAction, Is.EqualTo(checklist.ImmediateRiskNotifications[0].RecommendedImmediateAction));
            Assert.That(result.ImmediateRiskNotifications[0].Checklist.Id, Is.EqualTo(result.Id));

            Assert.That(result.ImmediateRiskNotifications[1].Id, Is.Not.EqualTo(checklist.ImmediateRiskNotifications[1].Id));
            Assert.That(result.ImmediateRiskNotifications[1].Reference, Is.EqualTo(checklist.ImmediateRiskNotifications[1].Reference));
            Assert.That(result.ImmediateRiskNotifications[1].Title, Is.EqualTo(checklist.ImmediateRiskNotifications[1].Title));
            Assert.That(result.ImmediateRiskNotifications[1].SignificantHazardIdentified, Is.EqualTo(checklist.ImmediateRiskNotifications[1].SignificantHazardIdentified));
            Assert.That(result.ImmediateRiskNotifications[1].RecommendedImmediateAction, Is.EqualTo(checklist.ImmediateRiskNotifications[1].RecommendedImmediateAction));                                                          
            Assert.That(result.ImmediateRiskNotifications[1].Checklist.Id, Is.EqualTo(result.Id));
        }


        [Test]
        public void given_checklist_when_copyWithResponses_then_Assigned_to_and_timescale_fields_are_not_set()
        {
            //given
            var siteId = 213123;
            var clientId = 54321;
            var userForAuditing = new UserForAuditing() { Id = Guid.NewGuid() };
            var copiedByUserName = "Abc";

            var question = Question.Create(Guid.NewGuid(), "Title",
              new Category() { Id = Guid.NewGuid(), Title = "Care" }, false, userForAuditing);

            var answer = new ChecklistAnswer()
            {
                Id = Guid.NewGuid(),
                AssignedTo = new BusinessSafe.Domain.Entities.Employee() { Id = Guid.NewGuid() },
                EmployeeNotListed = "employee not listed",
                Timescale = new Timescale() { Id = 2L },
                Question = question,
                Response = new QuestionResponse()
                {
                    Id = Guid.NewGuid(),
                    SupportingEvidence = "sp",
                    GuidanceNotes = "the guidance notes",
                    ActionRequired = "the action required",
                    ResponseType = "Positive",
                    Question = question,
                },
                SupportingDocumentationDate = DateTime.Now.Date,
                SupportingDocumentationStatus = "SS",
                SupportingEvidence = "se"
            };

            var answersList = new List<ChecklistAnswer>() { answer };
            var checklist = new Checklist() { Id = Guid.NewGuid() };

            checklist.Questions.Add(new ChecklistQuestion() { Id = Guid.NewGuid(), Question = question, Checklist = checklist, CategoryNumber = 1, QuestionNumber = 4 });
            checklist.UpdateAnswers(answersList, new UserForAuditing());

            //when
            var result = checklist.CopyToSiteWithResponses(siteId, clientId, userForAuditing, copiedByUserName, false);

            //then
            Assert.That(result.Answers.Count, Is.EqualTo(1));
            Assert.That(result.Answers[0].Question.Id, Is.EqualTo(checklist.Answers[0].Question.Id));            

            Assert.AreEqual(null, result.Answers[0].AssignedTo);
            Assert.AreEqual(null, result.Answers[0].Timescale);          
        }

        [Test]
        public void given_checklist_when_copyWithResponses_with_question_marked_as_acceptable_then_SupportingDocumentationDate_is_not_set()
        {
            //given
            var siteId = 213123;
            var clientId = 54321;
            var userForAuditing = new UserForAuditing() { Id = Guid.NewGuid() };
            var copiedByUserName = "Abc";

            var question = Question.Create(Guid.NewGuid(), "Title",
              new Category() { Id = Guid.NewGuid(), Title = "Care" }, false, userForAuditing);
            
            var answer = new ChecklistAnswer()
            {
                Id = Guid.NewGuid(),
                AssignedTo = new BusinessSafe.Domain.Entities.Employee() { Id = Guid.NewGuid() },
                EmployeeNotListed = "employee not listed",
                Timescale = new Timescale() { Id = 2L },
                Question = question,
                Response = new QuestionResponse()
                {
                    Id = Guid.NewGuid(),
                    SupportingEvidence = "sp",
                    GuidanceNotes = "the guidance notes",
                    ActionRequired = "the action required",
                    ResponseType = "Positive",
                    Question = question

                },
                SupportingDocumentationDate = DateTime.Now.Date,
                SupportingDocumentationStatus = "SS",
                SupportingEvidence = "se"
            };

            var answersList = new List<ChecklistAnswer>() { answer };
            var checklist = new Checklist() { Id = Guid.NewGuid() };

            checklist.Questions.Add(new ChecklistQuestion() { Id = Guid.NewGuid(), Question = question, Checklist = checklist, CategoryNumber = 1, QuestionNumber = 4 });
            checklist.UpdateAnswers(answersList, new UserForAuditing());

            //when
            var result = checklist.CopyToSiteWithResponses(siteId, clientId, userForAuditing, copiedByUserName, false);

            //then
            Assert.That(result.Answers.Count, Is.EqualTo(1));
            Assert.That(result.Answers[0].Question.Id, Is.EqualTo(checklist.Answers[0].Question.Id));
            Assert.AreEqual(null, result.Answers[0].SupportingDocumentationDate);            
        }
      
    }
}
