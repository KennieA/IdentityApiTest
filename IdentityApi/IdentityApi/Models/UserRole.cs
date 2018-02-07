using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApi.Models
{
    public class UserRole
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        
    }
}
