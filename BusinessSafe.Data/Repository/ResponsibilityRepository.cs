using System;
using System.Linq;
using System.Collections.Generic;
using BusinessSafe.Data.Common;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;

using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.SqlCommand;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class ResponsibilityRepository : Repository<Responsibility, long>, IResponsibilityRepository
    {
        public ResponsibilityRepository(IBusinessSafeSessionManager sessionManager) : base(sessionManager)
        {
        }

        public Responsibility GetByIdAndCompanyId(long id, long companyId)
        {
            var query = SessionManager.Session.Query<Responsibility>()
                .SingleOrDefault(x =>
                                 x.CompanyId == companyId &&
                                 x.Id == id);

            return query;
        }

        public IEnumerable<Responsibility> GetStatutoryByCompanyId(long companyId)
        {
            return SessionManager.Session
                .CreateCriteria<Responsibility>()
                .Add(Restrictions.Eq("CompanyId", companyId))
                .Add(Restrictions.IsNotNull("StatutoryResponsibilityTemplateCreatedFrom"))
                .Add(Restrictions.Eq("Deleted", false))
                .List<Responsibility>();
        }

        public IEnumerable<Responsibility> Search(
            Guid currentUserId,
            IEnumerable<long> allowedSiteIds,
            long companyId,
            long? responsibilityCategoryId,
            long? siteId,
            long? siteGroupId,
            string title,
            DateTime? createdFrom,
            DateTime? createdTo,
            bool showDeleted,
            bool showCompleted,
            int page,
            int pageSize,
            ResponsibilitiesRequestOrderByColumn orderBy,
            bool Ascending)
        {
            var query = CreateQuery(companyId, responsibilityCategoryId, siteId, siteGroupId, title, createdFrom, createdTo, showDeleted, allowedSiteIds);

            switch( orderBy ) 
            {
                case ResponsibilitiesRequestOrderByColumn.Category:
                    query = (Ascending == true ? query.OrderBy(p => p.ResponsibilityCategory.Category) : query.OrderByDescending(p => p.ResponsibilityCategory.Category) );
                    break;
                
                case ResponsibilitiesRequestOrderByColumn.Title:
                    query = (Ascending == true ? query.OrderBy(p => p.Title) : query.OrderByDescending(p => p.Title));
                    break;

                case ResponsibilitiesRequestOrderByColumn.Description:
                    query = (Ascending == true ? query.OrderBy(p => p.Description) : query.OrderByDescending(p => p.Description));
                    break;
                
                case ResponsibilitiesRequestOrderByColumn.Site:
                    query = (Ascending == true ? query.OrderBy(p => p.Site.Name) : query.OrderByDescending(p => p.Site.Name));
                    break;

                case ResponsibilitiesRequestOrderByColumn.Reason:
                    query = (Ascending == true ? query.OrderBy(p => p.ResponsibilityReason.Reason) : query.OrderByDescending(p => p.ResponsibilityReason.Reason));
                    break;

                case ResponsibilitiesRequestOrderByColumn.AssignedTo:
                    query = (Ascending == true  ? query.OrderBy(p => p.Owner.Surname).ThenBy(p => p.Owner.Forename) 
                                                : query.OrderByDescending(p => p.Owner.Surname).ThenByDescending(p => p.Owner.Forename));
                    break;

                //case ResponsibilitiesRequestOrderByColumn.Status:
                //    query = (Ascending == true ? query.OrderBy(p => p.GetStatusDerivedFromTasks()) : query.OrderByDescending(p => p.GetStatusDerivedFromTasks()));
                //    break;

                case ResponsibilitiesRequestOrderByColumn.CreatedDateFormatted:
                    query = (Ascending == true ? query.OrderBy(p => p.CreatedOn) : query.OrderByDescending(p => p.CreatedOn));
                    break;

                case ResponsibilitiesRequestOrderByColumn.Frequency:
                    query = (Ascending == true ? query.OrderBy(p => (p.InitialTaskReoccurringType)) 
                                                : query.OrderByDescending(p => p.InitialTaskReoccurringType));
                    break;

                case ResponsibilitiesRequestOrderByColumn.DueDateFormatted:
                    query = (Ascending == true ? query.OrderBy(p =>  p.ResponsibilityTasks.Where(responsibilityTask => responsibilityTask.TaskStatus == TaskStatus.Outstanding && !responsibilityTask.Deleted)
                    .Min(responsibilityTask => responsibilityTask.TaskCompletionDueDate)) : query.OrderByDescending(p => p.ResponsibilityTasks.Where(responsibilityTask => responsibilityTask.TaskStatus == TaskStatus.Outstanding && !responsibilityTask.Deleted)
                    .Min(responsibilityTask => responsibilityTask.TaskCompletionDueDate)));
                    break;                
            }
           

            return query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        private IQueryable<Responsibility> CreateQuery(long companyId, long? responsibilityCategoryId, long? siteId, long? siteGroupId,
                                       string title, DateTime? createdFrom, DateTime? createdTo, bool showDeleted, IEnumerable<long> allowedSiteIds)
        {
            var query = SessionManager.Session.Query<Responsibility>()
                .Where(x => x.CompanyId == companyId &&
                            x.Deleted == showDeleted);

            if (responsibilityCategoryId > 0)
            {
                query = query.Where(x => x.ResponsibilityCategory.Id == responsibilityCategoryId);
            }

            if (siteGroupId > 0)
            {
                var siteGroup = SessionManager
                    .Session
                    .CreateCriteria<SiteGroup>()
                    .Add(Restrictions.Eq("Id", siteGroupId.Value))
                    .UniqueResult<SiteGroup>();

                var siteIdsDescendingFromGroup = siteGroup.GetThisAndAllDescendants().Select(x => x.Id);

                query = query.Where(x => siteIdsDescendingFromGroup.ToArray().Contains(x.Site.Id));
            }

            if (siteId > 0)
            {
                query = query.Where(x => x.Site.Id == siteId);
            }

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(x => x.Title.Contains(title));
            }

            if (createdFrom.HasValue)
            {
                query = query.Where(x => x.CreatedOn >= createdFrom.Value);
            }

            if (createdTo.HasValue)
            {
                query = query.Where(x => x.CreatedOn <= createdTo.Value);
            }

            if (allowedSiteIds.Any())
            {
                query = query.Where(x => x.Site == null || x.Site.Id == null || allowedSiteIds.ToList().Contains(x.Site.Id));
            }
            
            return query;
        }

        public int Count(
            Guid currentUserId,
            IEnumerable<long> allowedSiteIds,
            long companyId,
            long? responsibilityCategoryId,
            long? siteId,
            long? siteGroupId,
            string title,
            DateTime? createdFrom,
            DateTime? createdTo,
            bool showDeleted,
            bool showCompleted)
        {
            var query = CreateQuery(companyId, responsibilityCategoryId, siteId, siteGroupId, title, createdFrom, createdTo, showDeleted, allowedSiteIds);
            return query.Count();
        }
    }
}
