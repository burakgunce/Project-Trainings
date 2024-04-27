using MicroserviceTrainings.Eventbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTrainings.Services.PaymentService
{
    internal class OrderStartedIntegrationEventHandler
    {
        // bunun tetiklenme sebebi baseeventbus taki processevent metodunun en altındaki handle kısmı
    }
    public class OrderStartedIntegrationEventHandler : IIntegrationEventHandler<OrderStartedIntegrationEvent>
    {
        private readonly IConfiguration configuration; // Uygulama yapılandırmasını temsil eden bir özellik.
        private readonly IEventBus eventBus; // Olay otobüsünü temsil eden bir özellik.
        private readonly ILogger<OrderStartedIntegrationEventHandler> logger; // Günlük oluşturmayı sağlayan bir özellik.

        public OrderStartedIntegrationEventHandler(IConfiguration configuration, IEventBus eventBus, ILogger<OrderStartedIntegrationEventHandler> logger)
        {
            // Bağımlılıkları enjekte eden bir yapılandırıcı.
            // IConfiguration, IEventBus ve ILogger özelliklerini alır ve ilgili alanlara atar.
        }

        public Task Handle(OrderStartedIntegrationEvent @event)
        {
            // Olayı işleyen bir yöntem.
            // Bu yöntemde bir ödeme işlemi simüle edilir.
            // Ödeme başarılı mı başarısız mı olduğu, yapılandırmadan alınır.
            // Başarılı veya başarısız olduğuna bağlı olarak uygun bir entegrasyon olayı oluşturulur.
            // Olay günlüğe kaydedilir ve olay otobüsüne yayınlanır.
            // İşlem tamamlandığında bir tamamlama belirtisi döndürülür.
        }
    }

}
