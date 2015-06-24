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
    public class NewTests
    {
        private const long CompanyId = 1234;
        private const long RiskAssessmentId = 5678;
        private long SignificantFindingId = 500;

        private Mock<IAddFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory> _addViewModelFactory;
        private Mock<IBusinessSafeSessionManager> _businessSafeSessionManager;

        [SetUp]
        public void Setup()
        {
            _addViewModelFactory =
                new Mock<IAddFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory>();

            _addViewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_addViewModelFactory.Object);

            _addViewModelFactory
                .Setup(x => x.WithRiskAssessmentId(It.IsAny<long>()))
                .Returns(_addViewModelFactory.Object);

            _addViewModelFactory
               .Setup(x => x.WithSignificantFindingId(It.IsAny<long>()))
               .Returns(_addViewModelFactory.Object);

            _addViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new AddEditFireRiskAssessmentFurtherControlMeasureTaskViewModel());

            _businessSafeSessionManager = new Mock<IBusinessSafeSessionManager>();
        }

        [Test]
        public void Given_get_When_New_FurtherControlMeasureTask_Request_Then_returns_Partial_View()
        {
            // Given
            var controller = GetTarget();

            // When
            var result = controller.New(CompanyId, RiskAssessmentId, SignificantFindingId);

            // Then
            Assert.That(result, Is.InstanceOf<PartialViewResult>());
            Assert.That(result.ViewName, Is.EqualTo("_AddFireRiskAssessmentFurtherControlMeasureTask"));
        }

        [Test]
        public void Given_get_When_New_FurtherControlMeasureTask_Request_Then_returns_correct_viewmodel()
        {
            // Given
            var controller = GetTarget();

            // When
            var result = controller.New(CompanyId, RiskAssessmentId, SignificantFindingId);

            // Then
            Assert.That(result.Model, Is.InstanceOf<AddEditFireRiskAssessmentFurtherControlMeasureTaskViewModel>());
        }

        [Test]
        public void Given_get_When_New_FurtherControlMeasureTask_Request_Then_Correct_ViewModelFactory_Is_Called()
        {
            // Given
            var controller = GetTarget();

            // When
            var result = controller.New(CompanyId, RiskAssessmentId, SignificantFindingId);
            var model = result.Model as AddEditFireRiskAssessmentFurtherControlMeasureTaskViewModel;

            // Then
            _addViewModelFactory.VerifyAll();
        }

        private FurtherControlMeasureTaskController GetTarget()
        {
            return new FurtherControlMeasureTaskController(_addViewModelFactory.Object, null, null,null, null,null, _businessSafeSessionManager.Object, null);
        }
    }
}