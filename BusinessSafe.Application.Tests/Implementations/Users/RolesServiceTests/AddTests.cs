using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using BusinessSafe.Application.Implementations.Users;
using BusinessSafe.Application.Request;
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
    public class AddTests
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
            var request = AddUserRoleRequestBuilder.Create().WithCompanyId(1234L).WithRoleName("admin").Build();
            var target = CreateRolesService();

            // When
            target.Add(request);

            // Then
            _roleRepository.Verify(x => x.SaveOrUpdate(It.IsAny<Role>()));
            _userRepository.Verify(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.Is<long>(y => y == request.CompanyId)));
            _permissionRepository.Verify(x => x.GetAll());
        }
        

        private RolesService CreateRolesService()
        {
            return new RolesService(_roleRepository.Object, _permissionRepository.Object, _userRepository.Object, _log.Object, null);
        }
    }
}