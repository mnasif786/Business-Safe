using System;
using System.Collections.Generic;
using BusinessSafe.Application.Implementations.Users;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.Users.UserServiceTests
{
    [TestFixture]
    public class SetRoleAndSiteTests
    {
        private Mock<IUserForAuditingRepository> _userForAuditingRepo;
        private Mock<IUserRepository> _userRepo;
        private Mock<ISiteStructureElementRepository> _siteStructureRepository;
        private Mock<ISiteRepository> _siteRepository;
        private Mock<IRoleRepository> _roleRepository;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void SetUp()
        {
            _userForAuditingRepo = new Mock<IUserForAuditingRepository>();
            _userRepo = new Mock<IUserRepository>();
            _siteStructureRepository = new Mock<ISiteStructureElementRepository>();
            _roleRepository = new Mock<IRoleRepository>();
            _log = new Mock<IPeninsulaLog>();
            _siteRepository = new Mock<ISiteRepository>();
        }

        [Test]
        public void Given_we_are_setting_user_access_to_all_sites_when_SetRoleAndSite_then_siteid_is_set_to_the_main_site()
        {
            //Given
            var target = CreateUserService();
            long companyId = 999L;
            var mainSiteId = 9586L;

            var userToUpdate = new User
            {
                Id = new Guid("00000000-0000-0000-0000-0000000000A1"),
                CompanyId = companyId
            };

            var actioningUser = new UserForAuditing
            {
                Id = new Guid("00000000-0000-0000-0000-0000000000A2"),
                CompanyId = companyId
            };


            var role = new Role
            {
                Id = new Guid("00000000-0000-0000-0000-0000000000A3"),
                CompanyId = companyId
            };

            var mainSite = new Site {Id = mainSiteId, Name = "Main Site", ClientId = companyId,Parent = null};

            _userRepo.Setup(x => x.GetByIdAndCompanyIdIncludeDeleted(userToUpdate.Id, companyId)).Returns(userToUpdate);
            _userForAuditingRepo.Setup(x => x.GetByIdAndCompanyId(actioningUser.Id, companyId)).Returns(actioningUser);
            _roleRepository.Setup(x => x.LoadById(role.Id)).Returns(role);
            _siteRepository.Setup(x => x.GetSiteAddressByCompanyId(It.IsAny<long>()))
                .Returns(() => new List<Site>() {new Site() {Id = 123, Parent = mainSite}, mainSite});

            _siteStructureRepository.Setup(x => x.GetByCompanyId(It.IsAny<long>()))
                .Returns(() => new List<SiteStructureElement>() { new Site() { Id = 123, Parent = mainSite }, mainSite });

            User savedUser = null;
            _userRepo.Setup(x => x.Save(It.IsAny<User>()))
                .Callback<User>(parameter => savedUser = parameter);

            var request = new SetUserRoleAndSiteRequest
            {
                ActioningUserId = actioningUser.Id,
                UserToUpdateId = userToUpdate.Id,
                RoleId = role.Id,
                CompanyId = companyId,
                SiteId = 236246245,
                PermissionsApplyToAllSites = true
            };

            //WHEN
            target.SetRoleAndSite(request);

            //THEN
            Assert.That(savedUser.Id, Is.EqualTo(userToUpdate.Id));
            Assert.That(savedUser.Role.Id, Is.EqualTo(role.Id));
            Assert.That(savedUser.Site.Id, Is.EqualTo(mainSiteId));
        }

        [Test]
        public void Given_we_are_setting_user_access_to_a_child_site_when_SetRoleAndSite_then_siteid_is_set_to_the_child_site()
        {
            //Given
            var target = CreateUserService();
            long companyId = 999L;

            var userToUpdate = new User
            {
                Id = new Guid("00000000-0000-0000-0000-0000000000A1"),
                CompanyId = companyId
            };

            var actioningUser = new UserForAuditing
            {
                Id = new Guid("00000000-0000-0000-0000-0000000000A2"),
                CompanyId = companyId
            };


            var role = new Role
            {
                Id = new Guid("00000000-0000-0000-0000-0000000000A3"),
                CompanyId = companyId
            };

            var mainSite = new Site { Id = 123123, Name = "Main Site", ClientId = companyId, Parent = null };
            var childSite = new Site { Id = 4678478, Name = "Main Site", ClientId = companyId, Parent = mainSite };

            _userRepo.Setup(x => x.GetByIdAndCompanyIdIncludeDeleted(userToUpdate.Id, companyId)).Returns(userToUpdate);
            _userForAuditingRepo.Setup(x => x.GetByIdAndCompanyId(actioningUser.Id, companyId)).Returns(actioningUser);
            _roleRepository.Setup(x => x.LoadById(role.Id)).Returns(role);

            _siteStructureRepository.Setup(x => x.GetByCompanyId(It.IsAny<long>()))
                .Returns(() => new List<SiteStructureElement>() { childSite, mainSite });

            _siteStructureRepository.Setup(x => x.LoadById(childSite.Id))
                .Returns(() =>  childSite);

            User savedUser = null;
            _userRepo.Setup(x => x.Save(It.IsAny<User>()))
                .Callback<User>(parameter => savedUser = parameter);

            var request = new SetUserRoleAndSiteRequest
            {
                ActioningUserId = actioningUser.Id,
                UserToUpdateId = userToUpdate.Id,
                RoleId = role.Id,
                CompanyId = companyId,
                SiteId = childSite.Id,
                PermissionsApplyToAllSites = false
            };


            //WHEN
            target.SetRoleAndSite(request);

            //THEN
            Assert.That(savedUser.Id, Is.EqualTo(userToUpdate.Id));
            Assert.That(savedUser.Role.Id, Is.EqualTo(role.Id));
            Assert.That(savedUser.Site.Id, Is.EqualTo(childSite.Id));
        }

        private UserService CreateUserService()
        {
            var target = new UserService(
                _userForAuditingRepo.Object, 
                _siteStructureRepository.Object,
                _roleRepository.Object,
                null,
                _userRepo.Object, _log.Object, null, null);

            return target;
        }    
    }
}