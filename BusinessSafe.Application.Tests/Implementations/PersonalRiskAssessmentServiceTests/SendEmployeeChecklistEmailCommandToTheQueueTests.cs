using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Implementations.PersonalRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Messages.Commands;
using Moq;
using NServiceBus;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.PersonalRiskAssessmentServiceTests
{
    [TestFixture]
    public class SendEmployeeChecklistEmailCommandToTheQueueTests
    {
        private Mock<IPersonalRiskAssessmentRepository> _riskAssessmentRepo;
        private Mock<IUserForAuditingRepository> _userRepo;
        private Mock<IPeninsulaLog> _log;
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<IChecklistRepository> _checklistRepository;
        private Mock<IBus> _bus;
        
        [Test]
        public void Given_a_PRA_when_the_email_checklist_command_has_been_sent_to_the_queue_then_EmployeeChecklistStatus_equals_Generating()
        {
            //given
            long riskAssId = 3248;
            var personalRiskAssessment = new PersonalRiskAssessment();
            personalRiskAssessment.Id = riskAssId;
            personalRiskAssessment.PersonalRiskAssessementEmployeeChecklistStatus = PersonalRiskAssessementEmployeeChecklistStatusEnum.NotSet;

            _log = new Mock<IPeninsulaLog>();
            _log.Setup(x => x.Add(It.IsAny<object>()));
            
            _userRepo = new Mock<IUserForAuditingRepository>();
            _riskAssessmentRepo = new Mock<IPersonalRiskAssessmentRepository>();
            _riskAssessmentRepo.Setup(x => x.GetById(riskAssId))
                .Returns(personalRiskAssessment);

            _employeeRepository = new Mock<IEmployeeRepository>();
            _checklistRepository = new Mock<IChecklistRepository>();
            _bus = new Mock<IBus>();

            var request = new GenerateEmployeeChecklistEmails {RiskAssessmentId = riskAssId};

            var target = GetTarget();

            //when
            target.SetAsGenerating(request.RiskAssessmentId);

            //then
            _riskAssessmentRepo.Verify(x=> x.GetById(riskAssId));
            _riskAssessmentRepo.Verify(x => x.SaveOrUpdate(It.Is<PersonalRiskAssessment>(pra => pra.Id == riskAssId)));
            _riskAssessmentRepo.Verify(x => x.SaveOrUpdate(It.Is<PersonalRiskAssessment>(pra => pra.PersonalRiskAssessementEmployeeChecklistStatus == PersonalRiskAssessementEmployeeChecklistStatusEnum.Generating)));
            
        }

        private PersonalRiskAssessmentService GetTarget()
        {
            return new PersonalRiskAssessmentService(_riskAssessmentRepo.Object
                                                     , _userRepo.Object
                                                     ,_employeeRepository.Object
                                                     , _checklistRepository.Object
                                                     , _log.Object
                                                     , null
                                                     , null
                                                     , null, null);
        }

        [Test]
        [Ignore("Application layer no longer sending to bus.")]
        public void Given_a_PRA_when_sending_the_email_checklist_command_then_assert_that_it_has_been_sent_to_the_queue()
        {
            //given
            long riskAssId = 3248;
            var personalRiskAssessment = new PersonalRiskAssessment();
            personalRiskAssessment.Id = riskAssId;
            personalRiskAssessment.PersonalRiskAssessementEmployeeChecklistStatus = PersonalRiskAssessementEmployeeChecklistStatusEnum.NotSet;

            _log = new Mock<IPeninsulaLog>();
            _log.Setup(x => x.Add(It.IsAny<object>()));

            _userRepo = new Mock<IUserForAuditingRepository>();
            _riskAssessmentRepo = new Mock<IPersonalRiskAssessmentRepository>();
            _riskAssessmentRepo.Setup(x => x.GetById(riskAssId))
                .Returns(personalRiskAssessment);

            _employeeRepository = new Mock<IEmployeeRepository>();
            _checklistRepository = new Mock<IChecklistRepository>();

            _bus = new Mock<IBus>();
            
            var request = new GenerateEmployeeChecklistEmails { RiskAssessmentId = riskAssId };

            var target = GetTarget();

            //when
            target.SetAsGenerating(request.RiskAssessmentId);

            //then
            _bus.Verify(x => x.Send(request));
            

        }
    }
}
