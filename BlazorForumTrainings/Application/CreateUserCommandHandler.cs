using BlazorForumTrainings.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForumTrainings.Application
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        // Bağımlılıkların enjekte edildiği kurucu metot.
        public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        // Kullanıcı oluşturma işlemini gerçekleştiren metot.
        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // E-posta adresi veritabanında zaten kayıtlı mı kontrol edilir.
            var existsUser = await _userRepository.GetSingleAsync(i => i.EmailAddress == request.EmailAddress);

            // Eğer kullanıcı zaten varsa istisna fırlatılır.
            if (existsUser is not null)
                throw new DatabaseValidationException("User already exists!");

            // Yeni kullanıcı veritabanına eklenecek şekilde eşlenir.
            var dbUser = _mapper.Map<Domain.Models.User>(request);

            // Kullanıcı veritabanına eklenir ve etkilenen satır sayısı alınır.
            var rows = await _userRepository.AddAsync(dbUser);

            // Eğer kullanıcı başarıyla oluşturulmuşsa e-posta adresi değişikliği olayı yayımlanır.
            if (rows > 0)
            {
                // Yeni kullanıcı için e-posta adresi değişikliği olayı oluşturulur.
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
            }

            // Oluşturulan kullanıcının kimlik numarası döndürülür.
            return dbUser.Id;
        }
    }

}
