using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.RepositoryContracts.SafeCheck;
using NHibernate.Linq;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository.SafeCheck
{
    public class QuestionRepository  : Repository<Question, Guid>, IQuestionRepository
    {
        public QuestionRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public IEnumerable<ClientQuestion> GetByClientAccountNumber(string accountNumber)
        {
            return SessionManager.Session.Query<ClientQuestion>()
            .Where(x => x.ClientAccountNumber == accountNumber);
        }

        public IEnumerable<ClientQuestion> GetAllByClient()
        {
            return SessionManager.Session.Query<ClientQuestion>();
        }

        public IEnumerable<Question> GetAllNonClientSpecific()
        {
            return SessionManager.Session.Query<Question>()
            .Where(x => !x.SpecificToClientId.HasValue && !x.CustomQuestion);
        }
    }
}

