using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Authenticate(UserDto userDto)
        {
            var user = _userService.Authenticate(userDto.UserName, userDto.Password);

            if (user == null)
                return BadRequest(new { message = "Kullanıcı adı veya parola hatalı" });

            return Ok(user);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            bool userBool = await _userService.IsUniqueUser(userDto.UserName);

            if (!userBool)
                return BadRequest(new { message = "Kullanıcı adı zaten mevcut..." });

            var user = await _userService.Register(userDto.UserName, userDto.Password);

            if (user == null)
                return BadRequest(new { message = "Kayıt esnasında hata oluştu..." });

            return Ok(user);
        }
    }
}
