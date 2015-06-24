using System;
using System.Linq;

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
    public class CreateAdminUserTests
    {
        private Mock<IUserRepository> _userRepo;
        private Mock<IUserForAuditingRepository> _userForAuditingRepo;
        private Mock<ISiteRepository> _siteRepo;
        private Mock<IRoleRepository> _roleRepo;
        private Mock<IPeninsulaLog> _log;        
        
        private Site _rootSite;
        private Role _adminRole;
        private UserForAuditing _nsbSystemUser;

        [SetUp]
        public void SetUp()
        {
            _nsbSystemUser = new UserForAuditing();
            _userRepo = new Mock<IUserRepository>();
            _userForAuditingRepo = new Mock<IUserForAuditingRepository>();
            _userForAuditingRepo.Setup(x => x.GetSystemUser()).Returns(_nsbSystemUser);
            _userRepo.Setup(x => x.Save(It.IsAny<User>()));

            _adminRole = new Role();
            _roleRepo = new Mock<IRoleRepository>();
            _roleRepo.Setup(x => x.GetAdminRole()).Returns(_adminRole);

            _rootSite = new Site();
            _siteRepo = new Mock<ISiteRepository>();
            _siteRepo.Setup(x => x.GetRootSiteByCompanyId(It.IsAny<long>())).Returns(_rootSite);

            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void When_CreateAdminUser_Then_systemUser_is_retrieved_from_user_repository()
        {
            // Given
            const long clientId = 1234L;
            var request = new CreateAdminUserRequest()
            {
                ClientId = clientId
            };
            var target = GetTarget();

            // When
            target.CreateAdminUser(request);

            // Then
            _userForAuditingRepo.Verify(x => x.GetSystemUser());
        }

        [Test]
        public void When_CreateAdminUser_Then_root_site_for_users_company_is_retrieved_from_site_repository()
        {
            // Given
            const long clientId = 1234L;
            var request = new CreateAdminUserRequest()
                          {
                              ClientId = clientId
                          };
            var target = GetTarget();

            // When
            target.CreateAdminUser(request);

            // Then
            _siteRepo.Verify(x => x.GetRootSiteByCompanyId(clientId));
        }

        [Test]
        public void When_CreateAdminUser_Then_admin_role_is_retrieved_from_role_repository()
        {
            // Given
            const long clientId = 1234L;
            var request = new CreateAdminUserRequest()
            {
                ClientId = clientId
            };
            var target = GetTarget();

            // When
            target.CreateAdminUser(request);

            // Then
            _roleRepo.Verify(x => x.GetAdminRole());
        }

        [Test]
        public void When_CreateAdminUser_Then_new_user_is_saved_to_user_repository()
        {
            // Given
            var userId = Guid.NewGuid();
            var companyId = 1234L;
            var request = new CreateAdminUserRequest()
            {
                ClientId = companyId,
                UserId = userId
            };
            var expectedUser = new User()
                               {
                                   Id = userId,
                                   CompanyId = companyId,
                                   Role = _adminRole,
                                   Site = _rootSite,
                                   CreatedBy = _nsbSystemUser
                               };
            var target = GetTarget();

            // When
            target.CreateAdminUser(request);

            // Then
            _userRepo.Verify(x => x.Save(expectedUser));
        }

        [Test]
        public void When_CreateAdminUser_Then_new_employee_is_created()
        {
            // Given
            var userId = Guid.NewGuid();
            const long companyId = 1234L;
            var request = new CreateAdminUserRequest()
            {
                ClientId = companyId,
                UserId = userId,
                Forename = "Maggie",
                Surname = "May",
                Email = "maggiemay@weloverod.com"
            };

            User createdUser = null;
            _userRepo.Setup(x => x.Save(It.IsAny<User>()))
                .Callback<User>(x => createdUser = x);


            var target = GetTarget();

            // When
            target.CreateAdminUser(request);

            // Then
            Assert.IsNotNull(createdUser.Employee);
            Assert.AreEqual(request.Forename, createdUser.Employee.Forename);
            Assert.AreEqual(request.Surname, createdUser.Employee.Surname);
            Assert.AreEqual(request.Email, createdUser.Employee.ContactDetails.First().Email);
            
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
    }
}