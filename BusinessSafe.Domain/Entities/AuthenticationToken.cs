using System;

using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class AuthenticationToken : BaseEntity<Guid>
    {
        public virtual ApplicationToken ApplicationToken { get; set; }
        public virtual User User { get; set; }
        public virtual DateTime LastAccessDate { get; set; }
        public virtual bool IsEnabled { get; set; }
        public virtual string ReasonForDeauthorisation { get; set; }

        public static AuthenticationToken Create(User user, ApplicationToken applicationToken)
        {
            var token = new AuthenticationToken()
                   {
                       Id = Guid.NewGuid(),
                       User = user,
                       ApplicationToken = applicationToken,
                       CreatedOn = DateTime.Now,
                       IsEnabled = true
                   };
            token.UpdateLastAccessedDate();

            return token;
        }

        private void UpdateLastAccessedDate()
        {
            LastAccessDate = DateTime.Now;
        }
    }
}
