using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Core.ViewModels;

namespace NLayer.Web
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {
        private readonly IService<T> _service;

        public NotFoundFilter(IService<T> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault(); //Burada action'dan donen Id degerini aliriz...

            if (idValue == null)
            {
                await next.Invoke(); 
                return; 
            }
            var id = (int)idValue;
            var anyEntity = await _service.AnyAsync(x => x.Id == id);

            if (anyEntity)
            {
                await next.Invoke();
                return;
            }
            var errorViewModel = new ErrorViewModel();
            errorViewModel.Errors.Add($"{typeof(T).Name}({id}) not found...");

            context.Result = new RedirectToActionResult("Error", "Home", errorViewModel);
            //context.Result = new NotFoundObjectResult(CustomResponseDto<T>.Fail(404, $"{typeof(T).Name}:{id} not found"));
        }
    }
}
