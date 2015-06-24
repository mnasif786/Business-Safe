using System;
using System.Linq;
using System.Web.UI.WebControls;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.WebSite.ViewModels;
using BusinessSafe.WebSite.Extensions;

namespace BusinessSafe.WebSite.Areas.AccidentReports.Factories
{
    public class InjuredPersonViewModelFactory : IInjuredPersonViewModelFactory
    {
        private readonly IAccidentRecordService _accidentRecordService;
        private readonly IEmployeeService _employeeService;
        private readonly ILookupService _lookupService;
        private long _companyId;
        private long _accidentRecordId;

        public InjuredPersonViewModelFactory(
            IAccidentRecordService accidentRecordService,
            IEmployeeService employeeService,
            ILookupService lookupService)
        {
            _accidentRecordService = accidentRecordService;
            _employeeService = employeeService;
            _lookupService = lookupService;
        }

        public IInjuredPersonViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IInjuredPersonViewModelFactory WithAccidentRecordId(long accidentRecordId)
        {
            _accidentRecordId = accidentRecordId;
            return this;
        }

        public InjuredPersonViewModel GetViewModel()
        {
            var accidentRecord = _accidentRecordService.GetByIdAndCompanyIdWithEmployeeInjured(_accidentRecordId, _companyId);
            var employees = _employeeService.GetAll(_companyId);
            var countries = _lookupService.GetCountries();
            var othersInvolved = _lookupService.GetOthersInvolved();

            var viewModel = new InjuredPersonViewModel();
            viewModel.PersonInvolvedType = accidentRecord.PersonInvolved;
            viewModel.PersonInvolvedOtherDescription = accidentRecord.PersonInvolvedOtherDescription;
            viewModel.PersonInvolvedOtherDescriptionId = accidentRecord.PersonInvolvedOtherDescriptionId != null ? accidentRecord.PersonInvolvedOtherDescriptionId : null;
            viewModel.PersonInvolvedOtherDescriptionOther = accidentRecord.PersonInvolvedOtherDescriptionId == 9 ? accidentRecord.PersonInvolvedOtherDescription : null;
            viewModel.Employee = accidentRecord.EmployeeInjured != null ? accidentRecord.EmployeeInjured.FullName : null;
            viewModel.EmployeeId = accidentRecord.EmployeeInjured != null ? accidentRecord.EmployeeInjured.Id : (Guid?)null;
            viewModel.Employees = employees.Select(AutoCompleteViewModel.ForEmployee).AddDefaultOption();
            viewModel.Forename = accidentRecord.NonEmployeeInjuredForename;
            viewModel.Surname = accidentRecord.NonEmployeeInjuredSurname;
            viewModel.AddressLine1 = accidentRecord.NonEmployeeInjuredAddress1;
            viewModel.AddressLine2 = accidentRecord.NonEmployeeInjuredAddress2;
            viewModel.AddressLine3 = accidentRecord.NonEmployeeInjuredAddress3;
            viewModel.County = accidentRecord.NonEmployeeInjuredCountyState;
            viewModel.Country = accidentRecord.NonEmployeeInjuredCountry != null ? accidentRecord.NonEmployeeInjuredCountry.Name : null;
            viewModel.CountryId = accidentRecord.NonEmployeeInjuredCountry != null ? accidentRecord.NonEmployeeInjuredCountry.Id : (int?)null;
            viewModel.Countries = AutoCompleteViewModel.ForCountries(countries);
            viewModel.Postcode = accidentRecord.NonEmployeeInjuredPostcode;
            viewModel.ContactNo = accidentRecord.NonEmployeeInjuredContactNumber;
            viewModel.Occupation = accidentRecord.NonEmployeeInjuredOccupation;
            viewModel.CompanyId = _companyId;
            viewModel.AccidentRecordId = _accidentRecordId;
            viewModel.NextStepsVisible = accidentRecord.NextStepsAvailable;
            viewModel.OthersInvolved = othersInvolved.Select(AutoCompleteViewModel.ForOthersInvolved).AddDefaultOption();
            return viewModel;
        }

        public InjuredPersonViewModel GetViewModel(InjuredPersonViewModel viewModel)
        {
            var employees = _employeeService.GetAll(_companyId);
            var countries = _lookupService.GetCountries();
            var others = _lookupService.GetOthersInvolved();
            viewModel.Employees = employees.Select(AutoCompleteViewModel.ForEmployee).AddDefaultOption();
            viewModel.Countries = AutoCompleteViewModel.ForCountries(countries);
            viewModel.OthersInvolved = others.Select(AutoCompleteViewModel.ForOthersInvolved).AddDefaultOption();
            return viewModel;
        }
    }
}