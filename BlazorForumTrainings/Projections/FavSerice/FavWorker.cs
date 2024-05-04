using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;
using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Events.EntryComment;
using BlazorSozluk.Projections.FavoriteService.Services;
using RabbitMQ.Client.Events;
using BlazorForumTrainings.Common.Infrastructure;

public class FavWorker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _configuration;

    // Constructor injection of ILogger and IConfiguration
    public FavWorker(ILogger<Worker> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    // Override of ExecuteAsync method to start the background service
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Retrieving the connection string for SQL Server from appsettings.json
        var connStr = _configuration.GetConnectionString("SqlServer");

        // Creating an instance of FavoriteService using the connection string
        var favService = new FavoriteService(connStr);

        // Setting up RabbitMQ consumers for different types of events

        // Consumer for creating entry favorites
        QueueFactory.CreateBasicConsumer()
            .EnsureExchange(SozlukConstants.FavExchangeName)
            .EnsureQueue(SozlukConstants.CreateEntryFavQueueName, SozlukConstants.FavExchangeName)
            .Receive<CreateEntryFavEvent>(async fav =>
            {
                await favService.CreateEntryFav(fav); // Handling the event asynchronously
                _logger.LogInformation($"Received EntryId {fav.EntryId}");
            })
            .StartConsuming(SozlukConstants.CreateEntryFavQueueName);

        // Consumer for deleting entry favorites
        QueueFactory.CreateBasicConsumer()
            .EnsureExchange(SozlukConstants.FavExchangeName)
            .EnsureQueue(SozlukConstants.DeleteEntryFavQueueName, SozlukConstants.FavExchangeName)
            .Receive<DeleteEntryFavEvent>(fav =>
            {
                favService.DeleteEntryFav(fav).GetAwaiter().GetResult(); // Handling the event synchronously
                _logger.LogInformation($"Deleted Received EntryId {fav.EntryId}");
            })
            .StartConsuming(SozlukConstants.DeleteEntryFavQueueName);

        // Consumer for creating entry comment favorites
        QueueFactory.CreateBasicConsumer()
            .EnsureExchange(SozlukConstants.FavExchangeName)
            .EnsureQueue(SozlukConstants.CreateEntryCommentFavQueueName, SozlukConstants.FavExchangeName)
            .Receive<CreateEntryCommentFavEvent>(fav =>
            {
                favService.CreateEntryCommentFav(fav).GetAwaiter().GetResult(); // Handling the event synchronously
                _logger.LogInformation($"Create EntryComment Received EntryCommentId {fav.EntryCommentId}");
            })
            .StartConsuming(SozlukConstants.CreateEntryCommentFavQueueName);

        // Consumer for deleting entry comment favorites
        QueueFactory.CreateBasicConsumer()
            .EnsureExchange(SozlukConstants.FavExchangeName)
            .EnsureQueue(SozlukConstants.DeleteEntryCommentFavQueueName, SozlukConstants.FavExchangeName)
            .Receive<DeleteEntryCommentFavEvent>(fav =>
            {
                favService.DeleteEntryCommentFav(fav).GetAwaiter().GetResult(); // Handling the event synchronously
                _logger.LogInformation($"Deleted Received EntryCommentId {fav.EntryCommentId}");
            })
            .StartConsuming(SozlukConstants.DeleteEntryCommentFavQueueName);
    }
}

