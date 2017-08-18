using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataImport
{
    public static class Helper
    {
        public static string ToStringIfNull(this string txt) {
            if (txt==null)
            {
                return "";
            }
            return txt;
        }
    }
}
