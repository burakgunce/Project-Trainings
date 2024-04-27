using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTrainings.Eventbus
{
    internal class EventBusRabbitMQ
    {
    }
    public class EventBusRabbitMQ : BaseEventBus
    {
        RabbitMQPersistentConnection persistentConnection; // RabbitMQ bağlantısını sağlayan bir özellik tanımlar.
        private readonly IConnectionFactory connectionFactory; // Bağlantı fabrikasını temsil eden bir özellik tanımlar.
        private readonly IModel consumerChannel; // Tüketici kanalını temsil eden bir özellik tanımlar.

        public EventBusRabbitMQ(EventBusConfig config, IServiceProvider serviceProvider) : base(config, serviceProvider)
        {
            // Yapılandırmayı ve hizmet sağlayıcıyı kullanarak bir RabbitMQ bağlantısı oluşturur.
            // Bağlantı oluşturulurken, yapılandırma ayarlarına ve yeniden deneme sayısına dikkat edilir.
            // Ayrıca, abonelik yöneticisinin bir olayı kaldırdığında çağrılacak bir olay dinleyici eklenir.
        }

        private void SubsManager_OnEventRemoved(object sender, string eventName)
        {
            // Abonelik yöneticisi bir olay kaldırıldığında tetiklenen olay dinleyicisi.
            // Olayı işlemek için bağlantının olup olmadığı kontrol edilir.
            // Bağlantı varsa, tüketici kanalından ilgili olayın bağlantısını kaldırır.
            // Eğer abonelikler boşsa, tüketici kanalını kapatır.
        }

        public override void Publish(IntegrationEvent @event)
        {
            // Bir olayı RabbitMQ'ya yayınlamak için kullanılan yöntem.
            // Eğer bağlantı yoksa, bağlantıyı yeniden dener.
            // Yeniden deneme politikası tanımlanır ve olay, JSON olarak seri hale getirilip yayınlanır.
        }

        public override void Subscribe<T, TH>()
        {
            // Bir olaya abone olmak için kullanılan yöntem.
            // Eğer belirtilen olay için henüz abonelik yoksa, bağlantıyı yeniden dener.
            // Abonelik yoksa, ilgili olay için kuyruğu oluşturur ve olayı bekler.
        }

        public override void UnSubscribe<T, TH>()
        {
            // Bir olay aboneliğinden çıkmak için kullanılan yöntem.
            // Abonelik yöneticisinden ilgili aboneliği kaldırır.
        }

        private IModel CreateConsumerChannel()
        {
            // Bir tüketici kanalı oluşturur.
            // Eğer bağlantı yoksa, bağlantıyı yeniden dener.
            // Kanal, RabbitMQ ile etkileşimde bulunur ve gerekli değişimleri oluşturur.
        }

        private void StartBasicConsume(string eventName)
        {
            // Bir olayı tüketmek için temel bir tüketiciyi başlatır.
            // Eğer tüketici kanalı mevcutsa, bir tüketici oluşturur ve olayı işlemek için uygun bir yöntemi dinler.
        }

        private async void Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
        {
            // Bir olayı tükettiğinde çağrılan olay dinleyicisi.
            // Gelen olayı işlemek için bir işleyici çağırılır ve ardından işlenen olay için RabbitMQ'ya doğrulama yapılır.
        }
    }

}
