using KalaMarketStore.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.Province
{
    public class ProvinceService : IProvinceService
    {
        private readonly AppDbContext context;
        public ProvinceService(AppDbContext context)
        {
            this.context = context;
        }

        public int AddProvince(DataLayer.Entities.Address.Province Province)
        {
            try
            {
                context.Provinces.Add(Province);
                context.SaveChanges();
                return Province.ProvinceId;
            }
            catch (Exception)
            {

                return 0;
            }
        }


        public bool DeleteProvince(DataLayer.Entities.Address.Province Province)
        {
            try
            {
                Province.IsDelete = true;
                context.Provinces.Update(Province);
                context.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }


        public bool ExistProvince(string FaName, 
            int ProvinceId)
        {
            return context.Provinces.Any(x => x.ProvinceName == FaName  && x.ProvinceId != ProvinceId && !x.IsDelete);
        }


        public DataLayer.Entities.Address.Province FindProvinceById(int ProvinceId)
        {
            return context.Provinces.Find(ProvinceId);
        }


        public List<DataLayer.Entities.Address.Province> ShowAllProvince(int page)
        {
            int skip = (page - 1) * 10;
            return context.Provinces.Where(x => !x.IsDelete).Skip(skip).Take(10).ToList();
        }
        public IEnumerable<DataLayer.Entities.Address.Province> ShowAllProvince()
        {

            return context.Provinces.Where(x => !x.IsDelete).ToList();
        }


        public bool UpdateProvince(DataLayer.Entities.Address.Province Province)
        {
            try
            {
                context.Provinces.Update(Province);
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
            int count = context.Provinces.Count();
            if (count % 10 != 0)
            {
                count++;
            }
            count = count / 10;
            return count;
        }







    }
}
