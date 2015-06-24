using System;
using System.Collections.Generic;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.Checklists;
using BusinessSafe.Domain.Entities;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.Checklists.EmployeeChecklistServiceTests
{
    [TestFixture]
    public class CompleteTests : BaseEmployeeServiceTests
    {
        [SetUp]
        public void Setup()
        {
            base.Setup();

            _employeeChecklistrepo.Setup(x => x.SaveOrUpdate(It.IsAny<EmployeeChecklist>()));
        }

        [Test]
        public void When_Complete_Set_Complete_Date()
        {
            // Given
            EmployeeChecklist passedEmployeeChecklist = null;

            _employeeChecklistrepo
                .Setup(x => x.SaveOrUpdate(It.IsAny<EmployeeChecklist>()))
                .Callback<EmployeeChecklist>(y => passedEmployeeChecklist = y);

            var returnedEmployeeChecklist = new Mock<EmployeeChecklist>() { CallBase = true };
            returnedEmployeeChecklist.Setup(x => x.AreAllQuestionsAnswered()).Returns(true);
            returnedEmployeeChecklist.Setup((x=> x.Id)).Returns(Guid.NewGuid());

             _employeeChecklistrepo
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(returnedEmployeeChecklist.Object);

             _questionRepository
                 .Setup(x => x.GetById(It.IsAny<long>()))
                 .Returns(new Question());

            var request = new CompleteEmployeeChecklistRequest()
                          {
                              EmployeeChecklistId = returnedEmployeeChecklist.Object.Id,
                              Answers = new List<SubmitAnswerRequest>
                              {
                                  new SubmitAnswerRequest
                                      {
                                          QuestionId = 1L,
                                          BooleanResponse = true,
                                          AdditionalInfo = "Additional Info 1"
                                      },
                                  new SubmitAnswerRequest
                                      {
                                          QuestionId = 2L,
                                          BooleanResponse = false,
                                          AdditionalInfo = "Additional Info 2"
                                      },
                              },
                              CompletedDate = DateTime.Now
                          };

            var target = GetTarget();

            // When
            target.Complete(request);

            // Then
            Assert.That(passedEmployeeChecklist.CompletedDate, Is.Not.Null);
            Assert.That(passedEmployeeChecklist.CompletedDate, Is.EqualTo(request.CompletedDate));
            Assert.AreEqual(passedEmployeeChecklist.Id, request.EmployeeChecklistId);
        }

        [Test]
        public void When_Complete_Retrives_Checklist_from_repository()
        {
            // Given
            EmployeeChecklist passedEmployeeChecklist = null;

            _employeeChecklistrepo
                .Setup(x => x.SaveOrUpdate(It.IsAny<EmployeeChecklist>()))
                .Callback<EmployeeChecklist>(y => passedEmployeeChecklist = y);

            var returnedEmployeeChecklist = new Mock<EmployeeChecklist>() { CallBase = true };
            returnedEmployeeChecklist.Setup(x => x.AreAllQuestionsAnswered()).Returns(true);
            returnedEmployeeChecklist.Setup((x => x.Id)).Returns(Guid.NewGuid());

            _employeeChecklistrepo
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(returnedEmployeeChecklist.Object);

            _questionRepository
                .Setup(x => x.GetById(It.IsAny<long>()))
                .Returns(new Question());

            var request = new CompleteEmployeeChecklistRequest()
            {
                EmployeeChecklistId = Guid.NewGuid(),
                Answers = new List<SubmitAnswerRequest>
                              {
                                  new SubmitAnswerRequest
                                      {
                                          QuestionId = 1L,
                                          BooleanResponse = true,
                                          AdditionalInfo = "Additional Info 1"
                                      },
                                  new SubmitAnswerRequest
                                      {
                                          QuestionId = 2L,
                                          BooleanResponse = false,
                                          AdditionalInfo = "Additional Info 2"
                                      },
                              }
            };

            var target = GetTarget();

            // When
            target.Complete(request);

            // Then
           _employeeChecklistrepo.Verify(x=> x.GetById(request.EmployeeChecklistId));
           _questionRepository.Verify(x => x.GetById(1L), Times.Once());
           _questionRepository.Verify(x => x.GetById(2L), Times.Once());
        }
    }
}
