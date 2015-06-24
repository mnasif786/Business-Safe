using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.ParameterClasses.SafeCheck;
using NUnit.Framework;
using Checklist = BusinessSafe.Domain.Entities.SafeCheck.Checklist;
using Question = BusinessSafe.Domain.Entities.SafeCheck.Question;

namespace BusinessSafe.Domain.Tests.Entities.SafeCheckTests.ChecklistTests
{
    [TestFixture]
    public class UpdateChecklistAnswersTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Given_new_answers_when_UpdateAnswers_then_new_answers_added_to_the_database()
        {
            // Given
            var newAnswer = new ChecklistAnswer()
                                {
                                    Id = Guid.NewGuid(),
                                    ActionRequired = "the action required",
                                    AssignedTo = new BusinessSafe.Domain.Entities.Employee() {Id = Guid.NewGuid()}
                                    ,
                                    EmployeeNotListed = "employee not listed",
                                    GuidanceNotes = "the guidance notes",
                                    SupportingEvidence = "the supporting evidence",
                                    Question = new Question() {Id = new Guid()}
                                    , Response = new QuestionResponse(){Id = Guid.NewGuid()}
                                    , Timescale = new Timescale(){ Id = 2L}
                                };
            var newAnswers = new List<ChecklistAnswer>() { newAnswer };

            var checklist = new Checklist();

           checklist.UpdateAnswers(newAnswers,new UserForAuditing());

            // When
            Assert.That(checklist.Answers.Count, Is.EqualTo(1));
            Assert.That(checklist.Answers.First().ActionRequired, Is.EqualTo(newAnswer.ActionRequired));
            Assert.That(checklist.Answers.First().AssignedTo, Is.EqualTo(newAnswer.AssignedTo));
            Assert.That(checklist.Answers.First().EmployeeNotListed, Is.EqualTo(newAnswer.EmployeeNotListed));
            Assert.That(checklist.Answers.First().GuidanceNotes, Is.EqualTo(newAnswer.GuidanceNotes));
            Assert.That(checklist.Answers.First().SupportingEvidence, Is.EqualTo(newAnswer.SupportingEvidence));
            Assert.That(checklist.Answers.First().Question, Is.EqualTo(newAnswer.Question));
            Assert.That(checklist.Answers.First().Response, Is.EqualTo(newAnswer.Response));
            Assert.That(checklist.Answers.First().Timescale, Is.EqualTo(newAnswer.Timescale));
            Assert.That(checklist.Answers.First().LastModifiedBy, Is.Not.Null);
            Assert.That(checklist.Answers.First().LastModifiedOn, Is.Not.Null);

        }

        [Test]
        public void Given_an_answer_has_been_changed_when_UpdateAnswers_then_answers_update()
        {
            // Given
            var originalAnswer = new ChecklistAnswer()
            {
                Id = Guid.NewGuid(),
                ActionRequired = "the action required",
                AssignedTo = new BusinessSafe.Domain.Entities.Employee() { Id = Guid.NewGuid() }
                ,
                EmployeeNotListed = "employee not listed",
                GuidanceNotes = "the guidance notes",
                SupportingEvidence = "the supporting evidence",
                Question = new Question() { Id = new Guid() }
                ,
                Response = new QuestionResponse() { Id = Guid.NewGuid() }
                ,
                Timescale = new Timescale() { Id = 2L }
            };

            var newAnswer = new ChecklistAnswer()
            {
                Id = originalAnswer.Id,
                ActionRequired = "the action required updated",
                AssignedTo = new BusinessSafe.Domain.Entities.Employee() { Id = Guid.NewGuid() }
                ,
                EmployeeNotListed = "employee not listed updated",
                GuidanceNotes = "the guidance notes updated",
                SupportingEvidence = "the supporting evidence updated",
                Question = originalAnswer.Question
                ,
                Response = new QuestionResponse() { Id = Guid.NewGuid() }
                ,
                Timescale = new Timescale() { Id = 5L }
            };

            var newAnswers = new List<ChecklistAnswer>() { newAnswer };

            var checklist = new Checklist();
            checklist.Answers.Add(originalAnswer);
            checklist.UpdateAnswers(newAnswers, new UserForAuditing());

            // When
            Assert.That(checklist.Answers.Count, Is.EqualTo(1));
            Assert.That(checklist.Answers.First().ActionRequired, Is.EqualTo(newAnswer.ActionRequired));
            Assert.That(checklist.Answers.First().AssignedTo, Is.EqualTo(newAnswer.AssignedTo));
            Assert.That(checklist.Answers.First().EmployeeNotListed, Is.EqualTo(newAnswer.EmployeeNotListed));
            Assert.That(checklist.Answers.First().GuidanceNotes, Is.EqualTo(newAnswer.GuidanceNotes));
            Assert.That(checklist.Answers.First().SupportingEvidence, Is.EqualTo(newAnswer.SupportingEvidence));
            Assert.That(checklist.Answers.First().Question, Is.EqualTo(originalAnswer.Question));
            Assert.That(checklist.Answers.First().Response, Is.EqualTo(newAnswer.Response));
            Assert.That(checklist.Answers.First().Timescale, Is.EqualTo(newAnswer.Timescale));
            Assert.That(checklist.Answers.First().LastModifiedBy, Is.Not.Null);
            Assert.That(checklist.Answers.First().LastModifiedOn, Is.Not.Null);

        }

        [Test]
        public void Given_answer_hasnt_changed_when_UpdateAnswers_then_lastmodifiedDate_is_not_updated()
        {
            var originalLastModifiedByDate = DateTime.Now.AddDays(-5);

            // Given
            var originalAnswer = new ChecklistAnswer()
            {
                Id = Guid.NewGuid(),
                ActionRequired = "the action required",
                AssignedTo = new BusinessSafe.Domain.Entities.Employee() { Id = Guid.NewGuid() }
                ,
                EmployeeNotListed = "employee not listed",
                GuidanceNotes = "the guidance notes",
                SupportingEvidence = "the supporting evidence",
                Question = new Question() { Id = new Guid() }
                ,
                Response = new QuestionResponse() { Id = Guid.NewGuid() }
                ,
                Timescale = new Timescale() { Id = 2L }
                ,
                LastModifiedBy = new UserForAuditing() { Id = Guid.NewGuid() },
                LastModifiedOn = originalLastModifiedByDate
            };

            var newAnswer = new ChecklistAnswer()
            {
                Id = originalAnswer.Id,
                ActionRequired = originalAnswer.ActionRequired,
                AssignedTo = originalAnswer.AssignedTo,
                EmployeeNotListed = originalAnswer.EmployeeNotListed,
                GuidanceNotes = originalAnswer.GuidanceNotes,
                SupportingEvidence = originalAnswer.SupportingEvidence,
                Question = originalAnswer.Question,
                Response = originalAnswer.Response,
                Timescale = originalAnswer.Timescale
            };

            var newAnswers = new List<ChecklistAnswer>() { newAnswer };

            var checklist = new Checklist();
            checklist.Answers.Add(originalAnswer);
            checklist.UpdateAnswers(newAnswers, new UserForAuditing());

            // When
            Assert.That(checklist.Answers.First().LastModifiedOn.Value, Is.EqualTo(originalLastModifiedByDate));
            

        }

        [Test]
        public void Given_answer_response_changed_when_UpdateAnswers_then_lastmodifiedDate_updated()
        {
            var originalLastModifiedByDate = DateTime.Now.AddDays(-5);

            // Given
            var originalAnswer = new ChecklistAnswer()
            {
                Id = Guid.NewGuid(),
                ActionRequired = "the action required",
                AssignedTo = new BusinessSafe.Domain.Entities.Employee() { Id = Guid.NewGuid() }
                ,
                EmployeeNotListed = "employee not listed",
                GuidanceNotes = "the guidance notes",
                SupportingEvidence = "the supporting evidence",
                Question = new Question() { Id = new Guid() }
                ,
                Response = null
                ,
                Timescale = new Timescale() { Id = 2L }
                ,
                LastModifiedBy = new UserForAuditing() { Id = Guid.NewGuid() },
                LastModifiedOn = originalLastModifiedByDate
            };

            var newAnswer = new ChecklistAnswer()
            {
                Id = originalAnswer.Id,
                ActionRequired = originalAnswer.ActionRequired,
                AssignedTo = originalAnswer.AssignedTo,
                EmployeeNotListed = originalAnswer.EmployeeNotListed,
                GuidanceNotes = originalAnswer.GuidanceNotes,
                SupportingEvidence = originalAnswer.SupportingEvidence,
                Question = originalAnswer.Question,
                Response = new QuestionResponse() { Id = Guid.NewGuid() },
                Timescale = originalAnswer.Timescale
            };

            var newAnswers = new List<ChecklistAnswer>() { newAnswer };

            var checklist = new Checklist();
            checklist.Answers.Add(originalAnswer);
            checklist.UpdateAnswers(newAnswers, new UserForAuditing());

            // When
            Assert.That(checklist.Answers.Count, Is.EqualTo(1));
            Assert.That(checklist.Answers.First().Response.Id, Is.EqualTo(newAnswer.Response.Id));
            Assert.That(checklist.Answers.First().LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
        }

        [Test]
        public void Given_actionrequired_changed_when_UpdateAnswers_then_lastmodifiedDate_updated()
        {
            var originalLastModifiedByDate = DateTime.Now.AddDays(-5);

            // Given
            var originalAnswer = new ChecklistAnswer()
            {
                Id = Guid.NewGuid(),
                ActionRequired = null,
                AssignedTo = new BusinessSafe.Domain.Entities.Employee() { Id = Guid.NewGuid() }
                ,
                EmployeeNotListed = "employee not listed",
                GuidanceNotes = "the guidance notes",
                SupportingEvidence = "the supporting evidence",
                Question = new Question() { Id = new Guid() }
                ,
                Response = new QuestionResponse(){Id = Guid.NewGuid()}
                ,
                Timescale = new Timescale() { Id = 2L }
                ,
                LastModifiedBy = new UserForAuditing() { Id = Guid.NewGuid() },
                LastModifiedOn = originalLastModifiedByDate
            };

            var newAnswer = new ChecklistAnswer()
            {
                Id = originalAnswer.Id,
                ActionRequired = "new action required",
                AssignedTo = originalAnswer.AssignedTo,
                EmployeeNotListed = originalAnswer.EmployeeNotListed,
                GuidanceNotes = originalAnswer.GuidanceNotes,
                SupportingEvidence = originalAnswer.SupportingEvidence,
                Question = originalAnswer.Question,
                Response =originalAnswer.Response,
                Timescale = originalAnswer.Timescale
            };

            var newAnswers = new List<ChecklistAnswer>() { newAnswer };

            var checklist = new Checklist();
            checklist.Answers.Add(originalAnswer);
            checklist.UpdateAnswers(newAnswers, new UserForAuditing());

            // When
            Assert.That(checklist.Answers.Count, Is.EqualTo(1));
            Assert.That(checklist.Answers.First().ActionRequired, Is.EqualTo(newAnswer.ActionRequired));
            Assert.That(checklist.Answers.First().LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
        }

        [Test]
        public void Given_assignedTo_changed_when_UpdateAnswers_then_lastmodifiedDate_updated()
        {
            var originalLastModifiedByDate = DateTime.Now.AddDays(-5);

            // Given
            var originalAnswer = new ChecklistAnswer()
            {
                Id = Guid.NewGuid(),
                ActionRequired = null,
                AssignedTo = new BusinessSafe.Domain.Entities.Employee() { Id = Guid.NewGuid() },
                EmployeeNotListed = "employee not listed",
                GuidanceNotes = "the guidance notes",
                SupportingEvidence = "the supporting evidence",
                Question = new Question() { Id = new Guid() },
                Response = new QuestionResponse() { Id = Guid.NewGuid() },
                Timescale = new Timescale() { Id = 2L },
                LastModifiedBy = new UserForAuditing() { Id = Guid.NewGuid() },
                LastModifiedOn = originalLastModifiedByDate
            };

            var newAnswer = new ChecklistAnswer()
            {
                Id = originalAnswer.Id,
                ActionRequired = originalAnswer.ActionRequired,
                AssignedTo = new BusinessSafe.Domain.Entities.Employee() { Id = Guid.NewGuid() },
                EmployeeNotListed = originalAnswer.EmployeeNotListed,
                GuidanceNotes = originalAnswer.GuidanceNotes,
                SupportingEvidence = originalAnswer.SupportingEvidence,
                Question = originalAnswer.Question,
                Response = originalAnswer.Response,
                Timescale = originalAnswer.Timescale,
            };

            var newAnswers = new List<ChecklistAnswer>() { newAnswer };

            var checklist = new Checklist();
            checklist.Answers.Add(originalAnswer);
            checklist.UpdateAnswers(newAnswers, new UserForAuditing());

            // When
            Assert.That(checklist.Answers.Count, Is.EqualTo(1));
            Assert.That(checklist.Answers.First().AssignedTo, Is.EqualTo(newAnswer.AssignedTo));
            Assert.That(checklist.Answers.First().LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
        }

        [Test]
        public void Given_employeeNotListed_changed_when_UpdateAnswers_then_lastmodifiedDate_updated()
        {
            var originalLastModifiedByDate = DateTime.Now.AddDays(-5);

            // Given
            var originalAnswer = new ChecklistAnswer()
            {
                Id = Guid.NewGuid(),
                ActionRequired = null,
                AssignedTo = new BusinessSafe.Domain.Entities.Employee() { Id = Guid.NewGuid() },
                EmployeeNotListed = "employee not listed",
                GuidanceNotes = "the guidance notes",
                SupportingEvidence = "the supporting evidence",
                Question = new Question() { Id = new Guid() },
                Response = new QuestionResponse(){Id = Guid.NewGuid()},
                Timescale = new Timescale() { Id = 2L },
                LastModifiedBy = new UserForAuditing() { Id = Guid.NewGuid() },
                LastModifiedOn = originalLastModifiedByDate
            };

            var newAnswer = new ChecklistAnswer()
            {
                Id = originalAnswer.Id,
                ActionRequired = originalAnswer.ActionRequired,
                AssignedTo = originalAnswer.AssignedTo,
                EmployeeNotListed = "new emp not listed",
                GuidanceNotes = originalAnswer.GuidanceNotes,
                SupportingEvidence = originalAnswer.SupportingEvidence,
                Question = originalAnswer.Question,
                Response = originalAnswer.Response,
                Timescale = originalAnswer.Timescale
            };

            var newAnswers = new List<ChecklistAnswer>() { newAnswer };

            var checklist = new Checklist();
            checklist.Answers.Add(originalAnswer);
            checklist.UpdateAnswers(newAnswers, new UserForAuditing());

            // When
            Assert.That(checklist.Answers.Count, Is.EqualTo(1));
            Assert.That(checklist.Answers.First().EmployeeNotListed, Is.EqualTo(newAnswer.EmployeeNotListed));
            Assert.That(checklist.Answers.First().LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
        }

        [Test]
        public void Given_GuidanceNotes_changed_when_UpdateAnswers_then_lastmodifiedDate_updated()
        {
            var originalLastModifiedByDate = DateTime.Now.AddDays(-5);

            // Given
            var originalAnswer = new ChecklistAnswer()
            {
                Id = Guid.NewGuid(),
                ActionRequired = null,
                AssignedTo = new BusinessSafe.Domain.Entities.Employee() { Id = Guid.NewGuid() },
                EmployeeNotListed = "employee not listed",
                GuidanceNotes = "the guidance notes",
                SupportingEvidence = "the supporting evidence",
                Question = new Question() { Id = new Guid() },
                Response = new QuestionResponse(){Id=Guid.NewGuid()},
                Timescale = new Timescale() { Id = 2L },
                LastModifiedBy = new UserForAuditing() { Id = Guid.NewGuid() },
                LastModifiedOn = originalLastModifiedByDate
            };

            var newAnswer = new ChecklistAnswer()
            {
                Id = originalAnswer.Id,
                ActionRequired = originalAnswer.ActionRequired,
                AssignedTo = originalAnswer.AssignedTo,
                EmployeeNotListed = originalAnswer.EmployeeNotListed,
                GuidanceNotes = "new guidance notes",
                SupportingEvidence = originalAnswer.SupportingEvidence,
                Question = originalAnswer.Question,
                Response = originalAnswer.Response,
                Timescale = originalAnswer.Timescale
            };

            var newAnswers = new List<ChecklistAnswer>() { newAnswer };

            var checklist = new Checklist();
            checklist.Answers.Add(originalAnswer);
            checklist.UpdateAnswers(newAnswers, new UserForAuditing());

            // When
            Assert.That(checklist.Answers.Count, Is.EqualTo(1));
            Assert.That(checklist.Answers.First().GuidanceNotes, Is.EqualTo(newAnswer.GuidanceNotes));
            Assert.That(checklist.Answers.First().LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
        }

        [Test]
        public void Given_supportingevidence_changed_when_UpdateAnswers_then_lastmodifiedDate_updated()
        {
            var originalLastModifiedByDate = DateTime.Now.AddDays(-5);

            // Given
            var originalAnswer = new ChecklistAnswer()
            {
                Id = Guid.NewGuid(),
                ActionRequired = null,
                AssignedTo = new BusinessSafe.Domain.Entities.Employee() { Id = Guid.NewGuid() },
                EmployeeNotListed = "employee not listed",
                GuidanceNotes = "the guidance notes",
                SupportingEvidence = "the supporting evidence",
                Question = new Question() { Id = new Guid() },
                Response = new QuestionResponse() { Id = Guid.NewGuid() },
                Timescale = new Timescale() { Id = 2L },
                LastModifiedBy = new UserForAuditing() { Id = Guid.NewGuid() },
                LastModifiedOn = originalLastModifiedByDate
            };

            var newAnswer = new ChecklistAnswer()
            {
                Id = originalAnswer.Id,
                ActionRequired = originalAnswer.ActionRequired,
                AssignedTo = originalAnswer.AssignedTo,
                EmployeeNotListed = originalAnswer.EmployeeNotListed,
                GuidanceNotes = originalAnswer.GuidanceNotes,
                SupportingEvidence = "new supporting evidence",
                Question = originalAnswer.Question,
                Response = originalAnswer.Response,
                Timescale = originalAnswer.Timescale
            };

            var newAnswers = new List<ChecklistAnswer>() { newAnswer };

            var checklist = new Checklist();
            checklist.Answers.Add(originalAnswer);
            checklist.UpdateAnswers(newAnswers, new UserForAuditing());

            // When
            Assert.That(checklist.Answers.Count, Is.EqualTo(1));
            Assert.That(checklist.Answers.First().SupportingEvidence, Is.EqualTo(newAnswer.SupportingEvidence));
            Assert.That(checklist.Answers.First().LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
        }

        [Test]
        public void Given_timescale_changed_when_UpdateAnswers_then_lastmodifiedDate_updated()
        {
            var originalLastModifiedByDate = DateTime.Now.AddDays(-5);

            // Given
            var originalAnswer = new ChecklistAnswer()
            {
                Id = Guid.NewGuid(),
                ActionRequired = null,
                AssignedTo = new BusinessSafe.Domain.Entities.Employee() { Id = Guid.NewGuid() },
                EmployeeNotListed = "employee not listed",
                GuidanceNotes = "the guidance notes",
                SupportingEvidence = "the supporting evidence",
                Question = new Question() { Id = new Guid() },
                Response = new QuestionResponse() { Id = Guid.NewGuid() },
                Timescale = new Timescale() { Id = 2L },
                LastModifiedBy = new UserForAuditing() { Id = Guid.NewGuid() },
                LastModifiedOn = originalLastModifiedByDate
            };

            var newAnswer = new ChecklistAnswer()
            {
                Id = originalAnswer.Id,
                ActionRequired = originalAnswer.ActionRequired,
                AssignedTo = originalAnswer.AssignedTo,
                EmployeeNotListed = originalAnswer.EmployeeNotListed,
                GuidanceNotes = originalAnswer.GuidanceNotes,
                SupportingEvidence = originalAnswer.SupportingEvidence,
                Question = originalAnswer.Question,
                Response = originalAnswer.Response,
                Timescale = new Timescale() { Id = 12L }
            };

            var newAnswers = new List<ChecklistAnswer>() { newAnswer };

            var checklist = new Checklist();
            checklist.Answers.Add(originalAnswer);
            checklist.UpdateAnswers(newAnswers, new UserForAuditing());

            // When
            Assert.That(checklist.Answers.Count, Is.EqualTo(1));
            Assert.That(checklist.Answers.First().Timescale, Is.EqualTo(newAnswer.Timescale));
            Assert.That(checklist.Answers.First().LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
        }

        [Test]
        public void Given_answer_is_signed_off_by_a_QA_then_qa_name_and_date_are_saved()
        {

            // Given
            var originalAnswer = new ChecklistAnswer()
            {
                Id = Guid.NewGuid(),
                Question = new Question() { Id = new Guid() },
                Response = new QuestionResponse(){Id = Guid.NewGuid()}
                ,LastModifiedBy = new UserForAuditing() { Id = Guid.NewGuid() },
                LastModifiedOn = DateTime.Now.AddDays(-5)
            };

            var newAnswer = new ChecklistAnswer()
            {
                Id = originalAnswer.Id,
                Question = originalAnswer.Question,
                QaSignedOffBy = "David Brieley"
                ,QaSignedOffDate = DateTime.Now
                ,Response= originalAnswer.Response
            };

            
            var newAnswers = new List<ChecklistAnswer>() { newAnswer };

            var checklist = new Checklist();
            checklist.Answers.Add(originalAnswer);
            checklist.UpdateAnswers(newAnswers, new UserForAuditing());


            Assert.That(checklist.Answers.First().QaSignedOffBy, Is.EqualTo(newAnswer.QaSignedOffBy));
            Assert.That(checklist.Answers.First().QaSignedOffDate.Value, Is.EqualTo(newAnswer.QaSignedOffDate.Value));

        }

        [Test]
        public void Given_answer_QA_signed_off_date_hasnt_change_then_qa_name_and_date_are_not_saved()
        {
            var originalQaSignedOffDate = new DateTime(2013, 12, 01, 15, 15, 15, 123);
            var originalLastModifiedOn = DateTime.Now.AddDays(-5);


            // Given
            var originalAnswer = new ChecklistAnswer()
            {
                Id = Guid.NewGuid(),
                Question = new Question() { Id = new Guid() },
                LastModifiedBy = new UserForAuditing() { Id = Guid.NewGuid() },
                LastModifiedOn = originalLastModifiedOn,
                QaSignedOffBy = "David Brieley"
                ,
                QaSignedOffDate = originalQaSignedOffDate
            };

            var newAnswer = new ChecklistAnswer()
            {
                Id = originalAnswer.Id,
                Question = originalAnswer.Question,
                QaSignedOffBy = "David Brieley"
                ,
                QaSignedOffDate = new DateTime(2013, 12, 01, 15, 15, 15, 12)

            };

            var newAnswers = new List<ChecklistAnswer>() { newAnswer };

            var checklist = new Checklist();
            checklist.Answers.Add(originalAnswer);
            checklist.UpdateAnswers(newAnswers, new UserForAuditing());

            Assert.That(checklist.Answers.First().LastModifiedOn, Is.EqualTo(originalLastModifiedOn));
            Assert.That(checklist.Answers.First().QaSignedOffDate.Value, Is.EqualTo(originalQaSignedOffDate));
        }

        [Test]
        public void Given_answer_QA_signed_off_date_has_been_removed_then_date_is_null()
        {
            var originalQaSignedOffDate = new DateTime(2013, 12, 01, 15, 15, 15, 123);

            // Given
            var originalAnswer = new ChecklistAnswer()
            {
                Id = Guid.NewGuid(),
                Question = new Question() { Id = new Guid() },
                LastModifiedBy = new UserForAuditing() { Id = Guid.NewGuid() },
                LastModifiedOn = DateTime.Now.AddDays(-5),
                QaSignedOffBy = "David Brieley",
                QaSignedOffDate = originalQaSignedOffDate,
                Response = new QuestionResponse() { Id = Guid.NewGuid() },
            };

            var newAnswer = new ChecklistAnswer()
            {
                Id = originalAnswer.Id,
                Question = originalAnswer.Question,
                QaSignedOffBy = "David Brieley",
                QaSignedOffDate = null,
                Response = originalAnswer.Response
            };

            var newAnswers = new List<ChecklistAnswer>() { newAnswer };

            var checklist = new Checklist();
            checklist.Answers.Add(originalAnswer);
            checklist.UpdateAnswers(newAnswers, new UserForAuditing());

            Assert.That(checklist.Answers.First().QaSignedOffDate, Is.Null);
        }

        [Test]
        public void Given_answer_QA_signed_off_date_is_null_then_lastmodifed_not_changed()
        {
            var originalLastModifiedOn = DateTime.Now.AddDays(-5);

            // Given
            var originalAnswer = new ChecklistAnswer()
            {
                Id = Guid.NewGuid(),
                Question = new Question() { Id = new Guid() }
                ,
                LastModifiedOn = originalLastModifiedOn
            };

            var newAnswer = new ChecklistAnswer()
            {
                Id = originalAnswer.Id,
                Question = originalAnswer.Question,
            };

            var newAnswers = new List<ChecklistAnswer>() { newAnswer };

            var checklist = new Checklist();
            checklist.Answers.Add(originalAnswer);
            checklist.UpdateAnswers(newAnswers, new UserForAuditing());

            Assert.That(checklist.Answers.First().LastModifiedOn, Is.EqualTo(originalLastModifiedOn));
        }

        [Test]
        public void Given_answer_area_of_non_compliance_changed_then_area_of_non_compliance_updated()
        {
            var originalLastModifiedOn = DateTime.Now.AddDays(-5);

            // Given
            var originalAnswer = new ChecklistAnswer()
            {
                Id = Guid.NewGuid(),
                Question = new Question() { Id = new Guid() }
                ,Response = new QuestionResponse(){Id = Guid.NewGuid()}
                ,LastModifiedOn = originalLastModifiedOn
            };

            var newAnswer = new ChecklistAnswer()
            {
                Id = originalAnswer.Id,
                Question = originalAnswer.Question,
                Response = originalAnswer.Response,
                AreaOfNonCompliance = "you have 1 second to comply"
            };

            var newAnswers = new List<ChecklistAnswer>() { newAnswer };

            var checklist = new Checklist();
            checklist.Answers.Add(originalAnswer);
            checklist.UpdateAnswers(newAnswers, new UserForAuditing());

            Assert.That(checklist.Answers.First().AreaOfNonCompliance, Is.EqualTo(newAnswer.AreaOfNonCompliance));
            Assert.That(checklist.Answers.First().LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
        }

        [Test]
        public void Given_answer_supporting_documentation_status_changed_then_supporting_documentation_status_updated()
        {
            var originalLastModifiedOn = DateTime.Now.AddDays(-5);

            // Given
            var originalAnswer = new ChecklistAnswer()
            {
                Id = Guid.NewGuid(),
                Question = new Question() { Id = new Guid() },
                LastModifiedOn = originalLastModifiedOn,
                Response = new QuestionResponse() { Id = Guid.NewGuid() }
            };

            var newAnswer = new ChecklistAnswer()
            {
                Id = originalAnswer.Id,
                Question = originalAnswer.Question,
                SupportingDocumentationStatus = "Reported",
                Response = originalAnswer.Response
            };

            var newAnswers = new List<ChecklistAnswer>() { newAnswer };

            var checklist = new Checklist();
            checklist.Answers.Add(originalAnswer);
            checklist.UpdateAnswers(newAnswers, new UserForAuditing());

            Assert.That(checklist.Answers.First().SupportingDocumentationStatus, Is.EqualTo(newAnswer.SupportingDocumentationStatus));
            Assert.That(checklist.Answers.First().LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
        }

        [Test]
        public void Given_answer_supporting_documentation_date_changed_then_supporting_documentation_date_updated()
        {
            var originalLastModifiedOn = DateTime.Now.AddDays(-5);

            // Given
            var originalAnswer = new ChecklistAnswer()
            {
                Id = Guid.NewGuid(),
                Question = new Question() { Id = new Guid() },
                Response = new QuestionResponse() { Id = Guid.NewGuid() }
                ,
                LastModifiedOn = originalLastModifiedOn,
                SupportingDocumentationStatus = "Reported"
            };

            var newAnswer = new ChecklistAnswer()
            {
                Id = originalAnswer.Id,
                Question = originalAnswer.Question,
                SupportingDocumentationStatus = "Reported",
                SupportingDocumentationDate =  DateTime.Now.AddDays(-51),
                Response = originalAnswer.Response

            };

            var newAnswers = new List<ChecklistAnswer>() { newAnswer };

            var checklist = new Checklist();
            checklist.Answers.Add(originalAnswer);
            checklist.UpdateAnswers(newAnswers, new UserForAuditing());

            Assert.That(checklist.Answers.First().SupportingDocumentationDate, Is.EqualTo(newAnswer.SupportingDocumentationDate));
            Assert.That(checklist.Answers.First().LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
        }


        [Test]
        public void Given_answer_has_previously_been_given_when_a_question_without_an_answer_is_saved_then_original_answer_is_not_removed()
        {
            var originalResponseId = Guid.NewGuid();
            var originalLastModifiedOn = DateTime.Now.AddDays(-5);

            // Given
            var originalAnswer = new ChecklistAnswer()
            {
                Id = Guid.NewGuid(),
                Question = new Question() {Id = new Guid()},
                LastModifiedOn = originalLastModifiedOn,
                SupportingDocumentationStatus = "Reported",
                Response = new QuestionResponse() {Id = originalResponseId}
            };

            var newAnswer = new ChecklistAnswer()
            {
                Id = originalAnswer.Id,
                Question = originalAnswer.Question,
                Response = null
            };

            var newAnswers = new List<ChecklistAnswer>() {newAnswer};

            var checklist = new Checklist();
            checklist.Answers.Add(originalAnswer);
            checklist.UpdateAnswers(newAnswers, new UserForAuditing());


            Assert.That(checklist.Answers.First().SupportingDocumentationStatus, Is.EqualTo("Reported"));
            Assert.That(checklist.Answers.First().LastModifiedOn.Value, Is.EqualTo(originalLastModifiedOn));
            Assert.That(checklist.Answers.First().Response.Id, Is.EqualTo(originalResponseId));
        }

    }
}
