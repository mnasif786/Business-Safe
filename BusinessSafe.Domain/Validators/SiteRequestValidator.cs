using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Entities;
using FluentValidation;

namespace BusinessSafe.Domain.Validators
{
    public class SiteRequestValidator : AbstractValidator<Site>
    {
        public SiteRequestValidator(IEnumerable<SiteStructureElement> sites)
        {
            RuleFor(siteRequestStructureValidator => siteRequestStructureValidator.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(siteRequestStructureValidator => siteRequestStructureValidator.Name).Must(n => !Duplicated(n, sites)).WithMessage("Name Already Exists");
        }

        private static bool Duplicated(string name, IEnumerable<SiteStructureElement> sites)
        {
            return sites.Any(s => s.Name == name);
        }
    }
}