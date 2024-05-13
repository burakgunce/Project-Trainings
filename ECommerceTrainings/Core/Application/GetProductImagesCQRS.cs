using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Core.Application
{
    internal class GetProductImagesCQRS
    {
        /*
         * public class GetProductImagesQueryRequest : IRequest<List<GetProductImagesQueryResponse>>
{
    // Ürün ID'si
    public string Id { get; set; }
}

public class GetProductImagesQueryResponse
{
    // Resmin yol bilgisi
    public string Path { get; set; }
    // Resmin dosya adı
    public string FileName { get; set; }
    // Resmin ID'si
    public Guid Id { get; set; }
}

public class GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQueryRequest, List<GetProductImagesQueryResponse>>
{
    readonly IProductReadRepository _productReadRepository; // Ürün okuma deposu
    readonly IConfiguration configuration; // Uygulama yapılandırması

    // Constructor: Ürün okuma deposu ve yapılandırma enjekte edilir
    public GetProductImagesQueryHandler(IProductReadRepository productReadRepository, IConfiguration configuration)
    {
        _productReadRepository = productReadRepository;
        this.configuration = configuration;
    }

    // İsteği işler
    public async Task<List<GetProductImagesQueryResponse>> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
    {
        // Ürünü bulur
        Domain.Entities.Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
               .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
        
        // Eğer ürün bulunduysa, ürün resimlerini dönüştürür
        return product?.ProductImageFiles.Select(p => new GetProductImagesQueryResponse
        {
            // Resmin yolunu oluşturur
            Path = $"{configuration["BaseStorageUrl"]}/{p.Path}",
            // Resmin dosya adını belirler
            FileName = p.FileName,
            // Resmin ID'sini belirler
            Id = p.Id
        }).ToList();
    }
}

         * 
         * 
         */
    }
}
