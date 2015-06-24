using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Linq;

using BusinessSafe.Domain.Common;
using BusinessSafe.Infrastructure.Attributes;

namespace BusinessSafe.Application.Common
{
    [DataContract]
    [CoverageExclude]
    public class ValidationFault
    {
        private List<ValidationFaultMessage> _messages;

        public ValidationFault(IEnumerable<ValidationMessage> validationMessages)
        {
            _messages = new List<ValidationFaultMessage>();

            foreach(var validationMessage in validationMessages)
            {
                _messages.Add(new ValidationFaultMessage
                {
                    Field = validationMessage.Field,
                    Text = validationMessage.Text
                });
            }
        }

        [DataMember]
        public List<ValidationFaultMessage> Messages
        {
            get { return _messages; }
            set { _messages = value; }
        }

        [DataMember]
        public string Summary
        {
            get
            {
                var combinedMessage = new StringBuilder("The following validation errors occured: ");

                foreach (var message in _messages)
                {
                    if (message != _messages.First()) combinedMessage.Append(", ");
                    combinedMessage.Append(message.Text);
                }

                return combinedMessage.ToString();
            }
            set { }
        }
    }
}
