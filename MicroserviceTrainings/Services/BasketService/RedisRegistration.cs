using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTrainings.Services.BasketService
{
    public static class RedisRegistration
    {
        // Redis yapılandırmasını sağlayan genişletme yöntemi
        public static ConnectionMultiplexer ConfigureRedis(this IServiceProvider services, IConfiguration configuration)
        {
            // Redis yapılandırma ayarlarını alınır ve ConfigurationOptions nesnesine ayrıştırılır
            var redisConf = ConfigurationOptions.Parse(configuration["RedisSettings:ConnectionString"], true);

            // DNS çözümlemesi için belirtilen ayarın yapılıp yapılmayacağı belirlenir
            redisConf.ResolveDns = true;

            // Redis sunucusuna bağlanmak için bir ConnectionMultiplexer nesnesi oluşturulur ve döndürülür
            return ConnectionMultiplexer.Connect(redisConf);
        }
    }

}
