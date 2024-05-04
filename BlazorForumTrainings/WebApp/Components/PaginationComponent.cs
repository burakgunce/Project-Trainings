using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForumTrainings.WebApp.Components
{
    internal class PaginationComponent
    {
    }

    @if(TotalPage > 0 && CurrentPage > 0)
    {
    < !--Sayfalama düğmelerini oluşturmak için bir kontrol-- >
    < div class="d-flex flex-row-reverse">
        <div class="p-2">
            <!-- Sayfalama seçeneklerinin bulunduğu bir açılır menü -->
            <select class="form-select text-secondary" @onchange="Changed">
                <!-- Her bir sayfa numarası için bir seçenek oluştur -->
                @for(int i = 1; i <= TotalPage; i++)
                {
                    <!-- Geçerli sayfa numarasına göre seçenekleri işaretleme veya işaretsiz bırakma -->
                    @if(CurrentPage == i)
    {
                        < option value = "@i" selected > @i </ option >
                    }
                    else
                    {
                        <option value = "@i" > @i </ option >
                    }
                }
            </ select >
        </ div >
    </ div >
}
    
    @code {
    // Razor bileşeninin parametreleri
    [Parameter]
    public int TotalPage { get; set; }

    [Parameter]
    public int CurrentPage { get; set; }

    [Parameter]
    public EventCallback<int> OnPageChanged { get; set; }

    // Sayfa değişikliklerini işleyen metot
    private async Task Changed(ChangeEventArgs e)
    {
        // Seçilen sayfa numarasını al ve OnPageChanged geri çağrısına ileterek sayfa değişikliğini tetikle
        await OnPageChanged.InvokeAsync(int.Parse(e.Value.ToString()));
    }
}


}
