using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;

using NHibernate.Criterion;
using NHibernate.Linq;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class AddedDocumentRepository : Repository<AddedDocument, long>, IAddedDocumentRepository
    {
        public AddedDocumentRepository(IBusinessSafeSessionManager sessionManager) : base(sessionManager) { }

        public IEnumerable<AddedDocument> Search(long? companyId, string titleLike, long? documentTypeId, long? siteId, long? siteGroupId, IList<long> allowedSiteIds)
        {
            var query = SessionManager.Session.Query<AddedDocument>()
                .Where(x => x.Deleted == false);

            if (companyId.HasValue && companyId.Value != default(long))
            {
                query = query.Where(x => x.ClientId == companyId);
            }
            else
            {
                throw new ArgumentException("Attempt to load document without specified company.");
            }

            if (siteGroupId.HasValue && siteGroupId != default(long))
            {
                var siteGroup = SessionManager
                    .Session
                    .CreateCriteria<SiteGroup>()
                    .Add(Restrictions.Eq("Id", siteGroupId.Value))
                    .UniqueResult<SiteGroup>();

                var siteIdsDescendingFromGroup = siteGroup.GetThisAndAllDescendants().Select(x => x.Id);
                query = query.Where(x => siteIdsDescendingFromGroup.ToArray().Contains(x.Site.Id));
            }

            if (siteId.HasValue && siteId.Value != default(long))
            {
                query = query.Where(x => x.Site.Id == siteId);
            }

            if (!String.IsNullOrEmpty(titleLike))
                query = query.Where(x => x.Title.Contains(titleLike));

            if (documentTypeId.HasValue && documentTypeId.Value != default(long))
                query = query.Where(x => x.DocumentType.Id == documentTypeId.Value);

            //todo: problem getting documents with no site to show.
            if (allowedSiteIds != null && allowedSiteIds.Count > 0)
            {
                query = query.Where(x => x.Site == null || x.Site.Id == default(long) || allowedSiteIds.Contains(x.Site.Id));
            }

            return query.ToList();
        }
    }
}