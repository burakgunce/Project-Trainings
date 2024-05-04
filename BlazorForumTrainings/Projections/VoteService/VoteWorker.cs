using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;
using BlazorSozluk.Common.Constants;
using BlazorSozluk.Common.Events.Vote;
using RabbitMQ.Client;
using BlazorSozluk.Infrastructure.Queue;
using BlazorSozluk.Services.Vote;
using BlazorForumTrainings.Common.Infrastructure;

public class VoteWorker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _configuration;

    // Constructor to inject ILogger and IConfiguration
    public VoteWorker(ILogger<Worker> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    // Method to execute background tasks
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Get connection string from configuration
        var connStr = _configuration.GetConnectionString("SqlServer");

        // Create an instance of VoteService using the connection string
        var voteService = new Services.VoteService(connStr);

        // Consume messages from the queue for creating entry votes
        QueueFactory.CreateBasicConsumer()
            .EnsureExchange(SozlukConstants.VoteExchangeName)
            .EnsureQueue(SozlukConstants.CreateEntryVoteQueueName, SozlukConstants.VoteExchangeName)
            .Receive<CreateEntryVoteEvent>(vote =>
            {
                // Create entry vote and log information
                voteService.CreateEntryVote(vote).GetAwaiter().GetResult();
                _logger.LogInformation("Create Entry Received EntryId: {0}, VoteType: {1}", vote.EntryId, vote.VoteType);
            })
            .StartConsuming(SozlukConstants.CreateEntryVoteQueueName);

        // Consume messages from the queue for deleting entry votes
        QueueFactory.CreateBasicConsumer()
            .EnsureExchange(SozlukConstants.VoteExchangeName)
            .EnsureQueue(SozlukConstants.DeleteEntryVoteQueueName, SozlukConstants.VoteExchangeName)
            .Receive<DeleteEntryVoteEvent>(vote =>
            {
                // Delete entry vote and log information
                voteService.DeleteEntryVote(vote.EntryId, vote.CreatedBy).GetAwaiter().GetResult();
                _logger.LogInformation("Delete Entry Received EntryId: {0}", vote.EntryId);
            })
            .StartConsuming(SozlukConstants.DeleteEntryVoteQueueName);

        // Consume messages from the queue for creating entry comment votes
        QueueFactory.CreateBasicConsumer()
            .EnsureExchange(SozlukConstants.VoteExchangeName)
            .EnsureQueue(SozlukConstants.CreateEntryCommentVoteQueueName, SozlukConstants.VoteExchangeName)
            .Receive<CreateEntryCommentVoteEvent>(vote =>
            {
                // Create entry comment vote and log information
                voteService.CreateEntryCommentVote(vote).GetAwaiter().GetResult();
                _logger.LogInformation("Create Entry Comment Received EntryCommentId: {0}, VoteType: {1}", vote.EntryCommentId, vote.VoteType);
            })
            .StartConsuming(SozlukConstants.CreateEntryCommentVoteQueueName);

        // Consume messages from the queue for deleting entry comment votes
        QueueFactory.CreateBasicConsumer()
            .EnsureExchange(SozlukConstants.VoteExchangeName)
            .EnsureQueue(SozlukConstants.DeleteEntryCommentVoteQueueName, SozlukConstants.VoteExchangeName)
            .Receive<DeleteEntryCommentVoteEvent>(vote =>
            {
                // Delete entry comment vote and log information
                voteService.DeleteEntryCommentVote(vote.EntryCommentId, vote.CreatedBy).GetAwaiter().GetResult();
                _logger.LogInformation("Delete Entry Comment Received EntryCommentId: {0}", vote.EntryCommentId);
            })
            .StartConsuming(SozlukConstants.DeleteEntryCommentVoteQueueName);
    }
}

