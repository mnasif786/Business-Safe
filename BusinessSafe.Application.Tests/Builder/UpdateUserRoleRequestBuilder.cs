using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Tests.Builder
{
    public class UpdateUserRoleRequestBuilder
    {
        private string _roleName;
        private long _companyId;
        private int[] _permissions;

        public static UpdateUserRoleRequestBuilder Create()
        {
            return new UpdateUserRoleRequestBuilder();
        }

        public UpdateUserRoleRequest Build()
        {
            return new UpdateUserRoleRequest()
            {
                RoleName = _roleName,
                CompanyId = _companyId,
                Permissions = _permissions
            };
        }

        public UpdateUserRoleRequestBuilder WithRoleName(string testingrole)
        {
            _roleName = testingrole;
            return this;
        }

        public UpdateUserRoleRequestBuilder WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public UpdateUserRoleRequestBuilder WithPermissions(int[] permissions)
        {
            _permissions = permissions;
            return this;
        }
    }
}