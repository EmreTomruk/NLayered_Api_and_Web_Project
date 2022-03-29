using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        private readonly DbSet<User> _dbSet;

        public UserRepository(AppDbContext context, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _mapper = mapper;
            _dbSet = _context.Set<User>();
        }

        public User Authenticate(string userName, string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserName == userName && x.Password == password);

            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(5),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = String.Empty;
            user.FullName = "Admin_" + userName;

            return user;
        }

        public async Task<bool> IsUniqueUser(string userName)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == userName);

            if (user == null)
                return true;

            return false;
        }

        public async Task<User> Register(string userName, string password)
        {
            UserDto userDto = new UserDto()
            {
                UserName = userName,
                Password = password,
                //Role = "Admin",
                //Confirmation = false
            };

            var user = _mapper.Map<User>(userDto);
            await _dbSet.AddAsync(user); /*_context.Users.AddAsync(user); 2 kullanim sekli de olabilr...*/
            await _context.SaveChangesAsync();
            //user.Password = String.Empty;

            return user;
        }       
    }
}
