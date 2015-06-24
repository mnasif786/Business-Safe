using System;
using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.GeneralRiskAssessment.Factories
{
    [TestFixture]
    public class EditSummaryViewModelFactoryTests
    {
        private Mock<IGeneralRiskAssessmentService> _riskAssessmentService;
        private Mock<ISiteService> _siteService;
        private Mock<IRiskAssessorService> _riskAssessorService;
        private EditGeneralRiskAssessmentSummaryViewModelFactory target;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentService = new Mock<IGeneralRiskAssessmentService>();
            _riskAssessmentService
                .Setup(x => x.GetRiskAssessment(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(new GeneralRiskAssessmentDto());

            _riskAssessorService = new Mock<IRiskAssessorService>();
            _riskAssessorService
                .Setup(x => x.Search(It.IsAny<SearchRiskAssessorRequest>()))
                .Returns(new List<RiskAssessorDto>());

            _siteService = new Mock<ISiteService>();
            _siteService
                .Setup(x => x.Search(It.IsAny<SearchSitesRequest>()))
                .Returns(new List<SiteDto>());

            target = new EditGeneralRiskAssessmentSummaryViewModelFactory(_riskAssessmentService.Object, _riskAssessorService.Object, _siteService.Object);
        }

        [Test]
        public void Given_GetViewModel_Then_Request_From_Risk_Assessment_Service()
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
            _riskAssessmentService.Verify(x => x.GetRiskAssessment(riskAssessmentId, companyId));
        }

        [Test]
        public void Given_GetViewModel_Then_returns_EditSummaryViewModel()
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
            Assert.That(result, Is.InstanceOf<EditSummaryViewModel>());
        }

        [Test]
        public void When_GetViewModel_Then_view_models_risk_assessors_populated()
        {
            // Given
            const long riskAssessmentId = 100;
            const long companyId = 200;

            var riskAssessors = new List<RiskAssessorDto>()
                                {
                                    new RiskAssessorDto() { Employee = new EmployeeDto(){ FullName = "employee a "}},
                                    new RiskAssessorDto() { Employee = new EmployeeDto(){ FullName = "employee b "}},
                                    new RiskAssessorDto() { Employee = new EmployeeDto(){ FullName = "employee c "}},
                                    new RiskAssessorDto() { Employee = new EmployeeDto(){ FullName = "employee d "}}
                                };

            _riskAssessorService
                .Setup(x => x.Search(It.IsAny<SearchRiskAssessorRequest>()))
                .Returns(riskAssessors);

            // When
            var result = target
                .WithRiskAssessmentId(riskAssessmentId)
                .WithCompanyId(companyId)
                .GetViewModel();

            // Then
            Assert.That(result.RiskAssessmentAssessors.Count(), Is.EqualTo(riskAssessors.Count + 1)); // "-- select option --" option
        }

        [Test]
        public void Given_Site_already_set_When_GetViewModel_Then_only_retrieve_assessors_associated_with_that_site()
        {
            // Given
            const long riskAssessmentId = 100;
            const long companyId = 200;

            var riskAssessmentSite = new Site() { Id = 1234L };

            var gra = Domain.Entities.GeneralRiskAssessment.Create("GRA Title", "GRA Reference", companyId, new UserForAuditing());
            //gra.RiskAssessor = new RiskAssessor();
            //{
            //    Id = 345L,
            //    F
            //};
            gra.RiskAssessmentSite = riskAssessmentSite;

            _riskAssessmentService
                .Setup(x => x.GetRiskAssessment(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(GeneralRiskAssessmentDto.CreateFrom(gra));

            var riskAssessors = new List<RiskAssessorDto>()
                                {
                                    new RiskAssessorDto() { Employee = new EmployeeDto(){ FullName = "employee a "}},
                                    new RiskAssessorDto() { Employee = new EmployeeDto(){ FullName = "employee b "}},
                                    new RiskAssessorDto() { Employee = new EmployeeDto(){ FullName = "employee c "}}
                                };

            _riskAssessorService
                .Setup(x => x.Search(It.Is<SearchRiskAssessorRequest>(y => y.SiteId == riskAssessmentSite.Id)))
                .Returns(riskAssessors);

            // When
            var result = target
                .WithRiskAssessmentId(riskAssessmentId)
                .WithCompanyId(companyId)
                .GetViewModel();

            // Then
            Assert.That(result.RiskAssessmentAssessors.Count(), Is.EqualTo(riskAssessors.Count + 1)); // "-- select option --" option
        }

        [Test]
        public void Given_GetViewModel_Then_ViewModel_Is_Populated()
        {
            // Given
            const long riskAssessmentId = 100;
            const long companyId = 200;
            const long riskAssessorId = 2143L;
            var employeeId = Guid.NewGuid();
            var gra = Domain.Entities.GeneralRiskAssessment.Create("GRA Title", "GRA Reference", companyId, new UserForAuditing() { Employee = new EmployeeForAuditing() });
            var date = new DateTime(2025, 6, 23);
            gra.AssessmentDate = date;
            var dto = GeneralRiskAssessmentDto.CreateFrom(gra);

            dto.RiskAssessor = new RiskAssessorDto
                                   {
                                       Id = riskAssessorId,
                                       FormattedName = "GRA Risk Assessor"
                                   };

            _riskAssessmentService
                .Setup(x => x.GetRiskAssessment(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(dto);

            // When
            var result = target
                .WithRiskAssessmentId(riskAssessmentId)
                .WithCompanyId(companyId)
                .GetViewModel();

            // Then
            Assert.That(result.RiskAssessmentId, Is.EqualTo(riskAssessmentId));
            Assert.That(result.CompanyId, Is.EqualTo(companyId));
            Assert.That(result.Title, Is.EqualTo("GRA Title"));
            Assert.That(result.Reference, Is.EqualTo("GRA Reference"));
            Assert.That(result.RiskAssessor, Is.EqualTo("GRA Risk Assessor"));
            Assert.That(result.RiskAssessorId, Is.EqualTo(riskAssessorId));
            Assert.That(result.DateOfAssessment, Is.EqualTo(date));
        }

        [Test]
        public void Given_GetViewModel_When_RiskAssessment_Doesnt_Have_AssessmentDate_Yet_Then_ViewModel_AssessmentDate_is_Today()
        {
            // Given
            const long riskAssessmentId = 100;
            const long companyId = 200;

            var gra = Domain.Entities.GeneralRiskAssessment.Create("GRA Title", "GRA Reference", companyId, new UserForAuditing());

            _riskAssessmentService
                .Setup(x => x.GetRiskAssessment(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(GeneralRiskAssessmentDto.CreateFrom(gra));

            // When
            var result = target
                .WithRiskAssessmentId(riskAssessmentId)
                .WithCompanyId(companyId)
                .GetViewModel();

            // Then
            Assert.That(result.DateOfAssessment, Is.EqualTo(DateTime.Today));
        }

        [Test]
        public void Given_When_GetViewModel_Then_Sites_are_set()
        {
            // Given
            const long riskAssessmentId = 100;
            const long companyId = 200;

            var sites = new List<SiteDto>
                        {
                            new SiteDto
                            {
                                Id=888L,
                                Name = "Coventry"
                            },
                            new SiteDto
                            {
                                Id=889L,
                                Name = "Bradford"
                            },
                            new SiteDto
                            {
                                Id=890L,
                                Name = "Bracknell"
                            },
                            new SiteDto
                            {
                                Id=890L,
                                Name = "Birmingham"
                            }
                        };
            _siteService
                .Setup(x => x.Search(It.IsAny<SearchSitesRequest>()))
                .Returns(sites);

            // When
            var result = target
                .WithRiskAssessmentId(riskAssessmentId)
                .WithCompanyId(companyId)
                .GetViewModel();

            // Then
            Assert.That(result.Sites.Count(), Is.EqualTo(sites.Count + 1)); // "-- select option --" option
        }

        [Test]
        public void Given_allowed_sites_When_GetViewModel_Then_site_search_returns()
        {
            // Given
            var allowedSites = new[] { 888L, 890L };
            const long riskAssessmentId = 100;
            const long companyId = 200;

            // When
            var result = target
                .WithRiskAssessmentId(riskAssessmentId)
                .WithAllowableSiteIds(allowedSites)
                .WithCompanyId(companyId)
                .GetViewModel();

            // Then
            _siteService.Verify(x => x.Search(It.Is<SearchSitesRequest>(y => y.AllowedSiteIds == allowedSites)));
        }
    }
}
