using BusinessSafe.MessageHandlers.Emails.Activation;
using NHibernate;
using NHibernate.Context;
using NServiceBus;
using StructureMap;
using BusinessSafe.Data.NHibernate.BusinessSafe;

namespace BusinessSafe.MessageHandlers.Emails.Core
{
    public class MessageModule : IMessageModule
    {
        private readonly ISessionFactory _sessionFactory;

        public MessageModule()
        {
            _sessionFactory = ObjectFactory.GetInstance<IBusinessSafeSessionFactory>().GetSessionFactory();
        }

        public void HandleBeginMessage()
        {
            Log4NetHelper.Log.Debug("MessageModule HandleBeginMessage");
            CurrentSessionContext.Bind(_sessionFactory.OpenSession());
        }

        public void HandleEndMessage()
        {
            Log4NetHelper.Log.Debug("MessageModule HandleEndMessage");
            Cleardown();
        }

        public void HandleError()
        {
            Log4NetHelper.Log.Debug("MessageModule HandleError");
            Cleardown();
        }

        private void Cleardown()
        {
            _sessionFactory.GetCurrentSession().Dispose();
            CurrentSessionContext.Unbind(_sessionFactory);
            Log4NetHelper.Log.Debug("MessageModule Cleardown");
        }
    }
}
