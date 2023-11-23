using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.ExtentionMethod
{
    public static class ReplacePrice
    {
        public static int replacePrice(this string price)
        {
            int _price = 0;

            if (!String.IsNullOrEmpty(price))
            {
                _price = int.Parse(price.Replace(",", ""));
            }
            return _price;  
        }
    }
}
