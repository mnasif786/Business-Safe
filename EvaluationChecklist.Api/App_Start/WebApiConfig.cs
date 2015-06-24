using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace EvaluationChecklist.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DeleteChecklist",
                routeTemplate: "api/deletechecklist/{id}",
                defaults: new { controller = "Checklist", action = "DeleteChecklist", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") });

            config.Routes.MapHttpRoute(
                name: "Questions",
                routeTemplate: "api/questions",
                defaults: new {controller = "Question", action = "GetALLQuestions"});

            config.Routes.MapHttpRoute(
                name: "ClientQuestions",
                routeTemplate: "api/client/{id}/questions",
                defaults: new { controller = "Question", action = "GetQuestionsByClient" });

            config.Routes.MapHttpRoute(
                name: "AllClientQuestions",
                routeTemplate: "api/client/questions",
                defaults: new { controller = "Question", action = "GetAllClientQuestions" }
                , constraints: new { httpMethod = new HttpMethodConstraint("GET") });
            
            config.Routes.MapHttpRoute(
                name: "PostQuestion",
                routeTemplate: "api/question/{id}",
                defaults: new {controller = "Question", action = "PostQuestion", id = UrlParameter.Optional}
                , constraints: new {httpMethod = new HttpMethodConstraint("POST")});

            config.Routes.MapHttpRoute(
                name: "AllClientQuestionsOptions",
                routeTemplate: "api/client/questions",
                defaults: new { controller = "Question", action = "Options" }
                , constraints: new { httpMethod = new HttpMethodConstraint("Options") });

            config.Routes.MapHttpRoute(
                name: "Categories",
                routeTemplate: "api/categories",
                defaults: new { controller = "Category", action = "Get" }
                , constraints: new { httpMethod = new HttpMethodConstraint("GET") });

            config.Routes.MapHttpRoute(
                name: "CategoriesOptions",
                routeTemplate: "api/categories",
                defaults: new { controller = "Category", action = "Options" }
                , constraints: new {httpMethod = new HttpMethodConstraint("Options")});

            config.Routes.MapHttpRoute(
                name: "ClientByCAN",
                routeTemplate: "api/clients/query/{clientAccountNumber}",
                defaults: new {controller = "Client", action = "Query"}
                , constraints: new {httpMethod = new HttpMethodConstraint("GET")});

            config.Routes.MapHttpRoute(
                name: "EmployyesByClient",
                routeTemplate: "api/clients/{clientId}/Employees",
                defaults: new { controller = "Client", action = "GetEmployees" }
                , constraints: new { httpMethod = new HttpMethodConstraint("GET") });

            config.Routes.MapHttpRoute(
                name: "ClientById",
                routeTemplate: "api/clients/{id}",
                defaults: new {controller = "Client", action = "Get"}
                , constraints: new {httpMethod = new HttpMethodConstraint("GET")});

            config.Routes.MapHttpRoute(
                name: "ClientByIdOPTIONS",
                routeTemplate: "api/clients/{id}",
                defaults: new { controller = "Client", action = "Options" }
                , constraints: new { httpMethod = new HttpMethodConstraint("OPTIONS") });

            config.Routes.MapHttpRoute(
                name: "Checklist",
                routeTemplate: "api/checklists/{id}",
                defaults: new {controller = "Checklist", action = "GetChecklist"}
                , constraints: new {httpMethod = new HttpMethodConstraint("GET")});

            //config.Routes.MapHttpRoute(
            //    name: "AllChecklists",
            //    routeTemplate: "api/checklists",
            //    defaults: new {controller = "Checklist", action = "GetAll"}
            //    , constraints: new {httpMethod = new HttpMethodConstraint("GET")});

            config.Routes.MapHttpRoute(
                name: "PostChecklist",
                routeTemplate: "api/checklists/{id}",
                defaults: new {controller = "Checklist", action = "PostChecklist", id = UrlParameter.Optional}
                , constraints: new {httpMethod = new HttpMethodConstraint("POST")});

            config.Routes.MapHttpRoute(
              name: "QueryChecklists",
              routeTemplate: "api/checklistsquery",
              defaults: new { controller = "Checklist", action = "QueryUsingQueryObject" }
              , constraints: new { httpMethod = new HttpMethodConstraint("GET") });


            config.Routes.MapHttpRoute(
                name: "PostChecklistOPTIONS",
                routeTemplate: "api/checklists/{id}",
                defaults: new {controller = "Checklist", action = "Options"}
                , constraints: new {httpMethod = new HttpMethodConstraint("OPTIONS")});

            config.Routes.MapHttpRoute(
                name: "GetChecklistsByClientId",
                routeTemplate: "api/clients/{clientId}/checklists",
                defaults: new { controller = "Checklist", action = "GetByClientId" }
                , constraints: new {httpMethod = new HttpMethodConstraint("GET")});

            config.Routes.MapHttpRoute(
                name: "GetChecklistsByClientIdOPTIONS",
                routeTemplate: "api/clients/{clientId}/checklists",
                defaults: new { controller = "Checklist", action = "Options" }
                , constraints: new { httpMethod = new HttpMethodConstraint("OPTIONS") });

            config.Routes.MapHttpRoute(
               name: "Industries",
               routeTemplate: "api/industries",
               defaults: new { controller = "Industry", action = "Get" }
                , constraints: new { httpMethod = new HttpMethodConstraint("GET") });

            config.Routes.MapHttpRoute(
               name: "IndustriesOptions",
               routeTemplate: "api/industries",
               defaults: new { controller = "Industry", action = "Options" }
               , constraints: new { httpMethod = new HttpMethodConstraint("OPTIONS") });


            config.Routes.MapHttpRoute(
                name: "Advisors",
                routeTemplate: "api/advisors",
                defaults: new { controller = "QaAdvisor", action = "Get" }
                , constraints: new {httpMethod = new HttpMethodConstraint("GET")});

            config.Routes.MapHttpRoute(
                name: "AdvisorsOptions",
                routeTemplate: "api/advisors",
                defaults: new { controller = "QaAdvisor", action = "Options" }
                , constraints: new {httpMethod = new HttpMethodConstraint("OPTIONS")});


            config.Routes.MapHttpRoute(
               name: "CompleteSetOfQuestions",
               routeTemplate: "api/completesetofquestions",
               defaults: new { controller = "Question", action = "GetCompleteSetOfQuestions" }
               , constraints: new { httpMethod = new HttpMethodConstraint("GET") });
			   
			config.Routes.MapHttpRoute(
              name: "GetImpressionTypes",
              routeTemplate: "api/impressions",
              defaults: new { controller = "Checklist", action = "GetImpressionTypes" }
              , constraints: new { httpMethod = new HttpMethodConstraint("GET") });

            config.Routes.MapHttpRoute(
              name: "ChecklistGetDistinctCreatedBy",
              routeTemplate: "api/checklist/getdistinctcreatedby",
              defaults: new { controller = "Checklist", action = "GetDistinctCreatedBy" }
              , constraints: new { httpMethod = new HttpMethodConstraint("GET") });

            config.Routes.MapHttpRoute(
               name: "AssignChecklistToQaAdvisor",
               routeTemplate: "api/checklist/{id}/AssignChecklistToQaAdvisor",
               defaults: new { controller = "Checklist", action = "AssignChecklistToQaAdvisor", id = UrlParameter.Optional }
               , constraints: new { httpMethod = new HttpMethodConstraint("POST") });

            config.Routes.MapHttpRoute(
               name: "SendUpdateRequiredEmailNotification",
               routeTemplate: "api/updaterequirenotification/checklist/{id}",
               defaults: new { controller = "Checklist", action = "SendUpdateRequiredEmailNotification", id = UrlParameter.Optional }
               , constraints: new { httpMethod = new HttpMethodConstraint("POST") });
            
            config.Routes.MapHttpRoute(
               name: "SendChecklistCompleteEmailNotification",
               routeTemplate: "api/checklists/SendChecklistCompleteEmailNotification/{id}",
               defaults: new { controller = "Checklist", action = "SendChecklistCompleteEmailNotification", id = UrlParameter.Optional }
               , constraints: new { httpMethod = new HttpMethodConstraint("POST") });
          
           RouteTable.Routes.MapRoute("ActionPlanHTML", "api/reports/actionplan",
                                       new {controller = "Document", action = "ActionPlan", returnUrl = UrlParameter.Optional}
                                       , constraints: new {httpMethod = new HttpMethodConstraint("POST")});

            RouteTable.Routes.MapRoute("ActionPlanPDF", "api/reports/actionplan/pdf",
                                       new { controller = "Document", action = "CreateComplianceReviewPDF", returnUrl = UrlParameter.Optional }
                                       , constraints: new { httpMethod = new HttpMethodConstraint("POST") });

            RouteTable.Routes.MapRoute("ExecutiveSummaryHTML", "api/reports/executivesummary",
                                       new {controller = "Document", action = "ExecutiveSummary", returnUrl = UrlParameter.Optional});
           
        }
    }
}
