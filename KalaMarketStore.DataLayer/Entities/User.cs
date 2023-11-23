using KalaMarketStore.DataLayer.Entities.Comments_Q_A;
using KalaMarketStore.DataLayer.Entities.Discount;
using KalaMarketStore.DataLayer.Entities.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.DataLayer.Entities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int UserId { get; set; }
        public string UserAccount { get; set; }
        public string? UserName { get; set; }
        public string? UserFamily { get; set; }
        public string? Mobile { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }   

        public DateTime CreateAccount { get; set; }

        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string ActiveCode { get; set; }


        #region Relation

        public List<Question>? Questions { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<Cart>? Carts { get; set; }
        public List<UserDisCount>? UserDisCounts { get; set; }
        List<UserRole>? UserRole { get; set; }


        #endregion

    }
}
