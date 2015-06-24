using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.AccidentReports.Factories
{
    public class AccidentDetailsViewModelFactory : IAccidentDetailsViewModelFactory
    {
        private readonly IEmployeeService _employeeService;
        private readonly ISiteService _siteService;
        private readonly IAccidentTypeService _accidentTypeService;
        private long _companyId;
        private IList<long> _sites;
        private readonly ICauseOfAccidentService _causeOfAccidentService;
        private long _accidentRecordId;
        private IAccidentRecordService _accidentRecordService;

        public AccidentDetailsViewModelFactory(IEmployeeService employeeService, ISiteService siteService,
                                               IAccidentTypeService accidentTypeService,
                                               ICauseOfAccidentService causeOfAccidentService,
                                               IAccidentRecordService accidentRecordService)
        {
            _employeeService = employeeService;
            _siteService = siteService;
            _accidentTypeService = accidentTypeService;
            _causeOfAccidentService = causeOfAccidentService;
            _accidentRecordService = accidentRecordService;
        }

        public IAccidentDetailsViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IAccidentDetailsViewModelFactory WithAccidentRecordId(long accidentRecordId)
        {
            _accidentRecordId = accidentRecordId;
            return this;
        }

        public IAccidentDetailsViewModelFactory WithSites(IList<long> sites)
        {
            _sites = sites;
            return this;
        }

        public AccidentDetailsViewModel GetViewModel()
        {
            var accidentRecord = _accidentRecordService.GetByIdAndCompanyIdWithSite(_accidentRecordId,_companyId);

            var employees = _employeeService.GetAll(_companyId);
            var sites = _siteService.Search(new SearchSitesRequest
                                                {
                                                    CompanyId = _companyId,
                                                    AllowedSiteIds = _sites
                                                });

            var accidentTypes = _accidentTypeService.GetAllForCompany(_companyId);                      
            var accidentCauses = _causeOfAccidentService.GetAll();

            var viewModel = new AccidentDetailsViewModel();
            viewModel.CompanyId = _companyId;
            if (accidentRecord != null)
            {
                viewModel.AccidentRecordId = accidentRecord.Id;
                viewModel.DateOfAccident = accidentRecord.DateAndTimeOfAccident.HasValue ? accidentRecord.DateAndTimeOfAccident.Value.ToShortDateString() : string.Empty;
                viewModel.TimeOfAccident = accidentRecord.DateAndTimeOfAccident.HasValue ? accidentRecord.DateAndTimeOfAccident.Value.ToShortTimeString() : string.Empty;

                if (accidentRecord.SiteWhereHappened ==null && !string.IsNullOrEmpty(accidentRecord.OffSiteSpecifics))
                {
                    viewModel.SiteId = AccidentDetailsViewModel.OFF_SITE;
                }
                else if(accidentRecord.SiteWhereHappened!=null)
                {
                    viewModel.SiteId = accidentRecord.SiteWhereHappened.Id;
                    viewModel.Site = accidentRecord.SiteWhereHappened.Name;
                }


                viewModel.OffSiteName = accidentRecord.OffSiteSpecifics;
                viewModel.Location = accidentRecord.Location;

                viewModel.AccidentTypeId = accidentRecord.AccidentType != null ? accidentRecord.AccidentType.Id : 0;
                viewModel.AccidentType = accidentRecord.AccidentType != null ? accidentRecord.AccidentType.Description: string.Empty;
                viewModel.OtherAccidentType = accidentRecord.AccidentTypeOther;


                viewModel.AccidentCauseId = accidentRecord.CauseOfAccident != null ? accidentRecord.CauseOfAccident.Id : 0;
                viewModel.AccidentCause = accidentRecord.CauseOfAccident != null ? accidentRecord.CauseOfAccident.Description : string.Empty;
                viewModel.OtherAccidentCause = accidentRecord.CauseOfAccidentOther;


                if (accidentRecord.EmployeeFirstAider == null && !string.IsNullOrEmpty(accidentRecord.NonEmployeeFirstAiderSpecifics))
                {
                    viewModel.FirstAiderEmployeeId = Guid.Empty;
                    viewModel.FirstAiderEmployee = "Other";
                    viewModel.ShowNonEmployeeFirstAidInputs = true;
                }
                else if(accidentRecord.EmployeeFirstAider!=null)
                {
                    viewModel.FirstAiderEmployeeId = accidentRecord.EmployeeFirstAider.Id;
                    viewModel.FirstAiderEmployee = accidentRecord.EmployeeFirstAider.FullName;
                }

                viewModel.NonEmployeeFirstAiderName = accidentRecord.NonEmployeeFirstAiderSpecifics;
                viewModel.FirstAidAdministered = accidentRecord.FirstAidAdministered.HasValue && accidentRecord.FirstAidAdministered.Value;
                viewModel.DetailsOfFirstAid = accidentRecord.DetailsOfFirstAidTreatment;

            }
            viewModel.Employees =
                employees.Select(AutoCompleteViewModel.ForEmployee)
                .AddDefaultOption(String.Empty)
                .WithOtherOption(new AutoCompleteViewModel("Non-employee", Guid.Empty.ToString()));

            viewModel.Sites =
                sites.Select(AutoCompleteViewModel.ForSite)
                .AddDefaultOption(String.Empty)
                .WithOtherOption(new AutoCompleteViewModel("Off-site", AccidentDetailsViewModel.OFF_SITE.ToString()));

            //viewModel.AccidentTypes =
            //    accidentTypes.Select(AutoCompleteViewModel.ForAccidentTypes)
            //    .AddDefaultOption(String.Empty);

            viewModel.AccidentTypes = AutoCompleteViewModel.ForAccidentTypes(accidentTypes);

            viewModel.AccidentCauses =
                accidentCauses.Select(AutoCompleteViewModel.ForAccidentCauses)
                .AddDefaultOption(String.Empty);
               
            
            return viewModel;
        }
    }
}