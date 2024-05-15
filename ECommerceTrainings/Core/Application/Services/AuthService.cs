using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Core.Application.Services
{
    internal class AuthService
    {
        /*
         * public class AuthService : IAuthService
{
    readonly HttpClient _httpClient;
    readonly IConfiguration _configuration;
    readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
    readonly ITokenHandler _tokenHandler;
    readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;
    readonly IUserService _userService;
    readonly IMailService _mailService;

    public AuthService(IHttpClientFactory httpClientFactory, IConfiguration configuration, UserManager<Domain.Entities.Identity.AppUser> userManager, ITokenHandler tokenHandler, SignInManager<AppUser> signInManager, IUserService userService, IMailService mailService)
    {
        // HttpClient, IConfiguration ve diğer bağımlılıkları enjekte ediyoruz.
        _httpClient = httpClientFactory.CreateClient();
        _configuration = configuration;
        _userManager = userManager;
        _tokenHandler = tokenHandler;
        _signInManager = signInManager;
        _userService = userService;
        _mailService = mailService;
    }

    async Task<Token> CreateUserExternalAsync(AppUser user, string email, string name, UserLoginInfo info, int accessTokenLifeTime)
    {
        // Kullanıcı yoksa oluşturuyoruz veya buluyoruz.
        bool result = user != null;
        if (user == null)
        {
            user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new()
                {
                    // Kullanıcı bilgilerini ayarlıyoruz.
                    Id = Guid.NewGuid().ToString(),
                    Email = email,
                    UserName = email,
                    NameSurname = name
                };
                // Kullanıcı oluşturuluyor.
                var identityResult = await _userManager.CreateAsync(user);
                result = identityResult.Succeeded;
            }
        }

        // Kullanıcı oluşturulduysa veya bulunduysa, dışarıdan gelen kullanıcıyı ekliyoruz ve bir erişim jetonu oluşturuyoruz.
        if (result)
        {
            await _userManager.AddLoginAsync(user, info); //AspNetUserLogins

            Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
            await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 15);
            return token;
        }
        throw new Exception("Invalid external authentication.");
    }

    // Facebook dış giriş işlemi
    public async Task<Token> FacebookLoginAsync(string authToken, int accessTokenLifeTime)
    {
        // Facebook erişim belirteci alma
        string accessTokenResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_configuration["ExternalLoginSettings:Facebook:Client_ID"]}&client_secret={_configuration["ExternalLoginSettings:Facebook:Client_Secret"]}&grant_type=client_credentials");

        FacebookAccessTokenResponse_DTO? facebookAccessTokenResponse = JsonSerializer.Deserialize<FacebookAccessTokenResponse_DTO>(accessTokenResponse);

        string userAccessTokenValidation = await _httpClient.GetStringAsync("https// link + input_token {authToken}, access_token {facebookAccessTokenResponse?.AccessToken}");

        FacebookUserAccessTokenValidation_DTO? validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidation_DTO>(userAccessTokenValidation);

        if (validation?.Data.IsValid != null)
        {
            string userInfoResponse = await _httpClient.GetStringAsync("https link + me? + fields email,name & access_token {authToken}");

            FacebookUserInfoResponse? userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);

            var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");

            Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            return await CreateUserExternalAsync(user, userInfo.Email, userInfo.Name, info, accessTokenLifeTime);
        }
        throw new Exception("Invalid external authentication.");
    }

    // Google dış giriş işlemi
    public async Task<Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string> { _configuration["ExternalLoginSettings:Google:Client_ID"] }
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
        var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");

        Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

        return await CreateUserExternalAsync(user, payload.Email, payload.Name, info, accessTokenLifeTime);
    }

    // Kullanıcı girişi işlemi
    public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
    {
        // Kullanıcıyı kullanıcı adına veya e-postaya göre buluyoruz.
        Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
        if (user == null)
            user = await _userManager.FindByEmailAsync(usernameOrEmail);

        if (user == null)
            throw new NotFoundUserException();

        // Kullanıcının şifresini doğruluyoruz.
        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (result.Succeeded)
        {
            // Başarılı giriş ise bir erişim jetonu oluşturuyoruz.
            Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
            await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 15);
            return token;
        }
        else
            throw new AuthenticationErrorException();
    }

    // Yenileme belirtecine göre kullanıcı girişi işlemi
    public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
    {
        AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
        {
            // Yenileme belirtecini kontrol ediyoruz ve kullanıcıya bir erişim jetonu oluşturuyoruz.
            Token token = _tokenHandler.CreateAccessToken(15, user);
            await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 15);
            return token;
        }
        else
            throw new NotFoundUserException() ;
    }

    // Şifre sıfırlama işlemi
    public async Task PasswordResetAsnyc(string email)
    {
        // E-posta adresine göre kullanıcıyı buluyoruz.
        AppUser user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            // Şifre sıfırlama belirteci oluşturuyoruz ve e-posta ile gönderiyoruz.
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            resetToken = resetToken.UrlEncode();

            await _mailService.SendPasswordResetMailAsync(email, user.Id, resetToken);
        }
    }

    // Şifre sıfırlama belirtecinin doğrulanması
    public async Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
    {
        // Kullanıcıyı kimliğine göre buluyoruz.
        AppUser user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            resetToken = resetToken.UrlDecode();

            // Şifre sıfırlama belirtecinin doğruluğunu doğruluyoruz.
            return await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetToken);
        }
        return false;
    }
}

         * 
         * 
         */
    }
}
