using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.ActionPlan;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Implementations.ActionPlan;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.WebSite.Areas.ActionPlans.Factories;
using BusinessSafe.WebSite.Areas.ActionPlans.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.ActionPlan.SearchActionPlan
{
    [TestFixture]
    [Category("Unit")]
    public class SearchActionPlanTests
    {
        private Mock<IActionPlanService> _actionPlanService;
        private Mock<ISiteService> _siteService;
        private Mock<ISiteGroupService> _siteGroupService;

        [SetUp]
        public void Setup()
        {
            _actionPlanService = new Mock<IActionPlanService>();
            _siteService = new Mock<ISiteService>();
            _siteGroupService = new Mock<ISiteGroupService>();
        }

        [Test]
        public void given_view_model_factory_when_get_view_model_then_result_returns_correct_view_model()
        {
            //given
            var target = GetTarget();
            
            //when
            var result = target.GetViewModel();
            
            //then
            Assert.That(result, Is.InstanceOf<ActionPlanIndexViewModel>());
        }


        [Test]
        public void given_company_id_when_get_view_model_then_service_is_called_with_corect_parameters()
        {
            //given
            var companyId = 123L;
            var target = GetTarget();
            var page = 1;
            var size = 10;
            var orderBy = "Title-asc";
            long[] allowedSiteIds = {132123, 123123};
            
            //when
            var result = target
                .WithCompanyId(companyId)
                .WithPageNumber(page)
                .WithPageSize(size)
                .WithOrderBy(orderBy)
                .WithAllowedSiteIds(allowedSiteIds)
                .GetViewModel();

            //then
            _actionPlanService.Verify(x => x.Search(It.Is<SearchActionPlanRequest>(y=>y.CompanyId==companyId)));
            _actionPlanService.Verify(x => x.Search(It.Is<SearchActionPlanRequest>(y => y.Page == page)));
            _actionPlanService.Verify(x => x.Search(It.Is<SearchActionPlanRequest>(y => y.PageSize == size)));
            _actionPlanService.Verify(x => x.Search(It.Is<SearchActionPlanRequest>(y => y.OrderBy == ActionPlanOrderByColumn.Title)));
            _actionPlanService.Verify(x => x.Search(It.Is<SearchActionPlanRequest>(y => y.Ascending == true)));
            _actionPlanService.Verify(x => x.Search(It.Is<SearchActionPlanRequest>(y => y.AllowedSiteIds == allowedSiteIds)));
        }

        [Test]
        public void given_factory_when_get_view_model_then_result_maps_dto_to_viewmodel()
        {
            //given
            var companyId = 123L;
            var expected = new ActionPlanDto()
                               {
                                   Id = 1234L,
                                   CompanyId = 1L,
                                   Title = "Action Plan1",
                                   Site = new SiteDto
                                              {
                                                  SiteId = 1L,
                                                  Name = "Site1"
                                              },
                                   DateOfVisit = DateTime.Now,
                                   VisitBy = "Consultant1",
                                   SubmittedOn = DateTime.Now.AddDays(-1)
                               };

            var actionPlans = new List<ActionPlanDto> {expected};

            var target = GetTarget();
            _actionPlanService.Setup(x => x.Search(It.IsAny<SearchActionPlanRequest>())).Returns(actionPlans);

            //when
            var result = target.WithCompanyId(companyId).GetViewModel();
            var actual = result.ActionPlans.FirstOrDefault();

            //then
            Assert.That(actual.Id,Is.EqualTo(expected.Id));
            Assert.That(actual.Title,Is.EqualTo(expected.Title));
            Assert.That(actual.Site,Is.EqualTo(expected.Site.Name));
            Assert.That(actual.VisitDate,Is.EqualTo(expected.DateOfVisit));
            Assert.That(actual.VisitBy,Is.EqualTo(expected.VisitBy));
            Assert.That(actual.SubmittedDate,Is.EqualTo(expected.SubmittedOn));
            //Assert.That(actual.Status,Is.EqualTo(expected.Status));
            //Assert.That(actual.EvaluationReport, Is.EqualTo(expected.EvaluationReport));
        }

        [Test]
        public void given_site_is_null_when_get_view_model_then_action_plan_site_is_null()
        {
            //given
            var companyId = 123L;
            var expected = new ActionPlanDto()
            {
                Id = 1234L,
                CompanyId = 1L,
                Title = "Action Plan1",
                DateOfVisit = DateTime.Now,
                VisitBy = "Consultant1",
                SubmittedOn = DateTime.Now.AddDays(-1)
            };

            var actionPlans = new List<ActionPlanDto> { expected };

            var target = GetTarget();
            _actionPlanService.Setup(x => x.Search(It.IsAny<SearchActionPlanRequest>())).Returns(actionPlans);

            //when
            var result = target.WithCompanyId(companyId).GetViewModel();
            var actual = result.ActionPlans.FirstOrDefault();

            //then
            Assert.IsNull(actual.Site);

        }

       

        public SearchActionPlanViewModelFactory GetTarget()
        {
            return new SearchActionPlanViewModelFactory(_actionPlanService.Object,_siteGroupService.Object, _siteService.Object);
        }
    }
}
