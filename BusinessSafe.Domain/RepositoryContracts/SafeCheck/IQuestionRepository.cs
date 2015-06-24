using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities.SafeCheck;

namespace BusinessSafe.Domain.RepositoryContracts.SafeCheck
{
    public interface IQuestionRepository : IRepository<Question, Guid>
    {
        IEnumerable<ClientQuestion> GetByClientAccountNumber(string accountNumber);
        IEnumerable<ClientQuestion> GetAllByClient();
        IEnumerable<Question> GetAllNonClientSpecific();
    }
}

