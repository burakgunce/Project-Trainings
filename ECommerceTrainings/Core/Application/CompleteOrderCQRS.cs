using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Core.Application
{
    internal class CompleteOrderCQRS
    {
        /*
         * // Bu sınıf, bir siparişi tamamlama isteğini işlemekle sorumludur.
// IRequestHandler arayüzünü uygular ve CompleteOrderCommandRequest ve CompleteOrderCommandResponse türleri ile çalışır.
public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommandRequest, CompleteOrderCommandResponse>
{
    // Sipariş hizmeti bağımlılığı.
    IOrderService _orderService;

    // Mail hizmeti bağımlılığı.
    IMailService _mailService;

    // Constructor, bağımlılıkları alır.
    public CompleteOrderCommandHandler(IOrderService orderService, IMailService mailService)
    {
        _orderService = orderService;
        _mailService = mailService;
    }

    // İstek işleme metodu. CompleteOrderCommandRequest alır ve CompleteOrderCommandResponse döner.
    public async Task<CompleteOrderCommandResponse> Handle(CompleteOrderCommandRequest request, CancellationToken cancellationToken)
    {
        // Siparişi tamamlama işlemi çağrılır ve sonucu ve sipariş bilgilerini içeren DTO döner.
        (bool succeeded, CompletedOrderDTO dto) = await _orderService.CompleteOrderAsync(request.Id);

        // Eğer işlem başarılı ise, tamamlama e-postası gönderilir.
        if (succeeded)
            await _mailService.SendCompletedOrderMailAsync(dto.Email, dto.OrderCode, dto.OrderDate, dto.Username);

        // Yeni bir yanıt nesnesi döner.
        return new();
    }
}

        // Bu metod, verilen sipariş kimliğine göre siparişi tamamlar.
public async Task<(bool, CompletedOrderDTO)> CompleteOrderAsync(string id)
{
    // Verilen sipariş kimliğine göre sipariş detaylarını ve ilişkili sepet ve kullanıcı bilgilerini içeren sorgu yapılır.
    Order? order = await _orderReadRepository.Table
        .Include(o => o.Basket) // Siparişle ilişkili sepet bilgilerini dahil eder.
        .ThenInclude(b => b.User) // Sepetle ilişkili kullanıcı bilgilerini dahil eder.
        .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id)); // Verilen kimlik ile eşleşen ilk siparişi alır.

    // Eğer sipariş bulunursa:
    if (order != null)
    {
        // Tamamlanmış sipariş kaydını ekler.
        await _completedOrderWriteRepository.AddAsync(new() { OrderId = Guid.Parse(id) });

        // Değişiklikleri kaydeder ve eğer başarılı olursa DTO döner.
        return (await _completedOrderWriteRepository.SaveAsync() > 0, new()
        {
            // Sipariş detaylarını DTO'ya aktarır.
            OrderCode = order.OrderCode,
            OrderDate = order.CreatedDate,
            Username = order.Basket.User.UserName,
            Email = order.Basket.User.Email
        });
    }

    // Eğer sipariş bulunamazsa, işlem başarısız olur ve null döner.
    return (false, null);
}


         * 
         * 
         * 
         * 
         */
    }
}
