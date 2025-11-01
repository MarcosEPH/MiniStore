using RefactoredMiniStore.Core;
using System;

namespace RefactoredMiniStore.Infra
{
    public class ConsoleEmailNotifier : IEmailNotifier
    {
        public void SendEmail(string to, string subject, string body)
            => Console.WriteLine($"[EMAIL] To:{to} Subj:{subject} Body:{body}");
    }

    public class ConsoleSmsNotifier : ISmsNotifier
    {
        public void SendSms(string phone, string message)
            => Console.WriteLine($"[SMS] To:{phone} Msg:{message}");
    }

    public class CompositeNotifier : INotifier
    {
        public IEmailNotifier Email { get; }
        public ISmsNotifier Sms { get; }

        public CompositeNotifier(IEmailNotifier email, ISmsNotifier sms)
        {
            Email = email;
            Sms = sms;
        }
    }
}
