using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.Province
{
    public interface IProvinceService
    {

        List<DataLayer.Entities.Address.Province> ShowAllProvince(int page);
        IEnumerable<DataLayer.Entities.Address.Province> ShowAllProvince();

        int AddProvince(DataLayer.Entities.Address.Province Province);

        bool UpdateProvince(DataLayer.Entities.Address.Province Province);

        bool DeleteProvince(DataLayer.Entities.Address.Province Province);

        bool ExistProvince(string FaName, int ProvinceId);

        DataLayer.Entities.Address.Province FindProvinceById(int ProvinceId);

        int CountInPage();
    }
}
