using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.GeneralRiskAssessmentFurtherActionTasksTests
{
    [TestFixture]
    public class DefaultDocumentTypeTests
    {
        [Test]
        public void Given_a_FireFCMTask_when_DefaultDocumentType_returns_FRA_DocumentType()
        {
            //given
            var fcmTask = new FireRiskAssessmentFurtherControlMeasureTask();

            //when
            var result = fcmTask.DefaultDocumentType;

            //then
            Assert.AreEqual(DocumentTypeEnum.FRADocumentType, result);
        }

        [Test]
        public void Given_a_HSRAFCMTask_Type_when_DefaultDocumentType_returns_HSRA_DocumentType()
        {
            //given
            var hsrTask = new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask();

            //when
            var result = hsrTask.DefaultDocumentType;

            //then
            Assert.AreEqual(DocumentTypeEnum.HSRADocumentType, result);
        }

        [Test]
        public void Given_a_MulitHazardFCMTask_Type_when_DefaultDocumentType_returns_GRA_DocumentType()
        {
            //given
            var task = new MultiHazardRiskAssessmentFurtherControlMeasureTask();

            //when
            var result = task.DefaultDocumentType;

            //then
            Assert.AreEqual(DocumentTypeEnum.GRADocumentType, result);
        }

        [Test]
        public void Given_unknown_task_when_DefaultDocType_returns_exception()
        {
            //given
            var hsrTask = new Mock<FurtherControlMeasureTask>() {CallBase = true}.Object;

            //when
            var exception = Assert.Throws<Exception>(() =>
                                                         {
                                                             var result = hsrTask.DefaultDocumentType;
                                                         });

    

        }
     
    }
}
