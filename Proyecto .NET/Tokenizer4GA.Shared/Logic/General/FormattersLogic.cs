using Tokenizer4GA.Shared.Constants;
using Tokenizer4GA.Shared.WebServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tokenizer4GA.Shared.Logic.General
{
    public static class FormattersLogic
    {
        public static string CreateCompleteErrorMessage(string specificMessage, BaseResponse response) =>
            string.IsNullOrWhiteSpace(response.Message) ?
                $"{specificMessage} ({response.ResponseCode})" :
                $"{specificMessage} ({response.ResponseCode}: {response.Message})";

        public static string CreateCompleteMessage(string specificMessage, BaseResponse response) =>
                string.IsNullOrWhiteSpace(response.Message) ?
                $"{specificMessage}" :
                $"{response.Message}";

        public static string CreateResourcePathWithParameters(string resource, Dictionary<string, string> parameters)
        {
            var joinedParameters = new List<string>();
            foreach (var key in parameters.Keys)
                joinedParameters.Add(string.Format($"{key}={parameters[key]}"));
            return $"{resource}?{string.Join("&", joinedParameters)}";
        }

        public static string FormatPhone(string phone)
        {
            phone = phone
                .Insert(8, Strings.Space)
                .Insert(6, Strings.Space);
            var phoneFirstTwoDigits = phone.Substring(0, 2);
            if (Collections.TwoDigitCodes.Contains(phoneFirstTwoDigits))
                phone = phone
                    .Insert(4, Strings.Space)
                    .Insert(2, Strings.Space);
            else
                phone = phone.Insert(3, Strings.Space);
            return phone;
        }

        public static int GetDayOfWeekNumberWhenMondayIsFirst(DayOfWeek dayOfWeek) =>
            dayOfWeek switch
            {
                DayOfWeek.Monday => 0,
                DayOfWeek.Tuesday => 1,
                DayOfWeek.Wednesday => 2,
                DayOfWeek.Thursday => 3,
                DayOfWeek.Friday => 4,
                DayOfWeek.Saturday => 5,
                DayOfWeek.Sunday => 6,
                _ => 0,
            };

        public static string GetResourcePathParameters(string resource)
        {
            if (!resource.Contains('?'))
                return null;
            return resource.Split('?')[1];
        }

        public static string GetResourcePathWithoutParameters(string resource) =>
            resource.Split('?')[0];

        public static string RemoveTrailingAndDoubleSpaces(string text)
        {
            text = text.Trim();
            while (text.Contains(Strings.DoubleSpace))
                text = text.Replace(Strings.DoubleSpace, Strings.Space);
            return text;
        }
        
        public static string UnformatPhone(string formattedPhone) =>
            formattedPhone.Replace(Strings.Space, string.Empty);
    }
}
