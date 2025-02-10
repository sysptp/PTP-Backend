using BussinessLayer.DTOs.ModuloGeneral.Email;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.SMTP;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Email;
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

        public async Task SendAsync(GnEmailMessageDto request,long companyId)
        {
            try
            {
                MimeMessage email = new();
                var smtpServer = await _configuracionRepository.GetSMTPByCompanyId(companyId);
                    email.Sender = MailboxAddress.Parse($"{smtpServer.NombreRemitente} <{smtpServer.Remitente}>");
                if (request.To != null)
                {
                    foreach (var recipient in request.To)
                    {
                        email.To.Add(MailboxAddress.Parse(recipient));
                    }
                }
                email.Subject = request.Subject;
                BodyBuilder builder = new();
                builder.HtmlBody = request.Body;
                email.Body = builder.ToMessageBody();

                using MailKit.Net.Smtp.SmtpClient smtp = new();
                smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                smtp.Connect(smtpServer.Servidor, smtpServer.Puerto, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate(smtpServer.UsuarioSmtp,smtpServer.PassUsuario);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {

            }
        }
    }
}

