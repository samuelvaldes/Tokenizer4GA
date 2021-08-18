using System;
using System.Collections.Generic;
using System.Text;

namespace Tokenizer4GA.Shared.Models.Tracking
{
    public class SyncDelete
    {
        public int TypeOperation { get; set; }
        public string TableOperation { get; set; }
        public int IdReference { get; set; }
    }
}
