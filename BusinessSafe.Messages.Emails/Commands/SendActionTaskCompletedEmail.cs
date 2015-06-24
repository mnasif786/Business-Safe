using System;
using NServiceBus;

namespace BusinessSafe.Messages.Emails.Commands
{
    public class SendActionTaskCompletedEmail : IMessage
    {
        public Guid TaskGuid;
        
    }
}
