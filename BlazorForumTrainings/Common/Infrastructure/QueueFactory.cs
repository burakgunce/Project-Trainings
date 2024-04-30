using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorForumTrainings.Common.Infrastructure
{
    public static class QueueFactory
    {
        // RabbitMQ'ya mesaj göndermek için kullanılır.
        public static void SendMessageToExchange(string exchangeName, string exchangeType, string queueName, object obj)
        {
            // Temel tüketiciyi oluşturur.
            var channel = CreateBasicConsumer()
                            // Belirtilen değiş tokuşu (exchange) oluşturur veya kontrol eder.
                            .EnsureExchange(exchangeName, exchangeType)
                            // Belirtilen kuyruğu oluşturur veya kontrol eder.
                            .EnsureQueue(queueName, exchangeName)
                            .Model;

            // Gönderilecek nesneyi JSON formatına dönüştürür.
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj));

            // Mesajı belirtilen değiş tokuşa (exchange) gönderir.
            channel.BasicPublish(exchange: exchangeName,
                                 routingKey: queueName,
                                 basicProperties: null,
                                 body: body);
        }

        // RabbitMQ sunucusuna bağlanmak için bir tüketici oluşturur.
        public static EventingBasicConsumer CreateBasicConsumer()
        {
            var factory = new ConnectionFactory() { HostName = SozlukConstants.RabbitMQHost };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            return new EventingBasicConsumer(channel);
        }

        // Belirtilen değiş tokuşu (exchange) oluşturur veya kontrol eder.
        public static EventingBasicConsumer EnsureExchange(this EventingBasicConsumer consumer, string exchangeName,
            string exchangeType = SozlukConstants.DefaultExchangeType)
        {
            // Değiş tokuşu oluşturur veya kontrol eder.
            consumer.Model.ExchangeDeclare(exchange: exchangeName,
                                           type: exchangeType,
                                           durable: false,
                                           autoDelete: false);
            return consumer;
        }

        // Belirtilen kuyruğu oluşturur veya kontrol eder.
        public static EventingBasicConsumer EnsureQueue(this EventingBasicConsumer consumer, string queueName, string exchangeName)
        {
            // Kuyruğu oluşturur veya kontrol eder.
            consumer.Model.QueueDeclare(queue: queueName,
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        null);

            // Değiş tokuş ve kuyruk arasında bağlantı kurar.
            consumer.Model.QueueBind(queueName, exchangeName, queueName);

            return consumer;
        }

        // Kuyruktan mesajları alır ve belirtilen işlevi uygular.
        public static EventingBasicConsumer Receive<T>(this EventingBasicConsumer consumer, Action<T> act)
        {
            consumer.Received += (m, eventArgs) =>
            {
                // Gelen mesajı işler.
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var model = JsonSerializer.Deserialize<T>(message);
                act(model);
                // Mesaj işlendikten sonra RabbitMQ'ya ACK (onay) gönderilir.
                consumer.Model.BasicAck(eventArgs.DeliveryTag, false);
            };
            return consumer;
        }

        // Belirtilen bir kuyruğu dinlemeye (consume) başlar.
        public static EventingBasicConsumer StartConsuming(this EventingBasicConsumer consumer, string queueName)
        {
            // Kuyruğu dinlemeye başlar.
            consumer.Model.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
            return consumer;
        }
    }

}
