using System;
using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Tests.Builder
{
    public class DeleteUserRoleRequestBuilder
    {
        private Guid _roleId;
        private long _companyId;
        private Guid _userId;


        public static DeleteUserRoleRequestBuilder Create()
        {
            return new DeleteUserRoleRequestBuilder();
        }

        public MarkUserRoleAsDeletedRequest Build()
        {
            return new MarkUserRoleAsDeletedRequest()
                       {
                           CompanyId = _companyId,
                           UserId = _userId,
                           UserRoleId = _roleId
                       };
        }

        public DeleteUserRoleRequestBuilder WithRoleId(Guid roleId)
        {
            _roleId = roleId;
            return this;
        }

        public DeleteUserRoleRequestBuilder WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public DeleteUserRoleRequestBuilder WithUserId(Guid userId)
        {
            _userId = userId;
            return this;
        }
     
    }
}