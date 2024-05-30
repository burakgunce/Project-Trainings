using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Core.Application
{
    internal class AssingRoleToUserCQRS
    {
        /*
         * // Bir kullanıcıya rol atama işlemi için istek sınıfı.
// IRequest arayüzünü uygular ve yanıt tipi olarak AssignRoleToUserCommandResponse kullanılır.
public class AssignRoleToUserCommandRequest : IRequest<AssignRoleToUserCommandResponse>
{
    // Kullanıcının kimliği.
    public string UserId { get; set; }
    // Kullanıcıya atanacak roller.
    public string[] Roles { get; set; }
}


        // Bir kullanıcıya rol atama işleminin yanıt sınıfı.
// Şu an için boş, ama gelecekte ek bilgiler eklenebilir.
public class AssignRoleToUserCommandResponse
{
}



        // AssignRoleToUserCommandRequest isteğini işleyen sınıf.
// IRequestHandler arayüzünü uygular ve request/response tiplerini belirtir.
public class AssignRoleToUserCommandHandler : IRequestHandler<AssignRoleToUserCommandRequest, AssignRoleToUserCommandResponse>
{
    // Kullanıcı servisinin sadece okunabilir bir referansı.
    readonly IUserService _userService;

    // Kurucu metod, bağımlılık olarak IUserService alır ve sınıfın alanına atar.
    public AssignRoleToUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    // Handle metodu, AssignRoleToUserCommandRequest isteğini işleyip yanıt döner.
    public async Task<AssignRoleToUserCommandResponse> Handle(AssignRoleToUserCommandRequest request, CancellationToken cancellationToken)
    {
        // Kullanıcıya verilen rolleri atar.
        await _userService.AssignRoleToUserAsnyc(request.UserId, request.Roles);
        // Boş bir yanıt döner.
        return new AssignRoleToUserCommandResponse();
    }
}

         * 
         * 
         * 
         */
    }
}
