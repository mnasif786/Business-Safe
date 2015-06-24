using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Implementations.Users;
using BusinessSafe.Application.Request.Users;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.Users.UserServiceTests
{
    [TestFixture]
    public class CreateNewUserTests
    {
        private Mock<IUserRepository> _userRepo;
        private Mock<IUserForAuditingRepository> _userForAuditingRepo;
        private Mock<ISiteRepository> _siteRepo;
        private Mock<IRoleRepository> _roleRepo;
        private Mock<IPeninsulaLog> _log;

        private Site _rootSite;
        private Role _adminRole;
        private Role _role;
        private UserForAuditing _nsbSystemUser;

        [SetUp]
        public void SetUp()
        {
            _nsbSystemUser = new UserForAuditing();
            _userRepo = new Mock<IUserRepository>();
            _userForAuditingRepo = new Mock<IUserForAuditingRepository>();
            _userForAuditingRepo.Setup(x => x.GetSystemUser()).Returns(_nsbSystemUser);
            _userRepo.Setup(x => x.Save(It.IsAny<User>()));

            _adminRole = new Role(){Id=Guid.NewGuid()};
            _roleRepo = new Mock<IRoleRepository>();
            _roleRepo.Setup(x => x.GetAdminRole()).Returns(_adminRole);

            _role = new Role() {Id = Guid.NewGuid()};
            _roleRepo = new Mock<IRoleRepository>();
            _roleRepo.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(_role);

            _rootSite = new Site() {Id = 3495L};
            _siteRepo = new Mock<ISiteRepository>();
            _siteRepo.Setup(x => x.GetRootSiteByCompanyId(It.IsAny<long>())).Returns(_rootSite);

            _log = new Mock<IPeninsulaLog>();
        }
        private UserService GetTarget()
        {
            var target = new UserService(
                _userForAuditingRepo.Object,
                null,
                _roleRepo.Object,
                _siteRepo.Object, _userRepo.Object, _log.Object, null, null);
            return target;
        }

        [Test]
        public void When_CreateUser_Then_new_employee_is_created()
        {
            // Given
            var request = new CreateUserRequest()
            {
                ClientId = 1234L,
                UserId =  Guid.NewGuid(),
                Forename = "Maggie",
                Surname = "May",
                Email = "maggiemay@weloverod.com",
                RoleId = _role.Id
            };

            User createdUser = null;
            _userRepo.Setup(x => x.Save(It.IsAny<User>()))
                .Callback<User>(x => createdUser = x);


            var target = GetTarget();

            // When
            target.CreateUser(request);

            // Then
            Assert.IsNotNull(createdUser.Employee);
            Assert.AreEqual(request.Forename, createdUser.Employee.Forename);
            Assert.AreEqual(request.Surname, createdUser.Employee.Surname);
            Assert.AreEqual(_rootSite.Id, createdUser.Site.Id);
            Assert.AreEqual(request.RoleId, createdUser.Role.Id);
            Assert.AreEqual(request.Email, createdUser.Employee.ContactDetails.First().Email);

        }
    }
}
