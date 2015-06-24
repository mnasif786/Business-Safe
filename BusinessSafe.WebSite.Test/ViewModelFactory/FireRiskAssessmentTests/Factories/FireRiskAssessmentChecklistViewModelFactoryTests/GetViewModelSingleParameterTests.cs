using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request.FireRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;

using FluentValidation.Results;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.FireRiskAssessmentTests.Factories.FireRiskAssessmentChecklistViewModelFactoryTests
{
    [TestFixture]
    public class GetViewModelSingleParameterTests
    {
        private Mock<IFireRiskAssessmentService> _riskAssessmentService;
        private Mock<IFireRiskAssessmentChecklistService> _fireRiskAssessmentChecklistService;
        private FireRiskAssessmentChecklistViewModelFactory _target;

        private long _riskAssessmentId;
        private long _companyId;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentService = new Mock<IFireRiskAssessmentService>();
            _fireRiskAssessmentChecklistService = new Mock<IFireRiskAssessmentChecklistService>();
            _target = new FireRiskAssessmentChecklistViewModelFactory(_riskAssessmentService.Object, _fireRiskAssessmentChecklistService.Object);

            _riskAssessmentId = 123L;
            _companyId = 456L;

            var validationResult = new ValidationResult
                (
                   new List<ValidationFailure>
                       {
                           new ValidationFailure
                               (
                                "10",
                                "error 10"
                               ),
                               new ValidationFailure
                               (
                                "20",
                                "error 20"
                               ),
                               new ValidationFailure
                               (
                                "30",
                                "error 30"
                               )
                       }
                );

            _fireRiskAssessmentChecklistService
                .Setup(x => x.ValidateFireRiskAssessmentChecklist(It.IsAny<ValidateCompleteFireRiskAssessmentChecklistRequest>()))
                .Returns(validationResult);
        }

        [Test]
        public void When_GetViewModel_Then_should_call_FRAChecklistService_GetWithLatestFireRiskAssessmentChecklist()
        {
            // Given
            _riskAssessmentService
                .Setup(x => x.GetWithLatestFireRiskAssessmentChecklist(_riskAssessmentId, _companyId))
                .Returns(new FireRiskAssessmentDto());

            // When
            _target
                .WithRiskAssessmentId(_riskAssessmentId)
                .WithCompanyId(_companyId)
                .GetViewModel();

            // Then
            _riskAssessmentService
                .Verify(x => x.GetWithLatestFireRiskAssessmentChecklist(_riskAssessmentId, _companyId));
        }

        [Test]
        public void When_GetViewModel_Then_returned_model_has_requested_riskAssessmentId_set()
        {
            // Given
            _riskAssessmentService
                .Setup(x => x.GetWithLatestFireRiskAssessmentChecklist(_riskAssessmentId, _companyId))
                .Returns(new FireRiskAssessmentDto());

            // When
            var result = _target
                .WithRiskAssessmentId(_riskAssessmentId)
                .WithCompanyId(_companyId)
                .GetViewModel();

            // Then
            Assert.That(result.RiskAssessmentId, Is.EqualTo(_riskAssessmentId));
        }

        [Test]
        public void When_GetViewModel_Then_returned_model_has_requested_checklistId_set()
        {
            // Given
            _riskAssessmentService
                .Setup(x => x.GetWithLatestFireRiskAssessmentChecklist(_riskAssessmentId, _companyId))
                .Returns(new FireRiskAssessmentDto());

            // When
            var result = _target
                .WithRiskAssessmentId(_riskAssessmentId)
                .WithCompanyId(_companyId)
                .GetViewModel();

            // Then
            Assert.That(result.CompanyId, Is.EqualTo(_companyId));
        }

        [Test]
        public void When_GetViewModel_Then_returned_model_has_requested_Sections_set()
        {
            // Given
            _riskAssessmentService
                .Setup(x => x.GetWithLatestFireRiskAssessmentChecklist(_riskAssessmentId, _companyId))
                .Returns(new FireRiskAssessmentDto());

            // When
            var result = _target
                .WithRiskAssessmentId(_riskAssessmentId)
                .WithCompanyId(_companyId)
                .GetViewModel();

            // Then
            Assert.IsInstanceOf<List<SectionViewModel>>(result.Sections);
            Assert.That(result.Sections.Count, Is.EqualTo(0));
        }

        [Test]
        public void Given_a_checklist_exists_When_GetViewModel_Then_view_model_lastest_checklist_id_is_set()
        {
            // Given
            var fireRiskAssessmentDto = GetFireRiskAssessmentDto();

            _riskAssessmentService
                .Setup(x => x.GetWithLatestFireRiskAssessmentChecklist(_riskAssessmentId, _companyId))
                .Returns(fireRiskAssessmentDto);

            // When
            var result = _target
                .WithRiskAssessmentId(_riskAssessmentId)
                .WithCompanyId(_companyId)
                .GetViewModel();

            // Then
            Assert.That(result.FireRiskAssessmentChecklistId, Is.EqualTo(fireRiskAssessmentDto.LatestFireRiskAssessmentChecklist.Id));
        }

        [Test]
        public void Given_a_checklist_with_sections_When_GetViewModel_Then_view_model_has_sections_populated()
        {
            // Given
            var fireRiskAssessmentDto = GetFireRiskAssessmentDto();
            var sections = fireRiskAssessmentDto.LatestFireRiskAssessmentChecklist.Checklist.Sections;

            _riskAssessmentService
                .Setup(x => x.GetWithLatestFireRiskAssessmentChecklist(_riskAssessmentId, _companyId))
                .Returns(fireRiskAssessmentDto);

            // When
            var result = _target
                .WithRiskAssessmentId(_riskAssessmentId)
                .WithCompanyId(_companyId)
                .GetViewModel();

            // Then
            for (var i = 0; i < sections.Count(); i++)
            {
                Assert.That(sections.ElementAt(i).ShortTitle, Is.EqualTo(result.Sections.ElementAt(i).Title));
                Assert.That(string.Format("Section_{0}", sections.ElementAt(i).Id), Is.EqualTo(result.Sections.ElementAt(i).ControlId));
            }
        }

        [Test]
        public void Given_a_checklist_with_sections_When_GetViewModel_Then_view_model_has_sections_with_questions_populated()
        {
            // Given
            var fireRiskAssessmentDto = GetFireRiskAssessmentDto();
            var sections = fireRiskAssessmentDto.LatestFireRiskAssessmentChecklist.Checklist.Sections;


            _riskAssessmentService
                .Setup(x => x.GetWithLatestFireRiskAssessmentChecklist(_riskAssessmentId, _companyId))
                .Returns(fireRiskAssessmentDto);

            // When
            var result = _target
                .WithRiskAssessmentId(_riskAssessmentId)
                .WithCompanyId(_companyId)
                .GetViewModel();

            // Then
            for (var i = 0; i < sections.Count(); i++)
            {
                var section = sections.ElementAt(i);
                var questions = section.Questions;

                for (int j = 0; j < questions.Count(); j++)
                {
                    var question = questions.ElementAt(j);
                    Assert.That(question.Id, Is.EqualTo(result.Sections.ElementAt(i).Questions.ElementAt(j).Id));
                    Assert.That(question.ListOrder, Is.EqualTo(result.Sections.ElementAt(i).Questions.ElementAt(j).ListOrder));
                    Assert.That(question.Text, Is.EqualTo(result.Sections.ElementAt(i).Questions.ElementAt(j).Text));
                    Assert.That(question.Information, Is.EqualTo(result.Sections.ElementAt(i).Questions.ElementAt(j).Information));
                }
            }
        }
        
        [Test]
        public void Given_a_checklist_with_answers_When_GetViewModel_Then_answer_is_associated_to_its_corresponding_question()
        {
            // Given
            var fireRiskAssessmentDto = GetFireRiskAssessmentDto();
            var answers = fireRiskAssessmentDto.LatestFireRiskAssessmentChecklist.Answers;

            _riskAssessmentService
                .Setup(x => x.GetWithLatestFireRiskAssessmentChecklist(_riskAssessmentId, _companyId))
                .Returns(fireRiskAssessmentDto);

            // When
            var result = _target
                .WithRiskAssessmentId(_riskAssessmentId)
                .WithCompanyId(_companyId)
                .GetViewModel();

            // Then
            var answerInViewModelToCheck = result.Sections.First().Questions.First().Answer;
            var answerInDtoToCheck = answers.First();
            Assert.That(answerInViewModelToCheck.Id, Is.EqualTo(answerInDtoToCheck.Id));
            Assert.That(answerInViewModelToCheck.YesNoNotApplicableResponse, Is.EqualTo(answerInDtoToCheck.YesNoNotApplicableResponse));
            Assert.That(answerInViewModelToCheck.AdditionalInfo, Is.EqualTo(answerInDtoToCheck.AdditionalInfo));
        }

        [Test]
        public void Given_a_checklist_with_sections_When_GetViewModel_Then_first_section_is_set_to_active()
        {
            // Given
            var fireRiskAssessmentDto = GetFireRiskAssessmentDto();

            _riskAssessmentService
                .Setup(x => x.GetWithLatestFireRiskAssessmentChecklist(_riskAssessmentId, _companyId))
                .Returns(fireRiskAssessmentDto);

            // When
            var result = _target
                .WithRiskAssessmentId(_riskAssessmentId)
                .WithCompanyId(_companyId)
                .GetViewModel();

            // Then
            Assert.IsTrue(result.Sections.First().Active);
        }

        [Test]
        public void Given_a_checklist_with_sections_When_HasLatestFireChecklistGotCompleteFailureAttempt_is_true_Then_verify_calls_service()
        {
            // Given
            var fireRiskAssessmentDto = GetFireRiskAssessmentDto();

            _riskAssessmentService
                .Setup(x => x.GetWithLatestFireRiskAssessmentChecklist(_riskAssessmentId, _companyId))
                .Returns(fireRiskAssessmentDto);

            // When
            var result = _target
                .WithRiskAssessmentId(_riskAssessmentId)
                .WithCompanyId(_companyId)
                .GetViewModel();

            // Then
            _fireRiskAssessmentChecklistService.VerifyAll();
        }


        private static FireRiskAssessmentDto GetFireRiskAssessmentDto()
        {
            long section1Id = 1L;
            long question1Id = 10L;
            long question2Id = 20L;
            long question3Id = 30L;
            long answer1Id = 100L;
            long answer2Id = 200L;
            long answer3Id = 300L;

            var question1Dto = new QuestionDto
            {
                Id = question1Id,
                ListOrder = (int)question1Id,
                Text = "question " + question1Id,
                Information = "some info " + question1Id,
                QuestionType = QuestionType.YesNoNotApplicable,
                IsRequired = false
            };

            var question2Dto = new QuestionDto
            {
                Id = question2Id,
                ListOrder = (int)question2Id,
                Text = "question " + question2Id,
                Information = "some info " + question2Id,
                QuestionType = QuestionType.YesNoNotApplicable,
                IsRequired = false
            };
            var question3Dto = new QuestionDto
            {
                Id = question3Id,
                ListOrder = (int)question3Id,
                Text = "question " + question3Id,
                Information = "some info " + question3Id,
                QuestionType = QuestionType.YesNoNotApplicable,
                IsRequired = false
            };

            var section1Dto = new SectionDto
            {
                Id = section1Id,
                ListOrder = (int)section1Id,
                ShortTitle = "short title " + section1Id.ToString(),
                Title = "normal title " + section1Id.ToString(),
                Questions = new List<QuestionDto>
                            {
                                question1Dto,
                                question2Dto,
                                question3Dto
                            }
            };

            long section2Id = 1L;
            long question4Id = 40L;
            long question5Id = 50L;
            long question6Id = 60L;
            long answer4Id = 400L;
            long answer5Id = 500L;
            long answer6Id = 600L;

            var question4Dto = new QuestionDto
            {
                Id = question4Id,
                ListOrder = (int)question4Id,
                Text = "question " + question4Id,
                Information = "some info " + question4Id,
                QuestionType = QuestionType.YesNoNotApplicable,
                IsRequired = false
            };

            var question5Dto = new QuestionDto
            {
                Id = question5Id,
                ListOrder = (int)question5Id,
                Text = "question " + question5Id,
                Information = "some info " + question5Id,
                QuestionType = QuestionType.YesNoNotApplicable,
                IsRequired = false
            };
            var question6Dto = new QuestionDto
            {
                Id = question6Id,
                ListOrder = (int)question6Id,
                Text = "question " + question6Id,
                Information = "some info " + question6Id,
                QuestionType = QuestionType.YesNoNotApplicable,
                IsRequired = false
            };

            var section2Dto = new SectionDto
            {
                Id = section2Id,
                ListOrder = (int)section2Id,
                ShortTitle = "short title " + section2Id.ToString(),
                Title = "normal title " + section2Id.ToString(),
                Questions = new List<QuestionDto>
                            {
                                question4Dto,
                                question5Dto,
                                question6Dto
                            }
            };

            var checklistDto = new ChecklistDto
            {
                Sections = new List<SectionDto>
                {
                    section1Dto,
                    section2Dto
                }
            };

            var fireAnswer1Dto = new FireAnswerDto
            {
                Id = answer1Id,
                Question = question1Dto,
                AdditionalInfo = "some information",
                YesNoNotApplicableResponse = YesNoNotApplicableEnum.No
            };
            var fireAnswer2Dto = new FireAnswerDto
            {
                Id = answer2Id,
                Question = question2Dto,
                AdditionalInfo = "some information",
                YesNoNotApplicableResponse = YesNoNotApplicableEnum.Yes
            };
            var fireAnswer3Dto = new FireAnswerDto
            {
                Id = answer3Id,
                Question = question3Dto,
                AdditionalInfo = "some information",
                YesNoNotApplicableResponse = YesNoNotApplicableEnum.NotApplicable
            };

            var fireAnswer4Dto = new FireAnswerDto
            {
                Id = answer4Id,
                Question = question4Dto,
                AdditionalInfo = "some information",
                YesNoNotApplicableResponse = YesNoNotApplicableEnum.No
            };
            var fireAnswer5Dto = new FireAnswerDto
            {
                Id = answer5Id,
                Question = question5Dto,
                AdditionalInfo = "some information",
                YesNoNotApplicableResponse = YesNoNotApplicableEnum.Yes
            };
            var fireAnswer6Dto = new FireAnswerDto
            {
                Id = answer6Id,
                Question = question6Dto,
                YesNoNotApplicableResponse = YesNoNotApplicableEnum.NotApplicable
            };

            var fireRiskAssessmentChecklistDto = new FireRiskAssessmentChecklistDto
            {
                Id = 12345L,
                Checklist = checklistDto,
                Answers = new List<FireAnswerDto>
                {
                    fireAnswer1Dto,
                    fireAnswer2Dto,
                    fireAnswer3Dto,
                    fireAnswer4Dto,
                    fireAnswer5Dto,
                    fireAnswer6Dto
                },
                HasCompleteFailureAttempt = true
            };

            var fireRiskAssessmentDto = new FireRiskAssessmentDto
            {
                LatestFireRiskAssessmentChecklist = fireRiskAssessmentChecklistDto
            };

            return fireRiskAssessmentDto;
        }

        //[Test]
        //public void Given_fire_risk_assessment_checklist_with_complete_failed_attempt_When_GetViewModel_Then_should_call_correct_methods()
        //{
        //    // Given
        //    var riskAssessmentDto = new FireRiskAssessmentDto()
        //                                {

        //                                    LatestFireRiskAssessmentChecklist = new FireRiskAssessmentChecklistDto()
        //                                                                            {
        //                                                                                HasCompleteFailureAttempt = true,
        //                                                                                Checklist = new ChecklistDto()
        //                                                                                                {
        //                                                                                                    Sections = new SectionDto[] { }
        //                                                                                                }
        //                                                                            }
        //                                };
        //    _riskAssessmentService
        //        .Setup(x => x.GetWithLatestFireRiskAssessmentChecklist(riskAssessmentId, companyId))
        //        .Returns(riskAssessmentDto);

        //    _fireRiskAssessmentChecklistService
        //        .Setup(x => x.ValidateFireRiskAssessmentChecklist(It.IsAny<ValidateCompleteFireRiskAssessmentChecklistRequest>()))
        //        .Returns(new ValidationResult());

        //    // When
        //    _target
        //        .WithRiskAssessmentId(riskAssessmentId)
        //        .WithCompanyId(companyId)
        //        .GetViewModel();

        //    // Then
        //    _riskAssessmentService.VerifyAll();
        //    _fireRiskAssessmentChecklistService.VerifyAll();
        //}

        //[Test]
        //public void Given_validation_errors_When_GetViewModel_Then_should_return_correct_result()
        //{
        //    // Given
        //    var viewModel = new FireRiskAssessmentChecklistViewModel()
        //                        {
        //                            Sections = new List<SectionViewModel>()
        //                                           {
        //                                               new SectionViewModel()
        //                                                   {
        //                                                       Questions = new List<QuestionViewModel>()
        //                                                                       {
        //                                                                           new QuestionViewModel()
        //                                                                               {
        //                                                                                   Id = 1L
        //                                                                               }
        //                                                                       }
        //                                                   }
        //                                           }
        //                        };
        //    var errors = new List<ValidationFailure>()
        //                     {
        //                         new ValidationFailure("1","blah blah blah")
        //                     };

        //    // When
        //    var result = _target.GetViewModel(viewModel, errors);

        //    // Then
        //    Assert.That(result.IsValid, Is.False);
        //    Assert.That(result.Sections.First().IsSectionValid, Is.False);
        //    Assert.That(result.Sections.First().Questions.First().IsQuestionValid, Is.False);
        //}
    }
}