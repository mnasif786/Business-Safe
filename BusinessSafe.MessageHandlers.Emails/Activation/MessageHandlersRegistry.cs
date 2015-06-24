using System;
using System.Collections.Generic;
using BusinessSafe.Application.Common;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Implementations.Company;
using BusinessSafe.Application.RestAPI;
using BusinessSafe.Data.Common;
using BusinessSafe.Data.NHibernate.BusinessSafe;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.MessageHandlers.Emails.ClientDocumentService;
using BusinessSafe.MessageHandlers.Emails.DocumentLibraryService;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using NServiceBus;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace BusinessSafe.MessageHandlers.Emails.Activation
{
    public class MessageHandlersRegistry : Registry
    {
        public MessageHandlersRegistry()
        {
            Configure(x =>
                          {
                              Log4NetHelper.Log.Debug("MessageHandlersRegistry Configure Start");
                              For<IEmailSender>().Use<EmailSender.EmailSender>();

                              For<IBusinessSafeEmailLinkBaseUrlConfiguration>()
                                            .Use(new BusinessSafeEmailLinkBaseUrlConfiguration(
                                                     new Dictionary<string, string>
                                                                       {
                                                                           {"PBS43758", "http://businesssafe.dev-peninsula-online.com"},
                                                                           {"PBS44109", "http://businesssafe.dev-peninsula-online.com"},
                                                                           {"PBS43516", "http://businesssafe.dev-peninsula-online.com"},
                                                                           {"PBS43083", "http://businesssafe.dev-peninsula-online.com"},
                                                                           {"PBS42691", "http://businesssafe.dev-peninsula-online.com"},
                                                                           {"PBS42848", "http://businesssafe.dev-peninsula-online.com"},
                                                                           {"PBS42576", "http://businesssafe.dev-peninsula-online.com"},
                                                                           {"PBS43753", "http://businesssafe.dev-peninsula-online.com"},
                                                                           {"PBS44143", "http://businesssafe.dev-peninsula-online.com"},
                                                                           {"PBSCIHRO1", "http://businesssafe.dev-peninsula-online.com"},
                                                                           {"PBSWEBTEST1", "http://businesssafe.peninsula-uk.local"},
                                                                           {"UATBSOCI1", "http://businesssafe.dev-peninsula-online.com"},
                                                                           {"UATBSO1", "http://businesssafe.peninsula-uk.local"},
                                                                           {"UATBSO2", "http://businesssafe.peninsula-uk.local"},
                                                                           {"PBSSERVICEBUS1", "https://businesssafe.peninsula-online.com"},
                                                                       }));

                              For<ISafeCheckEmailLinkBaseUrlConfiguration>()
                                            .Use(new SafeCheckEmailLinkBaseUrlConfiguration(
                                                     new Dictionary<string, string>
                                                                       {
                                                                           {"PBS$$109", "http://localhost:8107"},
                                                                           {"PBS43758", "http://localhost:8107"},
                                                                           {"PBS43516", "http://localhost:8107"},
                                                                           {"PBS43083", "http://localhost:8107"},
                                                                           {"PBS42691", "http://localhost:8107"},
                                                                           {"PBS42848", "http://localhost:8107"},
                                                                           {"PBS42576", "http://localhost:8107"},
                                                                           {"PBS43753", "http://localhost:8107"},
                                                                           {"PBS44143", "http://localhost:8107"},
                                                                           {"PBSCIHRO1", "http://localhost:8107"},
                                                                           {"PBSWEBTEST1", "http://uatbso1:8107"},
                                                                           {"UATBSOCI1", "http://localhost:8107"},
                                                                           {"UATBSO1", "http://uatbso1:8107"},
                                                                           {"UATBSO2", "http://uatbso1:8107"},
                                                                           {"PBSSERVICEBUS1", "http://safecheck"},
                                                                       }));

                              x.ImportRegistry(typeof(ApplicationRegistry));
                              //x.ImportRegistry(typeof(DataRegistry));
                              ForSingletonOf<IBusinessSafeSessionFactory>().Use<BusinessSafeSessionFactory>();
                              For<IBusinessSafeSessionManager>().HybridHttpOrThreadLocalScoped().Use<CurrentContextSessionManager>();
                              For<IDocumentLibraryService>().HybridHttpOrThreadLocalScoped().Use(new DocumentLibraryServiceClient("DocumentLibraryService"));
                              For<IClientDocumentService>().HybridHttpOrThreadLocalScoped().Use(new ClientDocumentServiceClient("ClientDocumentService"));
                              For<IClientService>().Use<ClientService>();
                              
                              
                              For<ICompanyDetailsService>().Use<CompanyDetailsService>();


                              Log4NetHelper.Log.Debug("MessageHandlersRegistry Configure End");
                          });
          
        }
    }
}