using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Presentation.Api
{
    internal class SerilogStructure
    {
        /*
         * Logger log = new LoggerConfiguration()
    .WriteTo.Console() // Konsola log yaz
    .WriteTo.File("logs/log.txt") // Dosyaya log yaz
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL"), "logs", needAutoCreateTable: true, // PostgreSQL veritabanına log yaz
    columnOptions: new Dictionary<string, ColumnWriterBase>
    {
        {"message", new RenderedMessageColumnWriter(NpgsqlDbType.Text)}, // 'message' sütununu mesaj içeriğiyle doldur
        {"message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text)}, // 'message_template' sütununu mesaj şablonuyla doldur
        {"level", new LevelColumnWriter(true , NpgsqlDbType.Varchar)}, // 'level' sütununu log seviyesiyle doldur
        {"time_stamp", new TimestampColumnWriter(NpgsqlDbType.Timestamp)}, // 'time_stamp' sütununu zaman damgasıyla doldur
        {"exception", new ExceptionColumnWriter(NpgsqlDbType.Text)}, // 'exception' sütununu istisna ile doldur
        {"log_event", new LogEventSerializedColumnWriter(NpgsqlDbType.Json)}, // 'log_event' sütununu JSON olarak doldur
        {"user_name", new UsernameColumnWriter()} // 'user_name' sütununu kullanıcı adıyla doldur
    })
    .WriteTo.Seq(builder.Configuration["Seq:ServerURL"]) // Seq sunucusuna log yaz
    .Enrich.FromLogContext() // Log bağlamından zenginleştirme ekle
    .MinimumLevel.Information() // Minimum log seviyesini ayarla
    .CreateLogger(); // Logger nesnesini oluştur

builder.Host.UseSerilog(log); // ASP.NET Core uygulamasına Serilog'u entegre et

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All; // Tüm HTTP alanlarını logla
    logging.RequestHeaders.Add("sec-ch-ua"); // İstek başlıklarına 'sec-ch-ua' eklemeyi logla
    logging.MediaTypeOptions.AddText("application/javascript"); // Ortam türü seçeneklerine 'application/javascript' eklemeyi logla
    logging.RequestBodyLogLimit = 4096; // İstek gövdelerinin log limitini ayarla
    logging.ResponseBodyLogLimit = 4096; // Yanıt gövdelerinin log limitini ayarla
});

app.UseSerilogRequestLogging(); // HTTP isteklerini otomatik olarak günlüğe al

app.UseHttpLogging(); // HTTP günlüğünü kullan

app.UseCors(); // CORS özelliğini etkinleştir

app.UseHttpsRedirection(); // HTTPS yönlendirmesini kullan

app.UseAuthentication(); // Kimlik doğrulamasını etkinleştir

app.UseAuthorization(); // Yetkilendirme işlemlerini etkinleştir

app.Use(async (context, next) =>
{
    var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null; // Kullanıcı adını al
    LogContext.PushProperty("user_name", username); // Log bağlamına kullanıcı adını ekle
    await next(); // Sonraki middleware'a geç
});

public class UsernameColumnWriter : ColumnWriterBase
{
    public UsernameColumnWriter() : base(NpgsqlDbType.Varchar)
    {
    }

    public override object GetValue(LogEvent logEvent, IFormatProvider formatProvider = null)
    {
        var (username, value) = logEvent.Properties.FirstOrDefault(p => p.Key == "user_name"); // Log olayından kullanıcı adı değerini al
        return value?.ToString() ?? null; // Değer varsa dön, yoksa null dön
    }
}


        UseSerilogRequestLogging() BUNDAN ONCE YAZAN SEYLERI LOGLAMAZ SONRAKILERI LOGLAR BU YUZDEN NEREYE YAZDIGIN ONEMLI

        UsernameColumnWriter BUNU ISE HANGI KULLANICININ NE YAPTIGINI GOREBILMEK ICIN EXTENSION TARZINDA BIR MUHABBETLE YAPIYA EKLIYORUZ KULLANICI BILGISINI BURASI VE MIDDLEWARE DEKI USE(ASYNC (CONTEXT, NEXT)) SAYESINDE ALIYORUZ

         * 
         * 
         */
    }
}
