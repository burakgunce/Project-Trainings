using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Presentation.Api.Extensions
{
    internal class GlobalExceptionHandler
    {
        /*
         * static public class ConfigureExceptionHandlerExtension
{
    // Genişletme metodu (extension method) olarak tanımlanan ConfigureExceptionHandler metodu
    // Bu metot, WebApplication türündeki nesneler için kullanılabilir.
    // ILogger<T> türünde bir logger nesnesi alır.
    public static void ConfigureExceptionHandler<T>(this WebApplication application, ILogger<T> logger)
    {
        // UseExceptionHandler metodu, ASP.NET Core uygulamasında bir istisna oluştuğunda çalışacak bir middleware ekler.
        application.UseExceptionHandler(builder =>
        {
            // Run metodu, istisna yöneticisi işleyicisini tanımlar.
            builder.Run(async context =>
            {
                // HTTP yanıt kodunu içeren HTTP yanıt başlığını ayarlar.
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                // Yanıt içeriğinin türünü belirler.
                context.Response.ContentType = MediaTypeNames.Application.Json;

                // IExceptionHandlerFeature, bir HTTP isteği sırasında oluşan bir istisnayı temsil eder.
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    // Log kaydını oluşturur. (Hata mesajını loglar)
                    logger.LogError(contextFeature.Error.Message);

                    // Yanıt olarak hata detaylarını içeren JSON yanıtı gönderir.
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = contextFeature.Error.Message,
                        Title = "Something went wrong"
                    }));
                }
            });
        });
    }
}



        Lambda ifadesi (=>), bir anonim fonksiyonu temsil eder. Yukarıdaki kodda, lambda ifadesi builder.Run(...) satırında kullanılmıştır. Lambda ifadesi, bir parametre listesi ve bir işlev gövdesi içerir. Parametre listesi sağ tarafta, işlev gövdesi ise sol tarafta yer alır. İşlev gövdesi => işaretinden sonra gelir. Lambda ifadesi, bu örnekte bir HttpContext nesnesini temsil eden context parametresini alır ve bu parametre üzerinde belirli işlemleri gerçekleştirir. Bu kullanım, builder.Run(...) metodu içinde bir anonim işlev tanımlar ve bu işlev, bir HTTP isteği sırasında oluşan bir istisna durumunu ele alır.

         * 
         * 
         */
    }
}
