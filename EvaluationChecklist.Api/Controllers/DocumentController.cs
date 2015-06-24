using System;
using System.Web;
using System.Web.Mvc;
using EvaluationChecklist.Helpers;
using EvaluationChecklist.Models;
using StructureMap;
using log4net;

namespace EvaluationChecklist.Controllers
{
    public class DocumentController : Controller
    {

        private readonly IComplianceReviewReportViewModelFactory _complianceReviewReportViewModelFactory;

        public DocumentController()
        {
            _complianceReviewReportViewModelFactory =
                ObjectFactory.GetInstance<ComplianceReviewReportViewModelFactory>();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ActionPlan(ChecklistViewModel model)
        {
            return View("ActionPlan", _complianceReviewReportViewModelFactory.GetViewModel(model));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="checklist">json value of the checklist model</param>
        [HttpPost, ValidateInput(false)]
        public void CreateComplianceReviewPDF(string checklist)
        {
            try
            {
                var model = Newtonsoft.Json.JsonConvert.DeserializeObject<ChecklistViewModel>(checklist);
                CreateComplianceReviewPDF(model);    
            }
            catch(Exception ex)
            {
                LogManager.GetLogger(typeof(DocumentController)).Error(ex);
                throw;
            }
            
        }


        private void CreateComplianceReviewPDF(ChecklistViewModel model)
        {
            var checklistPdfCreator = ObjectFactory.GetInstance<IChecklistPdfCreator>();
            checklistPdfCreator.ChecklistViewModel = model;

            model.ContentPath = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, "/content");

            var filename = checklistPdfCreator.GetFileName();
            var pdfBytes = checklistPdfCreator.GetBytes();

            // get the object representing the HTTP response to browser
            HttpResponse httpResponse = System.Web.HttpContext.Current.Response;

            // add the Content-Type and Content-Disposition HTTP headers
            httpResponse.AddHeader("Content-Type", "application/pdf");

            httpResponse.AddHeader("Content-Disposition", String.Format("attachment; filename={1}; size={0}",
                                                                        pdfBytes.Length.ToString(),filename));


            // write the PDF document bytes as attachment to HTTP response 
            httpResponse.BinaryWrite(pdfBytes);

            // Note: it is important to end the response, otherwise the ASP.NET
            // web page will render its content to PDF document stream
            httpResponse.End();
        }
    }
}
