using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "Admin";
        public bool Confirmation { get; set; } = false;
    }
}
