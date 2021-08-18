namespace Tokenizer4GA.Shared.Extensions
{
    public static class IsValid
    {
        public static bool IsValidString(this string value)
        {
            return !string.IsNullOrEmpty(value) || !string.IsNullOrWhiteSpace(value);
        }

        public static bool IsDifferentFromNullEmptyOrSpace(this object value)
        {
            return !string.IsNullOrEmpty(value.ToString()) || !string.IsNullOrWhiteSpace(value.ToString());
        }
    }
}
