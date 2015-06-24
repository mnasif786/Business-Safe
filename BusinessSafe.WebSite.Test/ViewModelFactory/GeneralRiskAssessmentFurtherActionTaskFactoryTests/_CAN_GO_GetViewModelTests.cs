//using System;
//using System.Collections.Generic;
//using System.Linq;
//using BusinessSafe.Application.Contracts;
//using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
//using BusinessSafe.Application.DataTransferObjects;
//using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Factories;
//using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;
////using BusinessSafe.WebSite.Factories;
////using BusinessSafe.WebSite.ViewModels;
//using Moq;
//using NUnit.Framework;
//using BusinessSafe.Application.Contracts.RiskAssessments;

//namespace BusinessSafe.WebSite.Tests.ViewModelFactory.GeneralRiskAssessmentFurtherActionTaskFactoryTests
//{
//    [TestFixture]
//    [Category("Unit")]
//    public class GetViewModelTests
//    {
//        private Mock<IFurtherControlMeasureTaskService> _service;
//        private Mock<IRiskAssessmentHazardService> _riskAssessmentHazardService;
//        [SetUp]
//        public void Setup()
//        {
//            _service = new Mock<IFurtherControlMeasureTaskService>();
//            _riskAssessmentHazardService = new Mock<IRiskAssessmentHazardService>();

//        }

//        [Test]
//        public void When_get_view_model_without_riskassessmentfurtheractiontaskid_Then_return_correct_view_model()
//        {
//            //Given
//            var target = CreateEGeneralRiskAssessmentFurtherActionTaskViewModelFactory();

//            _service
//                .Setup(x => x.GetByIdAndCompanyId(0, 100))
//                .Returns(new GeneralRiskAssessmentFurtherControlMeasureTaskDto()
//                {
//                    Documents = new List<TaskDocumentDto>(),
//                    RiskAssessmentHazard = new RiskAssessmentHazardDto()
//                    {
//                        RiskAssessment = new GeneralRiskAssessmentDto(),
//                        Hazard = new HazardDto()
//                    },
//                    RiskAssessment = new GeneralRiskAssessmentDto()
//                }
//                );

//            _riskAssessmentHazardService
//                .Setup(x => x.GetByIdAndCompanyId(300, 100))
//                .Returns(new RiskAssessmentHazardDto
//                             {
//                                 Id = 300,
//                                 RiskAssessment = new GeneralRiskAssessmentDto
//                                                      {
//                                                          Id = 200,
//                                                          Title = "title"
//                                                      },
//                                 Hazard = new HazardDto
//                                              {
//                                                  Name = "name"
//                                              }
//                             });

//            //When
//            var result = target
//                            .WithRiskAssessmentFurtherActionTaksId(0)
//                            .WithCompanyId(100)
//                            .WithRiskAssessmentId(200)
//                            .WithRiskAssessmentHazardId(300)
//                            .GetViewModel();

//            //Then
//            Assert.That(result, Is.TypeOf<AddEditFurtherControlMeasureTaskViewModel>());
//            Assert.That(result.CompanyId, Is.EqualTo(0));
//            Assert.That(result.RiskAssessmentId, Is.EqualTo(200));
//            Assert.That(result.RiskAssessmentHazardId, Is.EqualTo(300));
//        }

//        [Test]
//        public void When_get_view_model_without_riskassessmentfurtheractiontaskid_Then_should_call_correct_methods()
//        {
//            //Given
//            var target = CreateEGeneralRiskAssessmentFurtherActionTaskViewModelFactory();

//            _service
//                .Setup(x => x.GetByIdAndCompanyId(0, 100))
//                .Returns(new GeneralRiskAssessmentFurtherControlMeasureTaskDto()
//                {
//                    Documents = new List<TaskDocumentDto>(),
//                    RiskAssessmentHazard = new RiskAssessmentHazardDto()
//                    {
//                        RiskAssessment = new GeneralRiskAssessmentDto(),
//                        Hazard = new HazardDto()
//                    },
//                    RiskAssessment = new GeneralRiskAssessmentDto()
//                }
//                );

//            _riskAssessmentHazardService
//                .Setup(x => x.GetByIdAndCompanyId(300, 100))
//                .Returns(new RiskAssessmentHazardDto
//                             {
//                                 Id = 300,
//                                 RiskAssessment = new GeneralRiskAssessmentDto
//                                                      {
//                                                          Id = 200,
//                                                          Title = "title"
//                                                      },
//                                 Hazard = new HazardDto
//                                              {
//                                                  Name = "name"
//                                              }
//                             });

//            //When
//            target
//                            .WithCompanyId(100)
//                            .WithRiskAssessmentId(200)
//                            .WithRiskAssessmentHazardId(300)
//                            .WithRiskAssessmentFurtherActionTaksId(0)
//                            .GetViewModel();

//            //Then
//            //_service.Verify(x => x.GetFurtherActionTask(200, 300, 500, 100), Times.Never());
//            _service.Verify(x => x.GetByIdAndCompanyId(500, 100), Times.Never());
//        }

//        [Test]
//        public void When_get_view_model_with_riskassessmentfurtheractiontaskid_Then_should_call_correct_methods()
//        {
//            //Given
//            var target = CreateEGeneralRiskAssessmentFurtherActionTaskViewModelFactory();

//            //var furtherActionTaskDto = new TaskDto();
//            //_service
//            //    .Setup(x => x.GetFurtherActionTask(200, 300, 500, 100))
//            //    .Returns(new FurtherControlMeasureTaskDto()
//            //                 {
//            //                     Documents = new List<FurtherControlMeasureDocumentDto>(),
//            //                     RiskAssessmentHazard = new RiskAssessmentHazardDto()
//            //                                                {
//            //                                                    RiskAssessment = new RiskAssessmentDto(),
//            //                                                    Hazard = new HazardDto()
//            //                                                },
//            //                     RiskAssessment = new RiskAssessmentDto()
//            //                 }
//            //    );

//            _service
//                .Setup(x => x.GetByIdAndCompanyId(500, 100))
//                .Returns(new GeneralRiskAssessmentFurtherControlMeasureTaskDto()
//                {
//                    Documents = new List<TaskDocumentDto>(),
//                    RiskAssessmentHazard = new RiskAssessmentHazardDto()
//                    {
//                        RiskAssessment = new GeneralRiskAssessmentDto(),
//                        Hazard = new HazardDto()
//                    },
//                    RiskAssessment = new GeneralRiskAssessmentDto()
//                }
//                );

//            //When
//            target
//                            .WithCompanyId(100)
//                            .WithRiskAssessmentId(200)
//                            .WithRiskAssessmentHazardId(300)
//                            .WithRiskAssessmentFurtherActionTaksId(500)
//                            .GetViewModel();

//            //Then
//            //_service.Verify(x => x.GetFurtherActionTask(200, 300, 500, 100));
//            _service.Verify(x => x.GetByIdAndCompanyId(500, 100));

//        }

//        private FurtherControlMeasureTaskViewModelFactory CreateEGeneralRiskAssessmentFurtherActionTaskViewModelFactory()
//        {
//            return new FurtherControlMeasureTaskViewModelFactory(_service.Object, _riskAssessmentHazardService.Object);
//        }
//    }
//}