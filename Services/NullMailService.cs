using Microsoft.Extensions.Logging;

namespace DutchTreat.Services {
    public class NullMailService : IMailService {
        private readonly ILogger<IMailService> logger;

        public NullMailService (ILogger<NullMailService> logger) {
            this.logger = logger;
        }
        public void SendMail (string to, string subject, string body) {
            // Log the message
            logger.LogInformation ($"To: {to} Subject: {subject} Body: {body}");
        }
    }
}