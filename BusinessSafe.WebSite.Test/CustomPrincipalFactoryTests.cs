using System;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.RestAPI;
using BusinessSafe.WebSite.Helpers;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests
{
    [TestFixture]
    public class CustomPrincipalFactoryTests
    {
        private Mock<ICacheHelper> _cacheHelper;
        private Mock<IUserService> _userService;
        private Mock<IClientService> _clientService;
        private int _companyId;
        private Guid _userId;
        private UserDto _userDto;
        private CompanyDetailsDto _companyDto;

        [SetUp]
        public void SetUp()
        {
            _cacheHelper = new Mock<ICacheHelper>();
            _userService = new Mock<IUserService>();
            _clientService = new Mock<IClientService>();
            _companyId = 500;
            _userId = Guid.NewGuid();
            _userDto = new UserDto()
                          {
                              Id = _userId
                          };
            _companyDto = new CompanyDetailsDto(_companyId, "Test Company", string.Empty, string.Empty, string.Empty,
                                                string.Empty, string.Empty, 100, string.Empty, string.Empty,
                                                string.Empty, string.Empty);

        }

        [Test]
        public void Given_user_and_company_not_in_cache_When_create_custom_principal_Then_should_call_correct_methods()
        {
            // Given
            string userCacheKey = "User:" + _userId;
            string companyCacheKey = "Company:" + _companyId;

            var target = CreateTarget();

            _userService
                .Setup(x => x.GetByIdAndCompanyIdIncludeDeleted(_userId, _companyId))
                .Returns(_userDto);

            _clientService
                .Setup(x => x.GetCompanyDetails(_companyId))
                .Returns(_companyDto);

            // When
            target.Create(_companyId, _userId);
            
            // Then
            _userService.VerifyAll();
            _cacheHelper.Verify(x => x.Add(_userDto, userCacheKey, 1));
            _clientService.VerifyAll();
            _cacheHelper.Verify(x => x.Add(It.Is<CompanyDto>(y => y.CompanyName == _companyDto.CompanyName), companyCacheKey, 1440));
        }

        [Test]
        public void Given_user_in_cache_and_company_not_in_cache_When_create_custom_principal_Then_should_call_correct_methods()
        {
            // Given
            string userCacheKey = "User:" + _userId;
            string companyCacheKey = "Company:" + _companyId;

            var target = CreateTarget();

            _clientService
                .Setup(x => x.GetCompanyDetails(_companyId))
                .Returns(_companyDto);

            var userDto = new UserDto();
            _cacheHelper
                .Setup(x => x.Load(userCacheKey, out userDto))
                .Returns(true);

            // When
            target.Create(_companyId, _userId);

            // Then
             _cacheHelper.Verify(x => x.Add(_userDto, userCacheKey, 5),Times.Never());
            _clientService.VerifyAll();
            _cacheHelper.Verify(x => x.Add(It.Is<CompanyDto>(y => y.CompanyName == _companyDto.CompanyName), companyCacheKey, 1440));
        }

        [Test]
        public void Given_user_not_in_cache_and_company_in_cache_When_create_custom_principal_Then_should_call_correct_methods()
        {
            // Given
            string userCacheKey = "User:" + _userId;
            string companyCacheKey = "Company:" + _companyId;

            var target = CreateTarget();

            _userService
                .Setup(x => x.GetByIdAndCompanyIdIncludeDeleted(_userId, _companyId))
                .Returns(_userDto);

            var companyDto = new CompanyDto();
            _cacheHelper
                .Setup(x => x.Load(companyCacheKey, out companyDto))
                .Returns(true);

            // When
            target.Create(_companyId, _userId);

            // Then
            _cacheHelper.Verify(x => x.Add(_userDto, userCacheKey, 1));
            _userService.VerifyAll();
            _cacheHelper.Verify(x => x.Add(It.Is<CompanyDto>(y => y.CompanyName == _companyDto.CompanyName), companyCacheKey, 1440),Times.Never());
        }

        [Test] public void When_create_custom_principal_Then_should_return_correct_result()
        {
            // Given
            var target = CreateTarget();

            _userService
                .Setup(x => x.GetByIdAndCompanyIdIncludeDeleted(_userId, _companyId))
                .Returns(_userDto);

            _clientService
                .Setup(x => x.GetCompanyDetails(_companyId))
                .Returns(_companyDto);

            // When
            var result = target.Create(_companyId, _userId);

            // Then
            Assert.That(result.UserId, Is.EqualTo(_userId));
            Assert.That(result.CompanyName, Is.EqualTo(_companyDto.CompanyName));
        }

        private CustomPrincipalFactory CreateTarget()
        {
            var target = new CustomPrincipalFactory(_userService.Object, _clientService.Object, _cacheHelper.Object);
            return target;
        }
    }
}