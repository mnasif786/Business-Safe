using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BusinessSafe.Domain.Entities
{
    public enum AccidentRecordStatusEnum
    {   
        [Description("Closed")]
        Closed = 0,

        [Description("Open")]
        Open = 1
    }
}
