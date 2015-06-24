using BusinessSafe.WebSite.Areas.Company.Tasks;

namespace BusinessSafe.WebSite.Areas.Company.Factories
{
    public interface ICompanyDefaultsTaskFactory
    {
        ICompanyDefaultsSaveTask CreateSaveTask(string companyDefaultType);
        ICompanyDefaultDeleteTask CreateMarkForDeletedTask(string companyDefaultType);
    }
}