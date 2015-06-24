using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel; 

namespace UpdateSafeCheckQuestions
{
    internal class Program
    {
        private static List<QuestionStructure> QuestionList = new List<QuestionStructure>();
        private static List<QuestionStructure> QuestionListSorted = new List<QuestionStructure>();
        private static List<AnswersQuestionsResponse> QuestionResponseList = new List<AnswersQuestionsResponse>();
        private static List<IndustryQuestion> IndustryQuestionList = new List<IndustryQuestion>();
        private static List<CategoryStructure> Categories = new List<CategoryStructure>();


        private static Dictionary<Guid, String> ReportLetterCategories
        {
            get
            {
                var letterCategories = new Dictionary<Guid, String>();

                letterCategories.Add(Guid.Parse("9548DF42-716F-43EA-A446-269F7668326C"), "Management of Practices and Procedures");
                letterCategories.Add(Guid.Parse("652043C9-EDDF-414D-9204-62EB76E3F86D"), "Health and Safety Risk Management");
                letterCategories.Add(Guid.Parse("A3AFDFE3-EB69-4BD6-AB7E-68317ADF0D22"), "Management of Health and Safety Documentation");
                letterCategories.Add(Guid.Parse("C0F7FB70-7351-4B4E-891B-FADBE1A08F38"), "Management of the Premises");

                return letterCategories;
            }
        }

        private static void Main(string[] args)
        {
            Excel._Application app = new Microsoft.Office.Interop.Excel.Application();

            var wordbook =
                app.Workbooks.Open(@"C:\EvaluationChecklistScripts\SafeCheck Template Questions v3.xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                   Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                   Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                   Type.Missing, Type.Missing);

            var sheet = (Excel.Worksheet) wordbook.Sheets["Questions"];

            var sheetRange = sheet.UsedRange;

            var questionViewModels = new List<QuestionListViewModel>();

            var workingRange = sheet.get_Range("A2:P" + sheetRange.Rows.Count.ToString(), Type.Missing);
            foreach (Range row in workingRange.Rows)
            {
                var rowArray = (System.Array) row.Cells.Value2;
                if (rowArray.GetValue(1, 1) != null)
                {

                    var questionViewModel = new QuestionListViewModel();
                    questionViewModel.Id = rowArray.GetValue(1, 1).ToString();
                    questionViewModel.PositionId = rowArray.GetValue(1, 2).ToString();
                    questionViewModel.IsInAllIndustries = rowArray.GetValue(1, 3) != null && rowArray.GetValue(1, 3).ToString() == "Y";
                    questionViewModel.IsInManufacturing = rowArray.GetValue(1, 4) != null && rowArray.GetValue(1, 4).ToString() == "Y";
                    questionViewModel.IsInTransport = rowArray.GetValue(1, 5) != null && rowArray.GetValue(1, 5).ToString() == "Y";
                    questionViewModel.IsInCare = rowArray.GetValue(1, 6) != null && rowArray.GetValue(1, 6).ToString() == "Y";
                    questionViewModel.IsInHotelLeisure = rowArray.GetValue(1, 7) != null && rowArray.GetValue(1, 7).ToString() == "Y";
                    questionViewModel.IsInRetailService = rowArray.GetValue(1, 8) != null && rowArray.GetValue(1, 8).ToString() == "Y";
                    questionViewModel.Category = rowArray.GetValue(1, 9).ToString();
                    questionViewModel.Importance = rowArray.GetValue(1, 10).ToString();
                    questionViewModel.Question = rowArray.GetValue(1, 11).ToString();
                    questionViewModel.GuidanceNote = rowArray.GetValue(1, 12) != null ? rowArray.GetValue(1, 12).ToString() : null;
                    questionViewModel.Finding = rowArray.GetValue(1, 13) != null ? rowArray.GetValue(1, 13).ToString() : null;
                    questionViewModel.ActionRequiredOrSupportingEvidence = rowArray.GetValue(1, 14) != null ? rowArray.GetValue(1, 14).ToString() : null;
                    questionViewModel.CategoriesForReportLetter = rowArray.GetValue(1, 15) != null ? rowArray.GetValue(1, 15).ToString() : null;
                    questionViewModel.SummaryOfFindings = rowArray.GetValue(1, 16) != null ? rowArray.GetValue(1, 16).ToString() : null;

                    questionViewModels.Add(questionViewModel);
                }
            }

            Categories = CreateCategories(questionViewModels);

            QuestionListSorted = CreateQuestions(questionViewModels);

            QuestionResponseList = CreateQuestionResponseList(questionViewModels);


            IndustryQuestionList.AddRange(GetManaufacturingIndustryQuestions(questionViewModels));
            IndustryQuestionList.AddRange(GetTransportIndustryQuestions(questionViewModels));
            IndustryQuestionList.AddRange(GetCareIndustryQuestions(questionViewModels));
            IndustryQuestionList.AddRange(GetHotelIndustryQuestions(questionViewModels));
            IndustryQuestionList.AddRange(GetRetailIndustryQuestions(questionViewModels));


            PrintQuestions();
            PrintResponses();
            PrintIndustries();
            PrintCategories();
        }

        private static List<AnswersQuestionsResponse> CreateQuestionResponseList(List<QuestionListViewModel> questionViewModels)
        {
            return questionViewModels
                .Select(x => new AnswersQuestionsResponse
                                 {
                                     Id = Guid.NewGuid(),
                                     Title =  x.Finding.ToLower() == "partially acceptable" ? "Improvement Required" : x.Finding,
                                     GuidanceNotes = x.GuidanceNote,
                                     QuestionId = QuestionListSorted.First(q => q.Title.ToLower() == x.Question.ToLower()).Id,
                                     ResponseType = getResponseType(x.Finding),
                                     ReportLetterCategory =  ReportLetterCategories.Any(c=> c.Value == x.CategoriesForReportLetter)  ? ReportLetterCategories.First(c=> c.Value == x.CategoriesForReportLetter).Key.ToString() : null ,
                                     ReportLetterStatement = x.SummaryOfFindings,
                                     SupportingEvidence = (x.Finding.ToLower()== "acceptable" ? x.ActionRequiredOrSupportingEvidence : ""),
                                     ActionRequired = (x.Finding.ToLower() == "unacceptable" || x.Finding.ToLower() == "partially acceptable" ? x.ActionRequiredOrSupportingEvidence : "")
                                 }).ToList();
        }

        private static List<IndustryQuestion> GetManaufacturingIndustryQuestions(List<QuestionListViewModel> questionViewModels)
        {
            return questionViewModels
                .Where(x => x.IsInAllIndustries || x.IsInManufacturing)
                .Select(x=> x.Question)
                .Distinct(StringComparer.CurrentCultureIgnoreCase)
                .Select(x => new IndustryQuestion()
                                 {
                                     Id = Guid.NewGuid(),
                                     IndustryId = Guid.Parse("86EED5FD-4AF0-47A1-8795-594E573A81EC")
                                     ,
                                     QuestionId = QuestionListSorted.First(q => q.Title.ToLower() == x.ToLower()).Id
                                 })
                .ToList();
        }

        private static List<IndustryQuestion> GetTransportIndustryQuestions(List<QuestionListViewModel> questionViewModels)
        {
            return questionViewModels
                .Where(x => x.IsInAllIndustries || x.IsInTransport)
                .Select(x => x.Question)
                .Distinct(StringComparer.CurrentCultureIgnoreCase)
                .Select(x => new IndustryQuestion()
                {
                    Id = Guid.NewGuid(),
                    IndustryId = Guid.Parse("ABB4D820-75B3-4D54-A597-C9DE4F03DDE4")
                    ,
                    QuestionId = QuestionListSorted.First(q => q.Title.ToLower() == x.ToLower()).Id
                })
                .ToList();
        }

        private static List<IndustryQuestion> GetCareIndustryQuestions(List<QuestionListViewModel> questionViewModels)
        {
            return questionViewModels
                .Where(x => x.IsInAllIndustries || x.IsInCare)
                .Select(x => x.Question)
                .Distinct(StringComparer.CurrentCultureIgnoreCase)
                .Select(x => new IndustryQuestion()
                {
                    Id = Guid.NewGuid(),
                    IndustryId = Guid.Parse("6BA2AD9D-2013-44AB-A86F-4F8F1FDAD62F")
                    ,
                    QuestionId = QuestionListSorted.First(q => q.Title.ToLower() == x.ToLower()).Id
                })
                .ToList();
        }

        private static List<IndustryQuestion> GetHotelIndustryQuestions(List<QuestionListViewModel> questionViewModels)
        {
            return questionViewModels
                .Where(x => x.IsInAllIndustries || x.IsInHotelLeisure)
                .Select(x => x.Question)
                .Distinct(StringComparer.CurrentCultureIgnoreCase)
                .Select(x => new IndustryQuestion()
                {
                    Id = Guid.NewGuid(),
                    IndustryId = Guid.Parse("804F0C74-C0AD-4687-9F22-C95DAC20D51F")
                    ,
                    QuestionId = QuestionListSorted.First(q => q.Title.ToLower() == x.ToLower()).Id
                })
                .ToList();
        }

        private static List<IndustryQuestion> GetRetailIndustryQuestions(List<QuestionListViewModel> questionViewModels)
        {
            return questionViewModels
                .Where(x => x.IsInAllIndustries || x.IsInRetailService)
                .Select(x => x.Question)
                .Distinct(StringComparer.CurrentCultureIgnoreCase)
                .Select(x => new IndustryQuestion()
                {
                    Id = Guid.NewGuid(),
                    IndustryId = Guid.Parse("EFEC8F04-E53F-4E19-B6DF-79F06A02DC0E")
                    ,
                    QuestionId = QuestionListSorted.First(q => q.Title.ToLower() == x.ToLower()).Id
                })
                .ToList();
        }

        private static List<QuestionStructure> CreateQuestions(List<QuestionListViewModel> questionViewModels)
        {
            return questionViewModels.Select(x => new {x.Id, x.Question, x.Importance, x.Category})
                .Distinct()
                .Select(x => new QuestionStructure
                                 {
                                     Id = Guid.NewGuid(),
                                     CustomQuestion = false,
                                     Mandatory = !string.IsNullOrEmpty(x.Importance) && x.Importance == "Mandatory" ? true : false,
                                     RelatedCategoryId =  Categories.FirstOrDefault(c => c.Title.ToLower() == x.Category.ToLower()).Id,
                                     Title = x.Question,
                                     OrderNumber = int.Parse(x.Id)
                                 })
                                 .ToList();
        }

        private static List<CategoryStructure> CreateCategories(List<QuestionListViewModel> questionViewModels)
        {
            return questionViewModels.Select(x => x.Category)
                .Distinct(StringComparer.CurrentCultureIgnoreCase)
                .Select(x => new CategoryStructure { Id = Guid.NewGuid(), Title = x 
                    ,OrderNumber = GetCategoryOrderNumber(x)
                })
                .ToList();
        }

        private static int GetCategoryOrderNumber(string category)
        {
            switch (category)
            {
                default:
                case "Documentation":
                    return 1;
                case "Safety Arrangements":
                    return 2;
                case "Risk Assessments":
                    return 3;
                case "Fire":
                    return 4;
                case "People Management":
                    return 5;
                case "Premises Management":
                    return 6;
                case "Equipment":
                    return 7;
                case "Other subjects":
                case "Other Subjects":
                    return 8;
            }
        }

        private static void PrintQuestions()
        {
            string filepath = @"C:\EvaluationChecklistScripts\InsertQuestionsScript.sql";

            int length = QuestionListSorted.Count;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("USE [BusinessSafe]\r\n" +
                     "GO\r\n" +
                     "" +
                     " delete from [SafeCheckClientQuestion]\r\n" + 
                     " delete from [SafeCheckQuestion]\r\n" +
                     "" +
                     " GO \r\n");
            foreach (var question in QuestionListSorted)
            {
                sb.AppendLine(
                    "INSERT [dbo].[SafeCheckQuestion] ("+
                    "[Id], [CustomQuestion], [Title], [RelatedCategoryId], [Mandatory],[OrderNumber]) " +
                    "VALUES (N'" +
                     question.Id.ToString() + "',N'" + 
                     question.CustomQuestion + "',N'" +
                     (question.Title != null ? question.Title.Replace("\'", "\'\'") : "") + "',N'" + 
                     question.RelatedCategoryId.ToString() + "',N'" + 
                     question.Mandatory + "', " + question.OrderNumber.ToString() +")");
            }
            
            File.WriteAllText(filepath, sb.ToString());
        }

        private static void PrintResponses()
        {
            string filepath = @"C:\EvaluationChecklistScripts\InsertQuestionResponses.sql";
           
            int length = QuestionResponseList.Count;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("USE [BusinessSafe]\r\n" +
                       "GO\r\n" +
                       "" +
                       " DELETE FROM dbo.[SafeCheckCheckListAnswer]\r\n" +
                       " delete from [SafeCheckQuestionResponse]\r\n" +
                       "" +
                       " GO \r\n");
            foreach( var questionResponse in  QuestionResponseList)
            {
                sb.AppendLine(String.Format
                    ("INSERT [dbo].[SafeCheckQuestionResponse] (" +
                        "[Id], [Title], [Date], [ResponseType], [QuestionId], " +
                        "[SupportingEvidence], [ActionRequired], [GuidanceNotes], [TimeScaleId], [ReportLetterStatement], [ReportLetterStatementCategoryId]) VALUES (" +
                        "N'{0}',N'{1}',{2},N'{3}',N'{4}',N'{5}',N'{6}',N'{7}',{8},N'{9}',{10})", 
                        questionResponse.Id.ToString(),
                        questionResponse.Title != null ? questionResponse.Title.Replace("\'", "\'\'") : null, 
                        "NULL", 
                        questionResponse.ResponseType,
                        questionResponse.QuestionId,
                        questionResponse.SupportingEvidence != null ? questionResponse.SupportingEvidence.Replace("\'", "\'\'") : null,
                        questionResponse.ActionRequired != null ? questionResponse.ActionRequired.Replace("\'", "\'\'") : null,
                        questionResponse.GuidanceNotes != null ? questionResponse.GuidanceNotes.Replace("\'", "\'\'") : null,
                        "NULL",
                        questionResponse.ReportLetterStatement != null ? questionResponse.ReportLetterStatement.Replace("\'", "\'\'") : null
                        , !string.IsNullOrEmpty(questionResponse.ReportLetterCategory) ? "'" + questionResponse.ReportLetterCategory + "'" : "null"));

            }
            
            File.WriteAllText(filepath, sb.ToString());
        }

        private static void PrintIndustries()
        {
            string filepath = @"C:\EvaluationChecklistScripts\InsertsafeCheckIndustries.sql";

            int length = IndustryQuestionList.Count;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("USE [BusinessSafe]\r\n" + "GO\r\n" + "" +
                          " delete from [SafeCheckIndustryQuestion]\r\n" +
                          "" +
                          " GO\r\n ");
            foreach (var industry in IndustryQuestionList )
            {
                sb.AppendLine(String.Format("INSERT [dbo].[SafeCheckIndustryQuestion] ([Id], [IndustryId],[QuestionId]) Values (N'{0}',N'{1}',N'{2}')",
                              industry.Id, industry.IndustryId, industry.QuestionId));

            }

            File.WriteAllText(filepath, sb.ToString());
        }

        private static void PrintCategories()
        {
            string filepath = @"C:\EvaluationChecklistScripts\InsertsafeCheckCategories.sql";

            int length = Categories.Count;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("USE [BusinessSafe]\r\n" + "GO\r\n" + "" +
                          " delete from [SafeCheckCategory]\r\n" +
                          "" +
                          " GO\r\n ");
            foreach (var category in Categories)
            {
                sb.AppendLine(String.Format("INSERT [dbo].[SafeCheckCategory] ([Id], [Title], [OrderNumber]) Values (N'{0}',N'{1}',{2})",
                              category.Id, category.Title, category.OrderNumber.ToString()));

            }

            File.WriteAllText(filepath, sb.ToString());
        }

        private static string getResponseType(string Title)
        {
            switch (Title.ToLower())
            {
                case "acceptable":
                    return "Positive";
                case "unacceptable":
                    return "Negative";
                case "not applicable":
                    return "Neutral";
                case "partially acceptable":
                    return "neutral";
                default:
                    return "neutral";
            }
        }

        
    }

    public class QuestionListViewModel
    {
        public string Id { get; set; }
        public string PositionId { get; set; }
        public bool IsInAllIndustries { get; set; }
        public bool IsInManufacturing { get; set; }
        public bool IsInTransport { get; set; }
        public bool IsInCare { get; set; }
        public bool IsInHotelLeisure { get; set; }
        public bool IsInRetailService { get; set; }
        public string Category { get; set; }
        public string Importance { get; set; }
        public string Question { get; set; }
        public string Finding { get; set; }
        public string GuidanceNote { get; set; }
        public string ActionRequiredOrSupportingEvidence { get; set; }
        public string CategoriesForReportLetter { get; set; }
        public string SummaryOfFindings { get; set; }

    }

    public class AnswersQuestionsResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ResponseType { get; set; }
        public Guid QuestionId { get; set; }
        public string SupportingEvidence { get; set; }
        public string ActionRequired { get; set; }
        public string GuidanceNotes { get; set; }
        public string ReportLetterCategory { get; set; }
        public string ReportLetterStatement { get; set; }
    }
   
    public class QuestionStructure
    {
        public Guid Id { get; set; }
        public bool CustomQuestion { get; set; }
        public string Title { get; set; }
        public Guid RelatedCategoryId { get; set; }
        public bool Mandatory { get; set; }

        public int OrderNumber { get; set; }
    }

    public class IndustryQuestion
    {
        public Guid Id { get; set; }
        public Guid IndustryId { get; set; }
        public Guid QuestionId { get; set; }
    
    }

    public class CategoryStructure
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public int OrderNumber { get; set; }
    }
}
