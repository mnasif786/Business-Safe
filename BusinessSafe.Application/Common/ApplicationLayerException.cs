using System;

namespace BusinessSafe.Application.Common
{
    public class ApplicationLayerException : Exception
    {
        public string Code { get; set; }

        public ApplicationLayerException(string code) : base("An error occured with error code: " + code)
        {
            Code = code;
        }
    }
}