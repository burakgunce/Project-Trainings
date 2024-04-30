using BlazorForumTrainings.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForumTrainings.Application
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        // Bağımlılıkların enjekte edildiği kurucu metot.
        public UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        // Kullanıcı güncelleme işlemini gerçekleştiren metot.
        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            // Kullanıcı veritabanından alınıyor.
            var dbUser = await _userRepository.GetByIdAsync(request.Id);

            // Kullanıcı bulunamadıysa istisna fırlatılır.
            if (dbUser == null)
                throw new DatabaseValidationException("User not found!");

            // E-posta adresi değişip değişmediği kontrol ediliyor.
            var dbEmailAddress = dbUser.EmailAddress;
            var emailChanged = string.CompareOrdinal(dbEmailAddress, request.EmailAddress) != 0;

            // Gelen komut ile veritabanındaki kullanıcı eşleştiriliyor.
            _mapper.Map(request, dbUser);

            // Kullanıcı güncelleniyor ve etkilenen satır sayısı alınıyor.
            var rows = await _userRepository.UpdateAsync(dbUser);

            // E-posta adresi değiştiyse ve güncelleme başarılıysa ilgili olay yayımlanır.
            if (emailChanged && rows > 0)
            {
                // Kullanıcı e-posta adresi değişikliği olayı oluşturuluyor.
                var @event = new UserEmailChangedEvent()
                {
                    OldEmailAddress = null,
                    NewEmailAddress = dbUser.EmailAddress
                };

                // Olay RabbitMQ'ya gönderilir.
                QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.UserExchangeName,
                                                   exchangeType: SozlukConstants.DefaultExchangeType,
                                                   queueName: SozlukConstants.UserEmailChangedQueueName,
                                                   obj: @event);

                // E-posta adresi onay bekleyen duruma getirilir.
                dbUser.EmailConfirmed = false;
                await _userRepository.UpdateAsync(dbUser);
            }

            // Güncellenen kullanıcının kimlik numarası döndürülür.
            return dbUser.Id;
        }
    }

}
