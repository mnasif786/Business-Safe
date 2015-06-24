using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.RestAPI.Responses;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using EvaluationChecklist.ClientDetails;
using EvaluationChecklist.Controllers;
using EvaluationChecklist.Helpers;
using EvaluationChecklist.Models;
using Moq;
using NUnit.Framework;

namespace EvaluationChecklist.Api.Tests.DocumentControllerTests
{
    [TestFixture]
    public class CreateComplianceReviewReportViewModel
    {
        private Mock<IClientDetailsService> _clientDetailsService;
        private Mock<IEmployeeRepository> _employeeRepository;
                       
        private const int IMPROVEMENT_REQUIRED_RESPONSE =4;
        private const int ACCEPTABLE_RESPONSE = 1;
        private const int UNACCEPTABLE_RESPONSE = 2;
        private const int NOT_APPLICABLE_RESPONSE = 3;        
                          
             
        [SetUp]
        public void Setup()
        {
            _employeeRepository = new Mock<IEmployeeRepository>();

            _clientDetailsService = new Mock<IClientDetailsService>();
            _clientDetailsService.Setup(x => x.GetSite(It.IsAny<int>(),It.IsAny<int>()))
                .Returns(new SiteAddressResponse());         
        }

        private ChecklistViewModel CreateChecklistViewModel()
        {
            ChecklistViewModel chkModel = new ChecklistViewModel();
            chkModel.SiteVisit = new SiteVisitViewModel()
                                      {
                                          AreasVisited = "Visited area",
                                          AreasNotVisited = "Non-visited area",
                                          EmailAddress = "yabba@dabba.doo.com",
                                          VisitDate = DateTime.Now.ToShortDateString(),
                                          VisitBy = "Barney Rubble",
                                          VisitType = "Initial visit"
                                      };

            chkModel.SiteVisit.PersonSeen = new PersonSeenViewModel()
                                                 {
                                                     JobTitle = "Dogsbody",
                                                     Name = "Fred Flintstone",
                                                     Salutation = "Mr"
                                                 };

            chkModel.SiteVisit.SelectedImpressionType = new ImpressionTypeViewModel()
                                                             {
                                                                 Id = Guid.NewGuid(),
                                                                 Title = "the title",
                                                                 Comments = "some comments"
                                                             };

            return chkModel;
        }

        /// <summary>
        /// create a question with 3 responses
        /// </summary>
        /// <param name="ResponseType">the selected answer</param>
        /// <returns></returns>
        private static QuestionAnswerViewModel CreateQuestionAnswerViewModel( int ResponseType )
        {
            QuestionAnswerViewModel qaViewModel = new QuestionAnswerViewModel();

            Guid categoryId = new Guid();
            qaViewModel.Question = new QuestionViewModel()
            {
                Id = Guid.NewGuid(),
                Text = "QuestionText",
                PossibleResponses = new List<QuestionResponseViewModel>(),
                Mandatory = false,
                SpecificToClientId = (long)1234,
                Category = new CategoryViewModel()
                               {
                                   Id = categoryId,
                                   Title = "TestCategory"                                   
                               },
                CategoryId = categoryId
            };           

            QuestionResponseViewModel qrAcceptableViewModel = new QuestionResponseViewModel()
            {
                Id = Guid.NewGuid(),
                Title = "Acceptable",
                SupportingEvidence = "Acceptable supporting evidence",
                ActionRequired = "Acceptable action required",
                ResponseType = "Positive",
                GuidanceNotes = "Acceptable guidance notes",ReportLetterStatement = "acceptable letter statement"
            };

            QuestionResponseViewModel qrUnacceptableViewModel = new QuestionResponseViewModel()
            {
                Id = Guid.NewGuid(),
                Title = "Unacceptable",
                SupportingEvidence = "Unacceptable supporting evidence",
                ActionRequired = "Unacceptable action required",
                ResponseType = "Negative",
                GuidanceNotes = "Unacceptable guidance notes",ReportLetterStatement = "unacceptable letter statement"
            };

            QuestionResponseViewModel qrImprovementRequiredViewModel = new QuestionResponseViewModel()
            {
                Id = Guid.NewGuid(),
                Title = "Improvement Required",
                SupportingEvidence = "Improvement Required supporting evidence",
                ActionRequired = "Improvement Required action required",
                ResponseType = "Neutral",
                GuidanceNotes = "Improvement Required guidance notes"
            };

            QuestionResponseViewModel qrNotApplicableRequiredViewModel = new QuestionResponseViewModel()
            {
                Id = Guid.NewGuid(),
                Title = "Not Applicable",
                SupportingEvidence = "Not Applicable supporting evidence",
                ActionRequired = "Not Applicable Required action required",
                ResponseType = "Neutral",
                GuidanceNotes = "Not Applicable guidance notes",
                ReportLetterStatement = "not applicable letter statement"
            };
          

            qaViewModel.Question.PossibleResponses.Add(qrAcceptableViewModel);
            qaViewModel.Question.PossibleResponses.Add(qrUnacceptableViewModel);
            qaViewModel.Question.PossibleResponses.Add(qrImprovementRequiredViewModel);
            qaViewModel.Question.PossibleResponses.Add(qrNotApplicableRequiredViewModel);

            qaViewModel.Answer = new AnswerViewModel()
            {               
                SupportingEvidence = "Answer supporting evidence",
                ActionRequired = "Answer action required",
                GuidanceNotes = "Answer guidance notes",
                Timescale = new TimescaleViewModel()
                {
                    Id = 1234,
                    Name = "This Week"
                },
                AssignedTo = new AssignedToViewModel()
                {
                    Id = Guid.NewGuid(),
                    NonEmployeeName = "Bob Roberts"
                },
                QuestionId = Guid.NewGuid()
            };

            switch( ResponseType )
            {
                case ACCEPTABLE_RESPONSE:
                    qaViewModel.Answer.SelectedResponseId = qrAcceptableViewModel.Id;
                    break;

                case UNACCEPTABLE_RESPONSE:
                    qaViewModel.Answer.SelectedResponseId = qrUnacceptableViewModel.Id;
                    break;              

                case IMPROVEMENT_REQUIRED_RESPONSE:
                    qaViewModel.Answer.SelectedResponseId = qrImprovementRequiredViewModel.Id;
                    break;

                case NOT_APPLICABLE_RESPONSE:
                    qaViewModel.Answer.SelectedResponseId = qrNotApplicableRequiredViewModel.Id;
                    break;
            }           

            return qaViewModel;
        }

        private static ImmediateRiskNotificationViewModel CreateImmediateRiskNotificationViewModel()
        {
            ImmediateRiskNotificationViewModel model = new ImmediateRiskNotificationViewModel()
                                                           {
                                                               Id = Guid.NewGuid(),
                                                               Reference = "Ref 123",
                                                               Title = "IRN building is on fire",
                                                               SignificantHazard = "IRN being on fire hazard",
                                                               RecommendedImmediate = "Put out fire"                                                                
                                                           };            
            return model;
        }

        [Test]
        public void Given_a_valid_checklist_Then_basic_site_visit_details_are_returned()
        {
            // GIVEN    
            ChecklistViewModel chkModel = CreateChecklistViewModel();

            chkModel.SiteVisit.VisitDate = "05.06.2014";         
            var expectedDate = new DateTime(2014, 6, 5);

            // WHEN
            var target = GetTarget();
            ComplianceReviewReportViewModel model = target.GetViewModel( chkModel );

            // THEN
            Assert.AreEqual( chkModel.SiteVisit.PersonSeen.Name,    model.PersonSeen );
            Assert.AreEqual( expectedDate,                          model.VisitDate);
            Assert.AreEqual( chkModel.SiteVisit.AreasVisited,       model.AreasVisited );
            Assert.AreEqual( chkModel.SiteVisit.AreasNotVisited,    model.AreasNotVisited );
        }

        [Test]
        public void Given_a_checklist_with_a_valid_site_id_Then_site_details_are_returned()
        {
            var clientId = 114876;
            // GIVEN    
            var siteAddressResponse = new SiteAddressResponse();
            siteAddressResponse.Id = 1234124124;
            siteAddressResponse.SiteName = "the site name";
            siteAddressResponse.Address1 = "1 main streest";
            siteAddressResponse.Address2 = "the town";
            siteAddressResponse.Address3 = "the county";
            siteAddressResponse.Address4 = "somethin else";
            siteAddressResponse.Postcode = "M21 7ND";
           
            //siteAddressResponse.Address1 = "Address line 1";

            _clientDetailsService.Setup(x => x.GetSite(clientId,(int) siteAddressResponse.Id))
               .Returns(siteAddressResponse);         


            ChecklistViewModel chkModel = CreateChecklistViewModel();
            chkModel.SiteId = (int)siteAddressResponse.Id;
            chkModel.ClientId = clientId;

            // WHEN
            var target = GetTarget();
            ComplianceReviewReportViewModel model = target.GetViewModel(chkModel);

            // THEN
            Assert.AreEqual(siteAddressResponse.Id, model.Site.Id);
            Assert.AreEqual(siteAddressResponse.SiteName, model.Site.SiteName);

            Assert.AreEqual( siteAddressResponse.Address1, model.Site.Address1);
            Assert.AreEqual( siteAddressResponse.Address2, model.Site.Address2);
            Assert.AreEqual( siteAddressResponse.Address3, model.Site.Address3);
            Assert.AreEqual( siteAddressResponse.Address4, model.Site.Address4);
            Assert.AreEqual( siteAddressResponse.Postcode, model.Site.Postcode);            
        }

        [Test]
        public void Given_a_checklist_with_clientid_and_siteid_no_specified_Then_site_details_are__not_returned()
        {
            var clientId = 114876;
            // GIVEN    
            ChecklistViewModel chkModel = CreateChecklistViewModel();
            chkModel.SiteId = null;
            chkModel.ClientId = null;

            // WHEN
            var target = GetTarget();
            ComplianceReviewReportViewModel model = target.GetViewModel(chkModel);

            // THEN
            Assert.IsNotNull(model.Site);
            _clientDetailsService.Verify(x=> x.GetSite(It.IsAny<int>(),It.IsAny<int>()),Times.Never());
        } 

        [Test]
        public void Given_a_valid_checklist_containing_a_question_and_responses_Then_an_action_plan_item_is_returned()
        {
            // GIVEN  
            ChecklistViewModel chkModel = CreateChecklistViewModel();
            chkModel.Questions = new List<QuestionAnswerViewModel>();

            var qaViewModel = CreateQuestionAnswerViewModel(UNACCEPTABLE_RESPONSE);

            chkModel.Questions.Add(qaViewModel);

            // WHEN
            var target = GetTarget();
            ComplianceReviewReportViewModel model = target.GetViewModel(chkModel);

            // THEN
            Assert.IsNotNull(model.ActionPlanItems);
            Assert.AreEqual( 1, model.ActionPlanItems.Count);                       
            Assert.AreEqual( qaViewModel.Question.PossibleResponses.First(x => x.Title == "Unacceptable").ReportLetterStatement, model.ActionPlanItems[0].AreaOfNonCompliance);
            Assert.AreEqual( qaViewModel.Answer.ActionRequired, model.ActionPlanItems[0].ActionRequired);
            Assert.AreEqual( qaViewModel.Answer.GuidanceNotes, model.ActionPlanItems[0].GuidanceNumber);
            Assert.AreEqual( qaViewModel.Answer.Timescale.Name, model.ActionPlanItems[0].TargetTimescale);
            Assert.AreEqual( qaViewModel.Answer.AssignedTo.NonEmployeeName, model.ActionPlanItems[0].AllocatedTo);            
        }

        [Test]
        public void Given_a_valid_checklist_containing_an_immediate_risk_notification_then_an_immediate_risk_notification_plan_item_is_returned()
        {
            // GIVEN  
            ChecklistViewModel chkModel = CreateChecklistViewModel();            
            chkModel.ImmediateRiskNotifications = new List<ImmediateRiskNotificationViewModel>();

            var irnModel = CreateImmediateRiskNotificationViewModel();
            chkModel.ImmediateRiskNotifications.Add(irnModel);

            // WHEN
            var target = GetTarget();
            ComplianceReviewReportViewModel model = target.GetViewModel(chkModel);

            // THEN
            Assert.IsNotNull(model.ImmediateRiskNotifications);
            Assert.AreEqual( 1, model.ImmediateRiskNotifications.Count);      
                 
            Assert.AreEqual( irnModel.SignificantHazard,    model.ImmediateRiskNotifications[0].SignificantHazardIdentified);
            Assert.AreEqual( irnModel.RecommendedImmediate, model.ImmediateRiskNotifications[0].RecommendedImmediateAction);                
            Assert.AreEqual( chkModel.SiteVisit.PersonSeen.Name, model.ImmediateRiskNotifications[0].AllocatedTo);                             
        }


        [Test]
        public void Given_a_valid_checklist_containing_a_question_and_responses_Then_a_compliance_review_item_is_returned()
        {
            // GIVEN  
            ChecklistViewModel chkModel = CreateChecklistViewModel();
            chkModel.Questions = new List<QuestionAnswerViewModel>();

            var qaViewModel = CreateQuestionAnswerViewModel(UNACCEPTABLE_RESPONSE);

            chkModel.Questions.Add(qaViewModel);


            // WHEN
            var target = GetTarget();
            ComplianceReviewReportViewModel model = target.GetViewModel(chkModel);

            // THEN
            Assert.IsNotNull(model.ComplianceReviewItems);
            Assert.AreEqual(1, model.ComplianceReviewItems.Count);

            Assert.AreEqual( qaViewModel.Question.Text, model.ComplianceReviewItems[0].QuestionText);                             
            Assert.AreEqual( qaViewModel.Answer.SupportingEvidence , model.ComplianceReviewItems[0].SupportingEvidence);            
            Assert.AreEqual( qaViewModel.Answer.ActionRequired, model.ComplianceReviewItems[0].ActionRequired);
            Assert.AreEqual( qaViewModel.Question.Category.Title, model.ComplianceReviewItems[0].CategoryName);
        }


        [Test]
        public void Given_a_valid_checklist_containing_two_questions_with_acceptable_responses_Then_two_action_plan_items_are_returned()
        {
            // GIVEN              
            List<QuestionAnswerViewModel> qaModels = new List<QuestionAnswerViewModel>();

            var acceptResp1 = CreateQuestionAnswerViewModel(ACCEPTABLE_RESPONSE);
            var acceptResp2 = CreateQuestionAnswerViewModel(ACCEPTABLE_RESPONSE);
            var unacceptResp1 = CreateQuestionAnswerViewModel(UNACCEPTABLE_RESPONSE);
            var unacceptResp2 = CreateQuestionAnswerViewModel(UNACCEPTABLE_RESPONSE);
            //var neutrResp1 = CreateQuestionAnswerViewModel(NOTAPPLICABLE_RESPONSE);
            //var naResp2 = CreateQuestionAnswerViewModel(NOTAPPLICABLE_RESPONSE);

            qaModels.Add(acceptResp1);
            qaModels.Add(unacceptResp1);
            //qaModels.Add(naResp1);

            qaModels.Add(acceptResp2);
            qaModels.Add(unacceptResp2);
            //qaModels.Add(naResp2);

            ChecklistViewModel chkModel = CreateChecklistViewModel();
            chkModel.Questions = new List<QuestionAnswerViewModel>();
            foreach (var qam in qaModels)
            {
                chkModel.Questions.Add(qam);
            }

            // WHEN
            var target = GetTarget();
            ComplianceReviewReportViewModel model = target.GetViewModel(chkModel);

            // THEN
            Assert.IsNotNull(model.ActionPlanItems);
            Assert.AreEqual(2, model.ActionPlanItems.Count);
        }

        
        [Test]
        public void Given_a_valid_checklist_containing_six_questions_with_mixed_responses_Then_six_compliance_review_actions_are_returned()
        {
            // GIVEN              
            List<QuestionAnswerViewModel> qaModels = new List<QuestionAnswerViewModel>();

            var acceptResp1 = CreateQuestionAnswerViewModel(ACCEPTABLE_RESPONSE);
            var acceptResp2 = CreateQuestionAnswerViewModel(ACCEPTABLE_RESPONSE);
            var unacceptResp1 = CreateQuestionAnswerViewModel(UNACCEPTABLE_RESPONSE);
            var unacceptResp2 = CreateQuestionAnswerViewModel(UNACCEPTABLE_RESPONSE);
            //var naResp1 = CreateQuestionAnswerViewModel(NOTAPPLICABLE_RESPONSE);
            //var naResp2 = CreateQuestionAnswerViewModel(NOTAPPLICABLE_RESPONSE);

            qaModels.Add(acceptResp1);
            qaModels.Add(unacceptResp1);
            //qaModels.Add(naResp1);

            qaModels.Add(acceptResp2);
            qaModels.Add(unacceptResp2);
            //qaModels.Add(naResp2);

            ChecklistViewModel chkModel = CreateChecklistViewModel();
            chkModel.Questions = new List<QuestionAnswerViewModel>();
            foreach (var qam in qaModels)
            {
                chkModel.Questions.Add(qam);
            }

            // WHEN
            var target = GetTarget();
            ComplianceReviewReportViewModel model = target.GetViewModel(chkModel);

            // THEN
            Assert.IsNotNull(model.ComplianceReviewItems);
            Assert.AreEqual(4, model.ComplianceReviewItems.Count);            
        }


        [Test]
        public void Given_a_question_has_been_answered_as_unacceptable_then_the_ComplianceReviewItem_status_should_equal_ImmediateActionRequired()
        {
            var checklistViewModel = new ChecklistViewModel();
            checklistViewModel.Questions.Add(CreateQuestionAnswerViewModel(UNACCEPTABLE_RESPONSE));

            var target = GetTarget();

            var result = target.GetViewModel(checklistViewModel);

            Assert.That(result.ComplianceReviewItems[0].Status, Is.EqualTo(ComplianceReviewItemStatus.ImmediateActionRequired));

        }

        [Test]
        public void Given_a_question_has_been_answered_as_acceptable_then_the_ComplianceReviewItem_status_should_equal_Satisfactory()
        {
            var checklistViewModel = new ChecklistViewModel();
            checklistViewModel.Questions.Add(CreateQuestionAnswerViewModel(ACCEPTABLE_RESPONSE));

            var target = GetTarget();

            var result = target.GetViewModel(checklistViewModel);

            Assert.That(result.ComplianceReviewItems[0].Status, Is.EqualTo(ComplianceReviewItemStatus.Satisfactory));

        }

        [Test]
        public void Given_a_question_has_been_answered_as_Improvement_Required_then_the_ComplianceReviewItem_status_should_equal_FurtherActionRequired()
        {
            var checklistViewModel = new ChecklistViewModel();
            checklistViewModel.Questions.Add(CreateQuestionAnswerViewModel(IMPROVEMENT_REQUIRED_RESPONSE));

            var target = GetTarget();

            var result = target.GetViewModel(checklistViewModel);

            Assert.That(result.ComplianceReviewItems[0].Status, Is.EqualTo(ComplianceReviewItemStatus.FurtherActionRequired));

        }

        [Test]
        public void Given_a_question_has_been_answered_as_Not_Applicable_then_no_ComplianceReviewItem_should_be_added()
        {
            var checklistViewModel = new ChecklistViewModel();
            checklistViewModel.Questions.Add(CreateQuestionAnswerViewModel(NOT_APPLICABLE_RESPONSE));

            var target = GetTarget();

            var result = target.GetViewModel(checklistViewModel);

            Assert.AreEqual(0, result.ComplianceReviewItems.Count);
        }


        [Test]
        public void Given_a_question_has_been_answered_as_Not_Applicable_then_no_action_plan_items_should_be_added()
        {
            var checklistViewModel = new ChecklistViewModel();
            checklistViewModel.Questions.Add(CreateQuestionAnswerViewModel(NOT_APPLICABLE_RESPONSE));

            var target = GetTarget();

            var result = target.GetViewModel(checklistViewModel);

            Assert.AreEqual(0, result.ActionPlanItems.Count);
        }


        [Test]public void Given_a_question_has_been_answered_as_unacceptable_but_has_timescale_null_then_the_action_Plan_item_has_empty_Timescale_string()
        {
            var checklistViewModel = new ChecklistViewModel();
            checklistViewModel.Questions.Add(CreateQuestionAnswerViewModel(UNACCEPTABLE_RESPONSE));

            checklistViewModel.Questions[0].Answer.Timescale =null;

            var target = GetTarget();

            var result = target.GetViewModel(checklistViewModel);

            Assert.AreEqual(String.Empty, result.ActionPlanItems[0].TargetTimescale);
        }

        [Test]
        public void Given_a_question_answer_is_null_then_no_action_Plan_items_will_be_returned()
        {
            var checklistViewModel = new ChecklistViewModel();
            checklistViewModel.Questions.Add(CreateQuestionAnswerViewModel(UNACCEPTABLE_RESPONSE));

            checklistViewModel.Questions[0].Answer = null;

            var target = GetTarget();

            var result = target.GetViewModel(checklistViewModel);

            Assert.AreEqual(0, result.ActionPlanItems.Count);
        }

        [Test]           
        public void Given_a_question_answer_is_null_then_an_item_will_still_be_added_to_compliance_review_items()
        {
            var checklistViewModel = new ChecklistViewModel();
            checklistViewModel.Questions.Add(CreateQuestionAnswerViewModel(UNACCEPTABLE_RESPONSE));

            checklistViewModel.Questions[0].Answer = null;

            var target = GetTarget();

            var result = target.GetViewModel(checklistViewModel);

            Assert.AreEqual(1, result.ComplianceReviewItems.Count);
        }

        [Test]
        public void Given_a_question_has_been_answered_as_Improvement_Required_then_an_ActionPlanItem_is_created()
        {
            var checklistViewModel = new ChecklistViewModel();
            checklistViewModel.Questions.Add(CreateQuestionAnswerViewModel(IMPROVEMENT_REQUIRED_RESPONSE));

            var target = GetTarget();

            var result = target.GetViewModel(checklistViewModel);

            Assert.That(result.ActionPlanItems.Count, Is.EqualTo(1));

        }



        [Test]
        public void Given_a_set_of_questions_has_been_answered_as_Improvement_Required_or_Unacceptable_then_ActionPlanItems_are_created_in_order()
        {
            var checklistViewModel = new ChecklistViewModel();

            QuestionAnswerViewModel improvNone = CreateQuestionAnswerViewModel(IMPROVEMENT_REQUIRED_RESPONSE);
            improvNone.Answer.Timescale.Name = "None";
            checklistViewModel.Questions.Add(improvNone);

            QuestionAnswerViewModel improvSixMonths = CreateQuestionAnswerViewModel(IMPROVEMENT_REQUIRED_RESPONSE);
            improvSixMonths.Answer.Timescale.Name = "Six Months";
            checklistViewModel.Questions.Add(improvSixMonths);

            QuestionAnswerViewModel improvThreeMonths = CreateQuestionAnswerViewModel(IMPROVEMENT_REQUIRED_RESPONSE);
            improvThreeMonths.Answer.Timescale.Name = "Three Months";
            checklistViewModel.Questions.Add(improvThreeMonths);

            QuestionAnswerViewModel improvOneMonth = CreateQuestionAnswerViewModel(IMPROVEMENT_REQUIRED_RESPONSE);
            improvOneMonth.Answer.Timescale.Name = "One Month";
            checklistViewModel.Questions.Add(improvOneMonth);

            QuestionAnswerViewModel improvUrgent = CreateQuestionAnswerViewModel(IMPROVEMENT_REQUIRED_RESPONSE);
            improvUrgent.Answer.Timescale.Name = "Urgent Action Required";
            checklistViewModel.Questions.Add(improvUrgent);




            QuestionAnswerViewModel  unacceptableNone = CreateQuestionAnswerViewModel(UNACCEPTABLE_RESPONSE);
            unacceptableNone.Answer.Timescale.Name = "None";
            checklistViewModel.Questions.Add(unacceptableNone);

            QuestionAnswerViewModel unacceptableSixMonths = CreateQuestionAnswerViewModel(UNACCEPTABLE_RESPONSE);
            unacceptableSixMonths.Answer.Timescale.Name = "Six Months";
            checklistViewModel.Questions.Add(unacceptableSixMonths);

            QuestionAnswerViewModel unacceptableThreeMonths = CreateQuestionAnswerViewModel(UNACCEPTABLE_RESPONSE);
            unacceptableThreeMonths.Answer.Timescale.Name = "Three Months";
            checklistViewModel.Questions.Add(unacceptableThreeMonths);

            QuestionAnswerViewModel unacceptableOneMonth = CreateQuestionAnswerViewModel(UNACCEPTABLE_RESPONSE);
            unacceptableOneMonth.Answer.Timescale.Name = "One Month";
            checklistViewModel.Questions.Add(unacceptableOneMonth);

            QuestionAnswerViewModel unacceptableUrgent = CreateQuestionAnswerViewModel(UNACCEPTABLE_RESPONSE);
            unacceptableUrgent.Answer.Timescale.Name = "Urgent Action Required";
            checklistViewModel.Questions.Add(unacceptableUrgent);


            var target = GetTarget();

            var result = target.GetViewModel(checklistViewModel);

            Assert.That(result.ActionPlanItems.Count, Is.EqualTo(10));
            Assert.That(result.ActionPlanItems[0].TargetTimescale, Is.EqualTo(unacceptableUrgent.Answer.Timescale.Name));
            Assert.That(result.ActionPlanItems[1].TargetTimescale, Is.EqualTo(unacceptableOneMonth.Answer.Timescale.Name));
            Assert.That(result.ActionPlanItems[2].TargetTimescale, Is.EqualTo(unacceptableThreeMonths.Answer.Timescale.Name));
            Assert.That(result.ActionPlanItems[3].TargetTimescale, Is.EqualTo(unacceptableSixMonths.Answer.Timescale.Name));
            Assert.That(result.ActionPlanItems[4].TargetTimescale, Is.EqualTo(unacceptableNone.Answer.Timescale.Name));
            Assert.That(result.ActionPlanItems[5].TargetTimescale, Is.EqualTo(improvUrgent.Answer.Timescale.Name));
            Assert.That(result.ActionPlanItems[6].TargetTimescale, Is.EqualTo(improvOneMonth.Answer.Timescale.Name));
            Assert.That(result.ActionPlanItems[7].TargetTimescale, Is.EqualTo(improvThreeMonths.Answer.Timescale.Name));
            Assert.That(result.ActionPlanItems[8].TargetTimescale, Is.EqualTo(improvSixMonths.Answer.Timescale.Name));
            Assert.That(result.ActionPlanItems[9].TargetTimescale, Is.EqualTo(improvNone.Answer.Timescale.Name));
        }







        [Test]
        public void Given_a_question_has_been_allocated_to_a_user_then_an_ActionPlanItem_is_created_with_allocatedTo_set_correctly()
        {
            var checklistViewModel = new ChecklistViewModel();

            Guid employeeId = Guid.NewGuid();
            Employee employee = new Employee()
                                    {
                                        Id = employeeId,
                                        Forename = "Benny",
                                        Surname = "Hill"
                                    };
            string employeeFullName = employee.Forename + " " + employee.Surname;

            _employeeRepository.Setup(x => x.GetById( employeeId ))
               .Returns(employee);      

            QuestionAnswerViewModel model = CreateQuestionAnswerViewModel(IMPROVEMENT_REQUIRED_RESPONSE);
            model.Answer.AssignedTo = new AssignedToViewModel()
                                          {
                                              Id = employeeId,
                                              NonEmployeeName = String.Empty
                                          };
            checklistViewModel.Questions.Add(model);


            var target = GetTarget();

            var result = target.GetViewModel(checklistViewModel);
            
            Assert.AreEqual( employeeFullName, result.ActionPlanItems[0].AllocatedTo);
        }

        [Test]
        public void Given_a_question_has_been_allocated_to_a_nonemployee_then_an_ActionPlanItem_is_created_with_allocatedTo_nonemployeename()
        {
            var checklistViewModel = new ChecklistViewModel();           

            QuestionAnswerViewModel model = CreateQuestionAnswerViewModel(IMPROVEMENT_REQUIRED_RESPONSE);
            model.Answer.AssignedTo = new AssignedToViewModel()
            {
                Id = Guid.NewGuid(), // redundant, as no employees in database
                NonEmployeeName = "Norman Notemployedhere"
            };
            checklistViewModel.Questions.Add(model);


            var target = GetTarget();

            var result = target.GetViewModel(checklistViewModel);

            Assert.AreEqual(model.Answer.AssignedTo.NonEmployeeName, result.ActionPlanItems[0].AllocatedTo);
        }

        private IComplianceReviewReportViewModelFactory GetTarget()
        {
           return  new ComplianceReviewReportViewModelFactory(_clientDetailsService.Object, _employeeRepository.Object);            
        }
    }
}
