using System.Collections.Generic;

using BusinessSafe.Application.Implementations.Users;
using BusinessSafe.Application.Tests.Builder;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.Users.RolesServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class UpdateTests
    {
        private Mock<IRoleRepository> _roleRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IPermissionRepository> _permissionRepository;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void Setup()
        {
            _roleRepository = new Mock<IRoleRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _permissionRepository = new Mock<IPermissionRepository>();

            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_valid_request_When_add_user_role_Then_should_call_correct_methods()
        {

            // Given
            var request = UpdateUserRoleRequestBuilder
                .Create()
                .WithRoleName("testing")
                .Build();

            var target = CreateRolesService();


            var role = new Mock<Role>();
            _roleRepository.Setup(x => x.GetByIdAndCompanyId(request.RoleId, request.CompanyId)).Returns(role.Object);

            var user = new Mock<UserForAuditing>();
            _userRepository.Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId)).Returns(user.Object);

            // When
            target.Update(request);

            // Then
            _roleRepository.Verify(x => x.SaveOrUpdate(role.Object));

            _permissionRepository.Verify(x => x.GetAll());

            role.Verify(
                x =>
                x.Amend(It.Is<string>(y => y == request.RoleName), It.IsAny<IEnumerable<Permission>>(),
                        It.Is<UserForAuditing>(y => y == user.Object)));
        }

        private RolesService CreateRolesService()
        {
            return new RolesService(_roleRepository.Object, _permissionRepository.Object, _userRepository.Object, _log.Object,null);
        }
    }
}