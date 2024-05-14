using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceTrainings.Core.Application
{
    internal class CreateUserCQRS
    {
        /*
         * public class CreateUserCommandRequest : IRequest<CreateUserCommandResponse>
{
    // Kullanıcının adı ve soyadı
    public string NameSurname { get; set; }
    // Kullanıcı adı
    public string Username { get; set; }
    // Kullanıcının e-posta adresi
    public string Email { get; set; }
    // Kullanıcının parolası
    public string Password { get; set; }
    // Parola onayı
    public string PasswordConfirm { get; set; }
}

public class CreateUserCommandResponse
{
    // İşlemin başarılı olup olmadığını belirtir
    public bool Succeeded { get; set; }
    // Oluşturma işlemiyle ilgili mesaj
    public string Message { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
{
    readonly IUserService _userService; // Kullanıcı servisi

    // Constructor: Kullanıcı servisi enjekte edilir
    public CreateUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    // İsteği işler
    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
        // Kullanıcı oluşturma işlemini gerçekleştirir
        CreateUserResponse response = await _userService.CreateAsync(new()
        {
            Email = request.Email,
            NameSurname = request.NameSurname,
            Password = request.Password,
            PasswordConfirm = request.PasswordConfirm,
            Username = request.Username
        });

        // İşlem sonucunu ve mesajı döndürür
        return new() 
        {
            Message = response.Message,
            Succeeded = response.Succeeded
        };

        //throw new UserCreateFailedException();
    }
}
        ILK ONCE CLIENT TAN SANA REQUEST VE RESPONSE GELIYO DAHA SONRA SENIN BUNLARI DTO ILE KARSILAMAN GEREKIYO DTO LARDA GENE REQUEST VE RESPONSE DENK SEN BU REQ VE RESPONSE U DTO LARA MAP LEYIP OYLE ISLEM YAPIYOSUN EKLEME SILME VS SERVISLER ARASINDA DTO YU KULLANIYOSUN
         * 
         * 
         */
    }
}
