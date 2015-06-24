using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Tests.Builder
{
    public class AddUserRoleRequestBuilder
    {
        private string _roleName;
        private long _companyId;
        private int[] _permissions;

        public static AddUserRoleRequestBuilder Create()
        {
            return new AddUserRoleRequestBuilder();
        }

        public AddUserRoleRequest Build()
        {
            return new AddUserRoleRequest()
                       {
                           RoleName = _roleName,
                           CompanyId = _companyId,
                           Permissions = _permissions
                       };
        }

        public AddUserRoleRequestBuilder WithRoleName(string testingrole)
        {
            _roleName = testingrole;
            return this;
        }

        public AddUserRoleRequestBuilder WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public AddUserRoleRequestBuilder WithPermissions(int[] permissions)
        {
            _permissions = permissions;
            return this;
        }
    }
}