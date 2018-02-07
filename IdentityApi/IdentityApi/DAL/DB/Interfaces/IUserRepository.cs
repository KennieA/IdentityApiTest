using IdentityApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApi.DAL.DB
{
    public interface IUserRepository
    {
        DTOUser GetUserById(int id);
        List<DTOUser> GetAllUsers();
        User GetUserByUsername(User user);
        bool ValidatePassword(User user, string plainTextPassword);
        DTOUser UpdateUser(DTOUser user);
        bool DeleteUser(int id);
        DTOUser DTOConverter(User user);


    }
}
