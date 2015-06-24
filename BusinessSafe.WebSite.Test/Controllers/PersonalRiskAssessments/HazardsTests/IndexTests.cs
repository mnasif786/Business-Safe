using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Implementations.Company;
using BusinessSafe.Application.Implementations.RiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Controllers;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.PersonalRiskAssessments.HazardsTests
{
    [TestFixture]
    public class HazardsTests
    {
        private Mock<ICompanyDefaultService> _companyDefaultService;
        private Mock<IPersonalRiskAssessmentService> _riskAssessmentService;

        [SetUp]
        public void Setup()
        {
            _companyDefaultService = new Mock<ICompanyDefaultService>();
            _riskAssessmentService = new Mock<IPersonalRiskAssessmentService>();
        }

        [Test]
        public void Given_hazards_have_been_selected_then_these_hazards_do_not_appear_in_the_hazard_list_of_the_view_model()
        {
            //Given
            const long companyId = 100;
            const long riskAssessmentId = 50;
            var selectedHazard = new HazardDto() { Id = 349875, Name = "Eden" };

            _riskAssessmentService
               .Setup(x => x.GetRiskAssessmentWithHazards(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<Guid>()))
               .Returns(new PersonalRiskAssessmentDto()
               {
                   Id = riskAssessmentId,
                   CreatedOn = DateTime.Now,
                   CompanyId = companyId,
                   Hazards = new List<HazardDto>() { selectedHazard }
                  
               });

            _companyDefaultService
                .Setup(x => x.GetAllMultiHazardRiskAssessmentHazardsForCompany(It.IsAny<long>(), It.IsAny<HazardTypeEnum>(), It.IsAny<long>()))
                .Returns(() => new List<CompanyDefaultDto>()
                {
                    new CompanyDefaultDto() {Id = selectedHazard.Id, Name = selectedHazard.Name}
                });


            var target = GetTarget();

            //When
            var result = (target.Index(companyId, riskAssessmentId) as ViewResult).Model as HazardsViewModel;

            //Then
            Assert.That(result.SelectedHazards.Count(), Is.EqualTo(1));
            Assert.That(result.Hazards.Count(), Is.EqualTo(0));
        }

        [Test]
        public void Given_hazards_have_not_been_selected_then_these_hazards_appear_in_the_hazard_list_of_the_view_model()
        {
            //Given
            const long companyId = 100;
            const long riskAssessmentId = 50;

            _riskAssessmentService
               .Setup(x => x.GetRiskAssessmentWithHazards(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<Guid>()))
               .Returns(new PersonalRiskAssessmentDto
               {
                   Id = riskAssessmentId,
                   CreatedOn = DateTime.Now,
                   CompanyId = companyId,
                   Hazards = new List<HazardDto>()
               });

            _companyDefaultService
                .Setup(x => x.GetAllMultiHazardRiskAssessmentHazardsForCompany(It.IsAny<long>(), It.IsAny<HazardTypeEnum>(), It.IsAny<long>()))
                .Returns(() => new List<CompanyDefaultDto>()
                {
                    new CompanyDefaultDto(){Id = 123123},
                    new CompanyDefaultDto(){Id = 124154134},
                    new CompanyDefaultDto(){Id = 123}
                });

            var target = GetTarget();

            //When
            var result = (target.Index(companyId, riskAssessmentId) as ViewResult).Model as HazardsViewModel;

            //Then
            Assert.That(result.SelectedHazards.Count(), Is.EqualTo(0));
            Assert.That(result.Hazards.Count(), Is.EqualTo(3));
        }

        private HazardsController GetTarget()
        {
            var result = new HazardsController(_companyDefaultService.Object, _riskAssessmentService.Object,null,null);

            return TestControllerHelpers.AddUserToController(result);
        }  
    }
}
