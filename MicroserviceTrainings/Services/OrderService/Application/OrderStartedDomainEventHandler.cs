using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTrainings.Services.OrderService.Application
{
    class OrderStartedDomainEventHandler : INotificationHandler<OrderStartedDomainEvent>
    {
        private readonly IBuyerRepository buyerRepository; // Alıcı repository'sine erişimi sağlayan bir referans

        // Yapılandırıcı, bağımlılıkları alır.
        public OrderStartedDomainEventHandler(IBuyerRepository buyerRepository)
        {
            this.buyerRepository = buyerRepository; // Alıcı repository'sini ayarlar
        }

        // INotificationHandler arayüzünden uygulanan Handle yöntemi, domain olayını işler.
        public async Task Handle(OrderStartedDomainEvent orderStartedEvent, CancellationToken cancellationToken)
        {
            // Kart tipi ID'si varsa, 0 olmayan kart tipi ID'sini kullanır, aksi halde varsayılan bir değer kullanır.
            var cardTypeId = (orderStartedEvent.CardTypeId != 0) ? orderStartedEvent.CardTypeId : 1;

            // Kullanıcı adına göre alıcıyı alır, eğer bulunamazsa null döner.
            var buyer = await buyerRepository.GetSingleAsync(i => i.Name == orderStartedEvent.UserName, i => i.PaymentMethods);

            // Alıcının önceden var olup olmadığını kontrol eder.
            bool buyerOriginallyExisted = buyer != null;

            // Eğer alıcı önceden var olmuyorsa, yeni bir alıcı oluşturur.
            if (!buyerOriginallyExisted)
            {
                buyer = new Buyer(orderStartedEvent.UserName);
            }

            // Alıcının ödeme yöntemini doğrular veya ekler.
            buyer.VerifyOrAddPaymentMethod(cardTypeId,
                                           $"Payment Method on {DateTime.UtcNow}",
                                           orderStartedEvent.CardNumber,
                                           orderStartedEvent.CardSecurityNumber,
                                           orderStartedEvent.CardHolderName,
                                           orderStartedEvent.CardExpiration,
                                           orderStartedEvent.Order.Id);

            // Alıcı önceden varsa güncellenir, aksi halde eklenir.
            var buyerUpdated = buyerOriginallyExisted ?
                buyerRepository.Update(buyer) :
                await buyerRepository.AddAsync(buyer);

            // Değişiklikler veritabanına kaydedilir.
            await buyerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            // Sipariş durumu değişikliği olayı burada tetiklenebilir.
        }
    }

}
