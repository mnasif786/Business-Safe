using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BusinessSafe.Application.Common;
using BusinessSafe.Data.NHibernate.BusinessSafe;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.RepositoryContracts.SafeCheck;
using EvaluationChecklist.Models;
using Moq;
using NServiceBus;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using RestSharp;
using StructureMap;

namespace EvaluationChecklist.IntegrationTests.Checklist
{
    [TestFixture]
    public class PostChecklist : BaseIntegrationTest
    {
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
        
        [TearDown]
        public void TearDown()
        {
            var sessionManager = ObjectFactory.GetInstance<IBusinessSafeSessionManager>();
            sessionManager.CloseSession();
        }

        [Test]
        public void Given_A_Checklist_Is_Being_Saved_Then_Returns_Status_OK()
        {
            // Given
            var client = new RestClient(Url.AbsoluteUri);
            client.Authenticator = new NtlmAuthenticator( "continuous.int","is74rb80pk52" );

            const int numberOfRequestsToSend = 15;
            var stopWatch = new System.Diagnostics.Stopwatch();
            stopWatch.Start();
            var parallelLoopResult = Parallel.For(0, numberOfRequestsToSend, x =>
                                                                                 {
                                                                                     //GIVEN
                                                                                     var model = CreateChecklistViewModel();
                                                                                     var resourceUrl = string.Format("{0}{1}/{2}", ApiBaseUrl, "checklists", model.Id.ToString());
                                                                                     var request = new RestRequest(resourceUrl);
                                                                                     request.AddHeader("Content-Type", "application/json");
                                                                                     request.RequestFormat = DataFormat.Json;
                                                                                     request.Method = Method.POST;
                                                                                     request.AddBody(model);

                                                                                     // When
                                                                                     var response = client.Execute(request);

                                                                                     //THEN
                                                                                     Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                                                                                 });

            stopWatch.Stop();

            var processingSeconds = TimeSpan.FromMilliseconds(stopWatch.ElapsedMilliseconds).TotalSeconds;
            Assert.That(parallelLoopResult.IsCompleted);
            Console.WriteLine(string.Format("average: {0}", processingSeconds / numberOfRequestsToSend));
        }

        private ChecklistViewModel CreateChecklistViewModel()
        {
            var model = new ChecklistViewModel()
                            {
                                Id = Guid.NewGuid(),
                                ClientId = 55881,
                                SiteId = 1331348,
                                Site = new SiteViewModel() {Id = 222, Address1 = "Address", Postcode = "MA 1AA"},
                                CoveringLetterContent = "Letter content",
                                Questions = new List<QuestionAnswerViewModel>(),
                                Categories = new List<CategoryQuestionAnswerViewModel>(),
                                CreatedOn = DateTime.Now,
                                Status = "Draft",
                                SiteVisit = new SiteVisitViewModel()
                                                {
                                                    AreasVisited = "Areas Visited", AreasNotVisited = "Areas Not Visited", EmailAddress = "email",
                                                    PersonSeen = new PersonSeenViewModel() {JobTitle = "Job", Name = "Name", Salutation = "Salutatiuon"},
                                                    SelectedImpressionType = new ImpressionTypeViewModel() {Id = Guid.NewGuid()},
                                                    VisitBy = "VisitBy", VisitDate = "10/10/2013", VisitType = "Visit Type"
                                                },
                                ImmediateRiskNotifications = new List<ImmediateRiskNotificationViewModel>(),
                                Submit = false,
                                CreatedBy = "John Smithy"
                            };

            GetQuestions(150)
                .ToList()
                .ForEach(x => model.Questions.Add(CreateQuestionAnswerViewModel(x)));
            return model;
        }

        private IEnumerable<Question> GetQuestions(int numberOfQuestions)
        {
            var _questionRepository = ObjectFactory.GetInstance<IQuestionRepository>();

            var questions = _questionRepository.GetAll();

            return questions.Take(numberOfQuestions);
        }

        private static QuestionAnswerViewModel CreateQuestionAnswerViewModel(Question question)
        {
            var questionResponse = question.PossibleResponses.FirstOrDefault();
            return new QuestionAnswerViewModel(){
                Answer = new AnswerViewModel() {
                    ActionRequired = questionResponse != null ? questionResponse.ActionRequired : null
                    , GuidanceNotes = "1.4"
                    , QuestionId = question.Id
                    , SupportingEvidence = "the supporting evidence"
                    ,SelectedResponseId = questionResponse != null ? questionResponse.Id : (Guid?) null
                    
                      }
                ,Question = new QuestionViewModel()
                                {
                                    Id = question.Id
                                }};
        }
    }
}
