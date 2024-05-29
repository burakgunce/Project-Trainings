using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Core.Application.Services
{
    internal class ApplicationService
    {
        /*
         * Rol based acces control ıcın baslangıc buradan
         * burada endpointlerini cekecek bir mimari kuruyosun daha sonra senın manuel olarak kontrol etmene gerek kalmıyo rol ıcın gereken tum endpoıntler buraya dusuyor
         * 
         * 
         * 
         * // Bu sınıf, bir yöntem veya sınıf için yetkilendirme tanımları eklemek amacıyla kullanılır.
// Attribute sınıfından türetilmiştir ve aşağıdaki özelliklere sahiptir.
public class AuthorizeDefinitionAttribute : Attribute
{
    // Menü adı. Bu özellik, hangi menüye ait olduğunu belirtir.
    public string Menu { get; set; }
    
    // Yetkilendirme tanımının açıklaması.
    public string Definition { get; set; }
    
    // Eylem türü. Enum türünde olup, belirli bir eylemi ifade eder.
    public ActionType ActionType { get; set; }
}





        // Bu sınıf, bir menüyü temsil eder.
// Menü adını ve menüdeki eylemleri (actions) içerir.
public class Menu
{
    // Menünün adı.
    public string Name { get; set; }
    
    // Menünün içindeki eylemlerin listesi.
    public List<Action> Actions { get; set; } = new();
}





        // Bu sınıf, bir eylemi temsil eder.
// Eylem türünü, HTTP türünü, açıklamasını ve kodunu içerir.
public class Action
{
    // Eylem türü. Örneğin: "Create", "Update" vb.
    public string ActionType { get; set; }
    
    // HTTP türü. Örneğin: "GET", "POST" vb.
    public string HttpType { get; set; }
    
    // Eylemin açıklaması.
    public string Definition { get; set; }
    
    // Eylemin benzersiz kodu.
    public string Code { get; set; }
}








        // Bu sınıf, uygulama servislerini temsil eder ve IApplicationService arayüzünü uygular.
public class ApplicationService : IApplicationService
{
    // Bu metod, belirli bir türdeki (type) yetkilendirme tanımlı uç noktalarını (endpoints) alır.
    public List<Menu> GetAuthorizeDefinitionEndpoints(Type type)
    {
        // Verilen türün ait olduğu assembly'i alır.
        Assembly assembly = Assembly.GetAssembly(type);
        
        // Assembly içindeki tüm ControllerBase türünden türeyen tipleri alır.
        var controllers = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ControllerBase)));

        // Menü listesi oluşturur.
        List<Menu> menus = new();
        
        // Eğer controllerlar varsa, her biri için işlem yapar.
        if (controllers != null)
            foreach (var controller in controllers)
            {
                // Controller içindeki yetkilendirme tanımlı (AuthorizeDefinitionAttribute ile işaretlenmiş) metodları alır.
                var actions = controller.GetMethods().Where(m => m.IsDefined(typeof(AuthorizeDefinitionAttribute)));
                
                // Eğer bu metodlar varsa, her biri için işlem yapar.
                if (actions != null)
                    foreach (var action in actions)
                    {
                        // Metodun tüm özniteliklerini (attributes) alır.
                        var attributes = action.GetCustomAttributes(true);
                        
                        // Eğer öznitelikler varsa, işleme devam eder.
                        if (attributes != null)
                        {
                            // Geçici menü değişkeni tanımlar.
                            Menu menu = null;

                            // Öznitelikler arasından AuthorizeDefinitionAttribute türündeki özniteliği bulur.
                            var authorizeDefinitionAttribute = attributes.FirstOrDefault(a => a.GetType() == typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;
                            
                            // Eğer menü listesinde öznitelikte belirtilen menü adı yoksa, yeni bir menü oluşturur ve listeye ekler.
                            if (!menus.Any(m => m.Name == authorizeDefinitionAttribute.Menu))
                            {
                                menu = new() { Name = authorizeDefinitionAttribute.Menu };
                                menus.Add(menu);
                            }
                            else
                                // Menü listesinde mevcut menü varsa, onu bulur.
                                menu = menus.FirstOrDefault(m => m.Name == authorizeDefinitionAttribute.Menu);

                            // Yeni bir eylem (action) nesnesi oluşturur ve öznitelikteki bilgileri eyleme aktarır.
                            Application.DTOs.Configuration.Action _action = new()
                            {
                                ActionType = Enum.GetName(typeof(ActionType), authorizeDefinitionAttribute.ActionType),
                                Definition = authorizeDefinitionAttribute.Definition
                            };

                            // HTTP metodunu belirlemek için öznitelikler arasında HttpMethodAttribute arar.
                            var httpAttribute = attributes.FirstOrDefault(a => a.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as HttpMethodAttribute;
                            
                            // Eğer HttpMethodAttribute varsa, HTTP metodunu belirler.
                            if (httpAttribute != null)
                                _action.HttpType = httpAttribute.HttpMethods.First();
                            else
                                // Eğer yoksa, varsayılan olarak GET metodunu kullanır.
                                _action.HttpType = HttpMethods.Get;

                            // Eylemin benzersiz kodunu oluşturur.
                            _action.Code = $"{_action.HttpType}.{_action.ActionType}.{_action.Definition.Replace(" ", "")}";

                            // Eylemi menüye ekler.
                            menu.Actions.Add(_action);
                        }
                    }
            }

        // Menüleri döner.
        return menus;
    }
}



         * 
         * 
         * 
         * 
         */
    }
}
