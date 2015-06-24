using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using BusinessSafe.Application.Common;
using BusinessSafe.Data.NHibernate.BusinessSafe;
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
    public class PostTests : BaseIntegrationTest
    {       
        private IImpressionTypeRepository _impressionTypeRepository;
        private Guid _impressionTypeId;
		
		public PostTests()
        {
           
        }
		
        public void AssertNameIsInList(string name, IList<Parameter> list)
        {
            if (!list.Any(x => x.Name == name))
            {
                Assert.Fail(string.Format("{0} does not exist in list", name));
            }
        }

            
        public static object[] ResourceURLs
        {
            get
            {
                return new string[]
                           {
                               string.Format("{0}client/questions", ApiBaseUrl),
                               string.Format("{0}categories", ApiBaseUrl),
                               string.Format("{0}industries", ApiBaseUrl),
                           };
            }
        }

        [Test, TestCaseSource("ResourceURLs")]
        public void TestOptions(string resourceUrl)
        {

            var client = new RestClient(Url.AbsoluteUri);
            //client.BaseUrl
            var request = new RestRequest(resourceUrl);
            request.AddHeader("host", Url.Authority);
            request.AddHeader("Access-Control-Request-Method", "GET");
            request.AddHeader("Origin", "http://localhost:8088");
            request.AddHeader("Access-Control-Request-Headers", "accept, origin, authenticationtoken, content-type");
            request.AddHeader("Referer", "http://localhost:8088");
            request.Method = Method.OPTIONS;

            var response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            AssertNameIsInList("Access-Control-Allow-Headers", response.Headers);
            AssertNameIsInList("Access-Control-Allow-Methods", response.Headers);
            AssertNameIsInList("Access-Control-Allow-Origin", response.Headers);
            
        }

        
        [TestFixtureSetUp]
        public void Setup()
        {
            _impressionTypeRepository = ObjectFactory.GetInstance<IImpressionTypeRepository>();
            _impressionTypeId =_impressionTypeRepository.GetAll().Select( i => i.Id).First();
            ObjectFactory.Container.GetInstance<IBusinessSafeSessionManager>().CloseSession();

        }
       


        [Test]
        public void Given_A_New_Checklist_Is_Being_Saved_Then_Returns_Status_OK()
        {            
            ChecklistViewModel model = new ChecklistViewModel()
            {
                Id = Guid.NewGuid(),
                SiteId = 1234,
                Site = new SiteViewModel() { Id = 222, Address1 = "Address", Postcode = "MA 1AA" },
            };

            model.SiteVisit = new SiteVisitViewModel()
            {
                AreasNotVisited = "Ground floor",
                AreasVisited = "First floor",
                EmailAddress = "email@server.com",
                VisitDate = DateTime.Now.Date.ToShortDateString(),
                VisitBy = "ABC",
                VisitType = "Fire & Safety check",
                PersonSeen = new PersonSeenViewModel() { Name = "Richard",JobTitle = "Consultant",Salutation = "Mr."}
            };

            model.SiteVisit.SelectedImpressionType = new ImpressionTypeViewModel() {Id = _impressionTypeId};

            model.Questions = new List<QuestionAnswerViewModel>();
            model.Questions.Add(new QuestionAnswerViewModel()
            {
                Question = new QuestionViewModel()
                             {
                                 Id = Guid.Parse("614D9A92-78ED-4D37-BD86-BD963C053EC5"),
                                 PossibleResponses = new List<QuestionResponseViewModel>(),
                                 Text = "Question 1"
                             },
                Answer = new AnswerViewModel()
                             {
                                 QuestionId = Guid.Parse("614D9A92-78ED-4D37-BD86-BD963C053EC5"),
                                 SelectedResponseId = Guid.Parse("A415F8B0-65F8-4947-B390-0018465F46CE"),
                                 AssignedTo = new AssignedToViewModel() { Id = Guid.Empty, NonEmployeeName= "Nauman Asif"}
                             }
            }
            );

            ResourceUrl = string.Format("{0}{1}/{2}", ApiBaseUrl, "checklists", model.Id.ToString());

            // Given
            var client = new RestClient(Url.AbsoluteUri);
            var request = new RestRequest(ResourceUrl);
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.Method = Method.POST;
            request.AddBody(model);

           
            // When
            var response = client.Execute(request);

            // Then
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);          
        }

       }
}

