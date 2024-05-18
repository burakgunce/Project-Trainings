using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Core.Application.Services
{
    internal class BasketService
    {
        /*
         * // BasketService sınıfı, IBasketService arayüzünü uygular ve sepet yönetimi sağlar.
public class BasketService : IBasketService
{
    // Bağımlılıkları (dependencies) tanımlar.
    readonly IHttpContextAccessor _httpContextAccessor;
    readonly UserManager<AppUser> _userManager;
    readonly IOrderReadRepository _orderReadRepository;
    readonly IBasketWriteRepository _basketWriteRepository;
    readonly IBasketReadRepository _basketReadRepository;
    readonly IBasketItemWriteRepository _basketItemWriteRepository;
    readonly IBasketItemReadRepository _basketItemReadRepository;

    // Constructor, bağımlılıkları injekte eder.
    public BasketService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IOrderReadRepository orderReadRepository, IBasketWriteRepository basketWriteRepository, IBasketReadRepository basketReadRepository, IBasketItemWriteRepository basketItemWriteRepository, IBasketItemReadRepository basketItemReadRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _orderReadRepository = orderReadRepository;
        _basketWriteRepository = basketWriteRepository;
        _basketReadRepository = basketReadRepository;
        _basketItemWriteRepository = basketItemWriteRepository;
        _basketItemReadRepository = basketItemReadRepository;
    }

    // Kullanıcının aktif sepetini getiren özel bir yöntem.
    private async Task<Basket?> ContextUser()
    {
        // HTTP bağlamından kullanıcının adını alır.
        var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
        
        // Kullanıcı adı boş değilse.
        if (!string.IsNullOrEmpty(username))
        {
            // Kullanıcıyı ve sepetlerini veritabanından getirir.
            AppUser? user = await _userManager.Users
                     .Include(u => u.Baskets)
                     .FirstOrDefaultAsync(u => u.UserName == username);

            // Kullanıcının sepetlerini ve ilgili siparişleri sorgular.
            var _basket = from basket in user.Baskets
                          join order in _orderReadRepository.Table
                          on basket.Id equals order.Id into BasketOrders
                          from order in BasketOrders.DefaultIfEmpty()
                          select new
                          {
                              Basket = basket,
                              Order = order
                          };

            Basket? targetBasket = null;
            // Siparişi olmayan sepet var mı kontrol eder, varsa bu sepeti seçer.
            if (_basket.Any(b => b.Order is null))
                targetBasket = _basket.FirstOrDefault(b => b.Order is null)?.Basket;
            else
            {
                // Yoksa yeni bir sepet oluşturur ve kullanıcıya ekler.
                targetBasket = new();
                user.Baskets.Add(targetBasket);
            }

            // Değişiklikleri kaydeder.
            await _basketWriteRepository.SaveAsync();
            return targetBasket;
        }
        // Kullanıcı adı yoksa hata fırlatır.
        throw new Exception("Something went wrong !!!");
    }

    // Sepete ürün eklemek için kullanılan yöntem.
    public async Task AddItemToBasketAsync(VM_Create_BasketItem basketItem)
    {
        // Kullanıcının sepetini getirir.
        Basket? basket = await ContextUser();
        if (basket != null)
        {
            // Sepette bu üründen daha önce var mı kontrol eder.
            BasketItem _basketItem = await _basketItemReadRepository.GetSingleAsync(bi => bi.BasketId == basket.Id && bi.ProductId == Guid.Parse(basketItem.ProductId));
            if (_basketItem != null)
                // Varsa miktarını artırır.
                _basketItem.Quantity++;
            else
                // Yoksa yeni bir sepet öğesi oluşturur.
                await _basketItemWriteRepository.AddAsync(new()
                {
                    BasketId = basket.Id,
                    ProductId = Guid.Parse(basketItem.ProductId),
                    Quantity = basketItem.Quantity
                });

            // Değişiklikleri kaydeder.
            await _basketItemWriteRepository.SaveAsync();
        }
    }

    // Sepetteki ürünleri getirmek için kullanılan yöntem.
    public async Task<List<BasketItem>> GetBasketItemsAsync()
    {
        // Kullanıcının sepetini getirir.
        Basket? basket = await ContextUser();
        // Sepeti ve içindeki ürünleri veritabanından getirir.
        Basket? result = await _basketReadRepository.Table
             .Include(b => b.BasketItems)
             .ThenInclude(bi => bi.Product)
             .FirstOrDefaultAsync(b => b.Id == basket.Id);

        // Sepet öğelerini liste olarak döner.
        return result.BasketItems
            .ToList();
    }

    // Sepetten ürün çıkarmak için kullanılan yöntem.
    public async Task RemoveBasketItemAsync(string basketItemId)
    {
        // Sepet öğesini ID ile bulur.
        BasketItem? basketItem = await _basketItemReadRepository.GetByIdAsync(basketItemId);
        if (basketItem != null)
        {
            // Sepet öğesini siler.
            _basketItemWriteRepository.Remove(basketItem);
            // Değişiklikleri kaydeder.
            await _basketItemWriteRepository.SaveAsync();
        }
    }

    // Sepet öğesinin miktarını güncellemek için kullanılan yöntem.
    public async Task UpdateQuantityAsync(VM_Update_BasketItem basketItem)
    {
        // Sepet öğesini ID ile bulur.
        BasketItem? _basketItem = await _basketItemReadRepository.GetByIdAsync(basketItem.BasketItemId);
        if (_basketItem != null)
        {
            // Miktarını günceller.
            _basketItem.Quantity = basketItem.Quantity;
            // Değişiklikleri kaydeder.
            await _basketItemWriteRepository.SaveAsync();
        }
    }

    // Kullanıcının aktif sepetini getiren özellik.
    public Basket? GetUserActiveBasket
    {
        get
        {
            // Kullanıcının aktif sepetini döner.
            Basket? basket = ContextUser().Result;
            return basket;
        }
    }
}

         * 
         * 
         */
    }
}
