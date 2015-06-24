using System;
using System.Collections.Generic;
using System.Linq;

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
    public class GetAllRolesTests
    {
        private Mock<IRoleRepository> _roleRepository;
        private Mock<IPeninsulaLog> _log;
        private long _companyId;

        [SetUp]
        public void SetUp()
        {
            _roleRepository = new Mock<IRoleRepository>();
            _companyId = 555;
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_that_get_all_roles_is_called_Then_should_call_the_correct_methods()
        {
            //Given
            var target = CreatePermissionsService();

            var role = new Role()
                                 {
                                     Name = "Permission"
                                 };


            var returningRoles = new List<Role>()
                                                {
                                                    role
                                                };

            _roleRepository.Setup(x => x.GetAllByCompanyId(_companyId)).Returns(returningRoles);

            //When
            target.GetAllRoles(_companyId);

            //Then
            _roleRepository.VerifyAll();
        }

        [Test]
        public void Given_that_get_all_roles_is_called_Then_should_return_correct_result()
        {
            //Given
            var target = CreatePermissionsService();

            var firstRole = new Mock<Role>();
            var role1Id = Guid.NewGuid();
            firstRole.SetupGet(x => x.Id).Returns(role1Id);
            firstRole.SetupGet(x => x.Name).Returns("Role 1");
            firstRole.SetupGet(x => x.Permissions).Returns(new List<RolePermission>());

            var secondRole = new Mock<Role>();
            var role2Id = Guid.NewGuid();
            secondRole.SetupGet(x => x.Id).Returns(role2Id);
            secondRole.SetupGet(x => x.Name).Returns("Role 2");
            secondRole.SetupGet(x => x.Permissions).Returns(new List<RolePermission>());

            var returningRoles = new List<Role>()
                                                {
                                                    firstRole.Object, secondRole.Object
                                                };

            _roleRepository.Setup(x => x.GetAllByCompanyId(_companyId)).Returns(returningRoles);
            ;

            //When
            var result = target.GetAllRoles(_companyId);

            //Then
            Assert.That(result.Count(), Is.EqualTo(returningRoles.Count));

            Assert.That(result.First().Name, Is.EqualTo(firstRole.Object.Name));
            Assert.That(result.First().Id, Is.EqualTo(firstRole.Object.Id));


            Assert.That(result.Last().Name, Is.EqualTo(secondRole.Object.Name));
            Assert.That(result.Last().Id, Is.EqualTo(secondRole.Object.Id));

        }

        private RolesService CreatePermissionsService()
        {
            return new RolesService(_roleRepository.Object, null, null, _log.Object,null);
        }
    }
}