using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.Custom_Exceptions;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.Users;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.RepositoryContracts;
using NServiceBus;
using Peninsula.Online.Messages.Commands;

namespace BusinessSafe.Application.Implementations.Users
{
    public class UserService : IUserService
    {
        private readonly IUserForAuditingRepository _auditedUserRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISiteStructureElementRepository _siteStructureElementRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ISiteRepository _siteRepository;
        private readonly IPeninsulaLog _log;
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly IBus _bus;

        public UserService(IUserForAuditingRepository auditedUserRepository, ISiteStructureElementRepository siteStructureElementRepository, IRoleRepository roleRepository
            , ISiteRepository siteRepository, IUserRepository userRepository, IPeninsulaLog log, IUserRegistrationService userRegistrationService, IBus bus)
        {
            _auditedUserRepository = auditedUserRepository;
            _siteStructureElementRepository = siteStructureElementRepository;
            _roleRepository = roleRepository;
            _siteRepository = siteRepository;
            _log = log;
            _userRegistrationService = userRegistrationService;
            _userRepository = userRepository;
            _bus = bus;
        }

        public IEnumerable<UserDto> GetAll(long companyId)
        {
            var users = _userRepository.GetAllByCompanyId(companyId);
            return new UserDtoMapper().MapIncludingRole(users);
}

        public UserDto GetIncludingRoleByIdAndCompanyId(Guid id, long companyId)
        {
            var user = _userRepository.GetByIdAndCompanyId(id, companyId);
            return new UserDtoMapper().MapIncludingRole(user);
        }

        public IEnumerable<UserDto> GetIncludingRoleByIdsAndCompanyId(IEnumerable<Guid> ids, long companyId)
        {
            var user = _userRepository.GetByIdsAndCompanyId(ids.ToArray(), companyId);
            return new UserDtoMapper().MapIncludingRole(user);
        }

        public UserDto GetIncludingEmployeeAndSiteByIdAndCompanyId(Guid id, long companyId)
        {
            var user = _userRepository.GetByIdAndCompanyId(id, companyId);
            return new UserDtoMapper().MapIncludingEmployeeAndSite(user);
        }

        public UserDto GetByIdAndCompanyIdIncludeDeleted(Guid id, long companyId)
        {
            var user = _userRepository.GetByIdAndCompanyIdIncludeDeleted(id, companyId);
            return new UserDtoMapper().MapIncludingAllowedSitesAndEmployee(user);
        }

        public UserDto GetByIdAndCompanyId(Guid id, long companyId)
        {
             var user = _userRepository.GetByIdAndCompanyId(id, companyId);
            return new UserDtoMapper().MapIncludingAllowedSitesAndEmployee(user);

        }

        public void CreateAdminUser(CreateAdminUserRequest request)
        {
            _log.Add();

            var systemUser = _auditedUserRepository.GetSystemUser();
            var site = _siteRepository.GetRootSiteByCompanyId(request.ClientId);
            var role = _roleRepository.GetAdminRole();

            var employee = Employee.Create(new AddUpdateEmployeeParameters
                {
                    ClientId = request.ClientId,
                    Forename = request.Forename,
                    Surname = request.Surname,
                    Site = site.Self as Site
                }, systemUser);

            var contactDetails = EmployeeContactDetail.Create(new AddUpdateEmployeeContactDetailsParameters { Email = request.Email }, systemUser, employee);
            employee.AddContactDetails(contactDetails);
            var user = _userRepository.GetById(request.UserId) ?? User.CreateUser(request.UserId, request.ClientId, role, site, employee, systemUser);
            _userRepository.Save(user);
        }

        public IEnumerable<UserDto> Search(SearchUsersRequest request)
        {
            var users = _userRepository.Search(request.CompanyId, request.ForenameLike, request.SurnameLike, request.AllowedSiteIds, request.SiteId, request.ShowDeleted, request.MaximumResults);
            return new UserDtoMapper().MapIncludingEmployeeAndSite(users);
        }

        public void SetRoleAndSite(SetUserRoleAndSiteRequest request)
        {
            _log.Add(request);
            
            //todo: throw errors if things are not set.
            var userToUpdate = _userRepository.GetByIdAndCompanyIdIncludeDeleted(request.UserToUpdateId, request.CompanyId);
            _userRepository.Initialize(userToUpdate.Site);
            _userRepository.Initialize(userToUpdate.Role); //Need to do this or you will get the 'collection not processed by Flush()' auditing error!
            var actioningUser = _auditedUserRepository.GetByIdAndCompanyId(request.ActioningUserId, request.CompanyId);
            SiteStructureElement site = null;

            if (request.PermissionsApplyToAllSites)
            {
                site = GetMainSite(request.CompanyId);
                _siteStructureElementRepository.Initialize(site);
            }
            else if (request.SiteId != default(long))
            {
                site = _siteStructureElementRepository.LoadById(request.SiteId);
                _siteStructureElementRepository.Initialize(site);
            }

            Role role = null;

            if (request.RoleId != default(Guid))
            {
                role = _roleRepository.LoadById(request.RoleId);
                _roleRepository.Initialize(role);
            }

            userToUpdate.SetRoleAndSite(role, site, actioningUser);
            _userRepository.Save(userToUpdate);
            _userRepository.Flush();
        }

        private SiteStructureElement GetMainSite(long companyId)
        {
            return _siteStructureElementRepository.
                GetByCompanyId(companyId).First(x => x.IsMainSite);
        }

        public void ReinstateUser(Guid userIdToReinstated, Guid? actioningUserId)
        {
            _log.Add(new object[] {userIdToReinstated, actioningUserId.GetValueOrDefault()});

            var userToReinstate = _userRepository.GetById(userIdToReinstated);
            ReinstateUser(userToReinstate, actioningUserId);
        }

        public void ReinstateUser(Guid userIdToReinstate, long companyId, Guid currentUserId)
        {
            _log.Add(new object[] {userIdToReinstate, companyId, currentUserId});

            var userToReinstate = _userRepository.GetByIdAndCompanyIdIncludeDeleted(userIdToReinstate, companyId);
            ReinstateUser(userToReinstate, currentUserId);
        }

        private void ReinstateUser(User userToReinstate, Guid? actioningUserId)
        {
            if (userToReinstate != null && userToReinstate.Deleted)
            {
                var actioningUser = actioningUserId.HasValue ? _auditedUserRepository.GetById(actioningUserId.Value) : _auditedUserRepository.GetSystemUser();
                userToReinstate.ReinstateFromDelete(actioningUser);
                _userRepository.SaveOrUpdate(userToReinstate);

                _bus.Send(new ReinstateUser { UserId = userToReinstate.Id, ActioningUserId = actioningUser.Id });

                if (userToReinstate.IsRegistered == false)
                {
                    _bus.Send(new UpdateUserRegistration
                    {
                        UserId = userToReinstate.Id,
                        ActioningUserId = actioningUser.Id,
                        SecurityAnswer =
                            String.IsNullOrEmpty(userToReinstate.Employee.MainContactDetails.Telephone1)
                                ? userToReinstate.Employee.MainContactDetails.Telephone2
                                : userToReinstate.Employee.MainContactDetails.Telephone1,
                        Email = userToReinstate.Employee.MainContactDetails.Email
                    });
                }
            }
        }

        public void DeleteUser(Guid userToDeleteId, Guid? actioningUserId)
        {
            _log.Add(new object[] { userToDeleteId, actioningUserId });

            var userToDelete = _userRepository.GetById(userToDeleteId);

            if (userToDelete == null)
                return;

            var actioningUser = actioningUserId.HasValue ? _auditedUserRepository.GetById(actioningUserId.Value) : _auditedUserRepository.GetSystemUser();

            userToDelete.Delete(actioningUser);
            userToDelete.DisabledAuthenticationTokens();

            _userRepository.SaveOrUpdate(userToDelete);
        }

        public void DeleteUser(Guid userToDeleteId, long companyId, Guid actioningUserId)
        {
            _log.Add(new object[] { userToDeleteId, companyId, actioningUserId });

            var actioningUser = _auditedUserRepository.GetByIdAndCompanyId(actioningUserId, companyId);
            var userToDelete = _userRepository.GetByIdAndCompanyId(userToDeleteId, companyId);
            userToDelete.Delete(actioningUser);
            userToDelete.DisabledAuthenticationTokens();

            _userRepository.SaveOrUpdate(userToDelete);
        }

        public void RegisterUser(Guid userId)
        {
            _log.Add(new object[] { userId });

            var systemUser = _auditedUserRepository.GetSystemUser();
            var user = _userRepository.GetById(userId);
            user.Register(systemUser);
            _userRepository.Save(user);
        }

        public void CreateUser(CreateUserRequest request)
        {
            var systemUser = _auditedUserRepository.GetSystemUser();
            var site = _siteRepository.GetRootSiteByCompanyId(request.ClientId);
            var role = _roleRepository.GetById(request.RoleId);
            
            var user = User.CreateUser(request.UserId, request.ClientId, role, site, request.Forename, request.Surname, request.Email, systemUser);
            _userRepository.Save(user);
        }

        public void DisableAuthenticationTokens(Guid userId,Guid? actioningUserId)
        {
            var userToDelete = _userRepository.GetById(userId);

            if (userToDelete == null) return;

            var actioningUser = actioningUserId.HasValue ? _auditedUserRepository.GetById(actioningUserId.Value) : _auditedUserRepository.GetSystemUser();

            userToDelete.DisabledAuthenticationTokens();

            _userRepository.Save(userToDelete);
        }

        public void UpdateEmailAddress(Guid userId, long companyId, string email, Guid? actioningUserId)
        {
            var actioningUser = actioningUserId.HasValue ? _auditedUserRepository.GetById(actioningUserId.Value) : _auditedUserRepository.GetSystemUser();
            var user = _userRepository.GetByIdAndCompanyId(userId, companyId);

            //email is changing
            if (user.Employee.MainContactDetails != null && user.Employee.MainContactDetails.Email != email)
            {
                if (_userRegistrationService.HasEmailBeenRegistered(email))
                {
                    throw new EmailRegisteredToOtherUserException();
                }
                else
                {
                    user.Employee.SetEmail(email,actioningUser);
                }
            }
        }

    }
}