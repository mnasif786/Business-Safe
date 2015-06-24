using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using TechTalk.SpecFlow;
using WatiN.Core;


namespace BusinessSafe.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class BaseSteps: Steps
    {
        public const int LastRiskAssessmentId = 59;
        public const int LastTaskId = 50;
        public const int DevBoxMaxSleep = 1500;

        public static readonly string BusinessSafeConnectionString = ConfigurationManager.ConnectionStrings["BusinessSafe_Intranetadmin"].ConnectionString;

        [BeforeTestRun()]
        public static void BeforeTestRun()
        {
            AcceptanceTestBootstrap.Run();

            try
            {
                //warm-up website so we don't get the SQL connection error when the first test is ran
                var webClient = new WebClient();
                var result = webClient.DownloadString(string.Format("{0}AutoLogInFromPeninsula/Index?companyId=55881&userId=16ac58fb-4ea4-4482-ac3d-000d607af67c", ConfigurationManager.AppSettings["baseURL"]));    
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error warming up web site");
                Console.WriteLine(ex);
            }
            
        }

        [BeforeScenario(@"Acceptance")]
        [BeforeFeature(@"Acceptance")]
        [AfterScenario(@"Acceptance")]
        [AfterFeature(@"Acceptance")]
        public static void DeleteRowsCreatedInTests()
        {
            //remember to change FRACoreFunctionalitySteps

            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM GeneralRiskAssessment Where Id > " + LastRiskAssessmentId);
            sql.AppendLine("DELETE FROM MultiHazardRiskAssessment Where Id > " + LastRiskAssessmentId);
            sql.AppendLine("DELETE FROM RiskAssessment Where Id > " + LastRiskAssessmentId);
            sql.AppendLine("DELETE FROM MultiHazardRiskAssessmentHazard Where RiskAssessmentId > " + LastRiskAssessmentId);
            sql.AppendLine("DELETE FROM PersonalRiskAssessment WHERE ID > " + LastRiskAssessmentId);
            sql.AppendLine(string.Format("DELETE FROM Task Where Id > {0}", LastTaskId));
            sql.AppendLine("DELETE FROM RiskAssessmentReview ");
            sql.AppendLine("DELETE FROM NonEmployee Where Name != 'Dave Smith' ");
            sql.AppendLine("DELETE FROM TaskDocument WHERE Id > 9 ");
            sql.AppendLine("DELETE FROM AddedDocument WHERE Id > 2 ");
            sql.AppendLine("DELETE FROM RiskAssessmentDocument WHERE Id > 6 ");
            sql.AppendLine("DELETE FROM Document WHERE Id > 9 ");
            sql.AppendLine("DELETE FROM HazardousSubstanceRiskAssessment WHERE ID > " + LastRiskAssessmentId);
          //  sql.AppendLine("DELETE FROM Hazard WHERE Id > 25");
          //  sql.AppendLine("DELETE FROM PeopleAtRisk WHERE Id > 9");

            sql.AppendLine("DELETE FROM RiskAssessmentPeopleAtRisk WHERE RiskAssessmentId = 2");
            sql.AppendLine("DELETE FROM MultiHazardRiskAssessmentHazard WHERE RiskAssessmentId = 2");
            sql.AppendLine("DELETE FROM EmployeeEmergencyContactDetails Where Id = 1");
            sql.AppendLine("DELETE FROM Responsibility Where Id > 12");

            sql.AppendLine("DELETE FROM MultiHazardRiskAssessmentHazard Where RiskAssessmentId = 54");
            sql.AppendLine("DELETE FROM PeopleAtRisk WHERE RiskAssessmentId = 54");
            sql.AppendLine("DELETE FROM RiskAssessmentPeopleAtRisk WHERE RiskAssessmentId = 54");
            sql.AppendLine("DELETE FROM ChecklistGeneratorEmployee");
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                runSQLCommand(sql.ToString(), conn);

            }
        }

        protected static object runSQLCommand(string sql, SqlConnection conn)
        {
            object val;
            using (var command = new SqlCommand(sql, conn))
            {
                val = command.ExecuteScalar();
            }
            return val;
        }

        public void GotoHomePage()
        {
            WebBrowser.Driver.Navigate("");
        }

        public void GotoResponsibilityPlanner()
        {
            GotoHomePage(); 
            WebBrowser.Current.Link(x => x.Id == "myPlannerLink").Click();
            WebBrowser.Current.Link(x => x.Id == "viewMyPlannerLink").Click();
        }

        public void GotoCompanyDetailsPage(int companyId)
        {
            GotoHomePage();
            WebBrowser.Driver.Navigate("/Company/Company/Index/" + companyId); //TODO Need to sort this when logging done
        }
        
        public void GotoCompanyDefaultsPage()
        {
            GotoHomePage();
            WebBrowser.Current.Link(x => x.Id == "myProfileLink").Click();
            WebBrowser.Current.Link(x => x.Id == "companyDefaultsLink").Click();
        }

        public static void SleepIfEnvironmentIsPotentiallySlow(int ms)
        {
            if(NotADevBox())
            {
                Thread.Sleep(ms);
            }else
            {
                if (ms > DevBoxMaxSleep)
                {
                    Thread.Sleep(DevBoxMaxSleep);
                }
            }
        }

        private static bool NotADevBox()
        {
            var devMachines = new[] { "PBS43516", "PBS43083", "PBS42691", "PBS42576", "PBS44109" };
            var machineName = System.Environment.MachineName;

            return devMachines.Contains(machineName) == false;
        }

        protected long GetCurrentRiskAssessmentId()
        {
            var url = WebBrowser.Current.Url;
            var regexResult = Regex.Match(url, @"([riskAssessmentId=|hazardousSubstanceRiskAssessmentId=])(\d+)");
            var riskAssessmentId = regexResult.Groups[2].Value;
            return long.Parse(riskAssessmentId);
        }

        protected long GetCurrentCompanyId()
        {
            var url = WebBrowser.Current.Url;
            var regexResult = Regex.Match(url, @"([C|c]ompanyId=)(\d+)");
            var riskAssessmentId = regexResult.Groups[2].Value;
            return long.Parse(riskAssessmentId);
        }

        protected Guid GetEmployeeIdByName(string employeeName)
        {
            var sql = new StringBuilder();
            var namesSplit = employeeName.Split(' ');
            var forename = namesSplit.First().Trim();
            var surname = namesSplit.Skip(1).Take(1).First().Trim();

            sql.Append("SELECT Id FROM Employee WHERE Forename = '" + forename + "' AND Surname = '" + surname + "'");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                using (var command = new SqlCommand(sql.ToString(), conn))
                {
                    conn.Open();
                    var result = command.ExecuteScalar();
                    return (Guid) result;
                }
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            try
            {
                WebBrowser.Current.ForceClose();
                Process[] processes = Process.GetProcessesByName("iexplore");

                foreach (Process process in processes)
                {
                    process.Kill();
                }
            }
            catch
            {
            }
        }

        protected Element GetElement(Func<string, Element> findMethod, string elementIdentifier, int waitForMS)
        {
            Element element;
            const int loopSleepLength = 250;
            var loopIndex = 1;
            var numberOfLoopsToTry = waitForMS / loopSleepLength;

            do
            {
                element = findMethod(elementIdentifier);
                if (element == null)
                {
                    loopIndex++;
                    Thread.Sleep(loopSleepLength);
                }
            }
            while (element == null && loopIndex <= numberOfLoopsToTry);

            return element;
        }

        protected Element GetElement(Func<string, Form, Element> findMethod, string elementIdentifier, Form form, int waitForMS)
        {
            Element element;
            const int loopSleepLength = 250;
            var loopIndex = 1;
            var numberOfLoopsToTry = waitForMS / loopSleepLength;

            do
            {
                element = findMethod(elementIdentifier, form);
                if (element == null)
                {
                    loopIndex++;
                    Thread.Sleep(loopSleepLength);
                }
            }
            while (element == null && loopIndex <= numberOfLoopsToTry);

            return element;
        }

        protected Element GetElement(Func<object[], Element> findMethod, string[] args, int waitForMS)
        {
            Element element;
            const int loopSleepLength = 250;
            var loopIndex = 1;
            var numberOfLoopsToTry = waitForMS / loopSleepLength;

            do
            {
                element = findMethod(args);
                if (element == null)
                {
                    loopIndex++;
                    Thread.Sleep(loopSleepLength);
                }
            }
            while (element == null && loopIndex <= numberOfLoopsToTry);

            return element;
        }
    }
}
