using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI.WebControls;


/// <summary>
/// Summary description for MailCommon
/// </summary>
public class MailCommon
{
    public MailCommon()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string Sendmail(string From, string To, string subject, string username, string contact, string message2, string host, int hostno, string pass, string path, FileUpload upload)
    {
        try
        {
            MailMessage message = new MailMessage();
            using (message)
            {
                message.From = new MailAddress(From);
                message.Subject = subject;
                message.Body = Populatebody(username, contact, To, subject, message2, path);
                message.IsBodyHtml = true;
                message.To.Add(new MailAddress(To));
                if (upload.HasFile)                                     //for file upload attachment
                {
                    HttpFileCollection fc = HttpContext.Current.Request.Files;
                    for (int i = 0; i <= fc.Count - 1; i++)
                    {
                        HttpPostedFile pf = fc[i];
                        Attachment attach = new Attachment(pf.InputStream, pf.FileName);
                        message.Attachments.Add(attach);
                    }
                }
                SmtpClient client = new SmtpClient(host, hostno);
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(From, pass);
                client.Send(message);
                return "message was sent successfully";
            }
        }
        catch (Exception ex)
        {
            return ex.StackTrace;
        }
    }
    public string Populatebody(string username, string to, string contact, string subject, string message2, string path) //html mail body for more mail body copy this fucntion
    {
        try
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath(path)))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", username);
            body = body.Replace("{Email}", to);
            body = body.Replace("{Contact}", contact);
            body = body.Replace("{Subject}", subject);
            body = body.Replace("{Messsage}", message2);
            return body;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
