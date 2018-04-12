using System;
using System.DirectoryServices.AccountManagement;
using Microsoft.IdentityModel.Claims;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration.Claims;
using Microsoft.SharePoint.WebControls;
using Oasis.Common;
using Partner.Administration;
using Partner.Infrastructure;

namespace Partner.MiscWebParts.Layouts.Partner.MiscWebParts.Pages
{
    public partial class ChangeEmail : LayoutsPageBase
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
                    Label2.Text = userPrincipal.EmailAddress;
                });
            }
            catch (Exception ex)
            {
                Label4.Text = "Error: " + ex.Message;
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
                        userPrincipal.EmailAddress = (TextBox1.Text.Length > 0) ? TextBox1.Text : null;
                        userPrincipal.Save();
                        // Label2.Text = userPrincipal.EmailAddress;
                        // Label4.Text = "Email address changed.";

                        using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                        {
                            try
                            {
                                site.RootWeb.AllowUnsafeUpdates = true;
                                SPUser user = site.RootWeb.SiteUsers[LoginEncodedTrustedClaim(site, userPrincipal.SamAccountName)];
                                user.Email = (TextBox1.Text.Length > 0) ? TextBox1.Text : null;
                                user.Update();

                                String msg = String.Format("Email changed for {0} by {1}", identity, SPContext.Current.Web.CurrentUser.Name);
                                LogHelper.LogInformation(msg, 4001);
                            }
                            catch (Exception ex)
                            {
                                String msg = String.Format("Error: Unable to change email address for {0}\nAttempt by {1}\nDomain: {2}\nError: ",
                                    identity,
                                    SPContext.Current.Web.CurrentUser.Name,
                                    SPContext.Current.Site.WebApplication.Farm.GetProperty(PropertyBags.PartnerImport_UserDomain));
                                LogHelper.LogError(msg, ex, 4101);
                            }
                            finally
                            {
                                site.RootWeb.AllowUnsafeUpdates = false;
                            }
                        }

                        // close dialog...
                        Context.Response.Write("<script type='text/javascript'>window.frameElement.commitPopup();</script>");
                        Context.Response.Flush();
                        Context.Response.End();
                    }
                    else
                    {
                        Label4.Text = "Access denied.";
                    }
                });
            }
            catch (Exception ex)
            {
                Label4.Text = "Error: " + ex.Message;
                String msg = String.Format("Error: Unable to change email address for {0}\nAttempt by {1}\nDomain: {2}\nError: ",
                    identity,
                    SPContext.Current.Web.CurrentUser.Name,
                    SPContext.Current.Site.WebApplication.Farm.GetProperty(PropertyBags.PartnerImport_UserDomain));
                LogHelper.LogError(msg, ex, 4102);
            }
        }

        internal String LoginEncodedTrustedClaim(SPSite site, String loginName)
        {
            String provider = String.Format("TrustedProvider:{0}", site.WebApplication.Farm.GetProperty(PropertyBags.PartnerImport_ClaimsProvider));
            SPClaimProviderManager manager = SPClaimProviderManager.Local;
            SPClaim claim = new SPClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsaccountname", loginName, ClaimValueTypes.String, provider);
            return manager.EncodeClaim(claim);
        }

    }
}
