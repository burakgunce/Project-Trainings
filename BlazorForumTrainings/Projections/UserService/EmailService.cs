using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration; // Import IConfiguration
using Microsoft.Extensions.Logging; // Import ILogger

public class EmailService
{
    private readonly IConfiguration _configuration; // Configuration object for accessing app settings
    private readonly ILogger<EmailService> _logger; // Logger for logging email operations

    // Constructor to inject IConfiguration and ILogger
    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration; // Assign IConfiguration object
        _logger = logger; // Assign ILogger object
    }

    // Method to generate a confirmation link based on the provided confirmation ID
    public string GenerateConfirmationLink(Guid confirmationId)
    {
        // Get the base URL for confirmation link from app settings and append the confirmation ID
        var baseUrl = _configuration["ConfirmationLinkBase"] + confirmationId;
        return baseUrl; // Return the generated confirmation link
    }

    // Method to send an email with the provided recipient email address and content
    public Task SendEmail(string toEmailAddress, string content)
    {
        // Log the email sending operation including recipient email address and content
        _logger.LogInformation($"Email sent to {toEmailAddress} with content of {content}");

        // Return a completed task since this method does not perform actual email sending (for demonstration purposes)
        return Task.CompletedTask;
    }
}

