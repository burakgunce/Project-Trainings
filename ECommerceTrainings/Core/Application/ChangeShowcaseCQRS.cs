using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Core.Application
{
    internal class ChangeShowcaseCQRS
    {
        /*
         * // Bu sınıf, bir komut isteğini temsil eder. IRequest arayüzünü uygular ve
// ChangeShowcaseImageCommandResponse döndürür.
public class ChangeShowcaseImageCommandRequest : IRequest<ChangeShowcaseImageCommandResponse>
{
    // Vitrin görselinin ID'sini temsil eden özellik.
    public string ImageId { get; set; }
    
    // Ürünün ID'sini temsil eden özellik.
    public string ProductId { get; set; }
}

// Bu sınıf, komut isteğine verilecek cevabı temsil eder. Şu anda boş.
public class ChangeShowcaseImageCommandResponse
{
}

// Bu sınıf, komut işleyicisini temsil eder. IRequestHandler arayüzünü uygular
// ve ChangeShowcaseImageCommandRequest ve ChangeShowcaseImageCommandResponse türlerini kullanır.
public class ChangeShowcaseImageCommandHandler : IRequestHandler<ChangeShowcaseImageCommandRequest, ChangeShowcaseImageCommandResponse>
{
    // Ürün görsel dosyalarını yazmak için kullanılan repository.
    readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

    // İşleyici oluşturucu, repository'yi bağımlılık olarak alır.
    public ChangeShowcaseImageCommandHandler(IProductImageFileWriteRepository productImageFileWriteRepository)
    {
        _productImageFileWriteRepository = productImageFileWriteRepository;
    }

    // İstek işleyici metodu. İstek ve iptal belirteci alır ve yanıt döndürür.
    public async Task<ChangeShowcaseImageCommandResponse> Handle(ChangeShowcaseImageCommandRequest request, CancellationToken cancellationToken)
    {
        // Veritabanı sorgusu oluşturulur. Ürünleri içeren ve vitrin görsellerini seçen sorgu.
        var query = _productImageFileWriteRepository.Table
                  .Include(p => p.Products)  // Ürünleri içeren ilişkili veriler dahil edilir.
                  .SelectMany(p => p.Products, (pif, p) => new
                  {
                      pif,  // Ürün görsel dosyası
                      p     // Ürün
                  });

        // İstek ile eşleşen ve vitrin görseli olan ilk kaydı getirir.
        var data = await query.FirstOrDefaultAsync(p => p.p.Id == Guid.Parse(request.ProductId) && p.pif.Showcase);

        // Eğer böyle bir kayıt varsa, vitrin özelliğini false yapar.
        if (data != null)
            data.pif.Showcase = false;

        // Yeni vitrin görseli olarak işaretlenecek görseli getirir.
        var image = await query.FirstOrDefaultAsync(p => p.pif.Id == Guid.Parse(request.ImageId));
        
        // Eğer böyle bir görsel varsa, vitrin özelliğini true yapar.
        if (image != null)
            image.pif.Showcase = true;

        // Değişiklikleri veritabanına kaydeder.
        await _productImageFileWriteRepository.SaveAsync();

        // Boş bir yanıt döndürür.
        return new();
    }
}

         * 
         * 
         * 
         */
    }
}
