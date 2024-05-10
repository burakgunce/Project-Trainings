using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Infrastructure.Persistence
{
    internal class SaveChancesAsync
    {
        /*
         * 
         * public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
{
    // Değişiklik takipçisinden (ChangeTracker) tüm BaseEntity türevli varlıkları alır.
    var datas = ChangeTracker.Entries<BaseEntity>();

    // Her bir varlık için duruma göre işlem yapılır.
    foreach (var data in datas)
    {
        _ = data.State switch
        {
            // Eğer varlık yeni ekleniyorsa, CreatedDate özelliği şu anki zamanla güncellenir.
            EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,

            // Eğer varlık güncelleniyorsa, UpdatedDate özelliği şu anki zamanla güncellenir.
            EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,

            // Eğer varlık eklenmiyorsa veya güncellenmiyorsa, zaman bilgisine dokunulmaz.
            _ => DateTime.UtcNow
        };
    }

    // Temel SaveChangesAsync metodu çağrılarak değişiklikler veritabanına kaydedilir.
    return await base.SaveChangesAsync(cancellationToken);
}

         * 
         */
    }
}
