using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityApi.DAL.DB;
using IdentityApi.Models;

namespace IdentityApi.Controllers
{
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        IServiceProvider _serviceProvider;
        IUserRepository userRepository;
        public AdminController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            userRepository = new UserRepository(_serviceProvider);
        }

        // GET api/Admin
        [HttpGet]
        public List<DTOUser> GetAll()
        {
            return userRepository.GetAllUsers();
        }

        // Delete api/Admin
        [HttpDelete("{id}")]
        public bool DeleteUser([FromRoute] int id)
        {
            return userRepository.DeleteUser(id);
        }

        // PUT api/Admin/5
        [HttpPut("{id}")]
        public DTOUser Put(int id, DTOUser dTOUser)
        {
            return userRepository.UpdateUser(dTOUser);
        }
    }
}