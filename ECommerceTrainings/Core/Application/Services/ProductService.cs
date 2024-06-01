using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Core.Application.Services
{
    internal class ProductService
    {
        /*
         * public class ProductService : IProductService
{
    // Okuma ve yazma işlemleri için ürün depoları ve QR kod servisi bağımlılıkları.
    readonly IProductReadRepository _productReadRepository;
    readonly IProductWriteRepository _productWriteRepository;
    readonly IQRCodeService _qrCodeService;

    // Yapıcı metot, bağımlılıkları alır ve sınıf içinde kullanmak için atar.
    public ProductService(IProductReadRepository productReadRepository, IQRCodeService qrCodeService, IProductWriteRepository productWriteRepository)
    {
        _productReadRepository = productReadRepository;
        _qrCodeService = qrCodeService;
        _productWriteRepository = productWriteRepository;
    }

    // Belirtilen ürün ID'sine sahip ürün için QR kod oluşturur.
    public async Task<byte[]> QrCodeToProductAsync(string productId)
    {
        // Ürünü okuma deposundan ID'ye göre asenkron olarak getirir.
        Product product = await _productReadRepository.GetByIdAsync(productId);
        
        // Ürün bulunamazsa bir istisna fırlatır.
        if (product == null)
            throw new Exception("Product not found");

        // Ürün bilgilerini içeren düz bir nesne oluşturur.
        var plainObject = new
        {
            product.Id,
            product.Name,
            product.Price,
            product.Stock,
            product.CreatedDate
        };

        // Nesneyi JSON formatında serileştirir.
        string plainText = JsonSerializer.Serialize(plainObject);

        // Serileştirilen metni kullanarak QR kodu oluşturur ve bayt dizisi olarak döner.
        return _qrCodeService.GenerateQRCode(plainText);
    }

    // Belirtilen ürün ID'sine sahip ürünün stok miktarını günceller.
    public async Task StockUpdateToProductAsync(string productId, int stock)
    {
        // Ürünü okuma deposundan ID'ye göre asenkron olarak getirir.
        Product product = await _productReadRepository.GetByIdAsync(productId);

        // Ürün bulunamazsa bir istisna fırlatır.
        if (product == null)
            throw new Exception("Product not found");

        // Ürünün stok miktarını günceller.
        product.Stock = stock;

        // Değişiklikleri yazma deposuna kaydeder.
        await _productWriteRepository.SaveAsync();
    }
}

         * 
         * 
         * 
         */
    }
}
