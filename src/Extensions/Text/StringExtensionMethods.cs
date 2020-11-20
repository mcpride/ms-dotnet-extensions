using System;
using System.Collections.Generic;

namespace MS.Extensions.Text
{
    public static class StringExtensionMethods
    {
        public static string Capitalize(this string s)
        {
            var a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
    }
}