using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Outlook;

namespace BirthdayApp
{
    public class MailManager
    {

        public static void SendMail(String to,String cc, String subject, String message)
        {
            Application oApp = new Application();
            MailItem mailItem = (MailItem)oApp.CreateItem(OlItemType.olMailItem);
            mailItem.Subject = subject;
            mailItem.To = to;
            mailItem.CC = cc;
            
            mailItem.Body = message;
            //  mailItem.BodyFormat = OlBodyFormat.olFormatPlain;
            mailItem.Send();
        }

        public static void SendMail(String to,String cc, String subject, String message, String path)
        {
            Application oApp = new Application();
            MailItem mailItem = (MailItem)oApp.CreateItem(OlItemType.olMailItem);
            mailItem.Subject = subject;
            mailItem.To = to;
            mailItem.CC = cc;
            Attachment attachmnt = mailItem.Attachments.Add(path, OlAttachmentType.olEmbeddeditem);
            mailItem.HTMLBody = message;
            //  mailItem.BodyFormat = OlBodyFormat.olFormatPlain;
            mailItem.Send();

        }
    }
}
