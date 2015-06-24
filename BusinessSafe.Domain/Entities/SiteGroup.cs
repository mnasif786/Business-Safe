using System;
using System.Linq;
using System.Collections.Generic;
using BusinessSafe.Domain.Validators;
using FluentValidation;

namespace BusinessSafe.Domain.Entities
{
    public class SiteGroup : SiteStructureElement
    {
        public static SiteGroup Create(SiteStructureElement parent, long clientId, string siteName, UserForAuditing currentUser)
        {
            return new SiteGroup
                       {
                           ClientId = clientId,
                           Name = siteName,
                           Parent = parent,
                           CreatedOn = DateTime.Now,
                           CreatedBy = currentUser
                       };
        }

        public virtual bool HasChildren
        {
            get { return Children.Any(); }
        }

        public virtual void Validate(IEnumerable<SiteStructureElement> allSitesWithGivenName)
        {
            new SiteAddressRequestValidator(allSitesWithGivenName).ValidateAndThrow(this);
        }

        public virtual void Update(SiteStructureElement parent, string name, UserForAuditing currentUser)
        {
            Parent = parent;
            Name = name;
            SetLastModifiedDetails(currentUser);
        }
    }
}