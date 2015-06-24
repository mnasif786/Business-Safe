using System;
using System.Collections.Generic;
using AutoMapper;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Employees.ViewModels;

namespace BusinessSafe.WebSite.Controllers.AutoMappers
{
    public class EmployeeEmergencyContactRequestMapper
    {
        private readonly AutoMapperConfiguration _autoMapperConfiguration = AutoMapperConfiguration.Instance;

        public IList<EmployeeEmergencyContactRequest> Map(IList<EmergencyContactViewModel> employeeEmergencyContactRequest, Guid employeeId, Guid userId)
        {
            var result = Mapper.Map<IList<EmergencyContactViewModel>, IList<EmployeeEmergencyContactRequest>>(employeeEmergencyContactRequest);
            
            foreach (var emergencyContactRequest in result)
            {
                emergencyContactRequest.UserAddingOrUpdatingId = userId;
                emergencyContactRequest.EmployeeId = String.IsNullOrWhiteSpace(emergencyContactRequest.EmployeeId) ? employeeId.ToString() : emergencyContactRequest.EmployeeId;
            }
            return result;
        }
    }
}