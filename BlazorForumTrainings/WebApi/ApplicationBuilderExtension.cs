using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForumTrainings.WebApi
{
    //genel bir hata yönetim servisi bunu normalde kendin library olarak ta yazıp kullanabilirsin
    public static class ApplicationBuilderExtension
    {
        // ConfigureExceptionHandling metodu, istisna işleme için middleware ekler
        // includeExceptionDetails parametresiyle istisna ayrıntılarının dahil edilip edilmeyeceği belirlenir
        // useDefaultHandlingResponse parametresiyle varsayılan işleme yanıtının kullanılıp kullanılmayacağı belirlenir
        // handleException parametresiyle özel bir istisna işleyicisi belirlenebilir
        public static IApplicationBuilder ConfigureExceptionHandling(this IApplicationBuilder app,
            bool includeExceptionDetails = false,
        bool useDefaultHandlingResponse = true,
            Func<HttpContext, Exception, Task> handleException = null)
        {
            app.UseExceptionHandler(options =>
            {
                options.Run(context =>
                {
                    // İstisna nesnesi alınır
                    var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();

                    // useDefaultHandlingResponse false ve handleException null ise istisna işleyicisi belirtilmemiş demektir
                    if (!useDefaultHandlingResponse && handleException == null)
                        throw new ArgumentNullException(nameof(handleException),
                            $"{nameof(handleException)} cannot be null when {nameof(useDefaultHandlingResponse)} is false");

                    // useDefaultHandlingResponse false ve handleException belirtilmişse özel işleyici kullanılır
                    if (!useDefaultHandlingResponse && handleException != null)
                        return handleException(context, exceptionObject.Error);

                    // Varsayılan işleme yanıtı kullanılır
                    return DefaultHandleException(context, exceptionObject.Error, includeExceptionDetails);
                });
            });

            return app;
        }

        // Varsayılan istisna işleyicisi
        private static async Task DefaultHandleException(HttpContext context, Exception exception, bool includeExceptionDetails)
        {
            // Varsayılan durum kodu ve mesajı
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string message = "Internal server error occurred!";

            // Eğer istisna yetkilendirme hatası ise durum kodu Unauthorized (401) yapılır
            if (exception is UnauthorizedAccessException)
                statusCode = HttpStatusCode.Unauthorized;

            // Eğer istisna veritabanı doğrulama hatası ise durum kodu BadRequest (400) yapılır
            if (exception is DatabaseValidationException)
            {
                statusCode = HttpStatusCode.BadRequest;
                var validationResponse = new ValidationResponseModel(exception.Message);
                await WriteResponse(context, statusCode, validationResponse);
                return;
            }

            // Yanıt nesnesi oluşturulur
            var res = new
            {
                HttpStatusCode = (int)statusCode,
                Detail = includeExceptionDetails ? exception.ToString() : message
            };

            // Yanıt yazılır
            await WriteResponse(context, statusCode, res);
        }

        // Yanıt yazma işlemi
        private static async Task WriteResponse(HttpContext context, HttpStatusCode statusCode, object responseObj)
        {
            // Yanıt türü belirlenir
            context.Response.ContentType = "application/json";
            // Durum kodu belirlenir
            context.Response.StatusCode = (int)statusCode;
            // Yanıt JSON olarak yazılır
            await context.Response.WriteAsJsonAsync(responseObj);
        }
    }

}
