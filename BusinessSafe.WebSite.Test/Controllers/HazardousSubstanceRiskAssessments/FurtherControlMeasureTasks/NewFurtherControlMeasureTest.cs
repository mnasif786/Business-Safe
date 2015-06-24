using System.Web.Mvc;
using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.Request.HazardousSubstanceRiskAssessments;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;
using Moq;
using NServiceBus;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Tests.Controllers.HazardousSubstanceRiskAssessments.FurtherControlMeasureTasks
{
    [TestFixture]
    [Category("Unit")]
    public class NewFurtherControlMeasureTest
    {
        private const long CompanyId = 1234;
        private const long RiskAssessmentId = 5678;
        private Mock<IHazardousSubstanceRiskAssessmentFurtherControlMeasureTaskService> _furtherControlMeasureTaskService;

        private Mock<IEditHazardousSubstanceFurtherControlMeasureTaskViewModelFactory>
            _addEditFurtherControlMeasureTaskViewModelFactory;

        private Mock<IAddHazardousSubstanceFurtherControlMeasureTaskViewModelFactory>
             _addHazardousSubstanceFurtherControlMeasureTaskViewModelFactory;

        private Mock<IBusinessSafeSessionManager> _businessSafeSessionManager;
        private Mock<IBus> _bus;

        [SetUp]
        public void Setup()
        {
            _furtherControlMeasureTaskService = new Mock<IHazardousSubstanceRiskAssessmentFurtherControlMeasureTaskService>();
            _furtherControlMeasureTaskService
                .Setup(x => x.AddFurtherControlMeasureTask(It.IsAny<SaveFurtherControlMeasureTaskRequest>()));

            _addEditFurtherControlMeasureTaskViewModelFactory = new Mock<IEditHazardousSubstanceFurtherControlMeasureTaskViewModelFactory>();

            _addHazardousSubstanceFurtherControlMeasureTaskViewModelFactory =
                new Mock<IAddHazardousSubstanceFurtherControlMeasureTaskViewModelFactory>();

            _addHazardousSubstanceFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_addHazardousSubstanceFurtherControlMeasureTaskViewModelFactory.Object);

            _addHazardousSubstanceFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.WithRiskAssessmentId(It.IsAny<long>()))
                .Returns(_addHazardousSubstanceFurtherControlMeasureTaskViewModelFactory.Object);

            _addHazardousSubstanceFurtherControlMeasureTaskViewModelFactory
                 .Setup(x => x.WithCanDeleteDocuments(It.IsAny<bool>()))
                 .Returns(_addHazardousSubstanceFurtherControlMeasureTaskViewModelFactory.Object);

            _addHazardousSubstanceFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new AddHazardousSubstanceFurtherControlMeasureTaskViewModel());

            _businessSafeSessionManager = new Mock<IBusinessSafeSessionManager>();
            _bus = new Mock<IBus>();
        }

        [Test]
        public void Given_get_When_New_FurtherControlMeasureTask_Request_Then_returns_Partial_View()
        {
            // Given
            var controller = GetTarget();

            // When
            var result = controller.New(CompanyId, RiskAssessmentId);

            // Then
            Assert.That(result, Is.InstanceOf<PartialViewResult>());
        }

        [Test]
        public void Given_get_When_New_FurtherControlMeasureTask_Request_Then_returns_correct_viewmodel()
        {
            // Given
            var controller = GetTarget();

            // When
            var result = controller.New(CompanyId, RiskAssessmentId);

            // Then
            Assert.That(result.Model, Is.InstanceOf<AddHazardousSubstanceFurtherControlMeasureTaskViewModel>());
        }

        [Test]
        public void Given_get_When_New_FurtherControlMeasureTask_Request_Then_Correct_ViewModelFactory_Is_Called()
        {
            // Given
            var controller = GetTarget();

            // When
            var result = controller.New(CompanyId, RiskAssessmentId);
            var model = result.Model as AddHazardousSubstanceFurtherControlMeasureTaskViewModel;

            // Then
            _addEditFurtherControlMeasureTaskViewModelFactory.VerifyAll();
        }

        private FurtherControlMeasureTaskController GetTarget()
        {
            return new FurtherControlMeasureTaskController(
                _furtherControlMeasureTaskService.Object,
                _addHazardousSubstanceFurtherControlMeasureTaskViewModelFactory.Object, null, null, null, null, _businessSafeSessionManager.Object, _bus.Object);
        }    
    }
}