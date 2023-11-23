using KalaMarketStore.DataLayer.Context;
using KalaMarketStore.DataLayer.Entities.Roles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.UserRole
{
    public class UserRoleService : IUserRoleService
    {
        private readonly AppDbContext context;
        public UserRoleService(AppDbContext context)
        {
            this.context = context;
        }

        public List<DataLayer.Entities.Roles.UserRole> ShowUserRole()
        {
            return context.UserRoles.Include(x => x.Role).Include(x => x.User).ToList();
        }

        public int AddUserRole(DataLayer.Entities.Roles.UserRole userRole)
        {
            try
            {
                context.UserRoles.Add(userRole);
                context.SaveChanges();
                return userRole.UserRoleId;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public bool UpdateUserRole(DataLayer.Entities.Roles.UserRole userRole)
        {
            try
            {
                context.UserRoles.Update(userRole);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }     
        
        
        public bool DeleteUserRole(DataLayer.Entities.Roles.UserRole userRole)
        {
            try
            {
                context.UserRoles.Remove(userRole);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public DataLayer.Entities.Roles.UserRole FindUserRoleById(int id)
        {
          return context.UserRoles.Include(x => x.Role).Include(x => x.User)
                .FirstOrDefault(x => x.UserRoleId == id);
        }


        public bool ExistUserRole(int userid ,int roleid, int userRoleid)

        {
            return context.UserRoles.Any(x => x.UserId == userid&&x.RoleId==roleid && x.UserRoleId != userRoleid);
        }



    }
}
