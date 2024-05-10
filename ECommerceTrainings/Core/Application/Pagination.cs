using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Core.Application
{
    internal class Pagination
    {
        /*
         * 
         * public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
{
    // Tüm ürünlerin alınması işlemi başlatılıyor, bu bir sorgu işleyicisidir.
    _logger.LogInformation("get all products");

    // Toplam ürün sayısını almak için tüm ürünlerin sayısını sayar.
    var totalProductCount = _productReadRepository.GetAll().Count();

    // Ürünlerin sayfalı bir listesi alınır.
    var products = _productReadRepository.GetAll(false) // İzleme devre dışı bırakılmış ürünler alınıyor.
        .Skip(request.Page * request.Size) // Sayfa numarasına göre ürünlerin belirli bir kısmı atlanır.
        .Take(request.Size) // Sayfa boyutu kadar ürün alınır.
        .Select(p => new // Her üründen belirli alanlar seçilir.
        {
            p.Id,
            p.Name,
            p.Stock,
            p.Price,
            p.CreatedDate,
            p.UpdatedDate
        })
        .ToList(); // Seçilen ürünler bir liste olarak alınır.

    // Sorgu sonucunu temsil eden bir nesne oluşturulur ve geri döndürülür.
    return new GetAllProductQueryResponse()
    {
        Products = products, // Ürünler
        TotalProductCount = totalProductCount // Toplam ürün sayısı
    };
}

         * 
         */
    }
}
