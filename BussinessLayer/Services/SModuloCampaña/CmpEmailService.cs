using BussinessLayer.DTOs.ModuloCampaña.CmpEmail;
using BussinessLayer.Interfaces.ModuloCampaña.Repository;
using DataLayer.Models.ModuloCampaña;
using MailKit.Net.Smtp;
using MimeKit;
using Org.BouncyCastle.Cms;

public class EmailService
{
    private readonly ICmpConfiguracionesSmtpRepository _cmpConfiguracionesSmtpRepository;

    public EmailService(ICmpConfiguracionesSmtpRepository cmpConfiguracionesSmtpRepository)
    {
        _cmpConfiguracionesSmtpRepository = cmpConfiguracionesSmtpRepository;
    }

    public async Task SendEmailAsync(CmpEmailMessageDto emailMessage)
    {
        // Obtener datos para el envío SMTP.
        CmpConfiguracionesSmtp configuracionDto = await _cmpConfiguracionesSmtpRepository.GetyByIdAsync(Convert.ToInt32(emailMessage.ConfiguracionId), Convert.ToInt32(emailMessage.EmpresaId));

        var email = new MimeMessage();

        email.From.Add(new MailboxAddress(configuracionDto.Usuario, configuracionDto.Email));
        foreach (var recipient in emailMessage.To)
        {
            email.To.Add(MailboxAddress.Parse(recipient));
        }

        foreach (var cc in emailMessage.Cc)
        {
            email.Cc.Add(MailboxAddress.Parse(cc));
        }
        email.Subject = emailMessage.Subject;

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = emailMessage.IsHtml ? emailMessage.Body : null,
            TextBody = emailMessage.IsHtml ? emailMessage.Body : null
        };

        // Procesar y agregar adjuntos
        if (emailMessage.Attachments != null && emailMessage.Attachments.Count > 0)
        {
            foreach (var attachment in emailMessage.Attachments)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await attachment.CopyToAsync(memoryStream);
                    bodyBuilder.Attachments.Add(attachment.FileName, memoryStream.ToArray(), ContentType.Parse(attachment.ContentType));
                }
            }
        }
        email.Body = bodyBuilder.ToMessageBody();

        // Enviar correo
        using var smtpClient = new SmtpClient();
        try
        {
            await smtpClient.ConnectAsync(configuracionDto.ServidoresSmtp.Host, configuracionDto.ServidoresSmtp.Puerto);
            await smtpClient.AuthenticateAsync(configuracionDto.Email, configuracionDto.Contraseña);
            await smtpClient.SendAsync(email);
        }
        finally
        {
            await smtpClient.DisconnectAsync(true);
        }
    }
}
