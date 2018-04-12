using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Oasis.Common;
using Partner.Administration;
using Partner.Infrastructure;

namespace Partner.MiscWebParts.Layouts.Partner.MiscWebParts.Pages
{
    public partial class NewUser : LayoutsPageBase
    {
        private String identity = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblUsernameValidation.Visible = false;
            lblEmailValidation.Visible = false;
            lblPartnerValidation.Visible = false;
            lblStatus.Text = String.Empty;
            lblStatus.Visible = true;
        }

        protected virtual void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Web.Site.ID))
                    {
                        // if (PartnerHelper.CurrentUserIsAdmin())
                        if (PartnerHelper.CurrentUserCanOnboard())
                        {
                            // do stuff with things...
                            if (IsValidActiveDirectoryLoginName(tbUsername.Text))
                            {
                                if (!UserExists(site, tbUsername.Text))
                                {
                                    if (tbDisplayName.Text.Length == 0)
                                    {
                                        tbDisplayName.Text = tbUsername.Text;
                                    }

                                    if (ValidEmailAddresses(tbEmail.Text))
                                    {
                                        if (PartnerExists(site, tbPartnerId.Text))
                                        {
                                            tbPartnerName.Text = GetPartnerName(site, tbPartnerId.Text);
                                            Provision(site, tbUsername.Text, tbDisplayName.Text, tbEmail.Text, tbPartnerId.Text, tbPartnerName.Text);                                            
                                        }
                                        else
                                        {
                                            if (tbPartnerId.Text.Length > 0)
                                            {
                                                if (tbPartnerName.Text.Length == 0)
                                                {
                                                    lblPartnerValidation.Text = "Please provide partner name.";
                                                    lblPartnerValidation.Visible = true;
                                                }
                                                else
                                                {
                                                    Provision(site, tbUsername.Text, tbDisplayName.Text, tbEmail.Text, tbPartnerId.Text, tbPartnerName.Text);
                                                }
                                            }
                                            else
                                            {
                                                lblPartnerValidation.Text = "Please provide partner id.";
                                                lblPartnerValidation.Visible = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        lblEmailValidation.Text = "Valid email address is required!";
                                        lblEmailValidation.Visible = true;
                                    }
                                }
                                else
                                {
                                    lblUsernameValidation.Text = "User already exists!";
                                    lblUsernameValidation.Visible = true;
                                }
                            }
                            else
                            {
                                lblUsernameValidation.Text = "Not a valid login name!";
                                lblUsernameValidation.Visible = true;
                            }
                        }
                        else
                        {
                            lblStatus.ForeColor = System.Drawing.Color.Red;
                            lblStatus.Text = "Access denied.";

                        }
                    }
                });
            }
            catch (Exception ex)
            {
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Text = ex.Message;
            }
        }

        protected void Provision(SPSite site, String userName, String displayName, String email, String partnerId, String partnerName)
        {
            try
            {
                String password = ParnterProvisioningHelper.ProvisionUser(site, userName, displayName, email, partnerId, partnerName);
                ParnterProvisioningHelper.ProvisionPartner(site, userName, email, partnerId, partnerName);

                tbUsername.Text = String.Empty;
                tbDisplayName.Text = String.Empty;
                tbEmail.Text = String.Empty;
                tbPartnerId.Text = String.Empty;
                tbPartnerName.Text = String.Empty;

                String status = String.Format("User account {0} created... password: <font face='courier'>{1}</font><br>Partner: {2} ({3})", userName, password, partnerName, partnerId);
                lblStatus.Text = status;
            }
            catch (Exception ex)
            {
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Text = ex.Message;
            }
        }

        public static Boolean IsValidActiveDirectoryLoginName(String loginName)
        {
            Boolean result = false;
            String validCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890.";
            // String optionalvalidCharacters = "`~!@#$%^&(){}'-_";

            if (!loginName.StartsWith("."))
            {
                foreach (Char chr in loginName.ToCharArray())
                {
                    result = (validCharacters.Contains(chr.ToString()));
                    if (!result) break;
                }
            }

            return result;
        }

        public static Boolean UserExists(SPSite site, String loginName)
        {
            Boolean result = false;
            try
            {
                String domain = site.WebApplication.Farm.GetProperty(PropertyBags.PartnerImport_UserDomain);
                String container = site.WebApplication.Farm.GetProperty(PropertyBags.PartnerImport_UserContainer);
                PrincipalContext context = new PrincipalContext(ContextType.Domain, domain, container);
                if (UserPrincipal.FindByIdentity(context, loginName) != null)
                {
                    result = true;
                }
            }
            catch { }
 
            return result;
        }

        public static Boolean ValidEmailAddresses(String addresses)
        {
            Boolean result = false;
            Boolean badAddressFound = false;
            Regex regex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");

            List<String> addressList = new List<String>(addresses.Split(';'));
            foreach (String address in addressList)
            {
                if (!regex.IsMatch(address))
                {
                    badAddressFound = true;
                }
            }

            result = (badAddressFound) ? false : true;
            
            return result;
        }

        public static Boolean PartnerExists(SPSite site, String partnerId)
        {
            SPList partnerMaster = site.RootWeb.Lists[PartnerConstants.List_PartnerMaster];
            SPQuery query = new SPQuery();
            query.Query = String.Format("<Where><Eq><FieldRef Name='Partner_x0020_ID' /><Value Type='Text'>{0}</Value></Eq></Where>", partnerId);
            query.RowLimit = 1;
            SPListItemCollection collection = partnerMaster.GetItems(query);

            return (collection.Count > 0);
        }

        public static String GetPartnerName(SPSite site, String partnerId)
        {
            SPList partnerMaster = site.RootWeb.Lists[PartnerConstants.List_PartnerMaster];
            SPQuery query = new SPQuery();
            query.Query = String.Format("<Where><Eq><FieldRef Name='Partner_x0020_ID' /><Value Type='Text'>{0}</Value></Eq></Where>", partnerId);
            query.RowLimit = 1;
            SPListItemCollection collection = partnerMaster.GetItems(query);

            return collection[0].GetFormattedValue(PartnerConstants.Column_Title);
        }
    
    }
}
