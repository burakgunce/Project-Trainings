using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForumTrainings.WebApi
{
    public static class PagingExtensions
    {
        // IQueryable veri kümesini sayfalı bir şekilde döndüren genişletme yöntemi.
        public static async Task<PagedViewModel<T>> GetPaged<T>(this IQueryable<T> query, int currentPage, int pageSize) where T : class
        {
            // Veri kümesinin toplam öğe sayısını alır.
            var count = await query.CountAsync();

            // Sayfalama bilgilerini içeren bir Page nesnesi oluşturulur.
            Page paging = new(currentPage, pageSize, count);

            // Sayfalı veri kümesi, belirli sayfa ve sayfa boyutuna göre alınır.
            var data = await query.Skip(paging.Skip).Take(paging.PageSize).AsNoTracking().ToListAsync();

            // Sayfalı veri kümesi ve sayfalama bilgileriyle bir PagedViewModel oluşturulur ve döndürülür.
            var result = new PagedViewModel<T>(data, paging);
            return result;
        }
    }

}
