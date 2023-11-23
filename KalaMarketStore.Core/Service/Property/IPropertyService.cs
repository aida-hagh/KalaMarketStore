using KalaMarketStore.Core.ViewModel;
using KalaMarketStore.DataLayer.Entities.Products;
using KalaMarketStore.DataLayer.Entities.Products.M_to_M;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.Property
{
    public interface IPropertyService
    {

        List<DataLayer.Entities.Products.Property> ShowAllProperty(int page);
        List<DataLayer.Entities.Products.Property> ShowAllProperty();

        int AddProperty(DataLayer.Entities.Products.Property property);
       

        bool UpdateProperty(DataLayer.Entities.Products.Property property);

       

        bool ExistProperty(string propertyTitle, int propertyId);

        int CountInPage();

        bool AddPropertyForCategory(List<Category_Property> category_Properties);

        List<UpdatePropertyViewModel> ShowPropertyForUpdate(int propertyid);
        bool DeletePropertyForCategory(int propid);

        DataLayer.Entities.Products.Property FindPropertyById(int propertyId);


        bool DeleteProperty(DataLayer.Entities.Products.Property property);
    }
}
