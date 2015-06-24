using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.EventHandlers;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Events;
using NUnit.Framework;
using Moq;

namespace BusinessSafe.MessageHandlers.Emails.Test
{
    [TestFixture]
    public class ReviewAssignedHandlerTests
    {
        private Mock<ITasksRepository> _taskRepository;
        private Mock<IEmailSender> _emailSender;
        private Mock<IBusinessSafeEmailLinkBaseUrlConfiguration> _businessSafeEmailLinkBaseUrlConfiguration;
        private Mock<IUserRepository> _userRepository;
        private RiskAssessmentReviewTask _task;

        [SetUp]
        public void Setup()
        {
            _taskRepository = new Mock<ITasksRepository>();
            _emailSender = new Mock<IEmailSender>();
            _businessSafeEmailLinkBaseUrlConfiguration = new Mock<IBusinessSafeEmailLinkBaseUrlConfiguration>();
            _userRepository = new Mock<IUserRepository>();

            _userRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new User());

            _task = new RiskAssessmentReviewTask
            {
                TaskAssignedTo = new Employee
                                       {
                                           ContactDetails = new List<EmployeeContactDetail>() { new EmployeeContactDetail() { Email = "test@test.com" } }
                                       }
                ,
                CreatedBy = new UserForAuditing() { Id = Guid.NewGuid() }
                ,
                Title = "review task title"
                ,
                Reference = "review task reference"
                ,
                RiskAssessmentReview = new RiskAssessmentReview
                {
                    RiskAssessment = new GeneralRiskAssessment(){ Title = "gra title",Reference = "gra reference"}
                }
            };


        }

        public Mock<ReviewAssignedHandler> GetTarget()
        {
            
            var constructor = new object[] { _taskRepository.Object, _emailSender.Object, _businessSafeEmailLinkBaseUrlConfiguration.Object, _userRepository.Object };

            var target = new Mock<ReviewAssignedHandler>(constructor) { CallBase = true };
            target.Setup(x => x.SendEmail(It.IsAny<ReviewAssignedEmailViewModel>()));
            return target;
        }

        [Test]
        public void Given_task_assignedto_doesnt_have_contactdetails_when_event_handled_then_email_is_not_sent()
        {
            //given
            var reviewAssEvent = new ReviewAssigned {TaskGuid = Guid.NewGuid()};
            _task.TaskAssignedTo.ContactDetails = null;
            _taskRepository.Setup(x => x.GetByTaskGuid(reviewAssEvent.TaskGuid))
                .Returns(() => _task);

            //when
            var target = GetTarget();

            target.Object.Handle(reviewAssEvent);

            //then
            target.Verify(x => x.SendEmail(It.IsAny<ReviewAssignedEmailViewModel>()),Times.Never());
        }

        
        [TestCase("")]
        [TestCase(null)]
        public void Given_task_assignedto_doesnt_have_an_email_address_when_event_handled_then_email_is_not_sent(string email)
        {
            //given
            var reviewAssEvent = new ReviewAssigned { TaskGuid = Guid.NewGuid() };
            _task.TaskAssignedTo.ContactDetails = new List<EmployeeContactDetail>() {new EmployeeContactDetail() {Email = email}};
            
            _taskRepository.Setup(x => x.GetByTaskGuid(reviewAssEvent.TaskGuid))
                .Returns(() => _task);

            //when
            var target = GetTarget();

            target.Object.Handle(reviewAssEvent);

            //then
            target.Verify(x => x.SendEmail(It.IsAny<ReviewAssignedEmailViewModel>()), Times.Never());
        }

        [Test]
        public void Given_task_assignedto_does_have_an_email_when_event_handled_then_email_is_sent()
        {
            //given
            var reviewAssEvent = new ReviewAssigned { TaskGuid = Guid.NewGuid() };
          
            _taskRepository.Setup(x => x.GetByTaskGuid(reviewAssEvent.TaskGuid))
                .Returns(() => _task);

            //when
            var target = GetTarget();

            target.Object.Handle(reviewAssEvent);

            //then
            target.Verify(x => x.SendEmail(It.IsAny<ReviewAssignedEmailViewModel>()), Times.Once());
        }

        [Test]
        public void Given_task_when_event_handled_then_the_risk_assessment_title_is_used()
        {
            //given
            var reviewAssEvent = new ReviewAssigned { TaskGuid = Guid.NewGuid() };

            _taskRepository.Setup(x => x.GetByTaskGuid(reviewAssEvent.TaskGuid))
                .Returns(() => _task);

            var target = GetTarget();

            ReviewAssignedEmailViewModel para = null;
            target.Setup(x => x.SendEmail(It.IsAny<ReviewAssignedEmailViewModel>()))
                .Callback<ReviewAssignedEmailViewModel>(x => para = x);

            //when
            target.Object.Handle(reviewAssEvent);

            //then
            Assert.AreEqual(para.TaskTitle, _task.RiskAssessmentReview.RiskAssessment.Title);
        }

        [Test]
        public void Given_task_when_event_handled_then_the_risk_assessment_reference_is_used()
        {
            //given
            var reviewAssEvent = new ReviewAssigned { TaskGuid = Guid.NewGuid() };

            _taskRepository.Setup(x => x.GetByTaskGuid(reviewAssEvent.TaskGuid))
                .Returns(() => _task);

            var target = GetTarget();

            ReviewAssignedEmailViewModel para = null;
            target.Setup(x => x.SendEmail(It.IsAny<ReviewAssignedEmailViewModel>()))
                .Callback<ReviewAssignedEmailViewModel>(x => para = x);

            //when
            target.Object.Handle(reviewAssEvent);

            //then
            Assert.AreEqual(para.TaskReference, _task.RiskAssessmentReview.RiskAssessment.Reference);
        }


        [Test]
        public void Given_task_cannot_be_retrieved_when_event_handled_then_email_not_sent()
        {
            //given
            var reviewAssEvent = new ReviewAssigned { TaskGuid = Guid.NewGuid() };

            _taskRepository
                .Setup(x => x.GetByTaskGuid(reviewAssEvent.TaskGuid))
                .Throws( new Exception());
                
            //when
            var target = GetTarget();

            target.Object.Handle(reviewAssEvent);

            target.Verify(x => x.SendEmail(It.IsAny<ReviewAssignedEmailViewModel>()), Times.Never());

            var reviewAssEvent2 = new ReviewAssigned { TaskGuid = Guid.NewGuid() };

            _taskRepository
                .Setup(x => x.GetByTaskGuid(reviewAssEvent2.TaskGuid))
                .Returns(() => _task);


            target.Object.Handle(reviewAssEvent2);




            //then
            target.Verify(x => x.SendEmail(It.IsAny<ReviewAssignedEmailViewModel>()), Times.Once());
        }
    }
}
