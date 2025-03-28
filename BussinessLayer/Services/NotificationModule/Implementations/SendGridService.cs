using SendGrid;
using SendGrid.Helpers.Mail;

public class SendGridService : ISendGridService
{
   public async Task<Response> SendEmailAsync()
   {
    
    var client = new SendGridClient("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");

    var from = new EmailAddress("es.geraldsilverio@gmail.com", "Example User");
    var subject = "Sending with SendGrid is Fun";
        //var to = new EmailAddress("geraldsilverio412@gmail.com", "Example User");
        var to = new EmailAddress("djlocuralapara@gmail.com", "Example User");
        var plainTextContent = "and easy to do anywhere, even with C#";
    var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";

    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

    var response = await client.SendEmailAsync(msg);

    return response;

   }
    
}
