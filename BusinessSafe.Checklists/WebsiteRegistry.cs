using BusinessSafe.Application.Common;
using BusinessSafe.Checklists.ViewModelFactories;
using BusinessSafe.Data.NHibernate.BusinessSafe;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace BusinessSafe.Checklists
{
    public class WebsiteRegistry : Registry
    {
        public WebsiteRegistry()
        {
            ObjectFactory.Container.Configure(x =>
            {
                x.ForSingletonOf<IBusinessSafeSessionFactory>().Use<BusinessSafeSessionFactory>();
                x.For<IBusinessSafeSessionManager>().HybridHttpOrThreadLocalScoped().Use<BusinessSafeSessionManager>();
                x.For<IEmployeeChecklistViewModelFactory>().Use<EmployeeChecklistViewModelFactory>();
                x.AddRegistry<ApplicationRegistry>();
            });
        }
    }
}