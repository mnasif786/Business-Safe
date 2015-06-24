using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.FireRiskAssessmentTests.Factories
{
    [TestFixture]
    public class EditSummaryViewModelFactoryTests
    {
        private Mock<IEmployeeService> _employeeService;
        private Mock<IFireRiskAssessmentService> _riskAssessmentService;
        private Mock<ISiteService> _siteService;
        private EditFireRiskAssessmentSummaryViewModelFactory target;

        [SetUp]
        public void Setup()
        {
            _employeeService = new Mock<IEmployeeService>();
            _riskAssessmentService = new Mock<IFireRiskAssessmentService>();
            _siteService = new Mock<ISiteService>();

            target = new EditFireRiskAssessmentSummaryViewModelFactory(_employeeService.Object, _riskAssessmentService.Object, _siteService.Object);
        }

        [Test]
        public void When_GetViewModel_Then_should_call_correct_methods()
        {
            // Given
            const long riskAssessmentId = 100;
            const long companyId = 200;

            _employeeService
                .Setup(x => x.Search(It.Is<SearchEmployeesRequest>(y => y.CompanyId == companyId)))
                .Returns(new List<EmployeeDto>(){ new EmployeeDto()});

            var riskAssessmentDto = new FireRiskAssessmentDto();
            _riskAssessmentService
                .Setup(x => x.GetRiskAssessment(riskAssessmentId, companyId))
                .Returns(riskAssessmentDto);

            // When
            target
                .WithRiskAssessmentId(riskAssessmentId)
                .WithCompanyId(companyId)
                .GetViewModel();

            // Then
            _riskAssessmentService.VerifyAll();
            _employeeService.VerifyAll();
        }

        [Test]
        public void When_GetViewModel_Then_should_return_correct_result()
        {
            // Given
            const long riskAssessmentId = 100;
            const long companyId = 200;

            var riskAssessmentDto = new FireRiskAssessmentDto()
                                        {
                                            Reference = "REF",
                                            Title = "My Title",
                                            PersonAppointed = "Legend"
                                        };
            _riskAssessmentService
                .Setup(x => x.GetRiskAssessment(riskAssessmentId, companyId))
                .Returns(riskAssessmentDto);

            _employeeService
                .Setup(x => x.Search(It.Is<SearchEmployeesRequest>(y => y.CompanyId == companyId)))
                .Returns(new List<EmployeeDto>() { new EmployeeDto() });


            // When
            var result = target
                .WithRiskAssessmentId(riskAssessmentId)
                .WithCompanyId(companyId)
                .GetViewModel();

            // Then
            Assert.That(result.CompanyId,Is.EqualTo(companyId));
            Assert.That(result.RiskAssessmentId, Is.EqualTo(riskAssessmentId));
            Assert.That(result.PersonAppointed, Is.EqualTo(riskAssessmentDto.PersonAppointed));
            Assert.That(result.Reference, Is.EqualTo(riskAssessmentDto.Reference));
            Assert.That(result.Title, Is.EqualTo(riskAssessmentDto.Title));
            Assert.That(result.RiskAssessmentAssessors.Count(), Is.EqualTo(2));
        }
    }
}
