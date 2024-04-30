using BlazorForumTrainings.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForumTrainings.Application
{
    public class CreateEntryCommentFavCommandHandler : IRequestHandler<CreateEntryCommentFavCommand, bool>
    {
        // IRequestHandler arayüzünden Handle metodu uygulanır.
        public async Task<bool> Handle(CreateEntryCommentFavCommand request, CancellationToken cancellationToken)
        {
            // Yorum beğenisini oluşturan bir olay nesnesi oluşturulur ve kuyruğa iletilir.
            QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.FavExchangeName,
                exchangeType: SozlukConstants.DefaultExchangeType,
                queueName: SozlukConstants.CreateEntryCommentFavQueueName,
                obj: new CreateEntryCommentFavEvent()
                {
                    EntryCommentId = request.EntryCommentId,
                    CreatedBy = request.UserId
                });

            // İşlem başarıyla tamamlandıktan sonra true döndürülür.
            return await Task.FromResult(true);
        }
    }

}
