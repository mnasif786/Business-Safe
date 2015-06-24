using System;
using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.Users;
using System.ServiceModel;

namespace BusinessSafe.Application.Contracts.Users
{
    public interface IUserService 
    {
        IEnumerable<UserDto> GetAll(long companyId);
        UserDto GetIncludingRoleByIdAndCompanyId(Guid id, long companyId);
        IEnumerable<UserDto> GetIncludingRoleByIdsAndCompanyId(IEnumerable<Guid> ids, long companyId);
        UserDto GetIncludingEmployeeAndSiteByIdAndCompanyId(Guid id, long companyId);
        UserDto GetByIdAndCompanyIdIncludeDeleted(Guid id, long companyId);
        UserDto GetByIdAndCompanyId(Guid id, long companyId);
        IEnumerable<UserDto> Search(SearchUsersRequest request);
        void CreateAdminUser(CreateAdminUserRequest request);
        void SetRoleAndSite(SetUserRoleAndSiteRequest request);
        void ReinstateUser(Guid userIdToReinstate, long companyId, Guid currentUserId);
        void DeleteUser(Guid userToDeleteId, long companyId, Guid actioningUserId);

        /// <summary>
        /// Should only be used by Bus Event Handler as does not check if actioningUser is in same company
        /// </summary>
        void DeleteUser(Guid userToDeleteId, Guid? actioningUserId);
        void ReinstateUser(Guid userIdToReinstated, Guid? actioningUserId);
        void RegisterUser(Guid userId);
        void CreateUser(CreateUserRequest request);
        void DisableAuthenticationTokens(Guid userId, Guid? actioningUserId);
        void UpdateEmailAddress(Guid userId, long companyId, string email, Guid? actioningUserId);
    }
}
