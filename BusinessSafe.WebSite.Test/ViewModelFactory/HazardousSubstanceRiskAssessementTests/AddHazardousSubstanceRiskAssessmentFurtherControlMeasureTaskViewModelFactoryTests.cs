using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using Moq;
using NUnit.Framework;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.HazardousSubstanceRiskAssessementTests
{
    [TestFixture]
    [Category("Unit")]
    public class AddHazardousSubstanceRiskAssessmentFurtherControlMeasureTaskViewModelFactoryTests
    {
        protected Mock<IHazardousSubstanceRiskAssessmentService> _hazardousSubstanceRiskAssessmentService;
        protected HazardousSubstanceRiskAssessmentDto _riskAssessment;

        [SetUp]
        public void Setup()
        {
            _riskAssessment = new HazardousSubstanceRiskAssessmentDto()
            {
                RiskAssessor = new RiskAssessorDto()
            };

            _hazardousSubstanceRiskAssessmentService = new Mock<IHazardousSubstanceRiskAssessmentService>();

            _hazardousSubstanceRiskAssessmentService.Setup(x => x.GetRiskAssessment(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(() => _riskAssessment);

        }

        [Test]
        public void Given_correct_parameters_When_GetViewModel_called_Then_correct_view_model_is_returned()
        {
            var factory = GetTarget();

            var viewModel = factory
                .WithCompanyId(2378L)
                .WithRiskAssessmentId(66L)
                .GetViewModel();

            Assert.That(viewModel.CompanyId, Is.EqualTo(2378L));
            Assert.That(viewModel.RiskAssessmentId, Is.EqualTo(66L));
            Assert.That(viewModel.ExistingDocuments, Is.Not.Null);
        }

        [Test]
        public void Given_correct_parameters_When_GetViewModel_called_called_Then_DoNotSendTaskCompletedNotifications_set_from_riskassessor()
        {
            //given
            _riskAssessment.RiskAssessor.DoNotSendTaskCompletedNotifications = true;

            var factory = GetTarget();

            var viewModel = factory
                .WithCompanyId(2378L)
                .WithRiskAssessmentId(66L)
                .GetViewModel();

            Assert.IsTrue(viewModel.DoNotSendTaskCompletedNotification);
        }

        [Test]
        public void Given_correct_parameters_When_GetViewModel_called_called_Then_DoNotSendTaskOverdueNotifications_set_from_riskassessor()
        {
            //given
            _riskAssessment.RiskAssessor.DoNotSendTaskOverdueNotifications = true;
            var factory = GetTarget();

            var viewModel = factory
                .WithCompanyId(2378L)
                .WithRiskAssessmentId(66L)
                .GetViewModel();

            Assert.IsTrue(viewModel.DoNotSendTaskOverdueNotification);
        }

        private IAddHazardousSubstanceFurtherControlMeasureTaskViewModelFactory GetTarget()
        {
            return new AddHazardousSubstanceFurtherControlMeasureTaskViewModelFactory(_hazardousSubstanceRiskAssessmentService.Object);
        }
    }
}
