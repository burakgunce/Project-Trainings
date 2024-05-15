using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Core.Application
{
    internal class RefreshTokenCQRS
    {
        /*
         * public class RefreshTokenLoginCommandRequest : IRequest<RefreshTokenLoginCommandResponse>
{
    // Yenileme belirteci
    public string RefreshToken { get; set; }
}

public class RefreshTokenLoginCommandResponse
{
    // Yenilenmiş belirteç
    public Token Token { get; set; }
}

public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommandRequest, RefreshTokenLoginCommandResponse>
{
    readonly IAuthService _authService;

    public RefreshTokenLoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
    {
        // Yenileme belirteci kullanılarak oturum yenileme işlemi gerçekleştirilir
        Token token = await _authService.RefreshTokenLoginAsync(request.RefreshToken);
        // Yanıt oluşturulur
        return new RefreshTokenLoginCommandResponse
        {
            Token = token
        };
    }
}

         * 
         * 
         * 
         */
    }
}
