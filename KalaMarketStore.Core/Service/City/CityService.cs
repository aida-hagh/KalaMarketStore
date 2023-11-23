using KalaMarketStore.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.City
{
    public class CityService : ICityService
    {

        private readonly AppDbContext context;
        public CityService(AppDbContext context)
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


        public bool DeleteCity(DataLayer.Entities.Address.City City)
        {
            try
            {
                City.IsDelete = true;
                context.Cities.Update(City);
                context.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }


        public bool ExistCity(string FaName,
            int CityId)
        {
            return context.Cities.Any(x => x.CityName == FaName && x.CityId != CityId && !x.IsDelete);
        }


        public DataLayer.Entities.Address.City FindCityById(int CityId)
        {
            return context.Cities.Find(CityId);
        }


        public List<DataLayer.Entities.Address.City> ShowAllCity(int page)
        {
            int skip = (page - 1) * 10;
            return context.Cities.Where(x => !x.IsDelete).Skip(skip).Take(10).ToList();
        }
        public IEnumerable<DataLayer.Entities.Address.City> ShowAllCity()
        {

            return context.Cities.Where(x => !x.IsDelete).ToList();
        }


        public bool UpdateCity(DataLayer.Entities.Address.City City)
        {
            try
            {
                context.Cities.Update(City);
                context.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }


        public int CountInPage()
        {
            int count = context.Cities.Count();
            if (count % 10 != 0)
            {
                count++;
            }
            count = count / 10;
            return count;
        }

    }
}
