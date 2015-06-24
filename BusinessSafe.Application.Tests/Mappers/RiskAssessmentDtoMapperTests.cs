using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Mappers
{
    [TestFixture]
    public class RiskAssessmentDtoMapperTests
    {
        [Test]
        [Ignore("If you need the EmployeeChecklists, call GetWithEmployeeChecklists. PTD.")]
        public void Given_personal_risk_assessment_with_employee_checklists_when_mapping_to_DTO_employee_checklists_are_mapped()
        {
            var riskAss = new PersonalRiskAssessment();
            riskAss.EmployeeChecklists = new List<EmployeeChecklist>()
                                             {
                                                 new EmployeeChecklist {Id = Guid.NewGuid()}
                                                 , new EmployeeChecklist() {Id = Guid.NewGuid()}
                                             };
            
            var target = new RiskAssessmentDtoMapper();
            var riskAssDto = (PersonalRiskAssessmentDto) target.MapWithEmployeesAndNonEmployeesAndSiteAndRiskAssessor(riskAss);

            Assert.IsNotNull(riskAssDto.EmployeeChecklists);
            Assert.AreEqual(riskAss.EmployeeChecklists.Count, riskAssDto.EmployeeChecklists.Count());
        }

        [Test]
        [Ignore("If checklist is null, the DTO equivalent should be null. PTD")]
        public void Given_personal_risk_assessment_and_employee_checklists_isnull_when_mapping_to_DTO_employee_checklists_is_empty_list()
        {
            var riskAss = new PersonalRiskAssessment();
            riskAss.EmployeeChecklists = null;

            var target = new RiskAssessmentDtoMapper();
            var riskAssDto = (PersonalRiskAssessmentDto)target.MapWithEmployeesAndNonEmployeesAndSiteAndRiskAssessor(riskAss);

            Assert.IsNotNull(riskAssDto.EmployeeChecklists);
            Assert.AreEqual(0, riskAssDto.EmployeeChecklists.Count());
        }
    }
}
