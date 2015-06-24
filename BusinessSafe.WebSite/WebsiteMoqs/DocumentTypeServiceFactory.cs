using System;
using System.Configuration;
using BusinessSafe.WebSite.DocumentTypeService;
using StructureMap;

namespace BusinessSafe.WebSite.WebsiteMoqs
{
    public class DocumentTypeServiceFactory
    {
        public IDocumentTypeService Create()
        {
            var pluginType = ConfigurationManager.AppSettings["DocumentTypeService"];

            if (!string.IsNullOrEmpty(pluginType))
            {
                var documentTypeService = Type.GetType(pluginType);

                return (IDocumentTypeService)ObjectFactory.GetInstance(documentTypeService);
            }

            return new DocumentTypeServiceClient("DocumentTypeService");
        }
    }
}
