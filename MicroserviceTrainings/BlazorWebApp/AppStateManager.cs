using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTrainings.BlazorWebApp
{
    public class AppStateManager
    {
        public event Action<ComponentBase, string> StateChanged; // Uygulama durumu değiştiğinde tetiklenecek olay

        // Sepeti güncelleme işlemi için bir metod.
        public void UpdateCart(ComponentBase component)
        {
            // StateChanged olayını tetikler ve "updatebasket" işlemi olduğunu bildirir.
            StateChanged?.Invoke(component, "updatebasket");
        }

        // Giriş durumunu değiştirme işlemi için bir metod.
        public void LoginChanged(ComponentBase component)
        {
            // StateChanged olayını tetikler ve "login" işlemi olduğunu bildirir.
            StateChanged?.Invoke(component, "login");
        }
    }

}
