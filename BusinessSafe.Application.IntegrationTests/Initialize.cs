using System;
using BusinessSafe.Application.Common;
using BusinessSafe.Data.NHibernate.BusinessSafe;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using StructureMap;
using NServiceBus;

namespace BusinessSafe.Application.IntegrationTests
{
    [SetUpFixture]
    public class Initialize
    {
        [SetUp]
        public void Setup()
        {
            IBus bus = null;
            try
            {
                bus = Configure.With()
                    .DefineEndpointName("BusinessSafe.Application.IntegrationTests")
                    .DefaultBuilder()
                    .XmlSerializer()
                    .MsmqTransport()
                    .UnicastBus()
                    .SendOnly();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }


            ObjectFactory.Configure(
                x =>
                    {
                        x.AddRegistry<ApplicationRegistry>();
                        x.For<IBusinessSafeSessionFactory>().Use<BusinessSafeSessionFactory>();
                        x.For<IBusinessSafeSessionManager>().HybridHttpOrThreadLocalScoped().Use<BusinessSafeSessionManager>();
                        x.For<IBusinessSafeSessionManagerFactory>().Use<BusinessSafeSessionManagerFactory>();
                        x.For<IBus>().Use(bus);
                    });

            ObjectFactory.Inject<IPeninsulaLog>(new StubPeninsulaLog());

            if (Environment.MachineName != "PBSBSOSTAGE1"
            && Environment.MachineName != "PBSBS01"
            && Environment.MachineName != "PBSBS02"
            && Environment.MachineName != "PBSBS03"
            && Environment.MachineName != "PBSBS04"
            && Environment.MachineName != "PBSBS05")
            {
                HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            }
        }
    }
}
