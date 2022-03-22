﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;

namespace NLayer.API.Filters
{
    public class ValidateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid) //FluentValidation'a yazilan hatalar direkt ModelState'e mapplenir(FluentValidation kutuphanesi ModelState ile entegredir)..
            {
                var errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                context.Result = new BadRequestObjectResult(CustomResponseDto<NoContentDto>.Fail(400, errors)); //BadRequestObjectResult nesnesi ile response body'de hata mesajlari da gonderebiliriz...
            }
        }
    }
}
