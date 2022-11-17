using _06_Entity.Settings;
using _06_Entity.ViewModel;
using MailKit.Net.Smtp;
using MimeKit;

// -----------------------------
// -RAJOUTER LE PACKAGE MAILKIT-
// -----------------------------
namespace _06_Entity.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _mailSettings;

        public EmailService(/*EmailSettings mailSettings*/)
        {
            //_mailSettings = mailSettings;

            _mailSettings = new()
            {
                Mail = "judd.kris@ethereal.email",
                DisplayName = "Judd Kris",
                Password = "SM7pTm2W8RPFV7py2G",
                Host = "smtp.ethereal.email",
                Port = 587
            };
        }

        public async Task SenEmailAsync(EmailViewModel emailViewModel)
        {

            MimeMessage email = new();

            email.From.Add(new MailboxAddress(emailViewModel.FromEmail, _mailSettings.Mail));
            email.To.Add(MailboxAddress.Parse(_mailSettings.Mail));

            email.Subject = emailViewModel.Subject;

            BodyBuilder builder = new ();

            if (emailViewModel.Attachments is not null)
            {
                byte[] fileBytes;

                foreach (var file in emailViewModel.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (MemoryStream ms = new())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }

                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }

            builder.HtmlBody = emailViewModel.Body;

            email.Body = builder.ToMessageBody();

            using (SmtpClient smtp = new())
            {
                // smtp.Connect(_mailSettings.Host, _mailSettings.Port);
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);

                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);

                await smtp.SendAsync(email);

                // smtp.Disconnect(true); // Inutile car dans clause 'using'
                // smtp.Dispose();
            }
        }
    }
}
