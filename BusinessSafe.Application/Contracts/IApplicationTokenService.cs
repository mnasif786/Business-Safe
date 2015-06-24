using System;

using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.Application.Contracts
{
    public interface IApplicationTokenService
    {
        ApplicationTokenDto GetById(Guid id);
    }
}
