using KalaMarketStore.Core.ViewModel;
using KalaMarketStore.DataLayer.Entities.Products.M_to_M;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.PropertyValue
{
    public interface IPropertyValueService
    {


        List<DataLayer.Entities.Products.PropertyValue> ShowAllPropertyValue();

        int AddPropertyValue(DataLayer.Entities.Products.PropertyValue PropertyValue);


        bool UpdatePropertyValue(DataLayer.Entities.Products.PropertyValue PropertyValue);



        //bool ExistPropertyValue(string PropertyValueTitle, int PropertyValueId);

    

        DataLayer.Entities.Products.PropertyValue FindPropertyValueById(int PropertyValueId);


        bool DeletePropertyValue(DataLayer.Entities.Products.PropertyValue PropertyValue);



    }
}
