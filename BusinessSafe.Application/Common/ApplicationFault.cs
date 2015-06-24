using System.Runtime.Serialization;
using BusinessSafe.Infrastructure.Attributes;

namespace BusinessSafe.Application.Common
{
    [DataContract]
    [CoverageExclude]
    public class ApplicationFault
    {
        protected string _errorCode;

        public ApplicationFault(string errorCode)
        {
            _errorCode = errorCode;
        }

        [DataMember]
        public string ErrorCode
        {
            get { return _errorCode; }
            set { _errorCode = value; }
        }
    }
}