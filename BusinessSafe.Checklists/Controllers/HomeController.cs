using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BusinessSafe.Application.Contracts.Checklists;
using BusinessSafe.Application.Request.Checklists;
using BusinessSafe.Checklists.ViewModelFactories;
using BusinessSafe.Checklists.ViewModels;
using BusinessSafe.Messages.Commands;
using BusinessSafe.Messages.Emails.Commands;
using NServiceBus;
using SubmitAnswerRequest = BusinessSafe.Application.Request.SubmitAnswerRequest;

namespace BusinessSafe.Checklists.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeChecklistService _employeeChecklistService;
        private readonly IEmployeeChecklistViewModelFactory _employeeChecklistViewModelFactory;
        private readonly IBus _bus;

        public HomeController(IEmployeeChecklistService employeeChecklistService,
            IEmployeeChecklistViewModelFactory employeeChecklistViewModelFactory,
            IBus bus)
        {
            _employeeChecklistService = employeeChecklistService;
            _employeeChecklistViewModelFactory = employeeChecklistViewModelFactory;
            _bus = bus;
        }

        public FormsAuthenticationTicket GetAuthenticationTicket()
        {
            if (Request == null) return null;

            FormsAuthenticationTicket authenticationTicket = null;

            if (AuthenticationCookie != null)
            {
                authenticationTicket = FormsAuthentication.Decrypt(AuthenticationCookie.Value);
            }

            return authenticationTicket;
        }

        private HttpCookie AuthenticationCookie
        {
            get
            {
                return Request.Cookies["PeninsulaOnline"] ?? Request.Cookies["StagePeninsulaOnline"];
            }
        }

        public ActionResult Index(Guid employeeChecklistId, bool saved = false, bool completedOnEmployeesBehalf = false)
        {
            if (completedOnEmployeesBehalf && GetAuthenticationTicket() == null)
            {
                return View("NotAuthenticated");
            }

            var viewModel = _employeeChecklistViewModelFactory
                .WithEmployeeChecklistId(employeeChecklistId)
                .WithCompletedOnEmployeesBehalf(completedOnEmployeesBehalf)
                .GetViewModel();

            if(saved)
            {
                TempData["message"] = "Your checklist has been saved.";
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Save(EmployeeChecklistViewModel model)
        {
            var request = new SaveEmployeeChecklistRequest
            {
                EmployeeChecklistId = model.EmployeeChecklistId,
                Answers = GetListOfAnswers( model)
            };

            _employeeChecklistService.Save(request);

            return RedirectToAction("Index", new { employeeChecklistId = model.EmployeeChecklistId, saved = true });
        }

        private IList<SubmitAnswerRequest> GetListOfAnswers(EmployeeChecklistViewModel model)
        {
            return model.Sections.SelectMany(x => x.Questions)
                .Select(a => new SubmitAnswerRequest
                                 {
                                     AdditionalInfo = a.Answer.AdditionalInfo,
                                     BooleanResponse = a.Answer.BooleanResponse,
                                     QuestionId = a.Id
                                 }).ToList();
        }

        [HttpPost]
        public ActionResult Complete(EmployeeChecklistViewModel model, FormCollection formCollection)
        {
            var request = new CompleteEmployeeChecklistRequest
            {
                EmployeeChecklistId = model.EmployeeChecklistId,
                Answers = GetListOfAnswers(model)
            };

            var authenticationTicket = GetAuthenticationTicket();

            //todo: why isn't it binding to model.CompletedOnEmployeesBehalf ???
            if (Convert.ToBoolean(formCollection["CompletedOnEmployeesBehalf"]) && authenticationTicket != null)
            {
                request.CompletedOnEmployeesBehalfBy = new Guid(authenticationTicket.Name);
            }

            var validationMessages = _employeeChecklistService.ValidateComplete(request);

            if(validationMessages.Any())
            {
                validationMessages.ForEach(message => ModelState.AddModelError("", message.Text));
                return View("Index", model);
            }

          
            var completeCommandMessage = CreateCompleteEmployeeChecklistCommand(request);
            _bus.Send(completeCommandMessage);
            
            return RedirectToAction("Complete");
        }

        private static CompleteEmployeeChecklist CreateCompleteEmployeeChecklistCommand(CompleteEmployeeChecklistRequest request)
        {
            var completeCommand = new CompleteEmployeeChecklist
                                      {
                                          Answers =
                                              request.Answers.Select(
                                                  a =>
                                                  new SubmitAnswer
                                                      {
                                                          AdditionalInfo = a.AdditionalInfo,
                                                          BooleanResponse = a.BooleanResponse,
                                                          QuestionId = a.QuestionId
                                                      }).ToList()
                                          ,
                                          CompletedOnBehalf = request.CompletedOnBehalf,
                                          CompletedOnEmployeesBehalfBy = request.CompletedOnEmployeesBehalfBy,
                                          EmployeeChecklistId = request.EmployeeChecklistId,
                                          CompletedDate =  DateTime.Now
                                      };
            return completeCommand;
        }

        public ActionResult Complete()
        {
            return View();
        }

    }
}
