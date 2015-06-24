using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using EvaluationChecklist.Models;
using NUnit.Framework;
using RestSharp;

namespace EvaluationChecklist.IntegrationTests.Checklist
{
    [TestFixture]
    public class PostBespokeQuestion : BaseIntegrationTest
    {        
        [Test]
        public void Given_A_Bespoke_Question_Is_Being_Saved_Then_Returns_Status_OK()
        {
            QuestionViewModel model = new QuestionViewModel()
            {
                Id = Guid.NewGuid(), 
                Text = "Is there life on Mars?",
                CategoryId = Guid.Parse("3DEE8018-575E-4609-A5AC-C1DE2475C2DB"),      // Employees                             
            };

            model.PossibleResponses = new List<QuestionResponseViewModel>();
            model.PossibleResponses.Add( new QuestionResponseViewModel()    { Id = Guid.NewGuid(), ResponseType = "Neutral",  Title="Improvement Required" });
            model.PossibleResponses.Add( new QuestionResponseViewModel()    { Id = Guid.NewGuid(), ResponseType = "Positive", Title = "Acceptable" });
            model.PossibleResponses.Add( new QuestionResponseViewModel()    { Id = Guid.NewGuid(), ResponseType = "Negative", Title = "Unacceptable" });

            model.Category = new CategoryViewModel() {Id = model.CategoryId};

            Guid checklistID = Guid.Parse("EDAD1451-EF0F-2D38-16BF-BBFF6920C192");
            ResourceUrl = string.Format("{0}{1}?newQuestionId={2}&checklistId={3}", ApiBaseUrl, "question", model.Id.ToString(), checklistID);

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
