using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class CanMarkAsLiveRiskAssessmentTests
    {
        [Test]
        public void Given_risk_assessment_with_reviews_Then_should_be_able_to_mark_as_live()
        {
            //Given
            var riskAssessment = HazardousSubstanceRiskAssessment.Create("", "", default(long), null, null);
            riskAssessment.AddReview( new RiskAssessmentReview() { Id = default(long), Deleted = false} );

            //When
            var result = riskAssessment.HasAnyReviews();

            Assert.True(result);
        }

        [Test]
        public void Given_risk_assessment_with_No_reviews_Then_should_not_be_able_to_mark_as_live()
        {
            //Given
            var riskAssessment = HazardousSubstanceRiskAssessment.Create("", "", default(long), null, null);
            //When
            var result = riskAssessment.HasAnyReviews();

            Assert.False(result);
        }
    }
}
