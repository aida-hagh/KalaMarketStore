using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.DataLayer.Entities.Roles
{
    public class UserRole
    {
        [Key]
        public int UserRoleId { get; set; }

        public int UserId { get; set; }
        public int RoleId { get; set; }






        #region Relation

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; } 
        

        [ForeignKey(nameof(RoleId))]
        public Role? Role { get; set; }

        #endregion
    }
}
