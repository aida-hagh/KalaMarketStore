using KalaMarketStore.Core.ViewModel;
using KalaMarketStore.DataLayer.Entities.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.Address
{
    public interface IAddressService
    {
        #region Province

        List<DataLayer.Entities.Address.Province> ShowAllProvince();
        int AddProvince(DataLayer.Entities.Address.Province province);
        bool UpdateProvince(DataLayer.Entities.Address.Province province);
        bool DeleteProvince(DataLayer.Entities.Address.Province province);

        bool ExistProvince(int provinceid, string provincename);
        DataLayer.Entities.Address.Province FindProvincebyId(int provinceid);
        #endregion



        #region City

        List<DataLayer.Entities.Address.City> ShowAllCity();
        int AddCity(DataLayer.Entities.Address.City City);
        bool UpdateCity(DataLayer.Entities.Address.City City);
        bool DeleteCity(DataLayer.Entities.Address.City City);
        bool ExistCity(DataLayer.Entities.Address.City City);
        DataLayer.Entities.Address.City FindCitybyId(int Cityid);
        List<DataLayer.Entities.Address.City> ShowAllCityForProvince(int provinceid);
        #endregion


        #region Address
        ShowAddressForUserViewModel FindAddressForUser(int userid);

        int AddUserAddress(UserAddress useraddress);
       

        bool UpdateAddress(UserAddress useraddress);
        bool DeleteAddress(UserAddress useraddress);

        #endregion
    }
}
