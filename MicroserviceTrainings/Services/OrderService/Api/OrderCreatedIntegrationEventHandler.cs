using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTrainings.Services.OrderService.Api
{
    public class OrderCreatedIntegrationEventHandler : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
    {
        private readonly IMediator mediator; // Mediator aracılığıyla komut gönderme yeteneği sağlayan bir referans
        private readonly ILogger<OrderCreatedIntegrationEventHandler> logger; // Entegrasyon olaylarını günlüğe kaydetme yeteneği sağlayan bir logger

        // Yapılandırıcı, bağımlılıkları alır.
        public OrderCreatedIntegrationEventHandler(IMediator mediator, ILogger<OrderCreatedIntegrationEventHandler> logger)
        {
            this.mediator = mediator; // Mediator örneğini ayarlar
            this.logger = logger; // Logger örneğini ayarlar
        }

        // IIntegrationEventHandler arayüzünden uygulanan Handle yöntemi, entegrasyon olayını işler.
        public async Task Handle(OrderCreatedIntegrationEvent @event)
        {
            try
            {
                // Entegrasyon olayının işlendiğine dair bir bilgi kaydeder
                logger.LogInformation("Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})",
                    @event.Id,
                    typeof(Startup).Namespace,
                    @event);

                // Yeni bir sipariş oluşturmak için bir komut oluşturur.
                var createOrderCommand = new CreateOrderCommand(@event.Basket.Items,
                                @event.UserId, @event.UserName,
                                @event.City, @event.Street,
                                @event.State, @event.Country, @event.ZipCode,
                                @event.CardNumber, @event.CardHolderName, @event.CardExpiration,
                                @event.CardSecurityNumber, @event.CardTypeId);

                // Oluşturulan komutu mediator aracılığıyla gönderir.
                await mediator.Send(createOrderCommand);
            }
            catch (Exception ex)
            {
                // Hata durumunda hata bilgisini günlüğe kaydeder.
                logger.LogError(ex, ex.ToString());
            }
        }
    }

}
