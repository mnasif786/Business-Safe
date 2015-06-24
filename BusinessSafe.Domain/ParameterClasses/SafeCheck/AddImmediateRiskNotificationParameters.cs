using System;

namespace BusinessSafe.Domain.ParameterClasses.SafeCheck
{
    public class AddImmediateRiskNotificationParameters
    {
        public Guid Id { get; set; }
        public string Reference { get; set; }
        public string Title { get; set; }
        public string SignificantHazardIdentified { get; set; }
        public string RecommendedImmediateAction { get; set; }
    }
}
