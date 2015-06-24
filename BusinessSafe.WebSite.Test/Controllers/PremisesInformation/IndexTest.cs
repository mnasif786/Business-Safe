using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.PremisesInformation
{   
    [TestFixture]
    [Category("Unit")]
    public class IndexTest : BasePremisesInformationControllerTests
    {
        private List<RiskAssessmentEmployeeDto> _employees;
        private List<RiskAssessmentNonEmployeeDto> _nonEmployees;
        
        [SetUp]
        public new void SetUp()
        {
            _nonEmployees = new List<RiskAssessmentNonEmployeeDto>() { new RiskAssessmentNonEmployeeDto{ NonEmployee = new NonEmployeeDto{ Id = 1, Name = "11" }}};
            _employees = new List<RiskAssessmentEmployeeDto>() { new RiskAssessmentEmployeeDto{ Employee = new EmployeeDto() { Id = Guid.NewGuid(), FullName = "11" }}};
        }

        [Test]
        public void Given_a_request_Then_should_call_correct_methods()
        {
            //Given
            var target = GetTarget();
            const int expectedCompanyId = 100;
            const int expectedRiskAssessmentId = 3000;

            RiskAssessmentService
                .Setup(x => x.GetRiskAssessment(expectedRiskAssessmentId, expectedCompanyId))
                .Returns(new GeneralRiskAssessmentDto()
                             {
                                 CompanyId = expectedCompanyId,
                                 Id = expectedRiskAssessmentId,
                                 Employees = _employees,
                                 NonEmployees = _nonEmployees,
                                 CreatedOn = DateTime.Now
                             });

           

            //When
            target.Index(expectedRiskAssessmentId, expectedCompanyId);

            //Then
            RiskAssessmentService.VerifyAll();
        }

        [Test]
        public void Given_a_request_Then_should_retur_correct_model()
        {
            //Given
            var target = GetTarget();
            var riskAssessmentDto = new GeneralRiskAssessmentDto()
                                        {
                                            CompanyId = 1, 
                                            Id = 2, 
                                            Employees = _employees, 
                                            NonEmployees = _nonEmployees,
                                            CreatedOn = DateTime.Today,
                                            Reference = "Some Reference",
                                            Title = "My title",
                                            TaskProcessDescription = "Description",
                                            Location = "Some Location"
                                        };
            RiskAssessmentService
                .Setup(x => x.GetRiskAssessment(riskAssessmentDto.Id, riskAssessmentDto.CompanyId))
                .Returns(riskAssessmentDto);
            
            //When
            var result = target.Index(riskAssessmentDto.Id, riskAssessmentDto.CompanyId) as ViewResult;

            //Then
            var premisesInformationViewModel = (PremisesInformationViewModel)result.Model;
            Assert.That(premisesInformationViewModel.CompanyId, Is.EqualTo(riskAssessmentDto.CompanyId));
            Assert.That(premisesInformationViewModel.RiskAssessmentId, Is.EqualTo(riskAssessmentDto.Id));
            Assert.That(premisesInformationViewModel.CreatedOn, Is.EqualTo(riskAssessmentDto.CreatedOn));
            Assert.That(premisesInformationViewModel.Reference, Is.EqualTo(riskAssessmentDto.Reference));
            Assert.That(premisesInformationViewModel.Title, Is.EqualTo(riskAssessmentDto.Title));
            Assert.That(premisesInformationViewModel.TaskProcessDescription, Is.EqualTo(riskAssessmentDto.TaskProcessDescription));
            Assert.That(premisesInformationViewModel.LocationAreaDepartment, Is.EqualTo(riskAssessmentDto.Location)); 
            Assert.That(premisesInformationViewModel.Employees.Count(), Is.EqualTo(_employees.Count));
            Assert.That(premisesInformationViewModel.NonEmployees.Count(), Is.EqualTo(_nonEmployees.Count));

        }
    }
}
