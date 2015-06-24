using BusinessSafe.Application.Request;
using BusinessSafe.Application.Response;

namespace BusinessSafe.WebSite.Areas.Company.Tasks
{
    public interface INonEmployeeSaveTask
    {
        CompanyDefaultSaveResponse Execute(SaveNonEmployeeRequest request);
    }
}