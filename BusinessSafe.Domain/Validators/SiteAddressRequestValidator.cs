using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Entities;
using FluentValidation;

namespace BusinessSafe.Domain.Validators
{
    public class SiteAddressRequestValidator : AbstractValidator<SiteGroup>
    {
        public SiteAddressRequestValidator(IEnumerable<SiteStructureElement> allSitesWithGivenName)
        {
            RuleFor(siteRequestStructureValidator => siteRequestStructureValidator.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(siteRequestStructureValidator => siteRequestStructureValidator.Name).Must(n => !Duplicated(n, allSitesWithGivenName)).WithMessage("Name Already Exists");
        }

        private static bool Duplicated(string name, IEnumerable<SiteStructureElement> sites)
        {
            return sites.Any(s => s.Name == name);
        }


    }
}