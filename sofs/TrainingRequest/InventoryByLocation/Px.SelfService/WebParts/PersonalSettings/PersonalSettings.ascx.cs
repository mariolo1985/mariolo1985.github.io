using System;
using System.ComponentModel;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using System.Xml.Linq;
using Microsoft.SharePoint;
using Px.Common;

namespace Px.SelfService.WebParts.PersonalSettings
{
    [ToolboxItemAttribute(false)]
    public partial class PersonalSettings : WebPart
    {
        public PersonalSettings()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            try
            {
                base.OnInit(e);
                InitializeControl();

                // disable validation controls if page is in not in display mode...
                if (WebPartManager.GetCurrentWebPartManager(Page).DisplayMode != WebPartManager.BrowseDisplayMode)
                {
                    dummyValidator.Enabled = false;

                    foreach (var validator in Page.GetValidators("ChangeName"))
                    {
                        (validator as BaseValidator).Enabled = false;
                    }
                    foreach (var validator in Page.GetValidators("ChangePassword"))
                    {
                        (validator as BaseValidator).Enabled = false;
                    }
                    foreach (var validator in Page.GetValidators("ChangePhoneNumber"))
                    {
                        (validator as BaseValidator).Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "PersonalSettings.OnInit", ex.Message, ex.StackTrace, "WebPart", "Error");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ChromeType = PartChromeType.None;
                //Page.ClientScript.RegisterOnSubmitStatement(this.GetType(), "val", "OnUpdateValidators();");

                if (Context.User.Identity.IsAuthenticated)
                {
                    // Shared Navigation...
                    try
                    {
                        Boolean isDialog = (!String.IsNullOrEmpty(Page.Request.QueryString["IsDlg"])) ? Page.Request.QueryString["IsDlg"] == "1" : false;
                        if (!isDialog)
                        {
                            ltr_SharedNav.Text = StreamHelper.ReadWebRequest(SPContext.Current.Site.GetProperty(Constants.Property_SharedNavigation));
                        }
                    }
                    catch (Exception ex)
                    {
                        ltr_SharedNav.Text = "Shared Navigation Error: " + ex.Message;
                    }

                    // Username...
                    try
                    {
                        ltr_Username.Text = String.Format("<div id='hidden-username' style='display:none;'>{0}</div>", SPContext.Current.Web.CurrentUser.Name);
                    }
                    catch (Exception ex)
                    {
                        ltr_Username.Text = String.Format( "<div id='error-username' style='display:none;'>PxUsername Error: {0}</div>", ex.Message);
                    }

                    if (!Page.IsPostBack)
                    {
                        LoadSettings();
                    }

                    if (Page.IsPostBack)
                    {
                        txb_CurrentPassword.Attributes.Add("value", txb_CurrentPassword.Text);
                        txb_NewPassword.Attributes.Add("value", txb_NewPassword.Text);
                        txb_ConfirmNewPassword.Attributes.Add("value", txb_ConfirmNewPassword.Text);
                    }
                }
                else
                {
                    Page.Response.Redirect("/_layouts/15/Authenticate.aspx?Source=%2F", false);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "PersonalSettings.Page_Load", ex.Message, ex.StackTrace, "WebPart", "Error");
            }
        }

        protected void LoadSettings()
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    var context = new PrincipalContext(ContextType.Domain, SPContext.Current.Site.GetProperty(Constants.Property_ActiveDirectoryDomain));
                    var userPrincipal = UserPrincipal.FindByIdentity(context, SPContext.Current.Web.CurrentUser.Name);

                    if (userPrincipal != null)
                    {
                        ltr_DisplayName.Text = String.IsNullOrEmpty(userPrincipal.DisplayName) ? userPrincipal.Name : userPrincipal.DisplayName;
                        txb_FirstName.Text = userPrincipal.GivenName;
                        txb_LastName.Text = userPrincipal.Surname;
                        ltr_DisplayPhoneNumber.Text = userPrincipal.VoiceTelephoneNumber;
                        txb_NewPhoneNumber.Text = userPrincipal.VoiceTelephoneNumber;
                    }
                });
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "PersonalSettings.LoadSettings", ex.Message, ex.StackTrace, "WebPart", "Error");
            }
        }

        protected void ShowChangeName(object sender, EventArgs e)
        {
            ResetVisibility();
            section_DisplayName.Visible = false;
            section_ChangeName.Visible = true;
        }

        protected void HideChangeName(object sender, EventArgs e)
        {
            LoadSettings();
            ResetVisibility();
        }

        protected void ShowChangePassword(object sender, EventArgs e)
        {
            ResetVisibility();
            section_DisplayPassword.Visible = false;
            section_ChangePassword.Visible = true;
        }

        protected void HideChangePassword(object sender, EventArgs e)
        {
            LoadSettings();
            ResetVisibility();
        }

        protected void ShowChangePhoneNumber(object sender, EventArgs e)
        {
            ResetVisibility();
            section_DisplayPhoneNumber.Visible = false;
            section_ChangePhoneNumber.Visible = true;

        }

        protected void HideChangePhoneNumber(object sender, EventArgs e)
        {
            LoadSettings();
            ResetVisibility();
        }

        protected void ResetVisibility()
        {
            section_DisplayName.Visible = true;
            section_ChangeName.Visible = false;
            section_DisplayPassword.Visible = true;
            section_ChangePassword.Visible = false;
            section_DisplayPhoneNumber.Visible = true;
            section_ChangePhoneNumber.Visible = false;
        }

        protected void ValidatePassword(object sender, EventArgs e)
        {
            try
            {
                var validSpecialCharacters = @"~!@#$%^&*_-+=`|\(){}[]:;""'<>,.?/".ToCharArray();
                var alphaCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxwy".ToCharArray();
                var lowercaseAlphaCharacters = "abcdefghijklmnopqrstuvwxwy".ToCharArray();
                var uppercaseAlphaCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
                var numericCharacters = "0123456789".ToCharArray();

                if (txb_NewPassword.Text.IndexOfAny(validSpecialCharacters) == -1 ||
                    txb_NewPassword.Text.IndexOfAny(alphaCharacters) == -1 ||
                    txb_NewPassword.Text.IndexOfAny(numericCharacters) == -1 ||
                    txb_NewPassword.Text.Length < 8)
                {
                    custom_Password.ErrorMessage = "Password does not <br /> meet requirements.";
                    custom_Password.IsValid = false;
                    compare_ConfirmPassword.Validate();
                }
                else if (txb_NewPassword.Text == SPContext.Current.Web.CurrentUser.Name)
                {
                    custom_Password.ErrorMessage = "Password must not <br /> match Username.";
                    custom_Password.IsValid = false;
                    compare_ConfirmPassword.Validate();
                }
                else if (txb_NewPassword.Text == txb_CurrentPassword.Text)
                {
                    custom_Password.ErrorMessage = "Password must not <br /> match current password.";
                    custom_Password.IsValid = false;
                    compare_ConfirmPassword.Validate();
                }
                else
                {
                    custom_Password.IsValid = true;
                    compare_ConfirmPassword.Validate();
                }

                txb_ConfirmNewPassword.Focus();
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "UserRegistration.ValidatePassword", ex.Message, ex.StackTrace, "WebPart", "Error");
            }
        }

        protected void ChangeName_Click(object sender, EventArgs e)
        {
            try
            {
                require_FirstName.Enabled = true;
                require_LastName.Enabled = true;
                Page.Validate("ChangeName");
                if (Page.IsValid)
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        var context = new PrincipalContext(ContextType.Domain, SPContext.Current.Site.GetProperty(Constants.Property_ActiveDirectoryDomain));
                        var userPrincipal = UserPrincipal.FindByIdentity(context, SPContext.Current.Web.CurrentUser.Name);

                        if (userPrincipal != null)
                        {
                            userPrincipal.GivenName = String.IsNullOrEmpty(txb_FirstName.Text) ? " " : txb_FirstName.Text;
                            userPrincipal.Surname = String.IsNullOrEmpty(txb_LastName.Text) ? " " : txb_LastName.Text;
                            userPrincipal.DisplayName = txb_FirstName.Text + " " + txb_LastName.Text;
                            userPrincipal.Save();

                            ltr_DisplayName.Text = String.IsNullOrEmpty(userPrincipal.DisplayName) ? userPrincipal.Name : userPrincipal.DisplayName;
                        }

                        LoadSettings();
                        ResetVisibility();
                    });
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "PersonalSettings.ChangeName_Click", ex.Message, ex.StackTrace, "WebPart", "Error");
            }
        }

        protected void ChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                require_CurrentPassword.Enabled = true;
                require_NewPassword.Enabled = true;
                Page.Validate("ChangePassword");
                if (Page.IsValid)
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        try
                        {
                            SPContext.Current.Web.CurrentUser.ChangePassword(txb_CurrentPassword.Text, txb_NewPassword.Text);

                            LoadSettings();
                            ResetVisibility();
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.Contains("0x80070056"))
                                // The old password is incorrect.
                                ScriptManager.RegisterStartupScript(UpdatePanelOne, UpdatePanelOne.GetType(), "", "Dialog('The old password is incorrect.');", true);
                            else
                                // Unable to change password at this time.
                                ScriptManager.RegisterStartupScript(UpdatePanelOne, UpdatePanelOne.GetType(), "", "Dialog('Unable to change password at this time.');", true);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "PersonalSettings.ChangePassword_Click", ex.Message, ex.StackTrace, "WebPart", "Error");
            }
        }

        protected void ChangePhoneNumber_Click(object sender, EventArgs e)
        {
            try
            {
                require_PhoneNumber.Enabled = true;
                Page.Validate("ChangePhoneNumber");
                if (Page.IsValid)
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        var context = new PrincipalContext(ContextType.Domain, SPContext.Current.Site.GetProperty(Constants.Property_ActiveDirectoryDomain));
                        var userPrincipal = UserPrincipal.FindByIdentity(context, SPContext.Current.Web.CurrentUser.Name);

                        if (userPrincipal != null)
                        {
                            userPrincipal.VoiceTelephoneNumber = String.IsNullOrEmpty(txb_NewPhoneNumber.Text) ? " " : txb_NewPhoneNumber.Text;
                            userPrincipal.Save();
                            ltr_DisplayPhoneNumber.Text = userPrincipal.VoiceTelephoneNumber;
                        }

                        LoadSettings();
                        ResetVisibility();
                    });
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "PersonalSettings.ChangePhoneNumber_Click", ex.Message, ex.StackTrace, "WebPart", "Error");
            }
        }

    }
}
