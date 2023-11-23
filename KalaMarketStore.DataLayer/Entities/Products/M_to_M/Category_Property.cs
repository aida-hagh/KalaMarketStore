using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.DataLayer.Entities.Products.M_to_M
{
    public class Category_Property
    {
        [Key]
        public int Category_PropertyId { get; set; }
        public int CategoryId { get; set; }

        public int PropertyId { get; set; }


        #region Relation

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [ForeignKey("PropertyId")]
        public Property Property { get; set; }

        #endregion
    }
}
