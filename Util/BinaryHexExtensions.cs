using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AocUtil
{
    public static class BinaryHexExtensions
    {
        public static string HexToBinary(this string hex)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var c in hex.ToCharArray())
            {
                var intValue = int.Parse(c.ToString(), System.Globalization.NumberStyles.HexNumber);
                sb.Append(Convert.ToString(intValue, 2).PadLeft(4, '0'));
            }

            return sb.ToString();
        }
        public static string ToBinaryString(this int hex, int length = 16)
        {
            return Convert.ToString(hex, 2).PadLeft(length, '0');
        }
    }
}
