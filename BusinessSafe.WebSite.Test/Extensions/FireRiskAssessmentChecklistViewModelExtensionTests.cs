using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using FluentValidation.Results;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Extensions
{
    [TestFixture]
    public class FireRiskAssessmentChecklistViewModelExtensionTests
    {
        [TestCase("1", 1)]
        [TestCase("2", 2)]
        [TestCase("3", 3)]
        [TestCase("4", 4)]
        [TestCase("5", 5)]
        public void When_GetQuestionId_Then_returns_correct_answers(string questionId, long expectedResult)
        {
            // Given
            var error = new ValidationFailure(questionId, "blah blah blah");

            // When
            var result = error.GetQuestionId();

            // Then
            Assert.That(result, Is.EqualTo(expectedResult));
        }



        public void When_GetAllNoFireAnswerQuestionIds_Then_returns_correct_result()
        {
            // Given
            var viewModel = new FireRiskAssessmentChecklistViewModel()
                                {
                                    Sections = new List<SectionViewModel>()
                                                  {
                                                      new SectionViewModel()
                                                          {
                                                              Questions = new List<QuestionViewModel>()
                                                                              {
                                                                                  new QuestionViewModel()
                                                                                      {
                                                                                          Id = 5,
                                                                                          Answer = new FireAnswerViewModel()
                                                                                                       {
                                                                                                           YesNoNotApplicableResponse = YesNoNotApplicableEnum.No
                                                                                                       }
                                                                                      },
                                                                                  new QuestionViewModel()
                                                                                      {
                                                                                          Answer = new FireAnswerViewModel()
                                                                                                       {
                                                                                                           YesNoNotApplicableResponse = YesNoNotApplicableEnum.Yes
                                                                                                       }
                                                                                      }
                                                                              }
                                                          },

                                                      new SectionViewModel()
                                                          {
                                                              Questions = new List<QuestionViewModel>()
                                                                              {
                                                                                  new QuestionViewModel()
                                                                                      {
                                                                                          Id = 10,
                                                                                          Answer = new FireAnswerViewModel()
                                                                                                       {
                                                                                                           YesNoNotApplicableResponse = YesNoNotApplicableEnum.No
                                                                                                       }
                                                                                      },
                                                                                  new QuestionViewModel()
                                                                                      {
                                                                                          Answer = new FireAnswerViewModel()
                                                                                                       {
                                                                                                           YesNoNotApplicableResponse = YesNoNotApplicableEnum.Yes
                                                                                                       }
                                                                                      }
                                                                              }
                                                          }
                                                  }
                                };

            // When
            var result = viewModel.GetAllNonNotApplicableFireAnswerQuestionIds();

            // Then
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.Count(x => x == 5), Is.EqualTo(1));
            Assert.That(result.Count(x => x == 10), Is.EqualTo(1));
        }
    }
}