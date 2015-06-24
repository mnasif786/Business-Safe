using System.Collections.Generic;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories;
using Moq;
using NUnit.Framework;
using List = NHibernate.Mapping.List;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.FireRiskAssessmentTests.Factories
{
    [TestFixture]
    [Category("Unit")]
    public class AddFireRiskAssessmentFurtherControlMeasureTaskViewModelFactoryTests
    {
        protected Mock<IFireRiskAssessmentService> _fireRiskAssessmentService;
        protected FireRiskAssessmentDto _fireRiskAss;

        [SetUp]
        public void Setup()
        {
            _fireRiskAssessmentService = new Mock<IFireRiskAssessmentService>();
            _fireRiskAss = new FireRiskAssessmentDto() {RiskAssessor = new RiskAssessorDto()};
           
        }


        [Test]
        public void Given_correct_parameters_When_GetViewModel_called_Then_correct_view_model_is_returned()
        {
            _fireRiskAssessmentService.Setup(x => x.GetRiskAssessment(It.IsAny<long>(),It.IsAny<long>()))
                .Returns(() => _fireRiskAss);
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
        public void Given_correct_parameters_When_GetViewModel_called_Then_DoNotSendTaskCompletedNotifications_set_from_riskassessor()
        {
            //Given
            var companyId = 2378L;
            var riskAssId = 66L;
            var riskAssHazardId = 11239L;
            _fireRiskAss.Id = riskAssId;
            _fireRiskAss.Id = riskAssHazardId;
            _fireRiskAss.CompanyId = companyId;
            _fireRiskAss.RiskAssessor.DoNotSendTaskCompletedNotifications = true;

            _fireRiskAssessmentService.Setup(x => x.GetRiskAssessment(riskAssId, companyId))
                .Returns(() => _fireRiskAss);

            var factory = GetTarget();

            var viewModel = factory
                .WithCompanyId(companyId)
                .WithRiskAssessmentId(riskAssId)
                .GetViewModel();

            Assert.IsTrue(viewModel.DoNotSendTaskCompletedNotification);
        }

        [Test]
        public void Given_correct_parameters_When_GetViewModel_called_Then_DoNotSendTaskOverdueNotifications_set_from_riskassessor()
        {
            //Given
            var companyId = 2378L;
            var riskAssId = 66L;
            var riskAssHazardId = 11239L;
            _fireRiskAss.Id = riskAssId;
            _fireRiskAss.Id = riskAssHazardId;
            _fireRiskAss.CompanyId = companyId;
            _fireRiskAss.RiskAssessor.DoNotSendTaskOverdueNotifications = true;

            _fireRiskAssessmentService.Setup(x => x.GetRiskAssessment(riskAssId, companyId))
                .Returns(() => _fireRiskAss);

            var factory = GetTarget();

            var viewModel = factory
                .WithCompanyId(companyId)
                .WithRiskAssessmentId(riskAssId)
                .GetViewModel();

            Assert.IsTrue(viewModel.DoNotSendTaskOverdueNotification);
        }

        [Test]
        public void Given_a_significant_finding_has_bene_answered_no_When_GetViewModel_called_Then_answer_text_added_to_the_description_in_the_viewmodel()
        {
            //Given
            var expectedAnswerText = "I am not a number!!!";
            var significantFindingId = 67843L;
            var companyId = 2378L;
            var riskAssId = 66L;
            var riskAssHazardId = 11239L;
            _fireRiskAss.Id = riskAssId;
            _fireRiskAss.Id = riskAssHazardId;
            _fireRiskAss.CompanyId = companyId;
            _fireRiskAss.SignificantFindings = new List<SignificantFindingDto> () {new SignificantFindingDto() {Id = significantFindingId, FireAnswer = new FireAnswerDto(){AdditionalInfo = expectedAnswerText, YesNoNotApplicableResponse =  YesNoNotApplicableEnum.No }}};
            
            _fireRiskAssessmentService.Setup(x => x.GetRiskAssessment(riskAssId, companyId))
                .Returns(() => _fireRiskAss);

            var factory = GetTarget();

            var viewModel = factory
                .WithCompanyId(companyId)
                .WithRiskAssessmentId(riskAssId)
                .WithSignificantFindingId(significantFindingId)
                .GetViewModel();

            Assert.That(viewModel.SignificantFindingId, Is.EqualTo(significantFindingId));
            Assert.That(viewModel.Description, Is.EqualTo(expectedAnswerText));
        }

        private AddFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory GetTarget()
        {
            return new AddFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory(_fireRiskAssessmentService.Object);
        }
    }
}