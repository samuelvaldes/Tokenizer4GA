namespace Tokenizer4GA.Shared.Models.Information
{
    public class Menu
    {
        public int? Id { get; set; }
        public int? ParentId { get; set; }
        public int MenuOrder { get; set; }
        public string ImageDescriptionUri { get; set; }
        public string Description { get; set; }
        public string ImageUri { get; set; }
        public Menu[] Submenus { get; set; }
        public string DefaultUri { get; set; }
    }
}
