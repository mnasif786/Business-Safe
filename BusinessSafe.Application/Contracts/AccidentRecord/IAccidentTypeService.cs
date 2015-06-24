using System;
using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.Application.Contracts.AccidentRecord
{
    public interface IAccidentTypeService
    {
        IEnumerable<AccidentTypeDto> GetAll();
        IEnumerable<AccidentTypeDto> GetAllForCompany(long companyId);
    }
}