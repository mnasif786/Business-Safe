using System;
using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.HazardousSubstanceInventory;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;
using BusinessSafe.WebSite.ViewModels;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.HazardousSubstanceRiskAssessementTests
{
    [TestFixture]
    public class EditSummaryViewModelFactoryTests
    {
        private Mock<IHazardousSubstanceRiskAssessmentService> _riskAssessmentService;
        private Mock<IEmployeeService> _employeeService;
        private Mock<IHazardousSubstancesService> _substanceService;
        private Mock<ISiteService> _siteService;
        private EditHazardousSubstanceRiskAssessmentSummaryViewModelFactory target;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentService = new Mock<IHazardousSubstanceRiskAssessmentService>();
            _siteService = new Mock<ISiteService>();
            _riskAssessmentService
                .Setup(x => x.GetRiskAssessment(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(new HazardousSubstanceRiskAssessmentDto()
                         {
                             HazardousSubstance = new HazardousSubstanceDto()
                         });

            _employeeService = new Mock<IEmployeeService>();
            _employeeService
                .Setup(x => x.Search(It.IsAny<SearchEmployeesRequest>()))
                .Returns(new List<EmployeeDto>());

            _substanceService = new Mock<IHazardousSubstancesService>();
            _substanceService
                .Setup(x => x.Search(It.IsAny<SearchHazardousSubstancesRequest>()))
                .Returns(new List<HazardousSubstanceDto>());

            target = new EditHazardousSubstanceRiskAssessmentSummaryViewModelFactory(_riskAssessmentService.Object, _employeeService.Object, _substanceService.Object, _siteService.Object);
        }

        [Test]
        public void Given_GetViewModel_Then_Request_From_Service()
        {
            // Given
            const long riskAssessmentId = 100;
            const long companyId = 200;

            var passedSearchEmployeesRequest = new SearchEmployeesRequest();

            _employeeService
                .Setup(x => x.Search(It.IsAny<SearchEmployeesRequest>()))
                .Returns(new List<EmployeeDto>())
                .Callback<SearchEmployeesRequest>(y => passedSearchEmployeesRequest = y);

            var passedSearchHazardousSubstancesRequest = new SearchHazardousSubstancesRequest();

            _substanceService
                .Setup(x => x.Search(It.IsAny<SearchHazardousSubstancesRequest>()))
                .Returns(new List<HazardousSubstanceDto>())
                .Callback<SearchHazardousSubstancesRequest>(y => passedSearchHazardousSubstancesRequest = y);

            // When
            var result = target
                .WithRiskAssessmentId(riskAssessmentId)
                .WithCompanyId(companyId)
                .GetViewModel();

            // Then
            _riskAssessmentService.Verify(x => x.GetRiskAssessment(riskAssessmentId, companyId));
            Assert.That(result, Is.InstanceOf<EditSummaryViewModel>());
            Assert.That(passedSearchEmployeesRequest.CompanyId, Is.EqualTo(companyId));
            Assert.That(passedSearchEmployeesRequest.MaximumResults, Is.EqualTo(100));
            Assert.That(passedSearchHazardousSubstancesRequest.CompanyId, Is.EqualTo(companyId));
        }

        [Test]
        public void Given_GetViewModel_Then_Maps_HazardousSubstances_to_Dropdown()
        {
            // Given
            const long riskAssessmentId = 100;
            const long companyId = 200;

            // When
            var result = target
                .WithRiskAssessmentId(riskAssessmentId)
                .WithCompanyId(companyId)
                .GetViewModel();

            // Then
            Assert.That(result.HazardousSubstances, Is.InstanceOf<IEnumerable<AutoCompleteViewModel>>());
        }

        [Test]
        public void Given_GetViewModel_Then_ViewModel_Is_Populated()
        {
            // Given
            const long riskAssessmentId = 100;
            const long companyId = 200;
            var employeeId = Guid.NewGuid();
            var riskAssessorId = 432L;

            var date = new DateTime(2025, 6, 23);
            _riskAssessmentService
                .Setup(x => x.GetRiskAssessment(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(new HazardousSubstanceRiskAssessmentDto()
                {
                    Id = riskAssessmentId,
                    CompanyId = companyId,
                    Title = "HSRA Title",
                    Reference = "HSRA Reference",
                    HazardousSubstance = new HazardousSubstanceDto()
                    {
                        Id = 1234,
                        Name = "My HazSub"
                    },
                    RiskAssessor = new RiskAssessorDto
                    {
                        Id = riskAssessorId,
                        FormattedName = "HSRA Risk Assessor"
                    },
                    AssessmentDate = date
                });

            // When
            var result = target
                .WithRiskAssessmentId(riskAssessmentId)
                .WithCompanyId(companyId)
                .GetViewModel();

            // Then
            Assert.That(result.RiskAssessmentId, Is.EqualTo(riskAssessmentId));
            Assert.That(result.CompanyId, Is.EqualTo(companyId));
            Assert.That(result.Title, Is.EqualTo("HSRA Title"));
            Assert.That(result.Reference, Is.EqualTo("HSRA Reference"));
            Assert.That(result.RiskAssessor, Is.EqualTo("HSRA Risk Assessor"));
            Assert.That(result.RiskAssessorId, Is.EqualTo(riskAssessorId));
            Assert.That(result.HazardousSubstance, Is.EqualTo("My HazSub"));
            Assert.That(result.HazardousSubstanceId, Is.EqualTo(1234));
            Assert.That(result.DateOfAssessment, Is.EqualTo(date));
        }

        [Test]
        public void Given_GetViewModel_When_No_RiskAssessor_Set_Then_ViewModel_Is_Populated()
        {
            // Given
            const long riskAssessmentId = 100;
            const long companyId = 200;
            var employeeId = Guid.NewGuid();

            _riskAssessmentService
                .Setup(x => x.GetRiskAssessment(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(new HazardousSubstanceRiskAssessmentDto()
                {
                    Id = riskAssessmentId,
                    CompanyId = companyId,
                    Title = "HSRA Title",
                    Reference = "HSRA Reference",
 
                    HazardousSubstance = new HazardousSubstanceDto()
                    {
                        Id = 1234,
                        Name = "My HazSub"
                    }
                });

            // When
            var result = target
                .WithRiskAssessmentId(riskAssessmentId)
                .WithCompanyId(companyId)
                .GetViewModel();

            // Then
            Assert.That(result.RiskAssessmentId, Is.EqualTo(riskAssessmentId));
            Assert.That(result.CompanyId, Is.EqualTo(companyId));
            Assert.That(result.Title, Is.EqualTo("HSRA Title"));
            Assert.That(result.Reference, Is.EqualTo("HSRA Reference"));
            Assert.That(result.HazardousSubstance, Is.EqualTo("My HazSub"));
            Assert.That(result.HazardousSubstanceId, Is.EqualTo(1234));
        }

        [Test]
        public void Given_AttachDropdownData_Then_Requests_From_Service()
        {
            // Given
            const long companyId = 200;

            var passedSearchEmployeesRequest = new SearchEmployeesRequest();

            _employeeService
                .Setup(x => x.Search(It.IsAny<SearchEmployeesRequest>()))
                .Returns(new List<EmployeeDto>()
                         {
                             new EmployeeDto(),
                             new EmployeeDto(),
                             new EmployeeDto()
                         })
                .Callback<SearchEmployeesRequest>(y => passedSearchEmployeesRequest = y);

            var passedSearchHazardousSubstancesRequest = new SearchHazardousSubstancesRequest();

            _substanceService
                .Setup(x => x.Search(It.IsAny<SearchHazardousSubstancesRequest>()))
                .Returns(new List<HazardousSubstanceDto>()
                         {
                             new HazardousSubstanceDto(),
                             new HazardousSubstanceDto(),
                             new HazardousSubstanceDto(),
                             new HazardousSubstanceDto()
                         })
                .Callback<SearchHazardousSubstancesRequest>(y => passedSearchHazardousSubstancesRequest = y);

            var model = new EditSummaryViewModel();

            // When
            var result = target
                .WithCompanyId(companyId)
                .AttachDropDownData(model);

            // Then
            Assert.That(result, Is.InstanceOf<EditSummaryViewModel>());
            Assert.That(passedSearchEmployeesRequest.CompanyId, Is.EqualTo(companyId));
            Assert.That(passedSearchEmployeesRequest.MaximumResults, Is.EqualTo(100));
            Assert.That(passedSearchHazardousSubstancesRequest.CompanyId, Is.EqualTo(companyId));
            Assert.That(model.HazardousSubstances.Count(), Is.EqualTo(5)); // plus 1 for '--select option--'
            Assert.That(model.RiskAssessmentAssessors.Count(), Is.EqualTo(4)); // plus 1 for '--select option--'
        }

        [Test]
        public void Given_GetViewModel_When_RiskAssessment_Doesnt_Have_AssessmentDate_Yet_Then_ViewModel_AssessmentDate_is_Today()
        {
            // Given
            const long riskAssessmentId = 100;
            const long companyId = 200;
            var employeeId = Guid.NewGuid();

            var ra = Domain.Entities.HazardousSubstanceRiskAssessment.Create("GRA Title", "GRA Reference", companyId, new UserForAuditing(),  new HazardousSubstance());
            //ra.RiskAssessorEmployee = new Employee()
            //{
            //    Id = employeeId,
            //    Forename = "Risk",
            //    Surname = "Assessor"
            //};

            _riskAssessmentService
                .Setup(x => x.GetRiskAssessment(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(new HazardousSubstanceRiskAssessmentDto()
                         {
                             AssessmentDate = null,
                             Title = "Title",
                             Reference = "Reference",
                             HazardousSubstance = new HazardousSubstanceDto()
                                                  {
                                                      Id = 1,
                                                      Name = "hazsubname"
                                                  }
                         });

            // When
            var result = target
                .WithRiskAssessmentId(riskAssessmentId)
                .WithCompanyId(companyId)
                .GetViewModel();

            // Then
            Assert.That(result.DateOfAssessment, Is.EqualTo(DateTime.Today));
         }
    }
}
