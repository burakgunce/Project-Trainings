using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForumTrainings.WebApp.Components
{
    internal class EntryFooterFav
    {
    }

    @using BlazorSozluk.WebApp.Infrastructure.Models
    @if (identityService.IsLoggedIn)
    {
        < span class="entry-fav @(IsFavorited ? "entry-faved" : "entry-not-faved") p-3">
            <span @onclick = "( () => FavoritedClicked())" >
                @FavoritedCount
            </ span >
        </ span >
    }

    @code {
        [Inject]
    IIdentityService identityService { get; set; } // Kimlik hizmetini enjekte eder.

    [Parameter]
    public bool IsFavorited { get; set; } // Favori durumunu alır.

    [Parameter]
    public Guid? EntryId { get; set; } // Girişin kimliğini alır.

    [Parameter]
    public int FavoritedCount { get; set; } // Favorilendirilme sayısını alır.

    [Parameter]
    public EventCallback<FavClickedEventArgs> OnFavClicked { get; set; } // Favorilendirme olayını işlemek için geri çağrıyı alır.

    private async Task FavoritedClicked()
    {
        IsFavorited = !IsFavorited; // Favorilendirme durumunu tersine çevirir.

        var eventArgs = new FavClickedEventArgs();
        eventArgs.EntryId = EntryId;
        eventArgs.IsFaved = IsFavorited;

        await OnFavClicked.InvokeAsync(eventArgs); // Favorilendirme olayını tetikler.

        StateHasChanged(); // Bileşenin durumunu günceller.
    }
    }

}
