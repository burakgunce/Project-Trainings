using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForumTrainings.WebApp.Components
{
    internal class EntryFooterRate
    {
    }

    @using BlazorSozluk.WebApp.Infrastructure.Models
@using Blazored.LocalStorage


<div class="container">
    <span>
        <!-- Yukarı oy verme simgesi -->
        <a class="p-1" title="Up vote!">
            <!-- Oy verme simgesi, o anki oy durumuna göre renk değiştirir -->
            <span class="oi oi-arrow-top entry-vote @(Vote == VoteType.UpVote ? "text-success":"text-secondary")"
            @onclick="(() => UpClicked())"> </span>
        </a>

        <!-- Aşağı oy verme simgesi -->
        <a class="p-3" title="Down vote!">
            <!-- Oy verme simgesi, o anki oy durumuna göre renk değiştirir -->
            <span class="oi oi-arrow-bottom entry-vote @(Vote == VoteType.DownVote ? "text-danger":"text-secondary")"
            @onclick="(() => DownClicked())"> </span>
        </a>

        <!-- Favori ekleme/çıkarma bileşeni -->
        <EntryFooterFavoriteComponent EntryId = "@EntryId"
                                      FavoritedCount="@FavoritedCount"
                                      IsFavorited="@IsFavorited"
                                      OnFavClicked="@FavoritedClicked" />
    </span>
</div>

@code {

    [Inject]
    ISyncLocalStorageService localStorage { get; set; }

    [Parameter]
    public Common.ViewModels.VoteType Vote { get; set; }

    [Parameter]
    public EventCallback<FavClickedEventArgs> OnFavClicked { get; set; }

    [Parameter]
    public EventCallback<VoteClickedEventArgs> OnVoteClicked { get; set; }

    [Parameter]
    public bool IsFavorited { get; set; }

    [Parameter]
    public Guid? EntryId { get; set; }

    [Parameter]
    public int FavoritedCount { get; set; } = 0;

    // Favori ekleme/çıkarma işlemini gerçekleştirir
    private async Task FavoritedClicked()
    {
        var ea = new FavClickedEventArgs();

        // Favori durumunu tersine çevirir
        ea.EntryId = EntryId.Value;
        ea.IsFaved = !IsFavorited;

        // Favori durumunu ana bileşene iletmek için geri çağrıyı tetikler
        await OnFavClicked.InvokeAsync(ea);

        // Değişiklikleri görsel olarak güncellemek için bileşenin durumunu yeniler
        StateHasChanged();
    }

    // Aşağı oy verme işlemini gerçekleştirir
    private async Task DownClicked()
    {
        var ea = new VoteClickedEventArgs();

        ea.EntryId = EntryId.Value;
        ea.DownVoteDeleted = Vote == VoteType.DownVote;
        ea.IsDownVoteClicked = true;

        // Oy verme işlemini ana bileşene iletmek için geri çağrıyı tetikler
        await OnVoteClicked.InvokeAsync(ea);

        // Değişiklikleri görsel olarak güncellemek için bileşenin durumunu yeniler
        StateHasChanged();
    }

    // Yukarı oy verme işlemini gerçekleştirir
    private async Task UpClicked()
    {
        var ea = new VoteClickedEventArgs();

        ea.EntryId = EntryId.Value;
        ea.UpVoteDeleted = Vote == VoteType.UpVote;
        ea.IsUpVoteClicked = true;

        // Oy verme işlemini ana bileşene iletmek için geri çağrıyı tetikler
        await OnVoteClicked.InvokeAsync(ea);

        // Değişiklikleri görsel olarak güncellemek için bileşenin durumunu yeniler
        StateHasChanged();
    }
}

}
