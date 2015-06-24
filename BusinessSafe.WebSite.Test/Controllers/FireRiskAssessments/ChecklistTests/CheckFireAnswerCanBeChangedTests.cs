using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request.FireRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.FireRiskAssessments.ChecklistTests
{
    [TestFixture]
    public class CheckFireAnswerCanBeChangedTests
    {
        private long _fireChecklistId = 34L;
        private long _fireQuestionId = 56L;

        private Mock<IFireRiskAssessmentFurtherControlMeasureTaskService> _fireRiskAssessmentFurtherControlMeasureTaskService;


        [SetUp]
        public void Setup()
        {
            _fireRiskAssessmentFurtherControlMeasureTaskService = new Mock<IFireRiskAssessmentFurtherControlMeasureTaskService>();
        }

        [Test]
        public void When_CheckFireRiskAssessmentHasOutstandingFurtherControlMeasureTasks_Then_should_call_correct_methods()
        {
            // Given
            var target = GetTarget();
            var viewModel = new DoesAnswerHaveFurtherControlMeasureTasksViewModel
                                {
                                    FireChecklistId = _fireChecklistId,
                                    FireQuestionId = _fireQuestionId
                                };

            _fireRiskAssessmentFurtherControlMeasureTaskService
              .Setup(x => x.GetFurtherControlMeasureTaskCountsForAnswer(It.Is<GetFurtherControlMeasureTaskCountsForAnswerRequest>(
                                                                          y => y.FireChecklistId == viewModel.FireChecklistId &&
                                                                               y.FireQuestionId == viewModel.FireQuestionId
                                                                          ))).Returns(new GetFurtherControlMeasureTaskCountsForAnswerResponse() );

            // When
            target.CheckFireAnswerCanBeChanged(viewModel);

            // Then
            _fireRiskAssessmentFurtherControlMeasureTaskService
                .VerifyAll();
        }

        [Test]
        public void Given_tasks_exist_When_CheckFireRiskAssessmentHasOutstandingFurtherControlMeasureTasks_Then_should_return_correct_results()
        {
            // Given
            var target = GetTarget();
            var viewModel = new DoesAnswerHaveFurtherControlMeasureTasksViewModel
            {
                FireChecklistId = _fireChecklistId,
                FireQuestionId = _fireQuestionId
            };

            _fireRiskAssessmentFurtherControlMeasureTaskService
                .Setup(x => x.GetFurtherControlMeasureTaskCountsForAnswer(It.IsAny<GetFurtherControlMeasureTaskCountsForAnswerRequest>()))
                .Returns(new GetFurtherControlMeasureTaskCountsForAnswerResponse
                             {
                                 TotalFurtherControlMeasureTaskCount = 1
                             });

            // When
            var result = target.CheckFireAnswerCanBeChanged(viewModel) as JsonResult;

            // Then
            Assert.That(result.Data.ToString(), Contains.Substring("CanBeChanged = False"));
        }

        [Test]
        public void Given_completed_tasks_exist_When_CheckFireRiskAssessmentHasOutstandingFurtherControlMeasureTasks_Then_should_return_correct_results()
        {
            // Given
            var target = GetTarget();
            var viewModel = new DoesAnswerHaveFurtherControlMeasureTasksViewModel
            {
                FireChecklistId = _fireChecklistId,
                FireQuestionId = _fireQuestionId
            };

            _fireRiskAssessmentFurtherControlMeasureTaskService
                .Setup(x => x.GetFurtherControlMeasureTaskCountsForAnswer(It.IsAny<GetFurtherControlMeasureTaskCountsForAnswerRequest>()))
                .Returns(new GetFurtherControlMeasureTaskCountsForAnswerResponse
                {
                    TotalCompletedFurtherControlMeasureTaskCount = 1
                });

            // When
            var result = target.CheckFireAnswerCanBeChanged(viewModel) as JsonResult;

            // Then
            Assert.That(result.Data.ToString(), Contains.Substring("CanBeDeleted = False"));
        }

        [Test]
        public void Given_tasks_do_not_Exist_When_CheckFireRiskAssessmentHasOutstandingFurtherControlMeasureTasks_Then_should_return_correct_results()
        {
            // Given
            var target = GetTarget();
            var viewModel = new DoesAnswerHaveFurtherControlMeasureTasksViewModel
            {
                FireChecklistId = _fireChecklistId,
                FireQuestionId = _fireQuestionId
            };

            _fireRiskAssessmentFurtherControlMeasureTaskService
                .Setup(x => x.GetFurtherControlMeasureTaskCountsForAnswer(It.IsAny<GetFurtherControlMeasureTaskCountsForAnswerRequest>()))
                .Returns(new  GetFurtherControlMeasureTaskCountsForAnswerResponse
                             {
                                 TotalFurtherControlMeasureTaskCount = 0,
                                 TotalCompletedFurtherControlMeasureTaskCount = 0
                             });

            // When
            var result = target.CheckFireAnswerCanBeChanged(viewModel) as JsonResult;

            // Then
            Assert.That(result.Data.ToString(), Contains.Substring("CanBeChanged = True, CanBeDeleted = True"));
        }

        private ChecklistController GetTarget()
        {
            var target = new ChecklistController(
                null,
                null,
                null,
                _fireRiskAssessmentFurtherControlMeasureTaskService.Object
                );

            return TestControllerHelpers.AddUserToController(target);
        }
    }
}
