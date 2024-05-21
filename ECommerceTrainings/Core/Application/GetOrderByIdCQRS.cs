using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Core.Application
{
    internal class GetOrderByIdCQRS
    {
        /*
         * // GetOrderByIdQueryRequest sınıfı, belirli bir siparişi almak için kullanılan sorgu isteğini temsil eder.
// IRequest arayüzünü uygular ve GetOrderByIdQueryResponse tipinde bir yanıt döner.
public class GetOrderByIdQueryRequest : IRequest<GetOrderByIdQueryResponse>
{
    // Alınacak siparişin ID'si
    public string Id { get; set; }
}

// GetOrderByIdQueryResponse sınıfı, belirli bir siparişi getirme sorgusunun yanıtını temsil eder.
public class GetOrderByIdQueryResponse
{
    // Siparişin adresi
    public string Address { get; set; }

    // Sepet öğeleri
    public object BasketItems { get; set; }

    // Siparişin oluşturulma tarihi
    public DateTime CreatedDate { get; set; }

    // Siparişin açıklaması
    public string Description { get; set; }

    // Siparişin ID'si
    public string Id { get; set; }

    // Siparişin kodu
    public string OrderCode { get; set; }

    // Siparişin tamamlanma durumu
    public bool Completed { get; set; }
}

// GetOrderByIdQueryHandler sınıfı, GetOrderByIdQueryRequest sorgusunu işleyen sınıftır.
// IRequestHandler arayüzünü uygular ve GetOrderByIdQueryResponse tipinde bir yanıt döner.
public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQueryRequest, GetOrderByIdQueryResponse>
{
    // IOrderService bağımlılığı
    readonly IOrderService _orderService;

    // Constructor, IOrderService bağımlılığını enjekte eder.
    public GetOrderByIdQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    // Handle metodu, GetOrderByIdQueryRequest sorgusunu işler ve yanıtı döner.
    public async Task<GetOrderByIdQueryResponse> Handle(GetOrderByIdQueryRequest request, CancellationToken cancellationToken)
    {
        // IOrderService üzerinden siparişi ID ile alır.
        var data = await _orderService.GetOrderByIdAsync(request.Id);
        
        // GetOrderByIdQueryResponse nesnesi oluşturur ve döner.
        return new()
        {
            Id = data.Id,
            OrderCode = data.OrderCode,
            Address = data.Address,
            BasketItems = data.BasketItems,
            CreatedDate = data.CreatedDate,
            Description = data.Description,
            Completed = data.Completed
        };
    }
}

         * 
         * 
         */
    }
}
