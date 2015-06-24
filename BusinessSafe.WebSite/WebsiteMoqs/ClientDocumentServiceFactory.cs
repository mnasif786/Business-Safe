using System;
using System.Configuration;
using BusinessSafe.WebSite.ClientDocumentService;
using StructureMap;

namespace BusinessSafe.WebSite.WebsiteMoqs
{
    public class ClientDocumentServiceFactory
    {
        public IClientDocumentService Create()
        {
            var pluginType = ConfigurationManager.AppSettings["ClientDocumentServiceType"];

            if (!string.IsNullOrEmpty(pluginType))
            {
                var clientDocumentService = Type.GetType(pluginType);

                return (IClientDocumentService)ObjectFactory.GetInstance(clientDocumentService);
            }

            return new ClientDocumentServiceClient("ClientDocumentService");
        }
    }
}