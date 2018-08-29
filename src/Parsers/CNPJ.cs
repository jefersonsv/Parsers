using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Parsers
{
    [System.Diagnostics.DebuggerDisplay("{number}")]
    [TypeConverter(typeof(Parsers.Converter.CnpjTypeConverter))]
    public class CNPJ
    {
        static Int16 length = 14;
        static Regex cnpjValidRegex = new Regex(@"^[0-9]{14}|[0-9]{2}\.[0-9]{3}\.[0-9]{3}\/[0-9]{4}\-[0-9]{2}$", RegexOptions.Compiled);
        string number;

        public CNPJ(string cnpj)
        {
            if (CNPJ.IsCnpj(cnpj))
            {
                number = Utils.ExtractNumbers(cnpj);
            }
            else
            {
                throw new System.FormatException("Número do documento CNPJ não está no formato correto");
            }
        }

        public static implicit operator CNPJ(string number)
        {
            return new CNPJ(number);
        }

        public static CNPJ Parse(string cnpj)
        {
            var match = cnpjValidRegex.Match(cnpj);
            if (match.Success)
            {
                return new CNPJ(Utils.ExtractNumbers(match.Value));
            }
            else
            {
                throw new System.FormatException("Número do documento CNPJ não está no formato correto");
            }
        }

        public override string ToString()
        {
            return this.number.ToString().PadLeft(length, '0');
        }

        public string ToString(bool putMask)
        {
            if (!putMask)
                return this.ToString();
            else
                return Utils.Mask(this.ToString(), "99.999.999/9999-99");
        }

        public static bool IsCnpj(string number)
        {
            return cnpjValidRegex.IsMatch(number.Trim());
        }

        public bool IsCnpj()
        {
            return IsCnpj(number);
        }

        public bool IsValid()
        {
            return IsValid(number);
        }

        public static bool IsValid(string number)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            number = number.Trim();
            number = number.Replace(".", "").Replace("-", "").Replace("/", "");

            if (number.Length != 14)
                return false;

            tempCnpj = number.Substring(0, 12);

            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return number.EndsWith(digito);
        }
    }
}
