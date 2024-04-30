using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForumTrainings.Persistence
{
    internal class SeedData
    {
        // Rastgele kullanıcılar oluşturur ve döndürür
        private static List<User> GetUsers()
        {
            var result = new Faker<User>("tr")
                .RuleFor(i => i.Id, i => Guid.NewGuid()) // Rastgele bir GUID oluşturur
                .RuleFor(i => i.CreateDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now)) // Oluşturulma tarihini belirler
                .RuleFor(i => i.FirstName, i => i.Person.FirstName) // Rastgele bir isim oluşturur
                .RuleFor(i => i.LastName, i => i.Person.LastName) // Rastgele bir soyisim oluşturur
                .RuleFor(i => i.EmailAddress, i => i.Internet.Email()) // Rastgele bir e-posta adresi oluşturur
                .RuleFor(i => i.UserName, i => i.Internet.UserName()) // Rastgele bir kullanıcı adı oluşturur
                .RuleFor(i => i.Password, i => PasswordEncryptor.Encrypt(i.Internet.Password())) // Rastgele bir şifre oluşturur ve şifreler
                .RuleFor(i => i.EmailConfirmed, i => i.PickRandom(true, false)) // Rastgele bir onaylanmış/ onaylanmamış e-posta durumu oluşturur
                .Generate(500); // 500 kullanıcı oluşturur

            return result;
        }

        // Veritabanına başlangıç verilerini ekler
        public async Task SeedAsync(IConfiguration configuration)
        {
            var dbContextBuilder = new DbContextOptionsBuilder();
            dbContextBuilder.UseSqlServer(configuration["BlazorSozlukDbConnectionString"]); // Veritabanı bağlantı dizesini alır ve kullanır

            var context = new BlazorSozlukContext(dbContextBuilder.Options); // Veritabanı bağlamını oluşturur

            // Eğer veritabanında kullanıcı varsa işlemi tamamla
            if (context.Users.Any())
            {
                await Task.CompletedTask;
                return;
            }

            // Kullanıcıları al ve veritabanına ekle
            var users = GetUsers();
            await context.Users.AddRangeAsync(users);

            // Entry ve EntryComment nesnelerini oluştur ve veritabanına ekle
            var entries = GenerateEntries(users);
            var comments = GenerateComments(users, entries);

            await context.Entries.AddRangeAsync(entries);
            await context.EntryComments.AddRangeAsync(comments);

            // Değişiklikleri veritabanına kaydet
            await context.SaveChangesAsync();
        }

        // Girişler oluşturur
        private static List<Entry> GenerateEntries(List<User> users)
        {
            var guids = Enumerable.Range(0, 150).Select(i => Guid.NewGuid()).ToList(); // 150 adet GUID oluşturur
            int counter = 0;

            var entries = new Faker<Entry>("tr")
                .RuleFor(i => i.Id, i => guids[counter++]) // GUID'leri girişlere atar
                .RuleFor(i => i.CreateDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now)) // Oluşturulma tarihlerini belirler
                .RuleFor(i => i.Subject, i => i.Lorem.Sentence(5, 5)) // Konuları rastgele oluşturur
                .RuleFor(i => i.Content, i => i.Lorem.Paragraph(2)) // İçerikleri rastgele oluşturur
                .RuleFor(i => i.CreatedById, i => i.PickRandom(users.Select(u => u.Id))) // Oluşturan kullanıcıları rastgele seçer
                .Generate(150); // 150 adet giriş oluşturur

            return entries;
        }

        // Yorumlar oluşturur
        private static List<EntryComment> GenerateComments(List<User> users, List<Entry> entries)
        {
            var comments = new Faker<EntryComment>("tr")
                .RuleFor(i => i.Id, i => Guid.NewGuid()) // Rastgele bir GUID oluşturur
                .RuleFor(i => i.CreateDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now)) // Oluşturulma tarihlerini belirler
                .RuleFor(i => i.Content, i => i.Lorem.Paragraph(2)) // İçerikleri rastgele oluşturur
                .RuleFor(i => i.CreatedById, i => i.PickRandom(users.Select(u => u.Id))) // Oluşturan kullanıcıları rastgele seçer
                .RuleFor(i => i.EntryId, i => i.PickRandom(entries.Select(e => e.Id))) // Girişleri rastgele seçer
                .Generate(1000); // 1000 adet yorum oluşturur

            return comments;
        }
    }

}
