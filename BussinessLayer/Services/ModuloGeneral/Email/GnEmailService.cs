using BussinessLayer.DTOs.ModuloGeneral.Email;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.SMTP;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Email;
using MailKit.Security;
using MimeKit;

namespace BussinessLayer.Services.ModuloGeneral.Email
{
    public class GnEmailService : IGnEmailService
    {
        private readonly IGnSmtpConfiguracionRepository _configuracionRepository;

        public GnEmailService(IGnSmtpConfiguracionRepository configuracionRepository)
        {
            _configuracionRepository = configuracionRepository;
        }

        public async Task SendAsync(GnEmailMessageDto request, long companyId)
        {
            try
            {
                var smtpServer = await _configuracionRepository.GetSMTPByCompanyIdAsync(companyId)
                                 ?? throw new InvalidOperationException("No hay configuración SMTP para la empresa.");

                var email = new MimeMessage();

                // From y Sender
                var sender = new MailboxAddress(smtpServer.NombreRemitente ?? "", smtpServer.Remitente);
                email.From.Add(sender);
                email.Sender = sender;

                // TO
                if (request.To != null && request.To.Count() > 0)
                {
                    foreach (var recipient in request.To.Where(x => !string.IsNullOrWhiteSpace(x)))
                        email.To.Add(MailboxAddress.Parse(recipient));
                }

                // CC
                if (request.Cc != null && request.Cc.Count() > 0)
                {
                    foreach (var cc in request.Cc.Where(x => !string.IsNullOrWhiteSpace(x)))
                        email.Cc.Add(MailboxAddress.Parse(cc));
                }

                email.Subject = request.Subject ?? string.Empty;

                // Body
                var builder = new BodyBuilder();
                if (request.IsHtml)
                    builder.HtmlBody = request.Body ?? string.Empty;
                else
                    builder.TextBody = request.Body ?? string.Empty;

                // Adjuntos
                if (request.Attachments != null)
                {
                    foreach (var file in request.Attachments.Where(f => f?.Length > 0))
                    {
                        using var stream = file.OpenReadStream();
                        builder.Attachments.Add(file.FileName, stream);
                    }
                }

                email.Body = builder.ToMessageBody();

                using var smtp = new MailKit.Net.Smtp.SmtpClient();

                // ❗ Recomendado: NO deshabilitar validación de certificado en producción.
                // smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;

                // Elige la opción de seguridad según tu configuración (ejemplos):
                // var secure = smtpServer.UsarStartTls ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto;
                var secure = SecureSocketOptions.StartTls;

                await smtp.ConnectAsync(smtpServer.Servidor, smtpServer.Puerto, secure);
                await smtp.AuthenticateAsync(smtpServer.UsuarioSmtp, smtpServer.PassUsuario);

                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
