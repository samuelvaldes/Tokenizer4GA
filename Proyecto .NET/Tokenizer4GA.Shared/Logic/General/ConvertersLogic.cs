using Tokenizer4GA.Shared.Constants;
using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Tokenizer4GA.Shared.Logic.General
{
    public static class ConvertersLogic
    {
        public static async Task<string> ConvertStreamToBase64Async(Stream stream)
        {
            if (!stream.CanSeek)
                return null;
            var bytes = new byte[stream.Length];
            await stream.ReadAsync(bytes, 0, (int)stream.Length);
            return Convert.ToBase64String(bytes);
        }
    }
}
