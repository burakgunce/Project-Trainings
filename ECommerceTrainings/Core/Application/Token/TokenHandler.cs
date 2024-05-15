using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Core.Application.Token
{
    internal class TokenHandler
    {
        /*
         * 
         * public class TokenHandler : ITokenHandler
{
    readonly IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // Erişim belirteci oluşturma işlemi
    public Application.DTOs.Token CreateAccessToken(int second, AppUser user)
    {
        // Yeni bir erişim belirteci nesnesi oluşturulur
        Application.DTOs.Token token = new();

        // Güvenlik anahtarı oluşturulur. Güvenlik anahtarı, belirtecin imzalanması için kullanılacak gizli anahtardır.
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

        // Belirtecin imzalanması için gerekli olan imza bilgileri oluşturulur.
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        // Belirtecin geçerlilik süresi belirlenir. Bu, belirtecin son kullanma tarihini belirler.
        token.Expiration = DateTime.UtcNow.AddSeconds(second);
        
        // JWT (JSON Web Token) güvenlik belirteci oluşturulur.
        JwtSecurityToken securityToken = new(
            // Belirtecin hedef kitlesi (audience) belirlenir. Bu, belirtecin hangi uygulamalar tarafından kullanılabileceğini belirler.
            audience: _configuration["Token:Audience"],
            // Belirtecin yayıncısı (issuer) belirlenir. Bu, belirtecin kim tarafından oluşturulduğunu belirler.
            issuer: _configuration["Token:Issuer"],
            // Belirtecin son kullanma tarihi belirlenir.
            expires: token.Expiration,
            // Belirtecin geçerlilik tarih aralığı belirlenir. Bu, belirtecin ne zaman kullanılabileceğini belirler.
            notBefore: DateTime.UtcNow,
            // Belirtecin imza bilgileri belirlenir.
            signingCredentials: signingCredentials,
            // Belirtecin içinde bulunacak ek bilgiler (claims) belirlenir. Burada kullanıcı adı (user.UserName) belirtilir.
            claims: new List<Claim> { new(ClaimTypes.Name, user.UserName)}
            );

        // JWT belirteci işleyicisi oluşturulur.
        JwtSecurityTokenHandler tokenHandler = new();
        // JWT belirteci yazılır ve erişim belirtecine atanır.
        token.AccessToken = tokenHandler.WriteToken(securityToken);

        // Yenileme belirteci oluşturulur
        token.RefreshToken = CreateRefreshToken();
        
        return token;
    }

    // Yenileme belirteci oluşturma işlemi
    public string CreateRefreshToken()
    {
        // 32 byte uzunluğunda rastgele bir dizi oluşturulur.
        byte[] number = new byte[32];
        // Rastgele sayı üreteci oluşturulur.
        using RandomNumberGenerator random = RandomNumberGenerator.Create();
        // Rastgele sayılar oluşturulur.
        random.GetBytes(number);
        // Oluşturulan rastgele sayılar base64 formatına dönüştürülerek yenileme belirtecine atanır.
        return Convert.ToBase64String(number);
    }
}

         * 
         */
    }
}
