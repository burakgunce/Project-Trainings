using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForumTrainings.Application
{
    public class GetMainPageEntriesQueryHandler : IRequestHandler<GetMainPageEntriesQuery, PagedViewModel<GetEntryDetailViewModel>>
    {
        private readonly IEntryRepository _entryRepository;

        // Bağımlılıkların enjekte edildiği kurucu metot.
        public GetMainPageEntriesQueryHandler(IEntryRepository entryRepository)
        {
            _entryRepository = entryRepository;
        }

        // Ana sayfa girişlerini almak için kullanılan metot.
        public async Task<PagedViewModel<GetEntryDetailViewModel>> Handle(GetMainPageEntriesQuery request, CancellationToken cancellationToken)
        {
            // Girişlerin sorgusu yapılır.
            var query = _entryRepository.AsQueryable();

            // Girişlerin ilişkili verileri ile birlikte alınması sağlanır.
            query = query.Include(i => i.EntryFavorites)
                         .Include(i => i.CreatedBy)
                         .Include(i => i.EntryVotes);

            // Girişlerin detaylarının projeksiyonu yapılır.
            var list = query.Select(i => new GetEntryDetailViewModel()
            {
                Id = i.Id,
                Subject = i.Subject,
                Content = i.Content,
                IsFavorited = request.UserId.HasValue && i.EntryFavorites.Any(j => j.CreatedById == request.UserId),
                FavoritedCount = i.EntryFavorites.Count,
                CreatedDate = i.CreateDate,
                CreatedByUserName = i.CreatedBy.UserName,
                VoteType =
                    request.UserId.HasValue && i.EntryVotes.Any(j => j.CreatedById == request.UserId)
                    ? i.EntryVotes.FirstOrDefault(j => j.CreatedById == request.UserId).VoteType
                    : Common.ViewModels.VoteType.None
            });

            // Sayfalı bir şekilde girişler döndürülür.
            var entries = await list.GetPaged(request.Page, request.PageSize);

            return entries;
        }
    }

}
