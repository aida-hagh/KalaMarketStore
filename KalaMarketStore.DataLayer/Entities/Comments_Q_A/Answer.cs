using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.DataLayer.Entities.Comments_Q_A
{
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }

        [Display(Name = "متن پاسخ")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MinLength(10, ErrorMessage = "{0} نمیتواند کمتر از {1} باشد.")]
        [MaxLength(1000, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد.")]
        public string AnswerDescription { get; set; }

        public DateTime CreateDate { get; set; }
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }


        #region Relation

        [ForeignKey("QuestionId")]
        public Question? Question { get; set; }  
        
        
        [ForeignKey("UserId")]
        public User? user { get; set; }
        #endregion
    }
}
