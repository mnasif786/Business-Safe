using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Validators;
using FluentValidation;

namespace BusinessSafe.Domain.Entities
{
    public class Site : SiteStructureElement
    {
        public virtual long? SiteId { get; set; }
        public virtual string Reference { get; set; }

        public static Site Create(long? siteId, SiteStructureElement parent, long clientId, string siteName, string siteReference, string siteContact, UserForAuditing currentUser)
        {            
            return new Site
                       {
                           SiteId = siteId, 
                           ClientId = clientId, 
                           Name = siteName, 
                           Reference = siteReference,
                           Parent = parent,
                           SiteContact = siteContact,
                           CreatedBy = currentUser,
                           CreatedOn = DateTime.Now,
                           LastModifiedBy = currentUser,
                           LastModifiedOn = DateTime.Now
                       };
        }

        public virtual void Validate(IEnumerable<SiteStructureElement> sites)
        {
            new SiteRequestValidator(sites).ValidateAndThrow(this);
        }

        public virtual void Update(SiteStructureElement parent, string name, string reference, long clientId, string siteContact, UserForAuditing currentUser, bool isSiteOpenRequest, bool isSiteClosedRequest)
        {
            Parent = parent;
            Name = name;
            Reference = reference;
            ClientId = clientId;
            SiteContact = siteContact;
            SetLastModifiedDetails(currentUser);

            if (isSiteOpenRequest)
            {
                SiteClosedDate = null;
            }

            if (isSiteClosedRequest)
            {
                SiteClosedDate = DateTime.Now;
            }
            
        }
    }
}