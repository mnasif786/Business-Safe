using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using BusinessSafe.WebSite.ViewModels;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModels.FireRiskAssessmentTests
{
    [TestFixture]
    public class SearchRiskAssessmentsViewModelTests
    {
        [Test]
        public void Given_showing_deleted_tasks_When_IsShowOutsandingVisible_Then_is_true()
        {
            // Given
            var target = new SearchRiskAssessmentsViewModel
            {
                IsShowDeleted = true
            };

            // When
            var result = target.IsShowOpenVisible();

            // Then
            Assert.IsTrue(result);
        }

        [Test]
        public void Given_not_showing_deleted_tasks_When_IsShowOutsandingVisible_Then_is_false()
        {
            // Given
            var target = new SearchRiskAssessmentsViewModel
            {
                IsShowDeleted = false
            };

            // When
            var result = target.IsShowOpenVisible();

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_showing_archived_tasks_When_IsShowOutsandingVisible_Then_is_true()
        {
            // Given
            var target = new SearchRiskAssessmentsViewModel
            {
                IsShowArchived = true
            };

            // When
            var result = target.IsShowOpenVisible();

            // Then
            Assert.IsTrue(result);
        }

        [Test]
        public void Given_not_showing_archived_tasks_When_IsShowOutsandingVisible_Then_is_true()
        {
            // Given
            var target = new SearchRiskAssessmentsViewModel
            {
                IsShowArchived = false
            };

            // When
            var result = target.IsShowOpenVisible();

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_not_showing_archived_and_not_showing_deleted_tasks_When_IsShowOutsandingVisible_Then_is_true()
        {
            // Given
            var target = new SearchRiskAssessmentsViewModel
            {
                IsShowDeleted = false,
                IsShowArchived = false
            };

            // When
            var result = target.IsShowOpenVisible();

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_showing_deleted_When_GetAdditionalTitle_Then_return_Deleted()
        {
            // Given
            var target = new SearchRiskAssessmentsViewModel
            {
                IsShowDeleted = true
            };

            // When
            var result = target.GetAdditionalTitle();

            // Then
            Assert.That(result.ToString(), Is.EqualTo("<strong>Deleted</strong>"));
        }

        [Test]
        public void Given_showing_archived_When_GetAdditionalTitle_Then_return_Archived()
        {
            // Given
            var target = new SearchRiskAssessmentsViewModel
            {
                IsShowArchived = true
            };

            // When
            var result = target.GetAdditionalTitle();

            // Then
            Assert.That(result.ToString(), Is.EqualTo("<strong>Archived</strong>"));
        }

        [Test]
        public void Given_not_showing_archived_and_not_showing_deleted_When_GetAdditionalTitle_Then_return_Open()
        {
            // Given
            var target = new SearchRiskAssessmentsViewModel
            {
                IsShowArchived = false,
                IsShowDeleted = false
            };

            // When
            var result = target.GetAdditionalTitle();

            // Then
            Assert.That(result.ToString(), Is.EqualTo("<strong>Open</strong>"));
        }
    }
}
