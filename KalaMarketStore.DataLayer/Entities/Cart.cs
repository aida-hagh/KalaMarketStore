using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.DataLayer.Entities
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public int UserId { get; set; }
        public bool IsPay { get; set; }
        public string PaymentCode { get; set; }
        public DateTime CreateData { get; set; }
        public int TotalPrice { get; set; }



        #region Relation

        [ForeignKey(nameof(UserId))]
        public User? User  { get; set; }

        public List<CartDeatail>? CartDeatails { get; set; }

        #endregion
    }
}
