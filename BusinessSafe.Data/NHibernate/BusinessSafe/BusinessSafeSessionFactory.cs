using BusinessSafe.Data.Common;
using BusinessSafe.Data.NHibernate.Helpers;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Event;
using Peninsula.Online.Data.NHibernate;

namespace BusinessSafe.Data.NHibernate.BusinessSafe
{
    public interface IBusinessSafeSessionFactory : IServicesSessionFactory
    {
        void DisposeSessionFactory();
    }

    public class BusinessSafeSessionFactory : IBusinessSafeSessionFactory
    {
        private static ISessionFactory _sessionFactory;

        public BusinessSafeSessionFactory()
        {
            var hibernateConfigFilePath = new NHibernateConfigPathGenerator(Database.BusinessSafe).GetConfigFilePath();
            Log4NetHelper.Logger.Debug("BusinessSafeSessionFactory() - NHibernate Configuration File path: " + hibernateConfigFilePath);

            var configuration = new Configuration();
            configuration.Configure(hibernateConfigFilePath);
            configuration.SetProperty(Environment.ConnectionStringName, "BusinessSafe");

            configuration.EventListeners.PostUpdateEventListeners = new IPostUpdateEventListener[]
                                                                        {
                                                                            new AuditUpdateListener()
                                                                        };
            configuration.EventListeners.PostInsertEventListeners = new IPostInsertEventListener[]
                                                                        {
                                                                            new AuditUpdateListener()
                                                                        };
            configuration.EventListeners.PostDeleteEventListeners = new IPostDeleteEventListener[]
                                                                        {
                                                                            new AuditUpdateListener()
                                                                        };
            configuration.EventListeners.PostCollectionUpdateEventListeners = new IPostCollectionUpdateEventListener[]
                                                                        {
                                                                            new AuditUpdateListener()
                                                                        };

            _sessionFactory = configuration.BuildSessionFactory();
        }

        public ISession GetSession()
        {
            return _sessionFactory.OpenSession();
        }

        public ISessionFactory GetSessionFactory()
        {
            return _sessionFactory;
        }

        public void DisposeSessionFactory()
        {
            _sessionFactory.Close();
            _sessionFactory.Dispose();
        }
    }
}