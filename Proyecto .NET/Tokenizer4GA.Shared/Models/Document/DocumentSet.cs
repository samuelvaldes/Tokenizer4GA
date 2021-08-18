namespace Tokenizer4GA.Shared.Models.Document
{
    using System;
    public class DocumentSet
    {
        public int? Id { get; set; }
        public int VehicleId { get; set; }
        public bool Saved { get; set; }
        public string IsLoadedIcon { get => Saved ? "check_circle_outline_black_48.png" : "add_a_photo_black_48.png"; }
        public DocumentType DocumentType { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime DocumentDate { get; set; }
        public DateTime DocumentOrder { get; set; }
        public DocumentSetFile[] Files { get; set; }
        public DocumentSetFile FirstFile { get; set; }
    }
}
