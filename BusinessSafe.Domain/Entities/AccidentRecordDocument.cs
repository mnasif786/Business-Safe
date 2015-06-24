using System;
using BusinessSafe.Application.Request;

namespace BusinessSafe.Domain.Entities
{
    public class AccidentRecordDocument : Document
    {
      
        public virtual AccidentRecord AccidentRecord { get; set; }
        
        public override string DocumentReference
        {
            get { return ""; }
        }

        public override string SiteReference
        {
            get { return ""; }
        }

        public static AccidentRecordDocument Create(string description, string filename, long documentLibraryId, DocumentType documentType, AccidentRecord accidentRecordRequest, UserForAuditing user)
        {
            return new AccidentRecordDocument
            {
                Description = description,
                ClientId = accidentRecordRequest.CompanyId,
                Title = filename,
                CreatedBy = user,
                CreatedOn = DateTime.Now,
                Filename = filename,
                DocumentLibraryId = documentLibraryId,
                AccidentRecord = accidentRecordRequest,
                Deleted = false,
                DocumentType = documentType
            };
        }
    }
}
