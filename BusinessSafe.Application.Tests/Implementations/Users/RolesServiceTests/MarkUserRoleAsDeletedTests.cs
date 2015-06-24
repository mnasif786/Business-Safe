using System;

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
    public class MarkUserRoleAsDeletedTests
    {
        private Mock<IRoleRepository> _roleRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void Setup()
        {

            _roleRepository = new Mock<IRoleRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _log = new Mock<IPeninsulaLog>();
        }


        [Test]
        public void Given_valid_request_When_add_user_role_Then_should_call_correct_methods()
        {

            // Given
            var request = DeleteUserRoleRequestBuilder
                .Create()
                .WithCompanyId(100)
                .WithRoleId(Guid.NewGuid())
                .WithUserId(Guid.NewGuid())
                .Build();

            var user = new Mock<UserForAuditing>();
            _userRepository.Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId)).Returns(user.Object);

            var role = new Mock<Role>();
            _roleRepository.Setup(x => x.GetByIdAndCompanyId(request.UserRoleId, request.CompanyId)).Returns(role.Object);

            _roleRepository.Setup(x => x.SaveOrUpdate(role.Object));

            var target = CreateRolesService();

            // When
            target.MarkUserRoleAsDeleted(request);

            // Then
            _userRepository.VerifyAll();
            role.Verify(x => x.MarkForDelete(user.Object));
        }


        private RolesService CreateRolesService()
        {
            return new RolesService(_roleRepository.Object, null, _userRepository.Object, _log.Object,null);
        }
    }
}