using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsers
{
    internal class Utils
    {
        public static string ExtractNumbers(string cpfCnpj)
        {
            return new String(cpfCnpj.Where(x => Char.IsDigit(x)).ToArray());
        }

        public static string Mask(string text, string mask)
        {
            if (mask.Count(w => w.Equals('9')) != text.Length)
                throw new ArgumentException("The mask can't fit into text supplied");

            StringBuilder sb = new StringBuilder();
            var charIndex = 0;

            for (int i = 0; i < mask.Length; i++)
            {
                if (mask[i] == '9')
                {
                    sb.Append(text[charIndex]);
                    charIndex++;
                }
                else
                {
                    sb.Append(mask[i]);
                }
            }

            return sb.ToString();
        }
    }
}
