using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Events.EntryComment;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;

namespace BlazorSozluk.Projections.FavoriteService.Services
{
    public class FavoriteService
    {
        private readonly string _connectionString;

        // Bağlantı dizesini parametre olarak alan constructor
        public FavoriteService(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Bir girişi favorilere eklemek için kullanılan metot
        public async Task CreateEntryFav(CreateEntryFavEvent @event)
        {
            // SqlConnection oluşturma ve using bloğu içinde kullanma, IDisposable arayüzü ile otomatik kapatma
            using var connection = new SqlConnection(_connectionString);

            // ExecuteAsync metodu kullanılarak parametreleri içeren SQL sorgusunun çalıştırılması
            await connection.ExecuteAsync(
                "INSERT INTO EntryFavorite (Id, EntryId, CreatedById, CreateDate) VALUES(@Id, @EntryId, @CreatedById, GETDATE())",
                new
                {
                    Id = Guid.NewGuid(),  // Yeni bir GUID oluşturma
                    EntryId = @event.EntryId,  // Olay nesnesinden gelen giriş kimliği
                    CreatedById = @event.CreatedBy  // Olay nesnesinden gelen oluşturan kimlik
                });
        }

        // Bir giriş yorumunu favorilere eklemek için kullanılan metot
        public async Task CreateEntryCommentFav(CreateEntryCommentFavEvent @event)
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.ExecuteAsync(
                "INSERT INTO EntryCommentFavorite (Id, EntryCommentId, CreatedById, CreateDate) VALUES(@Id, @EntryCommentId, @CreatedById, GETDATE())",
                new
                {
                    Id = Guid.NewGuid(),
                    EntryCommentId = @event.EntryCommentId,
                    CreatedById = @event.CreatedBy
                });
        }

        // Bir girişi favorilerden kaldırmak için kullanılan metot
        public async Task DeleteEntryFav(DeleteEntryFavEvent @event)
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.ExecuteAsync(
                "DELETE FROM EntryFavorite WHERE EntryId = @EntryId AND CreatedById = @CreatedById",
                new
                {
                    Id = Guid.NewGuid(),
                    EntryId = @event.EntryId,
                    CreatedById = @event.CreatedBy
                });
        }

        // Bir giriş yorumunu favorilerden kaldırmak için kullanılan metot
        public async Task DeleteEntryCommentFav(DeleteEntryCommentFavEvent @event)
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.ExecuteAsync(
                "DELETE FROM EntryCommentFavorite WHERE EntryCommentId = @EntryCommentId AND CreatedById = @CreatedById",
                new
                {
                    Id = Guid.NewGuid(),
                    EntryCommentId = @event.EntryCommentId,
                    CreatedById = @event.CreatedBy
                });
        }
    }
}

