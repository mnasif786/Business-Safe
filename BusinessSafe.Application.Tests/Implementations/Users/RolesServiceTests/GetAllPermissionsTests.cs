using System.Collections.Generic;

using BusinessSafe.Application.Implementations.Users;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.Users.RolesServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetAllPermissionsTests
    {
        private Mock<IPermissionRepository> _permissionRepository;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void SetUp()
        {
            _permissionRepository = new Mock<IPermissionRepository>();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_that_get_all_permissions_is_called_Then_should_call_the_correct_methods()
        {
            //Given
            var target = CreatePermissionsService();


            var permissios = new List<Permission>();
            _permissionRepository.Setup(x => x.GetAll()).Returns(permissios);

            //When
            target.GetAllPermissions();

            //Then
            _permissionRepository.VerifyAll();
        }
        private RolesService CreatePermissionsService()
        {
            return new RolesService(null, _permissionRepository.Object,null,_log.Object, null);
        }
    }
}