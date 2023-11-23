using KalaMarketStore.Core.ViewModel;
using KalaMarketStore.DataLayer.Context;
using KalaMarketStore.DataLayer.Entities.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.User
{
    public class UserService : IUserService
    {

        private readonly AppDbContext context;
        public UserService(AppDbContext context)
        {
            this.context = context;
        }
        public int AddUser(DataLayer.Entities.User user)
        {
            try
            {

                context.Users.Add(user);
                context.SaveChanges();
                return user.UserId;


            }
            catch (Exception)
            {

                return 0;
            }
        }


        public bool DeleteUser(DataLayer.Entities.User user)
        {
            try
            {
                user.IsDelete = true;
                context.Users.Update(user);
                context.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }

        }



        public bool ExistEmaile(string email, int id)
        {
            return context.Users.Any(x => x.Email == email && x.UserId != id);
        }



        public bool UpddateUser(DataLayer.Entities.User user)
        {
            try
            {

                context.Users.Update(user);
                context.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }


        public DataLayer.Entities.User FindUser(int userid, string code)
        {
            return context.Users.Where(x => x.UserId == userid && x.ActiveCode == code).FirstOrDefault();
        } 
        
        public DataLayer.Entities.User FindUserById(int userid)
        {
            return context.Users.Where(x => x.UserId == userid ).FirstOrDefault();
        } 
        public List<DataLayer.Entities.User> FindUserByIdToList(int userid)
        {
            return context.Users.Where(x => x.UserId == userid ).ToList();
        }

        public DataLayer.Entities.User FindUserByEmail(string email)
        {
            return context.Users.Where(u => u.Email == email).FirstOrDefault();

        }

        public DataLayer.Entities.User LoginUser(string email, string pass)
        {
            var user = context.Users.Where(u => u.Email == email && u.Password == pass).SingleOrDefault();
            return user;
        }




        public DataLayer.Entities.User GetUserProfile(DataLayer.Entities.User user) 
        {
            var re = context.Users.Where(x => x.UserId == user.UserId).FirstOrDefault();
            return re;

        }

        public List<DataLayer.Entities.User> ShowAllUser()
        {
            return context.Users.ToList();
        }

    }
}
