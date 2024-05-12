using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Core.Application
{
    internal class CreateProductCQRS
    {
        /*
         * public class CreateProductCommandRequest : IRequest<CreateProductCommandResponse>
{
    // Ürün adı
    public string Name { get; set; }
    // Stok miktarı
    public int Stock { get; set; }
    // Fiyat
    public float Price { get; set; }
}

public class CreateProductCommandResponse
{
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
{
    IProductWriteRepository _productWriteRepository; // Ürün yazma deposu

    // Constructor: Ürün yazma deposu enjekte edilir
    public CreateProductCommandHandler(IProductWriteRepository productWriteRepository)
    {
        _productWriteRepository = productWriteRepository;
    }

    // İsteği işler
    public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        // Yeni ürünü ekler
        await _productWriteRepository.AddAsync(new()
        {
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock
        });

        // Değişiklikleri kaydeder
        await _productWriteRepository.SaveAsync();

        // Boş bir yanıt döndürür
        return new();
    }
}

         * 
         * 
         */
    }
}
