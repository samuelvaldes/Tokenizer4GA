namespace Tokenizer4GA.Shared.Extensions
{
    public static class ConvertTo
    {
        public static int ConvertToInt(this string value)
        {
            var returnValue = default(int);
            if(int.TryParse(value,out int aux))
            {
                returnValue = aux;
            }
            return returnValue;
        }
    }
}
