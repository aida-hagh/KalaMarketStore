using KalaMarketStore.Core.ViewModel;
using KalaMarketStore.DataLayer.Context;
using KalaMarketStore.DataLayer.Entities.Products.M_to_M;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace KalaMarketStore.Core.Service.Property
{

    public class PropertyService : IPropertyService
    {
        private readonly AppDbContext context;
        public PropertyService(AppDbContext context)
        {
            this.context = context;
        }



        public int AddProperty(DataLayer.Entities.Products.Property property)
        {
            try
            {
                context.Properties.Add(property);
                context.SaveChanges();
                return property.PropertyId;
            }
            catch (Exception)
            {

                return 0;
            }
        }



        public List<DataLayer.Entities.Products.Property> ShowAllProperty(int page)
        {
            int skip = (page - 1) * 4;
            return context.Properties.Skip(skip).Take(4).Where(x=>!x.IsDelete).ToList();
        } 
        
        public List<DataLayer.Entities.Products.Property> ShowAllProperty()
        {
           
            return context.Properties.Where(x => !x.IsDelete).ToList();
        }
        

       
        public int CountInPage()
        {
            int count = context.Properties.Count();
            if (count % 4 != 0)
            {
                count++;
            }
            count = count / 4;
            return count;
        }

        public bool ExistProperty(string propertyTitle, int propertyId)
        {
            return context.Properties.Any(x => x.PropertyTitle == propertyTitle && x.PropertyId != propertyId && !x.IsDelete);
        }



        public bool AddPropertyForCategory(List<Category_Property> category_Properties)
        {
            try
            {
                context.Category_Properties.AddRange(category_Properties);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }


        public List<UpdatePropertyViewModel> ShowPropertyForUpdate(int propertyid)
        {
           var result=(from property in context.Properties
                      join
                      propertyCategory in context.Category_Properties
                      on
                      property.PropertyId equals propertyCategory.PropertyId
                      where(propertyCategory.PropertyId== propertyid)
                      select new UpdatePropertyViewModel
                      {
                          PropertyId = property.PropertyId,
                          PropertyTitle = property.PropertyTitle,
                          CategoryId = propertyCategory.CategoryId,
                          Category_PropertyId = propertyCategory.Category_PropertyId,
                      }).ToList();
            return result;
        }


        public bool UpdateProperty(DataLayer.Entities.Products.Property property)
        {
            try
            {
                context.Properties.Update(property);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeletePropertyForCategory(int propid) 
        {
            try
            {
                List<Category_Property> category_Properties=context.Category_Properties.Where(x=>x.PropertyId==propid).ToList();
                context.Category_Properties.RemoveRange(category_Properties);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }


        public DataLayer.Entities.Products.Property FindPropertyById(int propertyId)
        {
            return context.Properties.Find(propertyId);
        }


        public bool DeleteProperty(DataLayer.Entities.Products.Property property)
        {
            try
            {
                property.IsDelete = true;
                context.Properties.Update(property);
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
