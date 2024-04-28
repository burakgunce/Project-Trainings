using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTrainings.Services.BasketService
{
    public class OrderCreatedIntegrationEventHandler : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
    {
        private readonly IBasketRepository _repository; // Sepet işlemlerini yapan bir repo örneği
        private readonly ILogger<OrderCreatedIntegrationEvent> _logger; // Entegrasyon olaylarını günlüğe kaydeden bir logger

        // Yapılandırıcı, bağımlılıkları alır.
        public OrderCreatedIntegrationEventHandler(IBasketRepository repository, ILogger<OrderCreatedIntegrationEvent> logger)
        {
            _repository = repository; // Sepet repository'sini ayarlar
            _logger = logger; // Logger örneğini ayarlar
        }

        // Olayı işleyen yöntem
        public async Task Handle(OrderCreatedIntegrationEvent @event)
        {
            // Günlüğe olayın işlendiğine dair bilgi kaydeder
            _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at BasketService.Api - ({@IntegrationEvent})", @event.Id, @event);

            // Kullanıcıya ait sepeti silen bir asenkron yöntem çağrılır.
            await _repository.DeleteBasketAsync(@event.UserId);
        }
    }

}
