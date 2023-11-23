using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.ExtentionMethod
{
    public static class ConvertDate
    {
        public static string MiladiToShamsi(this DateTime dateTime) 
        {
    PersianCalendar pc = new PersianCalendar();
            var date = pc.GetYear(dateTime).ToString("0000") + "/" + pc.GetMonth(dateTime).ToString("00") + "/" + pc.GetDayOfMonth(dateTime).ToString("00");
            return date;
        }
    
    }
}
