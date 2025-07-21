using disasterApi.Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML.Messaging;

namespace disasterApi.Core.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IConfiguration _configuration;
        
        public NotificationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string subject, string body, List<string> emailAddress)
        {

            var apiKey = _configuration["SendGrid:ApiKey"] ?? "";

            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("disaster@alert.com", "Disaster Alert"),
                Subject = subject,
                PlainTextContent = body,

            };
            msg.AddTos(emailAddress.Select(email => new EmailAddress(email)).ToList());
            var response = await client.SendEmailAsync(msg);

            var responseStatusCode = response.StatusCode;
        }

        public async Task SendMessageAsync(string message, string phoneNumber)
        {
            string accountSid = _configuration["Twilio:AccountSid"] ?? "";
            string authToken = _configuration["Twilio:AuthToken"] ?? "";
            string fromPhoneNumber = _configuration["Twilio:FromPhoneNumber"] ?? "";

            try
            {
                TwilioClient.Init(accountSid, authToken);
                var messageResponse = await MessageResource.CreateAsync(
                    body: message,
                    from: new Twilio.Types.PhoneNumber(fromPhoneNumber),
                    to: new Twilio.Types.PhoneNumber(string.Concat("+66", phoneNumber.AsSpan(1)))
                );

                var status = messageResponse.Status;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new InvalidOperationException($"Failed to send message via Twilio. {ex.Message}");
            }
        }
    }
}
