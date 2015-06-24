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
    public class RiskAssessmentDocumentRepository : Repository<RiskAssessmentDocument, long>, IRiskAssessmentDocumentRepository
    {
        public RiskAssessmentDocumentRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public IEnumerable<RiskAssessmentDocument> Search(long? companyId, string titleLike, long? documentTypeId, long? siteId, long? siteGroupId, IList<long> allowedSiteIds)
        {
            var query = SessionManager.Session.Query<RiskAssessmentDocument>()
                .Where(x => x.Deleted == false)
                .Where(x => x.RiskAssessment.Deleted == false);

            if (companyId.HasValue && companyId.Value != default(long))
            {
                query = query.Where(x => x.ClientId == companyId);
            }
            else
            {
                throw new ArgumentException("Attempt to load document without specified company.");
            }

            if (!String.IsNullOrEmpty(titleLike))
                query = query.Where(x => x.Title.Contains(titleLike));

            if (documentTypeId.HasValue && documentTypeId.Value != default(long))
                query = query.Where(x => x.DocumentType.Id == documentTypeId.Value);


            if (siteGroupId.HasValue && siteGroupId != default(long))
            {
                var siteGroup = SessionManager
                    .Session
                    .CreateCriteria<SiteGroup>()
                    .Add(Restrictions.Eq("Id", siteGroupId.Value))
                    .UniqueResult<SiteGroup>();

                var siteIdsDescendingFromGroup = siteGroup.GetThisAndAllDescendants().Select(x => x.Id);
                query = query.Where(x => siteIdsDescendingFromGroup.ToArray().Contains(x.RiskAssessment.RiskAssessmentSite.Id));
            }

            if (siteId .HasValue && siteId != default(long))
                query = query.Where(d => d.RiskAssessment.RiskAssessmentSite != null && d.RiskAssessment.RiskAssessmentSite.Id == siteId);

            //todo: problem getting documents with no site to show.
            if (allowedSiteIds != null && allowedSiteIds.Count > 0)
            {
                query = query.Where(x => x.RiskAssessment == null || x.RiskAssessment.RiskAssessmentSite == null || x.RiskAssessment.RiskAssessmentSite.Id == default(long) || allowedSiteIds.Contains(x.RiskAssessment.RiskAssessmentSite.Id));
            }

            //if Risk assessment is a Personal Risk assessment and Risk Assessment is marked as sensitive then exclude all the related documents
            query = query.Where(x => (x.RiskAssessment is PersonalRiskAssessment && !((PersonalRiskAssessment)x.RiskAssessment).Sensitive) 
                ||  !(x.RiskAssessment is PersonalRiskAssessment));

            return query.ToList();
        }
    }
}