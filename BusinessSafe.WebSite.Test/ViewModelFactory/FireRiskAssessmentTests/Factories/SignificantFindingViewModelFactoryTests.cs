using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.FireRiskAssessmentTests.Factories
{
    [TestFixture]
    public class SignificantFindingViewModelFactoryTests
    {
        private Mock<IFireRiskAssessmentService> _riskAssessmentService;
        private SignificantFindingViewModelFactory _target;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentService = new Mock<IFireRiskAssessmentService>();
            _target = new SignificantFindingViewModelFactory(_riskAssessmentService.Object);
        }


        [Test]
        public void When_GetViewModel_Then_should_call_correct_methods()
        {
            // Given
            const long riskAssessmentId = 100;
            const long companyId = 200;

            var riskAssessmentDto = new FireRiskAssessmentDto();
            _riskAssessmentService
                .Setup(x => x.GetRiskAssessmentWithSignificantFindings(riskAssessmentId, companyId))
                .Returns(riskAssessmentDto);

            // When
            _target
                .WithRiskAssessmentId(riskAssessmentId)
                .WithCompanyId(companyId)
                .GetViewModel();

            // Then
            _riskAssessmentService.VerifyAll();

        }

        [Test]
        public void When_GetViewModel_Then_should_return_correct_result()
        {
            // Given
            const long riskAssessmentId = 100;
            const long companyId = 200;

            var riskAssessmentDto = new FireRiskAssessmentDto()
                                        {
                                            Reference = "REF",
                                            Title = "My Title",
                                            PersonAppointed = "Legend",
                                            SignificantFindings = new SignificantFindingDto[]
                                                                      {
                                                                          new SignificantFindingDto()
                                                                              {
                                                                                  Id = 1,
                                                                                  FireAnswer = new FireAnswerDto
                                                                                                   {
                                                                                                       Id=10001,
                                                                                                       Question = new QuestionDto
                                                                                                                      {
                                                                                                                          ListOrder = 1,
                                                                                                                          Text="Test Title"
                                                                                                                      }
                                                                                                   },
                                                                                  FurtherActionTasks = new LinkedList<TaskDto>()
                                                                              } 
                                                                      }
                                        };
            _riskAssessmentService
                .Setup(x => x.GetRiskAssessmentWithSignificantFindings(riskAssessmentId, companyId))
                .Returns(riskAssessmentDto);

            // When
            var result = _target
                .WithRiskAssessmentId(riskAssessmentId)
                .WithCompanyId(companyId)
                .GetViewModel();

            // Then
            Assert.That(result.CompanyId, Is.EqualTo(companyId));
            Assert.That(result.RiskAssessmentId, Is.EqualTo(riskAssessmentId));
            Assert.That(result.SignificantFindings.Count(), Is.EqualTo(riskAssessmentDto.SignificantFindings.Count()));
            Assert.That(result.SignificantFindings.First().Id, Is.EqualTo(riskAssessmentDto.SignificantFindings.First().Id));
        }

        [Test]
        public void Given_risk_assessment_with_multiple_significant_findings_When_GetViewModel_Then_should_return_correct_result()
        {
            // Given
            const long riskAssessmentId = 100;
            const long companyId = 200;

            var riskAssessmentDto = new FireRiskAssessmentDto()
            {
                Reference = "REF",
                Title = "My Title",
                PersonAppointed = "Legend",
                SignificantFindings = new[]
                                                                      {
                                                                          new SignificantFindingDto()
                                                                              {
                                                                                  Id = 1,
                                                                                  FireAnswer = new FireAnswerDto
                                                                                                   {
                                                                                                       Id=10001,
                                                                                                       Question = new QuestionDto
                                                                                                                      {
                                                                                                                          ListOrder = 1,
                                                                                                                          Text="Test Title 1"
                                                                                                                      }
                                                                                                   },
                                                                                  FurtherActionTasks = new LinkedList<TaskDto>()
                                                                              },
                                                                         new SignificantFindingDto()
                                                                              {
                                                                                  Id = 2,
                                                                                  FireAnswer = new FireAnswerDto
                                                                                                   {
                                                                                                       Id=10002,
                                                                                                       Question = new QuestionDto
                                                                                                                      {
                                                                                                                          ListOrder = 2,
                                                                                                                          Text="Test Title 2"
                                                                                                                      }
                                                                                                   },
                                                                                  FurtherActionTasks = new LinkedList<TaskDto>()
                                                                              }         
                                                                      }
            };
            _riskAssessmentService
                .Setup(x => x.GetRiskAssessmentWithSignificantFindings(riskAssessmentId, companyId))
                .Returns(riskAssessmentDto);

            // When
            var result = _target
                .WithRiskAssessmentId(riskAssessmentId)
                .WithCompanyId(companyId)
                .GetViewModel();


            // Then
            Assert.That(result.CompanyId, Is.EqualTo(companyId));
            Assert.That(result.RiskAssessmentId, Is.EqualTo(riskAssessmentId));
            Assert.That(result.SignificantFindings.Count(), Is.EqualTo(riskAssessmentDto.SignificantFindings.Count()));
            Assert.That(result.SignificantFindings.First().Id, Is.EqualTo(riskAssessmentDto.SignificantFindings.First().Id));
            Assert.That(result.SignificantFindings.Last().Id, Is.EqualTo(riskAssessmentDto.SignificantFindings.Last().Id));
        }
    }
}