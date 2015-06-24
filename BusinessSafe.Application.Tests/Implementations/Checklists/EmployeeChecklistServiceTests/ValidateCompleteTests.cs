using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Request.Checklists;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.Checklists.EmployeeChecklistServiceTests
{
    [TestFixture]
    public class ValidateCompleteTests : BaseEmployeeServiceTests
    {
        [Test]
        public void When_ValidateComplete_is_called_Then_correct_methods_are_called()
        {
            var employeeChecklistId = Guid.NewGuid();
            var employeeChecklist = new Mock<EmployeeChecklist>();
            var validationMessages = new ValidationMessageCollection();

            var request = new CompleteEmployeeChecklistRequest
                              {
                                  EmployeeChecklistId = employeeChecklistId
                              };

            _employeeChecklistrepo
                .Setup(x => x.GetById(employeeChecklistId))
                .Returns(employeeChecklist.Object);

            employeeChecklist
                .Setup(x => x.ValidateComplete())
                .Returns(validationMessages);

            GetTarget().ValidateComplete(request);
            _employeeChecklistrepo.Verify(x => x.GetById(employeeChecklistId));
            employeeChecklist.Verify(x => x.ValidateComplete());
        }
    }
}
