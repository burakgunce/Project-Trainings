using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Infrastructure.Persistence
{
    internal class Repositories
    {
        /*
         * 
         * public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
{
    private readonly ETicaretAPIDbContext _context;

    // Yapıcı metot, bağımlılıkları enjekte eder ve DbContext'i alır.
    public ReadRepository(ETicaretAPIDbContext context)
    {
        _context = context;
    }

    // DbSet'i temsil eden özellik
    public DbSet<T> Table => _context.Set<T>();

    // Tüm varlıkları almak için kullanılır. İzleme ayarını belirler.
    public IQueryable<T> GetAll(bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return query;
    }

    // Belirli bir koşula uyan varlıkları almak için kullanılır.
    public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
    {
        var query = Table.Where(method);
        if (!tracking)
            query = query.AsNoTracking();
        return query;
    }

    // Tek bir varlık almak için kullanılır.
    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return await query.FirstOrDefaultAsync(method);
    }

    // Belirli bir kimliğe sahip varlığı almak için kullanılır.
    public async Task<T> GetByIdAsync(string id, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return await query.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
    }
}

         * 
         * public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
{
    private readonly ETicaretAPIDbContext _context;

    // Yapıcı metot, bağımlılıkları enjekte eder ve DbContext'i alır.
    public WriteRepository(ETicaretAPIDbContext context)
    {
        _context = context;
    }

    // DbSet'i temsil eden özellik
    public DbSet<T> Table => _context.Set<T>();

    // Yeni bir varlık ekler ve işlem başarılıysa true döner.
    public async Task<bool> AddAsync(T model)
    {
        EntityEntry<T> entityEntry = await Table.AddAsync(model);
        return entityEntry.State == EntityState.Added;
    }

    // Bir koleksiyon varlık ekler ve işlem başarılıysa true döner.
    public async Task<bool> AddRangeAsync(List<T> datas)
    {
        await Table.AddRangeAsync(datas);
        return true;
    }

    // Bir varlığı kaldırır ve işlem başarılıysa true döner.
    public bool Remove(T model)
    {
        EntityEntry<T> entityEntry = Table.Remove(model);
        return entityEntry.State == EntityState.Deleted;
    }

    // Bir varlığı kaldırır ve işlem başarılıysa true döner.
    public async Task<bool> RemoveAsync(string id)
    {
        T model = await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
        return Remove(model);
    }

    // Bir koleksiyon varlığı kaldırır ve işlem başarılıysa true döner.
    public bool RemoveRange(List<T> datas)
    {
        Table.RemoveRange(datas);
        return true;
    }

    // Bir varlığı günceller ve işlem başarılıysa true döner.
    public bool Update(T model)
    {
        EntityEntry entityEntry = Table.Update(model);
        return entityEntry.State == EntityState.Modified;
    }

    // Yapılan değişiklikleri kaydeder ve kaydetme işlemi sonucunda etkilenen satır sayısını döndürür.
    public async Task<int> SaveAsync()
        => await _context.SaveChangesAsync();
}

         * 
         * 
         */
    }
}
