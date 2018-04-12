using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Net.Mail;
using Microsoft.SharePoint.Administration;

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

            // message header
            mailBody.Append("<div><h3>Attention:</h3> The following training request task has been assigned to you.  Please review the request and take action on the task.</div><div></div>");

            // Request info section
            mailBody.Append("<table style='margin-left: 25px; border: none'>");
            mailBody.Append(string.Format("<tr><td>Training Request Title:</td><td>{0}</td></tr>", itemTitle));
            mailBody.Append(string.Format("<tr><td>Department:</td><td>{0}</td></tr>", itemDept));
            mailBody.Append(string.Format("<tr><td>Lifecycle:</td><td>{0}</td></tr>", itemLifecycle));
            mailBody.Append(string.Format("<tr><td>Request Type:</td><td>{0}</td></tr>", itemReqType));
            mailBody.Append(string.Format("<tr><td>Planned Training:</td><td>{0}</td></tr>", itemPlanned));
            mailBody.Append("</table>");

            mailBody.Append("<div></div>");

            // Link section
            mailBody.Append("<table style='margin-left: 25px; border: none'>");
            mailBody.Append(string.Format("<tr><td>Request Link:</td><td><a href='{0}'>Click Here</a></td></tr>", reqLink));
            mailBody.Append(string.Format("<tr><td>Task Link:</td><td><a href='{0}'>Click Here</a></td></tr>", taskLink));
            mailBody.Append("</table>");
            return mailBody.ToString();
        }// end BuildBodyHTML


        /// <summary>
        /// Sends an email through SP outbound mail service
        /// </summary>
        /// <param name="Recipient">single or delimited with commas</param>
        /// <param name="Subject"></param>
        /// <param name="Body">HTML formatted body</param>
        public static void SendNotification(string Recipient, string Subject, string Body)
        {
            try
            {
                SmtpClient mailClient = new SmtpClient(SPAdministrationWebApplication.Local.OutboundMailServiceInstance.Server.Address);

                MailMessage message = new MailMessage();
                message.From = new MailAddress(SPAdministrationWebApplication.Local.OutboundMailServiceInstance.Server.Address);
                message.To.Add(Recipient);
                message.Subject = Subject;
                message.IsBodyHtml = true;
                message.Body = Body;

                mailClient.Send(message);
                message.Dispose();
            }
            catch (Exception Err)
            {
                LogWriting.LogExceptionToSP("SendNotification()", Err);

            }

        }// end sendnotification
    }
}
