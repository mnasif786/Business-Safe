using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModels.GeneralRiskAssessments
{
    [TestFixture]
    public class PersonalRiskAssessmentSummaryViewModelTests
    {
      
        [Test]
        public void Given_not_deleted_and_not_have_permissioins_When_IsEditEnabled_Then_returns_false()
        {
            // Given
            var user = new Mock<IPrincipal>();
            user
                .Setup(x => x.IsInRole(Permissions.EditGeneralandHazardousSubstancesRiskAssessments.ToString()))
                .Returns(false);
            
                var viewmodel = new PersonalRiskAssessmentSummaryViewModel(
                    10L,
                    55881L,
                    "Some Title",
                    "Some Reference",
                    DateTime.Now.AddDays(-1),
                    RiskAssessmentStatus.Draft,
                    false,
                    false
                    );

            // When
            var result = viewmodel.IsEditEnabled(user.Object);

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_is_deleted_and_not_have_permissioins_When_IsEditEnabled_Then_returns_false()
        {
            // Given
            var user = new Mock<IPrincipal>();
            user
                .Setup(x => x.IsInRole(Permissions.EditGeneralandHazardousSubstancesRiskAssessments.ToString()))
                .Returns(false);

            var viewmodel = new PersonalRiskAssessmentSummaryViewModel(
                10L,
                55881L,
                "Some Title",
                "Some Reference",
                DateTime.Now.AddDays(-1),
                RiskAssessmentStatus.Draft,
                true,
                false
                );

            // When
            var result = viewmodel.IsEditEnabled(user.Object);

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_not_deleted_and_have_permissioins_When_IsEditEnabled_Then_returns_true()
        {
            // Given
            var user = new Mock<IPrincipal>();
            user
                .Setup(x => x.IsInRole(Permissions.EditGeneralandHazardousSubstancesRiskAssessments.ToString()))
                .Returns(true);

            var viewmodel = new PersonalRiskAssessmentSummaryViewModel(
                10L,
                55881L,
                "Some Title",
                "Some Reference",
                DateTime.Now.AddDays(-1),
                RiskAssessmentStatus.Draft,
                false,
                false
                );

            // When
            var result = viewmodel.IsEditEnabled(user.Object);

            // Then
            Assert.IsTrue(result);
        }

        [Test]
        public void Given_is_deleted_and_have_permissioins_When_IsEditEnabled_Then_returns_false()
        {
            // Given
            var user = new Mock<IPrincipal>();
            user
                .Setup(x => x.IsInRole(Permissions.EditGeneralandHazardousSubstancesRiskAssessments.ToString()))
                .Returns(true);

            var viewmodel = new PersonalRiskAssessmentSummaryViewModel(
                10L,
                55881L,
                "Some Title",
                "Some Reference",
                DateTime.Now.AddDays(-1),
                RiskAssessmentStatus.Draft,
                true,
                false
                );

            // When
            var result = viewmodel.IsEditEnabled(user.Object);

            // Then
            Assert.IsFalse(result);
        }
    }
}
