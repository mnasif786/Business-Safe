using System.Runtime.Serialization;

using BusinessSafe.Infrastructure.Attributes;

namespace BusinessSafe.Application.Common
{
    [DataContract]
    [CoverageExclude]
    public class ValidationFaultMessage
    {
        [DataMember]
        public string Field { get; set; }

        [DataMember]
        public string Text { get; set; }
    }
}
