using System.Collections.Generic;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Criterion;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class DocHandlerDocumentTypeRepository : Repository<DocHandlerDocumentType, long>, IDocHandlerDocumentTypeRepository
    {
        public DocHandlerDocumentTypeRepository(IBusinessSafeSessionManager sessionManager) : base(sessionManager) { }

        public IEnumerable<DocHandlerDocumentType> GetByDocHandlerDocumentTypeGroup(DocHandlerDocumentTypeGroup docHandlerDocumentTypeGroup)
        {
            var criteria = SessionManager.Session.CreateCriteria<DocHandlerDocumentType>();
            criteria.Add(Restrictions.Eq("DocHandlerDocumentTypeGroup", docHandlerDocumentTypeGroup));

            return criteria.List<DocHandlerDocumentType>();
        }

        public bool IsValidDocumentForDocumentGroup(long docTypeId, DocHandlerDocumentTypeGroup docHandlerDocumentTypeGroup)
        {
            throw new System.NotImplementedException();
        }
    }
}