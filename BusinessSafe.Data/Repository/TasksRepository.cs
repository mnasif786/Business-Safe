using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Data.Common;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class TasksRepository : Repository<Task, long>, ITasksRepository
    {
        public TasksRepository(IBusinessSafeSessionManager sessionManager) : base(sessionManager) { }


        public Task GetByIdAndCompanyId(long id, long companyId)
        {
            var query = SessionManager.Session.Query<Task>()
                .Where(t => t.Id == id)
                .Where(t => t.TaskAssignedTo.CompanyId == companyId)
                .Where(t => t.Deleted == false);

            var result = query.ToList();

            if (result == null || !result.Any())
                throw new TaskNotFoundException(id);

            return result[0];
        }

        public IEnumerable<Task> Search(long companyId, IEnumerable<Guid> employeeIds, DateTime? createdFrom,
                                        DateTime? createdTo, DateTime? completedFrom, DateTime? completedTo,
                                        long taskCategoryId, int taskStatusId, bool showDeleted, bool showCompleted,
                                        IEnumerable<long> siteIds, string title, int page, int pageSize,
                                        TaskOrderByColumn orderBy, bool ascending)
        {

            var result = CreateCriteria(companyId, employeeIds, createdFrom,
                                        createdTo, completedFrom, completedTo,
                                        taskCategoryId, taskStatusId, showDeleted, showCompleted,
                                        siteIds, title);


            var columnName = GetOrderColumnName(orderBy);
            result.AddOrder(new Order(columnName, ascending));


            if (page != default(int))
            {
                result.SetFirstResult(page == 1 ? 0 : (page - 1) * pageSize).SetMaxResults(pageSize);
            }

            result.SetResultTransformer(new DistinctRootEntityResultTransformer());

            return result.List<Task>();
        }

        public int Count(long companyId, IEnumerable<Guid> employeeIds, DateTime? createdFrom, DateTime? createdTo,
                         DateTime? completedFrom, DateTime? completedTo, long taskCategoryId, int taskStatusId,
                         bool showDeleted, bool showCompleted, IEnumerable<long> siteIds, string title)
        {
            var criteria = CreateCriteria(companyId, employeeIds, createdFrom,
                            createdTo, completedFrom, completedTo,
                            taskCategoryId, taskStatusId, showDeleted, showCompleted,
                            siteIds, title);

            var count = criteria.SetProjection(Projections.RowCount()).FutureValue<Int32>();
            return count.Value;
        }

        private ICriteria CreateCriteria(long companyId, IEnumerable<Guid> employeeIds, DateTime? createdFrom,
                                        DateTime? createdTo, DateTime? completedFrom, DateTime? completedTo,
                                        long taskCategoryId, int taskStatusId, bool showDeleted, bool showCompleted,
                                        IEnumerable<long> siteIds, string title)
        {
            var result = SessionManager.Session
             .CreateCriteria<Task>()
             .CreateAlias("TaskAssignedTo", "tat", JoinType.InnerJoin)
             .SetFetchMode("tat", FetchMode.Eager)
             .CreateAlias("Category", "category", JoinType.InnerJoin)
             .SetFetchMode("category", FetchMode.Eager)
             .SetFetchMode("Documents", FetchMode.Lazy)
             .Add(Restrictions.Eq("tat.CompanyId", companyId));

            if (employeeIds != null && employeeIds.Any())
                result.Add(Restrictions.In("tat.Id", employeeIds.ToList()));

            if (createdFrom.HasValue)
                result.Add((Restrictions.Ge("CreatedOn", createdFrom.Value)));

            if (createdTo.HasValue)
                result.Add((Restrictions.Le("CreatedOn", createdTo.Value)));

            if (completedFrom.HasValue)
                result.Add((Restrictions.Ge("TaskCompletionDueDate", completedFrom.Value)));

            if (completedTo.HasValue)
                result.Add((Restrictions.Le("TaskCompletionDueDate", completedTo.Value)));

            if (taskCategoryId > 0)
                result.Add(Restrictions.Eq("Category.Id", taskCategoryId));

            result.Add(Restrictions.Eq("Deleted", showDeleted));

            if (!string.IsNullOrEmpty(title))
            {
                result.Add(Restrictions.Like("Title", string.Format("%{0}%", title)));
            }

            //We have a problem on the feature where if you delete a RA, you need to delete associated tasks, currently we just set tasks to deleted.
            //But there may come a requirement where if we undelete a RA then only the tasks that were deleted with the RA come back, not others that were deleted
            //before. So instead of deleting those tasks with the RA, we should filter out tasks whose RA is deleted. This is a problem because different tasks
            //have different relationships with RA.
            result
                .CreateAlias("RiskAssessmentReview", "riskAssessmentReview", JoinType.LeftOuterJoin)
                .CreateAlias("riskAssessmentReview.RiskAssessment", "RiskAssessmentReview_RiskAssessment", JoinType.LeftOuterJoin)
                .CreateAlias("MultiHazardRiskAssessmentHazard", "riskAssessmentHazard", JoinType.LeftOuterJoin)
                .CreateAlias("riskAssessmentHazard.MultiHazardRiskAssessment", "RiskAssessmentHazard_RiskAssessment", JoinType.LeftOuterJoin)
                .CreateAlias("HazardousSubstanceRiskAssessment", "hazardousSubstanceRiskAssessment", JoinType.LeftOuterJoin)
                .CreateAlias("SignificantFinding", "significantFinding", JoinType.LeftOuterJoin)
                .CreateAlias("significantFinding.FireAnswer", "fireAnswer", JoinType.LeftOuterJoin)
                .CreateAlias("fireAnswer.FireRiskAssessmentChecklist", "fireRiskAssessmentChecklist", JoinType.LeftOuterJoin)
                .CreateAlias("fireRiskAssessmentChecklist.FireRiskAssessment", "fireRiskAssessment", JoinType.LeftOuterJoin)
                .CreateAlias("Responsibility", "responsibility", JoinType.LeftOuterJoin)
                .CreateAlias("Action", "action", JoinType.LeftOuterJoin)
                .CreateAlias("action.ActionPlan", "actionPlan", JoinType.LeftOuterJoin)
                .SetFetchMode("riskAssessmentReview", FetchMode.Eager)

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
                     );

            result.Add(showCompleted
                           ? Restrictions.Eq("TaskStatus", TaskStatus.Completed)
                           : Restrictions.Not(Restrictions.Eq("TaskStatus", TaskStatus.Completed)));

            if (siteIds != null)
            {


                ICriterion raReviewSiteCrtieria = Restrictions.And(
                                                            Restrictions.Eq("class", typeof(RiskAssessmentReviewTask)),
                                                            Restrictions.Or(
                                                                            Restrictions.IsNull("RiskAssessmentReview_RiskAssessment.RiskAssessmentSite.Id"),
                                                                            Restrictions.In("RiskAssessmentReview_RiskAssessment.RiskAssessmentSite.Id", siteIds.ToArray())
                                                                            )
                                                            );


                ICriterion graSiteCriteria = Restrictions.And(
                                                            Restrictions.Eq("class", typeof(MultiHazardRiskAssessmentFurtherControlMeasureTask)),
                                                            Restrictions.Or(
                                                                            Restrictions.IsNull("RiskAssessmentHazard_RiskAssessment.RiskAssessmentSite.Id"),
                                                                            Restrictions.In("RiskAssessmentHazard_RiskAssessment.RiskAssessmentSite.Id", siteIds.ToArray())
                                                                            )
                                                            );


                ICriterion hsraSiteCriteria = Restrictions.And(
                                                            Restrictions.Eq("class", typeof(HazardousSubstanceRiskAssessmentFurtherControlMeasureTask)),
                                                            Restrictions.Or(
                                                                            Restrictions.IsNull("hazardousSubstanceRiskAssessment.RiskAssessmentSite.Id"),
                                                                            Restrictions.In("hazardousSubstanceRiskAssessment.RiskAssessmentSite.Id", siteIds.ToArray())
                                                                            )
                                                            );

                ICriterion fraSiteCriteria = Restrictions.And(
                                                           Restrictions.Eq("class", typeof(FireRiskAssessmentFurtherControlMeasureTask)),
                                                           Restrictions.Or(
                                                                           Restrictions.IsNull("fireRiskAssessment.RiskAssessmentSite.Id"),
                                                                           Restrictions.In("fireRiskAssessment.RiskAssessmentSite.Id", siteIds.ToArray())
                                                                           )
                                                           );

                ICriterion responsibilityCriteria = Restrictions.And(
                                                           Restrictions.Eq("class", typeof(ResponsibilityTask)),
                                                           Restrictions.Or(
                                                                           Restrictions.IsNull("responsibility.Site.Id"),
                                                                           Restrictions.In("responsibility.Site.Id", siteIds.ToArray())
                                                                           )
                                                           );


                ICriterion actionCriteria = Restrictions.And(
                                           Restrictions.Eq("class", typeof(ActionTask)),
                                           Restrictions.Or(
                                                           Restrictions.IsNull("actionPlan.Site.Id"),
                                                           Restrictions.In("actionPlan.Site.Id", siteIds.ToArray())
                                                           )
                                           );




                ICriterion disjunction = Restrictions
                                            .Disjunction()
                                            .Add(raReviewSiteCrtieria)
                                            .Add(graSiteCriteria)
                                            .Add(hsraSiteCriteria)
                                            .Add(fraSiteCriteria)
                                            .Add(responsibilityCriteria)
                                            .Add(actionCriteria);

                result.Add(disjunction);
            }
            return result;
        }

        public Task GetByTaskGuid(Guid taskGuid)
        {
            var result = SessionManager
                 .Session
                 .CreateCriteria<Task>()
                 .Add(Restrictions.Eq("TaskGuid", taskGuid))
                 .SetMaxResults(1)
                 .UniqueResult<Task>();

            if (result == null)
                throw new TaskNotFoundException(0);

            return result;
        }

        private static string GetOrderColumnName(TaskOrderByColumn orderBy)
        {
            string columnName;

            switch (orderBy)
            {
                case TaskOrderByColumn.None:
                    columnName = "TaskCompletionDueDate";
                    break;
                case TaskOrderByColumn.TaskCategory:
                    columnName = "category.Category";
                    break;
                case TaskOrderByColumn.Reference:
                    columnName = "Reference";
                    break;
                case TaskOrderByColumn.Title:
                    columnName = "Title";
                    break;
                case TaskOrderByColumn.Description:
                    columnName = "Description";
                    break;
                case TaskOrderByColumn.TaskAssignedTo:
                    columnName = "tat.Surname";
                    break;
                case TaskOrderByColumn.CreatedDate:
                    columnName = "CreatedOn";
                    break;
                case TaskOrderByColumn.TaskCompletionDueDate:
                    columnName = "TaskCompletionDueDate";
                    break;
                case TaskOrderByColumn.DerivedDisplayStatus:
                    columnName = "TaskStatus";
                    break;
                default:
                    columnName = "TaskCompletionDueDate";
                    break;
            }
            return columnName;
        }
    }
}
