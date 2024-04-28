using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTrainings.Services.OrderService.Infrastructure
{
    public static class MediatorExtension
    {
        // IMediator arayüzünden genişletme yöntemi ile domain olaylarını tetiklemek için bir yöntem sağlar.
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, OrderDbContext ctx)
        {
            // Değişiklik takipçisinde (ChangeTracker) bulunan ve domain olayları içeren varlık girişlerini alır.
            var domainEntities = ctx.ChangeTracker
                                    .Entries<BaseEntity>()
                                    .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            // Varlık girişlerinden alınan domain olaylarını bir listeye toplar.
            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            // Varlık girişlerinden alınan domain olaylarını temizler.
            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            // Domain olaylarını yayınlamak için bir döngüde ilerler ve her bir olayı yayınlar.
            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }

}
