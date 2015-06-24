
using System.Web.Http.Controllers;

namespace BusinessSafe.API.Areas.HelpPage
{
    public interface IResponseDocumentationProvider
    {
        string GetResponseDocumentation(HttpActionDescriptor actionDescriptor);
    }
}
