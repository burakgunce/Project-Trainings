using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForumTrainings.Common.Infrastructure
{
    public class EntryService : IEntryService
    {
        private readonly HttpClient _client;

        public EntryService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<GetEntriesViewModel>> GetEntires()
        {
            // Belirli bir API endpoint'inden veri alır ve GetEntriesViewModel türünde bir listeye dönüştürür
            var result = await _client.GetFromJsonAsync<List<GetEntriesViewModel>>("/api/entry?todaysEnties=false&count=30");
            return result;
        }

        public async Task<GetEntryDetailViewModel> GetEntryDetail(Guid entryId)
        {
            // Belirli bir girdi (entry) için ayrıntıları alır
            var result = await _client.GetFromJsonAsync<GetEntryDetailViewModel>($"/api/entry/{entryId}");
            return result;
        }

        public async Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int page, int pageSize)
        {
            // Ana sayfa girdilerini belirli sayfa numarası ve boyutuyla getirir
            var result = await _client.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"/api/entry/mainpageentries?page={page}&pageSize={pageSize}");
            return result;
        }

        public async Task<PagedViewModel<GetEntryDetailViewModel>> GetProfilePageEntries(int page, int pageSize, string userName = null)
        {
            // Kullanıcının profili sayfasındaki girdileri belirli sayfa numarası ve boyutuyla getirir
            var result = await _client.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"/api/entry/UserEntries?userName={userName}&page={page}&pageSize={pageSize}");
            return result;
        }

        public async Task<PagedViewModel<GetEntryCommentsViewModel>> GetEntryComments(Guid entryId, int page, int pageSize)
        {
            // Bir girdiye ait yorumları belirli sayfa numarası ve boyutuyla getirir
            var result = await _client.GetFromJsonAsync<PagedViewModel<GetEntryCommentsViewModel>>($"/api/entry/comments/{entryId}?page={page}&pageSize={pageSize}");
            return result;
        }

        public async Task<Guid> CreateEntry(CreateEntryCommand command)
        {
            // Yeni bir girdi oluşturur
            var res = await _client.PostAsJsonAsync("/api/Entry/CreateEntry", command);
            if (!res.IsSuccessStatusCode)
                return Guid.Empty;
            var guidStr = await res.Content.ReadAsStringAsync();
            return new Guid(guidStr.Trim('"'));
        }

        public async Task<Guid> CreateEntryComment(CreateEntryCommentCommand command)
        {
            // Yeni bir girdi yorumu oluşturur
            var res = await _client.PostAsJsonAsync("/api/Entry/CreateEntryComment", command);
            if (!res.IsSuccessStatusCode)
                return Guid.Empty;
            var guidStr = await res.Content.ReadAsStringAsync();
            return new Guid(guidStr.Trim('"'));
        }

        public async Task<List<SearchEntryViewModel>> SearchBySubject(string searchText)
        {
            // Belirli bir metni arar ve sonuçları döndürür
            var result = await _client.GetFromJsonAsync<List<SearchEntryViewModel>>($"/api/entry/Search?searchText={searchText}");
            return result;
        }
    }

}
