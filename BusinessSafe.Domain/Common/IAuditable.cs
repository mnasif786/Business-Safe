using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.Common
{
    public interface IAuditable
    {
        string IdForAuditing { get; }
        UserForAuditing CreatedBy { get; }
        UserForAuditing LastModifiedBy { get; }
    }
    
}