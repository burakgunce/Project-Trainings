using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Core.Application
{
    internal class AddItemToBasketCQRS
    {
        /*
         * // Bir ürünü sepete eklemek için kullanılan komut isteği sınıfı.
// IRequest arayüzünü uygular ve yanıt türü olarak AddItemToBasketCommandResponse belirtir.
public class AddItemToBasketCommandRequest : IRequest<AddItemToBasketCommandResponse>
{
    // Sepete eklenecek ürünün ID'si.
    public string ProductId { get; set; }
    // Eklenecek ürün miktarı.
    public int Quantity { get; set; }
}

// Sepete ürün ekleme isteğine verilen yanıt için boş bir yanıt sınıfı.
public class AddItemToBasketCommandResponse
{
}

// Sepete ürün ekleme işlemini ele alan sınıf.
// IRequestHandler arayüzünü uygular ve isteğin türü ile yanıtın türünü belirtir.
public class AddItemToBasketCommandHandler : IRequestHandler<AddItemToBasketCommandRequest, AddItemToBasketCommandResponse>
{
    // Bağımlılığı tanımlar.
    readonly IBasketService _basketService;

    // Constructor, bağımlılığı injekte eder.
    public AddItemToBasketCommandHandler(IBasketService basketService)
    {
        _basketService = basketService;
    }

    // Sepete ürün ekleme isteğini ele alan yöntem. İsteği ve iptal tokenını parametre olarak alır.
    public async Task<AddItemToBasketCommandResponse> Handle(AddItemToBasketCommandRequest request, CancellationToken cancellationToken)
    {
        // BasketService'in AddItemToBasketAsync yöntemini çağırarak sepete ürün ekler.
        await _basketService.AddItemToBasketAsync(new()
        {
            // İstekten gelen ürün ID'sini ve miktarını kullanır.
            ProductId = request.ProductId,
            Quantity = request.Quantity
        });

        // Boş bir yanıt döner.
        return new();
    }
}

         * 
         * 
         */
    }
}
