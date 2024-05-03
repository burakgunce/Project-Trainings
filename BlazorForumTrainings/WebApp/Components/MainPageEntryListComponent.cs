using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForumTrainings.WebApp.Components
{
    internal class MainPageEntryListComponent
    {
    }

    @if(entries != null)
    {
    < !--Girişlerin listesini göster -->
    foreach (var entry in entries.Results)
        {
        < EntryViewerComponent Entry = "@entry" />
        < br />
    }

    < !--Sayfalama bileşenini göster -->
    < br />
    < br />
    < PaginationComponent TotalPage = "@entries.PageInfo.TotalPageCount"
                         CurrentPage = "@currentPage"
                         OnPageChanged = "@OnPageChanged" />
}


    @code {
    [Inject]
    IEntryService entryService { get; set; }

    private int currentPage = 1;

    PagedViewModel<GetEntryDetailViewModel> entries;

    // Sayfa değiştiğinde çağrılan metot
    public async Task OnPageChanged(int pageNumber)
    {
        currentPage = pageNumber;
        entries = await entryService.GetMainPageEntries(currentPage, 5);
    }

    // Bileşen yüklendiğinde çağrılan metot
    protected override async Task OnInitializedAsync()
    {
        // İlk sayfadaki girişleri al
        entries = await entryService.GetMainPageEntries(currentPage, 5);
        //TODO Pagination will be handled
    }
}

}
