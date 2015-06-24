using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessSafe.Application.RestAPI.Responses;
using BusinessSafe.Domain.Entities.SafeCheck;
using EvaluationChecklist.Models;

namespace EvaluationChecklist.Mappers
{

    public static class ChecklistIndexViewModelMapper
    {
        public static List<ChecklistIndexViewModel> Map(this IEnumerable<Checklist> checklists)
        {
            return checklists.Select(x => x.Map()).ToList();
        }

        public static ChecklistIndexViewModel Map(this Checklist checklist, SiteAddressResponse visitSite, string clientAccountNumber, QaAdvisor qaAdvisor)
        {
            var checklistViewModel = checklist.Map(visitSite);
            checklistViewModel.CAN = clientAccountNumber;
            checklistViewModel.HasQaComments = checklist.Answers.Any(x => !string.IsNullOrEmpty(x.QaComments) && x.QaSignedOffBy == null);
            checklistViewModel.Deleted = checklist.Deleted;

            if (qaAdvisor != null)
            {
                checklistViewModel.QaAdvisor = new QaAdvisorViewModel()
                {
                    Id = qaAdvisor.Id,
                    Forename = qaAdvisor.Forename,
                    Email = qaAdvisor.Email,
                    Fullname = qaAdvisor.Forename + ' ' + qaAdvisor.Surname,
                    Initials = qaAdvisor.Forename + ' ' +  (qaAdvisor.Surname.Length > 0 ? qaAdvisor.Surname.Substring(0, 1) : "")
                };    
            }

            return checklistViewModel;
        }

        public static ChecklistIndexViewModel Map(this Checklist checklist, SiteAddressResponse visitSite)
        {
            var checklistViewModel = checklist.Map();
            checklistViewModel.Site.Id = visitSite != null ? (int)visitSite.Id : -1;
            checklistViewModel.Site.Postcode = visitSite != null ? visitSite.Postcode : "";
            checklistViewModel.Site.SiteName = visitSite != null ? visitSite.SiteName : "";
            checklistViewModel.Site.Address1 = visitSite != null ? visitSite.Address1 : "";
            checklistViewModel.Site.Address2 = visitSite != null ? visitSite.Address2 : "";
            checklistViewModel.Site.Address3 = visitSite != null ? visitSite.Address3 : "";
            checklistViewModel.Site.Address4 = visitSite != null ? visitSite.Address4 : "";

            return checklistViewModel;
        }

        public static ChecklistIndexViewModel Map(this Checklist checklist)
        {
            return new ChecklistIndexViewModel()
            {
                Id = checklist.Id,
                Title = "Title",
                VisitDate = checklist.VisitDate,
                VisitBy = checklist.VisitBy,
                CreatedOn = checklist.ChecklistCreatedOn,
                CreatedBy = checklist.ChecklistCreatedBy,
                Site = new SiteViewModel() { Id = checklist.SiteId.HasValue ? checklist.SiteId.Value : -1 },
                Status = checklist.Status,
                ClientName = checklist.CompanyName
                //,ImmediateRiskNotifications = ImmediateRiskNotificationViewModelMapper.Map(checklist.ImmediateRiskNotifications)
            };
        }
    }
}
