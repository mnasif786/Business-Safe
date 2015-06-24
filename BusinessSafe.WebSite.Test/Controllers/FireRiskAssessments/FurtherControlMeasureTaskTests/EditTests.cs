using System.Web.Mvc;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using Moq;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Tests.Controllers.FireRiskAssessments.FurtherControlMeasureTaskTests
{
    [TestFixture]
    [Category("Unit")]
    public class EditTests
    {
        private long furtherControlMeasureTaskId = 888L;
        private Mock<IEditFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory> _viewModelFactory;
        private Mock<IBusinessSafeSessionManager> _businessSafeSessionManager;

        [SetUp]
        public void Setup()
        {
            _viewModelFactory =
                new Mock<IEditFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory>();

            _viewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_viewModelFactory.Object);


            _viewModelFactory
                .Setup(x => x.WithFurtherControlMeasureTaskId(furtherControlMeasureTaskId))
                .Returns(_viewModelFactory.Object);

            _viewModelFactory
                .Setup(x => x.WithCanDeleteDocuments(true))
                .Returns(_viewModelFactory.Object);

            _viewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new AddEditFireRiskAssessmentFurtherControlMeasureTaskViewModel());

            _businessSafeSessionManager = new Mock<IBusinessSafeSessionManager>();
        }

        [Test]
        public void Given_get_When_Edit_FurtherControlMeasureTask_Request_Then_returns_Partial_View()
        {
            // Given
            var controller = GetTarget();

            // When
            var result = controller.Edit(furtherControlMeasureTaskId);

            // Then
            Assert.That(result, Is.InstanceOf<PartialViewResult>());
            Assert.That(result.ViewName, Is.EqualTo("_EditFurtherControlMeasureTask"));
        }

        [Test]
        public void Given_get_When_New_FurtherControlMeasureTask_Request_Then_returns_correct_viewmodel()
        {
            // Given
            var controller = GetTarget();

            // When
            var result = controller.Edit(furtherControlMeasureTaskId);

            // Then
            Assert.That(result.Model, Is.InstanceOf<AddEditFireRiskAssessmentFurtherControlMeasureTaskViewModel>());
        }

        [Test]
        public void Given_get_When_New_FurtherControlMeasureTask_Request_Then_Correct_ViewModelFactory_Is_Called()
        {
            // Given
            var controller = GetTarget();

            // When
            var result = controller.Edit(furtherControlMeasureTaskId);
            var model = result.Model as AddEditFireRiskAssessmentFurtherControlMeasureTaskViewModel;

            // Then
            _viewModelFactory.VerifyAll();
        }

        private FurtherControlMeasureTaskController GetTarget()
        {
            var target = new FurtherControlMeasureTaskController(null, null, _viewModelFactory.Object,null, null,null, _businessSafeSessionManager.Object, null);

            return TestControllerHelpers.AddUserToController(target);
        }
    }
}