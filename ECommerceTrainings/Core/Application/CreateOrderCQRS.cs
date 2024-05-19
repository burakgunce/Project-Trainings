using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Core.Application
{
    internal class CreateOrderCQRS
    {
        /*
         * // Sipariş oluşturma isteği için bir istek sınıfı tanımlar. IRequest arayüzünü uygular ve yanıt türü olarak CreateOrderCommandResponse belirtir.
public class CreateOrderCommandRequest : IRequest<CreateOrderCommandResponse>
{
    // Sipariş açıklaması.
    public string Description { get; set; }
    // Siparişin adresi.
    public string Address { get; set; }
}

// Sipariş oluşturma isteğine verilen yanıt için boş bir yanıt sınıfı.
public class CreateOrderCommandResponse
{
}

// Sipariş oluşturma işlemini ele alan sınıf. IRequestHandler arayüzünü uygular ve isteğin türü ile yanıtın türünü belirtir.
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
{
    // Bağımlılıkları tanımlar.
    readonly IOrderService _orderService;
    readonly IBasketService _basketService;
    readonly IOrderHubService _orderHubService;

    // Constructor, bağımlılıkları injekte eder.
    public CreateOrderCommandHandler(IOrderService orderService, IBasketService basketService, IOrderHubService orderHubService)
    {
        _orderService = orderService;
        _basketService = basketService;
        _orderHubService = orderHubService;
    }

    // Sipariş oluşturma isteğini ele alan yöntem. İsteği ve iptal tokenını parametre olarak alır.
    public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
    {
        // Sipariş oluşturma işlemini başlatır. Sipariş servisini kullanarak yeni bir sipariş oluşturur.
        await _orderService.CreateOrderAsync(new()
        {
            // İstekten gelen adres ve açıklama bilgilerini atar.
            Address = request.Address,
            Description = request.Description,
            // Kullanıcının aktif sepetinin ID'sini alır ve dizeye dönüştürür.
            BasketId = _basketService.GetUserActiveBasket?.Id.ToString()
        });

        // Siparişin eklendiğini haber vermek için sipariş hub servisini kullanır.
        await _orderHubService.OrderAddedMessageAsync("New order has came !");
        
        // Boş bir yanıt döner.
        return new();
    }
}

         * 
         * 
         * 
         */
    }
}
