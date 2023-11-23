using KalaMarketStore.Core.ViewModel;
using KalaMarketStore.DataLayer.Context;
using KalaMarketStore.DataLayer.Entities.Address;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.Address
{
    public class AddressService : IAddressService
    {
        private readonly AppDbContext context;
        public AddressService(AppDbContext context)
        {
            this.context = context;
        }


        public int AddCity(DataLayer.Entities.Address.City City)
        {
            try
            {
                context.Cities.Add(City);
                context.SaveChanges();
                return City.CityId;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int AddProvince(DataLayer.Entities.Address.Province province)
        {
            try
            {
                context.Provinces.Add(province);
                context.SaveChanges();
                return province.ProvinceId;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public int AddUserAddress(UserAddress useraddress)
        {
            try
            {
                context.UserAddresses.Add(useraddress);
                context.SaveChanges();
                return useraddress.UserAddressId;
            }
            catch (Exception)
            {

                return 0;
            }
        }      
        
  
        public bool DeleteAddress(UserAddress useraddress)
        {
            try
            {
                useraddress.IsDelete = true;
                context.Update(useraddress);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteCity(DataLayer.Entities.Address.City City)
        {
            try
            {
                City.IsDelete = true;
                context.Update(City);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteProvince(DataLayer.Entities.Address.Province province)
        {
            try
            {
                province.IsDelete = true;
                context.Update(province);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ExistCity(DataLayer.Entities.Address.City City)
        {
            return context.Cities.Any(x=>x.CityName==City.CityName && x.CityId!=City.CityId);
        }

        public bool ExistProvince(int provinceid, string provincename)
        {
            return context.Provinces.Any(x => x.ProvinceName == provincename && x.ProvinceId != provinceid);
        }

        public ShowAddressForUserViewModel FindAddressForUser(int userid)
        {
            var useraddres = (from ua in context.UserAddresses
                              join p in context.Provinces on ua.ProvinceId equals p.ProvinceId
                              join c in context.Cities on p.ProvinceId equals c.ProvinceId

                              where (!ua.IsDelete && ua.UserId == userid)
                              select new ShowAddressForUserViewModel
                              {
                                  UserId = ua.UserId,
                                  UserAddressId = ua.UserAddressId,
                                  CityName = c.CityName,
                                  FullAddress = ua.FullAddress,
                                  Phone = ua.Phone,
                                  Mobile = ua.Mobile,
                                  Plaque = ua.Plaque,
                                  PostalCode = ua.PostalCode,
                                  ProvinceName = p.ProvinceName,
                                  RecipientName = ua.RecipientName,
                                  ProvinceId = p.ProvinceId,
                                  CityId = c.CityId,
                                  
                              }).FirstOrDefault();
            return useraddres;
        }

        public DataLayer.Entities.Address.City FindCitybyId(int Cityid)
        {
           return context.Cities.Find(Cityid);
        }

        public DataLayer.Entities.Address.Province FindProvincebyId(int provinceid)
        {
            return context.Provinces.Find(provinceid);
        }

        public List<DataLayer.Entities.Address.City> ShowAllCity()
        {
           return context.Cities.Where(x=>!x.IsDelete).ToList();
        }

        public List<DataLayer.Entities.Address.City> ShowAllCityForProvince(int provinceid)
        {
            return context.Cities.Where(c => !c.IsDelete && c.ProvinceId == provinceid).ToList();
        }

        public List<DataLayer.Entities.Address.Province> ShowAllProvince()
        {
            return context.Provinces.Where(x => !x.IsDelete).ToList();
        }

        public bool UpdateAddress(UserAddress useraddress)
        {
            try
            {
                context.Update(useraddress);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UpdateCity(DataLayer.Entities.Address.City City)
        {
            try
            {
                context.Update(City);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UpdateProvince(DataLayer.Entities.Address.Province province)
        {
            try
            {
                context.Update(province);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }




    }
}
