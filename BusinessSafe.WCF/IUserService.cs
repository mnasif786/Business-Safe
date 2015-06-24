using System;
using System.Collections.Generic;
using System.ServiceModel;
using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.WCF
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        UserDto[] GetIncludingRoleByIdsAndCompanyId(Guid[] ids, long companyId);

        [OperationContract]
        RoleDto[] GetRoles(long companyId);
    }
}
