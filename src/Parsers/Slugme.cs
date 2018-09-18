using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Parsers.Converter;

namespace Parsers
{
    [System.Diagnostics.DebuggerDisplay("{Slug}")]
    [TypeConverter(typeof(SlugmeTypeConverter))]
    public class Slugme
    {
        public string Slug { get; private set; }
        public string Text { get; private set; }

        public string Encoded { get; private set; }

        public Slugme(string text)
        {
            Text = text;
            Slug = Underscore(RemoveDiacritics(text)).Replace("_", "-");
        }

        public static implicit operator Slugme(string text)
        {
            return new Slugme(text);
        }

        public override string ToString()
        {
            return Slug;
        }

        string Underscore(string input)
        {
            return Regex.Replace(
                Regex.Replace(
                    Regex.Replace(input, @"([\p{Lu}]+)([\p{Lu}][\p{Ll}])", "$1_$2"), @"([\p{Ll}\d])([\p{Lu}])", "$1_$2"), @"[-\s]", "_").ToLower();
        }

        static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
