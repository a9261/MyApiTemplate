using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Environment
{
    public static class DateTimeHelper
    {
        public static DateTime GetUTCNow()
        {
            return DateTime.UtcNow;
        }

        public static DateTime GetUTCNow(int timeZone)
        {
            return DateTime.UtcNow.AddHours(timeZone);
        }
    }
}