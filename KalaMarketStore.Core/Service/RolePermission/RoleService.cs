using KalaMarketStore.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.RolePermission
{
    public class RoleService : IRoleService
    {
        private readonly AppDbContext context;
        public RoleService(AppDbContext context)
        {
            this.context = context;
        }

        public bool CheckPermission(int userid, int permissionid)
        {
            var roleid = context.UserRoles.Where(x => x.UserId == userid).Select(x => x.RoleId).ToList();

            if (!roleid.Any())
                return false;

            List<int> rolepermission = context.RolePermissions
                .Where(x => x.PermissionId == permissionid).Select(x => x.RoleId).ToList();

            return rolepermission.Any(x => roleid.Contains(x));



        }

        #region Role
        public int AddRole(DataLayer.Entities.Roles.Role Role)
        {
            try
            {
                context.Roles.Add(Role);
                context.SaveChanges();
                return Role.RoleId;
            }
            catch (Exception)
            {

                return 0;
            }
        }


        public bool DeleteRole(DataLayer.Entities.Roles.Role Role)
        {
            try
            {

                context.Roles.Remove(Role);
                context.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }


        public bool ExistRole(string Name, int RoleId)

        {
            return context.Roles.Any(x => x.RoleTitle == Name && x.RoleId != RoleId);
        }


        public DataLayer.Entities.Roles.Role FindRoleById(int RoleId)
        {
            return context.Roles.Find(RoleId);
        }



        public IEnumerable<DataLayer.Entities.Roles.Role> ShowAllRole()
        {

            return context.Roles.ToList();
        }


        public bool UpdateRole(DataLayer.Entities.Roles.Role Role)
        {
            try
            {
                context.Roles.Update(Role);
                context.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        #endregion




        #region RolePermission

        public List<DataLayer.Entities.Roles.RolePermission> ShowRolePermission()
        {
            return context.RolePermissions.Include(x => x.Role).Include(x => x.Permission).ToList();
        }

        public int AddRolePermission(DataLayer.Entities.Roles.RolePermission rolePermission)
        {
            try
            {
                context.RolePermissions.Add(rolePermission);
                context.SaveChanges();
                return rolePermission.RolePermissionId;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public bool UpdateRolePermission(DataLayer.Entities.Roles.RolePermission rolePermission)
        {
            try
            {
                context.RolePermissions.Update(rolePermission);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool DeleteRolePermission(DataLayer.Entities.Roles.RolePermission rolePermission)
        {
            try
            {
                context.RolePermissions.Remove(rolePermission);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public DataLayer.Entities.Roles.RolePermission FindRolePermissionById(int id)
        {
            return context.RolePermissions.Include(x => x.Role).Include(x => x.Permission)
                  .FirstOrDefault(x => x.RolePermissionId == id);
        }


        public bool ExistRolePermission(int permissionid, int roleid, int rolePermissionid)

        {
            return context.RolePermissions.Any(x => x.PermissionId == permissionid && x.RoleId == roleid && x.RolePermissionId != rolePermissionid);
        }
        #endregion
    }
}
