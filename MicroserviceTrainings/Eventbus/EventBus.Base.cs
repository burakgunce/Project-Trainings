using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTrainings.Eventbus
{
    internal class EventBus
    {
        //servisler arasındaki iletişimin yöntemlerini belirleme, servisler arası iletişimin saglanması
        /*
         * BaseEventBus = kullanacağımız eventbus a göre fonksiyonlar direkt burdan gidecek
         * integration event = diğer servislere haber ulaştırma
         * IEventBus = servislerin hangi eventlere subscribe edileceklerini
         * EventBus Config = eventbus için default ayarlar 
         * 
         * 
         * 
         * 
         */
    }
    public abstract class BaseEventBus : IEventBus, IDisposable // BaseEventBus adında bir soyut sınıf tanımlar. IEventBus ve IDisposable arabirimlerini uygular.
    {
        public readonly IServiceProvider ServiceProvider; // ServiceProvider adında bir okunabilir özellik tanımlar. IServiceProvider, hizmetlerin kullanımını sağlar.
        public readonly IEventBusSubscriptionManager SubsManager; // SubsManager adında bir okunabilir özellik tanımlar. IEventBusSubscriptionManager, olay aboneliklerini yönetir.

        public EventBusConfig EventBusConfig { get; private set; } // EventBusConfig adında bir özellik tanımlar. Bu, olay otobüsünün yapılandırma ayarlarını tutar.

        public BaseEventBus(EventBusConfig config, IServiceProvider serviceProvider) // Bir yapılandırıcı tanımlar.
        {
            EventBusConfig = config; // EventBusConfig özelliğine yapılandırma ayarlarını atar.
            ServiceProvider = serviceProvider; // ServiceProvider özelliğine hizmet sağlayıcıyı atar.
            SubsManager = new InMemoryEventBusSubscriptionManager(ProcessEventName); // SubsManager özelliğini başlatır.
        }

        public virtual string ProcessEventName(string eventName) // Olay adını işleyen bir yöntem tanımlar.
        {
            if (EventBusConfig.DeleteEventPrefix) // Eğer Event Bus yapılandırması önek silmeyi işaret ediyorsa
                eventName = eventName.TrimStart(EventBusConfig.EventNamePrefix.ToArray()); // Event adının başındaki önekleri kaldırır.

            if (EventBusConfig.DeleteEventSuffix) // Eğer Event Bus yapılandırması sonek silmeyi işaret ediyorsa
                eventName = eventName.TrimEnd(EventBusConfig.EventNameSuffix.ToArray()); // Event adının sonundaki sonekleri kaldırır.

            return eventName; // İşlenmiş olay adını döndürür.
        }

        public virtual string GetSubName(string eventName) // Bir abonelik adı döndüren bir yöntem tanımlar.
        {
            return $"{EventBusConfig.SubscriberClientAppName}.{ProcessEventName(eventName)}"; // Abone olunan olayın adına dayalı bir isim oluşturur.
        }

        public virtual void Dispose() // IDisposable arayüzünden gelen yöntemi uygular.
        {
            EventBusConfig = null; // EventBusConfig özelliğini boşaltır.
            SubsManager.Clear(); // SubsManager'ı temizler.
        }

        public async Task<bool> ProcessEvent(string eventName, string message) // Bir olayı işlemek için bir yöntem tanımlar.
        {
            eventName = ProcessEventName(eventName); // Olay adını işler.

            var processed = false; // İşlenip işlenmediğini belirlemek için bir bayrak tanımlar ve varsayılan olarak "false" değerini atar.

            if (SubsManager.HasSubscriptionsForEvent(eventName)) // Eğer belirtilen olay için abonelikler varsa
            {
                var subscriptions = SubsManager.GetHandlersForEvent(eventName); // Olay için abonelikleri alır.

                using (var scope = ServiceProvider.CreateScope()) // Servis kapsamını oluşturur.
                {
                    foreach (var subscription in subscriptions) // Abonelikler üzerinde döner.
                    {
                        var handler = ServiceProvider.GetService(subscription.HandlerType); // Aboneliğe karşılık gelen işleyiciyi alır.
                        if (handler == null) continue; // Eğer işleyici null ise sonraki aboneliğe geçer.

                        var eventType = SubsManager.GetEventTypeByName($"{EventBusConfig.EventNamePrefix}{eventName}{EventBusConfig.EventNameSuffix}"); // Olay türünü alır.
                        var integrationEvent = JsonConvert.DeserializeObject(message, eventType); // JSON mesajını belirtilen türe dönüştürür.

                        var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType); // Olay işleyici türünü oluşturur.
                        await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent }); // İşleyiciyi çağırır.
                    }
                }

                processed = true; // İşleme işlemini gerçekleştirdiği için bayrağı "true" olarak işaretler.
            }

            return processed; // İşleme işlemi başarıyla gerçekleştirildi ise "true", aksi takdirde "false" döndürür.
        }


        public abstract void Publish(IntegrationEvent @event); // Bir olayı yayınlamak için soyut bir yöntem tanımlar.

        public abstract void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>; // Bir olaya abone olmak için soyut bir yöntem tanımlar.

        public abstract void UnSubscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>; // Bir olay aboneliğinden çıkmak için soyut bir yöntem tanımlar.
    }
}
