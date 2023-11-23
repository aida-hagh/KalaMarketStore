using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.DisCount
{
    public interface IDisCountService
    {
        int CheckDisCount(int cartid, string discountCode);
    }
}
