using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using SharedLibrary.Dtos;
using SharedLibrary.Exceptions;

namespace SharedLibrary.Extensions
{
    public static class CustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    IExceptionHandlerFeature errorFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if(errorFeature != null)
                    {
                        Exception ex = errorFeature.Error;

                        ErrorDto errorDto = null;

                        if(ex is CustomException)
                        {
                            errorDto = new ErrorDto(ex.Message, true);
                        }
                        else
                        {
                            errorDto = new ErrorDto(ex.Message, false);
                        }

                        ResponseDto<NoDataDto> response = ResponseDto<NoDataDto>.Fail(errorDto, 500);

                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    }

                });
            });
        }
    }
}
