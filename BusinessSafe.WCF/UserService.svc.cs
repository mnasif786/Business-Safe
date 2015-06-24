using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Data.NHibernate.BusinessSafe;
using NHibernate.Context;
using StructureMap;
using NHibernate;

namespace BusinessSafe.WCF
{
    public class UserService : IUserService
    {
        private BusinessSafe.Application.Contracts.Users.IUserService _applicationLayerUserService;
        private ISessionFactory _sessionFactory;

        public UserService()
        {
            
        }

        public UserDto[] GetIncludingRoleByIdsAndCompanyId(Guid[] ids, long companyId)
        {
            _applicationLayerUserService =
                ObjectFactory.GetInstance<BusinessSafe.Application.Contracts.Users.IUserService>();

            _sessionFactory = ObjectFactory.GetInstance<IBusinessSafeSessionFactory>().GetSessionFactory();
            CurrentSessionContext.Bind(_sessionFactory.OpenSession());

            try
            {
                return _applicationLayerUserService.GetIncludingRoleByIdsAndCompanyId(ids, companyId).ToArray();
            }
            finally
            {
                _sessionFactory.GetCurrentSession().Dispose();
                CurrentSessionContext.Unbind(_sessionFactory);
                
            }
        }

        public RoleDto[] GetRoles(long companyId)
        {
            var rolesService =ObjectFactory.GetInstance<IRolesService>();

            _sessionFactory = ObjectFactory.GetInstance<IBusinessSafeSessionFactory>().GetSessionFactory();
            CurrentSessionContext.Bind(_sessionFactory.OpenSession());

            try
            {
                return rolesService.GetAllRoles(companyId).ToArray();
            }
            finally
            {
                _sessionFactory.GetCurrentSession().Dispose();
                CurrentSessionContext.Unbind(_sessionFactory);

            }
        }
    }
}
