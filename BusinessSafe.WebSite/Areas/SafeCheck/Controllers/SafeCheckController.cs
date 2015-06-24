using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using RestSharp;
using BusinessSafe.WebSite.Areas.SafeCheck.Models;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.AuthenticationService;

namespace BusinessSafe.WebSite.Areas.SafeCheck.Controllers
{

    public class JSONQuestion
    {
        public string Id { get; set; }
        public string Text { get; set; }      
    }

    public class JSONAnswer
    {
        public string Id { get; set; }

        public JSONQuestion Question { get; set; }

        public int SelectedAnswer { get; set; }

        public string Comment { get; set; }

    }

    public class JSONQuestionAnswer
    {
        public JSONQuestion Question { get; set; }

        public JSONAnswer Answer { get; set; }

    }

   


    public class JSONChecklist
    {
        public string CompanyName { get; set; }
        public string Id { get; set; }

       // public List<JSONQuestion> questions { get; set; }

      //  public List<JSONAnswere> answers { get; set; }

        public List<JSONQuestionAnswer> QuestionAnswers { get; set; }


    }

    public class SafeCheckController : BaseController
    {
        //
        // GET: /SafeCheck/SafeCheck/

        [PermissionFilter(Permissions.ViewPersonalRiskAssessments)]
        public ActionResult Index()
        {
            //Get Results from API
            //var url = ConfigurationManager.AppSettings["SafeCheckChecklistAPI"];

            // Using developer machine (SGG) to avoid this showing up on UAT/Live
            var url = "http://PBS43753:8345/api/";
            var client = new RestClient(url );
            var request = new RestRequest("checklist?id=17c0dac5-f940-4674-aeb5-7be0409e97f6", Method.GET);            
            IRestResponse<JSONChecklist> response2 = client.Execute<JSONChecklist>(request);
            
            
            CheckListViewModel model = new CheckListViewModel();
           
            model.CompanyName = response2.Data.CompanyName;
            model.Questions = new List<QuestionViewModel>();

            foreach (var qa in response2.Data.QuestionAnswers)
            {
                QuestionViewModel qvm = new QuestionViewModel();
                qvm.Text = qa.Question.Text;

                qvm.Answer = new AnswerViewModel();
                qvm.Answer.PossibleResponses = new List<string>();
                qvm.Answer.PossibleResponses.Add("----------");
                qvm.Answer.PossibleResponses.Add("Acceptable");
                qvm.Answer.PossibleResponses.Add("AttentionRequired");
                qvm.Answer.PossibleResponses.Add("Unacceptable");
                qvm.Answer.PossibleResponses.Add("NotRequired");

                qvm.Answer.SelectedResponse = qa.Answer.SelectedAnswer;
                qvm.Answer.Comment = qa.Answer.Comment;
                model.Questions.Add(qvm);
            }


            return View(model);
        }

    }
}
