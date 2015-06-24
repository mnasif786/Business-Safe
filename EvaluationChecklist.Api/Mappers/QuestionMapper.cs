using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Entities.SafeCheck;
using EvaluationChecklist.Models;

namespace EvaluationChecklist.Mappers
{
    public static class QuestionMapper
    {
        public static QuestionViewModel Map(this Question question)
        {
            var questionViewModel = new QuestionViewModel()
                                        {
                                            Id = question.Id,
                                            Text = question.Title,
                                            CategoryId =  question.Category.Id
                                            ,
                                            Category = new CategoryViewModel() {Id = question.Category.Id, Title = question.Category.Title, OrderNumber =  question.Category.OrderNumber, TabTitle = question.Category.TabTitle},
                                            Mandatory = question.Mandatory,
                                            SpecificToClientId = question.SpecificToClientId,
                                            OrderNumber = question.OrderNumber
                                            ,Deleted = question.Deleted,
                                            CustomQuestion = question.CustomQuestion
                                        };

            if (question.PossibleResponses != null)
            {
                questionViewModel.PossibleResponses = question.PossibleResponses
                    .Select(
                        x =>
                            new QuestionResponseViewModel()
                            {
                                SupportingEvidence = x.SupportingEvidence,
                                ActionRequired = x.ActionRequired,
                                ResponseType = x.ResponseType,
                                GuidanceNotes = x.GuidanceNotes,
                                Title = x.Title,
                                Id = x.Id,
                                ReportLetterStatement = x.ReportLetterStatement,
                                ReportLetterStatementCategory =
                                    x.ReportLetterStatementCategory != null
                                        ? x.ReportLetterStatementCategory.Name
                                        : string.Empty
                            }).Distinct(new QuestionResponseViewModel.PossibleResponsesComparer()).ToList();
            }

            return questionViewModel;
        }
        
        public static List<QuestionViewModel> Map(this IEnumerable<Question> questions)
        {
            return questions
                .Where(x=> x.Category != null)
                .Select(x => Map((Question)x)).OrderBy(y => y.Category.Title).ToList();
        }
    }

    public static class CategoryQuestionAnswerViewModelMapper
    {
        public static List<CategoryQuestionAnswerViewModel> DistinctListOfCategories(this IEnumerable<ChecklistQuestion> checklistQuestions)
        {
            return checklistQuestions.Select(x => x.Question.Category)
                .OrderBy(x => x.OrderNumber)
                .GroupBy(x => x.Title)
                .Select(x => x.First())
                .Select(x => new CategoryQuestionAnswerViewModel() {Id = x.Id, Title = x.Title, TabTitle = x.TabTitle})
                .ToList();
        }
    }

   
}