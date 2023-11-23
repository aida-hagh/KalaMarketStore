using KalaMarketStore.DataLayer.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.Guarantee
{
    public interface IGuaranteeService
    {
        List<ProductGuarantee> ShowAllGuarantee(int page);
        List<ProductGuarantee> ShowAllGuarantee();

        ProductGuarantee FindGuaranteeById(int guaranteeId);

        bool UpdateGuarantee(ProductGuarantee productGuarantee);

        bool DeleteGuarantee(ProductGuarantee productGuarantee);

        bool ExistGuarantee(string guaranteeName,int guaranteeId);

        int AddGuarantee(ProductGuarantee productGuarantee);

        int CountInPage();

    }
}
