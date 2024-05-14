using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Core.Application.Services
{
    internal class GoogleAndFacebookLogin
    {
        /*
         * public async Task<Token> FacebookLoginAsync(string authToken, int accessTokenLifeTime)
{
    // Facebook erişim belgesi yanıtını alır
    string accessTokenResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_configuration["ExternalLoginSettings:Facebook:Client_ID"]}&client_secret={_configuration["ExternalLoginSettings:Facebook:Client_Secret"]}&grant_type=client_credentials");

    // Facebook erişim belgesi yanıtı DTO'ya ayrıştırılır
    FacebookAccessTokenResponse_DTO? facebookAccessTokenResponse = JsonSerializer.Deserialize<FacebookAccessTokenResponse_DTO>(accessTokenResponse);

    // Kullanıcı erişim belgesi doğrulamasını yapar
    string userAccessTokenValidation = await _httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={authToken}&access_token={facebookAccessTokenResponse?.AccessToken}");

    // Facebook kullanıcı erişim belgesi doğrulama DTO'su ayrıştırılır
    FacebookUserAccessTokenValidation_DTO? validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidation_DTO>(userAccessTokenValidation);

    // Eğer doğrulama başarılı ise devam edilir
    if (validation?.Data.IsValid != null)
    {
        // Kullanıcı bilgi yanıtını alır
        string userInfoResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={authToken}");

        // Facebook kullanıcı bilgi yanıtı DTO'ya ayrıştırılır
        FacebookUserInfoResponse? userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);

        // Kullanıcı giriş bilgisi oluşturulur
        var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");

        // Kullanıcı veritabanında bulunur
        Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

        // Kullanıcı dış kaynaklı olarak oluşturulur ve erişim belgesi oluşturulur
        return await CreateUserExternalAsync(user, userInfo.Email, userInfo.Name, info, accessTokenLifeTime);
    }
    // Geçersiz dış kaynaklı kimlik doğrulaması durumunda istisna fırlatılır
    throw new Exception("Invalid external authentication.");
}

public async Task<Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime)
{
    // Google doğrulama ayarları oluşturulur
    var settings = new GoogleJsonWebSignature.ValidationSettings()
    {
        Audience = new List<string> { _configuration["ExternalLoginSettings:Google:Client_ID"] }
    };

    // ID belirteci doğrulanır ve payload alınır
    var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
    // Kullanıcı giriş bilgisi oluşturulur
    var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");

    // Kullanıcı veritabanında bulunur
    Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

    // Kullanıcı dış kaynaklı olarak oluşturulur ve erişim belgesi oluşturulur
    return await CreateUserExternalAsync(user, payload.Email, payload.Name, info, accessTokenLifeTime);
}

         * 
         * 
         */
    }
}
