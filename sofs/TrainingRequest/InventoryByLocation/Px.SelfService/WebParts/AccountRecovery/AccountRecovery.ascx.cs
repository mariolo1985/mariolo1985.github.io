using System;
using System.ComponentModel;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Px.Common;

namespace Px.SelfService.WebParts.AccountRecovery
{
    [ToolboxItemAttribute(false)]
    public partial class AccountRecovery : WebPart
    {
        public AccountRecovery()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();

            // disable validation controls if page is in not in display mode...
            if (WebPartManager.GetCurrentWebPartManager(Page).DisplayMode != WebPartManager.BrowseDisplayMode)
            {
                RequiredFieldValidator.Enabled = false;
                RegularExpressionValidator.Enabled = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ChromeType = PartChromeType.None;
            rbn_Password.Attributes.Add("onclick", "PasswordSelected()");
            rbn_Username.Attributes.Add("onclick", "UsernameSelected()");
        }

        protected void btn_Recover_Click(object sender, EventArgs e)
        {
            if (rbn_Password.Checked && !rbn_Username.Checked)
            {
                PasswordRecovery();
            }
            else if (!rbn_Password.Checked && rbn_Username.Checked)
            {
                UsernameRecovery();
            }
        }

        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(SPContext.Current.Site.Url, false);
        }

        protected void PasswordRecovery()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite elevatedSite = new SPSite(SPContext.Current.Site.ID))
                {
                    try
                    {
                        var redirect = false;
                        var context = new PrincipalContext(ContextType.Domain, elevatedSite.GetProperty(Constants.Property_ActiveDirectoryDomain));
                        var userPrincipal = UserPrincipal.FindByIdentity(context, txb_Prompt.Text);

                        if (userPrincipal != null)                        
                        {
                            elevatedSite.RootWeb.AllowUnsafeUpdates = true;

                            try
                            {
                                SPList list = elevatedSite.RootWeb.Lists[Constants.List_PasswordReset];
                                SPListItem item = list.AddItem();
                                item["Login Name"] = txb_Prompt.Text;
                                item["Email Address"] = userPrincipal.EmailAddress;
                                item.Update();

                                Guid data = item.UniqueId;
                                String href = String.Format("{0}?data={1}", elevatedSite.GetProperty(Constants.Property_PasswordRecoveryUrl), data.ToString());

                                String subject = elevatedSite.GetProperty(Constants.Property_PasswordRecoverySubject);
                                String body = elevatedSite.GetProperty(Constants.Property_PasswordRecoveryBody);

                                subject = subject.Replace("[username]", userPrincipal.SamAccountName);
                                subject = subject.Replace("[email]", userPrincipal.EmailAddress);
                                body = body.Replace("[username]", userPrincipal.SamAccountName);
                                body = body.Replace("[email]", userPrincipal.EmailAddress);
                                body = body.Replace("[link]", href);

                                EmailHelper.SendEmail(elevatedSite, userPrincipal.EmailAddress, subject, body);

                                var log = String.Empty;
                                if (SPContext.Current.Site.GetProperty(Constants.Property_PxDebug) == "True")
                                    log = String.Format("Subject: {0} - Body: {1}", subject, body);
                                
                                LogHelper.LogMessage(SPContext.Current.Site, "AccountRecovery.PasswordRecovery", "Password Recovery Email Sent", log, "WebPart", "Information");
                                redirect = true;
                            }
                            catch (Exception ex)
                            {
                                LogHelper.LogMessage(SPContext.Current.Site, "AccountRecovery.PasswordRecovery", ex.Message, ex.StackTrace, "WebPart", "Error");
                                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script type='text/javascript'>Dialog('Unknown Error. Please Contact your PCA for assistance.');</script>");
                            }
                            finally
                            {
                                elevatedSite.RootWeb.AllowUnsafeUpdates = false;
                            }
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script type='text/javascript'>Dialog('Unknown Error. Please Contact your PCA for assistance.');</script>");
                        }

                        if (redirect) 
                            Page.Response.Redirect(SPContext.Current.Site.Url, false);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.LogMessage(SPContext.Current.Site, "AccountRecovery.PasswordRecovery", ex.Message, ex.StackTrace, "WebPart", "Error");
                        Page.ClientScript.RegisterStartupScript(GetType(), "", "<script type='text/javascript'>Dialog('Unknown Error. Please Contact your PCA for assistance.');</script>");
                    }
                }
            });
        }

        protected void UsernameRecovery()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite elevatedSite = new SPSite(SPContext.Current.Site.ID))
                {
                    var redirect = false;

                    try
                    {
                        var emailAddress = txb_Prompt.Text;
                        var userPrincipal = ActiveDirectoryHelper.GetUserPrincipalByEmail(elevatedSite, emailAddress);

                        if (userPrincipal != null)
                        {
                            String subject = elevatedSite.GetProperty(Constants.Property_UsernameRecoverySubject);
                            String body = elevatedSite.GetProperty(Constants.Property_UsernameRecoveryBody);

                            subject = subject.Replace("[username]", userPrincipal.SamAccountName);
                            subject = subject.Replace("[email]", emailAddress);
                            body = body.Replace("[username]", userPrincipal.SamAccountName);
                            body = body.Replace("[email]", emailAddress);

                            EmailHelper.SendEmail(elevatedSite, userPrincipal.EmailAddress, subject, body);

                            var log = String.Empty;
                            if (SPContext.Current.Site.GetProperty(Constants.Property_PxDebug) == "True")
                                log = String.Format("Subject: {0} - Body: {1}", subject, body);
                            
                            LogHelper.LogMessage(SPContext.Current.Site, "AccountRecovery.UsernameRecovery", "Username Recovery Email Sent", log, "WebPart", "Information");

                            redirect = true;
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script type='text/javascript'>Dialog('Unknown Error. Please Contact your PCA for assistance.');</script>");
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.LogMessage(SPContext.Current.Site, "AccountRecovery.UsernameRecovery", ex.Message, ex.StackTrace, "WebPart", "Error");
                        Page.ClientScript.RegisterStartupScript(GetType(), "", "<script type='text/javascript'>Dialog('Unknown Error. Please Contact your PCA for assistance.');</script>");
                    }
                    finally
                    {
                        elevatedSite.RootWeb.AllowUnsafeUpdates = false;
                    }

                    if (redirect)
                        Page.Response.Redirect(SPContext.Current.Site.Url, false);
                }
            });
        }

    }
}
