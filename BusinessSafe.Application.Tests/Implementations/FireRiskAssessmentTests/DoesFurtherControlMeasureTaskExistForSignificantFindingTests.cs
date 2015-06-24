using System;
using System.Collections.Generic;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Application.Implementations.FireRiskAssessments;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.Application.Request.FireRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;
using NServiceBus;

namespace BusinessSafe.Application.Tests.Implementations.FireRiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class DoesFurtherControlMeasureTaskExistForSignificantFindingTests
    {
        private Mock<IFireAnswerRepository> _fireAnswerRepository;
        private Mock<IBus> _bus;

        [SetUp]
        public void Setup()
        {
            _fireAnswerRepository = new Mock<IFireAnswerRepository>();
            _bus = new Mock<IBus>();
        }

        [Test]
        public void When_GetByChecklistIdAndQuestionId_is_called_Then_should_call_correct_methods()
        {
            // Given
            var service = GetService();

            var request = new GetFurtherControlMeasureTaskCountsForAnswerRequest
            {
                FireChecklistId = 34L,
                FireQuestionId = 56L
            };

            _fireAnswerRepository
                .Setup(x => x.GetByChecklistIdAndQuestionId(request.FireChecklistId, request.FireQuestionId))
                .Returns(new FireAnswer());

            // When
            service.GetFurtherControlMeasureTaskCountsForAnswer(request);

            // Then
            _fireAnswerRepository.VerifyAll();
        }

        [Test]
        public void Given_fire_answer_does_not_have_significant_finding_When_GetByChecklistIdAndQuestionId_return_correct_result()
        {
            // Given
            var service = GetService();

            var request = new GetFurtherControlMeasureTaskCountsForAnswerRequest
            {
                FireChecklistId = 34L,
                FireQuestionId = 56L
            };

            var fireAnswer = new FireAnswer();
            fireAnswer.SignificantFinding = null;

            _fireAnswerRepository
                .Setup(x => x.GetByChecklistIdAndQuestionId(request.FireChecklistId, request.FireQuestionId))
                .Returns(fireAnswer)
                ;

            // When
            var result = service.GetFurtherControlMeasureTaskCountsForAnswer(request);

            // Then
            Assert.That(result.TotalCompletedFurtherControlMeasureTaskCount, Is.EqualTo(0));
            Assert.That(result.TotalFurtherControlMeasureTaskCount, Is.EqualTo(0));
        }

        [Test]
        public void Given_fire_answer_with_significant_finding_but_with_has_no_tasks_When_GetByChecklistIdAndQuestionId_return_correct_result()
        {
            // Given
            var service = GetService();

            var request = new GetFurtherControlMeasureTaskCountsForAnswerRequest
            {
                FireChecklistId = 34L,
                FireQuestionId = 56L
            };

            var fireAnswer = new FireAnswer();
            fireAnswer.SignificantFinding = new SignificantFinding();

            _fireAnswerRepository
                .Setup(x => x.GetByChecklistIdAndQuestionId(request.FireChecklistId, request.FireQuestionId))
                .Returns(fireAnswer)
                ;

            // When
            var result = service.GetFurtherControlMeasureTaskCountsForAnswer(request);

            // Then
            Assert.That(result.TotalCompletedFurtherControlMeasureTaskCount, Is.EqualTo(0));
            Assert.That(result.TotalFurtherControlMeasureTaskCount, Is.EqualTo(0));
        }

        [Test]
        public void Given_fire_answer_with_significant_finding_and_has_task_When_GetByChecklistIdAndQuestionId_return_correct_result()
        {
            // Given
            var service = GetService();

            var request = new GetFurtherControlMeasureTaskCountsForAnswerRequest
            {
                FireChecklistId = 34L,
                FireQuestionId = 56L
            };

            var fireAnswer = new FireAnswer();
            var significantFinding = new SignificantFinding
            {
                FurtherControlMeasureTasks = new List<FireRiskAssessmentFurtherControlMeasureTask>
                {
                    new FireRiskAssessmentFurtherControlMeasureTask
                    {
                        Id = 12L
                    }
                }
            };

            fireAnswer.SignificantFinding = significantFinding;

            _fireAnswerRepository
                .Setup(x => x.GetByChecklistIdAndQuestionId(request.FireChecklistId, request.FireQuestionId))
                .Returns(fireAnswer)
                ;

            // When
            var result = service.GetFurtherControlMeasureTaskCountsForAnswer(request);

            // Then
            Assert.That(result.TotalFurtherControlMeasureTaskCount, Is.EqualTo(1));
            Assert.That(result.TotalCompletedFurtherControlMeasureTaskCount, Is.EqualTo(0));
        }

        [Test]
        public void Given_fire_answer_with_significant_finding_and_has_task_deleted_When_GetByChecklistIdAndQuestionId_return_correct_result()
        {
            // Given
            var service = GetService();

            var request = new GetFurtherControlMeasureTaskCountsForAnswerRequest
            {
                FireChecklistId = 34L,
                FireQuestionId = 56L
            };

            var fireAnswer = new FireAnswer();
            var significantFinding = new SignificantFinding
            {
                FurtherControlMeasureTasks = new List<FireRiskAssessmentFurtherControlMeasureTask>
                {
                    new FireRiskAssessmentFurtherControlMeasureTask
                    {
                        Id = 12L,
                        Deleted = true
                    }
                }
            };
            fireAnswer.SignificantFinding = significantFinding;

            _fireAnswerRepository
                .Setup(x => x.GetByChecklistIdAndQuestionId(request.FireChecklistId, request.FireQuestionId))
                .Returns(fireAnswer)
                ;

            // When
            var result = service.GetFurtherControlMeasureTaskCountsForAnswer(request);

            // Then
            Assert.That(result.TotalFurtherControlMeasureTaskCount, Is.EqualTo(0));
            Assert.That(result.TotalCompletedFurtherControlMeasureTaskCount, Is.EqualTo(0));
        }

        [Test]
        public void Given_fire_answer_with_significant_finding_and_has_a_completed_task_When_GetByChecklistIdAndQuestionId_return_correct_result()
        {
            // Given
            var service = GetService();

            var request = new GetFurtherControlMeasureTaskCountsForAnswerRequest
            {
                FireChecklistId = 34L,
                FireQuestionId = 56L
            };

            var fireAnswer = new FireAnswer();
            var significantFinding = new SignificantFinding
            {
                FurtherControlMeasureTasks = new List<FireRiskAssessmentFurtherControlMeasureTask>
                {
                    new FireRiskAssessmentFurtherControlMeasureTask
                    {
                        Id = 12L,
                        TaskCompletedDate = DateTime.Now,
                        TaskCompletedComments = "my comments",
                        TaskStatus = TaskStatus.Completed,
                        Deleted = false
                    }
                }
            };
            fireAnswer.SignificantFinding = significantFinding;

            _fireAnswerRepository
               .Setup(x => x.GetByChecklistIdAndQuestionId(request.FireChecklistId, request.FireQuestionId))
               .Returns(fireAnswer)
               ;

            // When
            var result = service.GetFurtherControlMeasureTaskCountsForAnswer(request);

            // Then
            Assert.That(result.TotalCompletedFurtherControlMeasureTaskCount, Is.EqualTo(1));
        }

        private FireRiskAssessmentFurtherControlMeasureTaskService GetService()
        {
            var riskAssessmentService = new FireRiskAssessmentFurtherControlMeasureTaskService(
                null,
                null,
                null,
                null,
                null,
                _fireAnswerRepository.Object,
                _bus.Object
                );
            return riskAssessmentService;
        }
    }
}
