using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disasterApi.Core.Interfaces.Services
{
    public interface INotificationService
    {
        Task SendMessageAsync(string message, string phoneNumber);
        Task SendEmailAsync(string subject, string body, List<string> emailAddress);
    }
}
