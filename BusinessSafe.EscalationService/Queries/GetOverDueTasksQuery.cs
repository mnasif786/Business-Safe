using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Entities;
using BusinessSafe.EscalationService.Entities;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.EscalationService.Queries
{
    public class GetOverDueTasksQuery: IGetOverDueTasksQuery
    {

        public IList<Task> Execute(ISession session)
        {
           return session
                .CreateCriteria<Task>()
                .SetFetchMode("MultiHazardRiskAssessmentHazard", FetchMode.Eager)
                .SetFetchMode("RiskAssessment", FetchMode.Eager)
                .SetFetchMode("Responsibility", FetchMode.Eager)
                .Add(Restrictions.Eq("TaskStatus", TaskStatus.Outstanding))
                .Add(Restrictions.Eq("Deleted", false))
                .Add(Restrictions.Not(Restrictions.Eq("class", typeof (RiskAssessmentReviewTask))))
                .Add(Restrictions.Lt("TaskCompletionDueDate", DateTime.Now.AddDays(-1)))
                .Add(Restrictions.Eq("SendTaskOverdueNotification", true))
                .Add(Subqueries.PropertyNotIn("Id",
                    DetachedCriteria.For<EscalationOverdueTask>()
                        .SetProjection(Projections.Property("TaskId"))
                        .Add(Restrictions.IsNotNull("OverdueEmailSentDate"))
                    ))
                .CreateAlias("RiskAssessmentReview", "riskAssessmentReview", JoinType.LeftOuterJoin)
                .CreateAlias("riskAssessmentReview.RiskAssessment", "RiskAssessmentReview_RiskAssessment",
                    JoinType.LeftOuterJoin)
                .CreateAlias("MultiHazardRiskAssessmentHazard", "riskAssessmentHazard", JoinType.LeftOuterJoin)
                .CreateAlias("riskAssessmentHazard.MultiHazardRiskAssessment", "RiskAssessmentHazard_RiskAssessment",
                    JoinType.LeftOuterJoin)
                .CreateAlias("HazardousSubstanceRiskAssessment", "hazardousSubstanceRiskAssessment",
                    JoinType.LeftOuterJoin)
                .CreateAlias("SignificantFinding", "significantFinding", JoinType.LeftOuterJoin)
                .CreateAlias("significantFinding.FireAnswer", "fireAnswer", JoinType.LeftOuterJoin)
                .CreateAlias("fireAnswer.FireRiskAssessmentChecklist", "fireRiskAssessmentChecklist",
                    JoinType.LeftOuterJoin)
                .CreateAlias("fireRiskAssessmentChecklist.FireRiskAssessment", "fireRiskAssessment",
                    JoinType.LeftOuterJoin)
                .CreateAlias("Responsibility", "responsibility", JoinType.LeftOuterJoin)
                .CreateAlias("Action", "action", JoinType.LeftOuterJoin)
                .CreateAlias("action.ActionPlan", "actionPlan", JoinType.LeftOuterJoin)

                .Add(Restrictions.Or(
                    Restrictions.Not(
                        Restrictions.Eq("class", typeof(RiskAssessmentReviewTask))
                        ),
                    Restrictions.Eq("RiskAssessmentReview_RiskAssessment.Deleted", false))
                )
                .Add(Restrictions.Or(
                    Restrictions.Not(
                        Restrictions.Eq("class", typeof(MultiHazardRiskAssessmentFurtherControlMeasureTask))
                        ),
                    Restrictions.Eq("RiskAssessmentHazard_RiskAssessment.Deleted", false))
                )
                .Add(Restrictions.Or(
                    Restrictions.Not(
                        Restrictions.Eq("class", typeof(HazardousSubstanceRiskAssessmentFurtherControlMeasureTask))
                        ),
                    Restrictions.Eq("hazardousSubstanceRiskAssessment.Deleted", false))
                )
                .Add(Restrictions.Or(
                    Restrictions.Not(
                        Restrictions.Eq("class", typeof(FireRiskAssessmentFurtherControlMeasureTask))
                        ),
                    Restrictions.Eq("fireRiskAssessment.Deleted", false))
                // Significant Finding Not Deleted Also???

                )
                .Add(Restrictions.Or(
                    Restrictions.Not(
                        Restrictions.Eq("class", typeof(ResponsibilityTask))
                        ),
                    Restrictions.Eq("responsibility.Deleted", false))
                )
                .Add(Restrictions.Or(
                    Restrictions.Not(
                        Restrictions.Eq("class", typeof(ActionTask))
                        ),
                    Restrictions.Eq("action.Deleted", false))
                )

                .SetMaxResults(10000)
                .SetResultTransformer(new DistinctRootEntityResultTransformer())
                .List<Task>();
        }
    }
}
