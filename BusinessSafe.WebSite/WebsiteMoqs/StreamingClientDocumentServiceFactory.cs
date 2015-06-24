using System;
using System.Configuration;
using BusinessSafe.WebSite.DocumentTypeService;
using BusinessSafe.WebSite.StreamingClientDocumentService;
using StructureMap;

namespace BusinessSafe.WebSite.WebsiteMoqs
{
    public class StreamingClientDocumentServiceFactory
    {
        public IStreamingClientDocumentService Create()
        {
            var pluginType = ConfigurationManager.AppSettings["StreamingClientDocumentService"];

            if (!string.IsNullOrEmpty(pluginType))
            {
                var streamingClientDocumentService = Type.GetType(pluginType);

                return (IStreamingClientDocumentService)ObjectFactory.GetInstance(streamingClientDocumentService);
            }

            return new StreamingClientDocumentServiceClient("StreamingClientDocumentServiceClient");
        }
    }
}