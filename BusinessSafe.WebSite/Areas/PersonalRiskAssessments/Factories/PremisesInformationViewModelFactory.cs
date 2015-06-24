using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories
{
    public class PremisesInformationViewModelFactory : IPremisesInformationViewModelFactory
    {
        private long _riskAssessmentId;
        private long _companyId;
        private readonly IPersonalRiskAssessmentService _riskAssessmentService;
        private Guid _currentUserId;

        public PremisesInformationViewModelFactory(IPersonalRiskAssessmentService riskAssessmentService)
        {
            _riskAssessmentService = riskAssessmentService;
        }

        public IPremisesInformationViewModelFactory WithRiskAssessmentId(long riskAssessmentId)
        {
            _riskAssessmentId = riskAssessmentId;
            return this;
        }

        public IPremisesInformationViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IPremisesInformationViewModelFactory WithCurrentUserId(Guid currentUserId)
        {
            _currentUserId = currentUserId;
            return this;
        }

        public PremisesInformationViewModel GetViewModel()
        {
            var riskAssessment = _riskAssessmentService.GetRiskAssessment(_riskAssessmentId, _companyId, _currentUserId);
            
            var viewModel = new PremisesInformationViewModel()
                                {
                                    CompanyId = _companyId,
                                    RiskAssessmentId = _riskAssessmentId,
                                    NonEmployees = riskAssessment.NonEmployees != null ? GetNonEmployees(riskAssessment.NonEmployees) : new List<Tuple<long, string>>(),
                                    Employees = riskAssessment.Employees != null ? GetEmployees(riskAssessment.Employees) : new List<Tuple<Guid, string>>(),
                                    LocationAreaDepartment = riskAssessment.Location,
                                    TaskProcessDescription = riskAssessment.TaskProcessDescription
                                };
            
            return viewModel;
        }

        public PremisesInformationViewModel GetViewModel(PremisesInformationViewModel viewModel)
        {
            var riskAssessment = _riskAssessmentService.GetRiskAssessment(viewModel.RiskAssessmentId, viewModel.CompanyId, _currentUserId);
            viewModel.NonEmployees = riskAssessment.NonEmployees != null ? GetNonEmployees(riskAssessment.NonEmployees) : new List<Tuple<long, string>>();
            viewModel.Employees = riskAssessment.Employees != null ? GetEmployees(riskAssessment.Employees) : new List<Tuple<Guid, string>>();
           return viewModel;
        }
        
        private static IEnumerable<Tuple<long, string>> GetNonEmployees(IEnumerable<RiskAssessmentNonEmployeeDto> nonEmployees)
        {
            return nonEmployees.Select(riskAssessmentNonEmployee => riskAssessmentNonEmployee.NonEmployee).Select(nonEmployee => new Tuple<long, string>(nonEmployee.Id, nonEmployee.FormattedName)).ToList();
        }

        private static IEnumerable<Tuple<Guid, string>> GetEmployees(IEnumerable<RiskAssessmentEmployeeDto> employees)
        {
            return employees.Select(riskAssessmentEmployee => riskAssessmentEmployee.Employee).Select(employee => new Tuple<Guid, string>(employee.Id, employee.FullName)).ToList();
        }
    }
}