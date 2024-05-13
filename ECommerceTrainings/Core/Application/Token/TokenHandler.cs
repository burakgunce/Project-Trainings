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

    // Constructor: Configuration enjekte edilir
    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // Erişim belgesi oluşturur
    public Application.DTOs.Token CreateAccessToken(int second, AppUser user)
    {
        Application.DTOs.Token token = new();

        // Güvenlik anahtarı oluşturulur
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

        // İmzalama kimlik bilgileri oluşturulur
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        // Token'in son kullanma tarihini belirler
        token.Expiration = DateTime.UtcNow.AddSeconds(second);
        
        // JWT güvenlik belgesi oluşturulur
        JwtSecurityToken securityToken = new(
            audience: _configuration["Token:Audience"],
            issuer: _configuration["Token:Issuer"],
            expires: token.Expiration,
            notBefore: DateTime.UtcNow
            );

        // JWT güvenlik belgesi yazıcı oluşturulur
        JwtSecurityTokenHandler tokenHandler = new();
        // Erişim belgesini yazılı bir JWT belgesine dönüştürür
        token.AccessToken = tokenHandler.WriteToken(securityToken);

        return token;
    }
}
         * 
         */
    }
}
