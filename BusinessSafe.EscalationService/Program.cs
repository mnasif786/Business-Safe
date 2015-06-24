using System;
using System.Configuration;
using BusinessSafe.EscalationService.Activation;
using NServiceBus;
using Topshelf;

namespace BusinessSafe.EscalationService
{
    public class Program
    {
        public static void Main()
        {
            try
            {

                HostFactory.Run(x =>
                {
                    Log4NetHelper.Log.Debug("EscalationService Main Start");

                    IBus bus = Configure
                        .With()
                        .DefaultBuilder()
                        .Log4Net()
                        .UnicastBus()
                        .MsmqTransport()
                        .XmlSerializer()
                        .DisableRavenInstall()
                        .SendOnly();

                    Bootstrapper.Run(bus);


                    var serviceName = ConfigurationManager.AppSettings["serviceName"];
                    x.Service<EscalationService>(s =>
                    {
                        s.ConstructUsing(name => new EscalationService());
                        s.WhenStarted(tc => tc.Start());
                        s.WhenStopped(tc => tc.Stop());
                    });
                    x.RunAsNetworkService();
                    x.SetDescription("Business Safe Escalation Service");
                    x.SetDisplayName("Business Safe Escalation Service");
                    x.SetServiceName(serviceName);
                });
            }
            catch (Exception ex)
            {
                Log4NetHelper.Log.Error(ex);
            }
        }
    }
}
