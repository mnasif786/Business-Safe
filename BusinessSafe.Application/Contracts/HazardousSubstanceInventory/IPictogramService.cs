using System.Collections.Generic;

using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.Application.Contracts.HazardousSubstanceInventory
{
    public interface IPictogramService
    {
        IEnumerable<PictogramDto> GetAll();
    }
}
