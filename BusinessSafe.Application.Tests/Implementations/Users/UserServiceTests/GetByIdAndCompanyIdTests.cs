using System;
using System.Collections.Generic;

using BusinessSafe.Application.Implementations.Users;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.Users.UserServiceTests
{
    [TestFixture]
    public class GetByIdAndCompanyIdTests
    {
        private Mock<IUserRepository> _userRepo;
        private Mock<ISiteStructureElementRepository> _siteRepository;
        private Mock<IRoleRepository> _roleRepository;
        private Mock<IPeninsulaLog> _log;
        private Mock<IUserForAuditingRepository> _userForAuditingRepo;
        
        [SetUp]
        public void SetUp()
        {
            _userRepo = new Mock<IUserRepository>();
            _siteRepository = new Mock<ISiteStructureElementRepository>();
            _roleRepository = new Mock<IRoleRepository>();
            _log = new Mock<IPeninsulaLog>();
            _userForAuditingRepo = new Mock<IUserForAuditingRepository>();
        }


        [Test]
        public void Given_that_get_by_id_and_company_id_called_Then_should_call_appropiate_methods()
        {
            //Given
            var target = CreateUSerService();

            const int companyId = 1;
            var userId = Guid.Empty;

            var returnedUser = new User()
                                   {
                                       Role = new Role()
                                                   {
                                                       Permissions = new List<RolePermission>()
                                                   },
                                       Employee = new Employee(),
                                       Site = new Site()
                                                  {
                                                      Name = "Main Site"
                                                  }
                                   };
            _userRepo
                .Setup(x => x.GetByIdAndCompanyId(userId, companyId))
                .Returns(returnedUser);

            //When
            target.GetIncludingEmployeeAndSiteByIdAndCompanyId(userId, companyId);

            //Then
            _userRepo.VerifyAll();
        }
        
        private UserService CreateUSerService()
        {
            var target = new UserService(
                _userForAuditingRepo.Object, 
                _siteRepository.Object,
                _roleRepository.Object,
                null,
                _userRepo.Object, _log.Object, null, null);
            return target;
        }
    }
}