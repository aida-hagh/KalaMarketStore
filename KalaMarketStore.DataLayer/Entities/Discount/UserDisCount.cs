using KalaMarketStore.DataLayer.Entities.OfferCodes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.DataLayer.Entities.Discount
{
    public class UserDisCount
    {
        [Key]
        public int UserDisCountId { get; set; }
        public int DisCountId { get; set; }
        public int UserId { get; set; }




        #region Relation

        [ForeignKey(nameof( UserId))]  
        public User? User { get; set; }


        [ForeignKey(nameof(DisCountId))]
        public DisCount? DisCount { get; set; }


        #endregion
    }
}
