using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.DataLayer.Entities.Roles
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleTitle { get; set; }






        #region Relation
        List<RolePermission>? RolePermissions { get; set; }
        List<UserRole>? UserRole { get; set; }

        #endregion
    }
}
