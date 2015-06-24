using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using BusinessSafe.Messages.Emails.Commands;
using BusinessSafe.WebSite.Areas.Contact.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using NServiceBus;
using RestSharp.Validation;

namespace BusinessSafe.WebSite.Areas.Contact.Controllers
{
    public class ContactUsController :  BaseController
    {
        private readonly IBus _bus;

        public ContactUsController(IBus bus)
        {
            _bus = bus;
        }

        [PermissionFilter(Permissions.ViewSiteDetails)]
        public ActionResult Index()
        {
            return View();
        }


        [PermissionFilter(Permissions.ViewSiteDetails)]
        public ActionResult Send(ContactUsViewModel contactUsViewModel)
        {
            Validate(contactUsViewModel);

            if (!ModelState.IsValid)
            {
                return View("Index", contactUsViewModel);    
            }
            
            SendEmail(contactUsViewModel);
            TempData["Notice"] = "Email has been sent out sucessfully.";

            return RedirectToAction("Index");
            
        }

        private void Validate(ContactUsViewModel contactUsViewModel)
        {
            if (String.IsNullOrEmpty(contactUsViewModel.Name))
            {
                ModelState.AddModelError("Name", "Name is required.");
            }
            
            if (String.IsNullOrEmpty(contactUsViewModel.EmailAddress))
            {
                ModelState.AddModelError("EmailAddress", "Email Address is required.");
            }
            else if(!ValidEmailAddress(contactUsViewModel.EmailAddress))
            {
                ModelState.AddModelError("EmailAddressInvalid", "Email Address is invalid.");
            }

            if (String.IsNullOrEmpty(contactUsViewModel.Message))
            {
                ModelState.AddModelError("Message", "Message is required.");
            }
        }

        private bool ValidEmailAddress(string emailAddress)
        {
            return
                Regex.Match(emailAddress.TrimStart().TrimEnd(), @"^[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$").Success;
        }

        private void SendEmail(ContactUsViewModel contactUsViewModel)
        {
            _bus.Send(new SendTechnicalSupportEmail()
            {
                Name = contactUsViewModel.Name.TrimStart().TrimEnd(),
                FromEmailAddress = contactUsViewModel.EmailAddress.TrimStart().TrimEnd(),
                Message = contactUsViewModel.Message.TrimStart().TrimEnd()
            });
        }
    }
}
