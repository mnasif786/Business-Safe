using System;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class AuditedUserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}