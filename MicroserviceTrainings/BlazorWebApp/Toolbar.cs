using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTrainings.BlazorWebApp
{
    internal class Toolbar
    {
        /*
         * 
         * @using WebApp.Infrastructure; // Altyapıya erişmek için gerekli using direktifi

            @if (isLoggedIn)
            {
                <!-- Kullanıcı giriş yapmışsa, sepet sayısını ve kullanıcı adını gösteren bir link görüntülenir -->
                <a href="basket" class="ml-md-auto"> @identityService.GetUserName() - Basket (@basketItemCount) </a>
                <!-- Kullanıcı giriş yapmışsa, logout sayfasına yönlendiren bir link görüntülenir -->
                <a class="ml-md-auto" @onclick=@GoLogoutPage>Logout</a>
            }
            else
            {
                <!-- Kullanıcı giriş yapmamışsa, login sayfasına yönlendiren bir link görüntülenir -->
                <a class="ml-md-auto" @onclick=@GoLoginPage>Login</a>
            }


            @code // Razor bileşeninin kod bloğu
            {
                [Inject]
                IBasketService basketService { get; set; } // Servis bağımlılıkları

                [Inject]
                IIdentityService identityService { get; set; }

                [Inject]
                AppStateManager appState { get; set; }

                [Inject]
                NavigationManager navigationManager { get; set; }

                private int basketItemCount = 0; // Sepet öğe sayısı
                bool isLoggedIn = false; // Kullanıcının oturum durumu

                // Razor bileşeni ilk defa render edildikten sonra yapılacak işlemler
                protected override void OnAfterRender(bool firstRender)
                {
                    if (firstRender)
                        // Uygulama durumu değiştiğinde tetiklenecek olan olaya abone olunur
                        appState.StateChanged += async (source, property) => await AppState_StateChanged(source, property);
                }

                // Razor bileşeni ilk defa initialize edildikten sonra yapılacak işlemler
                protected async override Task OnInitializedAsync()
                {
                    await calculateItemCount(); // Sepet öğe sayısını hesaplamak için bir metod çağrılır
                }

                // Sepet öğe sayısını hesaplamak için bir metod
                private async Task<int> calculateItemCount()
                {
                    isLoggedIn = identityService.IsLoggedIn; // Kullanıcının oturum durumu kontrol edilir

                    if (!isLoggedIn)
                        return 0;

                    // Kullanıcı oturum açmışsa, sepet öğe sayısı hesaplanır
                    var basket = await basketService.GetBasket();
                    var count = basket.Items == null ? 0 : basket.Items.Sum(i => i.Quantity);

                    basketItemCount = count; // Hesaplanan öğe sayısı değişkene atanır

                    return count; // Hesaplanan öğe sayısı döndürülür
                }

                // Uygulama durumu değiştiğinde tetiklenecek olan olayı işleyen bir metod
                private async Task AppState_StateChanged(ComponentBase source, string property)
                {
                    if (source == this)
                        return;

                    // Gelen property'e göre işlem yapılır
                    if (property == "increase")
                    {
                        basketItemCount++; // Sepet öğe sayısı arttırılır
                    }
                    else if (property == "updatebasket" || property == "login")
                    {
                        await calculateItemCount(); // Sepet öğe sayısı güncellenir veya kullanıcı oturum açmışsa hesaplanır
                    }

                    await InvokeAsync(StateHasChanged); // Durumun güncellendiğini bildirir
                }

                // Login sayfasına yönlendiren bir metod
                private void GoLoginPage()
                {
                    navigationManager.NavigateTo($"login?returnUrl={Uri.EscapeDataString(navigationManager.Uri)}", false);
                }

                // Logout sayfasına yönlendiren bir metod
                private void GoLogoutPage()
                {
                    navigationManager.NavigateTo($"logout?returnUrl={Uri.EscapeDataString(navigationManager.Uri)}", false);
                }
            }


         * 
         */
    }
}
