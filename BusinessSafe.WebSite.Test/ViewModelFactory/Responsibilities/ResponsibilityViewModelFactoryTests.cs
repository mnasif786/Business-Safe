using System;
using System.Collections.Generic;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Responsibilities.Factories;
using BusinessSafe.WebSite.AuthenticationService;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.Responsibilities
{
    [TestFixture]
    [Category("Unit")]
    public class ResponsibilityViewModelFactoryTests
    {
        private Mock<IResponsibilitiesService> _responsibilitiesService;
        private Mock<IEmployeeService> _employeeService;
        private Mock<ISiteService> _siteService;

        private long _companyId;
        private long _responsibilityId;

        [SetUp]
        public void SetUp()
        {
            _responsibilitiesService = new Mock<IResponsibilitiesService>();
            _employeeService = new Mock<IEmployeeService>();
            _siteService = new Mock<ISiteService>();
            _companyId = 250;
            _responsibilityId = 1;

            _employeeService.Setup(x => x.GetEmployeeNames(It.IsAny<long>()))
                .Returns(() => new List<EmployeeName>());
        }

        [Test]
        public void Given_search_for_current_user_When_GetViewModel_is_called_Then_returns_model()
        {
            //Given
            var target = CreateTarget();

            var responsibility = new ResponsibilityDto
                                     {
                                         Id = 1L,
                                         ResponsibilityTasks = new List<ResponsibilityTaskDto>
                                                                   {
                                                                       new ResponsibilityTaskDto
                                                                           {
                                                                               Id = 1L,
                                                                               Description = "my description",
                                                                               TaskAssignedTo = new EmployeeDto{FullName = "Test"},
                                                                               CreatedDate = DateTime.Now.ToShortDateString(),
                                                                               TaskCompletionDueDate = DateTime.Now.ToShortDateString(),
                                                                               TaskStatusString = string.Empty,
                                                                               Site = new SiteDto{Id = 1L}
                                                                           }
                                                                   }
                                     };

            var reasons = new List<ResponsibilityReasonDto>
                                 {
                                     new ResponsibilityReasonDto {Id = default(long)}
                                 };

            var categories = new List<ResponsibilityCategoryDto>
                                 {
                                     new ResponsibilityCategoryDto {Id = default(long)}
                                 };

            var employess = new List<EmployeeDto>
                                {
                                    new EmployeeDto{Id = new Guid()}
                                };

            var sites = new List<SiteDto>
                            {
                                new SiteDto{Id = 1}
                            };

            _responsibilitiesService
                .Setup(x => x.GetResponsibility(It.IsAny<long>(), _companyId))
                .Returns(() => responsibility);

            _responsibilitiesService
                .Setup(x => x.GetResponsibilityReasons())
                .Returns(() => reasons);
            
            _responsibilitiesService
                .Setup(x => x.GetResponsibilityCategories())
                .Returns(() => categories);

            _employeeService
                .Setup(x => x.Search(It.IsAny<SearchEmployeesRequest>()))
                .Returns(() => employess);

            _employeeService
               .Setup(x => x.GetEmployeeNames(It.IsAny<long>()))
               .Returns(() => employess.Select(x => new EmployeeName(){Id = x.Id}).ToList());

            _siteService
                .Setup(x => x.Search(It.IsAny<SearchSitesRequest>()))
                .Returns(() => sites);

            //When
            var result = target.WithCompanyId(_companyId)
                .WithResponsibilityId(_responsibilityId)
                .GetViewModel();

            //Then
            Assert.That(result.ResponsibilityId, Is.EqualTo(responsibility.Id));
            Assert.That(result.Reasons.Count(),Is.EqualTo(reasons.Count+1)); //the extra item is the default option "--select option--"
            Assert.That(result.Categories.Count(), Is.EqualTo(categories.Count + 1)); //the extra item is the default option "--select option--"
            Assert.That(result.Employees.Count(), Is.EqualTo(employess.Count + 1)); //the extra item is the default option "--select option--"
            Assert.That(result.Sites.Count(), Is.EqualTo(sites.Count + 1)); //the extra item is the default option "--select option--"
            
        }


        [Test]
        public void Given_search_for_current_user_with_allowed_site_ids_When_GetViewModel_is_called_Then_returns_model()
        {
            //Given
            var target = CreateTarget();

            var responsibility = new ResponsibilityDto
            {
                Id = 1L,
                ResponsibilityTasks = new List<ResponsibilityTaskDto>
                                                                   {
                                                                       new ResponsibilityTaskDto
                                                                           {
                                                                               Id = 1L,
                                                                               Description = "my description",
                                                                               TaskAssignedTo = new EmployeeDto{FullName = "Test"},
                                                                               CreatedDate = DateTime.Now.ToShortDateString(),
                                                                               TaskCompletionDueDate = DateTime.Now.ToShortDateString(),
                                                                               TaskStatusString = string.Empty,
                                                                               Site = new SiteDto{Id = 1L}
                                                                           }
                                                                   }
            };

            var reasons = new List<ResponsibilityReasonDto>
                                 {
                                     new ResponsibilityReasonDto {Id = default(long)}
                                 };

            var categories = new List<ResponsibilityCategoryDto>
                                 {
                                     new ResponsibilityCategoryDto {Id = default(long)}
                                 };

            var employess = new List<EmployeeDto>
                                {
                                    new EmployeeDto{Id = new Guid()}
                                };
            var site1 = new SiteDto() { Id = 1L };
            var site2 = new SiteDto() { Id = 2L };
            var site3 = new SiteDto() { Id = 3L };

            var allowedSites = new List<long>() { 1L, 2L };

            var sites = new List<SiteDto>
                            {
                                site1, site2, site3
                            };

            _responsibilitiesService
                .Setup(x => x.GetResponsibility(It.IsAny<long>(), _companyId))
                .Returns(() => responsibility);

            _responsibilitiesService
                .Setup(x => x.GetResponsibilityReasons())
                .Returns(() => reasons);

            _responsibilitiesService
                .Setup(x => x.GetResponsibilityCategories())
                .Returns(() => categories);

            _employeeService
                .Setup(x => x.Search(It.IsAny<SearchEmployeesRequest>()))
                .Returns(() => employess);

            _employeeService
               .Setup(x => x.GetEmployeeNames(It.IsAny<long>()))
               .Returns(() => employess.Select(x => new EmployeeName() { Id = x.Id }).ToList());

            var searchSitesRequest = new SearchSitesRequest();
            _siteService
                .Setup(x => x.Search(It.IsAny<SearchSitesRequest>())).Returns(sites).Callback<SearchSitesRequest>(y => searchSitesRequest = y);

            //When
            var result = target.WithCompanyId(_companyId)
                .WithResponsibilityId(_responsibilityId)
                .WithAllowedSiteIds(allowedSites)
                .GetViewModel();

            //Then
            Assert.That(searchSitesRequest.AllowedSiteIds.Count, Is.EqualTo(allowedSites.Count));

        }

        private static CustomPrincipal CreateCustomPrincipal(UserDto userDto)
        {
            var customPrincipal = new CustomPrincipal(userDto, new CompanyDto());
            return customPrincipal;
        }

        private IResponsibilityViewModelFactory CreateTarget()
        {
            return new ResponsibilityViewModelFactory(_responsibilitiesService.Object, _siteService.Object, _employeeService.Object);
        }
    }
}
