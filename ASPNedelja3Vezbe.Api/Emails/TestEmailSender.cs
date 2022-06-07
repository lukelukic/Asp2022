using ASPNedelja3.Application.Emails;

namespace ASPNedelja3Vezbe.Api.Emails
{
    public class TestEmailSender : IEmailSender
    {
        public void Send(MessageDto message)
        {
            System.Console.WriteLine("Sending email:");
            System.Console.WriteLine("To: " + message.To);
            System.Console.WriteLine("Title: " + message.Title);
            System.Console.WriteLine("Body: " + message.Body);
        }
    }
}
