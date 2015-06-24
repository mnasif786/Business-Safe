using System.Collections.Generic;

namespace BusinessSafe.Application.Response
{
    public class UpdateNonEmployeeResponse
    {
        public long NonEmployeeId { get; set; }
        public bool Success { get;  set; }
        public string Message { get; set; }
        public IEnumerable<string> Matches{ get; set; }
        
        public UpdateNonEmployeeResponse()
        {
            Message = string.Empty;
        }

        public static UpdateNonEmployeeResponse NonEmployeeMatchingNamesExists(IEnumerable<string>matchingNames)
        {
           return new UpdateNonEmployeeResponse
                      {
                               Success = false,
                               Message = "Name already exists",
                               Matches = matchingNames
                           };
           
        }

        public static UpdateNonEmployeeResponse UpdatedSuccessfully(long updatedNonEmployeeId)
        {
            return new UpdateNonEmployeeResponse
                       {
                           Success = true,
                           NonEmployeeId = updatedNonEmployeeId
                       };
           
        }
    }
}