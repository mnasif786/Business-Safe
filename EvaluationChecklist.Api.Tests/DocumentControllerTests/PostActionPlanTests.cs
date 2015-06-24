using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using EvaluationChecklist.Controllers;
using NUnit.Framework;
using EvaluationChecklist.Models;

namespace EvaluationChecklist.Api.Tests.DocumentControllerTests
{
    [TestFixture]
    public class PostActionPlanTests
    {
        //IComplianceReviewReportViewModelFactory

        [SetUp]
        public void Setup()
        {
        }

        //private ChecklistViewModel GetChecklistViewModel()
        //{
        //    ChecklistViewModel chkModel = new ChecklistViewModel();
        //    chkModel.SiteVisit = new SiteVisitViewModel()
        //    {
        //        AreasVisited = "Visited area",
        //        AreasNotVisited = "Non-visited area",
        //        EmailAddress = "yabba@dabba.doo.com",
        //        VisitDate = DateTime.Now.ToShortDateString(),
        //        VisitBy = "Barney Rubble",
        //        VisitType = "Initial visit"
        //    };

        //    chkModel.SiteVisit.PersonSeen = new PersonSeenViewModel()
        //    {
        //        JobTitle = "Dogsbody",
        //        Name = "Fred Flintstone",
        //        Salutation = "Mr"
        //    };

        //    chkModel.SiteVisit.SelectedImpressionType = new ImpressionTypeViewModel()
        //    {
        //        Id = Guid.NewGuid(),
        //        Title = "the title",
        //        Comments = "some comments"
        //    };

        //    return chkModel;
        //}

        //[Test]
        //public void Given_a_checklist_then_the_values_are_correctly_mapped_to_the_action_plan()
        //{
        //    //// GIVEN
        //    var chklstViewModel = GetChecklistViewModel();

        //    //// WHEN 

        //    var target = GetTarget();
        //    target.ActionPlan(chklstViewModel);

        //    //// THEN
        //}

        //public DocumentController GetTarget()
        //{
        //    var controller = new DocumentController();
        //    //controller.Request = new HttpRequestMessage();
        //    return controller;
        //}
    }
}
