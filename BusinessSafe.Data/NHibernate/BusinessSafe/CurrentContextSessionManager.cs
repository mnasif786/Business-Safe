using System;
using BusinessSafe.Data.NHibernate.BusinessSafe;
using NHibernate;
using NHibernate.Context;

namespace Peninsula.Online.Data.NHibernate.ApplicationServices
{
    public class CurrentContextSessionManager : IBusinessSafeSessionManager
    {
        private readonly ISessionFactory _sessionFactory;

        public CurrentContextSessionManager(IBusinessSafeSessionFactory businessSafeSessionFactory)
        {
            _sessionFactory = businessSafeSessionFactory.GetSessionFactory();
        }

        public void DisposeFactory()
        {
            throw new NotImplementedException();
        }

        public void StartTransactionlessSession()
        {
            throw new NotImplementedException();
        }

        public ISession Session
        {
            get
            {
                if (!CurrentSessionContext.HasBind(_sessionFactory))
                {
                    CurrentSessionContext.Bind(_sessionFactory.OpenSession());
                }

                return _sessionFactory.GetCurrentSession();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Dispose() { }

        public void CloseSession()
        {
            Session.Close();
        }

        public void FlushSession()
        {
            Session.Flush();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }
    }
}
