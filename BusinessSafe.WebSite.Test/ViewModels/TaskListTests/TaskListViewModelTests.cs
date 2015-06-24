using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.WebSite.Areas.TaskList.ViewModels;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModels.TaskListTests
{
    [TestFixture]
    public class TaskListViewModelTests
    {
        [Test]
        public void Given_showing_deleted_tasks_When_IsShowOutsandingVisible_Then_is_true()
        {
            // Given
            var target = new TaskListViewModel
            {
                IsShowDeleted = true
            };

            // When
            var result = target.IsShowOutsandingVisible();

            // Then
            Assert.IsTrue(result);
        }

        [Test]
        public void Given_not_showing_deleted_tasks_When_IsShowOutsandingVisible_Then_is_false()
        {
            // Given
            var target = new TaskListViewModel
            {
                IsShowDeleted = false
            };

            // When
            var result = target.IsShowOutsandingVisible();

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_showing_completed_tasks_When_IsShowOutsandingVisible_Then_is_true()
        {
            // Given
            var target = new TaskListViewModel
            {
                IsShowCompleted = true
            };

            // When
            var result = target.IsShowOutsandingVisible();

            // Then
            Assert.IsTrue(result);
        }

        [Test]
        public void Given_showing_deleted_tasks_When_GetAdditionalTitle_Then_returns_deleted()
        {
            // Given
            var target = new TaskListViewModel
            {
                IsShowDeleted = true
            };

            // When
            var result = target.GetAdditionalTitle();

            // Then
            Assert.That(result.ToString(), Is.EqualTo("<strong>Deleted</strong>"));
        }

        [Test]
        public void Given_showing_completed_tasks_When_GetAdditionalTitle_Then_returns_completed()
        {
            // Given
            var target = new TaskListViewModel
            {
                IsShowCompleted = true
            };

            // When
            var result = target.GetAdditionalTitle();

            // Then
            Assert.That(result.ToString(), Is.EqualTo("<strong>Completed</strong>"));
        }

        [Test]
        public void Given_showing_bulk_reassign_tasks_When_GetAdditionalTitle_Then_returns_bulk_reassign()
        {
            // Given
            var target = new TaskListViewModel
            {
                IsBulkReassign = true
            };

            // When
            var result = target.GetAdditionalTitle();

            // Then
            Assert.That(result.ToString(), Is.EqualTo("<strong>Bulk Reassign</strong>"));
        }

        [Test]
        public void Given_showing_outstanding_tasks_When_GetAdditionalTitle_Then_returns_outstanding()
        {
            // Given
            var target = new TaskListViewModel
            {
                IsShowDeleted = false,
                IsShowCompleted = false,
                IsBulkReassign = false
            };

            // When
            var result = target.GetAdditionalTitle();

            // Then
            Assert.That(result.ToString(), Is.EqualTo("<strong>Outstanding</strong>"));
        }
    }
}
