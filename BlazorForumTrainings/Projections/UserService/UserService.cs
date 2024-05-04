using System;
using System.Data.SqlClient; // Import SQL client
using System.Threading.Tasks;
using BlazorSozluk.Common.Events.User;
using Dapper; // Import Dapper for database operations
using Microsoft.Extensions.Configuration; // Import IConfiguration

public class UserService
{
    private string connStr; // Connection string field to store database connection

    // Constructor to inject IConfiguration for accessing configuration settings
    public UserService(IConfiguration configuration)
    {
        // Get the connection string from IConfiguration
        connStr = configuration.GetConnectionString("SqlServer");
    }

    // Method to create email confirmation
    public async Task<Guid> CreateEmailConfirmation(UserEmailChangedEvent @event)
    {
        var guid = Guid.NewGuid(); // Generate a new GUID for email confirmation

        // Establish a connection to the database
        using var connection = new SqlConnection(connStr);

        // Execute an asynchronous SQL command to insert email confirmation details
        await connection.ExecuteAsync("INSERT INTO EMAILCONFIRMATION (Id, CreateDate, OldEmailAddress, NewEmailAddress) VALUES (@Id, GETDATE(), @OldEmailAddress, @NewEmailAddress)",
            new
            {
                Id = guid, // Assign GUID
                OldEmailAddress = @event.OldEmailAddress, // Get old email address from event
                NewEmailAddress = @event.NewEmailAddress // Get new email address from event
            });

        return guid; // Return the generated GUID
    }
}

