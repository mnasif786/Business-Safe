using System;
using BusinessSafe.Data.NHibernate.BusinessSafe;
using NServiceBus;
using StructureMap;

namespace BusinessSafe.MessageHandlers
{
    [EndpointName("businesssafe.messagehandlers")]
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public void Init()
        {
            try
            {

                Log4NetHelper.Log.Debug("MessageEndpoint start NServiceBus Configuration");

                if (Environment.MachineName != "PBSBSOSTAGE1"
                    && Environment.MachineName != "PBSBS01"
                    && Environment.MachineName != "PBSBS02"
                    && Environment.MachineName != "PBSBS03"
                    && Environment.MachineName != "PBSBS04"
                    && Environment.MachineName != "PBSBS05"
                    && Environment.MachineName != "PBSSERVICEBUS1")
                {
                    Log4NetHelper.Log.Debug("NHibernateProfiler Initialize");
                    HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
                }
                else
                {
                    Log4NetHelper.Log.Debug("Not initialising NHibernateProfiler");
                }

                Configure.With()
                    .StructureMapBuilder(ObjectFactory.Container)
                    .MsmqTransport()
                    .Log4Net()
                    .DisableRavenInstall()
                    .NHibernateUnitOfWork()
                    .NHibernateSagaPersister()
                    .DBSubcriptionStorage()
                    .UseInMemoryGatewayPersister()
                    .UseNHibernateTimeoutPersister()
                    .SetEndpointSLA(new TimeSpan(0, 2, 0))
                    .DisableRavenInstall()
                    .UnicastBus()
                    .RunTimeoutManager()
                    .ForInstallationOn<NServiceBus.Installation.Environments.Windows>()
                    .Install();

                Bootstrapper.Run();

                Log4NetHelper.Log.Debug("MessageEndpoint Init Building Session Factory");
                ObjectFactory.GetInstance<IBusinessSafeSessionFactory>().GetSessionFactory();

                Log4NetHelper.Log.Debug("MessageEndpoint end NServiceBus Configuration");
            }
            catch (Exception ex)
            {
                Log4NetHelper.Log.Error("EndpointConfig Exception Encoutered", ex);
                throw;
            }
            
        }
    }
}
