using System;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.RepositoryContracts.SafeCheck;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository.SafeCheck
{
    public class QuestionResponseRepository : Repository<QuestionResponse, Guid>, IQuestionResponseRepository
    {
        public QuestionResponseRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }      
    }
}
