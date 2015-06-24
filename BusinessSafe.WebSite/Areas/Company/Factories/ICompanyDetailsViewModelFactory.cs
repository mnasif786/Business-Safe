using System;
using BusinessSafe.WebSite.Areas.Company.ViewModels;

namespace BusinessSafe.WebSite.Areas.Company.Factories
{
    public interface ICompanyDetailsViewModelFactory
    {
        ICompanyDetailsViewModelFactory Id(long id);
        ICompanyDetailsViewModelFactory AddressLine1(string address1);
        ICompanyDetailsViewModelFactory AddressLine2(string address2);
        ICompanyDetailsViewModelFactory AddressLine3(string address3);
        ICompanyDetailsViewModelFactory AddressLine4(string address4);
        ICompanyDetailsViewModelFactory CAN(string can);
        ICompanyDetailsViewModelFactory CompanyName(string companyName);
        ICompanyDetailsViewModelFactory MainContact(string mainContact);
        ICompanyDetailsViewModelFactory Postcode(string postcode);
        ICompanyDetailsViewModelFactory Telephone(string telephone);
        ICompanyDetailsViewModelFactory Website(string website);
        ICompanyDetailsViewModelFactory EmployeeId(Guid employeeId);
        ICompanyDetailsViewModelFactory EmployeeName(string employeeName);
        CompanyDetailsViewModel GetViewModel();
    }
}