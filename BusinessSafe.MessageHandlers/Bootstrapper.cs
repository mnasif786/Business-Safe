using BusinessSafe.Application.Common;
using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.Implementations.Users;
using BusinessSafe.Data.NHibernate.BusinessSafe;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using StructureMap;

namespace BusinessSafe.MessageHandlers
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            ObjectFactory.Container.Configure(x =>
                                                  {
                                                      Log4NetHelper.Log.Debug("Bootstrapper Run Start");

                                                      x.For<IUserService>().HybridHttpOrThreadLocalScoped().Use<UserService>();
                                                      x.ForSingletonOf<IBusinessSafeSessionFactory>().Use<BusinessSafeSessionFactory>();
                                                      x.For<IBusinessSafeSessionManager>().HybridHttpOrThreadLocalScoped().Use<CurrentContextSessionManager>();
                                                      x.AddRegistry<ApplicationRegistry>();
                                                      x.For<IUserRegistrationService>().Use<RegistrationServiceStub>();
                                                      Log4NetHelper.Log.Debug("Bootstrapper Run Complete");
                                                  });
        }
    }
}