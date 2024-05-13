using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Core.Application.Services
{
    internal class UserService
    {
        /*
         * public class UserService : IUserService
{
    readonly UserManager<Domain.Entities.Identity.AppUser> _userManager; // Kullanıcı yöneticisi
    readonly IEndpointReadRepository _endpointReadRepository; // Uç nokta okuma deposu

    // Constructor: Kullanıcı yöneticisi ve uç nokta okuma deposu enjekte edilir
    public UserService(UserManager<AppUser> userManager, IEndpointReadRepository endpointReadRepository)
    {
        _userManager = userManager;
        _endpointReadRepository = endpointReadRepository;
    }

    // Yeni bir kullanıcı oluşturur
    public async Task<CreateUserResponse> CreateAsync(CreateUser model)
    {
        // Kullanıcıyı oluşturur
        IdentityResult result = await _userManager.CreateAsync(new()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = model.Username,
            Email = model.Email,
            NameSurname = model.NameSurname
        }, model.Password);

        // Oluşturma işleminin sonucunu ve mesajını döndürür
        CreateUserResponse response = new() { Succeeded = result.Succeeded };

        if (result.Succeeded)
            response.Message = "User has been created successfully";
        else
            foreach (var error in result.Errors)
                response.Message += $"{error.Code} - {error.Description}\n";

        return response;
    }

    // Kullanıcının parola sıfırlama işlemini gerçekleştirir
    public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
    {
        // Kullanıcıyı kimlik bilgisine göre bulur
        AppUser user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            // Parolayı sıfırlar
            resetToken = resetToken.UrlDecode();
            IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
            if (result.Succeeded)
                await _userManager.UpdateSecurityStampAsync(user);
            else
                throw new PasswordChangeFailedException();
        }
    }

    // Kullanıcıların belirli bir sayfa ve boyutta listesini alır
    public async Task<List<ListUser>> GetAllUsersAsync(int page, int size)
    {
        // Kullanıcıları belirli bir sayfa ve boyutta alır
        var users = await _userManager.Users
              .Skip(page * size)
              .Take(size)
              .ToListAsync();

        // Kullanıcıları belirli bir modele dönüştürür ve döndürür
        return users.Select(user => new ListUser
        {
            Id = user.Id,
            Email = user.Email,
            NameSurname = user.NameSurname,
            TwoFactorEnabled = user.TwoFactorEnabled,
            UserName = user.UserName

        }).ToList();
    }

    // Kullanıcıya rol atar
    public async Task AssignRoleToUserAsnyc(string userId, string[] roles)
    {
        // Kullanıcıyı kimlik bilgisine göre bulur
        AppUser user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            // Kullanıcının mevcut rollerini kaldırır ve yeni rolleri ekler
            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles);

            await _userManager.AddToRolesAsync(user, roles);
        }
    }

    // Kullanıcının rollerini alır
    public async Task<string[]> GetRolesToUserAsync(string userIdOrName)
    {
        // Kullanıcıyı kimlik bilgisine göre veya kullanıcı adına göre bulur
        AppUser user = await _userManager.FindByIdAsync(userIdOrName);
        if (user == null)
            user = await _userManager.FindByNameAsync(userIdOrName);

        // Kullanıcının rollerini döndürür
        if (user != null)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            return userRoles.ToArray();
        }
        return new string[] { };
    }

    // Kullanıcının belirli bir uç noktaya rol izni olup olmadığını kontrol eder
    public async Task<bool> HasRolePermissionToEndpointAsync(string name, string code)
    {
        // Kullanıcının rollerini alır
        var userRoles = await GetRolesToUserAsync(name);

        if (!userRoles.Any())
            return false;

        // Uç noktayı koduna göre bulur
        Endpoint? endpoint = await _endpointReadRepository.Table
                 .Include(e => e.Roles)
                 .FirstOrDefaultAsync(e => e.Code == code);

        // Uç nokta var mı kontrol eder
        if (endpoint == null)
            return false;

        // Kullanıcı rolleri ve uç nokta rolleri arasında eşleşme var mı kontrol eder
        foreach (var userRole in userRoles)
        {
            foreach (var endpointRole in endpoint.Roles.Select(r => r.Name))
                if (userRole == endpointRole)
                    return true;
        }

        return false;
    }
}

         * 
         * 
         * 
         */
    }
}
