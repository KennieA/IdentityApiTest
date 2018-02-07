using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApi.Models
{
    public class DTOUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string ProfileUri { get; set; }
        public bool IsDeleted { get; set; }
        //public string Role { get; set; }
        public List<Role> Roles { get; set; }

        //public ICollection<UserRole> UserRoles { get; set; }
    }
}
