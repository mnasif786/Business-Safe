using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.FireRiskAssessments.ChecklistTests
{
    [TestFixture]
    public class IndexTests
    {
        private Mock<IFireRiskAssessmentService> _fireRiskAssessmentService;
        private Mock<IFireRiskAssessmentChecklistService> _fireRiskAssessmentChecklistService;
        private IFireRiskAssessmentChecklistViewModelFactory _fireRiskAssessmentChecklistViewModelFactory;
        private long _fireRiskAssessmentId;
        private long _companyId;
        private long _fireRiskAssessmentChecklistId = 879L;
        private FireRiskAssessmentDto _fireRiskAssessment;

        [SetUp]
        public void Setup()
        {
            _fireRiskAssessmentId = 1832L;
            _companyId = 526L;

            _fireRiskAssessment = new FireRiskAssessmentDto
            {
                Id = _fireRiskAssessmentId,
                CompanyId = _companyId,
                LatestFireRiskAssessmentChecklist = new FireRiskAssessmentChecklistDto
                {
                    Id = _fireRiskAssessmentChecklistId,
                    Checklist = new ChecklistDto
                    {
                        Sections = new List<SectionDto>
                        {
                            new SectionDto
                            {
                                Id = 1L,
                                ShortTitle = "Test Section 1",
                                Questions = new List<QuestionDto>
                                {
                                    new QuestionDto
                                    {
                                        Id = 1L,
                                        ListOrder = 1,
                                        Text = "Test QuestionText 1",
                                        Information = "Test Information 1",
                                    },
                                    new QuestionDto
                                    {
                                        Id = 2L,
                                        ListOrder = 2,
                                        Text = "Test QuestionText 2",
                                        Information = "Test Information 2",
                                    }  
                                }
                            },
                            new SectionDto
                            {
                                Id = 2L,
                                ShortTitle = "Test Section 2",
                                Questions = new List<QuestionDto>
                                {
                                    new QuestionDto
                                    {
                                        Id = 3L,
                                        ListOrder = 3,
                                        Text = "Test QuestionText 3",
                                        Information = "Test Information 3",
                                    },
                                    new QuestionDto
                                    {
                                        Id = 4L,
                                        ListOrder = 4,
                                        Text = "Test QuestionText 4",
                                        Information = "Test Information 4",
                                    }  
                                }
                            } 
                        }
                    },
                    Answers = new List<FireAnswerDto>
                    {
                        new FireAnswerDto
                        {
                            Question = new QuestionDto
                            {
                                Id = 1L
                            },
                            YesNoNotApplicableResponse = YesNoNotApplicableEnum.Yes,
                            AdditionalInfo = "Test Additional Info 1"
                        },
                        new FireAnswerDto
                        {
                            Question = new QuestionDto
                            {
                                Id = 2L
                            },
                            YesNoNotApplicableResponse = YesNoNotApplicableEnum.No
                        },
                        new FireAnswerDto
                        {
                            Question = new QuestionDto
                            {
                                Id = 3L
                            }
                        } 
                    }
                }                                                              
            };

            _fireRiskAssessmentService = new Mock<IFireRiskAssessmentService>();
            _fireRiskAssessmentChecklistService = new Mock<IFireRiskAssessmentChecklistService>();

            _fireRiskAssessmentService
                .Setup(x => x.GetWithLatestFireRiskAssessmentChecklist(_fireRiskAssessmentId, _companyId))
                .Returns(_fireRiskAssessment);

            _fireRiskAssessmentChecklistViewModelFactory = new FireRiskAssessmentChecklistViewModelFactory(_fireRiskAssessmentService.Object, null);
        }

        [Test]
        public void When_Index_called_Then_view_model_contains_correct_risk_assessment_id()
        {
            Assert.That(GetViewModel().RiskAssessmentId, Is.EqualTo(_fireRiskAssessmentId));
        }

        [Test]
        public void When_Index_called_Then_view_model_contains_correct_company_id()
        {
            Assert.That(GetViewModel().CompanyId, Is.EqualTo(_companyId));
        }

        [Test]
        public void When_Index_called_then_view_model_conatins_correct_number_of_sections()
        {
            Assert.That(GetViewModel().Sections.Count, Is.EqualTo(2));
        }

        [Test]
        public void When_Index_called_then_section_1_has_correct_control_id()
        {
            Assert.That(GetViewModel().Sections[0].ControlId, Is.EqualTo("Section_1"));
        }

        [Test]
        public void When_Index_called_then_section_1_has_correct_title()
        {
            Assert.That(GetViewModel().Sections[0].Title, Is.EqualTo("Test Section 1"));
        }

        [Test]
        public void When_Index_called_then_section_1_is_active()
        {
            Assert.That(GetViewModel().Sections[0].Active);
        }

        [Test]
        public void When_Index_called_then_section_2_is_not_active()
        {
            Assert.That(GetViewModel().Sections[1].Active, Is.EqualTo(false));
        }

        [Test]
        public void When_Index_called_then_section_1_has_correct_number_of_questions()
        {
            Assert.That(GetViewModel().Sections[0].Questions.Count, Is.EqualTo(2));
        }

        [Test]
        public void When_Index_called_then_question_1_has_correct_id()
        {
            Assert.That(GetViewModel().Sections[0].Questions[0].Id, Is.EqualTo(1L));
        }

        [Test]
        public void When_Index_called_then_question_1_has_correct_test()
        {
            Assert.That(GetViewModel().Sections[0].Questions[0].Text, Is.EqualTo("Test QuestionText 1"));
        }

        [Test]
        public void When_Index_called_then_question_1_has_correct_information()
        {
            Assert.That(GetViewModel().Sections[0].Questions[0].Information, Is.EqualTo("Test Information 1"));
        }

        [Test]
        public void When_Index_called_then_question_1_answer_has_correct_yes_no_na_response()
        {
            Assert.That(GetViewModel().Sections[0].Questions[0].Answer.YesNoNotApplicableResponse, Is.EqualTo(YesNoNotApplicableEnum.Yes));
        }

        [Test]
        public void When_Index_called_then_question_1_answer_has_correct_additional_info()
        {
            Assert.That(GetViewModel().Sections[0].Questions[0].Answer.AdditionalInfo, Is.EqualTo("Test Additional Info 1"));
        }

        [Test]
        public void When_Index_called_then_question_2_answer_has_correct_yes_no_na_response()
        {
            Assert.That(GetViewModel().Sections[0].Questions[1].Answer.YesNoNotApplicableResponse, Is.EqualTo(YesNoNotApplicableEnum.No));
        }

        [Test]
        public void When_Index_called_then_question_2_answer_has_correct_additional_info()
        {
            Assert.That(GetViewModel().Sections[1].Questions[0].Answer.AdditionalInfo, Is.Null);
        }

        [Test]
        public void When_Index_called_then_question_3_answer_has_correct_yes_no_na_response()
        {
            Assert.That(GetViewModel().Sections[1].Questions[0].Answer.YesNoNotApplicableResponse, Is.Null);
        }

        [Test]
        public void When_Index_called_then_question_4_answer_has_correct_yes_no_na_response()
        {
            Assert.That(GetViewModel().Sections[1].Questions[1].Answer, Is.Null);
        }

        private FireRiskAssessmentChecklistViewModel GetViewModel()
        {
            return (GetTarget().Index(_companyId, _fireRiskAssessmentId) as ViewResult).Model as FireRiskAssessmentChecklistViewModel;
        }

        private ChecklistController GetTarget()
        {
            var target = new ChecklistController(
                _fireRiskAssessmentChecklistViewModelFactory,
                _fireRiskAssessmentChecklistService.Object,
                null,
                null);

            return TestControllerHelpers.AddUserToController(target);
        }
    }
}
