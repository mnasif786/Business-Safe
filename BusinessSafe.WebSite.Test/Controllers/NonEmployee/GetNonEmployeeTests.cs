using System.Collections.Generic;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Controllers;
using BusinessSafe.WebSite.Controllers;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.NonEmployee
{
    [TestFixture]
    [Category("Unit")]
    public class GetNonEmployeeTests
    {
        const string term = "anything";
        private Mock<IRiskAssessmentLookupService> _riskAssessmentLookupService;

        [SetUp]
        public void SetUp()
        {
            _riskAssessmentLookupService = new Mock<IRiskAssessmentLookupService>();
        }

        [Test]
        public void Given_that_json_get_request_to_get_non_employees_Then_should_return_correct_action_result()
        {
            //Given
            var target = CreateNonEmployeeController();

            _riskAssessmentLookupService
                .Setup(x => x.SearchForNonEmployeesNotAttachedToRiskAssessment(It.Is<NonEmployeesNotAttachedToRiskAssessmentSearchRequest>(y => y.SearchTerm ==  term && y.CompanyId == 1 && y.RiskAssessmentId == 1 && y.PageLimit == 20)))
                .Returns(new List<NonEmployeeDto>());

            //When
            var result = target.GetNonEmployees(term, 1, 1,20);

            //Then
            Assert.That(result, Is.TypeOf<JsonResult>());
        }

        [Test]
        public void Given_that_json_get_request_to_get_non_employees_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateNonEmployeeController();

            _riskAssessmentLookupService
                .Setup(x => x.SearchForNonEmployeesNotAttachedToRiskAssessment(It.Is<NonEmployeesNotAttachedToRiskAssessmentSearchRequest>(y => y.SearchTerm == term && y.CompanyId == 1 && y.RiskAssessmentId == 1 && y.PageLimit == 20)))
                .Returns(new List<NonEmployeeDto>());

            //When
            target.GetNonEmployees(term, 1, 1,20);

            //Then
            _riskAssessmentLookupService.VerifyAll();
        }

        [Test]
        public void Given_that_json_get_request_to_get_non_employees_Then_should_return_correct_results()
        {
            //Given
            var target = CreateNonEmployeeController();
            
            
            _riskAssessmentLookupService
                .Setup(x => x.SearchForNonEmployeesNotAttachedToRiskAssessment(It.Is<NonEmployeesNotAttachedToRiskAssessmentSearchRequest>(y => y.SearchTerm == term && y.CompanyId == 1 && y.RiskAssessmentId == 1 && y.PageLimit == 20)))
                .Returns(new List<NonEmployeeDto>()
                             {
                                 new NonEmployeeDto(),
                                 new NonEmployeeDto()
                             });

            //When
            var result = target.GetNonEmployees(term, 1, 1, 20);

            //Then
            dynamic data = result.Data; 
            Assert.AreEqual(2, data.Count);    
        }

        private RiskAssessmentNonEmployeeController CreateNonEmployeeController()
        {
            var target = new RiskAssessmentNonEmployeeController(_riskAssessmentLookupService.Object, null);
            return target;
        }
    }
}
