using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Core.Application
{
    internal class GetAllOrdersCQRS
    {
        /*
         * // GetAllOrdersQueryRequest sınıfı, tüm siparişleri almak için kullanılan sorgu isteğini temsil eder.
// IRequest arayüzünü uygular ve GetAllOrdersQueryResponse tipinde bir yanıt döner.
public class GetAllOrdersQueryRequest : IRequest<GetAllOrdersQueryResponse>
{
    // Sayfa numarası (varsayılan değeri 0)
    public int Page { get; set; } = 0;
    
    // Sayfa başına sipariş sayısı (varsayılan değeri 5)
    public int Size { get; set; } = 5;
}

// GetAllOrdersQueryResponse sınıfı, tüm siparişleri getirme sorgusunun yanıtını temsil eder.
public class GetAllOrdersQueryResponse
{
    // Toplam sipariş sayısı
    public int TotalOrderCount { get; set; }
    
    // Siparişlerin listesi
    public object Orders { get; set; }
}

// GetAllOrdersQueryHandler sınıfı, GetAllOrdersQueryRequest sorgusunu işleyen sınıftır.
// IRequestHandler arayüzünü uygular ve GetAllOrdersQueryResponse tipinde bir yanıt döner.
public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQueryRequest, GetAllOrdersQueryResponse>
{
    // IOrderService bağımlılığı
    readonly IOrderService _orderService;

    // Constructor, IOrderService bağımlılığını enjekte eder.
    public GetAllOrdersQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    // Handle metodu, GetAllOrdersQueryRequest sorgusunu işler ve yanıtı döner.
    public async Task<GetAllOrdersQueryResponse> Handle(GetAllOrdersQueryRequest request, CancellationToken cancellationToken)
    {
        // IOrderService üzerinden tüm siparişleri belirtilen sayfa ve boyut ile alır.
        var data = await _orderService.GetAllOrdersAsync(request.Page, request.Size);

        // GetAllOrdersQueryResponse nesnesi oluşturur ve döner.
        return new()
        {
            TotalOrderCount = data.TotalOrderCount,
            Orders = data.Orders
        };
    }
}

         * 
         * 
         */
    }
}
