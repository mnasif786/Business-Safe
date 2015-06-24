using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Criterion;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public static class RiskAssessmentQueryExtensions
    {
        public static IQueryable<T> AddOrder<T>(this IQueryable<T> query, RiskAssessmentOrderByColumn orderBy, OrderByDirection orderByDirection) where T : RiskAssessment
        {
            switch (orderBy)
            {
                case RiskAssessmentOrderByColumn.Reference:
                    return orderByDirection == OrderByDirection.Ascending ? query.OrderBy(x => x.Reference) : query.OrderByDescending(x => x.Reference);
                    break;
                case RiskAssessmentOrderByColumn.Title:
                    return orderByDirection == OrderByDirection.Ascending ? query.OrderBy(x => x.Title) : query.OrderByDescending(x => x.Title);
                    break;
                case RiskAssessmentOrderByColumn.Site:
                    return orderByDirection == OrderByDirection.Ascending ? query.OrderBy(x => x.RiskAssessmentSite.Name) : query.OrderByDescending(x => x.RiskAssessmentSite.Name);
                    break;
                case RiskAssessmentOrderByColumn.AssignedTo:
                    return orderByDirection == OrderByDirection.Ascending ? query.OrderBy(x => x.RiskAssessor.Employee.Surname)
                                                                                .ThenBy(x => x.RiskAssessor.Employee.Forename) : query.OrderByDescending(x => x.RiskAssessor.Employee.Surname)
                                                                                                                                     .ThenByDescending(x => x.RiskAssessor.Employee.Forename);
                    break;
               case RiskAssessmentOrderByColumn.CreatedOn:
                    return orderByDirection == OrderByDirection.Ascending ? query.OrderBy(x => x.CreatedOn) : query.OrderByDescending(x => x.CreatedOn);
                    break;

                case RiskAssessmentOrderByColumn.AssessmentDate:
                default:
                    return orderByDirection == OrderByDirection.Ascending ? query.OrderBy(x => x.AssessmentDate) : query.OrderByDescending(x => x.AssessmentDate);

                    break;
                case RiskAssessmentOrderByColumn.Status:
                    return orderByDirection == OrderByDirection.Ascending ? query.OrderBy(x => x.Status) : query.OrderByDescending(x => x.Status);
                    break;
                case RiskAssessmentOrderByColumn.NextReview:
                    return orderByDirection == OrderByDirection.Ascending ? query.OrderBy(x => x.NextReviewDate) : query.OrderByDescending(x => x.NextReviewDate);

            }
        }

        public static IQueryable<T> CreateQuery<T>(this IQueryable<T> query,IBusinessSafeSessionManager sessionManager, string title, long clientId, IEnumerable<long> siteIds,
                                                   DateTime? createdFrom, DateTime? createdTo, long? siteGroupId,
                                                   long? siteId, Guid currentUserId, bool showDeleted,
                                                   bool showArchived) where T : RiskAssessment
        {
            query = query.Where(x => x.CompanyId == clientId)
                .Where(x => x.Deleted == showDeleted);


            if (title != null)
            {
                query = query.Where(x => x.Title.Contains(title));
            }

            if (siteIds != null)
            {
                query = query.Where(x => siteIds.ToArray().Contains(x.RiskAssessmentSite.Id)
                                         || (x.RiskAssessmentSite == null && x.CreatedBy.Id == currentUserId));
            }
            else
            {
                query = query.Where(x => x.RiskAssessmentSite != null
                                         || (x.RiskAssessmentSite == null && x.CreatedBy.Id == currentUserId));
            }

            if (siteGroupId.HasValue)
            {
                var siteGroup = sessionManager
                    .Session
                    .CreateCriteria<SiteGroup>()
                    .Add(Restrictions.Eq("Id", siteGroupId.Value))
                    .UniqueResult<SiteGroup>();

                var siteIdsDescendingFromGroup = siteGroup.GetThisAndAllDescendants().Select(x => x.Id);
                query = query.Where(x => siteIdsDescendingFromGroup.ToArray().Contains(x.RiskAssessmentSite.Id));
            }

            if (siteId.HasValue)
            {
                query = query.Where(x => x.RiskAssessmentSite.Id == siteId.Value);
            }


            if (showArchived)
            {
                query = query.Where(x => x.Status == RiskAssessmentStatus.Archived);
            }
            else
            {
                query = query.Where(x => x.Status != RiskAssessmentStatus.Archived);
            }

            if (createdFrom.HasValue)
            {
                query = query.Where(x => x.CreatedOn >= createdFrom.Value);
            }

            if (createdTo.HasValue)
            {
                query = query.Where(x => x.CreatedOn <= createdFrom.Value);
            }

            return query;
        }

        public static IQueryable<PersonalRiskAssessment> CanUserAccess(this IQueryable<PersonalRiskAssessment> query, long[] latestReviewIds, Guid userId, Guid employeeId)
        {
            query = query.Where(x => x.RiskAssessor == null || x.RiskAssessor != null);
            query = query.Where(x => !x.Sensitive
                             || x.CreatedBy.Id == userId
                             || (x.RiskAssessor != null
                                 && x.RiskAssessor.Employee != null
                                 && x.RiskAssessor.Employee.Id == employeeId) // the reason why we are not added criteria for RiskAssessor.Employee.User.Id is because Nhibernate linq creates an inner join when we want a left join. Whcih means that PRA with null risk assessors are not returned
                                 //&& x.RiskAssessor.Employee.User != null
                                 //&& x.RiskAssessor.Employee.User.Id == userId)
                             || x.Reviews.Any(r => latestReviewIds.Contains(r.Id) && r.ReviewAssignedTo.Id == employeeId));

            return query;
        }
    }
}