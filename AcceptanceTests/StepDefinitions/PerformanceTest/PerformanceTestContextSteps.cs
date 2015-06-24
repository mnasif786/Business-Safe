using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.Contracts.MultiHazardRiskAssessment;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using StructureMap;
using TechTalk.SpecFlow;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.PerformanceTest
{
    [Binding]
    public class PerformanceTestContextSteps: BaseSteps
    {
        private static Dictionary<int, Guid> users;
        private static Dictionary<int, Guid> employees;
        private static Dictionary<int, long> sites;
        private static Dictionary<int, long> hazards;
        private static int companyId = 55881;
        
        [BeforeFeature("PerformanceTestContextSteps")]
        public static void BeforeFeature()
        {
            var sessionManager = ObjectFactory.GetInstance<IBusinessSafeSessionManager>();
            var session = sessionManager.Session; 
            employees = GetEmployeesDictionary();
            sites = GetSitesDictionary();
            users = GetUsersDictionary();
            hazards = GetHazardsDictionary();
            
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
        }

        [AfterFeature("PerformanceTestContextSteps")]
        public static void AfterFeature()
        {
            var sessionManager = ObjectFactory.GetInstance<IBusinessSafeSessionManager>();
            sessionManager.CloseSession();
        }

        [Given(@"I have entered '(.*)' Risk Assessments")]
        public void GivenIHaveEntered100RiskAssessments(int numberOfRiskAssessments)
        {
            for (int i = 0; i < numberOfRiskAssessments; i++)
            {
                CreateRiskAssessment();    
            }
            
        }

        private static long CreateRiskAssessment()
        {
            
            var userId = GetRandomUserId();

            var riskAssessmentService = ObjectFactory.GetInstance<IGeneralRiskAssessmentService>();

            string titleReference = DateTime.Now.Ticks.ToString() + new Random().Next(2500) + new Random().Next(3000);
            var request = new CreateRiskAssessmentRequest()
                              {
                                  CompanyId = companyId,
                                  Reference = titleReference,
                                  Title = titleReference,
                                  UserId = userId
                              };


            var result = riskAssessmentService.CreateRiskAssessment(request);
            ObjectFactory.GetInstance<IBusinessSafeSessionManager>().Session.Flush();
            return result;
        }

        private static Guid GetRandomUserId()
        {
            var randomUserIndex = new Random().Next(users.Count -1);
            var userId = users[randomUserIndex];
            return userId;
        }

        private static long GetRandomSiteId()
        {
            var randomSiteIndex = new Random().Next(sites.Count -1);
            var siteId = sites[randomSiteIndex];
            return siteId;
        }

        private static long GetRandomHazardId()
        {
            var randomHazardIndex = new Random().Next(hazards.Count - 1);
            var hazardId = hazards[randomHazardIndex];
            return hazardId;
        }

        private static Guid GetRandomEmployeeId()
        {
            var randomEmployeeIndex = new Random().Next(employees.Count -1);
            var employeeId = employees[randomEmployeeIndex];
            return employeeId;
        }

        private static Dictionary<int, Guid> GetEmployeesDictionary()
        {
            var employeeRepository = ObjectFactory.GetInstance<IEmployeeRepository>();
            var employees = employeeRepository.GetAll().Where(x => x.CompanyId == companyId).ToList();
            var employeesDictionary = new Dictionary<int, Guid>();
            var index = 0;
            foreach (var employee in employees)
            {
                employeesDictionary.Add(index, employee.Id);
                index++;
            }
            return employeesDictionary;
        }

        private static Dictionary<int, long> GetSitesDictionary()
        {
            var siteRepository = ObjectFactory.GetInstance<ISiteStructureElementRepository>();
            var sites = siteRepository.GetAll().Where(x => x.ClientId == companyId).ToList();
            var sitesDictionary = new Dictionary<int, long>();
            var index = 0;
            foreach (var site in sites)
            {
                sitesDictionary.Add(index, site.Id);
                index++;
            }
            return sitesDictionary;
        }

        private static Dictionary<int, long> GetHazardsDictionary()
        {
            var hazardRepository = ObjectFactory.GetInstance<IHazardRepository>();
            var hazards = hazardRepository.GetByCompanyId(companyId);
            var hazardsDictionary = new Dictionary<int, long>();
            var index = 0;
            foreach (var hazard in hazards)
            {
                hazardsDictionary.Add(index, hazard.Id);
                index++;
            }
            return hazardsDictionary;
        }

        private static Dictionary<int, Guid> GetUsersDictionary()
        {
            var userRepository = ObjectFactory.GetInstance<IUserRepository>();
            var users = userRepository.GetAll().Where(x => x.CompanyId == companyId && x.Deleted ==false).ToList();
            var usersDictionary = new Dictionary<int, Guid>();
            var index = 0;
            foreach (var user in users)
            {
                usersDictionary.Add(index, user.Id);
                index++;
            }
            return usersDictionary;
        }

        [Given(@"I have entered '(.*)' Risk Assessments each with '(.*)' Risk Assessment Documents")]
        public void GivenIHaveEntered100RiskAssessmentsEachWith10RiskAssessmentDocuments(int numberOfRiskAssessments, int numberOfRiskAssessmentsDocuments)
        {
            for (int i = 0; i < numberOfRiskAssessments; i++)
            {
                CreateRiskAssessmentWithDocuments(numberOfRiskAssessmentsDocuments);
            }
        }

        private void CreateRiskAssessmentWithDocuments(int amountOfDocumentsToAddToRiskAssessment)
        {
            var newRiskAssementId = CreateRiskAssessment();
            var riskAssessmentAttachmentService = ObjectFactory.GetInstance<IRiskAssessmentAttachmentService>();

            var documentsToAttach = new List<CreateDocumentRequest>();
            for (int i = 0; i < amountOfDocumentsToAddToRiskAssessment; i++)
            {
                var randomId = new Random().Next(2500);
                string titleDescription = "Test " + randomId;
                documentsToAttach.Add(new CreateDocumentRequest()
                {
                    Title = titleDescription,
                    Description = titleDescription,
                    DocumentLibraryId = i,
                    Filename = "Test File Name" + randomId,
                    DocumentType = DocumentTypeEnum.GRADocumentType,
                    DocumentOriginType = DocumentOriginType.TaskCompleted,
                    Extension = ".doc",
                    
                });    
            }
            
            
            var request = new AttachDocumentsToRiskAssessmentRequest()
                              {
                                  CompanyId = companyId,
                                  RiskAssessmentId = newRiskAssementId,
                                  UserId = GetRandomUserId(),
                                  DocumentsToAttach = documentsToAttach
                              };
            riskAssessmentAttachmentService.AttachDocumentsToRiskAssessment(request);
            ObjectFactory.GetInstance<IBusinessSafeSessionManager>().Session.Flush();
        }

        [Given(@"I have entered '(.*)' Risk Assessments each with '(.*)' Further Control Measure Task Documents")]
        public void GivenIHaveEntered100RiskAssessmentsEachWith10FurtherControlMeasureTaskDocuments(int numberOfRiskAssessments, int numberOfRiskAssessmentsFurtherControlMeasureDocuments)
        {
            for (int i = 0; i < numberOfRiskAssessments; i++)
            {
                CreateRiskAssessmentWithFurtherControlMeasureTasksDocuments(numberOfRiskAssessmentsFurtherControlMeasureDocuments);
            }
        }

        private void CreateRiskAssessmentWithFurtherControlMeasureTasksDocuments(int amountOfFurtherControlMeasureDocumentsToAdd)
        {
            var newRiskAssementId = CreateRiskAssessment();

            var sessionManager = ObjectFactory.GetInstance<IBusinessSafeSessionManager>();
            var riskAssessmentAttachmentService = ObjectFactory.GetInstance<IMultiHazardRiskAssessmentAttachmentService>();
            var attachHazardRequest = new AttachHazardsToRiskAssessmentRequest()
                                          {
                                              CompanyId = companyId,
                                              RiskAssessmentId = newRiskAssementId,
                                              UserId = GetRandomUserId(),
                                              Hazards = new List<AttachHazardsToRiskAssessmentHazardDetail>()
                                              {
                                                  new AttachHazardsToRiskAssessmentHazardDetail(){Id =GetRandomHazardId(), OrderNumber = 1},
                                              } 

                                          };
            riskAssessmentAttachmentService.AttachHazardsToRiskAssessment(attachHazardRequest);
            sessionManager.Session.Flush();


            var riskAssessmentFurtherActionService = ObjectFactory.GetInstance<IFurtherControlMeasureTaskService>();

            var documentsToAttach = new List<CreateDocumentRequest>();
            for (int i = 0; i < amountOfFurtherControlMeasureDocumentsToAdd; i++)
            {
                var randomId = new Random().Next(2500);
                string titleDescription = "Test " + randomId;
                documentsToAttach.Add(new CreateDocumentRequest()
                {
                    Title = titleDescription,
                    Description = titleDescription,
                    DocumentLibraryId = i,
                    Filename = "Test File Name" + randomId,
                    DocumentType = DocumentTypeEnum.GRADocumentType,
                    DocumentOriginType = DocumentOriginType.TaskCompleted,
                    Extension = ".doc",

                });
            }

            var riskAssessmentRepository = ObjectFactory.GetInstance<IGeneralRiskAssessmentRepository>();
            var riskAssessment = riskAssessmentRepository.GetById(newRiskAssementId);
            var riskAssessmentHazardId = riskAssessment.Hazards.First().Id;

            var request = new SaveFurtherControlMeasureTaskRequest()
            {
                CompanyId = companyId,
                RiskAssessmentId = newRiskAssementId,
                UserId = GetRandomUserId(),
                CreateDocumentRequests = documentsToAttach,
                Title = "",
                RiskAssessmentHazardId = riskAssessmentHazardId,
                FurtherControlMeasureTaskCategoryId = 3
            };
            riskAssessmentFurtherActionService.AddFurtherControlMeasureTask(request);
            sessionManager.Session.Flush();
        }
    }
}
