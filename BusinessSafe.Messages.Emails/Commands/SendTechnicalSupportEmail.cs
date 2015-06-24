using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace BusinessSafe.Messages.Emails.Commands
{
    public class SendTechnicalSupportEmail : IMessage
    {
       public string Name { get; set; }
       public string FromEmailAddress { get; set; }
       public string Message { get; set; }
    }
}
