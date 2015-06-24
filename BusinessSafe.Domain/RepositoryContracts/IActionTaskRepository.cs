using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IActionTaskRepository : IRepository<ActionTask, long>
    {
         ActionTask GetByIdAndCompanyId(long id, long companyId);
    }
}

