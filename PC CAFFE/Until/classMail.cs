namespace PCPOS.Util
{
    internal class classMail
    {
        public static void send_email(string _posiljatelj, string _password, string _primatelj, string _naslov, string _sadrzaj)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com");
            mail.To.Add(_primatelj);
            mail.Subject = _naslov;
            mail.From = new System.Net.Mail.MailAddress(_posiljatelj);
            mail.Body = _sadrzaj;

            smtp.Port = 587;
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtp.Credentials = new System.Net.NetworkCredential(_posiljatelj, _password);
            smtp.EnableSsl = true;

            smtp.Send(mail);
        }
    }
}