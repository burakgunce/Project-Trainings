using Microsoft.Data.SqlClient;
using Dapper;
using System;
using System.Threading.Tasks;
using BlazorSozluk.Common.Events.Vote;

public class VoteService
{
    private readonly string _connectionString;

    // Constructor to initialize the connection string
    public VoteService(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Method to create a vote for an entry
    public async Task CreateEntryVote(CreateEntryVoteEvent vote)
    {
        // Delete any existing vote for the same entry by the same user
        await DeleteEntryVote(vote.EntryId, vote.CreatedBy);

        // Insert the new vote into the database
        using var connection = new SqlConnection(_connectionString);

        await connection.ExecuteAsync("INSERT INTO ENTRYVOTE (Id, CreateDate, EntryId, VoteType, CreatedById) VALUES (@Id, GETDATE(), @EntryId, @VoteType, @CreatedById)",
            new
            {
                Id = Guid.NewGuid(),
                EntryId = vote.EntryId,
                VoteType = (int)vote.VoteType,
                CreatedById = vote.CreatedBy
            });
    }

    // Method to delete a vote for an entry
    public async Task DeleteEntryVote(Guid entryId, Guid userId)
    {
        // Delete the vote from the database based on entryId and userId
        using var connection = new SqlConnection(_connectionString);

        await connection.ExecuteAsync("DELETE FROM EntryVote WHERE EntryId = @EntryId AND CREATEDBYID = @UserId",
            new
            {
                EntryId = entryId,
                UserId = userId
            });
    }

    // Method to create a vote for an entry comment
    public async Task CreateEntryCommentVote(CreateEntryCommentVoteEvent vote)
    {
        // Delete any existing vote for the same entry comment by the same user
        await DeleteEntryCommentVote(vote.EntryCommentId, vote.CreatedBy);

        // Insert the new vote into the database
        using var connection = new SqlConnection(_connectionString);

        await connection.ExecuteAsync("INSERT INTO ENTRYCOMMENTVOTE (Id, CreateDate, EntryCommentId, VoteType, CREATEDBYID) VALUES (@Id, GETDATE(), @EntryCommentId, @VoteType, @CreatedById)",
            new
            {
                Id = Guid.NewGuid(),
                EntryCommentId = vote.EntryCommentId,
                VoteType = Convert.ToInt16(vote.VoteType),
                CreatedById = vote.CreatedBy
            });
    }

    // Method to delete a vote for an entry comment
    public async Task DeleteEntryCommentVote(Guid entryCommentId, Guid userId)
    {
        // Delete the vote from the database based on entryCommentId and userId
        using var connection = new SqlConnection(_connectionString);

        await connection.ExecuteAsync("DELETE FROM EntryCommentVote WHERE EntryCommentId = @EntryCommentId AND CREATEDBYID = @UserId",
            new
            {
                EntryCommentId = entryCommentId,
                UserId = userId
            });
    }
}

