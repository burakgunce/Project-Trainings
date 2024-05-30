using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Core.Application.Services
{
    internal class AuthorizationEndpointService
    {
        /*
         * // AuthorizationEndpointService sınıfı, IAuthorizationEndpointService arayüzünü uygular.
// Bu sınıf, yetkilendirme noktaları ile ilgili işlemleri gerçekleştirir.
public class AuthorizationEndpointService : IAuthorizationEndpointService
{
    // Bağımlılık olarak kullanılan servisler ve depoların tanımlanması.
    readonly IApplicationService _applicationService;
    readonly IEndpointReadRepository _endpointReadRepository;
    readonly IEndpointWriteRepository _endpointWriteRepository;
    readonly IMenuReadRepository _menuReadRepository;
    readonly IMenuWriteRepository _menuWriteRepository;
    readonly RoleManager<AppRole> _roleManager;

    // Kurucu metod, bağımlılıkları dependency injection yoluyla alır ve sınıfın alanlarına atar.
    public AuthorizationEndpointService(
        IApplicationService applicationService, 
        IEndpointReadRepository endpointReadRepository, 
        IEndpointWriteRepository endpointWriteRepository, 
        IMenuReadRepository menuReadRepository, 
        IMenuWriteRepository menuWriteRepository, 
        RoleManager<AppRole> roleManager)
    {
        _applicationService = applicationService;
        _endpointReadRepository = endpointReadRepository;
        _endpointWriteRepository = endpointWriteRepository;
        _menuReadRepository = menuReadRepository;
        _menuWriteRepository = menuWriteRepository;
        _roleManager = roleManager;
    }

    // Rolleri bir endpoint'e atayan metod.
    public async Task AssignRoleEndpointAsync(string[] roles, string menu, string code, Type type)
    {
        // Menü adıyla menüyü arar, bulamazsa yeni bir menü oluşturur ve kaydeder.
        Menu _menu = await _menuReadRepository.GetSingleAsync(m => m.Name == menu);
        if (_menu == null)
        {
            _menu = new Menu
            {
                Id = Guid.NewGuid(),
                Name = menu
            };
            await _menuWriteRepository.AddAsync(_menu);
            await _menuWriteRepository.SaveAsync();
        }

        // Endpoint'i menü adı ve koduyla arar.
        Endpoint? endpoint = await _endpointReadRepository.Table
            .Include(e => e.Menu)
            .Include(e => e.Roles)
            .FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);

        // Endpoint bulunamazsa, yeni bir endpoint oluşturur ve kaydeder.
        if (endpoint == null)
        {
            var action = _applicationService.GetAuthorizeDefinitionEndpoints(type)
                .FirstOrDefault(m => m.Name == menu)
                ?.Actions.FirstOrDefault(e => e.Code == code);

            endpoint = new Endpoint
            {
                Code = action.Code,
                ActionType = action.ActionType,
                HttpType = action.HttpType,
                Definition = action.Definition,
                Id = Guid.NewGuid(),
                Menu = _menu
            };

            await _endpointWriteRepository.AddAsync(endpoint);
            await _endpointWriteRepository.SaveAsync();
        }

        // Endpoint'e bağlı mevcut rolleri temizler.
        foreach (var role in endpoint.Roles.ToList())
            endpoint.Roles.Remove(role);

        // Verilen roller listesine göre rollerin AppRole nesnelerini alır.
        var appRoles = await _roleManager.Roles.Where(r => roles.Contains(r.Name)).ToListAsync();

        // Endpoint'e yeni rolleri ekler.
        foreach (var role in appRoles)
            endpoint.Roles.Add(role);

        // Değişiklikleri kaydeder.
        await _endpointWriteRepository.SaveAsync();
    }

    // Bir endpoint'e atanmış rolleri getiren metod.
    public async Task<List<string>> GetRolesToEndpointAsync(string code, string menu)
    {
        // Endpoint'i menü adı ve koduyla arar.
        Endpoint? endpoint = await _endpointReadRepository.Table
            .Include(e => e.Roles)
            .Include(e => e.Menu)
            .FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);

        // Endpoint bulunursa, rollerin isimlerini döner.
        if (endpoint != null)
            return endpoint.Roles.Select(r => r.Name).ToList();

        // Endpoint bulunamazsa, null döner.
        return null;
    }
}

         * 
         * 
         * 
         */
    }
}
