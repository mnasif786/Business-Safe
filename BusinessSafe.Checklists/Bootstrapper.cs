using System.Web.Mvc;
using NServiceBus;
using StructureMap;

namespace BusinessSafe.Checklists
{
    public static class Bootstrapper
    {
        private static IBus _bus;

        public static void Run()
        {
            _bus = Configure.With()
                .DefaultBuilder()
                .Log4Net()
                .UnicastBus()
                .MsmqTransport()
                .XmlSerializer()
                .DisableRavenInstall()
                .CreateBus()
                .Start(
                    () =>
                    Configure.Instance.ForInstallationOn<NServiceBus.Installation.Environments.Windows>().Install());

            ObjectFactory.Container.Configure(x =>
            {
                x.AddRegistry<WebsiteRegistry>();
                x.For<IBus>().Use(_bus);
            });

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(ObjectFactory.Container));
        }
    }
}