using System;
using System.Linq;
using BusinessSafe.Application.Common;
using BusinessSafe.Data.NHibernate.BusinessSafe;
using BusinessSafe.Data.Queries.CompletedTaskQuery;
using BusinessSafe.Data.Queries.DueTaskQuery;
using BusinessSafe.Data.Queries.OverdueTaskQuery;
using BusinessSafe.Domain.Entities;
using BusinessSafe.EscalationService.Commands;
using BusinessSafe.EscalationService.EscalateTasks;
using BusinessSafe.EscalationService.Queries;
using NHibernate;
using NServiceBus;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.Pipeline;
using StructureMap.TypeRules;

namespace BusinessSafe.EscalationService.Activation
{
    public static class Bootstrapper
    {
        internal class SingletonConvention<TPluginFamily> : IRegistrationConvention
        {
            public void Process(Type type, Registry registry)
            {
                if (!type.IsConcrete() || !type.CanBeCreated() || !type.AllInterfaces().Contains(typeof(TPluginFamily))) return;

                registry.For(typeof(TPluginFamily)).Singleton().Use(type);
            }
        }

        public static void Run(IBus bus)
        {
            Log4NetHelper.Log.Info("Bootstrapper Run Start");

            ObjectFactory.Container.Configure(x =>
             {
                 x.ForSingletonOf<IBusinessSafeSessionFactory>().Use<BusinessSafeSessionFactory>();
             
                 x.For<IBusinessSafeSessionManager>().Singleton().HybridHttpOrThreadLocalScoped().Use<BusinessSafeSessionManager>();

                 // Queries
                 x.For<IGetOverDueTasksQuery>().Use<GetOverDueTasksQuery>();
                 x.For<IGetNextReoccurringTasksLiveQuery>().Use<GetNextReoccurringTasksLiveQuery>();
                 x.For<IGetOverDueReviewsQuery>().Use<GetOverDueReviewsQuery>();
                 x.For<IGetAccidentRecordOffWorkReminderQuery>().Use<GetAccidentRecordOffWorkReminderQuery>();
                 x.For<IGetEmployeeQuery>().Use<GetEmployeeQuery>();
                 x.For<IGetTaskDueTomorrowQuery>().Use<GetTaskDueTomorrowQuery>();
                 x.For<IGetOverdueActionTasksQuery>().Use<GetOverdueActionTasksQuery>();

                 x.For<IGetOverdueMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>>().Use<GetOverdueMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>>();
                 x.For<IGetOverdueMultiHazardRiskAssessmentTasksForEmployeeQuery<PersonalRiskAssessment>>().Use<GetOverdueMultiHazardRiskAssessmentTasksForEmployeeQuery<PersonalRiskAssessment>>();

                 x.For<IGetOverdueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery>().Use<GetOverdueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery>();
                 x.For<IGetOverdueHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery>().Use<GetOverdueHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery>();
                 x.For<IGetOverdueRiskAssessmentReviewTasksForEmployeeQuery>().Use<GetOverdueRiskAssessmentReviewTasks>();
                 x.For<IGetOverdueResponsibilitiesTasksForEmployeeQuery>().Use<GetOverdueResponsibilitiesTasksForEmployeeQuery>();
                 x.For<IGetOverdueActionTasksForEmployeeQuery>().Use<GetOverdueActionTasksForEmployeeQuery>();

                 x.For<IGetCompletedHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery>().Use<GetCompletedHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery>();
                 x.For<IGetCompletedFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery>().Use<GetCompletedFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery>();
                 x.For<IGetCompletedMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>>().Use<GetCompletedMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>>();
                 x.For<IGetCompletedMultiHazardRiskAssessmentTasksForEmployeeQuery<PersonalRiskAssessment>>().Use<GetCompletedMultiHazardRiskAssessmentTasksForEmployeeQuery<PersonalRiskAssessment>>();
                 x.For<IGetCompletedRiskAssessmentReviewTasksForEmployeeQuery>().Use<GetCompletedRiskAssessmentReviewTasksForEmployeeQuery>();

                 x.For<IGetDueMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>>().Use<GetDueMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>>();
                 x.For<IGetDueMultiHazardRiskAssessmentTasksForEmployeeQuery<PersonalRiskAssessment>>().Use<GetDueMultiHazardRiskAssessmentTasksForEmployeeQuery<PersonalRiskAssessment>>();
                 x.For<IGetDueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery>().Use<GetDueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery>();
                 x.For<IGetDueHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery>().Use<GetDueHazardousSubstanceRiskAssessmentFurtherControlMeasureTask>();
                 x.For<IGetDueRiskAssessmentReviewTasksForEmployeeQuery>().Use<GetDueRiskAssessmentReviewTasksForEmployeeQuery>();
                 x.For<IGetDueResponsibilityTasksForEmployeeQuery>().Use<GetDueResponsibilityTasksForEmployeeQuery>();
                 x.For<IGetDueActionTasksForEmployeeQuery>().Use<GetDueActionTasksForEmployeeQuery>();

                 // Command
                 x.For<IOverdueTaskNotificationEmailSentCommand>().Singleton().Use<OverdueTaskNotificationEmailSentCommand>();
                 x.For<INextReoccurringTaskNotificationEmailSentCommand>().Singleton().Use<NextReoccurringTaskNotificationEmailSentCommand>();
                 x.For<IOverdueReviewNotificationEmailSentCommand>().Singleton().Use<OverdueReviewNotificationEmailSentCommand>();
                 x.For<IOffWorkReminderEmailSentCommand>().Singleton().Use<OffWorkReminderEmailSentCommand>();
                 x.For<ITaskDueTomorrowEmailSentCommand>().Singleton().Use<TaskDueTomorrowEmailSentCommand>();

                 
                 //Configuration
                 //x.For<IEmailDigestEscalationConfiguration>().Use<EmailDigestEscalationConfiguration>();
                
                 x.For<IBus>().Use(bus);

                 x.AddRegistry<ApplicationRegistry>();

                 
                 x.Scan(s =>
                 {
                     s.TheCallingAssembly();
                     s.With(new SingletonConvention<IEscalate>());
                     s.AddAllTypesOf<IEscalate>();
                     s.With(new SingletonConvention<ISession>());
                 });

             });

            if (Environment.MachineName != "PBSBSOSTAGE1"
                   && Environment.MachineName != "PBSBS01"
                   && Environment.MachineName != "PBSBS02"
                   && Environment.MachineName != "PBSBS03"
                   && Environment.MachineName != "PBSBS04"
                   && Environment.MachineName != "PBSBS05"
                   && Environment.MachineName != "PBSSERVICEBUS1")
            {
                Log4NetHelper.Log.Debug("NHibernateProfiler Initialize");
                HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            }
            else
            {
                Log4NetHelper.Log.Debug("Not initialising NHibernateProfiler");
            }

            Log4NetHelper.Log.Info("Bootstrapper Run Finish");
        }
        
    }
}