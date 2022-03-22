using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {
        private readonly IService<T> _service;

        public NotFoundFilter(IService<T> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) //Request herhangi bir filter'a takilmayacaksa next metodu ile request yoluna devam edecek...
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault(); //Burada action'dan donen Id degerini aliriz...

            if (idValue==null)
            {
                await next.Invoke(); //Yoluna devam et...
                return; //Geriye don, daha asagiya inmene gerek yok...
            }
            var id = (int)idValue;
            var anyEntity = await _service.AnyAsync(x => x.Id==id);

            if (anyEntity)
            {
                await next.Invoke();
                return;
            }
            context.Result = new NotFoundObjectResult(CustomResponseDto<T>.Fail(404, $"{typeof(T).Name}:{id} not found"));
        }
    }
}
