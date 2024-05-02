using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForumTrainings.WebApp
{
    public class AuthTokenHandler : DelegatingHandler
    {
        private readonly ISyncLocalStorageService _syncLocalStorageService; // Kullanıcının tarayıcı yerel depolama hizmetinden token alır.

        public AuthTokenHandler(ISyncLocalStorageService syncLocalStorageService)
        {
            _syncLocalStorageService = syncLocalStorageService; // Bağımlılığı enjekte eder.
        }

        // HTTP isteğini işlemek için bu metod kullanılır.
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _syncLocalStorageService.GetToken(); // Kullanıcının tarayıcı yerel depolama hizmetinden token bilgisini alır.

            if (!string.IsNullOrEmpty(token) && (request.Headers.Authorization is null || string.IsNullOrEmpty(request.Headers.Authorization.Parameter)))
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token); // İstek başlığına token ekler.

            return base.SendAsync(request, cancellationToken); // Temel işlemi gerçekleştirir ve sonucu döndürür.
        }
    }

}
