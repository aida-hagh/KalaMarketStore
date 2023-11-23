using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.DataLayer.Entities.Roles
{
    public class Permission
    {
        [Key]
        public int PermissionId { get; set; }
        public string PermissionTitle { get; set; }





        #region Relation
        List<RolePermission>? RolePermissions { get; set; }

        #endregion


    }
}
