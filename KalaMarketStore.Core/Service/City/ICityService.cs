using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.City
{
    public interface ICityService
    {
        List<DataLayer.Entities.Address.City> ShowAllCity(int page);
        IEnumerable<DataLayer.Entities.Address.City> ShowAllCity();

        int AddCity(DataLayer.Entities.Address.City City);

        bool UpdateCity(DataLayer.Entities.Address.City City);

        bool DeleteCity(DataLayer.Entities.Address.City City);

        bool ExistCity(string FaName, int CityId);

        DataLayer.Entities.Address.City FindCityById(int CityId);

        int CountInPage();
    }
}
