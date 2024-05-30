using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Core.Application
{
    internal class AssignRoleEndpointCQRS
    {
        /*
         * // Endpoint'e rol atama isteği için kullanılan sınıf.
// IRequest arayüzünü uygular ve yanıt tipi olarak AssignRoleEndpointCommandResponse kullanılır.
public class AssignRoleEndpointCommandRequest : IRequest<AssignRoleEndpointCommandResponse>
{
    // Atanacak rollerin dizisi.
    public string[] Roles { get; set; }
    // Endpoint'in kodu.
    public string Code { get; set; }
    // Menü adı.
    public string Menu { get; set; }
    // Endpoint'in tipi. Opsiyonel (nullable) olarak tanımlanmış.
    public Type? Type { get; set; }
}


        // Endpoint'e rol atama işleminin yanıt sınıfı.
// Şu an için boş, ama gelecekte ek bilgiler eklenebilir.
public class AssignRoleEndpointCommandResponse
{
}



        // AssignRoleEndpointCommandRequest isteğini işleyen sınıf.
// IRequestHandler arayüzünü uygular ve request/response tiplerini belirtir.
public class AssignRoleEndpointCommandHandler : IRequestHandler<AssignRoleEndpointCommandRequest, AssignRoleEndpointCommandResponse>
{
    // Yetkilendirme endpoint servisi referansı, sadece okunabilir.
    readonly IAuthorizationEndpointService _authorizationEndpointService;

    // Kurucu metod, bağımlılık olarak IAuthorizationEndpointService alır ve sınıfın alanına atar.
    public AssignRoleEndpointCommandHandler(IAuthorizationEndpointService authorizationEndpointService)
    {
        _authorizationEndpointService = authorizationEndpointService;
    }

    // Handle metodu, AssignRoleEndpointCommandRequest isteğini işleyip yanıt döner.
    public async Task<AssignRoleEndpointCommandResponse> Handle(AssignRoleEndpointCommandRequest request, CancellationToken cancellationToken)
    {
        // Yetkilendirme endpoint servisi kullanılarak rol atanır.
        await _authorizationEndpointService.AssignRoleEndpointAsync(request.Roles, request.Menu, request.Code, request.Type);
        
        // Boş bir yanıt döner.
        return new AssignRoleEndpointCommandResponse();
    }
}

         * 
         * 
         * 
         */
    }
}
