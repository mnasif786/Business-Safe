using System;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities.SafeCheck;

namespace BusinessSafe.Domain.RepositoryContracts.SafeCheck
{
    public interface IQuestionResponseRepository : IRepository<QuestionResponse, Guid>
    {
    }
}
