using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Responsibilities.Factories;
using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels.Wizard;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.Responsibilities.Wizard
{
    [TestFixture]
    public class GenerateResponsibilitiesViewModelFactoryTests
    {
        private GenerateResponsibilitiesViewModelFactory _target;
        private List<SiteDto> _returnedSites;
        private List<StatutoryResponsibilityTemplateDto> _returnTemplates;
        private List<long> _siteIds;
        private long[] _responsibilityTemplatesId;
        private long _companyId;
        private Mock<ISiteService> _siteService;
        private Mock<IStatutoryResponsibilityTemplateService> _statutoryResponsibilityTemplateService;
        private Mock<IEmployeeService> _employeeService;

        [SetUp]
        public void Setup()
        {
            _companyId = 12456436L;
            _siteIds = new List<long> { 1234L, 56873L, 234675L, 234675L };
            _responsibilityTemplatesId = new [] { 12344L, 373L, 31675L, 216752475L };

            _returnedSites = new List<SiteDto>
                             {
                                 new SiteDto() { Id = 1234L, Name = "site 1"},
                                 new SiteDto() { Id = 56873L, Name = "site 2"},
                                 new SiteDto() { Id = 234675L, Name = "site 3"},
                                 new SiteDto() { Id = 234675L, Name = "site 4"}
                             };
            _siteService = new Mock<ISiteService>();
            _siteService
                .Setup(x => x.Search(It.Is<SearchSitesRequest>(y => 
                    y.CompanyId == _companyId &&
                    y.AllowedSiteIds == _siteIds
                )))
                .Returns(_returnedSites);

            _returnTemplates = new List<StatutoryResponsibilityTemplateDto>
                                   {
                                       new StatutoryResponsibilityTemplateDto{Id = 1234L, Title = "Template1"},
                                       new StatutoryResponsibilityTemplateDto{Id = 2345L, Title = "Template2"}
                                   };
            _statutoryResponsibilityTemplateService = new Mock<IStatutoryResponsibilityTemplateService>();
            
            _statutoryResponsibilityTemplateService
                .Setup(x => x.GetStatutoryResponsibilityTemplates()).Returns(_returnTemplates);

            _employeeService = new Mock<IEmployeeService>();
            _employeeService.Setup(x => x.Search(It.IsAny<SearchEmployeesRequest>())).Returns(new List<EmployeeDto>());
        }

        [Test]
        public void Given_company_id_and_allowed_site_ids_When_GetViewModel_Then_populate_view_model_with_sites()
        {
            // Given
            _target = GetTarget();

            // When
            var model = _target
                .WithCompanyId(_companyId)
                .WithAllowedSiteIds(_siteIds)
                .GetViewModel();

            // Then
            Assert.That(model.Sites, Is.InstanceOf<List<LookupDto>>());
            for (var i = 0; i < _returnedSites.Count; i++)
            {
                Assert.That(model.Sites.ElementAt(i).Id, Is.EqualTo(_returnedSites[i].Id));
                Assert.That(model.Sites.ElementAt(i).Name, Is.EqualTo(_returnedSites[i].Name));
            }
        }

        //[Test]
        //public void Given_responsibility_template_ids_When_GetViewModel_Then_populate_view_model_with_responsibility_templates()
        //{
        //    // Given
        //    _target = GetTarget();

        //    // When
        //    var model = _target
        //        .WithResponsibilityTemplateIds(_responsibilityTemplatesId)
        //        .GetViewModel();

        //    // Then
        //    Assert.That(model.ResponsibilityTemplates, Is.InstanceOf<List<StatutoryResponsibilityViewModel>>());
        //    for (var i = 0; i < _returnedSites.Count; i++)
        //    {
        //        Assert.That(model.Sites.ElementAt(i).Id, Is.EqualTo(_returnedSites[i].Id));
        //        Assert.That(model.Sites.ElementAt(i).Name, Is.EqualTo(_returnedSites[i].Name));
        //    }
        //}

        private GenerateResponsibilitiesViewModelFactory GetTarget()
        {
            return new GenerateResponsibilitiesViewModelFactory(_siteService.Object, _statutoryResponsibilityTemplateService.Object, _employeeService.Object, null);
        }
    }
}
