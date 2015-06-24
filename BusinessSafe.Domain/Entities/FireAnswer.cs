using System;
using System.Linq;

namespace BusinessSafe.Domain.Entities
{
    public class FireAnswer : Answer
    {
        public virtual YesNoNotApplicableEnum? YesNoNotApplicableResponse { get; set; }
        public virtual FireRiskAssessmentChecklist FireRiskAssessmentChecklist { get; set; }
        public virtual SignificantFinding SignificantFinding { get; set; }

        public static FireAnswer Create(
            FireRiskAssessmentChecklist fireRiskAssessmentChecklist,
            Question question,
            YesNoNotApplicableEnum? yesNoNotApplicableResponse,
            string additionalInfo,
            UserForAuditing user)
        {
            var fireAnswer = new FireAnswer
                                 {
                                     FireRiskAssessmentChecklist = fireRiskAssessmentChecklist,
                                     Question = question,
                                     YesNoNotApplicableResponse = yesNoNotApplicableResponse,
                                     AdditionalInfo = additionalInfo,
                                     CreatedOn = DateTime.Now,
                                     CreatedBy = user
                                 };

            if (yesNoNotApplicableResponse == YesNoNotApplicableEnum.No)
            {
                var significantFinding = SignificantFinding.Create(fireAnswer, user);
                fireAnswer.SignificantFinding = significantFinding;
            }

            return fireAnswer;
        }

        public virtual void Update(YesNoNotApplicableEnum? newAnswer, string additionalInfo, UserForAuditing user)
        {
            if (!IsAnswerDifferent(newAnswer, additionalInfo))
                return;

            if (newAnswer != YesNoNotApplicableEnum.No)
            {
                if (SignificantFinding != null)
                {
                    SignificantFinding.MarkForDelete(user);
                }
            }
            else if (newAnswer == YesNoNotApplicableEnum.No)
            {
                if (SignificantFinding == null)
                {
                    SignificantFinding = SignificantFinding.Create(this, user);
                }
                else
                {
                    SignificantFinding.ReinstateFromDelete(user);
                }
            }

            YesNoNotApplicableResponse = newAnswer;
            AdditionalInfo = additionalInfo;
            SetLastModifiedDetails(user);
        }

        private bool IsAnswerDifferent(YesNoNotApplicableEnum? newAnswer, string additionalInfo)
        {
            return YesNoNotApplicableResponse != newAnswer || AdditionalInfo != additionalInfo;
        }

        public virtual bool IsValidateForCompleteChecklist()
        {
            switch (YesNoNotApplicableResponse)
            {
                case YesNoNotApplicableEnum.No:
                    if (SignificantFinding == null)
                    {
                        return false;
                    }

                    if (SignificantFinding.FurtherControlMeasureTasks.Any(x => x.TaskAssignedTo != null && x.TaskAssignedTo.Id != Guid.Empty) == false)
                    {
                        return false;
                    }
                    break;
                case YesNoNotApplicableEnum.Yes:
                    if (string.IsNullOrEmpty(AdditionalInfo))
                    {
                        return false;
                    }
                    break;
            }

            return true;
        }
    }
}
