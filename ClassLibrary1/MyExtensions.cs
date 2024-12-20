using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public static class MyExtensions
    {
        public static string ToNiceString(this DateTime dateTime)
        {
            return dateTime.ToString($"{dateTime.Year}:{dateTime.Month.ToString("00")}:{dateTime.Day.ToString("00")} {dateTime.Hour.ToString("00")}:{dateTime.Minute.ToString("00")}:{dateTime.Second.ToString("00")}");
        }
    }
}
