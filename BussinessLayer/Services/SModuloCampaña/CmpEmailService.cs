using BussinessLayer.DTOs.ModuloCampaña.CmpEmail;
using BussinessLayer.Interfaces.ModuloCampaña.Repository;
using DataLayer.Models.ModuloCampaña;
using MailKit.Net.Smtp;
using MimeKit;

public class CmpEmailService(ICmpConfiguracionesSmtpRepository cmpConfiguracionesSmtpRepository, ICmpLogsEnvioRepository cmpLogsEnvioRepository) : ICmpEmailService
{
    public async Task SendEmailAsync(CmpEmailMessageDto emailMessage)
    {
        CmpConfiguracionesSmtp configuracionDto = await cmpConfiguracionesSmtpRepository.GetyByIdAsync(emailMessage.ConfiguracionId, emailMessage.EmpresaId);

        MimeMessage email = CreateEmailMessage(emailMessage, configuracionDto);

        using var smtpClient = new SmtpClient();
        string response = string.Empty;

        try
        {
            await smtpClient.ConnectAsync(configuracionDto.ServidoresSmtp.Host, configuracionDto.ServidoresSmtp.Puerto, true);
            await smtpClient.AuthenticateAsync(configuracionDto.Email, configuracionDto.Contraseña);
            response = await smtpClient.SendAsync(email);
        }
        catch (Exception ex)
        {
            response = $"Error al enviar el correo: {ex.Message}";
        }
        finally
        {
            await smtpClient.DisconnectAsync(true);

            // Registrar log
            var destinatarios = string.Join(";", emailMessage.To.Concat(emailMessage.Cc));

            var log = new CmpLogsEnvio(
                emailMessage.Body,
                emailMessage.Subject,
                destinatarios,
                response,
                configuracionDto.EmpresaId
            );
            await cmpLogsEnvioRepository.AddAsync(log);
        }
    }
    private MimeMessage CreateEmailMessage(CmpEmailMessageDto emailMessage, CmpConfiguracionesSmtp configuracionDto)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(configuracionDto.Usuario, configuracionDto.Email));

        foreach (var recipient in emailMessage.To)
            email.To.Add(MailboxAddress.Parse(recipient));

        if(emailMessage.Cc != null)
        foreach (var cc in emailMessage.Cc)
            email.Cc.Add(MailboxAddress.Parse(cc));

        email.Subject = emailMessage.Subject;

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = emailMessage.IsHtml ? emailMessage.Body : null,
            TextBody = emailMessage.IsHtml ? null : emailMessage.Body
        };

        if (emailMessage.Attachments != null)
        {
            foreach (var attachment in emailMessage.Attachments)
            {
                using var memoryStream = new MemoryStream();
                attachment.CopyTo(memoryStream);
                bodyBuilder.Attachments.Add(attachment.FileName, memoryStream.ToArray(), ContentType.Parse(attachment.ContentType));
            }
        }
        email.Body = bodyBuilder.ToMessageBody();
        return email;
    }
}
