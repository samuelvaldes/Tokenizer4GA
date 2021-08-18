using System;

namespace Tokenizer4GA.Shared.Data
{
    public interface IEntityTracking
    {
        bool Deleted { get; set; }
        DateTimeOffset CreatedOn { get; set; }
        DateTimeOffset UpdatedOn { get; set; }
        //byte[] Version { get; set; }
    }
}
