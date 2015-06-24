using System;
using System.Data;
using BusinessSafe.Data.Common;
using BusinessSafe.Data.NHibernate.BusinessSafe;
using NHibernate;

namespace Peninsula.Online.Data.NHibernate.ApplicationServices
{
    public interface IBusinessSafeSessionManager
    {
        void StartTransactionlessSession();
        ISession Session { get; }
        void CloseSession();
        void FlushSession();
        void Rollback();
        void DisposeFactory();
    }

    public class BusinessSafeSessionManager : IBusinessSafeSessionManager
    {
        private readonly IBusinessSafeSessionFactory _businessSafeSessionFactory;
        private ISession _session;
        private ITransaction _transaction;

        public BusinessSafeSessionManager(IBusinessSafeSessionFactory businessSafeSessionFactory)
        {
            _businessSafeSessionFactory = businessSafeSessionFactory;
        }

        public void DisposeFactory()
        {
            _businessSafeSessionFactory.DisposeSessionFactory();
        }

        public void StartTransactionlessSession()
        {
            _session = _businessSafeSessionFactory.GetSession();
        }

        public ISession Session
        {
            get
            {
                if (_session == null || _session.IsOpen==false || _session.IsOpen==false)
                {
                    _session = _businessSafeSessionFactory.GetSession();
                    _transaction = _session.BeginTransaction(IsolationLevel.ReadUncommitted);
                }

                return _session;
            }
        }

        public void CloseSession()
        {
            if (_session == null)
            {
                Log4NetHelper.Logger.Debug("BusinessSafeSessionManager.CloseSession() - no session to close!");
                return;
            }
            try
            {
                if (_transaction != null)
                {
                    if (_transaction.IsActive && _transaction.WasCommitted == false)
                    {
                        try
                        {
                            //If you are getting an exception here that reads something like 'collection SiteStructureElement.Children was not processed by flush()'
                            //then this is because a reference to a Site or SiteStuctureElement is lazy, and is being lazy loaded in the AuditUpdateListener. Any
                            //attempt to lazy load an entity in these EventListeners will cause this exception, it is a known NHibernate error. Ensure the site is
                            //loaded before it gets to the AuditUpdateListener.
                            _transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            _transaction.Rollback();
                            throw;
                        }

                    }
                    else if (_transaction.WasRolledBack == false && _transaction.WasCommitted == false && _transaction.IsActive)
                    {
                        Log4NetHelper.Logger.Debug("BusinessSafeSessionManager.CloseSession() - Transaction RollBack");
                        _transaction.Rollback();
                    }
                }
            }
            finally
            {
                if (_session.IsOpen)
                {
                    _session.Close();
                }
                _session = null;
                _transaction = null;
            }
        }

        public void FlushSession()
        {
            _session.Flush();
        }

        public void Rollback()
        {
            if (_transaction != null)
            {
                if (_transaction.IsActive && _transaction.WasCommitted == false)
                {
                    Log4NetHelper.Logger.Debug("Transaction Rolled Back");
                    _transaction.Rollback();
                }
            }
        }

    }
}