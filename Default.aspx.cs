using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
public partial class _Default : System.Web.UI.Page
{
    MailCommon mc = new MailCommon();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void send_click(object sender, EventArgs e)
    {
        try
        {
            string From = "Mailaddress";         //enter your mail
            string To = to.Text;                           
            string subject = sub.Text;
            string username = from.Text;
            string contact = Contact.Text;
            string message2 = body2.Text;
            string host = "smtp.gmail.com";                     // host 
            int hostno = 587;                                   //port
            string pass = "Password";                   //password
            string path = "~/mail.html";  

            status.Text = mc.Sendmail(From, To, subject, username, contact, message2, host, hostno, pass, path, upload);            //call to class file 
           
        }
        catch (Exception ex)
        {
            status.Text = ex.StackTrace;
        }
    }
}