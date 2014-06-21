using System;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace RomanNumbers
{
    public class RomanConverter
    {
        private static readonly String[] Units = { "", "I", "II", "III", "V", "IV", "VI", "VII", "VIII", "IX" };
        private static readonly String[] Tens = { "", "X", "XX", "XXX", "L", "XL", "LX", "LXX", "LXXX", "XC" };
        private static readonly String[] Hundreds = { "", "C", "CC", "CCC", "D", "CD", "DC", "DCC", "DCCC", "CM" };
        private static readonly String[] Thousands = { "", "M", "MM", "MMM" };

        public static string ArabicToRoman(int number)
        {
            if (number > 0 && number < 4000)
            {
                int u = number % 10;
                number = number / 10;
                int d = number % 10;
                number = number / 10;
                int c = number % 10;
                number = number / 10;
                int m = number % 10;

                StringBuilder sb = new StringBuilder();
                sb.Append(Thousands[m]);
                if (c == 5)
                    sb.Append(Hundreds[c - 1]);
                else
                    if (c == 4)
                        sb.Append(Hundreds[c + 1]);
                    else
                        sb.Append(Hundreds[c]);
                if (d == 5)
                    sb.Append(Tens[d - 1]);
                else
                    if (d == 4)
                        sb.Append(Tens[d + 1]);
                    else
                        sb.Append(Tens[d]);
                if (u == 5)
                    sb.Append(Units[u - 1]);
                else
                    if (u == 4)
                        sb.Append(Units[u + 1]);
                    else
                        sb.Append(Units[u]);
                //sb.Append(Tens[d]);
                //sb.Append(Units[u]);
                return sb.ToString();
                //System.out.println("Numero em numeracao romana " + numero);
                //System.out.println();
                //System.out.println("Introduza o numero:");
            }
            return null;
        }

        public static int RomanToArabic(string roman)
        {
            if (String.IsNullOrEmpty(roman))
                return -1;
            int acc = 0;

            roman = roman.ToUpper();

            acc += Extract(Units, roman, out roman);
            acc += Extract(Tens, roman, out roman) * 10;
            acc += Extract(Hundreds, roman, out roman) * 100;
            acc += Extract(Thousands, roman, out roman) * 1000;

            if (!String.IsNullOrEmpty(roman))
                return -1;
            return acc;
        }

        private static int Extract(string[] values, string roman, out string updatedRoman)
        {
            int romanLastIndex = roman.Length - 1;
            for (int i = values.Length - 1; i > 0; i--)
            {

                bool found = true;

                for (int uIndex = values[i].Length - 1, rIndex = romanLastIndex;
                    uIndex >= 0;
                    --uIndex, --rIndex)
                {
                    if (rIndex < 0)
                    {
                        found = false;
                        break;
                    }
                    if (roman[rIndex] != values[i][uIndex])
                        found = false;
                }
                if (found)
                {
                    if (i == 5)
                    {
                        updatedRoman = roman.Substring(0, roman.Length - values[i].Length);
                        return 4;
                    }
                    if (i == 4)
                    {
                        updatedRoman = roman.Substring(0, roman.Length - values[i].Length);
                        return 5;
                    }
                    updatedRoman = roman.Substring(0, roman.Length - values[i].Length);
                    return i;
                }
            }
            updatedRoman = roman;
            return 0;
        }
    }
}
