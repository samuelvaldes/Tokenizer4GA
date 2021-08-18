using System;

namespace Tokenizer4GA.Shared.Extensions
{
    public static class Arrays
    {
        public static T GetPreviousElementOfArrayIfAvailable<T>(this T[] array, T element) where T : class
        {
            var currentIndex = Array.IndexOf(array, element);
            if (currentIndex <= 0)
                return element;
            return array[currentIndex - 1];
        }

        public static T GetNextElementOfArrayIfAvailable<T>(this T[] array, T element) where T : class
        {
            var currentIndex = Array.IndexOf(array, element);
            if (currentIndex >= array.Length - 1)
                return element;
            return array[currentIndex + 1];
        }
    }
}
