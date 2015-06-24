using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Controllers;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.FireRiskAssessments.Hazards
{
    [TestFixture]
    [Category("Unit")]
    public class LoadTabTests
    {
        Mock<ICompanyDefaultService> _companyDefaultService;
        private Mock<IFireRiskAssessmentService> _fireRiskAssessmentService;

        [SetUp]
        public void SetUp()
        {
            _companyDefaultService = new Mock<ICompanyDefaultService>();
            _fireRiskAssessmentService = new Mock<IFireRiskAssessmentService>();
        }

        [Test]
        public void When_get_Index_Then_should_return_the_correct_view()
        {
            //Given
            const long companyId = 100;
            const long riskAssessmentId = 50;

            var target = GetTarget();

            _fireRiskAssessmentService
                .Setup(x => x.GetRiskAssessmentWithFireSafetyControlMeasuresAndPeopleAtRisk(riskAssessmentId, companyId))
                .Returns(new FireRiskAssessmentDto());

            //When
            var result = target.Index(companyId, riskAssessmentId) as ViewResult;

            //Then
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void When_get_Index_Then_should_return_the_correct_view_model()
        {
            //Given
            const long companyId = 100;
            const long riskAssessmentId = 50;

            var target = GetTarget();

            IEnumerable<CompanyDefaultDto> companyDefaultsServiceReturnedPeopleAtRisk = new List<CompanyDefaultDto>()
                                                                      {
                                                                          new CompanyDefaultDto()
                                                                              {
                                                                                  Id = 1,
                                                                                  Name = "Test Name 1"
                                                                              },
                                                                          new CompanyDefaultDto()
                                                                              {
                                                                                  Id = 2,
                                                                                  Name = "Test Name 2"
                                                                              }
                                                                      };

            IEnumerable<CompanyDefaultDto> companyDefaultsServiceReturnedFireSafetyControlMeasures = new List<CompanyDefaultDto>()
                                                                      {
                                                                          new CompanyDefaultDto()
                                                                              {
                                                                                  Id = 1,
                                                                                  Name = "Test Name 1"
                                                                              },
                                                                          new CompanyDefaultDto()
                                                                              {
                                                                                  Id = 2,
                                                                                  Name = "Test Name 2"
                                                                              }
                                                                      };

            IEnumerable<CompanyDefaultDto> companyDefaultsServiceReturnedSourcesOfIgnition = new List<CompanyDefaultDto>()
                                                                      {
                                                                          new CompanyDefaultDto()
                                                                              {
                                                                                  Id = 1,
                                                                                  Name = "Test Name 1"
                                                                              },
                                                                          new CompanyDefaultDto()
                                                                              {
                                                                                  Id = 2,
                                                                                  Name = "Test Name 2"
                                                                              }
                                                                      };

            IEnumerable<CompanyDefaultDto> companyDefaultsServiceReturnedSourcesOfFuel = new List<CompanyDefaultDto>()
                                                                      {
                                                                          new CompanyDefaultDto()
                                                                              {
                                                                                  Id = 1,
                                                                                  Name = "Test Name 1"
                                                                              },
                                                                          new CompanyDefaultDto()
                                                                              {
                                                                                  Id = 2,
                                                                                  Name = "Test Name 2"
                                                                              }
                                                                      };
            

            _companyDefaultService.Setup(x => x.GetAllPeopleAtRiskForRiskAssessments(companyId,riskAssessmentId)).Returns(companyDefaultsServiceReturnedPeopleAtRisk);
            _companyDefaultService.Setup(x => x.GetAllFireSafetyControlMeasuresForRiskAssessments(companyId, riskAssessmentId)).Returns(companyDefaultsServiceReturnedFireSafetyControlMeasures);
            _companyDefaultService.Setup(x => x.GetAllSourceOfIgnitionForRiskAssessment(companyId,riskAssessmentId)).Returns(companyDefaultsServiceReturnedSourcesOfIgnition);
            _companyDefaultService.Setup(x => x.GetAllSourceOfFuelForRiskAssessment(companyId,riskAssessmentId)).Returns(companyDefaultsServiceReturnedSourcesOfFuel);

            var riskAssessmentFireSafetyControlMeasures = new List<FireRiskAssessmentControlMeasureDto>()
                                                              {
                                                                  new FireRiskAssessmentControlMeasureDto
                                                                  {
                                                                      ControlMeasure = new FireSafetyControlMeasureDto() { Id = 2, Name = "Test Name 2" }
                                                                  }
                                                              };

            var riskAssessmentSourcesOfIgnition = new List<FireRiskAssessmentSourceOfIgnitionDto>()
                                                      {
                                                          new FireRiskAssessmentSourceOfIgnitionDto
                                                              {
                                                                  SourceOfIgnition =
                                                                      new SourceOfIgnitionDto()
                                                                          {Id = 2, Name = "Test Name 2"}
                                                              }
                                                      };

            var riskAssessmentPeopleAtRisk = new List<PeopleAtRiskDto>() { new PeopleAtRiskDto() { Id = 2, Name = "Test Name 2" } };

            var riskAssessmentSourcesOfFuel = new List<FireRiskAssessmentSourceOfFuelDto>()
                                                  {
                                                      new FireRiskAssessmentSourceOfFuelDto
                                                          {
                                                              SourceOfFuel =
                                                                  new SourceOfFuelDto() {Id = 2, Name = "Test Name 2"}
                                                          }
                                                  };

            var riskAssessmentDto = new FireRiskAssessmentDto()
                                        {
                                            PeopleAtRisk = riskAssessmentPeopleAtRisk,
                                            FireSafetyControlMeasures = riskAssessmentFireSafetyControlMeasures,
                                            FireRiskAssessmentSourcesOfIgnition = riskAssessmentSourcesOfIgnition,
                                            FireRiskAssessmentSourcesOfFuel = riskAssessmentSourcesOfFuel
                                        };
            _fireRiskAssessmentService
                .Setup(x => x.GetRiskAssessmentWithFireSafetyControlMeasuresAndPeopleAtRisk(riskAssessmentId, companyId))
                .Returns(riskAssessmentDto);


            //When
            var result = target.Index(companyId, riskAssessmentId) as ViewResult;

            //Then
            var model = result.Model as HazardsViewModel;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.CompanyId, Is.EqualTo(companyId));
            Assert.That(model.RiskAssessmentId, Is.EqualTo(riskAssessmentId));
            
            Assert.That(model.PeopleAtRisk.Count(), Is.EqualTo(companyDefaultsServiceReturnedPeopleAtRisk.Count()));
            Assert.That(model.SelectedPeopleAtRisk.Count(), Is.EqualTo(riskAssessmentPeopleAtRisk.Count()));
            
            Assert.That(model.FireSafetyControlMeasures.Count(), Is.EqualTo(companyDefaultsServiceReturnedFireSafetyControlMeasures.Count()));
            Assert.That(model.SelectedFireSafetyControlMeasures.Count(), Is.EqualTo(riskAssessmentFireSafetyControlMeasures.Count()));

            Assert.That(model.SourceOfIgnitions.Count(), Is.EqualTo(companyDefaultsServiceReturnedSourcesOfIgnition.Count()));
            Assert.That(model.SelectedSourceOfIgnitions.Count(), Is.EqualTo(riskAssessmentSourcesOfIgnition.Count()));

            Assert.That(model.SourceOfFuels.Count(), Is.EqualTo(companyDefaultsServiceReturnedSourcesOfFuel.Count()));
            Assert.That(model.SelectedSourceOfFuels.Count(), Is.EqualTo(riskAssessmentSourcesOfFuel.Count()));

        }

        [Test]
        public void When_get_Index_Then_should_call_the_correct_methods()
        {
            //Given
            const long companyId = 100;
            const long riskAssessmentId = 50;

            var target = GetTarget();

            _fireRiskAssessmentService
                .Setup(x => x.GetRiskAssessmentWithFireSafetyControlMeasuresAndPeopleAtRisk(riskAssessmentId, companyId))
                .Returns(new FireRiskAssessmentDto());

            //When
            target.Index(companyId, riskAssessmentId);

            //Then
            _companyDefaultService.Verify(x => x.GetAllPeopleAtRiskForRiskAssessments(companyId, riskAssessmentId));
            _companyDefaultService.Verify(x => x.GetAllFireSafetyControlMeasuresForRiskAssessments(companyId, riskAssessmentId));
            _companyDefaultService.Verify(x => x.GetAllSourceOfIgnitionForRiskAssessment(companyId,riskAssessmentId));
            _companyDefaultService.Verify(x => x.GetAllSourceOfFuelForRiskAssessment(companyId,riskAssessmentId));
            _fireRiskAssessmentService.Verify(x => x.GetRiskAssessmentWithFireSafetyControlMeasuresAndPeopleAtRisk(riskAssessmentId, companyId));
        }

        private HazardsController GetTarget()
        {
            var target = new HazardsController(_companyDefaultService.Object, _fireRiskAssessmentService.Object, null);
            return TestControllerHelpers.AddUserToController(target);
        }
    }


}
