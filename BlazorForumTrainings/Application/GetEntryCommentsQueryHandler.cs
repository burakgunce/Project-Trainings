using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForumTrainings.Application
{
    public class GetEntryCommentsQueryHandler : IRequestHandler<GetEntryCommentsQuery, PagedViewModel<GetEntryCommentsViewModel>>
    {
        private readonly IEntryCommentRepository _entryCommentRepository;

        // Bağımlılıkları enjekte eden bir yapıcı metot.
        public GetEntryCommentsQueryHandler(IEntryCommentRepository entryCommentRepository)
        {
            _entryCommentRepository = entryCommentRepository;
        }

        // IRequestHandler arayüzünden Handle metodu uygulanır.
        public async Task<PagedViewModel<GetEntryCommentsViewModel>> Handle(GetEntryCommentsQuery request, CancellationToken cancellationToken)
        {
            // Giriş yorumlarını sorgulamak için bir sorgu oluşturulur.
            var query = _entryCommentRepository.AsQueryable();

            // Sorgu, giriş kimliğine göre filtrelenir ve ilişkili veriler yüklendirilir.
            query = query.Include(i => i.EntryCommentFavorites)
                         .Include(i => i.CreatedBy)
                         .Include(i => i.EntryCommentVotes)
                         .Where(i => i.EntryId == request.EntryId);

            // Giriş yorumlarını projeksiyona dönüştüren bir sorgu oluşturulur.
            var list = query.Select(i => new GetEntryCommentsViewModel()
            {
                Id = i.Id,
                Content = i.Content,
                IsFavorited = request.UserId.HasValue && i.EntryCommentFavorites.Any(j => j.CreatedById == request.UserId),
                FavoritedCount = i.EntryCommentFavorites.Count,
                CreatedDate = i.CreateDate,
                CreatedByUserName = i.CreatedBy.UserName,
                VoteType = request.UserId.HasValue && i.EntryCommentVotes.Any(j => j.CreatedById == request.UserId)
                    ? i.EntryCommentVotes.FirstOrDefault(j => j.CreatedById == request.UserId).VoteType
                    : Common.ViewModels.VoteType.None
            });

            // Sayfalı giriş yorumlarını almak için GetPaged metodu kullanılır.
            var entries = await list.GetPaged(request.Page, request.PageSize);

            return entries;
        }
    }

}
