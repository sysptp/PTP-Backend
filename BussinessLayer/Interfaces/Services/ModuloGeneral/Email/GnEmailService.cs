
//using BussinessLayer.DTOs.ModuloGeneral.Email;
//using Microsoft.Extensions.Options;
//using MimeKit;
//using SendGrid.Helpers.Mail;

//namespace BussinessLayer.Interfaces.Services.ModuloGeneral.Email
//{
//    public class GnEmailService
//    {

//            public GnEmailService()
//            {
//            }

//            public async Task SendAsync(GnEmailMessageDto request)
//            {
//                try
//                {
//                    MimeMessage email = new();
//                    GnSmtp
//                    email.Sender = MailboxAddress.Parse($"{_mailSettings.DisplayName} <{_mailSettings.EmailFrom}>");
//                    email.To.Add(MailboxAddress.Parse(request.To));
//                    email.Subject = request.To;
//                    BodyBuilder builder = new();
//                    builder.HtmlBody = request.Body;
//                    email.Body = builder.ToMessageBody();

//                    using MailKit.Net.Smtp.SmtpClient smtp = new();
//                    smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
//                    smtp.Connect(_mailSettings.smtp, _mailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
//                    smtp.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
//                    await smtp.SendAsync(email);
//                    smtp.Disconnect(true);
//                }
//                catch (Exception ex)
//                {

//                }
//            }
//        }
//    }
//}
//}
