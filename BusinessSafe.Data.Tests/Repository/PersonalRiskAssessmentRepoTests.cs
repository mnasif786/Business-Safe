using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Data.Repository;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NUnit.Framework;
using Moq;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Tests.Repository
{
    [TestFixture]
    public class PersonalRiskAssessmentRepoTests
    {
        private Employee _employee;
        private User _user;
        private long _companyId = 13123123;

        [SetUp]
        public void Setup()
        {
            _user = new User() { Id = Guid.NewGuid(), CompanyId = _companyId };
            _employee = new Employee() { Id = Guid.NewGuid(), CompanyId = _companyId };

            _user.Employee = _employee;
            _employee.User = _user;
        }

        private Mock<PersonalRiskAssessmentRepository> GetTarget()
        {
            var sessionManager = new Mock<IBusinessSafeSessionManager>();
            sessionManager.Setup(x => x.Session.Get<User>(It.IsAny<object>()))
                .Returns(_user);

            var args = new[] {sessionManager.Object};

            var target = new Mock<PersonalRiskAssessmentRepository>(args) {CallBase = true};

            return target;
        }

        [Test]
        public void Given_IsSensitive_is_false_then_PersonalRiskAssessment_returned()
        {
            var pras = new List<PersonalRiskAssessment>();
            var pra = PersonalRiskAssessment.Create("title", "reference", _companyId, new UserForAuditing() { Id = Guid.NewGuid() });
            pra.Sensitive = false;
            pra.RiskAssessmentSite = new Site();
            pra.RiskAssessor = new RiskAssessor() { Employee = _employee };
            pras.Add(pra);

            var target = GetTarget();

            target.Setup(x => x.PersonalRiskAssessments())
                .Returns(pras.AsQueryable);

            target.Setup(x => x.RiskAssessmentReviews())
               .Returns(pras.SelectMany(x=>x.Reviews).AsQueryable);

            var result = target.Object.Search(null, _companyId, null, null, null, null, null, _user.Id, false, false, 1, 10, RiskAssessmentOrderByColumn.Title, OrderByDirection.Ascending);

            //THEN
            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Given_IsSensitive_is_true_then_PersonalRiskAssessment_not_returned()
        {
            //GIVEN
            var pras = new List<PersonalRiskAssessment>();
            var pra = PersonalRiskAssessment.Create("title", "reference", _companyId, new UserForAuditing() { Id = Guid.NewGuid() });
            pra.Sensitive = true;
            pra.RiskAssessmentSite = new Site();
            pras.Add(pra);

            var target = GetTarget();

            target.Setup(x => x.PersonalRiskAssessments())
                .Returns(pras.AsQueryable);

            target.Setup(x => x.RiskAssessmentReviews())
               .Returns(pras.SelectMany(x => x.Reviews).AsQueryable);

            var result = target.Object.Search(null, _companyId, null, null, null, null, null, _user.Id, false, false, 1, 10, RiskAssessmentOrderByColumn.Title, OrderByDirection.Ascending);

            //THEN
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public void Given_IsSensitive_and_created_by_user_then_PersonalRiskAssessment_returned()
        {
            var pras = new List<PersonalRiskAssessment>();
            var pra = PersonalRiskAssessment.Create("title", "reference", _companyId, new UserForAuditing() { Id = _user.Id });
            pra.Sensitive = true;
            pra.RiskAssessmentSite = new Site();
            pras.Add(pra);

            var target = GetTarget();

            target.Setup(x => x.PersonalRiskAssessments())
                .Returns(pras.AsQueryable);

            target.Setup(x => x.RiskAssessmentReviews())
               .Returns(pras.SelectMany(x => x.Reviews).AsQueryable);

            var result = target.Object.Search(null, _companyId, null, null, null, null, null, _user.Id, false, false, 1, 10, RiskAssessmentOrderByColumn.Title, OrderByDirection.Ascending);

            //THEN
            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Given_IsSensitive_and_not_created_by_user_and_assigned_review_then_PersonalRiskAssessment_returned()
        {
            var pras = new List<PersonalRiskAssessment>();
            var pra = PersonalRiskAssessment.Create("title", "reference", _companyId, new UserForAuditing() {Id = Guid.NewGuid()});
            pra.Sensitive = true;
            pra.RiskAssessmentSite = new Site();
            pra.AddReview(new RiskAssessmentReview() {Id =123, ReviewAssignedTo = _employee});
            pras.Add(pra);

            var target = GetTarget();

            target.Setup(x => x.PersonalRiskAssessments())
                .Returns(pras.AsQueryable);

            target.Setup(x => x.RiskAssessmentReviews())
               .Returns(pras.SelectMany(x => x.Reviews).AsQueryable);

            var result = target.Object.Search(null, _companyId, null, null, null, null, null, _user.Id, false, false, 1, 10, RiskAssessmentOrderByColumn.Title, OrderByDirection.Ascending);

            //THEN
            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Given_IsSensitive_and_not_created_by_user_and_is_risk_assessor_then_PersonalRiskAssessment_returned()
        {
            var pras = new List<PersonalRiskAssessment>();
            var pra = PersonalRiskAssessment.Create("title", "reference", _companyId, new UserForAuditing() {Id = Guid.NewGuid()});
            pra.Sensitive = true;
            pra.RiskAssessmentSite = new Site();
            pra.RiskAssessor = new RiskAssessor() {Employee = _employee};
            pras.Add(pra);

            var target = GetTarget();

            target.Setup(x => x.PersonalRiskAssessments())
                .Returns(pras.AsQueryable);

            target.Setup(x => x.RiskAssessmentReviews())
               .Returns(pras.SelectMany(x => x.Reviews).AsQueryable);

            var result = target.Object.Search(null, _companyId, null, null, null, null, null, _user.Id, false, false, 1, 10, RiskAssessmentOrderByColumn.Title, OrderByDirection.Ascending);

            //THEN
            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Given_IsSensitive_and_not_created_by_user_and_not_assigned_to_latest_review_then_PersonalRiskAssessment_not_returned()
        {
            var pras = new List<PersonalRiskAssessment>();
            var pra = PersonalRiskAssessment.Create("title", "reference", _companyId, new UserForAuditing() { Id = Guid.NewGuid() });
            pra.Sensitive = true;
            pra.RiskAssessmentSite = new Site();
            pra.AddReview(new RiskAssessmentReview() {Id = 111, CreatedOn = DateTime.Now.AddDays(-10), ReviewAssignedTo = _employee});
            pra.AddReview(new RiskAssessmentReview() {Id=222, CreatedOn = DateTime.Now.AddDays(10),ReviewAssignedTo = new Employee() { User = new User() { Id = Guid.NewGuid() } } }); //the latest review no assigned to the user
            pras.Add(pra);

            var target = GetTarget();

            target.Setup(x => x.PersonalRiskAssessments())
                .Returns(pras.AsQueryable);

            target.Setup(x => x.RiskAssessmentReviews())
               .Returns(pras.SelectMany(x => x.Reviews).AsQueryable);

            var result = target.Object.Search(null, _companyId, null, null, null, null, null, _user.Id, false, false, 1, 10, RiskAssessmentOrderByColumn.Title, OrderByDirection.Ascending);

            //THEN
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public void Given_IsSensitive_and_not_created_by_user_and_assigned_to_a_deleted_review_then_PersonalRiskAssessment_not_returned()
        {
            var pras = new List<PersonalRiskAssessment>();
            var pra = PersonalRiskAssessment.Create("title", "reference", _companyId, new UserForAuditing() { Id = Guid.NewGuid() });
            pra.Sensitive = true;
            pra.RiskAssessmentSite = new Site();
            pra.AddReview(new RiskAssessmentReview() {Id =12, Deleted = true, ReviewAssignedTo = _employee });
            pras.Add(pra);

            var target = GetTarget();

            target.Setup(x => x.PersonalRiskAssessments())
                .Returns(pras.AsQueryable);

            target.Setup(x => x.RiskAssessmentReviews())
               .Returns(pras.SelectMany(x => x.Reviews).AsQueryable);

            var result = target.Object.Search(null, _companyId, null, null, null, null, null, _user.Id, false, false, 1, 10, RiskAssessmentOrderByColumn.Title, OrderByDirection.Ascending);

            //THEN
            Assert.That(result.Count(), Is.EqualTo(0));
        }
    }
}
