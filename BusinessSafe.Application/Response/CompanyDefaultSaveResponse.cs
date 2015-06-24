using System.Collections.Generic;

namespace BusinessSafe.Application.Response
{
    public class CompanyDefaultSaveResponse
    {
        public long Id { get; private set; }
        public bool Success { get;  private set; }
        public string Message { get; private set; }
        public IEnumerable<string> Matches { get; private set; }
        
        public CompanyDefaultSaveResponse()
        {
            Message = string.Empty;
        }

        public static CompanyDefaultSaveResponse CompanyDefaultMatches(IEnumerable<string> matches)
        {
            
                return new CompanyDefaultSaveResponse
                           {
                               Success = false,
                               Message = "Name already exists",
                               Matches = matches
                           };
             
        }

        public static CompanyDefaultSaveResponse CreateSavedSuccessfullyResponse(long id)
        {
          return new CompanyDefaultSaveResponse
                     {
                    Success = true,
                    Id = id
                };
           
        }

        public static CompanyDefaultSaveResponse CreateValidationFailedResponse(string message)
        {
            return new CompanyDefaultSaveResponse
            {
                Success = false,
                Message = message
            };
        }
    }
}