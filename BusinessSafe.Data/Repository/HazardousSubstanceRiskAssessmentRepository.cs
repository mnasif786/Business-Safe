using System.Linq;
using BusinessSafe.Data.Common;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using System.Collections.Generic;
using System;
using Expression = NHibernate.Criterion.Expression;

namespace BusinessSafe.Data.Repository
{
    public class HazardousSubstanceRiskAssessmentRepository : Repository<HazardousSubstanceRiskAssessment, long>, IHazardousSubstanceRiskAssessmentRepository
    {
        public HazardousSubstanceRiskAssessmentRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public HazardousSubstanceRiskAssessment GetByIdAndCompanyId(long hazardousSubstanceAssessmentId, long companyId)
        {
            var result = SessionManager
               .Session
               .CreateCriteria<HazardousSubstanceRiskAssessment>()
               .Add(Restrictions.Eq("Id", hazardousSubstanceAssessmentId))
               .Add(Restrictions.Eq("CompanyId", companyId))
                // Deleted Restrictions not applicable because need to view delete Risk Assessments
               .SetMaxResults(1)
               .UniqueResult<HazardousSubstanceRiskAssessment>();

            if (result == null)
                throw new HazardousSubstanceRiskAssessmentNotFoundException(hazardousSubstanceAssessmentId);

            return result;
        }

        public IEnumerable<HazardousSubstanceRiskAssessment> Search(string title, long companyId, DateTime? createdFrom, DateTime? createdTo, long hazardousSubstanceId, IEnumerable<long> allowedSiteIds, bool showDeleted, bool showArchived, Guid currentUserId, long? siteId, long? siteGroupId, int page, int pageSize, RiskAssessmentOrderByColumn orderBy, OrderByDirection sortOrder)
        {
            var result = CreateCriteria(title, companyId, createdFrom, createdTo, hazardousSubstanceId, allowedSiteIds, showDeleted, showArchived, currentUserId, siteId, siteGroupId);
           
            result.AddOrder(new Order(GetOrderProperty(orderBy), sortOrder == OrderByDirection.Ascending));

            if (page != default(int))
            {
                result.SetFirstResult(page == 1 ? 0 : (page - 1)*pageSize).SetMaxResults(pageSize);
            }

            return result.List<HazardousSubstanceRiskAssessment>();
        }

        public string GetOrderProperty(RiskAssessmentOrderByColumn orderBy)
        {
            switch (orderBy)
            {
                case RiskAssessmentOrderByColumn.Reference:
                    return RiskAssessmentOrderByColumn.Reference.ToString();
                case RiskAssessmentOrderByColumn.Title:
                    return RiskAssessmentOrderByColumn.Title.ToString();
                case RiskAssessmentOrderByColumn.Site:
                    return "site.Name";
                    break;
                case RiskAssessmentOrderByColumn.AssignedTo:
                    return "riskAssessor_Employee.Surname";
                case RiskAssessmentOrderByColumn.NextReview:
                    return "NextReviewDate";
                    break;
                case RiskAssessmentOrderByColumn.Status:
                    return RiskAssessmentOrderByColumn.Status.ToString();
                    break;
                case RiskAssessmentOrderByColumn.CreatedOn:
                    return RiskAssessmentOrderByColumn.CreatedOn.ToString();
                    break;
                default:
                case RiskAssessmentOrderByColumn.AssessmentDate:
                    return RiskAssessmentOrderByColumn.AssessmentDate.ToString();
                    break;
            }
        }
        
        public int Count(string title, long companyId, DateTime? createdFrom, DateTime? createdTo, long hazardousSubstanceId, IEnumerable<long> allowedSiteIds, bool showDeleted, bool showArchived, Guid currentUserId, long? siteId, long? siteGroupId)
        {
            var criteria = CreateCriteria(title, companyId, createdFrom, createdTo, hazardousSubstanceId, allowedSiteIds, showDeleted, showArchived, currentUserId, siteId, siteGroupId);

            var count = criteria.SetProjection(Projections.RowCount()).FutureValue<Int32>();
            return count.Value;
        }

        private ICriteria CreateCriteria(string title, long companyId, DateTime? createdFrom, DateTime? createdTo,
            long hazardousSubstanceId, IEnumerable<long> allowedSiteIds, bool showDeleted, bool showArchived, Guid currentUserId,
            long? siteId, long? siteGroupId)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<HazardousSubstanceRiskAssessment>();


            result.Add(Restrictions.Eq("CompanyId", companyId));
            result.Add(Restrictions.Eq("Deleted", showDeleted));

            result.CreateAlias("RiskAssessmentSite", "site", JoinType.LeftOuterJoin);
            result.CreateAlias("RiskAssessor", "riskAssessor", JoinType.LeftOuterJoin);
            result.CreateAlias("riskAssessor.Employee", "riskAssessor_Employee", JoinType.LeftOuterJoin);
            result.SetFetchMode("riskAssessor", FetchMode.Eager);
            result.SetFetchMode("riskAssessor_Employee", FetchMode.Eager);


            if (title != null)
            {
                result.Add(Restrictions.Like("Title", string.Format("%{0}%", title)));
            }

            if (showArchived)
            {
                result.Add(Restrictions.Eq("Status", RiskAssessmentStatus.Archived));
            }
            else
            {
                result.Add(Expression.Not(Restrictions.Eq("Status", RiskAssessmentStatus.Archived)));
            }

            if (createdFrom.HasValue)
            {
                result.Add((Restrictions.Ge("CreatedOn", createdFrom.Value)));
            }

            if (createdTo.HasValue)
            {
                result.Add((Restrictions.Le("CreatedOn", createdTo.Value)));
            }

            if (hazardousSubstanceId > 0)
            {
                result.Add(Restrictions.Eq("HazardousSubstance.Id", hazardousSubstanceId));
            }

            if (allowedSiteIds != null)
            {
                result.Add(Restrictions.Or(
                    Restrictions.In("site.Id", allowedSiteIds.ToArray()),
                    Restrictions.And(
                        Restrictions.IsNull("site.Id"),
                        Restrictions.Eq("CreatedBy.Id", currentUserId))));
            }
            else
            {
                result.Add(Restrictions.Or(
                    Restrictions.IsNotNull("site.Id"),
                    Restrictions.And(
                        Restrictions.IsNull("site.Id"),
                        Restrictions.Eq("CreatedBy.Id", currentUserId))));
            }


            if (siteGroupId.HasValue)
            {
                var siteGroup = SessionManager
                    .Session
                    .CreateCriteria<SiteGroup>()
                    .Add(Restrictions.Eq("Id", siteGroupId.Value))
                    .UniqueResult<SiteGroup>();

                var siteIdsDescendingFromGroup = siteGroup.GetThisAndAllDescendants().Select(x => x.Id);
                result.Add(Restrictions.In("site.Id", siteIdsDescendingFromGroup.ToArray()));
            }

            if (siteId.HasValue)
            {
                result.Add(Restrictions.Eq("site.Id", siteId.Value));
            }
            return result;
        }

        public bool DoesRiskAssessmentsExistForHazardousSubstance(long hazardousSubstanceId, long companyId)
        {
            var riskAssessmentsCount = (int)SessionManager
                                           .Session
                                           .CreateCriteria<HazardousSubstanceRiskAssessment>()
                                           .Add(Restrictions.Eq("CompanyId", companyId))
                                           .Add(Restrictions.Eq("Deleted", false))
                                           .Add(Restrictions.Eq("HazardousSubstance.Id", hazardousSubstanceId))
                                           .SetMaxResults(1)
                                           .SetProjection(Projections.Count("Id")).UniqueResult();
            return riskAssessmentsCount > 0;
        }
    }
}