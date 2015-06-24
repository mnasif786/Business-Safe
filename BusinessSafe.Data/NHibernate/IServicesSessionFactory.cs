using NHibernate;

namespace Peninsula.Online.Data.NHibernate
{
    public interface IServicesSessionFactory
    {
        ISession GetSession();
        ISessionFactory GetSessionFactory();
    }
}