using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.Application.Request
{
    public class SaveInjuryRequest
    {
        public SaveInjuryRequest()
        {}

        public SaveInjuryRequest(bool runMatchCheck)
        {
            RunMatchCheck = runMatchCheck;
        }

        public SaveInjuryRequest(long id, string name, long companyId, long? accidentRecordId, bool runMatchCheck, Guid userId)
        {
            Id = id;
            Name = name;
            CompanyId = companyId;
            AccidentRecordId = accidentRecordId;
            RunMatchCheck = runMatchCheck;
            UserId = userId;
        }
        
        public long Id { get; private set; }

        [Required(ErrorMessage = "Please enter Name.")]
        public string Name { get; private set; }

        public long CompanyId { get; private set; }
        public long? AccidentRecordId { get; private set; }
        public bool RunMatchCheck { get; private set; }

        public Guid UserId { get; set; }
    }
}