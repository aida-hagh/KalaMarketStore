using KalaMarketStore.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.MainSlider
{
    public class MainSliderService : IMainSliderService
    {
        private readonly AppDbContext _context;
        public MainSliderService(AppDbContext context)
        {
            _context = context;
        }



        public async Task<int> AddSlider(DataLayer.Entities.MainSlider mainSlider)
        {
            try
            {
                await _context.MainSliders.AddAsync(mainSlider);
                await _context.SaveChangesAsync();
                return mainSlider.SliderId;

            }

            catch (Exception ex)
            {

                return 0;
            }

        }


        public async Task<bool> DeleteSlider(DataLayer.Entities.MainSlider mainSlider)
        {
            try
            {
                _context.MainSliders.Remove(mainSlider);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }


        public async Task<DataLayer.Entities.MainSlider> FindSliderById(int sliderId)
        {
            return await _context.MainSliders.FindAsync(sliderId);
        }


        public async Task<IEnumerable<DataLayer.Entities.MainSlider>> ShowAllSlider(int page)
        {
            int skip = (page - 1) * 2;
            return await _context.MainSliders.OrderBy(x => x.SliderSort).Skip(skip).Take(2).ToListAsync();
        }


        public async Task<bool> UpdateSlider(DataLayer.Entities.MainSlider mainSlider)
        {
            try
            {
                _context.MainSliders.Update(mainSlider);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }



        public int SliderCount()
        {
            int sliderCounts = _context.MainSliders.Count();
            if (sliderCounts % 2 != 0)
            {
                sliderCounts++;
            }
            sliderCounts = sliderCounts / 2;
            return sliderCounts;
        }




        public List<DataLayer.Entities.MainSlider> ShowSliderForUsers()
        {
            return _context.MainSliders.Where(x=>x.IsActive).ToList();  
        }

    }
}
