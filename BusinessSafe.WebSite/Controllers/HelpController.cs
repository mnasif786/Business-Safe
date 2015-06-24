using System;
using System.Collections.Generic;
using System.Web.Mvc;

using BusinessSafe.WebSite.Filters;

namespace BusinessSafe.WebSite.Controllers
{
    public enum HelpDocuments
    {
        GettingStarted = 1,
        SetUpEmployees ,
        GeneralRiskAssessment,
        FireRiskAssessment,
        PersonalRiskAssessment,
        HazardousSubstancesRiskAssessment,
        HazardousSubstances,
        TaskList,

        SiteSetupVideo,
        AddEmployeesVideo,
        AddUsersVideos,
        ResponsibilitiesWizardVideo,
        GeneralRiskAssessmentVideo,
        FireRiskAssessmentVideo,
        PersonalRiskAssessmentVideo,
        HazardousSubstancesVideo
    }

    public class HelpController : Controller
    {
        
        private readonly IDictionary<HelpDocuments,string> _documentNames = new Dictionary<HelpDocuments, string>
                                                                       {
                                                                           {HelpDocuments.GettingStarted, "Getting Started.pdf"},
                                                                           {HelpDocuments.SetUpEmployees, "Setting up your company and employees.pdf"},
                                                                           {HelpDocuments.GeneralRiskAssessment, "General Risk Assessment Process.pdf"},
                                                                           {HelpDocuments.FireRiskAssessment, ""},
                                                                           {HelpDocuments.PersonalRiskAssessment, ""},
                                                                           {HelpDocuments.HazardousSubstancesRiskAssessment, ""},
                                                                           {HelpDocuments.HazardousSubstances, "Hazardous Substances.pdf"},
                                                                           {HelpDocuments.TaskList, "My Task List.pdf"},

                                                                           {HelpDocuments.SiteSetupVideo, "1.Sites Setup Video.mp4"},
                                                                           {HelpDocuments.AddEmployeesVideo, "2.Adding My Employees Video.mp4"},
                                                                           {HelpDocuments.AddUsersVideos, "3.Adding Users Video.mp4"},
                                                                           {HelpDocuments.ResponsibilitiesWizardVideo, "4.Responsibilities Wizard Video.mp4"},

                                                                           {HelpDocuments.GeneralRiskAssessmentVideo, "5.General Risk Assessment Video.mp4"},
                                                                           {HelpDocuments.FireRiskAssessmentVideo, "6.Fire Risk Assessment Video.mp4"},
                                                                           {HelpDocuments.PersonalRiskAssessmentVideo, "7.Personal Risk Assessment Video.mp4"},
                                                                           {HelpDocuments.HazardousSubstancesVideo, "8.Hazardous Substances Video.mp4"},


                                                                       };
        
        public FileStreamResult Index(HelpDocuments documentToLoad)
        {
            var fileName = _documentNames[documentToLoad];
            var stream = GetType()
                            .Assembly
                            .GetManifestResourceStream("BusinessSafe.WebSite.Documents." + fileName);

            var file = File(stream, GetContentType(GetFileExtension(fileName)), fileName);
            return file;
        }

        private string GetFileExtension(string fileName)
        {
            return fileName.Substring(fileName.LastIndexOf(".") + 1);
        }

        private string GetContentType(string fileExtension)
        {
            switch(fileExtension)
            {
                case "pdf":
                    return "application/pdf";
                case "mp4":
                    return "video/mp4";
                default:
                    throw new NullReferenceException(string.Format("File extension '{0}' not found", fileExtension));
            }
        }
    }
}
