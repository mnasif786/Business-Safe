using System;

namespace BusinessSafe.Application.Implementations
{
    public class TryingToDeleteSignificantFindingFromFireAnswer : ArgumentException
    {
        public TryingToDeleteSignificantFindingFromFireAnswer(long fireAnswerId)
            : base(string.Format("Trying to delete significant finding from Fire Answer ID {0}. No Significant Finding exists.", fireAnswerId))
        { }
    }
}