using System;
using System.ComponentModel;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Px.Common;

namespace Px.SelfService.WebParts.RecoverPassword
{
    [ToolboxItemAttribute(false)]
    public partial class RecoverPassword : WebPart
    {
        protected SPListItem Item;
        protected String Username;
        protected DateTime Created;
        protected TimeSpan Age;
        protected Int32 TimeToLive;

        public RecoverPassword()
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
                CompareValidator.Enabled = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ChromeType = PartChromeType.None;
            String data = (!String.IsNullOrEmpty(Page.Request.QueryString["data"])) ? Page.Request.QueryString["data"] : String.Empty;

            if (String.IsNullOrEmpty(data))
            {
                ltr_Notification.Text = "This link has expired or is invalid. Please try your request again.";
                pnl_Notifications.Visible = true;
                // Page.Response.Redirect(SPContext.Current.Site.Url, false);
            }
            else
            {
                Item = GetPassowrdResetItem(data);

                if (Item != null)
                {
                    try
                    {
                        Username = Item.GetFormattedValue("Login Name");
                        Created = Item.ReadDateTimeField(Constants.Field_Created);
                        Age = DateTime.Now.Subtract(Created);
                        TimeToLive = Convert.ToInt32(SPContext.Current.Site.GetProperty(Constants.Property_PasswordRecoveryTtl));

                        if (Age.Minutes > TimeToLive)
                        {
                            ltr_Notification.Text = "This link has expired or is invalid. Please try your request again.";
                            pnl_Notifications.Visible = true;
                        }

                        // enable btn_Change...
                        btn_Change.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.LogMessage(SPContext.Current.Site, "RecoverPassword.Page_Load", ex.Message, ex.StackTrace, "Web Part", "Error");
                    }
                }
                else
                {
                    ltr_Notification.Text = "This link has expired or is invalid. Please try your request again.";
                    pnl_Notifications.Visible = true;
                }
            }
        }

        protected void btn_Change_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (Age.Minutes < TimeToLive)
                {                    
                    try
                    {
                        var validSpecialCharacters = @"~!@#$%^&*_-+=`|\(){}[]:;""'<>,.?/".ToCharArray();
                        var alphaCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxwy".ToCharArray();
                        var lowercaseAlphaCharacters = "abcdefghijklmnopqrstuvwxwy".ToCharArray();
                        var uppercaseAlphaCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
                        var numericCharacters = "0123456789".ToCharArray();

                        if (txb_Prompt.Text.IndexOfAny(validSpecialCharacters) == -1 ||
                            txb_Prompt.Text.IndexOfAny(alphaCharacters) == -1 ||
                            txb_Prompt.Text.IndexOfAny(numericCharacters) == -1 ||
                            txb_Prompt.Text.Length < 8)
                        {
                            ltr_Notification.Text = "Password does not <br /> meet requirements.";
                            pnl_Notifications.Visible = true;
                        }
                        else if (txb_Prompt.Text == Username)
                        {
                            ltr_Notification.Text = "Password must not <br /> match Username.";
                            pnl_Notifications.Visible = true;
                        }
                        else
                        {
                            UserHelper.SetPassword(SPContext.Current.Site, Username, txb_Prompt.Text);
                            LogHelper.LogMessage(SPContext.Current.Site, "RecoverPassword.btn_Change_Click",
                                                 "Password Reset", "Password Reset for " + Username, "Web Part",
                                                 "Information");
                            Page.Response.Redirect(SPContext.Current.Site.Url, false);
                        }
                    }
                    catch (PasswordException ex)
                    {
                        if (ex.Message.Contains("HRESULT: 0x800708C5"))
                        {
                            ltr_Notification.Text = "The password you entered has been used too recently.";
                            pnl_Notifications.Visible = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        ltr_Notification.Text = "Error: " + ex.Message;
                        pnl_Notifications.Visible = true;
                    }
                }
            }
        }

        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(SPContext.Current.Site.Url, false);
        }

        /*protected Boolean IsPasswordStrong(String password)
        {
            if (password.Length > 7)
            {
                var result = 0;

                if (password.IndexOfAny("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray()) >= 0) result++;
                if (password.IndexOfAny("0123456789".ToCharArray()) >= 0) result++;
                if (password.IndexOfAny("`!@$%^&*()-_=+[];:;\",<.>/?".ToCharArray()) >= 0) result++;

                return (result > 2);
            }
            else
            {
                return false;
            }
        }*/

        protected SPListItem GetPassowrdResetItem(String data)
        {
            SPListItem item = null;

            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite elevatedSite = new SPSite(SPContext.Current.Site.ID))
                {
                    try
                    {
                        elevatedSite.RootWeb.AllowUnsafeUpdates = true;
                        SPList list = elevatedSite.RootWeb.Lists[Constants.List_PasswordReset];
                        item = list.GetItemByUniqueId(new Guid(data));
                    }
                    catch (Exception ex)
                    {
                        LogHelper.LogMessage(SPContext.Current.Site, "RecoverPassword.GetPassowrdResetItem", ex.Message, ex.StackTrace, "Web Part", "Error");
                    }
                    finally
                    {
                        elevatedSite.RootWeb.AllowUnsafeUpdates = false;
                    }
                }
            });

            return item;
        }

    }
}
