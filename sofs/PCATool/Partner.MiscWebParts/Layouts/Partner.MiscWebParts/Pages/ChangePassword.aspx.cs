using System;
using System.DirectoryServices.AccountManagement;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Oasis.Common;
using Partner.Administration;
using Partner.Infrastructure;

namespace Partner.MiscWebParts.Layouts.Partner.MiscWebParts.Pages
{
    public partial class ChangePassword : LayoutsPageBase
    {
        private String identity = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            identity = (!String.IsNullOrEmpty(this.Page.Request.QueryString["identity"])) ? this.Page.Request.QueryString["identity"] : String.Empty;
        }

        protected virtual void btnSubmit_Click(object sender, EventArgs e)
        {
            string userName = SPContext.Current.Web.CurrentUser.Name;
            
            string msg = string.Empty;
            string strEmail = string.Empty;
            string keyPriDomain = string.Empty;
        
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    keyPriDomain = SPContext.Current.Site.WebApplication.Farm.GetProperty(PropertyBags.PartnerImport_UserDomain);

                    if (PartnerHelper.CurrentUserIsAdmin())
                    {
                        using (PrincipalContext context = new PrincipalContext(ContextType.Domain, keyPriDomain))
                        {
                            using (UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(context, identity))
                            {
                                if (TextBox1.Text == TextBox2.Text)
                                {
                                    strEmail = userPrincipal.EmailAddress;
                                    userPrincipal.SetPassword(TextBox1.Text);
                                    userPrincipal.Save();
                                    
                                    //Send Email
                                    if (!string.IsNullOrEmpty(strEmail))
                                    {
                                        SendPasswordChangeMail(strEmail);
                                    }

                                    // close dialog...
                                    Context.Response.Write("<script type='text/javascript'>window.frameElement.commitPopup();</script>");
                                    Context.Response.Flush();
                                    Context.Response.End();

                                    msg = string.Format("Password changed for ({0}). Email sent to {1}", userName, strEmail);
                                    LogHelper.LogInformation(msg, 4002);
                                }
                                else
                                {
                                    lblOutput.Text = "Passwords do not match.";
                                }
                            }
                        }
                    }
                    else
                    {
                        lblOutput.Text = "Access denied.";
                    }
                });
            }
            catch (Exception ex)
            {
                lblOutput.Text = "Error: " + ex.Message;

                msg = String.Format("Error: Unable to change password for {0}\nAttempt by {1}\nDomain: {2}\nError: ", identity, userName, keyPriDomain);
                LogHelper.LogError(msg, ex, 4103);
            }
        }

        private void SendPasswordChangeMail(string strEmail)
        {
            try
            {
                string from = "partneroasispassword@overstock.com";
                string reply = "partneroasispassword@overstock.com";
                string recipients = strEmail;
                string cc = string.Empty;
                string bcc = string.Empty;
                string subject = "Overstock.com Partner Oasis Password Update";
                string body = string.Format("You are receiving this email because there has been activity on your Partner Oasis account. If you agree with the information displayed below, there is no action required on your part.\n\n");
                body += string.Format("Activity: Partner Oasis password changed\n Date: {0}\n Time: {1}\n\n", DateTime.Now.ToLocalTime().ToShortDateString(), DateTime.Now.ToLocalTime().ToShortTimeString());
                body += string.Format("If you did not recently change your password, please contact your Partner Care Associate directly.\n\n");
                body += string.Format("Note: Please do not respond to this email. This is an automatically generated email and replies to this message will not be read or responded to.");

                if (EmailHelper.ValidEmailAddresses(recipients))
                {
                    EmailHelper.SendEmail(from, reply, recipients, cc, bcc, subject, body);
                }
            }

            catch (Exception ex)
            {
                LogHelper.LogError("Unable to send Email to Partner.", ex, 4104);
            }
        }

    }
}
