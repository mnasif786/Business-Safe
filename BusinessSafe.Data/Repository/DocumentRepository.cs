using System;
using System.Collections.Generic;
using BusinessSafe.Data.Common;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class DocumentRepository : Repository<Document, long>, IDocumentRepository
    {
        public DocumentRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public IEnumerable<Document> Search(
            long? companyId,
            string titleLike,
            long? documentTypeId)
        {
            var criteria = SessionManager.Session.CreateCriteria<Document>();
            criteria.CreateAlias("DocumentType", "documentType");

            if (companyId.HasValue && companyId.Value != default(long))
            {
                criteria.Add(Restrictions.Eq("ClientId", companyId.Value));
            }
            else
            {
                throw new Exception("Attempt to load document without specified company.");
            }

            if (!String.IsNullOrEmpty(titleLike))
                criteria.Add(Restrictions.Like("Title", "%" + titleLike + "%"));

            if (documentTypeId.HasValue && documentTypeId.Value != default(long))
                criteria.Add(Restrictions.Eq("documentType.Id", documentTypeId.Value));

            criteria.Add(Restrictions.Eq("Deleted", false));

            var result = criteria
                .SetResultTransformer(new DistinctRootEntityResultTransformer())
                .List<Document>();

            return result;
        }

        public Document GetByIdAndCompanyId(long documentId, long companyId)
        {
            var result = SessionManager
               .Session
               .CreateCriteria<Document>()
               .Add(Restrictions.Eq("Id", documentId))
               .Add(Restrictions.Eq("ClientId", companyId))
               .Add(Restrictions.Eq("Deleted", false))
               .SetMaxResults(1)
               .UniqueResult<Document>();

            if (result == null)
                throw new DocumentNotFoundException(documentId);

            return result;
        }

        public Document GetByDocumentLibraryIdAndCompanyId(long documentLibraryId, long companyId)
        {
            var result = SessionManager
               .Session
               .CreateCriteria<Document>()
               .Add(Restrictions.Eq("DocumentLibraryId", documentLibraryId))
               .Add(Restrictions.Eq("ClientId", companyId))
               .Add(Restrictions.Eq("Deleted", false))
               .SetMaxResults(1)
               .UniqueResult<Document>();

            if (result == null)
                throw new DocumentNotFoundException(documentLibraryId);

            return result;
        }

        public IEnumerable<Document> Search(long companyId, string titleLike, long? documentTypeId, long ?siteId)
        {
            var criteria = SessionManager
                .Session
                .CreateCriteria<Document>()
                .Add(Restrictions.Eq("ClientId", companyId));

            criteria.CreateAlias("DocumentType", "documentType");

            if (!string.IsNullOrEmpty(titleLike))
                criteria.Add(Restrictions.Like("Title", titleLike, MatchMode.Anywhere));

            if (documentTypeId.HasValue && documentTypeId != default(long))
                criteria.Add(Restrictions.Eq("documentType.Id", documentTypeId));

            if (siteId.HasValue && siteId != default(long))
            {   
                ICriterion addedDocumentCritiera = Restrictions.And(
                                                Restrictions.Eq("class", typeof(AddedDocument)),
                                                Restrictions.Eq("Site.Id", siteId));


                ICriterion taskDocumentCritiera = Restrictions.And(
                                                Restrictions.Eq("class", typeof(TaskDocument)),
                                                Restrictions.Eq("Task.RiskAssessment.RiskAssessmentSite.Id", siteId));

                ICriterion riskAssessmentDocumentCritiera = Restrictions.And(
                                                Restrictions.Eq("class", typeof(RiskAssessmentDocument)),
                                                Restrictions.Eq("RiskAssessment.RiskAssessmentSite.Id", siteId));



                ICriterion disjunction = Restrictions
                    .Disjunction()
                    .Add(addedDocumentCritiera)
                    .Add(taskDocumentCritiera)
                    .Add(riskAssessmentDocumentCritiera);

                criteria.Add(disjunction);
            }

            var result = criteria.List<Document>();

            return result;
        }
    }
}