using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Presentation.Api
{
    internal class RolePermissonFilter
    {
        /*
         * // Bu sınıf, bir eylemin çalıştırılmadan önce kullanıcı izinlerini kontrol eden bir filtre uygular.
public class RolePermissionFilter : IAsyncActionFilter
{
    // Kullanıcı servisinin bağımlılığını tanımlar.
    readonly IUserService _userService;

    // Kullanıcı servisini bağımlılık olarak alır ve sınıfın bir örneğini oluşturur.
    public RolePermissionFilter(IUserService userService)
    {
        _userService = userService;
    }

    // Asenkron bir eylem filtreleme yöntemi tanımlar. Bu yöntem, eylemin çalıştırılmadan önce ve sonra yürütülmesini sağlar.
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // HTTP isteğindeki kullanıcının adını alır.
        var name = context.HttpContext.User.Identity?.Name;

        // Kullanıcı adı boş değilse ve "gncy" değilse devam eder.
        if (!string.IsNullOrEmpty(name) && name != "gncy")
        {
            // İlgili eylem tanımını alır.
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            // Eylem tanımında AuthorizeDefinitionAttribute özniteliğini alır.
            var attribute = descriptor.MethodInfo.GetCustomAttribute(typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;

            // Eylem tanımında HTTP yöntemi özniteliğini alır.
            var httpAttribute = descriptor.MethodInfo.GetCustomAttribute(typeof(HttpMethodAttribute)) as HttpMethodAttribute;

            // Eylem için bir kod oluşturur (HTTP yöntemi, eylem türü ve tanımına dayanarak).
            var code = $"{(httpAttribute != null ? httpAttribute.HttpMethods.First() : HttpMethods.Get)}.{attribute.ActionType}.{attribute.Definition.Replace(" ", "")}";

            // Kullanıcının bu eylemi gerçekleştirme iznine sahip olup olmadığını kontrol eder.
            var hasRole = await _userService.HasRolePermissionToEndpointAsync(name, code);

            // Kullanıcının izni yoksa yetkisiz sonuç döndürür, aksi halde eylemi çalıştırmaya devam eder.
            if (!hasRole)
                context.Result = new UnauthorizedResult();
            else
                await next();
        }
        else
            // Kullanıcı adı boşsa veya "gncy" ise eylemi çalıştırmaya devam eder.
            await next();
    }
}




        public async Task<bool> HasRolePermissionToEndpointAsync(string name, string code)
{
    // Kullanıcının rollerini asenkron olarak alır.
    var userRoles = await GetRolesToUserAsync(name);

    // Eğer kullanıcıya atanmış herhangi bir rol yoksa false döner.
    if (!userRoles.Any())
        return false;

    // Veritabanından belirtilen kod ile eşleşen endpoint'i ve ilgili rolleri asenkron olarak alır.
    Endpoint? endpoint = await _endpointReadRepository.Table
             .Include(e => e.Roles)
             .FirstOrDefaultAsync(e => e.Code == code);

    // Eğer endpoint bulunamazsa false döner.
    if (endpoint == null)
        return false;

    // Endpoint'in rollerini alır.
    var endpointRoles = endpoint.Roles.Select(r => r.Name);

    // Kullanıcının rollerini endpoint'in rolleri ile karşılaştırır ve eşleşen bir rol bulunursa true döner.
    foreach (var userRole in userRoles)
    {
        foreach (var endpointRole in endpointRoles)
            if (userRole == endpointRole)
                return true;
    }

    // Hiçbir eşleşme bulunamazsa false döner.
    return false;
}

         * 
         * 
         * 
         */
    }
}
