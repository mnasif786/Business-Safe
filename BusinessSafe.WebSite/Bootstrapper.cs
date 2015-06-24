using System;
using System.IO;
using System.Reflection;
using System.Web.Mvc;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using NServiceBus;
using StructureMap;

namespace BusinessSafe.WebSite
{
    public static class Bootstrapper
    {
        private static IBus bus;

        public static void Run()
        {
            
            bus = Configure.With()
                .DefaultBuilder()
                .DBSubcriptionStorage()
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
                                                      x.AddConfigurationFromXmlFile(GetStructureMapConfigFile());
                                                      x.AddRegistry<WebsiteRegistry>();
                                                      x.For<IBus>().Use(bus);
                                                  });

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(ObjectFactory.Container));
            Log.Add("Bootstrapper Run Complete");
        }

        private static string GetStructureMapConfigFile()
        {
            return PathToBin() + @"\StructureMap.config";
        }

        private static string PathToBin()
        {
            var codebasePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            if (codebasePath != null)
                return codebasePath.Replace("file:\\", "");

            throw new SystemException("Could not retrieve codebase directory path name");
        }
    }
}