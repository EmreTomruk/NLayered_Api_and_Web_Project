using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        protected readonly IMapper _mapper;

        public CustomBaseController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [NonAction]
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response) //Bu metod sayesinde OK(), BadRequest(), NoContent() gibi metodlari yazmaya gerek kalmiyor...
        {
            if (response.StatusCode == 204)
                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode
                };

            return new ObjectResult(response) //StatusCode ne ise geriye onu doner...
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
