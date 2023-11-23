using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.DataLayer.Entities.Products
{
    public class ProductGallery
    {
        [Key]
        public int ProductGalleryId { get; set; }

        [Display(Name = "تصویر محصول")]
        public string? ImgName { get; set; }

        public int ProductId { get; set; }


        #region Relation

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

        #endregion
    }
}
