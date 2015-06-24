using System.Collections.Generic;
using System.Linq;

namespace BusinessSafe.Domain.Common
{
    /// <summary>
    ///   Collection container for validation messages.
    /// </summary>
    /// <remarks>
    ///   Author : Paul Davies Date : 20th December
    /// </remarks>
    public class ValidationMessageCollection : List<ValidationMessage>
    {
        /// <summary>
        ///   Gets a list of messages with the status 'Info'.
        /// </summary>
        public List<ValidationMessage> Infos
        {
            get
            {
                return (from validationMessage
                    in this
                        where validationMessage.Type == MessageType.Info
                        select validationMessage).ToList();
            }
        }

        /// <summary>
        ///   Gets a list of messages with the status 'Warning'.
        /// </summary>
        public List<ValidationMessage> Errors
        {
            get
            {
                return (from validationMessage
                    in this
                        where validationMessage.Type == MessageType.Error
                        select validationMessage).ToList();
            }
        }

        /// <summary>
        ///   Gets a list of messages with the status 'Error'.
        /// </summary>
        public List<ValidationMessage> Warnings
        {
            get
            {
                return (from validationMessage
                    in this
                        where validationMessage.Type == MessageType.Warning
                        select validationMessage).ToList();
            }
        }

        /// <summary>
        ///   Adds a ValidationMessage with type 'Error' and the specified text.
        /// </summary>
        /// <param name="text"> The text for the ValidationMessage. </param>
        public void AddError(string text)
        {
            Add(new ValidationMessage(MessageType.Error, text));
        }

        /// <summary>
        ///   Adds a ValidationMessage with type 'Error' and the specified field and text.
        /// </summary>
        /// <param name="field"> The field the ValidationMessage relates to. </param>
        /// <param name="text"> The text for the ValidationMessage. </param>
        public void AddError(string field, string text)
        {
            Add(new ValidationMessage(MessageType.Error, field, text));
        }

        /// <summary>
        ///   Adds a ValidationMessage with type 'Warning' and the specified text.
        /// </summary>
        /// <param name="text"> The text for the ValidationMessage. </param>
        public void AddWarning(string text)
        {
            Add(new ValidationMessage(MessageType.Warning, text));
        }

        /// <summary>
        ///   Checks whether the collection contains a ValidationMessage with type 'Error' and the specified field and text.
        /// </summary>
        /// <param name="field"> The field the ValidationMessage relates to. </param>
        /// <param name="text"> The text for the ValidationMessage. </param>
        /// <returns> True if a ValidationMessage is found in this collection. </returns>
        public bool ContainsError(string field, string text)
        {
            return Contains(new ValidationMessage(MessageType.Error, field, text));
        }
    }
}