using System;
using System.Collections.Generic;
using System.Text;

namespace Tokenizer4GA.Shared.Models.Tracking
{
    public class TransactionModel<T>
    {
        public T Data { get; set; }
        public List<SyncDelete> DataDelete { get; set; }
        public int Version { get; set; }
        public DateTimeOffset Offset { get; set; }
    }
}
