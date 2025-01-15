//using BussinessLayer.DTOs.ModuloCampaña.CmpEmail;
//using DataLayer.Models.ModuloCampaña;
//using MailKit.Net.Smtp;
//using MimeKit;

//public class EmailService
//{
//    public async Task SendEmailAsync(CmpEmailMessageDto emailMessage)
//    {
//        var email = new MimeMessage();
//        //email.From.Add(new MailboxAddress(_emailConfig.SenderName, _emailConfig.SenderEmail));
//        foreach (var recipient in emailMessage.To)
//        {
//            email.To.Add(MailboxAddress.Parse(recipient));
//        }
//        email.Subject = emailMessage.Subject;

//        var bodyBuilder = new BodyBuilder
//        {
//            HtmlBody = emailMessage.IsHtml ? emailMessage.Body : null,
//            TextBody = !emailMessage.IsHtml ? emailMessage.Body : null
//        };

//        // Agregar adjuntos
//        foreach (var attachment in emailMessage.Attachments)
//        {
//            bodyBuilder.Attachments.Add(attachment.FileName, attachment.Content, ContentType.Parse(attachment.MimeType));
//        }

//        email.Body = bodyBuilder.ToMessageBody();

//        // Enviar correo
//        using var smtpClient = new SmtpClient();
//        try
//        {
//            await smtpClient.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, _emailConfig.UseSsl);
//            await smtpClient.AuthenticateAsync(_emailConfig.SenderEmail, _emailConfig.Password);
//            await smtpClient.SendAsync(email);
//        }
//        finally
//        {
//            await smtpClient.DisconnectAsync(true);
//        }
//    }
//}
