using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Domain.Entities;

using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.EmployeeChecklistsTests
{
    [TestFixture]
    public class AreAllQuestionsAnsweredTests
    {
        private object _target;
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Given_question_requires_booleanresponse_When_no_corresponding_answer_return_false()
        {
            // Given 
            var questions = new List<Question>()
                            {
                                new Question()
                                {
                                    Id = 1, 
                                    QuestionType = QuestionType.YesNo
                                }
                            };

            var checklist = new Checklist()
            {
                Sections = new List<Section>()
                                                         {
                                                             new Section()
                                                             {
                                                                 Questions = questions
                                                             }
                                                         }
            };

            var employeeChecklist = new EmployeeChecklist()
                                    {
                                        Checklist = checklist,
                                        Answers = new List<PersonalAnswer>()
                                    };

            // When
            var result = employeeChecklist.AreAllQuestionsAnswered();

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_question_requires_booleanresponse_When_answer_not_answered_return_false()
        {
            // Given 
            var questions = new List<Question>()
                            {
                                new Question()
                                {
                                    Id = 1, 
                                    QuestionType = QuestionType.YesNo
                                }
                            };

            var checklist = new Checklist()
            {
                Sections = new List<Section>()
                                                         {
                                                             new Section()
                                                             {
                                                                 Questions = questions
                                                             }
                                                         }
            };

            var employeeChecklist = new EmployeeChecklist()
            {
                Checklist = checklist,
                Answers = new List<PersonalAnswer>()
                          {
                              new PersonalAnswer() { Question = 
                                new Question()
                                {
                                    Id = 1, 
                                    QuestionType = QuestionType.YesNo
                                }}
                          }
            };

            // When
            var result = employeeChecklist.AreAllQuestionsAnswered();

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_question_requires_booleanresponse_When_answer_answered_return_true()
        {
            // Given 
            var questions = new List<Question>()
                            {
                                new Question()
                                {
                                    Id = 1, 
                                    QuestionType = QuestionType.YesNo
                                }
                            };

            var checklist = new Checklist()
            {
                Sections = new List<Section>()
                                                         {
                                                             new Section()
                                                             {
                                                                 Questions = questions
                                                             }
                                                         }
            };

            var employeeChecklist = new EmployeeChecklist()
            {
                Checklist = checklist,
                Answers = new List<PersonalAnswer>()
                          {
                              new PersonalAnswer() {
                                  BooleanResponse = true,
                                  Question = new Question()
                                {
                                    Id = 1, 
                                    QuestionType = QuestionType.YesNo
                                }}
                          }
            };

            // When
            var result = employeeChecklist.AreAllQuestionsAnswered();

            // Then
            Assert.IsTrue(result);
        }

        [Test]
        public void Given_an_answer_exists_when_DoesAnAnswerObjectExistForQuestion_then_return_true()
        {
            var questions = new List<Question>()
                            {
                                new Question()
                                {
                                    Id = 1, 
                                    QuestionType = QuestionType.YesNo
                                }
                            };

            var checklist = new Checklist()
            {
                Sections = new List<Section>()
                                                         {
                                                             new Section()
                                                             {
                                                                 Questions = questions
                                                             }
                                                         }
            };

            var employeeChecklist = new EmployeeChecklist()
            {
                Checklist = checklist,
                Answers = new List<PersonalAnswer>()
                          {
                              new PersonalAnswer() {
                                  BooleanResponse = true,
                                  Question = new Question()
                                {
                                    Id = 1, 
                                    QuestionType = QuestionType.YesNo
                                }}
                          }
            };
            var result = employeeChecklist.DoesAnAnswerObjectExistForQuestion(employeeChecklist.Answers[0].Question);

            Assert.IsTrue(result);
        }

        [Test]
        public void Given_an_answer_does_not_exist_when_DoesAnAnswerObjectExistForQuestion_then_return_false()
        {
            var questions = new List<Question>()
                            {
                                new Question()
                                {
                                    Id = 1, 
                                    QuestionType = QuestionType.YesNo
                                }
                            };

            var checklist = new Checklist()
            {
                Sections = new List<Section>()
                                                         {
                                                             new Section()
                                                             {
                                                                 Questions = questions
                                                             }
                                                         }
            };

            var employeeChecklist = new EmployeeChecklist()
            {
                Checklist = checklist,
                Answers = new List<PersonalAnswer>()
                         
            };
            var result = employeeChecklist.DoesAnAnswerObjectExistForQuestion(questions[0]);

            Assert.IsFalse(result);
        }

        [Test]
        public void Given_an_question_is_answered_when_IsQuestionAnswered_then_returns_true()
        {
            var questions = new List<Question>()
                            {
                                new Question()
                                {
                                    Id = 1, 
                                    QuestionType = QuestionType.YesNo
                                }
                            };

            var checklist = new Checklist()
            {
                Sections = new List<Section>()
                                                         {
                                                             new Section()
                                                             {
                                                                 Questions = questions
                                                             }
                                                         }
            };

            var employeeChecklist = new EmployeeChecklist()
            {
                Checklist = checklist,
                Answers = new List<PersonalAnswer>()
                          {
                              new PersonalAnswer() {
                                  BooleanResponse = true,
                                  Question = new Question()
                                {
                                    Id = 1, 
                                    QuestionType = QuestionType.YesNo
                                }}
                          }
            };
            var result = employeeChecklist.IsQuestionAnswered(employeeChecklist.Answers[0].Question);

            Assert.IsTrue(result);
        }

        [Test]
        public void Given_an_question_is_unanswered_when_IsQuestionAnswered_then_returns_false()
        {
            var questions = new List<Question>()
                            {
                                new Question()
                                {
                                    Id = 1, 
                                    QuestionType = QuestionType.YesNo
                                }
                            };

            var checklist = new Checklist()
            {
                Sections = new List<Section>()
                                                         {
                                                             new Section()
                                                             {
                                                                 Questions = questions
                                                             }
                                                         }
            };

            var employeeChecklist = new EmployeeChecklist()
            {
                Checklist = checklist,
                Answers = new List<PersonalAnswer>()
                          {
                              new PersonalAnswer() {
                                  BooleanResponse = null,
                                  Question = new Question()
                                {
                                    Id = 1, 
                                    QuestionType = QuestionType.YesNo
                                }}
                          }
            };
            var result = employeeChecklist.IsQuestionAnswered(employeeChecklist.Answers[0].Question);

            Assert.IsFalse(result);
        }

        [Test]
        public void Given_a_question_is_text_only_When_IsQuestionAnswered_returns_true()
        {
            var questions = new List<Question>()
                            {
                                new Question()
                                {
                                    Id = 1, 
                                    QuestionType = QuestionType.AdditionalInfo
                                }
                            };

            var checklist = new Checklist()
            {
                Sections = new List<Section>()
                                                         {
                                                             new Section()
                                                             {
                                                                 Questions = questions
                                                             }
                                                         }
            };

            var employeeChecklist = new EmployeeChecklist()
            {
                Checklist = checklist,
                Answers = new List<PersonalAnswer>()
                          {
                              new PersonalAnswer() {
                                  BooleanResponse = null,
                                  AdditionalInfo = "the answer",
                                  Question = new Question()
                                {
                                    Id = 1, 
                                    QuestionType = QuestionType.AdditionalInfo
                                }}
                          }
            };
            var result = employeeChecklist.IsQuestionAnswered(employeeChecklist.Answers[0].Question);

            Assert.IsTrue(result);
        }
    }
}
