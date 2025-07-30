//using SendGrid;
//using SendGrid.Helpers.Mail;

//namespace BussinessLayer.Services.NotificationModule.Implementations
//{
//    public class SendGridService
//    {
//        public async Task SendEmailAsync()
//        {
//            try
//            {
//                string apiKey = "";

//                SendGridClient sendGridClient = new SendGridClient(apiKey);

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
