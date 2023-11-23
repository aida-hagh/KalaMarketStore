using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.Permission
{
    public interface IPermissionService
    {
        IEnumerable<DataLayer.Entities.Roles.Permission> ShowAllPermission();

        int AddPermission(DataLayer.Entities.Roles.Permission Permission);

        bool UpdatePermission(DataLayer.Entities.Roles.Permission Permission);

        bool DeletePermission(DataLayer.Entities.Roles.Permission Permission);

        bool ExistPermission(string Name, int PermissionId);

        DataLayer.Entities.Roles.Permission FindPermissionById(int PermissionId);
    }
}
