using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.UserRole
{
    public interface IUserRoleService
    {
        List<DataLayer.Entities.Roles.UserRole> ShowUserRole();
        int AddUserRole(DataLayer.Entities.Roles.UserRole userRole);
        bool UpdateUserRole(DataLayer.Entities.Roles.UserRole userRole);
        bool DeleteUserRole(DataLayer.Entities.Roles.UserRole userRole);
        DataLayer.Entities.Roles.UserRole FindUserRoleById(int id);
        bool ExistUserRole(int userid, int roleid, int userRoleid);
    }
}
