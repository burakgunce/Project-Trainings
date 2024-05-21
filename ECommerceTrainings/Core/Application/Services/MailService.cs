using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Core.Application.Services
{
    internal class MailService
    {
        /*
         * // MailService sınıfı, IMailService arayüzünü uygulayarak e-posta gönderme işlevselliğini sağlar.
public class MailService : IMailService
{
    // IConfiguration arayüzünü kullanarak yapılandırma ayarlarına erişim sağlar.
    readonly IConfiguration _configuration;

    // Yapıcı metot, IConfiguration bağımlılığını enjekte eder.
    public MailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // Tek bir e-posta adresine e-posta göndermek için kullanılan metot.
    // Bu metot, aslında birden fazla alıcıya e-posta gönderen diğer metodu çağırır.
    public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
    {
        // Tek bir e-posta adresini diziye çevirerek diğer metodu çağırır.
        await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
    }

    // Birden fazla e-posta adresine e-posta göndermek için kullanılan metot.
    public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
    {
        // Yeni bir MailMessage nesnesi oluşturur.
        MailMessage mail = new();
        // E-posta içeriğinin HTML olup olmadığını belirtir.
        mail.IsBodyHtml = isBodyHtml;

        // Alıcıların e-posta adreslerini ekler.
        foreach (var to in tos)
            mail.To.Add(to);

        // E-posta konusunu ayarlar.
        mail.Subject = subject;
        // E-posta içeriğini ayarlar.
        mail.Body = body;

        // Gönderen e-posta adresini ve görüntülenen adını ayarlar.
        mail.From = new(_configuration["Mail:Username"], "BG", System.Text.Encoding.UTF8);

        // Yeni bir SmtpClient nesnesi oluşturur.
        SmtpClient smtp = new();
        // SMTP kimlik doğrulama bilgilerini ayarlar.
        smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
        // SMTP portunu yapılandırmadan okur ve ayarlar.
        smtp.Port = int.Parse(_configuration["Mail:Port"]);
        // SSL bağlantısını etkinleştirir.
        smtp.EnableSsl = true;
        // SMTP sunucusunun adresini yapılandırmadan okur ve ayarlar.
        smtp.Host = _configuration["Mail:Host"];

        // E-postayı gönderir.
        await smtp.SendMailAsync(mail);
    }

        // SendPasswordResetMailAsync metodu, belirtilen e-posta adresine şifre yenileme bağlantısı içeren bir e-posta gönderir.
public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
{
    // E-posta içeriğini oluşturmak için StringBuilder kullanılır.
    StringBuilder mail = new();
    
    // E-posta içeriğine HTML formatında metin ekler.
    mail.AppendLine("Merhaba<br>Eğer yeni şifre talebinde bulunduysanız aşağıdaki linkten şifrenizi yenileyebilirsiniz.<br><strong><a target=\"_blank\" href=\"");
    
    // E-posta içeriğine Angular istemci URL'sini ekler. Bu URL, yapılandırma dosyasından alınır.
    mail.AppendLine(_configuration["AngularClientUrl"]);
    
    // Angular istemci URL'sine şifre yenileme yolunu ekler.
    mail.AppendLine("/update-password/");
    
    // URL'ye kullanıcı kimliğini ekler.
    mail.AppendLine(userId);
    
    // URL'ye şifre yenileme jetonunu ekler.
    mail.AppendLine("/");
    mail.AppendLine(resetToken);
    
    // E-posta içeriğine bağlantıyı tamamlar ve kullanıcıya tıklaması için talimat verir.
    mail.AppendLine("\">Yeni şifre talebi için tıklayınız...</a></strong><br><br><span style=\"font-size:12px;\">NOT : Eğer ki bu talep tarafınızca gerçekleştirilmemişse lütfen bu maili ciddiye almayınız.</span><br>Saygılarımızla...<br><br><br>BG");

    // Hazırlanan e-posta içeriğini kullanarak e-posta gönderme işlemini gerçekleştirir.
    await SendMailAsync(to, "Şifre Yenileme Talebi", mail.ToString());
}


        ////////// auth service de olan bazı gerekli metodlar
        
        // PasswordResetAsync metodu, kullanıcının e-posta adresine şifre yenileme bağlantısı göndermek için kullanılır.
public async Task PasswordResetAsync(string email)
{
    // Kullanıcıyı e-posta adresine göre arar.
    AppUser user = await _userManager.FindByEmailAsync(email);
    
    // Kullanıcı bulunursa, if bloğu içindeki işlemler gerçekleştirilir.
    if (user != null)
    {
        // Kullanıcı için bir şifre yenileme jetonu oluşturulur.
        string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        // Jetonun URL dostu bir formatta olmasını sağlamak için kodlanır.
        // Önceki iki satır yoruma alınmış ve alternatif olarak UrlEncode kullanılmıştır.
        // byte[] tokenBytes = Encoding.UTF8.GetBytes(resetToken);
        // resetToken = WebEncoders.Base64UrlEncode(tokenBytes);
        resetToken = resetToken.UrlEncode();

        // Kullanıcının e-posta adresine şifre yenileme bağlantısı içeren e-posta gönderilir.
        await _mailService.SendPasswordResetMailAsync(email, user.Id, resetToken);
    }

        //////GeneratePasswordResetTokenAsync(user); bu metod için configs de bir servis eklemen lazım ki üretebilsin
}



        // VerifyResetTokenAsync metodu, verilen şifre yenileme jetonunun geçerliliğini doğrulamak için kullanılır.
public async Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
{
    // Kullanıcıyı kimliğine göre arar.
    AppUser user = await _userManager.FindByIdAsync(userId);
    
    // Kullanıcı bulunursa, if bloğu içindeki işlemler gerçekleştirilir.
    if (user != null)
    {
        // Jetonun URL dostu formatını geri çevirmek için kodu çözülür.
        // Önceki iki satır yoruma alınmış ve alternatif olarak UrlDecode kullanılmıştır.
        // byte[] tokenBytes = WebEncoders.Base64UrlDecode(resetToken);
        // resetToken = Encoding.UTF8.GetString(tokenBytes);
        resetToken = resetToken.UrlDecode();

        // Jetonun geçerliliğini doğrulamak için UserManager'ın VerifyUserTokenAsync metodu kullanılır.
        // Parametreler:
        // - user: Doğrulanacak kullanıcı nesnesi.
        // - _userManager.Options.Tokens.PasswordResetTokenProvider: Şifre yenileme jetonu sağlayıcısı.
        // - "ResetPassword": Jetonun tipi (şifre sıfırlama).
        // - resetToken: Doğrulanacak jeton.
        return await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetToken);
    }

    // Kullanıcı bulunamazsa, false döner.
    return false;
}


        // userserviste updatepassword ile daha sonra securitystamp propunu ezeceksin ki reset mailine sürekli girilemesin cunku refresh token degısmı olacak 



}

         * 
         * 
         */
    }
}
