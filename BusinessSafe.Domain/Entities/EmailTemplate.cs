using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class EmailTemplate : Entity<long>
    {
        public virtual string Name { get; protected set; }
        public virtual string Subject { get; protected set; }
        public virtual string Body { get; protected set; }
        
        public static EmailTemplate Create(string name, string subject, string body)
        {
            var emailTemplate = new EmailTemplate
            {
                Name = name,
                Subject = subject,
                Body  = name
            };

            return emailTemplate;
        }

    }

    public enum EmailTemplateName
    {
        None = 0,
        CompanySummeryChangeNotification = 1,
        SiteAddressChangeNotification = 2
    }
}

