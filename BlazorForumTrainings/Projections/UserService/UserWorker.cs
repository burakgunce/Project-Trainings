using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging; // Import ILogger
using UserService.Services; // Import UserService and EmailService
using Microsoft.Extensions.Hosting; // Import BackgroundService
using BlazorForumTrainings.Common.Infrastructure;

public class UserWorker : BackgroundService
{
    private readonly ILogger<Worker> _logger; // Logger for logging worker operations
    private readonly UserService _userService; // Service for user-related operations
    private readonly EmailService _emailService; // Service for email-related operations

    // Constructor to inject ILogger, UserService, and EmailService
    public UserWorker(ILogger<Worker> logger, UserService userService, EmailService emailService)
    {
        _logger = logger; // Assign ILogger object
        _userService = userService; // Assign UserService object
        _emailService = emailService; // Assign EmailService object
    }

    // Override method to define the background task execution logic
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Create a basic consumer for processing messages from the user email changed queue
        QueueFactory.CreateBasicConsumer()
            // Ensure the exchange exists and create it if not
            .EnsureExchange(SozlukConstants.UserExchangeName)
            // Ensure the queue exists and bind it to the exchange
            .EnsureQueue(SozlukConstants.UserEmailChangedQueueName, SozlukConstants.UserExchangeName)
            // Receive messages of type UserEmailChangedEvent
            .Receive<UserEmailChangedEvent>(user =>
            {
                // DB Insert 
                // Call UserService to create an email confirmation and get the confirmation ID
                var confirmationId = _userService.CreateEmailConfirmation(user).GetAwaiter().GetResult();

                // Generate Link
                // Generate a confirmation link using the EmailService and the obtained confirmation ID
                var link = _emailService.GenerateConfirmationLink(confirmationId);

                // Send Email
                // Send the email using the EmailService with the new email address and the generated link
                _emailService.SendEmail(user.NewEmailAddress, link).GetAwaiter().GetResult();
            })
            // Start consuming messages from the user email changed queue
            .StartConsuming(SozlukConstants.UserEmailChangedQueueName);
    }
}

