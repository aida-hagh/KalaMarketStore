using KalaMarketStore.DataLayer.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.DataLayer.Entities.Comments_Q_A
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }


        [Display(Name = "متن سوال")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MinLength(10, ErrorMessage = "{0} نمیتواند کمتر از {1} باشد.")]
        [MaxLength(1000, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد.")]
        public string QuestionDescription { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsActive { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }



        #region Relation

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

        public List<Answer>? Answers { get; set; }

        #endregion

    }
}
