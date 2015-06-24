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
    public class UserSearchTests
    {
        private Mock<IUserRepository> _userRepo;
        private Mock<ISiteStructureElementRepository> _siteRepository;
        private Mock<IRoleRepository> _roleRepository;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void SetUp()
        {
        
            _userRepo = new Mock<IUserRepository>();
            _siteRepository = new Mock<ISiteStructureElementRepository>();
            _roleRepository = new Mock<IRoleRepository>();
            

            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_valid_search_request_When_search_users_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateUserService();
            var searchCriteria = new SearchUsersRequest()
                                {
                                    CompanyId = 185674L, 
                                    ForenameLike = "Tyrion", 
                                    SurnameLike = "Lannister", 
                                    AllowedSiteIds = new List<long>() { 123L, 457L, 35473L }, 
                                    SiteId = 3453451L, 
                                    ShowDeleted = false, 
                                    MaximumResults = 1000
                                };
            var returnedUsers = new List<User>();

            _userRepo
                .Setup(x => x.Search(
                    searchCriteria.CompanyId,
                    searchCriteria.ForenameLike,
                    searchCriteria.SurnameLike, 
                    searchCriteria.AllowedSiteIds, 
                    searchCriteria.SiteId,
                    searchCriteria.ShowDeleted, 
                    searchCriteria.MaximumResults))
                .Returns(returnedUsers);

            //When
            target.Search(searchCriteria);

            //Then
            _userRepo.VerifyAll();
        }

        private UserService CreateUserService()
        {
            var target = new UserService(
                null, 
                _siteRepository.Object,
                _roleRepository.Object,
                null, _userRepo.Object, _log.Object, null, null);

            return target;
        }    
    }
}