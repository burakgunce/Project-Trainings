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
}

         * 
         * 
         */
    }
}
