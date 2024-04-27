using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTrainings.Services.NotificationService
{
    internal class OrderFailOrSuccessEventHandler
    {
        // payment service den dinlediği event e göre burda işlem yapıp burası da bir mesaj fırlatıyor
    }
    class OrderPaymentSuccessIntegrationEventHandler : IIntegrationEventHandler<OrderPaymentSuccessIntegrationEvent>
    {
        public Task Handle(OrderPaymentSuccessIntegrationEvent @event)
        {
            // Ödeme işleminin başarılı olduğunu işleyen bir yöntem.
            // Burada başarılı ödeme bildirimi gönderilir (SMS, E-posta, Push vb.).
            // Ayrıca, işlemle ilgili günlük kayıtları oluşturulur.
            // İşlem tamamlandığında bir tamamlama belirtisi döndürülür.
        }
    }

    class OrderPaymentFailedIntegrationEventHandler : IIntegrationEventHandler<OrderPaymentFailedIntegrationEvent>
    {
        public Task Handle(OrderPaymentFailedIntegrationEvent @event)
        {
            // Ödeme işleminin başarısız olduğunu işleyen bir yöntem.
            // Burada başarısız ödeme bildirimi gönderilir (SMS, E-posta, Push vb.).
            // Ayrıca, işlemle ilgili günlük kayıtları oluşturulur, hata mesajı da kaydedilir.
            // İşlem tamamlandığında bir tamamlama belirtisi döndürülür.
        }
    }

}
