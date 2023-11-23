using KalaMarketStore.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.Permission
{
    public class PermissionService : IPermissionService
    {

        private readonly AppDbContext context;
        public PermissionService(AppDbContext context)
        {
            this.context = context;
        }

        public int AddPermission(DataLayer.Entities.Roles.Permission Permission)
        {
            try
            {
                context.Permissions.Add(Permission);
                context.SaveChanges();
                return Permission.PermissionId;
            }
            catch (Exception)
            {

                return 0;
            }
        }


        public bool DeletePermission(DataLayer.Entities.Roles.Permission Permission)
        {
            try
            {

                context.Permissions.Remove(Permission);
                context.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }


        public bool ExistPermission(string Name, int PermissionId)

        {
            return context.Permissions.Any(x => x.PermissionTitle == Name && x.PermissionId != PermissionId);
        }


        public DataLayer.Entities.Roles.Permission FindPermissionById(int PermissionId)
        {
            return context.Permissions.Find(PermissionId);
        }


    
        public IEnumerable<DataLayer.Entities.Roles.Permission> ShowAllPermission()
        {

            return context.Permissions.ToList();
        }


        public bool UpdatePermission(DataLayer.Entities.Roles.Permission Permission)
        {
            try
            {
                context.Permissions.Update(Permission);
                context.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

    }
}
