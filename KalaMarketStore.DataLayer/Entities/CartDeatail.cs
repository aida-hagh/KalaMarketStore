using KalaMarketStore.DataLayer.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.DataLayer.Entities
{
    public class CartDeatail
    {
        [Key]
        public int CartDeatailId { get; set; }
        public int ProductPriceId { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public DateTime CreateDate { get; set; }
        public int CartId { get; set; }




        #region Relation

        [ForeignKey(nameof(CartId))]
        public Cart? Cart { get; set; }


        #endregion
    }
}
