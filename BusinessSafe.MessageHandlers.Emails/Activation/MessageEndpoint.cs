using System;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Data.NHibernate.BusinessSafe;
using BusinessSafe.MessageHandlers.Emails.ClientDocumentService;
using BusinessSafe.MessageHandlers.Emails.Controllers;
using BusinessSafe.MessageHandlers.Emails.DocumentLibraryService;
using NServiceBus;
using StructureMap;

namespace BusinessSafe.MessageHandlers.Emails.Activation
{
    [EndpointName("businesssafe.messagehandlers.emails")]
    public class MessageEndpoint : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public void Init()
        {
            try
            {
                Log4NetHelper.Log.Debug("MessageEndpoint Init Start NServiceBus Configuration");

                ObjectFactory.Container.Configure(x => x.AddRegistry<MessageHandlersRegistry>());

                Log4NetHelper.Log.Debug("ViewPath is '" + new MailerController().ViewPath + "'");

                Configure.With()
                    .StructureMapBuilder(ObjectFactory.Container)
                    .MsmqTransport()
                    .Log4Net()
                    .NHibernateUnitOfWork()
                    .NHibernateSagaPersister()
                    .DBSubcriptionStorage()
                    .UseInMemoryGatewayPersister()
                    .UseNHibernateTimeoutPersister()
                    .SetEndpointSLA(new TimeSpan(0, 2, 0))
                    .DisableRavenInstall()
                    .UnicastBus()
                    //.DoNotAutoSubscribe()
                    .RunTimeoutManager()
                    .ForInstallationOn<NServiceBus.Installation.Environments.Windows>()
                    .Install();

                //IDocumentLibraryService documentLibraryService, ICompanyDetailsService companyDetailsService
                //tests
                try
                {
                    var t = ObjectFactory.GetInstance<IDocumentLibraryService>();
                }
                catch (Exception ex)
                {
                    throw new Exception("could not find IDocumentLibraryService");
                }
                try
                {
                    var t1 = ObjectFactory.GetInstance<ICompanyDetailsService>();
                }
                catch (Exception ex)
                {
                    throw new Exception("could not find ICompanyDetailsService");
                }
                try
                {
                    var t2 = ObjectFactory.GetInstance<IClientDocumentService>();
                }
                catch (Exception)
                {
                    throw new Exception("could not find IClientDocumentService");
                    throw;
                }
                //
                //var t3 = ObjectFactory.GetInstance<IDocumentLibraryService>();
                //var t4 = ObjectFactory.GetInstance<IDocumentLibraryService>();

                Log4NetHelper.Log.Debug("MessageEndpoint Init Building Session Factory");
                ObjectFactory.GetInstance<IBusinessSafeSessionFactory>().GetSessionFactory();

                Log4NetHelper.Log.Debug("MessageEndpoint Init End NServiceBus Configuration");
               
            }
            catch (Exception ex)
            {
                Log4NetHelper.Log.Error("MessageEndpoint Exception Encoutered", ex);
                throw;
            }
        }
    }
}