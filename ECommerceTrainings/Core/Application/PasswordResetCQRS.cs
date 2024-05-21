using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Core.Application
{
    internal class PasswordResetCQRS
    {
        /*
         * // PasswordResetCommandRequest sınıfı, şifre sıfırlama isteği için kullanılan veri yapısını temsil eder.
// IRequest arayüzünden kalıtım alır ve PasswordResetCommandResponse tipinde bir yanıt döner.
public class PasswordResetCommandRequest : IRequest<PasswordResetCommandResponse>
{
    // Kullanıcının e-posta adresi.
    public string Email { get; set; }
}

// PasswordResetCommandResponse sınıfı, şifre sıfırlama isteğine verilen yanıtı temsil eder.
public class PasswordResetCommandResponse
{
    // Yanıt olarak şu an herhangi bir veri taşımıyor.
}

// PasswordResetCommandHandler sınıfı, şifre sıfırlama isteğini işlemek için kullanılır.
// IRequestHandler arayüzünden kalıtım alır ve PasswordResetCommandRequest ile PasswordResetCommandResponse tiplerinde çalışır.
public class PasswordResetCommandHandler : IRequestHandler<PasswordResetCommandRequest, PasswordResetCommandResponse>
{
    // Kimlik doğrulama hizmetine erişim sağlar.
    private readonly IAuthService _authService;

    // Constructor: Kimlik doğrulama hizmetini bağımlılık olarak alır.
    public PasswordResetCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    // Handle metodu, şifre sıfırlama isteğini işler.
    // PasswordResetCommandRequest tipinde bir istek alır ve PasswordResetCommandResponse tipinde bir yanıt döner.
    public async Task<PasswordResetCommandResponse> Handle(PasswordResetCommandRequest request, CancellationToken cancellationToken)
    {
        // Kimlik doğrulama hizmeti kullanılarak şifre sıfırlama işlemi başlatılır.
        await _authService.PasswordResetAsnyc(request.Email);

        // Boş bir yanıt nesnesi döner.
        return new PasswordResetCommandResponse();
    }
}

         * 
         * 
         * 
         */
    }
}
