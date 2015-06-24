﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IDocHandlerDocumentTypeRepository : IRepository<DocHandlerDocumentType, long>
    {
        IEnumerable<DocHandlerDocumentType> GetByDocHandlerDocumentTypeGroup(DocHandlerDocumentTypeGroup docHandlerDocumentTypeGroup);
    }
}
