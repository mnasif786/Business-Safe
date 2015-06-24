﻿using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class DocumentType : Entity<long>
    {
        public virtual string Name { get; set; }
    }
}