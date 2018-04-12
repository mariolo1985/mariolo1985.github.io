using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using System.Xml.Linq;
using Microsoft.SharePoint;
using Px.Common;


namespace Px.SelfService.WebParts.UserRegistration
{
    [ToolboxItemAttribute(false)]
    public partial class UserRegistration : WebPart
    {
        public UserRegistration()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();

            // disable validation controls if page is in not in display mode...
            if (WebPartManager.GetCurrentWebPartManager(Page).DisplayMode != WebPartManager.BrowseDisplayMode)
            {
                dummyValidator.Enabled = false;

                foreach (var validator in Page.GetValidators("StepOne"))
                {
                    (validator as BaseValidator).Enabled = false;
                }
                foreach (var validator in Page.GetValidators("StepTwo"))
                {
                    (validator as BaseValidator).Enabled = false;
                }
                foreach (var validator in Page.GetValidators("StepThree"))
                {
                    (validator as BaseValidator).Enabled = false;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                ChromeType = PartChromeType.None;

                //regex_PhoneNumber.ValidationExpression = SPContext.Current.Site.GetProperty(Constants.Property_RegexPhoneNumber);
                //regex_Password.ValidationExpression = SPContext.Current.Site.GetProperty(Constants.Property_RegexEmailAddress);
                
                Page.ClientScript.RegisterOnSubmitStatement(this.GetType(), "val", "OnUpdateValidators();");
                //chb_LegalEntity.Attributes.Add("onclick", "ToggleLegalEntity()");
                //chb_SameReturnsAddress.Attributes.Add("onclick", "ToggleProductReturnsAddress()");
                chb_ContactCustomerServiceSame.Attributes.Add("onclick", "CustomerServiceSameAsPrimary()");
                chb_ContactAccountsReceivableSame.Attributes.Add("onclick", "AccountsReceivableSameAsPrimary()");
                chb_ContactInventorySame.Attributes.Add("onclick", "InventorySameAsPrimary()");
                chb_ContactReturnsSame.Attributes.Add("onclick", "ReturnsSameAsPrimary()");
                foreach (ListItem item in rbl_ParticipateLtl.Items) item.Attributes.Add("onclick", "ToggleLTL(this);");
                
                // populate drop down lists...
                if (!Page.IsPostBack)
                {
                    ControlHelper.PopulateStates(ddl_State);
                    ControlHelper.PopulateStates(ddl_ReturnsState);
                    ControlHelper.PopulateStates(ddl_SmallParcelBillToState);
                    ControlHelper.PopulateStates(ddl_LtlBillToState);
                    ControlHelper.PopulateSmallParcelChoices(ddl_SmallParcelCarrier, ddl_SmallParcelServiceLevel);
                    ControlHelper.PopulateLtlChoices(ddl_LtlCarrier, ddl_LtlServiceLevel);                    
                }

                // populate fields with sample data...
                if (!Page.IsPostBack && SPContext.Current.Web.CurrentUser.IsSiteAdmin)
                {
                    if (SPContext.Current.Site.GetProperty(Constants.Property_PxDebug) == "True")
                    {
                        String id = DateTime.Now.ToString("MMddhhmmss");
                        txb_legalEntity.Text = "PxTest " + id;
                        txb_Username.Text = "test." + id;
                        txb_Password.Attributes.Add("value", "Passw0rd!");
                        txb_ConfirmPassword.Attributes.Add("value", "Passw0rd!");
                        txb_FirstName.Text = "PxTest";
                        txb_LastName.Text = id;
                        txb_EmailAddress.Text = "test@" + id + ".com";
                        txb_ConfirmEmail.Text = "test@" + id + ".com";
                        txb_PhoneNumber.Text = "123-123-1234";
                        txb_Address.Text = "123 Street Ave.";
                        txb_City.Text = "Cityville";
                        txb_Zip.Text = "12345";
                    }
                }

                // keep password on postback...
                if (Page.IsPostBack)
                {
                    txb_Password.Attributes.Add("value", txb_Password.Text);
                    txb_ConfirmPassword.Attributes.Add("value", txb_ConfirmPassword.Text);
                }

                // check querystring for print options...
                /*String option = (!String.IsNullOrEmpty(Page.Request.QueryString["option"])) ? Page.Request.QueryString["option"] : String.Empty;

                if (option == "terms")
                {
                    String html = "<script type='text/javascript'>window.onload=function(){window.print()};</script><h2>Terms & Conditions</h2>" 
                        + StreamHelper.ReadWebRequest(SPContext.Current.Site.GetProperty(Constants.Property_TermsConditions));
                    StreamHelper.PrintView(html);
                }
                else if (option == "3pterms")
                {
                    String html = "<script type='text/javascript'>window.onload=function(){window.print()};</script><h2>Third Party Terms & Conditions</h2>" 
                        + StreamHelper.ReadWebRequest(SPContext.Current.Site.GetProperty(Constants.Property_ThirdPartyTermsConditions));
                    StreamHelper.PrintView(html);
                }*/
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "UserRegistration", ex.Message, ex.StackTrace, "WebPart", "Error");
            }
        }

        protected void ToggleLegalEntity(object sender, EventArgs e)
        {
            legalEntity.Visible = chb_LegalEntity.Checked;
            require_CompanyName.Enabled = chb_LegalEntity.Checked;
        }

        protected void ToggleReturnsAddress(object sender, EventArgs e)
        {
            lbl_ReturnsAddress.CssClass = !chb_SameReturnsAddress.Checked ? "" : "disabledLabel";
            lbl_ReturnsSuite.CssClass = !chb_SameReturnsAddress.Checked ? "" : "disabledLabel";
            lbl_ReturnsCity.CssClass = !chb_SameReturnsAddress.Checked ? "" : "disabledLabel";
            lbl_ReturnsState.CssClass = !chb_SameReturnsAddress.Checked ? "" : "disabledLabel";
            lbl_ReturnsZip.CssClass = !chb_SameReturnsAddress.Checked ? "" : "disabledLabel";
            txb_ReturnsAddress.Enabled = !chb_SameReturnsAddress.Checked;
            txb_ReturnsSuite.Enabled = !chb_SameReturnsAddress.Checked;
            txb_ReturnsCity.Enabled = !chb_SameReturnsAddress.Checked;
            ddl_ReturnsState.Enabled = !chb_SameReturnsAddress.Checked;
            txb_ReturnsZip.Enabled = !chb_SameReturnsAddress.Checked;
            require_ReturnsAddress.Enabled = !chb_SameReturnsAddress.Checked;
            require_ReturnsCity.Enabled = !chb_SameReturnsAddress.Checked;
            require_ReturnsZip.Enabled = !chb_SameReturnsAddress.Checked;
        }

        protected void ActivateStep(Int32 step)
        {
            try
            {
                // breadcrumb...
                menu_StepOne.Attributes["class"] = (step == 1) ? "active first" : "first";
                menu_StepTwo.Attributes["class"] = (step == 2) ? "active" : "";
                menu_StepThree.Attributes["class"] = (step == 3) ? "active last" : "last";

                // form panels...
                panel_StepOne.Attributes["class"] = (step == 1) ? "activeStep" : "inactiveStep";
                panel_StepTwo.Attributes["class"] = (step == 2) ? "activeStep" : "inactiveStep";
                panel_StepThree.Attributes["class"] = (step == 3) ? "activeStep" : "inactiveStep";
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "UserRegistration.ActivateStep", ex.Message, ex.StackTrace, "WebPart", "Error");
            }            
        }

        protected void StepBackward(object sender, EventArgs e)
        {
            try
            {
                var btn = (Button)sender;
                switch (btn.CommandName)
                {
                    case "ReturnToStepOne":
                        ActivateStep(1);
                        break;
                    case "ReturnToStepTwo":
                        ActivateStep(2);
                        break;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "UserRegistration.ProcedeToStep", ex.Message, ex.StackTrace, "WebPart", "Error");
            }
        }

        protected void StepForward(object sender, EventArgs e)
        {
            try
            {
                var btn = (Button)sender;
                switch (btn.CommandName)
                {
                    case "ProceedToStepTwo":
                        if (UsernameIsValid() && EmailAddressIsValid())
                        {
                            Page.Validate("StepOne");
                            if (Page.IsValid) ActivateStep(2);
                            if (String.IsNullOrEmpty(txb_ContactPrimaryBusinessFirstName.Text))
                            {
                                txb_ContactPrimaryBusinessFirstName.Text = txb_FirstName.Text;
                                txb_ContactPrimaryBusinessLastName.Text = txb_LastName.Text;
                                txb_ContactPrimaryBusinessPhone.Text = txb_PhoneNumber.Text;
                                txb_ContactPrimaryBusinessEmail.Text = txb_ConfirmEmail.Text;
                            }
                        }
                        break;
                    case "ProceedToStepThree":
                        Page.Validate("StepTwo");
                        if (Page.IsValid) ActivateStep(3);
                        break;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "UserRegistration.ProcedeToStep", ex.Message, ex.StackTrace, "WebPart", "Error");
            }
        }

        protected void ValidateLegalEntity(object sender, EventArgs e)
        {
            try
            {
                if (LegalEntityIsTaken(txb_legalEntity.Text))
                {
                    custom_LegalEntity.ErrorMessage = "This company name is not available.";
                    custom_LegalEntity.IsValid = false;
                }
                else
                {
                    custom_LegalEntity.IsValid = true;
                }

                chb_LegalEntity.Focus();
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "UserRegistration.ValidateCompanyName", ex.Message, ex.StackTrace, "WebPart", "Error");
            }
        }

        protected Boolean LegalEntityIsTaken(String name)
        {
            Boolean result = false;

            try
            {
                var webUrl = WebRequestHelper.BuildGetCompanyByName(name);
                var xdoc = WebRequestHelper.GetSupplier(webUrl);
                var supplierId = xdoc != null ? WebRequestHelper.GetSupplierId(xdoc) : String.Empty;
                result = (!String.IsNullOrEmpty(supplierId));
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "UserRegistration.CompanyNameIsTaken", ex.Message, ex.StackTrace, "WebPart", "Error");
            }

            return result;

        }

        protected void ValidateUsername(object sender, EventArgs e)
        {
            try
            {
                using (var site = new SPSite(Page.Request.Url.ToString()))
                {
                    var invalidCharacters = @"""/\[]:;|=,+*?<>".ToCharArray();

                    if (txb_Username.Text.IndexOfAny(invalidCharacters) != -1)
                    {
                        custom_UserName.ErrorMessage = "Username does not <br /> meet requirements.";
                        custom_UserName.IsValid = false;
                    }
                    else if (ActiveDirectoryHelper.UsernameIsTaken(site, txb_Username.Text))
                    {
                        custom_UserName.ErrorMessage = "This username is not available.";
                        custom_UserName.IsValid = false;
                    }
                    else
                    {
                        custom_UserName.IsValid = true;
                    }
                }

                txb_Password.Focus();
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "UserRegistration.ValidateUsername", ex.Message, ex.StackTrace, "WebPart", "Error");
            }
        }

        protected Boolean UsernameIsValid()
        {
            try
            {
                using (var site = new SPSite(Page.Request.Url.ToString()))
                {                    
                    var invalidCharacters = @"""/\[]:;|=,+*?<>".ToCharArray();

                    if (txb_Username.Text.IndexOfAny(invalidCharacters) != -1)
                    {
                        custom_UserName.ErrorMessage = "Username does not <br /> meet requirements.";
                        custom_UserName.IsValid = false;
                        return false;
                    }
                    else if (ActiveDirectoryHelper.UsernameIsTaken(site, txb_Username.Text))
                    {
                        custom_UserName.ErrorMessage = "This username is not available.";
                        custom_UserName.IsValid = false;
                        return false;
                    }
                    else
                    {
                        custom_UserName.IsValid = true;
                        return true;
                    }
                }

                // ...
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "UserRegistration.UsernameIsValid", ex.Message, ex.StackTrace, "WebPart", "Error");
                return false;
            }
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

                if (txb_Password.Text.IndexOfAny(validSpecialCharacters) == -1 ||
                    txb_Password.Text.IndexOfAny(alphaCharacters) == -1 ||
                    txb_Password.Text.IndexOfAny(numericCharacters) == -1 ||
                    txb_Password.Text.Length < 8)
                {
                    custom_Password.ErrorMessage = "Password does not <br /> meet requirements.";
                    custom_Password.IsValid = false;
                    compare_ConfirmPassword.Validate();
                }
                else if (txb_Password.Text == txb_Username.Text)
                {
                    custom_Password.ErrorMessage = "Password must not <br /> match Username.";
                    custom_Password.IsValid = false;
                    compare_ConfirmPassword.Validate();
                }
                else
                {
                    custom_Password.IsValid = true;
                    compare_ConfirmPassword.Validate();
                }

                txb_ConfirmPassword.Focus();
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "UserRegistration.ValidatePassword", ex.Message, ex.StackTrace, "WebPart", "Error");
            }
        }

        protected void EmailAddressIsUnique(object sender, EventArgs e)
        {
            try
            {
                using (var site = new SPSite(Page.Request.Url.ToString()))
                {
                    if (ActiveDirectoryHelper.EmailAddressIsTaken(site, txb_EmailAddress.Text))
                    {
                        custom_EmailAdress.ErrorMessage = "This email address is not available.";
                        custom_EmailAdress.IsValid = false;
                    }
                    else
                    {
                        custom_UserName.IsValid = true;
                    }
                }

                txb_ConfirmEmail.Focus();
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "UserRegistration.ValidateUsername", ex.Message, ex.StackTrace, "WebPart", "Error");
            }
        }

        protected Boolean EmailAddressIsValid()
        {
            try
            {
                using (var site = new SPSite(Page.Request.Url.ToString()))
                {
                    if (ActiveDirectoryHelper.EmailAddressIsTaken(site, txb_EmailAddress.Text))
                    {
                        custom_EmailAdress.ErrorMessage = "This email address is not available.";
                        custom_EmailAdress.IsValid = false;
                        return false;
                    }
                    else
                    {
                        custom_UserName.IsValid = true;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "UserRegistration.ValidateUsername", ex.Message, ex.StackTrace, "WebPart", "Error");
                return false;
            }
        }

        protected void ThirdPartyOption(object sender, EventArgs e)
        {
            try
            {
                ltr_RequireThirdPartyOption.Visible = false; 
                
                switch (rbl_ThirdPartyOption.SelectedValue)
                {
                    case "Yes":
                        panel_3pbYes.Visible = true;
                        panel_3pbNo.Visible = false;
                        foreach (var validator in Page.GetValidators("StepThree"))
                        {
                            (validator as BaseValidator).Enabled = false;
                        }
                        require_UpsAccountNumber.Enabled = true;
                        break;
                    case "No":
                        panel_3pbYes.Visible = false;
                        panel_3pbNo.Visible = true;
                        foreach (var validator in Page.GetValidators("StepThree"))
                        {
                            (validator as BaseValidator).Enabled = true;
                            require_3pbShipperAcctNumber.Enabled = rbl_SmallParcelBillingType.SelectedValue == "3rd Party";
                        }
                        require_UpsAccountNumber.Enabled = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "UserRegistration.ThirdPartyOption", ex.Message, ex.StackTrace, "WebPart", "Error");
            }
        }

        protected void ToggleThirdPartyBilling(object sender, EventArgs e)
        {
            try
            {
                lbl_SmallParcel3pbShipperAcctNumber.CssClass = rbl_SmallParcelBillingType.SelectedValue == "3rd Party" ? "" : "disabledLabel";
                txb_SmallParcel3pbShipperAcctNumber.Enabled = rbl_SmallParcelBillingType.SelectedValue == "3rd Party";
                require_3pbShipperAcctNumber.Enabled = rbl_SmallParcelBillingType.SelectedValue == "3rd Party";
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "CompanySettings.ToggleThirdPartyBilling", ex.Message, ex.StackTrace, "WebPart", "Error");
            }
        }

        public void SetSmallParcelServiceLevels(object sender, EventArgs e)
        {
            ControlHelper.ConditionalSmallParcelServiceLevels(ddl_SmallParcelCarrier, ddl_SmallParcelServiceLevel);
        }

        public void SetLtlServiceLevels(object sender, EventArgs e)
        {
            ControlHelper.ConditionalLtlServiceLevels(ddl_LtlCarrier, ddl_LtlServiceLevel);
        }

        protected void btn_CreateAccount_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    if (!String.IsNullOrEmpty(rbl_ThirdPartyOption.SelectedValue))
                    {
                        RegisterUser();
                        Page.Response.Redirect(SPContext.Current.Site.Url + "/Pages/AccountCreated.aspx", false);
                    }
                    else
                    {
                        ltr_RequireThirdPartyOption.Text = "<span class='pxErrorText'>Third-party Billing option is required.</span>";
                        ltr_RequireThirdPartyOption.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.LogMessage(SPContext.Current.Site, "UserRegistration.btn_CreateAccount_Click", ex.Message, ex.StackTrace, "WebPart", "Error");
                    Page.Response.Redirect(SPContext.Current.Site.Url + "/Pages/AccountCreated.aspx?error=1", false);
                }
            }
        }

        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(SPContext.Current.Site.Url, false);
        }

        protected void RegisterUser()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                try
                {
                    using (var elevatedSite = new SPSite(SPContext.Current.Site.ID))
                    {
                        elevatedSite.RootWeb.AllowUnsafeUpdates = true;

                        // post new supplier to the bus...
                        var webUrl = WebRequestHelper.BuildPostUserRegistrationUrl();
                        var xdoc = WebRequestHelper.PostToBus(webUrl, BuildPostXml());
                        var supplierId = xdoc != null ? WebRequestHelper.GetSupplierId(xdoc) : String.Empty;
                        
                        if (xdoc != null)
                        {                            
                            // create AD user...
                            if (!String.IsNullOrEmpty(supplierId))
                            {
                                var userPrincipal = CreateActiveDirectoryUser();
                                var directoryEntry = userPrincipal.GetUnderlyingObject() as DirectoryEntry;
                                var property = elevatedSite.GetProperty(Constants.Property_SupplierIdAttribute);
                                ActiveDirectoryHelper.SetDirectoryEntryProperty(elevatedSite, directoryEntry, property, supplierId);
                            }
                            else
                            {
                                throw new Exception("Unable to retrieve Supplier ID from response.");
                            }
                        }
                        else
                        {
                            throw new Exception("No response from bus.");
                        }

                        var detail = String.Format("Detail \r\n Username: {0} \r\n Email: {1} \r\n Phone: {2} \r\n Supplier ID: {3}", txb_Username.Text, txb_EmailAddress.Text, txb_PhoneNumber.Text, supplierId);
                        LogHelper.LogMessage(SPContext.Current.Site, "UserRegistration.btn_CreateAccount_Click", "User Created: " + txb_Username.Text, detail, "WebPart", "Audit");
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.LogMessage(SPContext.Current.Site, "UserRegistration.btn_CreateAccount_Click", ex.Message, ex.StackTrace, "WebPart", "Error"); 
                    throw ex;
                }
                finally
                {
                    SPContext.Current.Site.RootWeb.AllowUnsafeUpdates = false;
                }
            });
        }

        protected UserPrincipal CreateActiveDirectoryUser()
        {
            UserPrincipal userPrincipal = null;

            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    var domain = SPContext.Current.Site.GetProperty(Constants.Property_ActiveDirectoryDomain);
                    var container = SPContext.Current.Site.GetProperty(Constants.Property_ActiveDirectoryContainer);

                    if (!String.IsNullOrEmpty(domain) && !String.IsNullOrEmpty(container))
                    {
                        var context = new PrincipalContext(ContextType.Domain, domain, container);

                        if (UserPrincipal.FindByIdentity(context, txb_Username.Text) == null)
                        {
                            userPrincipal = new UserPrincipal(context, txb_Username.Text, txb_Password.Text, true);
                            userPrincipal.GivenName = txb_FirstName.Text;
                            userPrincipal.Surname = txb_LastName.Text;
                            userPrincipal.DisplayName = txb_FirstName.Text + " " + txb_LastName.Text;
                            userPrincipal.EmailAddress = txb_EmailAddress.Text;
                            userPrincipal.VoiceTelephoneNumber = txb_PhoneNumber.Text;
                            //userPrincipal.Enabled = false;
                            userPrincipal.Save();
                        }
                    }
                    else
                    {
                        var detail = String.Format("Infratsructure configuration settings required to instantiate PrincipalContext are unavailable. \r\n Domain: {0} \r\n Container: {1}", domain, container);
                        LogHelper.LogMessage(SPContext.Current.Site, "UserRegistration.CreateActiveDirectoryUser", "Missing context settings.", detail, "Web Part", "Error");
                    }
                });
            }
            catch (PasswordException ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "UserRegistration.CreateActiveDirectoryUser", ex.Message, ex.StackTrace, "Web Part", "Error");
                throw ex;
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "UserRegistration.CreateActiveDirectoryUser", ex.Message, ex.StackTrace, "Web Part", "Error");
                throw ex;
            }

            return userPrincipal;
        }

        public String BuildPostXml()
        {
            var xml = String.Empty;

            try
            {
                var stringWriter = new StringWriter();
                var settings = new XmlWriterSettings();
                settings.OmitXmlDeclaration = true;
                settings.ConformanceLevel = ConformanceLevel.Fragment;
                var xmlWriter = XmlWriter.Create(stringWriter, settings);

                xmlWriter.WriteStartElement("supplier");

                xmlWriter.WriteConditionalElementString("supplierName", txb_legalEntity.Text);
                xmlWriter.WriteConditionalElementString("displayName", String.IsNullOrEmpty(txb_CompanyName.Text) ? txb_legalEntity.Text : txb_CompanyName.Text);
                xmlWriter.WriteConditionalElementString("status", "ACTIVE");
                xmlWriter.WriteConditionalElementString("thirdPartyBilling", (rbl_ThirdPartyOption.SelectedValue == "Yes").ToString().ToLower());

                xmlWriter.WriteStartElement("supplierAddresses");

                xmlWriter.WriteStartElement("supplierAddress");
                xmlWriter.WriteConditionalElementString("addressType", "RETURNS");
                xmlWriter.WriteStartElement("address");
                xmlWriter.WriteConditionalElementString("addressee", txb_legalEntity.Text);
                xmlWriter.WriteConditionalElementString("lineOne", chb_SameReturnsAddress.Checked ? txb_Address.Text : txb_ReturnsAddress.Text);
                xmlWriter.WriteConditionalElementString("lineTwo", chb_SameReturnsAddress.Checked ? txb_Suite.Text : txb_ReturnsSuite.Text);
                xmlWriter.WriteConditionalElementString("city", chb_SameReturnsAddress.Checked ? txb_City.Text : txb_ReturnsCity.Text);
                xmlWriter.WriteConditionalElementString("state", chb_SameReturnsAddress.Checked ? ddl_State.SelectedValue : ddl_ReturnsState.SelectedValue);
                xmlWriter.WriteConditionalElementString("countryCode", "US");
                xmlWriter.WriteConditionalElementString("postalCode", chb_SameReturnsAddress.Checked ? txb_Zip.Text : txb_ReturnsZip.Text);
                xmlWriter.WriteEndElement(); // address
                xmlWriter.WriteEndElement(); // supplierAddress

                xmlWriter.WriteStartElement("supplierAddress");
                xmlWriter.WriteConditionalElementString("addressType", "PRIMARY");
                xmlWriter.WriteStartElement("address");
                xmlWriter.WriteConditionalElementString("addressee", txb_legalEntity.Text);
                xmlWriter.WriteConditionalElementString("lineOne", txb_Address.Text);
                xmlWriter.WriteConditionalElementString("lineTwo", txb_Suite.Text);
                xmlWriter.WriteConditionalElementString("city", txb_City.Text);
                xmlWriter.WriteConditionalElementString("state", ddl_State.SelectedValue);
                xmlWriter.WriteConditionalElementString("countryCode", "US");
                xmlWriter.WriteConditionalElementString("postalCode", txb_Zip.Text);
                xmlWriter.WriteEndElement(); // address
                xmlWriter.WriteEndElement(); // supplierAddress

                xmlWriter.WriteEndElement(); // supplierAddresses
                                
                xmlWriter.WriteStartElement("contacts");

                // primary contact...
                xmlWriter.WriteStartElement("contact");
                xmlWriter.WriteConditionalElementString("firstName", txb_ContactPrimaryBusinessFirstName.Text);
                xmlWriter.WriteConditionalElementString("lastName", txb_ContactPrimaryBusinessLastName.Text);
                xmlWriter.WriteConditionalElementString("emailAddress", txb_ContactPrimaryBusinessEmail.Text);
                xmlWriter.WriteConditionalElementString("phoneNumber", txb_ContactPrimaryBusinessPhone.Text);
                xmlWriter.WriteConditionalElementString("contactType", "PRIMARY");
                xmlWriter.WriteEndElement(); // contact

                // customer service contact...
                xmlWriter.WriteStartElement("contact");
                xmlWriter.WriteConditionalElementString("firstName", txb_ContactCustomerServiceFirstName.Text);
                xmlWriter.WriteConditionalElementString("lastName", txb_ContactCustomerServiceLastName.Text);
                xmlWriter.WriteConditionalElementString("emailAddress", txb_ContactCustomerServiceEmail.Text);
                xmlWriter.WriteConditionalElementString("phoneNumber", txb_ContactCustomerServicePhone.Text);
                xmlWriter.WriteConditionalElementString("contactType", "CUSTOMER_SERVICE");
                xmlWriter.WriteEndElement(); // contact

                // accounts receivable contact...
                xmlWriter.WriteStartElement("contact");
                xmlWriter.WriteConditionalElementString("firstName", txb_ContactAccountsReceivableFirstName.Text);
                xmlWriter.WriteConditionalElementString("lastName", txb_ContactAccountsReceivableLastName.Text);
                xmlWriter.WriteConditionalElementString("emailAddress", txb_ContactAccountsReceivableEmail.Text);
                xmlWriter.WriteConditionalElementString("phoneNumber", txb_ContactAccountsReceivablePhone.Text);
                xmlWriter.WriteConditionalElementString("contactType", "ACCOUNTS_RECEIVABLE");
                xmlWriter.WriteEndElement(); // contact

                // inventory contact...
                xmlWriter.WriteStartElement("contact");
                xmlWriter.WriteConditionalElementString("firstName", txb_ContactInventoryFirstName.Text);
                xmlWriter.WriteConditionalElementString("lastName", txb_ContactInventoryLastName.Text);
                xmlWriter.WriteConditionalElementString("emailAddress", txb_ContactInventoryEmail.Text);
                xmlWriter.WriteConditionalElementString("phoneNumber", txb_ContactInventoryPhone.Text);
                xmlWriter.WriteConditionalElementString("contactType", "INVENTORY");
                xmlWriter.WriteEndElement(); // contact

                xmlWriter.WriteStartElement("contact");
                xmlWriter.WriteConditionalElementString("firstName", txb_ContactReturnsFirstName.Text);
                xmlWriter.WriteConditionalElementString("lastName", txb_ContactReturnsLastName.Text);
                xmlWriter.WriteConditionalElementString("emailAddress", txb_ContactReturnsEmail.Text);
                xmlWriter.WriteConditionalElementString("phoneNumber", txb_ContactReturnsPhone.Text);
                xmlWriter.WriteConditionalElementString("contactType", "RETURNS");
                xmlWriter.WriteEndElement(); // contact

                xmlWriter.WriteEndElement(); // contacts                
                
                xmlWriter.WriteStartElement("carrierAccounts");
                if (rbl_ThirdPartyOption.SelectedValue == "No")
                {
                    // smallParcelAccount...
                    xmlWriter.WriteStartElement("carrierAccount");
                    xmlWriter.WriteConditionalElementString("defaultServiceLevel", ddl_SmallParcelServiceLevel.SelectedValue);
                    xmlWriter.WriteConditionalElementString("carrierCode", ddl_SmallParcelCarrier.SelectedValue);
                    xmlWriter.WriteConditionalElementString("carrierType", "SMALLPARCEL");
                    xmlWriter.WriteConditionalElementString("shipperOfRecordAcctNumber", txb_SmallParcelShipperAcctNumber.Text);
                    if (rbl_SmallParcelBillingType.SelectedValue == "3rd Party")
                        xmlWriter.WriteConditionalElementString("billingAccountNumber", txb_SmallParcel3pbShipperAcctNumber.Text);
                    xmlWriter.WriteStartElement("carrierBillToAddress");
                    xmlWriter.WriteConditionalElementString("addressee", txb_SmallParcelBillToName.Text);
                    xmlWriter.WriteConditionalElementString("lineOne", txb_SmallParcelBillToAddress.Text);
                    xmlWriter.WriteConditionalElementString("lineTwo", txb_SmallParcelBillToSuite.Text);
                    xmlWriter.WriteConditionalElementString("city", txb_SmallParcelBillToCity.Text);
                    xmlWriter.WriteConditionalElementString("state", ddl_SmallParcelBillToState.SelectedValue);
                    xmlWriter.WriteConditionalElementString("countryCode", "US");
                    xmlWriter.WriteConditionalElementString("postalCode", txb_SmallParcelBillToZip.Text);
                    xmlWriter.WriteConditionalElementString("phoneNumber", txb_SmallParcelPhone.Text);
                    xmlWriter.WriteEndElement(); // carrierBillToAddress
                    xmlWriter.WriteConditionalElementString("thirdPartyBilling", (rbl_SmallParcelBillingType.SelectedValue == "3rd Party") ? "true" : "false");
                    xmlWriter.WriteConditionalElementString("defaultAccount", "true");                                        
                    xmlWriter.WriteEndElement(); // carrierAccount

                    // ltlAccount...
                    //xmlWriter.WriteStartElement("carrierAccount");
                    //xmlWriter.WriteConditionalElementString("defaultServiceLevel", ddl_LtlServiceLevel.SelectedValue);
                    //xmlWriter.WriteConditionalElementString("carrierCode", ddl_LtlCarrier.SelectedValue);
                    //xmlWriter.WriteConditionalElementString("carrierType", "LTL");
                    //xmlWriter.WriteStartElement("carrierBillToAddress");
                    //xmlWriter.WriteConditionalElementString("addressee", txb_LtlBillToName.Text);
                    //xmlWriter.WriteConditionalElementString("lineOne", txb_LtlBillToAddress.Text);
                    //xmlWriter.WriteConditionalElementString("lineTwo", txb_LtlBillToSuite.Text);
                    //xmlWriter.WriteConditionalElementString("city", txb_LtlBillToCity.Text);
                    //xmlWriter.WriteConditionalElementString("state", ddl_LtlBillToState.SelectedValue);
                    //xmlWriter.WriteConditionalElementString("countryCode", "US");
                    //xmlWriter.WriteConditionalElementString("postalCode", txb_LtlBillToZip.Text);
                    //xmlWriter.WriteEndElement(); // carrierBillToAddress
                    //xmlWriter.WriteConditionalElementString("thirdPartyBilling", rbl_ThirdPartyOption.ToString());
                    //xmlWriter.WriteConditionalElementString("defaultAccount", "true");
                    //xmlWriter.WriteConditionalElementString("specialInstructions", txb_LtlShippingInstructions.Text);
                    //xmlWriter.WriteEndElement(); // carrierAccount
                }
                else
                {
                    xmlWriter.WriteStartElement("carrierAccount");
                    xmlWriter.WriteConditionalElementString("shipperOfRecordAcctNumber", txb_UpsAccountNumber.Text);
                    xmlWriter.WriteConditionalElementString("defaultAccount", "true");
                    xmlWriter.WriteEndElement(); // carrierAccount                    
                }
                xmlWriter.WriteEndElement(); // carrierAccounts                

                xmlWriter.WriteEndElement(); // supplier

                xmlWriter.Flush();
                xml = stringWriter.ToString();
                xmlWriter.Close();
                stringWriter.Close();
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "UserRegistration.BuildPostXml", ex.Message, ex.StackTrace, "Web Part", "Error");
            }

            return xml;
        }

    }
}

