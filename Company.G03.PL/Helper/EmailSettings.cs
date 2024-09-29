using Company.G03.DAL.Model;
using System.Net;
using System.Net.Mail;

namespace Company.G03.PL.Helper
{
	public static class EmailSettings
	{
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("gemiahmed2002@gmail.com","sbxiwbyvmceyiqtm");
            client.Send("gemiahmed2002@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
