using System;
using System.ServiceModel;

using BusinessSafe.Infrastructure.Attributes;

namespace BusinessSafe.Infrastructure.Logging
{
    [CoverageExclude]
    public class ContextGuidExtension : IExtension<OperationContext>
    {
        public Guid ContextGuid { get; set; }
        public void Attach(OperationContext owner) { }
        public void Detach(OperationContext owner) { }
    }
}
