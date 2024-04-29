using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTrainings.ApiGateway
{
    //api gateway basketservice e direk ulaşasağı için basket servis bir token bekliyor ama tokeni veren aslında identity bu yüzden burda ordan gelen bi token olmayacak dolayısıyla senin dışardan gelen tokenleri burası aracılığıyla basket service e set etmen lazım
    public class HttpClientDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor httpContextAccessor; // HTTP bağlamına erişimi sağlayan bir referans

        // Yapılandırıcı, IHttpContextAccessor bağımlılığını alır.
        public HttpClientDelegatingHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor; // IHttpContextAccessor örneğini ayarlar
        }

        // SendAsync yöntemi, HTTP isteği gönderildiğinde çağrılır.
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // HTTP isteği üzerindeki Authorization başlığını alır.
            var authorizationHeader = httpContextAccessor.HttpContext.Request.Headers["Authorization"];

            // Eğer Authorization başlığı varsa, isteğin Authorization başlığını günceller veya ekler.
            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                if (request.Headers.Contains("Authorization"))
                    request.Headers.Remove("Authorization"); // Eğer istek üzerinde zaten bir Authorization başlığı varsa, kaldırır.

                request.Headers.Add("Authorization", new List<string>() { authorizationHeader }); // Yeni Authorization başlığını ekler.
            }

            // Üst sınıfa (DelegatingHandler) HTTP isteğini gönderir ve yanıtı alır.
            return base.SendAsync(request, cancellationToken);
        }
    }

}
