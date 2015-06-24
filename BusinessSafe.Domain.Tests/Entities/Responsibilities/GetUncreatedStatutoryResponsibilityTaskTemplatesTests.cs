using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.Tests.Entities.Responsibilities
{
    [TestFixture]
    public class GetUncreatedStatutoryResponsibilityTaskTemplatesTests
    {
        [Test]
        public void Given_some_tasks_are_created_nad_not_others_When_GetUncreatedStatutoryResponsibilityTaskTemplates_called_Then_returns_uncreated_task_templates()
        {
            var statutoryResponsibilityTemplate = new StatutoryResponsibilityTemplate
                                                      {
                                                          ResponsibilityTasks =
                                                              new List<StatutoryResponsibilityTaskTemplate>
                                                                  {
                                                                      new StatutoryResponsibilityTaskTemplate
                                                                          {
                                                                              Id = 201
                                                                          },
                                                                      new StatutoryResponsibilityTaskTemplate
                                                                          {
                                                                              Id = 202
                                                                          },
                                                                      new StatutoryResponsibilityTaskTemplate
                                                                          {
                                                                              Id = 203
                                                                          },
                                                                      new StatutoryResponsibilityTaskTemplate
                                                                          {
                                                                              Id = 204
                                                                          },
                                                                      new StatutoryResponsibilityTaskTemplate
                                                                          {
                                                                              Id = 205
                                                                          }
                                                                  }
                                                      };

            var responsibility = new Responsibility
                                     {
                                         StatutoryResponsibilityTemplateCreatedFrom = statutoryResponsibilityTemplate,
                                         ResponsibilityTasks = new List<ResponsibilityTask>
                                                                   {
                                                                       new ResponsibilityTask
                                                                           {
                                                                               StatutoryResponsibilityTaskTemplateCreatedFrom
                                                                                   =
                                                                                   statutoryResponsibilityTemplate.
                                                                                   ResponsibilityTasks[0]
                                                                           },
                                                                       new ResponsibilityTask
                                                                           {
                                                                               StatutoryResponsibilityTaskTemplateCreatedFrom
                                                                                   =
                                                                                   statutoryResponsibilityTemplate.
                                                                                   ResponsibilityTasks[1]
                                                                           },
                                                                       new ResponsibilityTask
                                                                           {
                                                                               StatutoryResponsibilityTaskTemplateCreatedFrom
                                                                                   =
                                                                                   statutoryResponsibilityTemplate.
                                                                                   ResponsibilityTasks[2]
                                                                           },
                                                                   }
                                     };

            var uncreatedTaskTemplates = responsibility.GetUncreatedStatutoryResponsibilityTaskTemplates();
            Assert.That(uncreatedTaskTemplates.Count(), Is.EqualTo(2));
            Assert.That(uncreatedTaskTemplates.Contains(statutoryResponsibilityTemplate.ResponsibilityTasks[3]));
            Assert.That(uncreatedTaskTemplates.Contains(statutoryResponsibilityTemplate.ResponsibilityTasks[4]));
        }
    }
}
