using System.Collections.Generic;

namespace BusinessSafe.Domain.InfrastructureContracts.Email
{
    public interface IEmail
    {
        string From { get; set; }
        IList<string> To { get; set; }
        IList<string> CC { get; set; }
        IList<string> Bcc { get; set; }
        IList<Attachment> Attachments { get; set; }
        string Subject { get; set; }
        string Body { get; set; }
        void Send();
    }

    public class FakeEmail : IEmail
    {
        public string From{ get; set; }
        public IList<string> To{ get; set; }
        public IList<string> CC{ get; set; }
        public IList<string> Bcc{ get; set; }
        public IList<Attachment> Attachments{ get; set; }
        public string Subject{ get; set; }
        public string Body { get; set; }
        public void Send()
        {
            
        }
    }
}
