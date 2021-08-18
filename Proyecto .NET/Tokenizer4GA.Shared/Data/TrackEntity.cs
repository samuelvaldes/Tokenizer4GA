using System;

namespace Tokenizer4GA.Shared.Data
{
    public class TrackEntity : BaseEntity, IEntityTracking
    {
        public bool Deleted { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
    }
}
