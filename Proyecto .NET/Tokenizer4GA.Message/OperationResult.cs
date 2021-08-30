using System;
using System.Collections.Generic;
using System.Text;

namespace Message
{
    public abstract class OperationResult
    {
        public bool Success { get; set; }
        public string ExceptionMessage { get; set; }
    }
}
