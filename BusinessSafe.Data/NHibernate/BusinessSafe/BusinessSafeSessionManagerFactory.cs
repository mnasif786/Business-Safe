using BusinessSafe.Data.NHibernate.BusinessSafe;

namespace Peninsula.Online.Data.NHibernate.ApplicationServices
{
    public interface IBusinessSafeSessionManagerFactory
    {
        BusinessSafeSessionManager CreateInstance();
    }

    public class BusinessSafeSessionManagerFactory : IBusinessSafeSessionManagerFactory
    {
        private readonly IBusinessSafeSessionFactory _businessSafeSessionFactory;
        public BusinessSafeSessionManagerFactory(IBusinessSafeSessionFactory businessSafeSessionFactory)
        {
            _businessSafeSessionFactory = businessSafeSessionFactory;
        }

        public BusinessSafeSessionManager CreateInstance()
        {
            return new BusinessSafeSessionManager(_businessSafeSessionFactory);
        }

    }
}