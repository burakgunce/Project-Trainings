using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Infrastructure.Infrastruct
{
    internal class ValidationFilter
    {
        /* default modelstate yerine fluent validation artık kontrolleri yapıyor
         * 
         * 
         * public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
{
    // Model durumu geçerli değilse işleme devam edilmez ve hata sonucu döndürülür.
    if (!context.ModelState.IsValid)
    {
        // Model durumu geçersiz olan hataların bir listesi oluşturulur.
        var errors = context.ModelState
               .Where(x => x.Value.Errors.Any())
               .ToDictionary(e => e.Key, e => e.Value.Errors.Select(e => e.ErrorMessage))
               .ToArray();

        // Hata sonucu BadRequestObjectResult tipinde bir sonuçla değiştirilir ve işleme devam edilmez.
        context.Result = new BadRequestObjectResult(errors);
        return;
    }

    // Eğer model durumu geçerli ise, işlem devam eder ve bir sonraki aksiyon çağrılır.
    await next();
}


        Bu metot, bir HTTP isteğinin, isteği karşılayan bir aksiyon metodu içerisindeki modelin doğruluğunu kontrol eder. Eğer model durumu geçerli değilse, modeldeki hatalar bir liste olarak toplanır ve BadRequestObjectResult tipinde bir sonuçla isteğe cevap olarak döndürülür. Eğer model durumu geçerli ise, isteğin işlenmesi devam eder ve bir sonraki aksiyon metodu çağrılır. Bu tür bir doğrulama genellikle, gelen verilerin uygunluğunu kontrol etmek için kullanılır ve istemcilerin hatalı veri göndermelerini önler.
         * 
         * 
         */
    }
}
