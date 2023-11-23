using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace KalaMarketStore.Core.Service.MainSlider
{
    public interface IMainSliderService
    {
        Task<IEnumerable<DataLayer.Entities.MainSlider>> ShowAllSlider(int page);

       Task <DataLayer.Entities.MainSlider> FindSliderById(int sliderId);


        Task<int> AddSlider(DataLayer.Entities.MainSlider mainSlider);

        Task<bool> UpdateSlider(DataLayer.Entities.MainSlider mainSlider);

        Task<bool> DeleteSlider(DataLayer.Entities.MainSlider mainSlider);


        int SliderCount();


        List<DataLayer.Entities.MainSlider> ShowSliderForUsers();
    }
}
