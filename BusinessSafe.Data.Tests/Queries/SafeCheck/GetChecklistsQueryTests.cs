using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Data.Queries.SafeCheck;
using BusinessSafe.Domain.Entities.SafeCheck;
using Moq;
using NHibernate;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Tests.Queries.SafeCheck
{
    [TestFixture]
    public class GetChecklistsQueryTests
    {
        private Mock<IBusinessSafeSessionManager> _sessionManager;
        private Mock<ISession> _session;
        private Mock<IQueryableWrapper<Checklist>> _queryableWrapper;

        [SetUp]
        public void Setup()
        {
            _session = new Mock<ISession>();
            _sessionManager = new Mock<IBusinessSafeSessionManager>();

            _sessionManager.Setup(x => x.Session)
                .Returns(() => _session.Object);

            _queryableWrapper = new Mock<IQueryableWrapper<Checklist>>();
        }

        [Test]
        public void Given_a_clientId_all_checklists_that_are_linked_to_client_are_returned()
        {
            //given
            var clientIdForTEST002 = 14145135;
            var checklists = new List<Checklist>
                                 {
                                     new Checklist() {ClientId = 5257647, QaAdvisor = new QaAdvisor{Id = Guid.NewGuid(), Forename = "Test", Surname = "Test"}}
                                     ,
                                     new Checklist() {ClientId = clientIdForTEST002, QaAdvisor = new QaAdvisor{Id = Guid.NewGuid(), Forename = "Test", Surname = "Test"}}
                                     ,
                                     new Checklist() {ClientId = 23452435, QaAdvisor = new QaAdvisor{Id = Guid.NewGuid(), Forename = "Test", Surname = "Test"}}
                                 };

            _queryableWrapper.Setup(x => x.Queryable())
                .Returns(() => checklists.AsQueryable());


            var target = new GetChecklistsQuery(_queryableWrapper.Object);
            
            //when
            var result = target
                .WithClientId(clientIdForTEST002)
                .Execute();

            //then
            Assert.That(result.Count,Is.EqualTo(1));
            Assert.That(result.First().ClientId, Is.EqualTo(clientIdForTEST002));

        }

        [Test]
        public void Given_a_consultant_all_checklists_that_are_visited_by_consultant_are_returned()
        {
            //given
            var consultantToFind = "Mr H&S";
            var expectedIdOfFoundChecklist = Guid.NewGuid();
            var checklists = new List<Checklist>
                                 {
                                     new Checklist() {ClientId = 14145135, QaAdvisor = new QaAdvisor{Id = Guid.NewGuid(), Forename = "Test", Surname = "Test"}}
                                     , new Checklist() {Id= expectedIdOfFoundChecklist, ClientId = 14145135, VisitBy = consultantToFind, ChecklistCreatedBy = consultantToFind, QaAdvisor = new QaAdvisor{Id = Guid.NewGuid(), Forename = "Test", Surname = "Test"}}
                                     , new Checklist() {ClientId = 23452435, QaAdvisor = new QaAdvisor{Id = Guid.NewGuid(), Forename = "Test", Surname = "Test"}}
                                 };

           
            _queryableWrapper.Setup(x => x.Queryable())
                .Returns(() => checklists.AsQueryable());


            var target = new GetChecklistsQuery(_queryableWrapper.Object);

            //when
            var result = target.WithConsultantName(consultantToFind)
                .Execute();

            //then
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Id, Is.EqualTo(expectedIdOfFoundChecklist));
        }

        [Test]
        public void Given_a_visit_date_all_checklists_that_are_visited_by_consultant_are_returned()
        {
            var visitDate = DateTime.Now;
            var expectedIdOfFoundChecklist = Guid.NewGuid();
            var consultant = "Mr H&S";
            var checklists = new List<Checklist>
                                 {
                                       new Checklist() {ClientId = 14145135, QaAdvisor = new QaAdvisor{Id = Guid.NewGuid(), Forename = "Test", Surname = "Test"}}
                                     , new Checklist() {Id= expectedIdOfFoundChecklist, ClientId = 14145135, VisitBy = consultant, VisitDate = visitDate, QaAdvisor = new QaAdvisor{Id = Guid.NewGuid(), Forename = "Test", Surname = "Test"}}
                                     , new Checklist() {ClientId = 23452435, QaAdvisor = new QaAdvisor{Id = Guid.NewGuid(), Forename = "Test", Surname = "Test"}}
                                 };
            
            _queryableWrapper.Setup(x => x.Queryable())
                .Returns(() => checklists.AsQueryable());
            
            var target = new GetChecklistsQuery(_queryableWrapper.Object);
            var result = target
                .WithVisitDate(visitDate)
                .Execute();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().VisitDate, Is.EqualTo(visitDate));
            Assert.That(result.First().Id, Is.EqualTo(expectedIdOfFoundChecklist));
        }

        [Test]
        public void Given_a_visit_date_and_consultant_only_checklists_that_are_visited_by_consultant_at_date_entered_are_returned()
        {
            //given
            var visitDate = DateTime.Now;
            var expectedIdOfFoundChecklist = Guid.NewGuid();
            var consultant = "Mr H&S";
            var checklists = new List<Checklist>
                                 {
                                       new Checklist() {ClientId = 14145135, QaAdvisor = new QaAdvisor{Id = Guid.NewGuid(), Forename = "Test", Surname = "Test"}}
                                     , new Checklist() {Id = Guid.NewGuid(), ClientId = 14145135, VisitBy = consultant, VisitDate = visitDate.AddDays(1), ChecklistCreatedBy = consultant, QaAdvisor = new QaAdvisor{Id = Guid.NewGuid(), Forename = "Test", Surname = "Test"}}
                                     , new Checklist() {ClientId = 23452435, VisitDate = visitDate, VisitBy = "consultant", QaAdvisor = new QaAdvisor{Id = Guid.NewGuid(), Forename = "Test", Surname = "Test"}}
                                     , new Checklist() {Id = expectedIdOfFoundChecklist, ClientId = 23452435, VisitDate = visitDate, VisitBy = consultant, ChecklistCreatedBy = consultant, QaAdvisor = new QaAdvisor{Id = Guid.NewGuid(), Forename = "Test", Surname = "Test"}}
                                 };

            _queryableWrapper.Setup(x => x.Queryable())
                .Returns(() => checklists.AsQueryable());

            var target = new GetChecklistsQuery(_queryableWrapper.Object);
            
            //when
            var result = target
                .WithConsultantName(consultant)
                .WithVisitDate(visitDate)
                .Execute();

            //then
            
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().VisitDate, Is.EqualTo(visitDate));
            Assert.That(result.First().Id, Is.EqualTo(expectedIdOfFoundChecklist));
        }

        [Test]
        public void Given_with_status_when_execute_then_result_returns_correct_count()
        {
            //given
            var checklists = new List<Checklist>
                                 {
                                     new Checklist{Status = Checklist.STATUS_DRAFT},
                                     new Checklist{Status = Checklist.STATUS_SUBMITTED}
                                 };

            _queryableWrapper.Setup(x => x.Queryable())
                .Returns(() => checklists.AsQueryable());

            //when
            var target = new GetChecklistsQuery(_queryableWrapper.Object);
            var result = target.WithStatus(Checklist.STATUS_SUBMITTED).Execute();
            //then

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void Given_with_status_of_submitted_when_execute_then_result_returns_correct_count()
        {
            //given
            var expectedDate = new DateTime(2014, 01, 01, 13,05,0,0);

            var checklists = new List<Checklist>
                                 {
                                     new Checklist{Status = Checklist.STATUS_DRAFT},
                                     new Checklist{Status = Checklist.STATUS_SUBMITTED, ChecklistSubmittedOn = expectedDate},
                                     new Checklist{Status = Checklist.STATUS_SUBMITTED, ChecklistSubmittedOn = DateTime.Now}
                                 };

            _queryableWrapper.Setup(x => x.Queryable())
                .Returns(() => checklists.AsQueryable());

            //when
            var target = new GetChecklistsQuery(_queryableWrapper.Object);
            var result = target
                .WithStatus(Checklist.STATUS_SUBMITTED)
                .WithStatusDateBetween(expectedDate,expectedDate.AddDays(1))
                .Execute();
            //then

            Assert.That(result.Count, Is.EqualTo(1));
        }


        [Test]
        public void Given_with_status_of_draft_when_execute_then_result_returns_correct_count()
        {
            //given
            var expectedDate = new DateTime(2014, 01, 01);

            var checklists = new List<Checklist>
                                 {
                                     new Checklist{Status = Checklist.STATUS_DRAFT, ChecklistCreatedOn = expectedDate },
                                     new Checklist{Status = Checklist.STATUS_DRAFT, ChecklistCreatedOn = DateTime.Now},
                                     new Checklist{Status = Checklist.STATUS_SUBMITTED, ChecklistSubmittedOn = DateTime.Now}
                                 };

            _queryableWrapper.Setup(x => x.Queryable())
                .Returns(() => checklists.AsQueryable());

            //when
            var target = new GetChecklistsQuery(_queryableWrapper.Object);
            var result = target
                .WithStatus(Checklist.STATUS_DRAFT)
                .WithStatusDateBetween(expectedDate, expectedDate.AddDays(1))
                .Execute();
            //then

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void Given_with_status_of_completed_when_execute_then_result_returns_correct_count()
        {
            //given
            var expectedDate = new DateTime(2014, 01, 01);

            var checklists = new List<Checklist>
                                 {
                                     new Checklist{Status = Checklist.STATUS_DRAFT, ChecklistCreatedOn = DateTime.Now },
                                     new Checklist{Status = Checklist.STATUS_COMPLETED, ChecklistCompletedOn = expectedDate},
                                     new Checklist{Status = Checklist.STATUS_COMPLETED, ChecklistCompletedOn = DateTime.Now},
                                     new Checklist{Status = Checklist.STATUS_SUBMITTED, ChecklistSubmittedOn = DateTime.Now}
                                 };

            _queryableWrapper.Setup(x => x.Queryable())
                .Returns(() => checklists.AsQueryable());

            //when
            var target = new GetChecklistsQuery(_queryableWrapper.Object);
            var result = target
                .WithStatus(Checklist.STATUS_COMPLETED)
                .WithStatusDateBetween(expectedDate, expectedDate.AddDays(1))
                .Execute();
            //then

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void Given_with_status_of_assigned_when_execute_then_result_returns_correct_count()
        {
            //given
            var expectedDate = new DateTime(2014, 01, 01);

            var checklists = new List<Checklist>
                                 {
                                     new Checklist{Status = Checklist.STATUS_DRAFT, ChecklistCreatedOn = DateTime.Now },
                                     new Checklist{Status = Checklist.STATUS_ASSIGNED, QaAdvisorAssignedOn = expectedDate},
                                     new Checklist{Status = Checklist.STATUS_ASSIGNED, QaAdvisorAssignedOn = DateTime.Now},
                                     new Checklist{Status = Checklist.STATUS_SUBMITTED, ChecklistSubmittedOn = DateTime.Now}
                                 };

            _queryableWrapper.Setup(x => x.Queryable())
                .Returns(() => checklists.AsQueryable());

            //when
            var target = new GetChecklistsQuery(_queryableWrapper.Object);
            var result = target
                .WithStatus(Checklist.STATUS_ASSIGNED)
                .WithStatusDateBetween(expectedDate, expectedDate.AddDays(1))
                .Execute();
            //then

            Assert.That(result.Count, Is.EqualTo(1));
        }


        [Test]
        public void Given_with_deleted_and_status_date_when_execute_then_result_returns_correct_count()
        {
            //given
            var expectedDate = new DateTime(2014, 01, 01);

            var checklists = new List<Checklist>
                                 {
                                     new Checklist{Status = Checklist.STATUS_DRAFT, ChecklistCreatedOn = DateTime.Now, LastModifiedOn = expectedDate, Deleted = false},
                                     new Checklist{Status = Checklist.STATUS_DRAFT, ChecklistCreatedOn = DateTime.Now, LastModifiedOn = DateTime.Now, Deleted = true},
                                     new Checklist{Status = Checklist.STATUS_DRAFT, ChecklistCreatedOn = DateTime.Now, LastModifiedOn = expectedDate, Deleted = true},
                                     new Checklist{Status = Checklist.STATUS_COMPLETED, ChecklistCompletedOn = DateTime.Now},
                                     new Checklist{Status = Checklist.STATUS_ASSIGNED, QaAdvisorAssignedOn = DateTime.Now},
                                     new Checklist{Status = Checklist.STATUS_SUBMITTED, ChecklistSubmittedOn = DateTime.Now}
                                 };

            _queryableWrapper.Setup(x => x.Queryable())
                .Returns(checklists.AsQueryable);

            //when
            var target = new GetChecklistsQuery(_queryableWrapper.Object);
            var result = target
                .WithDeletedOnly()
                .WithStatusDateBetween(expectedDate, expectedDate.AddDays(1))
                .Execute();
            //then

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void Given_qaAdvisor_is_entered_then_do_not_throw_exception()
        {
            var target = new GetChecklistsQuery(_queryableWrapper.Object);
            Assert.DoesNotThrow(() => target.Execute());

        }

        [Test]
        public void Given_Checklist_index_with_no_QA_comments_and_no_Exec_Summary_comments_then_commentStatus_is_None()
        {
            ChecklistIndex index = new ChecklistIndex();

            index.HasQaComments = false;
            index.HasResolvedQaComments = false;
            index.ExecutiveSummaryUpdateRequired = false;
            index.ExecutiveSummaryQACommentsResolved = false;

            index.HasSignedOffQaComments = false;

            Assert.AreEqual("None", index.QACommentStatus());
        }

        [Test]
        public void Given_Checklist_index_with_unresolved_QA_comments_and_no_Exec_Summary_comments_then_commentStatus_is_HasUnresolvedQaComments()
        {
            ChecklistIndex index = new ChecklistIndex();

            index.HasQaComments = true;
            index.HasResolvedQaComments = false;
            index.HasSignedOffQaComments = false;

            index.ExecutiveSummaryUpdateRequired = false;
            index.ExecutiveSummaryQACommentsResolved = false;

            Assert.AreEqual("HasUnresolvedQaComments", index.QACommentStatus());
        }

        [Test]
        public void Given_Checklist_index_with_resolved_QA_comments_and_no_Exec_Summary_comments_then_commentStatus_is_AllQaCommentsResolved()
        {
            ChecklistIndex index = new ChecklistIndex();

            index.HasQaComments = true;
            index.HasResolvedQaComments = true;

            index.ExecutiveSummaryUpdateRequired = false;
            index.ExecutiveSummaryQACommentsResolved = false;

            index.HasSignedOffQaComments = false;

            Assert.AreEqual("AllQaCommentsResolved", index.QACommentStatus());
        }

        [Test]
        public void Given_Checklist_index_with_resolved_QA_comments_and_no_Exec_Summary_comments_and_QA_comments_signed_off_then_commentStatus_is_None()
        {
            ChecklistIndex index = new ChecklistIndex();

            index.HasQaComments = true;
            index.HasResolvedQaComments = true;
            index.HasSignedOffQaComments = true;

            index.ExecutiveSummaryUpdateRequired = false;
            index.ExecutiveSummaryQACommentsResolved = false;

            Assert.AreEqual("None", index.QACommentStatus());
        }

        [Test]
        public void Given_Checklist_index_with_unresolved_Exec_Summary_comments_then_commentStatus_is_HasUnresolvedQaComments()
        {
            ChecklistIndex index = new ChecklistIndex();

            index.HasQaComments = false;
            index.HasResolvedQaComments = false;

            index.ExecutiveSummaryUpdateRequired = true;
            index.ExecutiveSummaryQACommentsResolved = false;

            index.HasSignedOffQaComments = false;

            Assert.AreEqual("HasUnresolvedQaComments", index.QACommentStatus());
        }

        [Test]
        public void Given_Checklist_index_with_resolved_Exec_Summary_comments_then_commentStatus_is_AllQaCommentsResolved()
        {
            ChecklistIndex index = new ChecklistIndex();

            index.HasQaComments = false;
            index.HasResolvedQaComments = false;

            index.ExecutiveSummaryUpdateRequired = true;
            index.ExecutiveSummaryQACommentsResolved = true;
            index.ExecutiveSummaryQASignedOff = false;

            index.HasSignedOffQaComments = false;

            Assert.AreEqual("AllQaCommentsResolved", index.QACommentStatus());
        }

        [Test]
        public void Given_Checklist_index_with_resolved_Exec_Summary_comments_and_unresolved_qacomments_then_commentStatus_is_HasUnresolvedQaComments()
        {
            ChecklistIndex index = new ChecklistIndex();

            index.HasQaComments = true;
            index.HasResolvedQaComments = false;
            index.ExecutiveSummaryUpdateRequired = true;
            index.ExecutiveSummaryQACommentsResolved = true;

            Assert.AreEqual("HasUnresolvedQaComments", index.QACommentStatus());
        }

        [Test]
        public void Given_Checklist_index_with_signed_off_Exec_Summary_comments_and_signed_off_qacomments_then_commentStatus_is_None()
        {
            ChecklistIndex index = new ChecklistIndex();

            index.HasQaComments = true;
            index.HasResolvedQaComments = true;
            index.HasSignedOffQaComments = true;

            index.ExecutiveSummaryUpdateRequired = true;
            index.ExecutiveSummaryQACommentsResolved = true;
            index.ExecutiveSummaryQASignedOff = true;

            

            Assert.AreEqual("None", index.QACommentStatus());
        }

        [Test]
        public void Given_Checklist_index_with_signed_off_Exec_Summary_comments_then_commentStatus_is_None()
        {
            ChecklistIndex index = new ChecklistIndex();

            index.HasQaComments = false;
            index.HasResolvedQaComments = false;
            index.HasSignedOffQaComments = false;

            index.ExecutiveSummaryUpdateRequired = true;
            index.ExecutiveSummaryQACommentsResolved = true;
            index.ExecutiveSummaryQASignedOff = true;

            

            Assert.AreEqual("None", index.QACommentStatus());
        }

        [Test]
        public void Given_Checklist_index_with_signed_off_comments_and_resolved_executive_summary_then_commentStatus_is_AllQaCommentsResolved()
        {
            ChecklistIndex index = new ChecklistIndex();

            index.HasQaComments = true;
            index.HasResolvedQaComments = true;
            index.HasSignedOffQaComments = true;

            index.ExecutiveSummaryUpdateRequired = true;
            index.ExecutiveSummaryQACommentsResolved = true;
            index.ExecutiveSummaryQASignedOff = false;



            Assert.AreEqual("AllQaCommentsResolved", index.QACommentStatus());
        }

    }
}
