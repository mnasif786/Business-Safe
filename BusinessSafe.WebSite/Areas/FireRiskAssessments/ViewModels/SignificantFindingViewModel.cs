using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels
{
    public class SignificantFindingViewModel
    {
        public long Id { get; set; }
        public string QuestionText { get; set; }
        public int QuestionNumber { get; set; }
        public string AdditionalInfo { get; set; }
        public IEnumerable<FurtherControlMeasureTaskViewModel> ActionsRequired { get; set; }

        public static SignificantFindingViewModel CreateFrom(SignificantFindingDto significantFinding)
        {
            return new SignificantFindingViewModel()
                       {
                           Id = significantFinding.Id,
                           QuestionText = GetQuestionText(significantFinding),
                           QuestionNumber = GetQuestionNumber(significantFinding),
                           ActionsRequired = FurtherControlMeasureTaskViewModel.CreateFrom(significantFinding.FurtherActionTasks),
                           AdditionalInfo = GetAdditionalInfo(significantFinding)
                       };
        }

        private static int GetQuestionNumber(SignificantFindingDto significantFinding)
        {
            return significantFinding.FireAnswer != null && significantFinding.FireAnswer.Question != null
                       ? significantFinding.FireAnswer.Question.ListOrder : 0;
        }

        private static string GetQuestionText(SignificantFindingDto significantFinding)
        {
            return significantFinding.FireAnswer != null && significantFinding.FireAnswer.Question != null
                       ? significantFinding.FireAnswer.Question.Text + " - No "
                       : string.Empty;
        }

        private static string GetAdditionalInfo(SignificantFindingDto significantFinding)
        {
            return significantFinding.FireAnswer != null && significantFinding.FireAnswer.Question != null
                       ? significantFinding.FireAnswer.AdditionalInfo
                       : string.Empty;
        }

      

        public static IEnumerable<SignificantFindingViewModel> CreateFrom(IEnumerable<SignificantFindingDto> significantFindings)
        {
            return significantFindings.Select(CreateFrom).OrderBy(x => x.QuestionNumber);
        }


    }
}
