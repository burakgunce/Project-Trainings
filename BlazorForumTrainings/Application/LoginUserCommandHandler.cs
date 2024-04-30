using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForumTrainings.Application
{
    internal class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly IUserRepository userRepository; // Kullanıcı veritabanı işlemlerini gerçekleştirmek için repository bağımlılığı
        private readonly IMapper mapper; // Veri aktarımı için nesne eşleştirme işlemlerini gerçekleştirmek için mapper bağımlılığı
        private readonly IConfiguration configuration; // Yapılandırma bilgilerine erişmek için bağımlılık

        public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        // Giriş işlemini gerçekleştiren metot
        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            // Kullanıcıyı e-posta adresine göre veritabanından getir
            var dbUser = await userRepository.GetSingleAsync(i => i.EmailAddress == request.EmailAddress);

            // Eğer kullanıcı bulunamazsa hata fırlat
            if (dbUser == null)
                throw new DatabaseValidationException("User not found!");

            // Kullanıcının parolasını şifrele ve veritabanındakiyle karşılaştır
            var pass = PasswordEncryptor.Encrypt(request.Password);
            if (dbUser.Password != pass)
                throw new DatabaseValidationException("Password is wrong!");

            // Eğer kullanıcının e-posta adresi onaylanmamışsa hata fırlat
            if (!dbUser.EmailConfirmed)
                throw new DatabaseValidationException("Email address is not confirmed yet!");

            // Kullanıcı bilgilerini bir view model'e eşle ve token oluştur
            var result = mapper.Map<LoginUserViewModel>(dbUser);

            var claims = new Claim[]
            {
            // JWT token için talepleri oluştur
            new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
            new Claim(ClaimTypes.Email, dbUser.EmailAddress),
            new Claim(ClaimTypes.Name, dbUser.UserName),
            new Claim(ClaimTypes.GivenName, dbUser.FirstName),
            new Claim(ClaimTypes.Surname, dbUser.LastName)
            };

            // Token oluştur ve view modele ekle
            result.Token = GenerateToken(claims);

            return result;
        }

        // JWT token oluşturma işlemini gerçekleştiren metot
        private string GenerateToken(Claim[] claims)
        {
            // Secret key ve signing credentials oluştur
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthConfig:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Token'in geçerlilik süresini belirle
            var expiry = DateTime.Now.AddDays(10);

            // Token'i oluştur
            var token = new JwtSecurityToken(
                claims: claims,
                expires: expiry,
                signingCredentials: creds,
                notBefore: DateTime.Now
            );

            // Token'i string olarak dön
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
