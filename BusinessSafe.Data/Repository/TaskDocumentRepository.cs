using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Data.Common;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Common;
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
    public class TaskDocumentRepository : Repository<TaskDocument, long>, ITaskDocumentRepository
    {
        public TaskDocumentRepository(IBusinessSafeSessionManager sessionManager) : base(sessionManager)
        {
            
        }

        public IEnumerable<TaskDocument> Search(long companyId, string titleLike, long? documentTypeId, long? siteId, long? siteGroupId, IList<long> allowedSiteIds)
        {
            if (companyId == 0)
            {
                throw new ArgumentException("Attempt to load document without specified company.");
            }

            var documents = SearchMultiHazardRiskAssessmentFurtherControlMeasureTask(companyId, titleLike, documentTypeId).ToList();
            documents.AddRange(SearchRiskAssessmentReviewTask(companyId, titleLike, documentTypeId));
            documents.AddRange(SearchHazardousSubstanceRiskAssessmentFurtherControlMeasureTask(companyId, titleLike, documentTypeId));
            documents.AddRange(SearchFireRiskAssessmentFurtherControlMeasureTask(companyId, titleLike, documentTypeId));

            if (siteGroupId.HasValue && siteGroupId != default(long))
            {
                var siteGroup = SessionManager
                    .Session
                    .CreateCriteria<SiteGroup>()
                    .Add(Restrictions.Eq("Id", siteGroupId.Value))
                    .UniqueResult<SiteGroup>();

                var siteIdsDescendingFromGroup = siteGroup.GetThisAndAllDescendants().Select(x => x.Id);
                documents = documents.Where(d => d.Task.RiskAssessment.RiskAssessmentSite != null && 
                                                 siteIdsDescendingFromGroup.ToArray().Contains(d.Task.RiskAssessment.RiskAssessmentSite.Id)).ToList();
            }

            if (siteId.HasValue && siteId != default(long))
                documents = documents.Where(d => d.Task.RiskAssessment.RiskAssessmentSite!=null && d.Task.RiskAssessment.RiskAssessmentSite.Id == siteId).ToList();

            //todo: problem getting documents with no site to show.
            if (allowedSiteIds != null && allowedSiteIds.Count > 0)
            {
                documents =
                    documents.Where(x => x.Task == null || x.Task.Site == null || x.Task.Site.Id == default(long) || allowedSiteIds.Contains(x.Task.Site.Id)).ToList();
            }

            return documents;
        }

        public IEnumerable<TaskDocument> SearchMultiHazardRiskAssessmentFurtherControlMeasureTask(long companyId, string titleLike, long? documentTypeId)
        {
            var query = SessionManager.Session.Query<MultiHazardRiskAssessmentFurtherControlMeasureTask>()
                .Where(td => td.Deleted == false)
                .Where(td => td.TaskAssignedTo.CompanyId == companyId)
                .Where(td => td.MultiHazardRiskAssessmentHazard.MultiHazardRiskAssessment.Deleted == false);

            var documentQuery = AppendDocumentQuery(titleLike, documentTypeId, query.SelectMany(x => x.Documents));

            return documentQuery.ToList();
        }

        private static IQueryable<TaskDocument> AppendDocumentQuery(string titleLike, long? documentTypeId, IQueryable<TaskDocument> documentQuery)
        {
            documentQuery = documentQuery.Where(x => x.Deleted == false);

            if (!String.IsNullOrEmpty(titleLike))
                documentQuery = documentQuery.Where(x => x.Title.Contains(titleLike));

            if (documentTypeId.HasValue && documentTypeId.Value != default(long))
                documentQuery = documentQuery.Where(x => x.DocumentType.Id == documentTypeId.Value);

            return documentQuery;
        }

        public IEnumerable<TaskDocument> SearchRiskAssessmentReviewTask(long companyId, string titleLike, long? documentTypeId)
        {
            var query = SessionManager.Session.Query<RiskAssessmentReviewTask>()
               .Where(td => td.Deleted == false)
               .Where(td => td.TaskAssignedTo.CompanyId == companyId)
               .Where(td => td.RiskAssessmentReview.RiskAssessment.Deleted == false);

            var documentQuery = AppendDocumentQuery(titleLike, documentTypeId, query.SelectMany(x => x.Documents));

            return documentQuery.ToList();
        }

        public IEnumerable<TaskDocument> SearchHazardousSubstanceRiskAssessmentFurtherControlMeasureTask(long companyId, string titleLike, long? documentTypeId)
        {
            var query = SessionManager.Session.Query<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask>()
              .Where(td => td.Deleted == false)
              .Where(td => td.TaskAssignedTo.CompanyId == companyId)
              .Where(td => td.HazardousSubstanceRiskAssessment.Deleted == false);

            var documentQuery = AppendDocumentQuery(titleLike, documentTypeId, query.SelectMany(x => x.Documents));

            return documentQuery.ToList();
        }

        public IEnumerable<TaskDocument> SearchFireRiskAssessmentFurtherControlMeasureTask(long companyId, string titleLike, long? documentTypeId)
        {
            var query = SessionManager.Session.Query<FireRiskAssessmentFurtherControlMeasureTask>()
              .Where(td => td.Deleted == false)
              .Where(td => td.TaskAssignedTo.CompanyId == companyId)
              .Where(td => td.SignificantFinding.FireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment.Deleted == false);

            var documentQuery = AppendDocumentQuery(titleLike, documentTypeId, query.SelectMany(x => x.Documents));
            
            return documentQuery.ToList();
        }
    }

}
