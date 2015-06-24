using BusinessSafe.Application.Request.HazardousSubstanceInventory;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModels
{
    [TestFixture]
    [Category("Unit")]
    public class AddHazardousSubstanceRequestTests
    {

        [Test]
        public void Given_a_request_where_previous_assessment_required_was_true_and_current_assessment_required_is_true_When_GetIsAssessmentRequired_Then_should_return_false()
        {
            // Arrange
            var request = new AddHazardousSubstanceRequest
                              {
                                  PreviousAssessmentRequired = true, 
                                  AssessmentRequired = true
                              };

            // Act
            bool result = request.IsAssessmentRequired();

            // Assert
            Assert.False(result);
        }

        [Test]
        public void Given_a_request_where_previous_assessment_required_was_false_and_current_assessment_required_is_true_When_GetIsAssessmentRequired_Then_should_return_true()
        {
            // Arrange
            var request = new AddHazardousSubstanceRequest
            {
                PreviousAssessmentRequired = false,
                AssessmentRequired = true
            };

            // Act
            bool result = request.IsAssessmentRequired();

            // Assert
            Assert.True(result);
        }

        [Test]
        public void Given_a_request_where_previous_assessment_required_was_true_and_current_assessment_required_is_false_When_GetIsAssessmentRequired_Then_should_return_false()
        {
            // Arrange
            var request = new AddHazardousSubstanceRequest
            {
                PreviousAssessmentRequired = true,
                AssessmentRequired = false
            };

            // Act
            bool result = request.IsAssessmentRequired();

            // Assert
            Assert.False(result);
        }

        [Test]
        public void Given_a_request_where_previous_assessment_required_was_true_and_current_assessment_required_has_not_got_value_When_GetIsAssessmentRequired_Then_should_return_false()
        {
            // Arrange
            var request = new AddHazardousSubstanceRequest
            {
                PreviousAssessmentRequired = true,
                AssessmentRequired = null
            };

            // Act
            bool result = request.IsAssessmentRequired();

            // Assert
            Assert.False(result);
        }

        [Test]
        public void Given_a_request_where_previous_assessment_required_was_false_and_current_assessment_required_is_false_When_GetIsAssessmentRequired_Then_should_return_false()
        {
            // Arrange
            var request = new AddHazardousSubstanceRequest
            {
                PreviousAssessmentRequired = false,
                AssessmentRequired = false
            };

            // Act
            bool result = request.IsAssessmentRequired();

            // Assert
            Assert.False(result);
        }

    }
}