using System.Runtime.Serialization;

namespace BusinessSafe.Domain.Common
{
    /// <summary>
    /// Represents the level of a validation error.
    /// </summary>
    [DataContract]
    public enum MessageType
    {
        [EnumMember]
        Info = 0,

        [EnumMember]
        Warning = 1,

        [EnumMember]
        Error = 2
    }

    /// <summary>
    /// Represents a validation error.
    /// </summary>
    /// <remarks>
    /// Author  :   Paul Davies
    /// Date    :   19th December 2010
    /// </remarks>
    [DataContract(IsReference = true)]
    public class ValidationMessage
    {
        private MessageType _type;
        private string _field;
        private string _text;

        /// <summary>
        /// Gets/sets the error level.
        /// </summary>
        [DataMember]
        public MessageType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// Gets/sets the field that this validation message relates to.
        /// </summary>
        [DataMember]
        public string Field
        {
            get { return _field; }
            set { _field = value; }
        }

        /// <summary>
        /// Gets/sets the error message.
        /// </summary>
        [DataMember]
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        
        /// <summary>
        /// Constructor that creates a validation error with the specified type and message.
        /// </summary>
        /// <param name="type">The type of the error.</param>
        /// <param name="text">The error message.</param>
        public ValidationMessage(MessageType type, string text)
        {
            _type = type;
            _text = text;
        }

        /// <summary>
        /// Constructor that creates a validation error with the specified type, field and message.
        /// </summary>
        /// <param name="type">The type of the error.</param>
        /// <param name="field">The field that this message relates to.</param>
        /// <param name="text">The error message.</param>
        public ValidationMessage(MessageType type, string field, string text)
        {
            _type = type;
            _field = field;
            _text = text;
        }

        /// <summary>
        /// Returns whether this is equal to the specified ValidationMessage.
        /// </summary>
        /// <param name="o">The ValidationMessage to compare to.</param>
        /// <returns>True if they are equal.</returns>
        public override bool Equals(object o)
        {
            var validationMessage = (ValidationMessage)o;
            return validationMessage.Type  == _type
                && validationMessage.Text  == _text
                && validationMessage.Field == _field;
        }

        /// <summary>
        /// Returns a HashCode.
        /// </summary>
        /// <returns>The HashCode.</returns>
        public override int GetHashCode()
        {
            return _type.GetHashCode()
                ^ (_field == null ? 0 : _field.GetHashCode())
                ^ (_text == null ? 0 : _text.GetHashCode());
        }
    }
}
