using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.RolePermission
{
    public interface IRoleService
    {

        bool CheckPermission(int userid, int permissionid);


        #region Role
        IEnumerable<DataLayer.Entities.Roles.Role> ShowAllRole();

        int AddRole(DataLayer.Entities.Roles.Role Role);

        bool UpdateRole(DataLayer.Entities.Roles.Role Role);

        bool DeleteRole(DataLayer.Entities.Roles.Role Role);

        bool ExistRole(string Name, int RoleId);

        DataLayer.Entities.Roles.Role FindRoleById(int RoleId);

        #endregion



        #region RolePermission

        List<DataLayer.Entities.Roles.RolePermission> ShowRolePermission();
        int AddRolePermission(DataLayer.Entities.Roles.RolePermission rolePermission);

        bool UpdateRolePermission(DataLayer.Entities.Roles.RolePermission rolePermission);
        bool DeleteRolePermission(DataLayer.Entities.Roles.RolePermission rolePermission);
        DataLayer.Entities.Roles.RolePermission FindRolePermissionById(int id);
        bool ExistRolePermission(int permissionid, int roleid, int rolePermissionid);

        #endregion
    }
}
