using System;

namespace BusinessSafe.Application.Custom_Exceptions
{
    public class AccidentRecordNotFoundException : NullReferenceException
    {
        public AccidentRecordNotFoundException(long accidentRecordId, long companyId)
            : base(string.Format("Could not find Accident Record with id {0}, belonging to company {1}", accidentRecordId, companyId))
        {}
    }
}