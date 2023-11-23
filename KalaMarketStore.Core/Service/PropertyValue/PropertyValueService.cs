using KalaMarketStore.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.PropertyValue
{
    public class PropertyValueService : IPropertyValueService
    {
        private readonly AppDbContext context;
        public PropertyValueService(AppDbContext context) 
        {
            this.context = context; 
        }

        public int AddPropertyValue(DataLayer.Entities.Products.PropertyValue PropertyValue)
        {
            try
            {
                context.PropertyValues.Add(PropertyValue);
                context.SaveChanges();
                return PropertyValue.PropertyValueId;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public bool DeletePropertyValue(DataLayer.Entities.Products.PropertyValue PropertyValue)
        {
            try
            {
                context.PropertyValues.Remove(PropertyValue);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        //public bool ExistPropertyValue(string PropertyValueTitle, int PropertyValueId)
        //{
        //   return context.PropertyValues.Any()
        //}

        public DataLayer.Entities.Products.PropertyValue FindPropertyValueById(int PropertyValueId)
        {
           return context.PropertyValues.AsNoTracking().FirstOrDefault(x=>x.PropertyValueId== PropertyValueId);
        }

        public List<DataLayer.Entities.Products.PropertyValue> ShowAllPropertyValue()
        {
           
          
              return context.PropertyValues.ToList();
        
        }

        public bool UpdatePropertyValue(DataLayer.Entities.Products.PropertyValue PropertyValue)
        {
            try
            {
                context.PropertyValues.Update(PropertyValue);
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
