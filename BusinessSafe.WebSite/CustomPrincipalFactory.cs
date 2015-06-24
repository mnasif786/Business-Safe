using System;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.RestAPI;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Custom_Exceptions;
using BusinessSafe.WebSite.Helpers;

namespace BusinessSafe.WebSite
{
    public interface ICustomPrincipalFactory
    {
        CustomPrincipal Create(int companyId, Guid userId);
    }

    public class CustomPrincipalFactory : ICustomPrincipalFactory
    {
        private readonly IUserService _userService;
        private readonly ICacheHelper _cacheHelper;
        private readonly IClientService _clientService;
        
        public CustomPrincipalFactory(IUserService userService, IClientService clientService, ICacheHelper cacheHelper)
        {
            _userService = userService;
            _cacheHelper = cacheHelper;
            _clientService = clientService;
        }

        public CustomPrincipal Create(int companyId, Guid userId)
        {
            var userDto = GetUserDto(companyId, userId);
            var companyDto = GetCompanyDto(companyId);
            var customPrincipal = new CustomPrincipal(userDto, companyDto);
            return customPrincipal;
        }

        private CompanyDto GetCompanyDto(int companyId)
        {
            CompanyDto companyDto;
            var companyKey = CreateCompanyKey(companyId);
            if (_cacheHelper.Load(companyKey, out companyDto) == false)
            {
                var companyDetailsDto = _clientService.GetCompanyDetails(companyId);
                companyDto = new CompanyDto()
                                 {
                                     Id = companyDetailsDto.Id,
                                     CompanyName = companyDetailsDto.CompanyName
                                 };
                _cacheHelper.Add(companyDto, companyKey, CacheTime.OneDay);
            }
            return companyDto;
        }

        private UserDto GetUserDto(int companyId, Guid userId)
        {
            UserDto userDto;
            var userKey = CreateUserKey(userId);
            if (_cacheHelper.Load(userKey, out userDto) == false)
            {
                userDto = _userService.GetByIdAndCompanyIdIncludeDeleted(userId, companyId);
                _cacheHelper.Add(userDto, userKey, CacheTime.OneMinute);
            }
            if (userDto.Deleted)
            {
                throw new BusinessSafeUnauthorisedException(userId);
            }
            return userDto;
        }

        private static string CreateCompanyKey(int companyId)
        {
            var key = "Company:" + companyId;
            return key;
        }

        private static string CreateUserKey(Guid userId)
        {
            var key = "User:" + userId;
            return key;
        }
    }
}