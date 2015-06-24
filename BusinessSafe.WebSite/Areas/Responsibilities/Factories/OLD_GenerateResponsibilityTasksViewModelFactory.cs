//using System.Collections.Generic;
//using BusinessSafe.Application.DataTransferObjects;
//using BusinessSafe.Domain.Entities;
//using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels.Wizard;

//namespace BusinessSafe.WebSite.Areas.Responsibilities.Factories
//{
//    public class GenerateResponsibilityTasksViewModelFactory : IGenerateResponsibilityTasksViewModelFactory
//    {
//        private long _companyId;

//        public GenerateResponsibilityTasksViewModelFactory WithCompanyId(long companyId)
//        {
//            _companyId = companyId;
//            return this;
//        }

//        public GenerateResponsibilityTasksViewModel GetViewModel()
//        {
//            //todo: for testing
//            return new GenerateResponsibilityTasksViewModel
//                            {
//                                Employees = new List<LookupDto>
//                                                {
//                                                    new LookupDto
//                                                        {
//                                                            Id = 1L,
//                                                            Name = "Employee1"
//                                                        }
//                                                },
//                                SelectedSites = new List<ResponsibilityWizardSite>
//                                                    {
//                                                        new ResponsibilityWizardSite
//                                                            {
//                                                                Id = 1L,
//                                                                Name = "Site1",
//                                                                Responsibilities = new List<StatutoryResponsibilityViewModel>
//                                                                                       {
//                                                                                           new StatutoryResponsibilityViewModel
//                                                                                               {
//                                                                                                   Id = 1L,
//                                                                                                   Title = "Responsibility1",
//                                                                                                   Description = "Description",
//                                                                                                   Category = "Test",
//                                                                                                   ResponsibilityReason = "Test",
//                                                                                                   Tasks = new List<StatutoryResponsibilityTaskViewModel>
//                                                                                                               {
//                                                                                                                   new StatutoryResponsibilityTaskViewModel
//                                                                                                                       {
//                                                                                                                           Id = 1L,
//                                                                                                                           Title = "Task1",
//                                                                                                                           Description = "Test",
//                                                                                                                           InitialFrequency = TaskReoccurringType.Weekly
//                                                                                                                       },
//                                                                                                                   new StatutoryResponsibilityTaskViewModel
//                                                                                                                       {
//                                                                                                                           Id = 1L,
//                                                                                                                           Title = "Task2",
//                                                                                                                           Description = "Test",
//                                                                                                                           InitialFrequency = TaskReoccurringType.Monthly
//                                                                                                                       }
//                                                                                                               }
//                                                                                               }
//                                                                                       }
//                                                            },
//                                                        new ResponsibilityWizardSite
//                                                            {
//                                                                Id = 1L,
//                                                                Name = "Site2",
//                                                                Responsibilities = new List<StatutoryResponsibilityViewModel>
//                                                                                       {
//                                                                                           new StatutoryResponsibilityViewModel
//                                                                                               {
//                                                                                                   Id = 1L,
//                                                                                                   Title = "Responsibility1",
//                                                                                                   Description = "Description",
//                                                                                                   Category = "Test",
//                                                                                                   ResponsibilityReason = "Test",
//                                                                                                   Tasks = new List<StatutoryResponsibilityTaskViewModel>
//                                                                                                               {
//                                                                                                                   new StatutoryResponsibilityTaskViewModel
//                                                                                                                       {
//                                                                                                                           Id = 1L,
//                                                                                                                           Title = "Task1",
//                                                                                                                           Description = "Test",
//                                                                                                                           InitialFrequency = TaskReoccurringType.Weekly
//                                                                                                                       },
//                                                                                                                   new StatutoryResponsibilityTaskViewModel
//                                                                                                                       {
//                                                                                                                           Id = 1L,
//                                                                                                                           Title = "Task2",
//                                                                                                                           Description = "Test",
//                                                                                                                           InitialFrequency = TaskReoccurringType.Monthly
//                                                                                                                       }
//                                                                                                               }
//                                                                                               }
//                                                                                       }
//                                                            }
//                                                    }
//                            };
//        }

//    }
//}