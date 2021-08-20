namespace Tokenizer4GA.Shared.Constants
{
    public static class Strings
    {
        public const string DoubleSpace = "  ";
        public const string EmptyJson = "{}";
        public const string Space = " ";

        public const string GuadalajaraCode = "33";
        public const string MexicoCityCode = "55";
        public const string MonterreyCode = "81";

        public const string PdfFileExtension = "pdf";
        public const string PngFileExtension = "png";    
        
        public const string EmailPattern = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
        public const string ExtendedAsciiPattern = @"[\x20-\xFF]";
        public const string PasswordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";
        public const string GuestPasswordPattern = @"^\d+$";
    }
}
