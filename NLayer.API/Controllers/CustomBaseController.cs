using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;

namespace NLayer.API.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response) //Bu metod sayesinde OK(), BadRequest(), NoContent() yazmaktan kurtuluruz...
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
