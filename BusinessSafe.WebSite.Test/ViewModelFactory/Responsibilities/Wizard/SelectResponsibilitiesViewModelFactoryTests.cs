using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Responsibilities.Factories;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.Responsibilities.Wizard
{
    [TestFixture]
    public class SelectResponsibilitiesViewModelFactoryTests
    {
        private Mock<IStatutoryResponsibilityTemplateService> _statutoryResponsibilityTemplateService;
        private Mock<IResponsibilitiesService> _responsibilitiesService;
        [SetUp]
        public void Setup()
        {
            _statutoryResponsibilityTemplateService = new Mock<IStatutoryResponsibilityTemplateService>();
            _responsibilitiesService = new Mock<IResponsibilitiesService>();

            _statutoryResponsibilityTemplateService.Setup(x => x.GetStatutoryResponsibilityTemplates())
                .Returns(() => new List<StatutoryResponsibilityTemplateDto>());
        }

        public SelectResponsibilitiesViewModelFactory GetTarget()
        {
            return new SelectResponsibilitiesViewModelFactory(_statutoryResponsibilityTemplateService.Object, _responsibilitiesService.Object);
        }

        [Test]
        public void Given_selected_responsibilities_When_GetViewModel_Then_those_responsibilities_are_marked_as_selected()
        {
            // Given
            const int companyId = 12345;
            var templates = new List<StatutoryResponsibilityTemplateDto>
                            {
                                new StatutoryResponsibilityTemplateDto() { Id = 123L, Description = "description", ResponsibilityCategory = new ResponsibilityCategoryDto() { Category = "category"}, ResponsibilityReason = new ResponsibilityReasonDto() { Reason = "reason"}},
                                new StatutoryResponsibilityTemplateDto() { Id = 456L, Description = "description", ResponsibilityCategory = new ResponsibilityCategoryDto() { Category = "category"}, ResponsibilityReason = new ResponsibilityReasonDto() { Reason = "reason"} },
                                new StatutoryResponsibilityTemplateDto() { Id = 789L, Description = "description", ResponsibilityCategory = new ResponsibilityCategoryDto() { Category = "category"}, ResponsibilityReason = new ResponsibilityReasonDto() { Reason = "reason"} }
                            };

            _statutoryResponsibilityTemplateService
               .Setup(x => x.GetStatutoryResponsibilityTemplates())
               .Returns(templates);

            // When
            var result = GetTarget()
                .WithCompanyId(companyId)
                .WithSelectedResponsibilityTemplates(new [] { templates.First().Id })
                .GetViewModel();

            // Then
            Assert.IsTrue(result.Responsibilities.Single(x => x.Id == templates.First().Id ).IsSelected);
            Assert.IsFalse(result.Responsibilities.Single(x => x.Id == templates.ElementAt(1).Id ).IsSelected);
            Assert.IsFalse(result.Responsibilities.Single(x => x.Id == templates.ElementAt(2).Id).IsSelected);
        }

        private ResponsibilityDto CreateResponsibilityDto(long siteId, List<StatutoryResponsibilityTaskTemplateDto> tasks)
        {
            return new ResponsibilityDto
            {
                Site = new SiteDto() { Id = siteId },
                UncreatedStatutoryResponsibilityTaskTemplates = tasks
            };
        }


    }
}
