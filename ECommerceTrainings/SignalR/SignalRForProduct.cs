using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.SignalR
{
    internal class SignalRForProduct
    {
        /*
         * 
         * public class ProductHub : Hub
            {
            }

        Bu sınıf, SignalR hub'ını tanımlar. SignalR hub'ları, istemcilerle sunucu arasındaki gerçek zamanlı iletişimi kolaylaştıran merkezi bir iletişim noktasıdır. Hub sınıfından türetilir ve hub'ın işlevselliğini sağlar.



        public class ProductHubService : IProductHubService
{
    readonly IHubContext<ProductHub> _hubContext;

    public ProductHubService(IHubContext<ProductHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task ProductAddedMessageAsync(string message)
    {
        await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.ProductAddedMessage, message);
    }
}
Bu sınıf, SignalR hub'ına erişmek için bir servis sağlar. IProductHubService arayüzünü uygular ve hub'a mesaj göndermek için bir metot sağlar. IHubContext<T> türü, belirtilen hub türüne erişmek için kullanılır. Bu örnekte ProductHub hub'ına erişmek için kullanılır.



        public static class HubRegistration
{
    public static void MapHubs(this WebApplication webApplication)
    {
        webApplication.MapHub<ProductHub>("/product-hub");
        webApplication.MapHub<OrderHub>("/orders-hub");
    }
}
Bu sınıf, hub'ları rotalara eşlemek için bir genişletme metodu sağlar. MapHub metodu, belirtilen hub türünü ve rotayı alır ve bu rotayı hub ile eşler. Bu sayede istemciler, belirtilen rota aracılığıyla hub'a bağlanabilirler.



        public static class ServiceRegistration
{
    public static void AddSignalRServices(this IServiceCollection collection)
    {
        collection.AddTransient<IProductHubService, ProductHubService>();
        collection.AddTransient<IOrderHubService, OrderHubService>();
        collection.AddSignalR();
    }
}
         * 
         */
    }
}
