namespace Tokenizer4GA.Shared.Models.Document
{
    public class DocumentSetFile
    {
        public int? Id { get; set; }
        public int DocumentId { get; set; }
        public string Description { get; set; }
        public string Base64 { get; set; }
        public string Url { get; set; }
        public DocumentSetFileType Type { get; set; }
    }
}
