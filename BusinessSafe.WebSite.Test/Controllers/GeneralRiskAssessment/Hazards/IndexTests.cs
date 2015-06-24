using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Implementations.Company;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.GeneralRiskAssessment.Hazards
{
    [TestFixture]
    [Category("Unit")]
    public class IndexTests : BaseControllersHazardsTests
    {
        [Test]
        public void Given_that_hazards_tab_is_called_Then_the_result_view_model_is_correct() 
        {
            //Given
            const long companyId = 100;
            const long riskAssessmentId = 50;

            var target = GetTarget(companyId, riskAssessmentId);

            //When
            var result = target.Index(companyId, riskAssessmentId) as ViewResult;            

            //Then
            Assert.That(result.Model, Is.TypeOf<HazardsViewModel>());
            Assert.That(((HazardsViewModel) result.Model).CompanyId, Is.EqualTo(companyId));
            Assert.That(((HazardsViewModel)result.Model).RiskAssessmentId, Is.EqualTo(riskAssessmentId));
        }

        [Test]
        public void Given_hazards_have_been_selected_then_these_hazards_do_not_appear_in_the_hazard_list_of_the_view_model()
        {
            //Given
            const long companyId = 100;
            const long riskAssessmentId = 50;
            var selectedHazard = new HazardDto() {Id = 349875, Name = "Eden"};

             RiskAssessmentService
                .Setup(x => x.GetRiskAssessmentWithHazardsAndPeopleAtRisk(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(new GeneralRiskAssessmentDto
                             {
                                 Id = riskAssessmentId, 
                                 CreatedOn = DateTime.Now, 
                                 CompanyId = companyId,
                                 Hazards = new List<HazardDto>() { selectedHazard } ,
                                 PeopleAtRisk = new List<PeopleAtRiskDto>()
                             });

            CompanyDefaultService
                .Setup(x => x.GetAllMultiHazardRiskAssessmentHazardsForCompany(It.IsAny<long>(), It.IsAny<HazardTypeEnum>(), It.IsAny<long>()))
                .Returns(() => new List<CompanyDefaultDto>()
                {
                    new CompanyDefaultDto() {Id = selectedHazard.Id, Name = selectedHazard.Name}
                });
                

            var target = GetTarget(companyId, riskAssessmentId);

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
         
            RiskAssessmentService
               .Setup(x => x.GetRiskAssessmentWithHazardsAndPeopleAtRisk(It.IsAny<long>(), It.IsAny<long>()))
               .Returns(new GeneralRiskAssessmentDto
               {
                   Id = riskAssessmentId,
                   CreatedOn = DateTime.Now,
                   CompanyId = companyId,
                   Hazards = new List<HazardDto>(),
                   PeopleAtRisk = new List<PeopleAtRiskDto>()
               });

            CompanyDefaultService
                .Setup(x => x.GetAllMultiHazardRiskAssessmentHazardsForCompany(It.IsAny<long>(), It.IsAny<HazardTypeEnum>(), It.IsAny<long>()))
                .Returns(() => new List<CompanyDefaultDto>()
                {
                    new CompanyDefaultDto(){Id = 123123},
                    new CompanyDefaultDto(){Id = 124154134},
                    new CompanyDefaultDto(){Id = 123},
                });

            var target = GetTarget(companyId, riskAssessmentId);

            //When
            var result = (target.Index(companyId, riskAssessmentId) as ViewResult).Model as HazardsViewModel;

            //Then
            Assert.That(result.SelectedHazards.Count(), Is.EqualTo(0));
            Assert.That(result.Hazards.Count(), Is.EqualTo(3));
        }
    }
}
