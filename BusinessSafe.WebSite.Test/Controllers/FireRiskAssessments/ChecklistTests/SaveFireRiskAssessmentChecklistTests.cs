using System;
using System.Collections.Generic;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.Request.FireRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.FireRiskAssessments.ChecklistTests
{
    [TestFixture]
    public class SaveFireRiskAssessmentChecklistTests
    {
        private Mock<IFireRiskAssessmentService> _fireRiskAssessmentService;
        private Mock<IFireRiskAssessmentChecklistService> _fireRiskAssessmentChecklistService;
        private IFireRiskAssessmentChecklistViewModelFactory _fireRiskAssessmentChecklistViewModelFactory;
        private FireRiskAssessmentChecklistViewModel _viewModel;
        private ChecklistController _controller;

        [TestFixtureSetUp]
        public void Setup()
        {
            _viewModel = new FireRiskAssessmentChecklistViewModel
            {
                CompanyId = 41212L,
                FireRiskAssessmentChecklistId = 121L,
                RiskAssessmentId = 142L,
                Sections = new List<SectionViewModel>
                {
                    new SectionViewModel
                    {
                         Questions = new List<QuestionViewModel>
                         {
                             new QuestionViewModel
                             {
                                 Id = 80L,
                                 Answer = new FireAnswerViewModel
                                 {
                                     YesNoNotApplicableResponse = YesNoNotApplicableEnum.Yes,
                                     AdditionalInfo = "Test Additional Info 1"
                                 }
                             },
                             new QuestionViewModel
                             {
                                 Id = 81L,
                                 Answer = new FireAnswerViewModel
                                 {
                                     YesNoNotApplicableResponse = YesNoNotApplicableEnum.No
                                 }
                             },
                             new QuestionViewModel
                             {
                                 Id = 82L,
                                 Answer = new FireAnswerViewModel
                                 {
                                     YesNoNotApplicableResponse = YesNoNotApplicableEnum.NotApplicable
                                 }
                             },
                             new QuestionViewModel
                             {
                                 Id = 83L,
                                 Answer = new FireAnswerViewModel
                                 {
                                     YesNoNotApplicableResponse = null
                                 }
                             },
                         }
                    }
                }
            };

            _fireRiskAssessmentService = new Mock<IFireRiskAssessmentService>();
            _fireRiskAssessmentChecklistService = new Mock<IFireRiskAssessmentChecklistService>();

            _fireRiskAssessmentChecklistViewModelFactory = new FireRiskAssessmentChecklistViewModelFactory(_fireRiskAssessmentService.Object, null);
            _controller = new ChecklistController(
                _fireRiskAssessmentChecklistViewModelFactory,
                _fireRiskAssessmentChecklistService.Object,
                null,
                null
                );

            _controller = TestControllerHelpers.AddUserToController(_controller);
            _controller.SaveChecklistOnlyForAuditing(_viewModel);
        }

        [Test]
        public void Given_valid_view_model_When_SaveFireRiskAssessmentCheklist_called_Then_service_is_called_correctly()
        {
            _fireRiskAssessmentChecklistService.Verify(x => x.Save(It.Is<SaveFireRiskAssessmentChecklistRequest>(
                y => y.CompanyId == _viewModel.CompanyId
                && y.FireRiskAssessmentChecklistId == _viewModel.FireRiskAssessmentChecklistId
                && y.Answers.Count == 4
                && y.CurrentUserId != default(Guid)
                && y.Answers[0].QuestionId == 80L
                && y.Answers[0].YesNoNotApplicableResponse.Value == YesNoNotApplicableEnum.Yes
                && y.Answers[0].AdditionalInfo == "Test Additional Info 1"
                && y.Answers[1].QuestionId == 81L
                && y.Answers[1].YesNoNotApplicableResponse.Value == YesNoNotApplicableEnum.No
                && y.Answers[1].AdditionalInfo == null
                && y.Answers[2].QuestionId == 82L
                && y.Answers[2].YesNoNotApplicableResponse.Value == YesNoNotApplicableEnum.NotApplicable
                && y.Answers[2].AdditionalInfo == null
                && y.Answers[3].QuestionId == 83L
                && y.Answers[3].YesNoNotApplicableResponse == null
                && y.Answers[3].AdditionalInfo == null
                )));
        }
    }
}
