using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Net.Mail;
using Microsoft.SharePoint.Administration;

using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;

namespace TrainingRequestWFProj.TrainingMaterialsWF.HelperClass
{
    public partial class EmailHelper
    {
        /// <summary>
        /// Builds the message body
        /// </summary>
        /// <returns>Message body string</returns>
        public static string BuildBodyHTML(string itemTitle, string itemDept, string itemLifecycle, string itemReqType, string itemPlanned, string reqLink, string taskLink)
        {
            StringBuilder mailBody = new StringBuilder();

            // styles
            mailBody.Append("<style type='text/css'>");
            mailBody.Append("th{text-align:left;}");
            mailBody.Append("table{margin-left:25px; border: none;}");
            mailBody.Append("</style>");

            // message header
            mailBody.Append("<div><b>Attention:</b> The following training request task has been assigned to you.  Please review the request and take action on the task.</div><br><br>");

            // Request info section
            mailBody.Append("<table>");
            mailBody.Append(string.Format("<tr><th>Training Request Title:</th><td>{0}</td></tr>", itemTitle));
            mailBody.Append(string.Format("<tr><th>Department:</th><td>{0}</td></tr>", itemDept));
            mailBody.Append(string.Format("<tr><th>Lifecycle:</th><td>{0}</td></tr>", itemLifecycle));
            mailBody.Append(string.Format("<tr><th>Request Type:</th><td>{0}</td></tr>", itemReqType));
            mailBody.Append(string.Format("<tr><th>Planned Training:</th><td>{0}</td></tr>", itemPlanned));
            //mailBody.Append("</table>");



            // Link section
            //mailBody.Append("<table style='margin-left: 25px; border: none'>");
            mailBody.Append(string.Format("<tr><th>Request Link:</th><td><a href='{0}'>Click Here</a></td></tr>", reqLink));
            mailBody.Append(string.Format("<tr><th>Task Link:</th><td><a href='{0}'>Click Here</a></td></tr>", taskLink));
            mailBody.Append("</table>");
            return mailBody.ToString();
        }// end BuildBodyHTML


        /// <summary>
        /// Sends an email through SP outbound mail service
        /// </summary>
        /// <param name="Recipient">single or delimited with commas</param>
        /// <param name="Subject"></param>
        /// <param name="Body">HTML formatted body</param>
        public static void SendNotification(SPWeb web, string Recipient, string Subject, string Body)
        {
            try
            {
                SPUtility.SendEmail(web, true, false, Recipient, Subject, Body);
                //SmtpClient mailClient = new SmtpClient(SPAdministrationWebApplication.Local.OutboundMailServiceInstance.Server.Address);

                //MailMessage message = new MailMessage();
                //message.From = new MailAddress(SPAdministrationWebApplication.Local.OutboundMailSenderAddress);
                //message.To.Add(Recipient);
                //message.Subject = Subject;
                //message.IsBodyHtml = true;
                //message.Body = Body;

                //mailClient.Send(message);
                //message.Dispose();
            }
            catch (Exception Err)
            {
                LogWriting.LogExceptionToSP("SendNotification()", Err);

            }

        }// end sendnotification
    }
}
