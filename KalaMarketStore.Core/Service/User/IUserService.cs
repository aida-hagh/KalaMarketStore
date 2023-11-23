using KalaMarketStore.DataLayer.Entities.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.Service.User;

public interface IUserService
{

    int AddUser(DataLayer.Entities.User user);

    bool UpddateUser(DataLayer.Entities.User user);
    bool DeleteUser(DataLayer.Entities.User user);

    bool ExistEmaile(string email, int id);

    DataLayer.Entities.User FindUser(int userid, string code);
    DataLayer.Entities.User FindUserById(int userid);
    DataLayer.Entities.User FindUserByEmail(string email);

    DataLayer.Entities.User LoginUser(string email, string pass);
    DataLayer.Entities.User GetUserProfile(DataLayer.Entities.User user);
    List<DataLayer.Entities.User> ShowAllUser();

    List<DataLayer.Entities.User> FindUserByIdToList(int userid);

}