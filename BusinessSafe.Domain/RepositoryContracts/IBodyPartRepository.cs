using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IBodyPartRepository : IRepository<BodyPart, long>
    { }
}