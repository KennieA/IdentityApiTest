using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IdentityApi.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;

namespace IdentityApi.DAL.DB
{
    public class UserRepository : IUserRepository
    {
        IServiceProvider _serviceProvider;
        

        public UserRepository(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }
        public bool DeleteUser(int id)
        {
            using (var context = new IdentityDbContext(_serviceProvider.GetRequiredService<DbContextOptions<IdentityDbContext>>()))
            {
                try {
                    var user = context.User.Where(u => u.Id == id).FirstOrDefault();
                    user.IsDeleted = true;
                    context.User.Update(user);
                    context.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public DTOUser GetUserById(int id)
        {
            using (var context = new IdentityDbContext(_serviceProvider.GetRequiredService<DbContextOptions<IdentityDbContext>>()))
            {
                var newUser = context.User.Where(u => u.Id == id).FirstOrDefault();
                var userRoles = context.UserRole.Include(u => u.Role).Where(u => u.UserId == id);

                DTOUser dtoUser = DTOConverter(newUser);

                dtoUser.Roles = userRoles.Select(c => c.Role).ToList();
                return dtoUser;
            }
            
        }

        public List<DTOUser> GetAllUsers()
        {
            using (var context = new IdentityDbContext(_serviceProvider.GetRequiredService<DbContextOptions<IdentityDbContext>>()))
            {
                var newUser = context.User;
                var userRoles = context.UserRole.Include(u => u.Role);

                List<DTOUser> dtoUsers = new List<DTOUser>();
                foreach (var user in newUser)
                {
                    dtoUsers.Add(DTOConverter(user));
                }

                foreach(var users in dtoUsers)
                {
                    users.Roles = userRoles.Where(c => c.UserId == users.Id).Select(c => c.Role).ToList();
                }

                return dtoUsers;
            }

        }

        public User GetUserByUsername(User user)
        {
            using (var context = new IdentityDbContext(_serviceProvider.GetRequiredService<DbContextOptions<IdentityDbContext>>()))
            {
                var newUser = context.User.Where(u => String.Equals(u.Email, user.Username)).FirstOrDefault();
                return newUser;
            }
        }

        public DTOUser UpdateUser(DTOUser dtoUser)
        {
            
            using (var context = new IdentityDbContext(_serviceProvider.GetRequiredService<DbContextOptions<IdentityDbContext>>()))
            {
                List<Role> roles = new List<Role>(dtoUser.Roles);
                User user = UserConverter(dtoUser);
                DTOUser newDto = new DTOUser();
                context.User.Update(user);
                context.SaveChanges();

                var newUser = context.User.Where(u => u.Id == user.Id).FirstOrDefault();
                if(newUser.Id == dtoUser.Id)
                {
                    newDto = DTOConverter(newUser);
                    newDto.Roles = roles;
                }
                return newDto;
            }

        }

        public bool ValidatePassword(User user, string plainTextPassword)
        {
            using (var context = new IdentityDbContext(_serviceProvider.GetRequiredService<DbContextOptions<IdentityDbContext>>()))
            {
                var newUser = context.User.Where(u => String.Equals(u.Email, user.Username)).FirstOrDefault();
                if (user == null) return false;
                if (String.Equals(plainTextPassword, user.Password)) return true;
                return false;
            }
        }

        public DTOUser DTOConverter(User user)
        {
            DTOUser DtoUser = new DTOUser
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                ProfileUri = user.ProfileUri,
                IsDeleted = user.IsDeleted,
                Roles = new List<Role>()
                
            };
            return DtoUser;
        }

        public User UserConverter(DTOUser dtoUser)
        {
            User user = new User
            {
                Id = dtoUser.Id,
                Firstname = dtoUser.Firstname,
                Lastname = dtoUser.Lastname,
                Username = dtoUser.Username,
                Password = dtoUser.Password,
                Email = dtoUser.Email,
                ProfileUri = dtoUser.ProfileUri,
                IsDeleted = dtoUser.IsDeleted

            };
            return user;
        }
    }
}
