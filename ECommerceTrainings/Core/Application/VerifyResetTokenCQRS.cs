using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Core.Application
{
    internal class VerifyResetTokenCQRS
    {
        /*
         * // VerifyResetTokenCommandRequest sınıfı, şifre sıfırlama token'ının doğruluğunu kontrol etmek için kullanılan veri yapısını temsil eder.
// IRequest arayüzünden kalıtım alır ve VerifyResetTokenCommandResponse tipinde bir yanıt döner.
public class VerifyResetTokenCommandRequest : IRequest<VerifyResetTokenCommandResponse>
{
    // Kullanıcının şifre sıfırlama token'ı.
    public string ResetToken { get; set; }

    // Kullanıcının ID'si.
    public string UserId { get; set; }
}

// VerifyResetTokenCommandResponse sınıfı, şifre sıfırlama token'ının doğruluğunu kontrol eden yanıtı temsil eder.
public class VerifyResetTokenCommandResponse
{
    // Token doğruluğunun sonucu (true veya false).
    public bool State { get; set; }
}

// VerifyResetTokenCommandHandler sınıfı, şifre sıfırlama token'ını doğrulama isteğini işlemek için kullanılır.
// IRequestHandler arayüzünden kalıtım alır ve VerifyResetTokenCommandRequest ile VerifyResetTokenCommandResponse tiplerinde çalışır.
public class VerifyResetTokenCommandHandler : IRequestHandler<VerifyResetTokenCommandRequest, VerifyResetTokenCommandResponse>
{
    // Kimlik doğrulama hizmetine erişim sağlar.
    private readonly IAuthService _authService;

    // Constructor: Kimlik doğrulama hizmetini bağımlılık olarak alır.
    public VerifyResetTokenCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    // Handle metodu, şifre sıfırlama token'ını doğrulama isteğini işler.
    // VerifyResetTokenCommandRequest tipinde bir istek alır ve VerifyResetTokenCommandResponse tipinde bir yanıt döner.
    public async Task<VerifyResetTokenCommandResponse> Handle(VerifyResetTokenCommandRequest request, CancellationToken cancellationToken)
    {
        // Kimlik doğrulama hizmeti kullanılarak şifre sıfırlama token'ının doğruluğu kontrol edilir.
        bool state = await _authService.VerifyResetTokenAsync(request.ResetToken, request.UserId);

        // Doğrulama sonucunu içeren bir yanıt nesnesi döner.
        return new VerifyResetTokenCommandResponse
        {
            State = state
        };
    }
}

         * 
         * 
         * 
         */
    }
}
