using DAL.Models;
using System.Net;
using System.Net.Mail;

namespace presentationProject.Utility
{
	public static class MailSettings
	{
		public static void SendEmail(Email email)
		{
			var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("hanykasim.tawfik@gmail.com", "mgqoexvyvdbwqxjv");
			client.Send("hanykasim.tawfik@gmail.com", email.Recepient, email.Subject, email.Body);
		}
	}
}
