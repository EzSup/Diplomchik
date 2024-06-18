using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Blazored.Toast.Services;

public static class ExceptionHandlerExtensions
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app, IToastService toastService)
    {
        if (app == null)
        {
            throw new ArgumentNullException(nameof(app));
        }

        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "text/plain";

                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                if (exceptionHandlerPathFeature?.Error != null)
                {
                    var errorMessage = exceptionHandlerPathFeature.Error.Message;
                                        
                    // Виклик ToastService для відображення повідомлення про помилку
                    toastService.ShowError(errorMessage);

                    await context.Response.WriteAsync(errorMessage);
                }
            });
        });
    }
}
