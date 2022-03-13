using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Model
{
    public class MsmqModel
    {
        MessageQueue messageQueue = new MessageQueue();

        public void Sender(string token)
        {
            this.messageQueue.Path = @".\private$\Tokens";
            try
            {
                if(!MessageQueue.Exists(this.messageQueue.Path))
                {
                    MessageQueue.Create(this.messageQueue.Path);
                }
                this.messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                this.messageQueue.ReceiveCompleted += MessageQueue_ReceiveCompleted;
                this.messageQueue.Send(token);
                this.messageQueue.BeginReceive();
                this.messageQueue.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var message = this.messageQueue.EndReceive(e.AsyncResult);
            string token = message.Body.ToString();
            try
            {
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("alberto12345sam@gmail.com", "sam&12345"),
                    EnableSsl = true
                };
                mailMessage.From = new MailAddress("alberto12345sam@gmail.com");
                mailMessage.To.Add(new MailAddress("alberto12345sam@gmail.com"));
                mailMessage.Body = token;
                mailMessage.Subject = "Fundoo Note App Reset Link";
                smtpClient.Send(mailMessage);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
   
}
