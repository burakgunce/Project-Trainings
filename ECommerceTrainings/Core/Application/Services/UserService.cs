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
         *
         *public class UserService : IUserService
{
    // UserManager ve IEndpointReadRepository bağımlılıklarını enjekte ediyoruz.
    readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
    readonly IEndpointReadRepository _endpointReadRepository;

    public UserService(UserManager<AppUser> userManager, IEndpointReadRepository endpointReadRepository)
    {
        // Bağımlılıkları sınıf içindeki private alanlara atıyoruz.
        _userManager = userManager;
        _endpointReadRepository = endpointReadRepository;
    }

    public async Task<CreateUserResponse> CreateAsync(CreateUser model)
    {
        // Yeni bir kullanıcı oluşturmak için UserManager'ı kullanıyoruz.
        IdentityResult result = await _userManager.CreateAsync(new()
        {
            // Yeni kullanıcının bilgilerini model nesnesinden alıyoruz.
            Id = Guid.NewGuid().ToString(),
            UserName = model.Username,
            Email = model.Email,
            NameSurname = model.NameSurname
        }, model.Password);

        // Oluşturma işleminin sonucuna göre bir yanıt oluşturuyoruz.
        CreateUserResponse response = new() { Succeeded = result.Succeeded };

        // Oluşturma başarılıysa bir mesaj atıyoruz, aksi halde hata mesajlarını ekliyoruz.
        if (result.Succeeded)
            response.Message = "User has been created successfully";
        else
            foreach (var error in result.Errors)
                response.Message += $"{error.Code} - {error.Description}\n";

        return response;
    }

    public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
    {
        if (user != null)
        {
            // Kullanıcının yenileme belirtecini güncelliyoruz.
            user.RefreshToken = refreshToken;
            user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
            await _userManager.UpdateAsync(user);
        }
        // Kullanıcı bulunamazsa bir hata fırlatıyoruz.
        throw new NotFoundUserException();
    }

    public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
    {
        // Kullanıcıyı kullanıcı kimliğine göre buluyoruz.
        AppUser user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            // Şifreyi sıfırlıyoruz.
            resetToken = resetToken.UrlDecode();
            IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
            // Şifre sıfırlama işlemi başarısız olursa bir hata fırlatıyoruz.
            if (result.Succeeded)
                await _userManager.UpdateSecurityStampAsync(user);
            else
                throw new PasswordChangeFailedException();
        }
    }

    public async Task<List<ListUser>> GetAllUsersAsync(int page, int size)
    {
        // Tüm kullanıcıları alıyoruz ve sayfalama yapıyoruz.
        var users = await _userManager.Users
              .Skip(page * size)
              .Take(size)
              .ToListAsync();

        return users.Select(user => new ListUser
        {
            Id = user.Id,
            Email = user.Email,
            NameSurname = user.NameSurname,
            TwoFactorEnabled = user.TwoFactorEnabled,
            UserName = user.UserName

        }).ToList();
    }

    public int TotalUsersCount => _userManager.Users.Count();

    public async Task AssignRoleToUserAsnyc(string userId, string[] roles)
    {
        // Kullanıcıya roller atıyoruz.
        AppUser user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles);

            await _userManager.AddToRolesAsync(user, roles);
        }
    }

    public async Task<string[]> GetRolesToUserAsync(string userIdOrName)
    {
        // Kullanıcıya ait rolleri alıyoruz.
        AppUser user = await _userManager.FindByIdAsync(userIdOrName);
        if (user == null)
            user = await _userManager.FindByNameAsync(userIdOrName);

        if (user != null)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            return userRoles.ToArray();
        }
        return new string[] { };
    }

    public async Task<bool> HasRolePermissionToEndpointAsync(string name, string code)
    {
        // Bir kullanıcının belirli bir uç noktaya erişim izni olup olmadığını kontrol ediyoruz.
        var userRoles = await GetRolesToUserAsync(name);

        if (!userRoles.Any())
            return false;

        Endpoint? endpoint = await _endpointReadRepository.Table
                 .Include(e => e.Roles)
                 .FirstOrDefaultAsync(e => e.Code == code);

        if (endpoint == null)
            return false;

        var hasRole = false;
        var endpointRoles = endpoint.Roles.Select(r => r.Name);

        foreach (var userRole in userRoles)
        {
            foreach (var endpointRole in endpointRoles)
                if (userRole == endpointRole)
                    return true;
        }

        return false;
    }
}

         * 
         * 
         */
    }
}
