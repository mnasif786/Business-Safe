using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Entities;
using FluentValidation;

namespace BusinessSafe.Domain.Validators
{
    //todo: this all probably needs to go as it has been replicated in PO now, and should be done there.
    public class RegisterEmployeeAsUserValidator : AbstractValidator<Employee>
    {
        public RegisterEmployeeAsUserValidator()
        {
            RuleFor(employee => employee.ContactDetails).Must(EmailAndPhoneNumberPresent).WithMessage(
                "Email and phone number must both be entered for employee.");
        }

        private static bool EmailAndPhoneNumberPresent(IList<EmployeeContactDetail> contactDetails)
        {
            var validContactDetails = contactDetails.Where(
                contactDetailsRecord => contactDetailsRecord.Email != null
                                        &&
                                        (contactDetailsRecord.Telephone1 != null ||
                                         contactDetailsRecord.Telephone2 != null));

            return validContactDetails.Any();
        }
    }
}
