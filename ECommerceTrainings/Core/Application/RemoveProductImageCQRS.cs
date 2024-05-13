using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Core.Application
{
    internal class RemoveProductImageCQRS
    {
        /*
         * public class RemoveProductImageCommandRequest : IRequest<RemoveProductImageCommandResponse>
{
    // Ürün ID'si
    public string Id { get; set; }
    // Resim ID'si (isteğe bağlı)
    public string? ImageId { get; set; }
}

public class RemoveProductImageCommandResponse
{
}

public class RemoveProductImageCommandHandler : IRequestHandler<RemoveProductImageCommandRequest, RemoveProductImageCommandResponse>
{
    readonly IProductReadRepository _productReadRepository; // Ürün okuma deposu
    readonly IProductWriteRepository _productWriteRepository; // Ürün yazma deposu

    // Constructor: Ürün okuma ve yazma depoları enjekte edilir
    public RemoveProductImageCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
    }

    // İsteği işler
    public async Task<RemoveProductImageCommandResponse> Handle(RemoveProductImageCommandRequest request, CancellationToken cancellationToken)
    {
        // Ürünü bulur
        Domain.Entities.Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));

        // Ürün resmini bulur
        Domain.Entities.ProductImageFile? productImageFile = product?.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(request.ImageId));

        // Eğer resim bulunduysa, üründen kaldırır
        if (productImageFile != null)
            product?.ProductImageFiles.Remove(productImageFile);

        // Değişiklikleri kaydeder
        await _productWriteRepository.SaveAsync();

        // Boş bir yanıt döndürür
        return new();
    }
}

         * 
         */
    }
}
