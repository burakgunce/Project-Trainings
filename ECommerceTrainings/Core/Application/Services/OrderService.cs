using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Core.Application.Services
{
    internal class OrderService
    {
        /*
         * // OrderService sınıfı, IOrderService arayüzünü uygular ve sipariş işlemlerini gerçekleştirir.
public class OrderService : IOrderService
{
    // Repository bağımlılıklarını tanımlar. 
    readonly IOrderWriteRepository _orderWriteRepository;
    readonly IOrderReadRepository _orderReadRepository;
    readonly ICompletedOrderWriteRepository _completedOrderWriteRepository;
    readonly ICompletedOrderReadRepository _completedOrderReadRepository;

    // Constructor, bağımlılıkları injekte eder.
    public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository, ICompletedOrderWriteRepository completedOrderWriteRepository, ICompletedOrderReadRepository completedOrderReadRepository)
    {
        _orderWriteRepository = orderWriteRepository;
        _orderReadRepository = orderReadRepository;
        _completedOrderWriteRepository = completedOrderWriteRepository;
        _completedOrderReadRepository = completedOrderReadRepository;
    }

    // Yeni bir sipariş oluşturma işlemi.
    public async Task CreateOrderAsync(CreateOrder createOrder)
    {
        // Rastgele bir sipariş kodu oluşturur.
        var orderCode = (new Random().NextDouble() * 10000).ToString();
        orderCode = orderCode.Substring(orderCode.IndexOf(".") + 1, orderCode.Length - orderCode.IndexOf(".") - 1);
        
        // Yeni siparişi repository'e ekler.
        await _orderWriteRepository.AddAsync(new()
        {
            Address = createOrder.Address,
            Id = Guid.Parse(createOrder.BasketId),
            Description = createOrder.Description,
            OrderCode = orderCode
        });
        
        // Değişiklikleri kaydeder.
        await _orderWriteRepository.SaveAsync();
    }

    // Tüm siparişleri belirli bir sayfa ve boyutla getirir.
    public async Task<ListOrder> GetAllOrdersAsync(int page, int size)
    {
        // Siparişleri ve ilgili verilerini (Basket, User, BasketItems, Product) içeren sorgu.
        var query = _orderReadRepository.Table.Include(o => o.Basket)
                  .ThenInclude(b => b.User)
                  .Include(o => o.Basket)
                  .ThenInclude(b => b.BasketItems)
                  .ThenInclude(bi => bi.Product);

        // Sorguyu sayfa ve boyuta göre sınırlar.
        var data = query.Skip(page * size).Take(size);

        // Siparişlerin tamamlanma durumunu da içeren bir sorgu.
        var data2 = from order in data
                    join completedOrder in _completedOrderReadRepository.Table
                       on order.Id equals completedOrder.OrderId into co
                    from _co in co.DefaultIfEmpty()
                    select new
                    {
                        Id = order.Id,
                        CreatedDate = order.CreatedDate,
                        OrderCode = order.OrderCode,
                        Basket = order.Basket,
                        Completed = _co != null ? true : false
                    };

        // Sipariş listesini oluşturur ve döner.
        return new()
        {
            TotalOrderCount = await query.CountAsync(),
            Orders = await data2.Select(o => new
            {
                Id = o.Id,
                CreatedDate = o.CreatedDate,
                OrderCode = o.OrderCode,
                TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                UserName = o.Basket.User.UserName,
                o.Completed
            }).ToListAsync()
        };
    }

    // Belirli bir ID'ye sahip siparişi getirir.
    public async Task<SingleOrder> GetOrderByIdAsync(string id)
    {
        // Siparişi ve ilgili verilerini (Basket, BasketItems, Product) içeren sorgu.
        var data = _orderReadRepository.Table
                             .Include(o => o.Basket)
                                 .ThenInclude(b => b.BasketItems)
                                     .ThenInclude(bi => bi.Product);

        // Belirli bir ID'ye sahip siparişi ve tamamlanma durumunu içeren sorgu.
        var data2 = await (from order in data
                           join completedOrder in _completedOrderReadRepository.Table
                               on order.Id equals completedOrder.OrderId into co
                           from _co in co.DefaultIfEmpty()
                           select new
                           {
                               Id = order.Id,
                               CreatedDate = order.CreatedDate,
                               OrderCode = order.OrderCode,
                               Basket = order.Basket,
                               Completed = _co != null ? true : false,
                               Address = order.Address,
                               Description = order.Description
                           }).FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));

        // SingleOrder nesnesini oluşturur ve döner.
        return new()
        {
            Id = data2.Id.ToString(),
            BasketItems = data2.Basket.BasketItems.Select(bi => new
            {
                bi.Product.Name,
                bi.Product.Price,
                bi.Quantity
            }),
            Address = data2.Address,
            CreatedDate = data2.CreatedDate,
            Description = data2.Description,
            OrderCode = data2.OrderCode,
            Completed = data2.Completed
        };
    }

    // Siparişi tamamlar ve tamamlanmış sipariş bilgilerini döner.
    public async Task<(bool, CompletedOrderDTO)> CompleteOrderAsync(string id)
    {
        // Belirtilen ID'ye sahip siparişi ve ilgili kullanıcı verilerini alır.
        Order? order = await _orderReadRepository.Table
            .Include(o => o.Basket)
            .ThenInclude(b => b.User)
            .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));

        if (order != null)
        {
            // Siparişi tamamlanmış olarak işaretler.
            await _completedOrderWriteRepository.AddAsync(new() { OrderId = Guid.Parse(id) });
            bool isSaved = await _completedOrderWriteRepository.SaveAsync() > 0;

            // Tamamlanmış sipariş bilgilerini döner.
            return (isSaved, new()
            {
                OrderCode = order.OrderCode,
                OrderDate = order.CreatedDate,
                Username = order.Basket.User.UserName,
                Email = order.Basket.User.Email
            });
        }

        // Sipariş bulunamazsa, başarısız durumu döner.
        return (false, null);
    }
}

         * 
         * 
         * 
         */
    }
}
