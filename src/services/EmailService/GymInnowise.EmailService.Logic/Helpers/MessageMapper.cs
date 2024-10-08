﻿using GymInnowise.EmailService.Logic.Results;
using OneOf;
using System.Text;

namespace GymInnowise.EmailService.Logic.Services
{
    public static class MessageMapper
    {
        public static OneOf<string, NotMapped> BuildMessage(string templateBody, Dictionary<string, string> data)
        {
            if (!CanBeMapped(templateBody, data.Keys))
            {
                return new NotMapped();
            }

            var parts = templateBody.Split(["{{", "}}"], StringSplitOptions.None);
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < parts.Length; i++)
            {
                if (i % 2 == 0)
                {
                    stringBuilder.Append(parts[i]);

                    continue;
                }

                if (data.TryGetValue(parts[i], out string? value))
                {
                    stringBuilder.Append(value);
                }
                else
                {
                    stringBuilder.Append($"{{{{{parts[i]}}}}}");
                }
            }

            return stringBuilder.ToString();
        }

        public static bool CanBeMapped(string templateBody, IEnumerable<string> keys)
        {
            var startIndex = 0;
            foreach (var key in keys)
            {
                var index = templateBody.IndexOf($"{{{{{key}}}}}", startIndex, StringComparison.Ordinal);
                if (index == -1)
                {
                    return false;
                }

                startIndex = index;
            }

            return true;
        }
    }
}