using System.Collections.Generic;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Controllers;
using BusinessSafe.WebSite.Controllers;
using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.HazardousSubstanceRiskAssessments.HazardousSubstanceRiskAssessmentEmployee
{
    [TestFixture]
    [Category("Unit")]
    public class GetEmployeeTests
    {
        const string term = "anything";
        private Mock<IRiskAssessmentLookupService> _riskAssessmentLookupService;

        [SetUp]
        public void SetUp()
        {
            _riskAssessmentLookupService = new Mock<IRiskAssessmentLookupService>();
        }

        [Test]
        public void Given_that_json_get_request_to_get_employees_Then_should_return_correct_action_result()
        {
            //Given
            var target = CreateNonEmployeeController();

            _riskAssessmentLookupService
                .Setup(x => x.SearchForEmployeesNotAttachedToRiskAssessment(It.Is<EmployeesNotAttachedToRiskAssessmentSearchRequest>(y => y.SearchTerm == term && y.CompanyId == 1 && y.RiskAssessmentId == 1 && y.PageLimit ==10)))
                .Returns(new List<EmployeeDto>());

            //When
            var result = target.GetEmployees(term, 1, 1, 10);

            //Then
            Assert.That(result, Is.TypeOf<JsonResult>());
        }

        [Test]
        public void Given_that_json_get_request_to_get_employees_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateNonEmployeeController();

            _riskAssessmentLookupService
                .Setup(x => x.SearchForEmployeesNotAttachedToRiskAssessment(It.Is<EmployeesNotAttachedToRiskAssessmentSearchRequest>(y => y.SearchTerm == term && y.CompanyId == 1 && y.RiskAssessmentId == 1 && y.PageLimit == 10)))
                .Returns(new List<EmployeeDto>());

            //When
            target.GetEmployees(term, 1, 1,10);

            //Then
            _riskAssessmentLookupService.VerifyAll();
        }

        [Test]
        public void Given_that_json_get_request_to_get_employees_Then_should_return_correct_results()
        {
            //Given
            var target = CreateNonEmployeeController();
            
            
            _riskAssessmentLookupService
                .Setup(x => x.SearchForEmployeesNotAttachedToRiskAssessment(It.Is<EmployeesNotAttachedToRiskAssessmentSearchRequest>(y => y.SearchTerm == term && y.CompanyId == 1 && y.RiskAssessmentId == 1 && y.PageLimit == 10)))
                .Returns(new List<EmployeeDto>()
                             {
                                 new EmployeeDto(),
                                 new EmployeeDto()
                             });

            //When
            var result = target.GetEmployees(term, 1, 1,10);

            //Then
            dynamic data = result.Data; 
            Assert.AreEqual(2, data.Count);    
        }

        private RiskAssessmentEmployeeController CreateNonEmployeeController()
        {
            var target = new RiskAssessmentEmployeeController(_riskAssessmentLookupService.Object, null);
            return target;
        }
    }
}
