using System;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace Parsers
{
    [System.Diagnostics.DebuggerDisplay("{number}")]
    [TypeConverter(typeof(Parsers.Converter.CpfTypeConverter))]
    public class CPF
    {
        static Regex cpfValidRegex = new Regex(@"^[0-9]{11}|[0-9]{3}\.[0-9]{3}\.[0-9]{3}\-[0-9]{2}$", RegexOptions.Compiled);

        static Int16 length = 11;

        string number;

        public CPF(string cpf)
        {
            if (CPF.IsCpf(cpf))
            {
                number = Utils.ExtractNumbers(cpf);
            }
            else
            {
                throw new System.FormatException("Número do documento CPF não está no formato correto");
            }
        }

        public static implicit operator CPF(string number)
        {
            return new CPF(number);
        }

        public static CPF Parse(string cpf)
        {
            var match = cpfValidRegex.Match(cpf);
            if (match.Success)
            {
                return new CPF(Utils.ExtractNumbers(match.Value));
            }
            else
            {
                throw new System.FormatException("Número do documento CPF não está no formato correto");
            }
        }

        public override string ToString()
        {
            return number.ToString().PadLeft(length, '0');
        }

        public string ToString(bool putMask)
        {
            if (!putMask)
                return this.ToString();
            else
                return Utils.Mask(this.ToString(), "999.999.999-99");
        }

        public static bool IsCpf(string number)
        {
            return cpfValidRegex.IsMatch(number.Trim());
        }

        public bool IsCpf()
        {
            return IsCpf(number);
        }

        public bool IsValid()
        {
            return IsValid(number);
        }

        public static bool IsValid(string number)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            number = number.Trim();
            number = number.Replace(".", "").Replace("-", "");

            if (number.Length != 11)
                return false;

            tempCpf = number.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return number.EndsWith(digito);
        }
    }
}
