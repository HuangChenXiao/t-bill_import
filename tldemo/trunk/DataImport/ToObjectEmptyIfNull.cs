using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataImport
{
    public static class ToObjectEmptyIfNull
    {
        public static string ToStringIfNull(this object text)
        {
            if (text == null)
            {
                return "";
            }
            return text.ToString();
        }
        public static string ToDateIfNull(this object text)
        {
            if (text == null)
            {
                return "";
            }
            return Convert.ToDateTime(text).ToString("yyyy-MM-dd");
        }
        public static decimal ToDecimalIfNull(this object text)
        {
            if (string.IsNullOrEmpty(text.ToString()))
            {
                return 0;
            }
            return Convert.ToDecimal(text);
        }
        public static int ToIntIfNull(this object text)
        {
            if (string.IsNullOrEmpty(text.ToString()))
            {
                return 0;
            }
            return Convert.ToInt32(text);
        }
    }
}
