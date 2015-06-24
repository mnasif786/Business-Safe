using System.Collections.Generic;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Controllers;
using Moq;
using NUnit.Framework;
using System;

namespace BusinessSafe.WebSite.Tests.Controllers.GeneralRiskAssessment.Hazards
{
    public class BaseControllersHazardsTests
    {
        public Mock<ICompanyDefaultService> CompanyDefaultService;
        public Mock<IGeneralRiskAssessmentAttachmentService> RiskAssessmentAttachmentService;
        public Mock<IGeneralRiskAssessmentService> RiskAssessmentService;

        [SetUp]
        public void SetUp()
        {
            CompanyDefaultService = new Mock<ICompanyDefaultService>();
            RiskAssessmentService = new Mock<IGeneralRiskAssessmentService>();
            RiskAssessmentAttachmentService = new Mock<IGeneralRiskAssessmentAttachmentService>();

            var riskAssessmentId = 1L;
            var companyId = 2L;

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
            RiskAssessmentService
                .Setup(x => x.GetRiskAssessmentWithHazards(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(new GeneralRiskAssessmentDto
                {
                    Id = riskAssessmentId,
                    CreatedOn = DateTime.Now,
                    CompanyId = companyId,
                    Hazards = new List<HazardDto>(),
                    PeopleAtRisk = new List<PeopleAtRiskDto>()
                });

            var hazards = new List<CompanyDefaultDto>();
            CompanyDefaultService
                .Setup(x => x.GetAllHazardsForCompany(It.IsAny<long>()))
                .Returns(hazards);

            var peopleAtRisk = new List<CompanyDefaultDto>();
            CompanyDefaultService
                .Setup(x => x.GetAllHazardsForCompany(It.IsAny<long>()))
                .Returns(peopleAtRisk);
        }

        public HazardsController GetTarget(long companyId, long riskAssessmentId)
        {
            var result = new HazardsController(RiskAssessmentService.Object, RiskAssessmentAttachmentService.Object, CompanyDefaultService.Object, null, null);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}