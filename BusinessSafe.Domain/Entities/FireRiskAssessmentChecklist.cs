using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.ParameterClasses;

namespace BusinessSafe.Domain.Entities
{
    public class FireRiskAssessmentChecklist : Entity<long>
    {
        public virtual FireRiskAssessment FireRiskAssessment { get; set; }
        public virtual Checklist Checklist { get; set; }
        public virtual DateTime? CompletedDate { get; set; }
        public virtual IList<FireAnswer> Answers { get; set; }
        public virtual RiskAssessmentReview ReviewGeneratedFrom { get; set; }
        public virtual bool? HasCompleteFailureAttempt{ get; set; }
        
        public FireRiskAssessmentChecklist()
        {
            Answers = new List<FireAnswer>();
        }

        public virtual IList<SignificantFinding> SignificantFindings
        {
            get
            {
                return Answers
                    .Where(answer => answer.SignificantFinding != null)
                    .Select(answer => answer.SignificantFinding)
                    .ToList();
            }
        }
        public virtual void Save(IList<SubmitFireAnswerParameters> answerParameterClasses, UserForAuditing submittingUser)
        {
            SaveChecklist(answerParameterClasses, submittingUser);
        }

        public virtual void Complete(IList<SubmitFireAnswerParameters> answerParameterClasses, UserForAuditing submittingUser)
        {
            HasCompleteFailureAttempt = false;
            SaveChecklist(answerParameterClasses, submittingUser);
            CompletedDate = DateTime.Now;
        }

        private void SaveChecklist(IEnumerable<SubmitFireAnswerParameters> answerParameterClasses, UserForAuditing submittingUser)
        {
            SetAnswers(answerParameterClasses, submittingUser);
            SetLastModifiedDetails(submittingUser);
        }

        private void SetAnswers(IEnumerable<SubmitFireAnswerParameters> answerParameterClasses, UserForAuditing submittingUser)
        {
            foreach (var answerParameters in answerParameterClasses)
            {
                if (Answers.Any(x => x.Question.Id == answerParameters.Question.Id))
                {
                    var answer = Answers.Single(x => x.Question.Id == answerParameters.Question.Id);
                    answer.Update(answerParameters.YesNoNotApplicableResponse, answerParameters.AdditionalInfo, submittingUser);
                }
                else
                {
                    var answer = FireAnswer.Create(this, answerParameters.Question, answerParameters.YesNoNotApplicableResponse,
                                               answerParameters.AdditionalInfo, submittingUser);

                    Answers.Add(answer);
                }
            }
        }

        public virtual FireRiskAssessmentChecklist CopyWithYesAnswers(UserForAuditing user)
        {
            var copiedFraChecklist = new FireRiskAssessmentChecklist();
            copiedFraChecklist.CreatedBy = user;
            copiedFraChecklist.Checklist = Checklist;
            copiedFraChecklist.CreatedOn = DateTime.Now;
            copiedFraChecklist.FireRiskAssessment = FireRiskAssessment;
            
            var clonedYesAnswers = Answers
                .Where(x => x.YesNoNotApplicableResponse == YesNoNotApplicableEnum.Yes || x.YesNoNotApplicableResponse == YesNoNotApplicableEnum.NotApplicable)
                .Where(x => x.Deleted == false)
                .Select(x => new SubmitFireAnswerParameters { AdditionalInfo = x.AdditionalInfo, Question = x.Question, YesNoNotApplicableResponse = x.YesNoNotApplicableResponse })
                .ToList();
            copiedFraChecklist.SetAnswers(clonedYesAnswers,user);

            return copiedFraChecklist;
        }


        public virtual void MarkChecklistWithCompleteFailureAttempt(UserForAuditing user)
        {
            HasCompleteFailureAttempt = true;
            SetLastModifiedDetails(user);
        }
        
    }
}
