using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForumTrainings.WebApp
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService; // Kullanıcının tarayıcı yerel depolama hizmetini kullanarak oturum kimliğini alır.
        private readonly AuthenticationState _anonymous; // Anonim kimlik durumu, kullanıcı oturum açmadığında kullanılır.

        public AuthStateProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())); // Anonim kimlik durumunu oluşturur.
        }

        // Kullanıcının oturum kimliği durumunu alır.
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var apiToken = await _localStorageService.GetToken(); // Kullanıcının tarayıcı yerel depolama hizmetinden API belirteç bilgisini alır.

            if (string.IsNullOrEmpty(apiToken)) // API belirteci yoksa, kullanıcı oturum açmamıştır.
            {
                return _anonymous; // Anonim kimlik durumunu döndürür.
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadJwtToken(apiToken); // JWT belirteç bilgisini çözümler.

            var cp = new ClaimsPrincipal(new ClaimsIdentity(securityToken.Claims, "jwtAuthType")); // JWT'den elde edilen bilgilerle kimlik oluşturur.

            return new AuthenticationState(cp); // Kimlik durumunu döndürür.
        }

        // Kullanıcı oturum açma işlemi gerçekleştiğinde bu metod çağrılır ve yeni kimlik durumu bildirilir.
        public void NotifyUserLogin(string userName, Guid userId)
        {
            var cp = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.Name, userName), // Kullanıcının adı
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()) // Kullanıcının benzersiz kimliği
        }, "jwtAuthType")); // Kimlik türü

            var authState = Task.FromResult(new AuthenticationState(cp)); // Yeni kimlik durumunu oluşturur.
            NotifyAuthenticationStateChanged(authState); // Yeni kimlik durumunu bildirir.
        }

        // Kullanıcı oturum kapatma işlemi gerçekleştiğinde bu metod çağrılır ve anonim kimlik durumu bildirilir.
        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(_anonymous); // Anonim kimlik durumunu oluşturur.
            NotifyAuthenticationStateChanged(authState); // Anonim kimlik durumunu bildirir.
        }
    }

}
