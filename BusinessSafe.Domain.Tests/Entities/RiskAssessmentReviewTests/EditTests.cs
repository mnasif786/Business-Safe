using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.CustomExceptions;
using NUnit.Framework;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.Factories;
using Moq;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentReviewTests
{
    [TestFixture]
    [Category("Unit")]
    public class EditTests
    {
        private Mock<ITaskCategoryRepository> _responsibilityTaskCategoryRepository;

        [SetUp]
        public void Setup()
        {
            _responsibilityTaskCategoryRepository = new Mock<ITaskCategoryRepository>();
        }

        [Test]
        public void Given_valid_RiskAssessmentReview_valid_parameters_When_update_called_Then_RiskAssessmentReview_is_updated_correctly()
        {
            //Given
            const long companyId = 1234L;
            var user = new UserForAuditing();

            var oldAssignee = Employee.Create(new AddUpdateEmployeeParameters
                {
                    ClientId = companyId,
                    Forename = "Al",
                    Surname = "Polden"
                },
                user);

            var newAssignee = Employee.Create(new AddUpdateEmployeeParameters
                {
                    ClientId = companyId,
                    Forename = "Paul",
                    Surname = "Davies"
                },
                user);

            var oldDate = new DateTime(2012, 01, 10);
            var taskCategory = TaskCategory.Create(3, "Test GRA Review");

            _responsibilityTaskCategoryRepository
                .Setup(x => x.GetGeneralRiskAssessmentTaskCategory())
                .Returns(taskCategory);

            var riskAssessmentReview = RiskAssessmentReviewFactory.Create(
                new GeneralRiskAssessment(),
                user,
                oldAssignee,
                oldDate,
                _responsibilityTaskCategoryRepository.Object,
                false,
                false,
                false,
                false,
                Guid.NewGuid());

            var newDate = new DateTime(2012, 11, 01);

            //When
            riskAssessmentReview.Edit(user, newAssignee, newDate);

            //Then
            Assert.That(riskAssessmentReview.ReviewAssignedTo, Is.EqualTo(newAssignee));
            Assert.That(riskAssessmentReview.CompletionDueDate, Is.EqualTo(newDate));
            Assert.That(riskAssessmentReview.RiskAssessmentReviewTask.TaskAssignedTo, Is.EqualTo(newAssignee));
            Assert.That(riskAssessmentReview.RiskAssessmentReviewTask.TaskCompletionDueDate, Is.EqualTo(newDate));
        }
    }
}
