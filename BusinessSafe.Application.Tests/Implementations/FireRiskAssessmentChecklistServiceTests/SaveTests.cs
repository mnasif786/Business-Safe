using System;
using System.Collections.Generic;
using BusinessSafe.Application.Implementations.FireRiskAssessments;
using BusinessSafe.Application.Request.FireRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.FireRiskAssessmentChecklistServiceTests
{
    [TestFixture]
    public class SaveTests
    {
        private Mock<IFireRiskAssessmentChecklistRepository> _fireRiskAssessmentChecklistRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IQuestionRepository> _questionRepository;
        private Mock<IPeninsulaLog> _log;
        private Mock<FireRiskAssessmentChecklist> _fireRiskAssessmentChecklist;
        private Guid _userId;
        private SaveFireRiskAssessmentChecklistRequest _request;
        private long _fireRiskAssessmentChecklistId;
        private long _companyId;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _fireRiskAssessmentChecklistId = 234L;
            _companyId = 4251L;
            _userId = Guid.NewGuid();
            _fireRiskAssessmentChecklistRepository = new Mock<IFireRiskAssessmentChecklistRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _questionRepository = new Mock<IQuestionRepository>();
            _log = new Mock<IPeninsulaLog>();
            _fireRiskAssessmentChecklist = new Mock<FireRiskAssessmentChecklist>();

            _request = new SaveFireRiskAssessmentChecklistRequest
                              {
                                  FireRiskAssessmentChecklistId = _fireRiskAssessmentChecklistId,
                                  CurrentUserId = _userId,
                                  CompanyId = _companyId,
                                  Answers = new List<SubmitFireAnswerRequest>
                                                {
                                                    new SubmitFireAnswerRequest
                                                        {
                                                            QuestionId = 201L,
                                                            YesNoNotApplicableResponse = YesNoNotApplicableEnum.Yes,
                                                            AdditionalInfo = "Test Additional Info 1"
                                                        },
                                                    new SubmitFireAnswerRequest
                                                        {
                                                            QuestionId = 202L,
                                                            YesNoNotApplicableResponse = YesNoNotApplicableEnum.No
                                                        },
                                                    new SubmitFireAnswerRequest
                                                        {
                                                            QuestionId = 203L,
                                                            YesNoNotApplicableResponse =
                                                                YesNoNotApplicableEnum.NotApplicable
                                                        },
                                                    new SubmitFireAnswerRequest
                                                        {
                                                            QuestionId = 204L
                                                        },
                                                }
                              };

            _fireRiskAssessmentChecklistRepository
                .Setup(x => x.GetById(_fireRiskAssessmentChecklistId))
                .Returns(_fireRiskAssessmentChecklist.Object);

            _userRepository
                .Setup(x => x.GetById(_userId))
                .Returns(new UserForAuditing());

            _questionRepository
                .Setup(x => x.GetById(201L))
                .Returns(new Question{ Id = 201L });

            _questionRepository
                .Setup(x => x.GetById(202L))
                .Returns(new Question { Id = 202L });

            _questionRepository
                .Setup(x => x.GetById(203L))
                .Returns(new Question { Id = 203L });

            _questionRepository
                .Setup(x => x.GetById(204L))
                .Returns(new Question { Id = 204L });

            var fireRiskAssessmentChecklistService = new FireRiskAssessmentChecklistService(
                _fireRiskAssessmentChecklistRepository.Object,
                _userRepository.Object,
                _questionRepository.Object,
                _log.Object,
                null);

            fireRiskAssessmentChecklistService.Save(_request);
        }

        [Test]
        public void Given_valid_request_When_save_called_Then_log_Add_called()
        {
            _log.Verify(x => x.Add(_request));
        }

        [Test]
        public void Given_valid_request_When_save_called_Then_FireRiskAssessmentRepository_GetById_is_called()
        {
            _fireRiskAssessmentChecklistRepository.Verify(x => x.GetById(_fireRiskAssessmentChecklistId));
        }

        [Test]
        public void Given_valid_request_When_save_called_Then_UserRepository_GetByIdAndCompanyId_is_called()
        {
            _userRepository.Verify(x => x.GetByIdAndCompanyId(_userId, _companyId));
        }

        [Test]
        public void Given_valid_request_When_save_called_Then_QuestionRepository_GetById_is_called_for_first_question()
        {
            _questionRepository.Verify(x => x.GetById(201L));
        }

        [Test]
        public void Given_valid_request_When_save_called_Then_QuestionRepository_GetById_is_called_for_second_question()
        {
            _questionRepository.Verify(x => x.GetById(202L));
        }

        [Test]
        public void Given_valid_request_When_save_called_Then_QuestionRepository_GetById_is_called_for_third_question()
        {
            _questionRepository.Verify(x => x.GetById(203L));
        }

        [Test]
        public void Given_valid_request_When_save_called_Then_QuestionRepository_GetById_is_called_for_four_question()
        {
            _questionRepository.Verify(x => x.GetById(204L));
        }

        [Test]
        public void Given_valid_request_When_save_called_Then_FireRiskAssessmentChecklist_Save_is_called_with_correct_parameters()
        {
            _fireRiskAssessmentChecklist.Verify(x => x.Save(
                It.Is<List<SubmitFireAnswerParameters>>(
                   y => y.Count == 4
                   && y[0].YesNoNotApplicableResponse.Value == YesNoNotApplicableEnum.Yes
                   && y[0].AdditionalInfo == "Test Additional Info 1"
                   && y[0].Question.Id == 201L
                   && y[1].YesNoNotApplicableResponse.Value == YesNoNotApplicableEnum.No
                   && y[1].AdditionalInfo == null
                   && y[1].Question.Id == 202L
                   && y[2].YesNoNotApplicableResponse.Value == YesNoNotApplicableEnum.NotApplicable
                   && y[2].AdditionalInfo == null
                   && y[2].Question.Id == 203L
                   && y[3].YesNoNotApplicableResponse == null
                   && y[3].AdditionalInfo == null
                   && y[3].Question.Id == 204L
                   ),
                It.IsAny<UserForAuditing>()));
        }
    }
}
