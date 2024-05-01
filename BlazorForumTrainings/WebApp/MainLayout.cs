using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForumTrainings.WebApp
{
    internal class MainLayout
    {
        /*
         * 
         * 
         * 
         * 
         */
    }
    @code {
    // Bileşen içerisinde kullanılacak değişkenlerin tanımlanması

    private EntriesComponent entriesComponent; // Girişleri listeleyen bileşene referans sağlar.
    public string EntriesSubject { get; set; } = "bugün"; // Kullanıcının seçtiği giriş konusunu tutar. Başlangıçta "bugün" olarak ayarlanmıştır.
    private string searchText; // Kullanıcının arama kutusuna girdiği metni tutar.
    private bool showSearchResult; // Arama sonuçlarının görünürlüğünü kontrol eder.
    private List<SearchEntryViewModel> searchResults = new List<SearchEntryViewModel>(); // Arama sonuçlarını tutar.

    [Inject]
    NavigationManager navigationManager { get; set; } // Sayfa yönlendirmeleri yapmak için kullanılır.

    [Inject]
    IEntryService entryService { get; set; } // Girişlerle ilgili iş mantığını sağlayan servis.

    // Girişleri yenileme işlemini gerçekleştirir.
    private async Task RefreshEntries()
    {
        await entriesComponent.RefreshList();
    }

    // "ME" sayfasına yönlendirme işlemi.
    private void NavigateMEPage()
    {
        navigationManager.NavigateTo("/me");
    }

    // "Login" sayfasına yönlendirme işlemi.
    private void NavigateLoginPage()
    {
        navigationManager.NavigateTo("/login");
    }

    // "Logout" sayfasına yönlendirme işlemi.
    private void NavigateLogoutPage()
    {
        navigationManager.NavigateTo("/logout");
    }

    // "CreateEntry" sayfasına yönlendirme işlemi.
    private void NavigateCreateEntryPage()
    {
        navigationManager.NavigateTo("/createEntry");
    }

    // Klavyeden Enter tuşuna basılınca veya Enter tuşuna basılınca arama işlemini gerçekleştirir.
    private async Task SearchKeyDown(KeyboardEventArgs e)
    {
        if (e.Code == "Enter" || e.Code == "NumpadEnter")
        {
            await Search();
        }
    }

    // Arama odak kaybı durumunda arama sonuçlarını gizler.
    private async Task OnSearchFocusOut()
    {
        await Task.Delay(100);
        if (showSearchResult)
            showSearchResult = false;
    }

    // Arama işlemini gerçekleştirir.
    private async Task Search()
    {
        if (string.IsNullOrEmpty(searchText))
            return;

        searchResults = await entryService.SearchBySubject(searchText);

        showSearchResult = true;

        searchText = string.Empty;
    }

    // Belirli bir girişe yönlendirme işlemi gerçekleştirir.
    private async Task GoEntry(Guid entryId)
    {
        await OnSearchFocusOut();
        navigationManager.NavigateTo($"/entry/{entryId}");
    }
}

}
