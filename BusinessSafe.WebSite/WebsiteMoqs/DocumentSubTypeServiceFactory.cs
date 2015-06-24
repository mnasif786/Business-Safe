using System;
using System.Configuration;
using BusinessSafe.WebSite.DocumentSubTypeService;
using StructureMap;

namespace BusinessSafe.WebSite.WebsiteMoqs
{
    public class DocumentSubTypeServiceFactory
    {
        public IDocumentSubTypeService Create()
        {
            var pluginType = ConfigurationManager.AppSettings["DocumentSubTypeService"];

            if (!string.IsNullOrEmpty(pluginType))
            {
                var documentTypeService = Type.GetType(pluginType);

                return (IDocumentSubTypeService)ObjectFactory.GetInstance(documentTypeService);
            }

            return new DocumentSubTypeServiceClient("DocumentSubTypeService");
        }
    }
}