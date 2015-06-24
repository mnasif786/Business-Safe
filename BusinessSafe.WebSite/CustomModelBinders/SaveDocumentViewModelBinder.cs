using System.Web.Mvc;
using BusinessSafe.WebSite.Controllers.AutoMappers;
using BusinessSafe.WebSite.ViewModels;
using StructureMap;

namespace BusinessSafe.WebSite.CustomModelBinders
{
    public class SaveDocumentViewModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var formCollection = new FormCollection(controllerContext.HttpContext.Request.Form);

            var documentRequestMapper = ObjectFactory.GetInstance<IDocumentRequestMapper>();
            
            var createDocumentRequests = documentRequestMapper.MapCreateRequests(formCollection);
            var deleteDocumentRequests = documentRequestMapper.MapDeleteRequests(formCollection);

            var result = new DocumentsToSaveViewModel()
                             {
                                 CreateDocumentRequests = createDocumentRequests,
                                 DeleteDocumentRequests = deleteDocumentRequests
                             };
            return result;
        }

    }
}