﻿using System;
using System.Collections.Generic;

using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IAuthenticationTokenRepository : IRepository<AuthenticationToken, Guid>
    {
    }
}
