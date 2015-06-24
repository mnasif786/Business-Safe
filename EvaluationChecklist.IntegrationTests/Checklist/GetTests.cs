using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using BusinessSafe.Data.NHibernate.BusinessSafe;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Domain.RepositoryContracts.SafeCheck;
using EvaluationChecklist.Models;
using Moq;
using NServiceBus;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using RestSharp;
using BusinessSafe.Data.Repository.SafeCheck;
using BusinessSafe.Application.Common;
using SC=BusinessSafe.Domain.Entities.SafeCheck;
using StructureMap;
using IQuestionRepository = BusinessSafe.Domain.RepositoryContracts.SafeCheck.IQuestionRepository;

namespace EvaluationChecklist.IntegrationTests.Checklist
{
    [TestFixture]
    public class GetTests : BaseIntegrationTest
    {
        private ICheckListRepository _checklistRepository;
        private IQuestionRepository _questionRepository;
        private IQuestionResponseRepository _questionResponseRepository;
        private IUserForAuditingRepository _userForAuditingRepository;
        private SC.Checklist _checklist;
        private int _totalNumberOfChecklistQuestions;
        private int _totalNumberOfChecklistAnswers;

        [SetUpFixture]
        public class SetupClass
        {
            [SetUp]
            public void RunBeforeAnyTestInThisNamespace()
            {
                ObjectFactory.Initialize(x =>
                {
                    x.ForSingletonOf<IBusinessSafeSessionFactory>().Use<BusinessSafeSessionFactory>();
                    x.For<IBusinessSafeSessionManager>().HybridHttpOrThreadLocalScoped().Use<BusinessSafeSessionManager>();
                    x.For<IBus>().Use(new Mock<IBus>().Object);
                    x.AddRegistry<ApplicationRegistry>();
                });

                //create the session factory
                var sessionFactory = ObjectFactory.Container.GetInstance<IBusinessSafeSessionFactory>();
            }
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            ResourceUrl = string.Format("{0}{1}/", ApiBaseUrl, "Checklists");
           
            _checklistRepository = ObjectFactory.GetInstance<ICheckListRepository>();
            _questionRepository = ObjectFactory.GetInstance<IQuestionRepository>();
            _questionResponseRepository = ObjectFactory.GetInstance<IQuestionResponseRepository>();
            _userForAuditingRepository = ObjectFactory.GetInstance<IUserForAuditingRepository>();

            //Creats a new checklist if does not exist.
            _checklist = _checklistRepository.GetById(ApiTestChecklistId);

            if (_checklist == null)
            {
                
                _checklist = new SC.Checklist()
                {
                    Id = ApiTestChecklistId,
                    ClientId = ApiTestClientId,
                    SiteId = APiTestSiteId,
                    CoveringLetterContent = ApiTestCoveringLetterContents,
                    CreatedBy = _userForAuditingRepository.GetSystemUser(),
                   CreatedOn = DateTime.Now,
                    LastModifiedBy = _userForAuditingRepository.GetSystemUser(),
                    LastModifiedOn = DateTime.Now
                };

                var questions = _questionRepository.GetAll().Take(1).ToList();
                
                //_checklist.UpdateQuestions(questions, new UserForAuditing());
                foreach (var question in questions)
                {
                    _checklist.Questions.Add(new ChecklistQuestion
                    {
                        Checklist = _checklist,
                        Question = question,
                        CreatedBy = _userForAuditingRepository.GetSystemUser(),
                        CreatedOn = DateTime.Now,
                        LastModifiedBy = _userForAuditingRepository.GetSystemUser(),
                        LastModifiedOn = DateTime.Now
                    });
                }

                var response = questions.First().PossibleResponses
                    .Where(r => r.ResponseType.Equals("Positive"))
                    .Select(
                        r => new SC.ChecklistAnswer()
                        {
                            SupportingEvidence = r.SupportingEvidence,
                            ActionRequired = r.ActionRequired,
                            Question = questions.First(),
                            Response = r,
                            CreatedBy = _userForAuditingRepository.GetSystemUser(),
                            CreatedOn = DateTime.Now,
                            LastModifiedBy = _userForAuditingRepository.GetSystemUser(),
                            LastModifiedOn = DateTime.Now
                        })
                    .ToList();

                _checklist.UpdateAnswers(response, _userForAuditingRepository.GetSystemUser());

                _checklistRepository.SaveOrUpdate(_checklist);
            }

            _totalNumberOfChecklistQuestions = _checklist.Questions.Count();
            _totalNumberOfChecklistAnswers = _checklist.Answers.Count();

            ObjectFactory.Container.GetInstance<IBusinessSafeSessionManager>().CloseSession();
            
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            //dont delete because we want to use the checklist again
            var sessionManager = ObjectFactory.GetInstance<IBusinessSafeSessionManager>();
            sessionManager.CloseSession();
        }
  

        [Test]
        public void Given_Checklist_Exists_When_Get_Then_Returns_Checklist()
        {
            // Given
            var client = new RestClient(Url.AbsoluteUri);
            client.Authenticator = new NtlmAuthenticator("continuous.int", "is74rb80pk52");
            var request = new RestRequest(ResourceUrl + ApiTestChecklistId);

            // When
            var response = client.Execute(request);

            // Then
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ChecklistViewModel>(response.Content);

            Assert.IsNotNull(result, "Checklist should contain data.");
            Assert.IsTrue(result.Id == ApiTestChecklistId);
        }

       
    }
}
