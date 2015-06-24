using BusinessSafe.Application.Common;
using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Implementations.Users;
using NServiceBus;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using StructureMap;
using BusinessSafe.Data.NHibernate.BusinessSafe;

namespace BusinessSafe.WCF.Common
{
    public static class WcfRegistry
    {
        public static void Run()
        {
            ObjectFactory.Container.Configure(x =>
                                                  {
                                                      x.ForSingletonOf<IBusinessSafeSessionFactory>().Use
                                                          <BusinessSafeSessionFactory>();
                                                      x.For<IBusinessSafeSessionManager>().HybridHttpOrThreadLocalScoped
                                                          ().Use<CurrentContextSessionManager>();
                                                      x.For<IUserRegistrationService>().Use<RegistrationServiceStub>();
                                                      x.AddRegistry<ApplicationRegistry>();
                                                      x.For<IBus>().Use<FakeBus>();
                                                  });
        }
    }
}