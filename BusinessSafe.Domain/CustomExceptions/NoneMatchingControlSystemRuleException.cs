using System;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class NoneMatchingControlSystemRuleException: ApplicationException
    {
        public NoneMatchingControlSystemRuleException(string hazardousSubstanceGroupCode, MatterState? matterState, Quantity? quantity, DustinessOrVolatility? dustinessOrVolatility)
            : base(string.Format("No matching control system rule configured. Hazardous Substance Group Code is {0}. MatterStateId is {1}. QuantityId is {2}. DustinessOrVolatileId is {3}.", hazardousSubstanceGroupCode, matterState.GetValueOrDefault(),quantity.GetValueOrDefault(),dustinessOrVolatility.GetValueOrDefault()))
        {}
    }
}