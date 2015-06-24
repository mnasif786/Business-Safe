using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Factories;
using BusinessSafe.WebSite.ViewModels;
using NUnit.Framework;
using BusinessSafe.WebSite.Factories;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.GeneralRiskAssessment.Factories
{
    [TestFixture]
    [Category("Unit")]
    public class CompleteRiskAssessmentReviewViewModelFactoryTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Given_the_factory_is_called_When_GetViewModel_is_called_Then_a_CompleteReviewViewModel_is_returned()
        {
            //Given
            var target = GetFactory();

            //When
            var result = target.GetViewModel();

            //Then
            Assert.That(result, Is.InstanceOf<CompleteReviewViewModel>());
        }

        [Test]
        public void Given_the_factory_is_called_When_WithCompanyId_has_called_before_GetViewModel_Then_the_companyId_is_applied_to_the_returned_CompleteReviewViewModel()
        {
            //Given
            var target = GetFactory();
            const long passedCompanyId = 1234;

            //When
            var result = target.WithCompanyId(passedCompanyId).GetViewModel();

            //Then
            Assert.That(result.CompanyId, Is.EqualTo(passedCompanyId));
        }

        [Test]
        public void Given_the_factory_is_called_When_WithReviewId_has_called_before_GetViewModel_Then_the_reviewId_is_applied_to_the_returned_CompleteReviewViewModel()
        {
            //Given
            var target = GetFactory();
            const long passedReviewId = 5678;

            //When
            var result = target.WithCompanyId(passedReviewId).GetViewModel();

            //Then
            Assert.That(result.CompanyId, Is.EqualTo(passedReviewId));
        }

        private CompleteReviewViewModelFactory GetFactory()
        {
            return new CompleteReviewViewModelFactory();
        }
    }
}