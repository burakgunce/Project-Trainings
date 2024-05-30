using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Core.Application.Services
{
    internal class RoleService
    {
        /*
         * // Bu sınıf, rollerle ilgili işlemleri gerçekleştirmek için kullanılan bir servis sınıfıdır.
// IRoleService arayüzünü uygular ve RoleManager<AppRole> sınıfını kullanarak ASP.NET Core Identity framework'ü ile entegrasyon sağlar.
public class RoleService : IRoleService
{
    // RoleManager sınıfı, rollerle ilgili işlemleri yönetmek için kullanılan bir ASP.NET Core Identity sınıfıdır.
    readonly RoleManager<AppRole> _roleManager;

    // Kurucu metod, RoleManager örneğini dependency injection yoluyla alır.
    public RoleService(RoleManager<AppRole> roleManager)
    {
        _roleManager = roleManager;
    }

    // Yeni bir rol oluşturur.
    public async Task<bool> CreateRole(string name)
    {
        // Yeni bir AppRole nesnesi oluşturur ve roleManager kullanarak bu rolü oluşturur.
        IdentityResult result = await _roleManager.CreateAsync(new AppRole { Id = Guid.NewGuid().ToString(), Name = name });
        
        // İşlemin başarılı olup olmadığını döner.
        return result.Succeeded;
    }

    // Mevcut bir rolü siler.
    public async Task<bool> DeleteRole(string id)
    {
        // Verilen ID'ye sahip rolü roleManager kullanarak bulur.
        AppRole appRole = await _roleManager.FindByIdAsync(id);
        
        // Bulunan rolü roleManager kullanarak siler.
        IdentityResult result = await _roleManager.DeleteAsync(appRole);
        
        // İşlemin başarılı olup olmadığını döner.
        return result.Succeeded;
    }

    // Tüm rolleri sayfalama ile birlikte getirir.
    public (object, int) GetAllRoles(int page, int size)
    {
        // Tüm rolleri sorgulamak için RoleManager'ın Roles özelliğini kullanır.
        var query = _roleManager.Roles;

        IQueryable<AppRole> rolesQuery = null;

        // Sayfalama parametreleri verilmişse, verilen sayfa ve boyuta göre rolleri alır.
        if (page != -1 && size != -1)
            rolesQuery = query.Skip(page * size).Take(size);
        else
            // Sayfalama parametreleri verilmemişse, tüm rolleri alır.
            rolesQuery = query;

        // Rolleri ve toplam rol sayısını döner.
        return (rolesQuery.Select(r => new { r.Id, r.Name }), query.Count());
    }

    // Belirli bir ID'ye sahip rolü getirir.
    public async Task<(string id, string name)> GetRoleById(string id)
    {
        // Verilen ID'ye sahip rolün adını roleManager kullanarak alır.
        string role = await _roleManager.GetRoleIdAsync(new AppRole { Id = id });
        
        // Rolün ID ve adını döner.
        return (id, role);
    }

    // Mevcut bir rolü günceller.
    public async Task<bool> UpdateRole(string id, string name)
    {
        // Verilen ID'ye sahip rolü roleManager kullanarak bulur.
        AppRole role = await _roleManager.FindByIdAsync(id);
        
        // Rolün adını günceller.
        role.Name = name;
        
        // Güncellenmiş rolü roleManager kullanarak kaydeder.
        IdentityResult result = await _roleManager.UpdateAsync(role);
        
        // İşlemin başarılı olup olmadığını döner.
        return result.Succeeded;
    }
}

         * 
         * 
         * 
         */
    }
}
