using System;
using System.Web.Mvc;

using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.ViewModels;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.RiskAssessmentReview
{
    [TestFixture]
    public class EditTests : BaseRiskAssessmentReviewTest
    {
        [SetUp]
        public new void SetUp()
        {
            _riskAssessmentService = new Mock<IRiskAssessmentService>();
            _employeeService = new Mock<IEmployeeService>();
            _riskAssessmentReviewService = new Mock<IRiskAssessmentReviewService>();
            _riskAssessmentReviewService 
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(new RiskAssessmentReviewDto()
                         {
                             RiskAssessment = new RiskAssessmentDto() { Id = 5678, CompanyId = 1234 },
                             ReviewAssignedTo = new EmployeeDto() { Id = Guid.NewGuid() },
                             Id = 5678,
                             CompletionDueDate = DateTime.Now
                         });

            _reviewViewFactory = new Mock<ICompleteReviewViewModelFactory>();
            _reviewViewFactory.Setup(x => x.WithCompanyId(It.IsAny<long>())).Returns(_reviewViewFactory.Object);
            _reviewViewFactory.Setup(x => x.WithReviewId(It.IsAny<long>())).Returns(_reviewViewFactory.Object);
            _reviewViewFactory.Setup(x => x.GetViewModel()).Returns(new CompleteReviewViewModel());
        }

        [Test]
        public void Given_that_edit_is_called_When_required_fields_are_supplied_Then_partial_view_is_returned()
        {
            // Given
            const long passedCompanyId = 1234;
            const long passedRiskAssessmentReviewId = 5678;

            var target = GetTarget();

            // When
            var result = target.Edit(1234, 5678);
            var model = result.Model as AddEditRiskAssessmentReviewViewModel;

            // Then
            Assert.That(result, Is.InstanceOf<PartialViewResult>());
            Assert.That(passedCompanyId, Is.EqualTo(model.CompanyId));
            Assert.That(passedRiskAssessmentReviewId, Is.EqualTo(model.RiskAssessmentReviewId));
        }
    }
}