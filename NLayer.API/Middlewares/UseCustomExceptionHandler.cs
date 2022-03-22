using Microsoft.AspNetCore.Diagnostics;
using NLayer.Core.DTOs;
using NLayer.Service.Exceptions;
using System.Text.Json;

namespace NLayer.API.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async httpContext => //Run() metodu sonlandirici Middleware'dir. Request buraya girdiginde buradan geri doner, controller'a gitmez...
                {
                    httpContext.Response.ContentType = "application/json";
                    var exceptionFeature = httpContext.Features.Get<IExceptionHandlerFeature>(); //IExceptionHandlerFeature: Hatalari verecek interface

                    var statusCode = exceptionFeature.Error switch //statusCode degerini atadik(ClientSideException ise 400, default 500)...
                    {
                        ClientSideException => 400,
                        NotFoundException => 404,
                        _ => 500
                    };
                    httpContext.Response.StatusCode = statusCode;
                    var response = CustomResponseDto<NoContentDto>.Fail(statusCode, exceptionFeature.Error.Message);

                    await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
                });
            });
        }
    }
}
