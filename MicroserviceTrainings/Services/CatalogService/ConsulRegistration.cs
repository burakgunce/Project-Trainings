using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTrainings.Services.CatalogService
{
    internal class ConsulRegistration
    {
    }
    public static class ConsulRegistration
    {
        // ConfigureServices yöntemi içinde Consul istemcisinin yapılandırılmasını sağlayan bir genişletme yöntemi.
        public static IServiceCollection ConfigureConsul(this IServiceCollection services, IConfiguration configuration)
        {
            // Consul istemcisini yapılandırmak için kullanılır.
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                // Yapılandırma dosyasından Consul'un adresini alır.
                var address = configuration["ConsulConfig:Address"];
                consulConfig.Address = new Uri(address);
            }));

            // Genişletilmiş IServiceCollection nesnesini döndürür.
            return services;
        }

        // Configure yöntemi içinde Consul'a kayıt yapmayı sağlayan bir genişletme yöntemi.
        public static IApplicationBuilder RegisterWithConsul(this IApplicationBuilder app, IHostApplicationLifetime lifetime, IConfiguration configuration)
        {
            // Gerekli hizmetlerin alınması.
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var loggingFactory = app.ApplicationServices.GetRequiredService
            <ILoggerFactory>();

            // ILoggerFactory aracılığıyla bir ILogger oluşturulur.
            var logger = loggingFactory.CreateLogger<IApplicationBuilder>();

            // Yapılandırmadan servis adresi, adı ve kimliği alınır.
            var uri = configuration.GetValue(Uri > ("ConsulConfig:ServiceAddress");
            var serviceName = configuration.GetValue<string>("ConsulConfig:ServiceName");
            var serviceId = configuration.GetValue<string>("ConsulConfig:ServiceId");

            // AgentServiceRegistration nesnesi oluşturulur ve Consul'a kayıt yapılır.
            var registration = new AgentServiceRegistration()
            {
                ID = serviceId ?? "CatalogService",
                Name = serviceName ?? "CatalogService",
                Address = $"{uri.Host}",
                Port = uri.Port,
                Tags = new[] { serviceName, serviceId }
            };

            // Consul'a kayıt yapılır ve işlem tamamlandığında kayıt silinir.
            logger.LogInformation("Registering with Consul");
            consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            consulClient.Agent.ServiceRegister(registration).Wait();

            // Uygulama sonlandırıldığında Consul'dan kaydın silinmesi sağlanır.
            lifetime.ApplicationStopping.Register(() =>
            {
                logger.LogInformation("Deregistering from Consul");
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            });

            // IApplicationBuilder nesnesi döndürülür.
            return app;
        }
    }
}
