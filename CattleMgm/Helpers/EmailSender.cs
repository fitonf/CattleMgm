using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace CattleMgm.Helpers
{
    
    public class EmailSender : IEmailSender
    {
        public IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //krijimi i smtp clientit
            SmtpClient smtpClient = new SmtpClient();
            //perdorimi i kredencialeve nga appsettings.json
            NetworkCredential basicCredential = new NetworkCredential(_configuration["EmailConfig:Email"], _configuration["EmailConfig:Password"]);
            //objekti i emailit
            MailMessage mailMessage = new MailMessage();
            //objekti i adreses prej te ciles vjen emaili 
            MailAddress fromAddress = new MailAddress(_configuration["EmailConfig:Email"]);
            //hosti i smtp 
            smtpClient.Host = _configuration["EmailConfig:Host"];
            //perdorimi i autentikimit me user dhe password te tonin
            smtpClient.UseDefaultCredentials = false;
            //perdorimi i autentikimit me user dhe password te tonin
            smtpClient.Credentials = basicCredential;
            smtpClient.EnableSsl = true;
            //appsettings.json port
            smtpClient.Port = Convert.ToInt16(_configuration["EmailConfig:Port"]);
            //email properties from, subject, body, to
            mailMessage.From = fromAddress;
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = BodyContent(htmlMessage, email, null, null, true);
            mailMessage.To.Add(email);

            //krijimi i nje threadi per dergim te emailit
            Thread thread = new Thread(async t =>
            {
                try
                {
                    //send email
                    smtpClient.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    
                }
            });
            //startimi i threadit
            thread.Start();
            
        }

        private string BodyContent(string mainContent, string Name = "", string CommentTitle = null, string CommentDescription = null, bool AddHeader = true)
        {
            //layout statik i emailit ne HTML
            string content = $"<div style='background-color:#f4f4f4; padding-bottom: 30px;'>" +
                $"<div style='margin:auto; max-width: 600px;'>" +
                $"<div style='padding: 10px 32px;border-radius: 0 20px; background-color:#FFFFFF;box-shadow: 5px 6px 17px #aaaaaa; margin: auto'>" +
                $"<div>";
            //nqs kemi header shto header ne html
            if (AddHeader)
                content += $"<p style='font-size: 20px; color:#094667'><b>I/e </b> {Name}, <br/></p>";
            //contenti i emailit dinamik
            content += $"{mainContent}" +
            $"<div>" +
            $"<p style='font-size: 18px; margin-bottom: 0px; text-align: right;'><b>Me respekt,</b></p>" +
            $"<p style='margin-top: 3px;text-align: right;'>Praktika team</p>" +
            $"</div>" +
            $"</div>" +
            $"</div>" +
            $"</div>";
            return content;
        }
    }
}
