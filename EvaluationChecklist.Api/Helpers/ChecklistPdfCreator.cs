using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EvaluationChecklist.Controllers;
using EvaluationChecklist.Models;
using StructureMap;

namespace EvaluationChecklist.Helpers
{
    public class ChecklistPdfCreator : IChecklistPdfCreator
    {
        private readonly IPDFGenerator _pdfGenerator;
        private ChecklistViewModel _checklistViewModel;
        private readonly IComplianceReviewReportViewModelFactory _complianceReviewReportViewModelFactory;

        public ChecklistPdfCreator()
        {
            _pdfGenerator = ObjectFactory.GetInstance<IPDFGenerator>();

            _complianceReviewReportViewModelFactory =
                ObjectFactory.GetInstance<ComplianceReviewReportViewModelFactory>();
        }

        public ChecklistViewModel ChecklistViewModel
        {
            get { return _checklistViewModel; }
            set { _checklistViewModel = value; }
        }

        public string GetFileName()
        {
            var filename = "ComplianceReview";

            if (filename.IndexOf(".pdf") == -1)
            {
                filename += ".pdf";
            }

            return filename;
        }

        public byte[] GetBytes()
        {
            var headerText = String.Empty;
            if (_checklistViewModel != null && _checklistViewModel.Site != null)
            {
                headerText = String.Format("{0} - {1}", _checklistViewModel.Site.SiteName, _checklistViewModel.Site.Postcode);
            }


            _checklistViewModel.CoveringLetterContent= ExecutiveSummaryLetterHeadFixer.UpdateLetterHeaderHtml(_checklistViewModel.CoveringLetterContent);
            AddStylesheetToCoveringLetterContent(_checklistViewModel);

            //string checklistViewModelString = RenderPartialViewToString(controller, "~/Views/Document/ActionPlan.cshtml", _complianceReviewReportViewModelFactory.GetViewModel(_checklistViewModel));

            var checklistViewModelString = RenderViewToString(_complianceReviewReportViewModelFactory.GetViewModel(_checklistViewModel));

            var pdfBytes = _pdfGenerator.CreateComplianceReviewReport(checklistViewModelString,
                                                                    _checklistViewModel.CoveringLetterContent,
                                                                    headerText,_checklistViewModel.ContentPath);

            return pdfBytes;
        }

        private static void AddStylesheetToCoveringLetterContent(ChecklistViewModel model)
        {
            var docType = @"<!DOCTYPE html>";
            var stylesheet = @"<style type=""text/css""> body, html, p {font-family:Arial, Helvetica, sans-serif; font-size:18pt; font-weight: normal;text-align:justify;} </style>";
            model.CoveringLetterContent = string.Format(@"{2}<html><head>{0}</head><body>{1}</body></html>", stylesheet, model.CoveringLetterContent, docType);

        }

        //http://wouterdekort.blogspot.co.uk/2012/10/rendering-aspnet-mvc-view-to-string-in.html
        //public static string RenderViewToString(string controllerName, string viewName, object viewData)
        public static string RenderViewToString(object viewData)
        {
            var controllerName = "Document";
            var viewName = "ActionPlan";
            var context = HttpContext.Current;   
            var contextBase = new HttpContextWrapper(context);  
            var routeData = new RouteData();   
            routeData.Values.Add("controller", controllerName);  
            var controllerContext = new ControllerContext(contextBase, routeData, new DocumentController());  
            var razorViewEngine = new RazorViewEngine();  
            var razorViewResult = razorViewEngine.FindView(controllerContext, viewName, "", false); 
            var writer = new StringWriter();
            var viewContext = new ViewContext(controllerContext, razorViewResult.View, new ViewDataDictionary(viewData), new TempDataDictionary(), writer); 
            razorViewResult.View.Render(viewContext, writer);
            return writer.ToString(); 
        }
    }
}