using System;
using System.DirectoryServices.AccountManagement;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Oasis.Common;
using Partner.Administration;
using Partner.Infrastructure;

namespace Partner.MiscWebParts.Layouts.Partner.MiscWebParts.Pages
{
    public partial class ToggleLock : LayoutsPageBase
    {
        private String identity = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            identity = (!String.IsNullOrEmpty(this.Page.Request.QueryString["identity"])) ? this.Page.Request.QueryString["identity"] : String.Empty;

            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    PrincipalContext context = new PrincipalContext(ContextType.Domain, SPContext.Current.Site.WebApplication.Farm.GetProperty(PropertyBags.PartnerImport_UserDomain));
                    UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(context, identity);
                    btnSubmit.Text = "Unlock";
                });
            }
            catch (Exception ex)
            {
                Label2.Text = "Error: " + ex.Message;
            }
        }

        protected virtual void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    if (PartnerHelper.CurrentUserIsAdmin())
                    {
                        PrincipalContext context = new PrincipalContext(ContextType.Domain, SPContext.Current.Site.WebApplication.Farm.GetProperty(PropertyBags.PartnerImport_UserDomain));
                        UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(context, identity);
                        userPrincipal.UnlockAccount();
                        userPrincipal.Save();

                        // close dialog...
                        Context.Response.Write("<script type='text/javascript'>window.frameElement.commitPopup();</script>");
                        Context.Response.Flush();
                        Context.Response.End();

                        String msg = String.Format("{0} account unlocked by {1}", identity, SPContext.Current.Web.CurrentUser.Name);
                        LogHelper.LogInformation(msg, 4003);
                    }
                    else
                    {
                        Label2.Text = "Access denied.";
                    }
                });
            }
            catch (Exception ex)
            {
                Label2.Text = "Error: " + ex.Message;
                String msg = String.Format("Error: Unable to unlock acount for {0}\nAttempt by {1}\nDomain: {2}\nError: ",
                    identity,
                    SPContext.Current.Web.CurrentUser.Name,
                    SPContext.Current.Site.WebApplication.Farm.GetProperty(PropertyBags.PartnerImport_UserDomain));
                LogHelper.LogError(msg, ex, 4103);
            }
        }

    }
}
