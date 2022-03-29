using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IUserService
    {
        Task<bool> IsUniqueUser(string userName);
        User Authenticate(string userName, string password);
        Task<User> Register(string userName, string password);
    }
}
