using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Core.Application
{
    internal class GetAllProductsCQRS
    {
        /*
         * 
         * public class GetAllProductQueryRequest : IRequest<GetAllProductQueryResponse>
{
    // Sayfalama bilgisi
    public int Page { get; set; } = 0; // Sayfa numarası
    public int Size { get; set; } = 5; // Sayfa boyutu
}

public class GetAllProductQueryResponse
{
    // Toplam ürün sayısı
    public int TotalProductCount { get; set; }
    // Ürünler
    public object Products { get; set; }
}

public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
{
    readonly IProductReadRepository _productReadRepository; // Ürün okuma deposu
    readonly ILogger<GetAllProductQueryHandler> _logger; // Günlüğe yazma

    // Constructor: Ürün okuma deposu ve günlüğe yazma enjekte edilir
    public GetAllProductQueryHandler(IProductReadRepository productReadRepository, ILogger<GetAllProductQueryHandler> logger)
    {
        _productReadRepository = productReadRepository;
        _logger = logger;
    }

    // İsteği işler
    public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("get all products"); // Tüm ürünleri alma işlemi hakkında bilgi günlüğe yazılır

        // Tüm ürünlerin sayısını alır
        var totalProductCount = _productReadRepository.GetAll().Count();
        // Sayfalama yaparak belirli bir aralıktaki ürünleri alır
        var products = _productReadRepository.GetAll(false).Skip(request.Page * request.Size).Take(request.Size)
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate
            }).ToList();

        // Ürünlerin ve toplam ürün sayısının olduğu yanıtı oluşturur ve döndürür
        return new()
        {
            Products = products,
            TotalProductCount = totalProductCount
        };
    }
}

         * 
         * 
         */
    }
}
