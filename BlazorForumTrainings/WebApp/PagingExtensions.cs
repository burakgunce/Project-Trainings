using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForumTrainings.WebApp
{
    public static class PagingExtensions
    {
        // IQueryable tipindeki sorguları sayfalama işlevselliğiyle genişleten metot
        public static async Task<PagedViewModel<T>> GetPaged<T>(this IQueryable<T> query, int currentPage,
                                                                int pageSize) where T : class
        {
            // Sorgudaki toplam öğe sayısını al
            var count = await query.CountAsync();
            // Sayfalama bilgisini oluştur
            Page paging = new Page(currentPage, pageSize, count);
            // Sayfalanmış veriyi al
            var data = await query.Skip(paging.Skip).Take(paging.PageSize).AsNoTracking().ToListAsync();
            // Sonucu PagedViewModel içinde paketle ve döndür
            var result = new PagedViewModel<T>(data, paging);
            return result;
        }
    }

}
