using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
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

namespace Px.SelfService.WebParts.CompanySettings
{
    [ToolboxItemAttribute(false)]
    public partial class CompanySettings : WebPart
    {
        public CompanySettings()
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
                    foreach (var validator in Page.GetValidators(null))
                    {
                        (validator as BaseValidator).Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "CompanySettings.OnInit", ex.Message, ex.StackTrace, "WebPart", "Error");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ChromeType = PartChromeType.None;

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
                        ltr_Username.Text = String.Format("<div id='error-username' style='display:none;'>PxUsername Error: {0}</div>", ex.Message);
                    }

                    // populate drop down lists...
                    if (!Page.IsPostBack)
                    {
                        ControlHelper.PopulateStates(ddl_State);
                        ControlHelper.PopulateStates(ddl_ReturnsState);
                        ControlHelper.PopulateStates(ddl_SmallParcelBillToState);
                        ControlHelper.PopulateStates(ddl_LtlBillToState);
                        ControlHelper.PopulateSmallParcelChoices(ddl_SmallParcelCarrier, ddl_SmallParcelServiceLevel);
                        ControlHelper.PopulateLtlChoices(ddl_LtlCarrier, ddl_LtlServiceLevel);

                        LoadSettings();
                    }
                }
                else
                {
                    Page.Response.Redirect("/_layouts/15/Authenticate.aspx?Source=%2F", false);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "CompanySettings.Page_Load", ex.Message, ex.StackTrace, "WebPart", "Error");
            }
        }

        protected void LoadSettings()
        {
            var supplierId = GetCurrentUserSupplierId();
            
            if (!String.IsNullOrEmpty(supplierId))
            {
                var webUrl = WebRequestHelper.BuildGetCompanySettingsUrl(supplierId);
                var xdoc = WebRequestHelper.GetSupplier(webUrl);

                if (xdoc != null)
                {
                    LoadControlValues(xdoc);
                }
                else
                {
                    if (SPContext.Current.Site.GetProperty(Constants.Property_PxDebug) == "True")
                        ltr_Debug.Text += "<h3 class='pxErrorText'><strong>Unable to communicate with Bus.</strong></h3>";
                }
            }
        }

        protected String GetCurrentUserSupplierId()
        {
            var supplierId = String.Empty;

            try
            {
                //var domain = SPContext.Current.Site.GetProperty(Constants.Property_ActiveDirectoryDomain);
                //var container = SPContext.Current.Site.GetProperty(Constants.Property_ActiveDirectoryContainer);
                //var context = new PrincipalContext(ContextType.Domain, domain, container);

                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    var context = new PrincipalContext(ContextType.Domain, SPContext.Current.Site.GetProperty(Constants.Property_ActiveDirectoryDomain));
                    var userPrincipal = UserPrincipal.FindByIdentity(context, SPContext.Current.Web.CurrentUser.Name);

                    if (userPrincipal != null)
                    {
                        supplierId = ActiveDirectoryHelper.GetDirectoryEntryProperty(
                            SPContext.Current.Site, userPrincipal.GetUnderlyingObject() as DirectoryEntry,
                            PropertyBagHelper.GetProperty(SPContext.Current.Site, Constants.Property_SupplierIdAttribute));
                    }
                    else
                    {
                        if (SPContext.Current.Site.GetProperty(Constants.Property_PxDebug) == "True")
                            ltr_Debug.Text += "<h3 class='pxErrorText'><strong>UserPrincipal not found. Unable to retrieve Supplier ID.</strong></h3>";
                    }
                });
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "CompanySettings.GetCurrentUSerSupplierId", ex.Message, ex.StackTrace, "WebPart", "Error");
            }

            return supplierId;
        }

        protected void LoadControlValues(XDocument xdoc)
        {
            try
            {
                if (xdoc != null)
                {                    
                    var supplier = xdoc.Element("supplier");
                    
                    if (supplier != null)
                    {
                        var supplierAddresses = supplier.Elements("supplierAddresses").Elements("supplierAddress");
                        var mainAddressElement = SearchForElement(supplierAddresses, "addressType", "PRIMARY");
                        var returnsAddressElement = SearchForElement(supplierAddresses, "addressType", "RETURNS");

                        var contacts = supplier.Elements("contacts").Elements("contact");
                        var primaryContactElement = SearchForElement(contacts, "contactType", "PRIMARY");
                        var customerServiceContactElement = SearchForElement(contacts, "contactType", "CUSTOMER_SERVICE");
                        var accountsReceivableContactElement = SearchForElement(contacts, "contactType", "ACCOUNTS_RECEIVABLE");
                        var inventoryContactElement = SearchForElement(contacts, "contactType", "INVENTORY");
                        var returnsContactElement = SearchForElement(contacts, "contactType", "RETURNS");

                        var carrierAccounts = supplier.Elements("carrierAccounts").Elements("carrierAccount");
                        var smallParcelAccount = SearchForElement(carrierAccounts, "carrierType", "SMALLPARCEL");
                        //var ltlAccount = SearchForElement(carrierAccounts, "carrierType", "LTL");
                        //var thirdPartyBillingAccount = SearchForElement(carrierAccounts, "carrierType", "3PB");

                        var supplierName = supplier.ReadElement("supplierName");
                        var displayName = supplier.ReadElement("displayName");

                        var participate3Pb = supplier.ReadElement("thirdPartyBilling");
                        divDisplaySmallParcel.Visible = (participate3Pb != "true");
                        divEditSmallParcel.Visible = (participate3Pb != "true");

                        var mainAddress = mainAddressElement.ReadElement("address", "lineOne");
                        var mainSuite = mainAddressElement.ReadElement("address", "lineTwo");
                        var mainCity = mainAddressElement.ReadElement("address", "city");
                        var mainState = mainAddressElement.ReadElement("address", "state");
                        var mainZip = mainAddressElement.ReadElement("address", "postalCode");

                        var returnsAddress = returnsAddressElement.ReadElement("address", "lineOne");
                        var returnsSuite = returnsAddressElement.ReadElement("address", "lineTwo");
                        var returnsCity = returnsAddressElement.ReadElement("address", "city");
                        var returnsState = returnsAddressElement.ReadElement("address", "state");
                        var returnsZip = returnsAddressElement.ReadElement("address", "postalCode");

                        var primaryFirstName = primaryContactElement.ReadElement("firstName");
                        var primaryLastName = primaryContactElement.ReadElement("lastName");
                        var primaryPhone = primaryContactElement.ReadElement("phoneNumber");
                        var primaryEmail = primaryContactElement.ReadElement("emailAddress");
                        var customerServiceFirstName = customerServiceContactElement.ReadElement("firstName");
                        var customerServiceLastName = customerServiceContactElement.ReadElement("lastName");
                        var customerServicePhone = customerServiceContactElement.ReadElement("phoneNumber");
                        var customerServiceEmail = customerServiceContactElement.ReadElement("emailAddress");
                        var accountsReceivableFirstName = accountsReceivableContactElement.ReadElement("firstName");
                        var accountsReceivableLastName = accountsReceivableContactElement.ReadElement("lastName");
                        var accountsReceivablePhone = accountsReceivableContactElement.ReadElement("phoneNumber");
                        var accountsReceivableEmail = accountsReceivableContactElement.ReadElement("emailAddress");
                        var inventoryFirstName = inventoryContactElement.ReadElement("firstName");
                        var inventoryLastName = inventoryContactElement.ReadElement("lastName");
                        var inventoryPhone = inventoryContactElement.ReadElement("phoneNumber");
                        var inventoryEmail = inventoryContactElement.ReadElement("emailAddress");
                        var returnsFirstName = returnsContactElement.ReadElement("firstName");
                        var returnsLastName = returnsContactElement.ReadElement("lastName");
                        var returnsPhone = returnsContactElement.ReadElement("phoneNumber");
                        var returnsEmail = returnsContactElement.ReadElement("emailAddress");

                        var smallParcelCarrier = smallParcelAccount.ReadElement("carrierCode");
                        var smallParcelServiceLevel = smallParcelAccount.ReadElement("defaultServiceLevel");
                        var smallParcelBillingAcctNumber = smallParcelAccount.ReadElement("billingAccountNumber");
                        var smallParcelShipperOfRecordAcctNumber = smallParcelAccount.ReadElement("shipperOfRecordAcctNumber");
                        var smallParcelBillingType = smallParcelAccount.ReadElement("thirdPartyBilling") == "true" ? "3rd Party" : "Prepaid";
                        var smallParcelBillToName = smallParcelAccount.ReadElement("carrierBillToAddress", "addressee");
                        var smallParcelAddress = smallParcelAccount.ReadElement("carrierBillToAddress", "lineOne");
                        var smallParcelSuite = smallParcelAccount.ReadElement("carrierBillToAddress", "lineTwo");
                        var smallParcelCity = smallParcelAccount.ReadElement("carrierBillToAddress", "city");
                        var smallParcelState = smallParcelAccount.ReadElement("carrierBillToAddress", "state");
                        var smallParcelZip = smallParcelAccount.ReadElement("carrierBillToAddress", "postalCode");
                        var smallParcelPhoneNumber = smallParcelAccount.ReadElement("carrierBillToAddress", "phoneNumber");

                        /*var ltlCarrier = ltlAccount.ReadElement("carrierCode");
                        var ltlServiceLevel = ltlAccount.ReadElement("defaultServiceLevel");
                        var specialInstructions = ltlAccount.ReadElement("specialInstructions");
                        var ltlBillToName = ltlAccount.ReadElement("carrierBillToAddress", "addressee");
                        var ltlAddress = ltlAccount.ReadElement("carrierBillToAddress", "lineOne");
                        var ltlSuite = ltlAccount.ReadElement("carrierBillToAddress", "lineTwo");
                        var ltlCity = ltlAccount.ReadElement("carrierBillToAddress", "city");
                        var ltlState = ltlAccount.ReadElement("carrierBillToAddress", "state");
                        var ltlZip = ltlAccount.ReadElement("carrierBillToAddress", "postalCode");
                        //var ltlPhoneNumber = ltlAccount.ReadElement("carrierBillToAddress", "");  // ??? */

                        // display controls...
                        ltr_LegalEntityName.Text = String.Format("<p>{0}</p>", supplierName);
                        ltr_CompanyName.Text = String.Format("<p>{0}</p>", displayName);

                        if (participate3Pb != "true")
                        {
                            ltr_SmallParcelCarrier.Text = String.Format("<p>{0}</p>", smallParcelCarrier);
                            ltr_SmallParcelBillingType.Text = String.Format("<p>{0}</p>", smallParcelBillingType);
                            ltr_SmallParcelShippingAccountNumber.Text = String.Format("<p>{0}</p>", smallParcelShipperOfRecordAcctNumber);
                            ltr_SmallParcelBillTo.Text = String.Format("<p>{0}</p>",
                                String.Format("{0}<br/>{1}<br/>{2}{3}, {4} {5}",
                                    smallParcelBillToName,
                                    smallParcelAddress,
                                    String.IsNullOrEmpty(
                                        smallParcelSuite)
                                        ? String.Empty
                                        : smallParcelSuite + "<br/>",
                                    smallParcelCity,
                                    smallParcelState,
                                    smallParcelZip));

                            /*ltr_LtlCarrier.Text = String.Format("<p>{0}</p>", ltlCarrier);
                            // ltr_LtlShippingAccountNumber.Text = String.Format("<p>{0}</p>", "");  // remove?
                            ltr_LtlBillTo.Text = String.Format("<p>{0}</p>",
                                String.Format("{0}<br/>{1}<br/>{2}{3}, {4} {5}",
                                    ltlBillToName,
                                    ltlAddress,
                                    String.IsNullOrEmpty(ltlSuite) ? String.Empty : ltlSuite + "<br/>",
                                    ltlCity,
                                    ltlState,
                                    ltlZip));*/
                        }

                        ltr_MainAddress.Text = String.Format("<p>{0}</p>", 
                            String.Format("{0}<br/>{1}{2}, {3} {4}",
                                mainAddress,
                                String.IsNullOrEmpty(mainSuite) ? String.Empty : mainSuite + "<br/>",
                                mainCity,
                                mainState,
                                mainZip));

                        ltr_ReturnsAddress.Text = String.Format("<p>{0}</p>",
                            String.Format("{0}<br/>{1}{2}, {3} {4}",
                                returnsAddress,
                                String.IsNullOrEmpty(returnsSuite) ? String.Empty : returnsSuite + "<br/>",
                                returnsCity,
                                returnsState,
                                returnsZip));

                        ltr_PrimaryBusinessFirstName.Text = String.Format("<p>{0}</p>", primaryFirstName);
                        ltr_PrimaryBusinessLastName.Text = String.Format("<p>{0}</p>", primaryLastName);
                        ltr_PrimaryBusinessPhone.Text = String.Format("<p>{0}</p>", primaryPhone);
                        ltr_PrimaryBusinessEmail.Text = String.Format("<p>{0}</p>", primaryEmail);
                        ltr_CustomerServiceFirstName.Text = String.Format("<p>{0}</p>", customerServiceFirstName);
                        ltr_CustomerServiceLastName.Text = String.Format("<p>{0}</p>", customerServiceLastName);
                        ltr_CustomerServicePhone.Text = String.Format("<p>{0}</p>", customerServicePhone);
                        ltr_CustomerServiceEmail.Text = String.Format("<p>{0}</p>", customerServiceEmail);
                        ltr_AccountsReceivableFirstName.Text = String.Format("<p>{0}</p>", accountsReceivableFirstName);
                        ltr_AccountsReceivableLastName.Text = String.Format("<p>{0}</p>", accountsReceivableLastName);
                        ltr_AccountsReceivablePhone.Text = String.Format("<p>{0}</p>", accountsReceivablePhone);
                        ltr_AccountsReceivableEmail.Text = String.Format("<p>{0}</p>", accountsReceivableEmail);
                        ltr_InventoryFirstName.Text = String.Format("<p>{0}</p>", inventoryFirstName);
                        ltr_InventoryLastName.Text = String.Format("<p>{0}</p>", inventoryLastName);
                        ltr_InventoryPhone.Text = String.Format("<p>{0}</p>", inventoryPhone);
                        ltr_InventoryEmail.Text = String.Format("<p>{0}</p>", inventoryEmail);
                        ltr_ReturnsFirstName.Text = String.Format("<p>{0}</p>", returnsFirstName);
                        ltr_ReturnsLastName.Text = String.Format("<p>{0}</p>", returnsLastName);
                        ltr_ReturnsPhone.Text = String.Format("<p>{0}</p>", returnsPhone);
                        ltr_ReturnsEmail.Text = String.Format("<p>{0}</p>", returnsEmail);

                        // edit controls...
                        txb_legalEntity.Text = supplierName;
                        txb_CompanyName.Text = displayName;

                        if (participate3Pb != "true")
                        {
                            //lbl_SmallParcel3pbShipperAcctNumber.CssClass = rbl_SmallParcelBillingType.SelectedValue == "3rd Party" ? "" : "disabledLabel";
                            //txb_SmallParcel3pbShipperAcctNumber.Enabled = rbl_SmallParcelBillingType.SelectedValue == "3rd Party";

                            // ddl_SmallParcelCarrier.SelectedValue = smallParcelCarrier;
                            // ddl_SmallParcelServiceLevel.SelectedValue = smallParcelServiceLevel;
                            ControlHelper.EnsureDropDownListValue(ddl_SmallParcelCarrier, smallParcelCarrier);
                            ControlHelper.EnsureDropDownListValue(ddl_SmallParcelServiceLevel, smallParcelServiceLevel);

                            rbl_SmallParcelBillingType.SelectedValue = smallParcelBillingType;

                            if (rbl_SmallParcelBillingType.SelectedValue == "3rd Party")
                            {
                                lbl_SmallParcel3pbShipperAcctNumber.CssClass = String.Empty;
                                txb_SmallParcel3pbShipperAcctNumber.Enabled = true;
                                require_3pbShipperAcctNumber.Enabled = true;
                            }
                            else
                            {
                                lbl_SmallParcel3pbShipperAcctNumber.CssClass = "disabledLabel";
                                txb_SmallParcel3pbShipperAcctNumber.Enabled = false;
                                require_3pbShipperAcctNumber.Enabled = false;
                            }
                            
                            txb_SmallParcelShipperAcctNumber.Text = smallParcelShipperOfRecordAcctNumber;
                            txb_SmallParcel3pbShipperAcctNumber.Text = smallParcelBillingAcctNumber;                            
                            txb_SmallParcelBillToName.Text = smallParcelBillToName;
                            txb_SmallParcelPhone.Text = smallParcelPhoneNumber;
                            txb_SmallParcelBillToAddress.Text = smallParcelAddress;
                            txb_SmallParcelBillToSuite.Text = smallParcelSuite;
                            txb_SmallParcelBillToCity.Text = smallParcelCity;
                            ddl_SmallParcelBillToState.SelectedValue = smallParcelState;
                            txb_SmallParcelBillToZip.Text = smallParcelZip;

                            /*ddl_LtlCarrier.SelectedValue = ltlCarrier;
                            ddl_LtlServiceLevel.SelectedValue = ltlServiceLevel;
                            //txb_LtlShipperAcctNumber.Text = ltlAccountNumber;  // ???
                            txb_LtlBillToName.Text = ltlBillToName;
                            txb_LtlPhone.Text = ltlPhoneNumber;
                            txb_LtlBillToAddress.Text = ltlAddress;
                            txb_LtlBillToSuite.Text = ltlSuite;
                            txb_LtlBillToCity.Text = ltlCity;
                            ddl_LtlBillToState.SelectedValue = ltlState;
                            txb_LtlBillToZip.Text = ltlZip;
                            txb_LtlShippingInstructions.Text = specialInstructions;*/
                        }

                        txb_Address.Text = mainAddress;
                        txb_Suite.Text = mainSuite;
                        txb_City.Text = mainCity;
                        ddl_State.SelectedValue = mainState;
                        txb_Zip.Text = mainZip;
                        txb_ReturnsAddress.Text = returnsAddress;
                        txb_ReturnsSuite.Text = returnsSuite;
                        txb_ReturnsCity.Text = returnsCity;
                        txb_ReturnsZip.Text = returnsZip;

                        txb_ContactPrimaryBusinessFirstName.Text = primaryFirstName;
                        txb_ContactPrimaryBusinessLastName.Text = primaryLastName;
                        txb_ContactPrimaryBusinessPhone.Text = primaryPhone;
                        txb_ContactPrimaryBusinessEmail.Text = primaryEmail;
                        txb_ContactCustomerServiceFirstName.Text = customerServiceFirstName;
                        txb_ContactCustomerServiceLastName.Text = customerServiceLastName;
                        txb_ContactCustomerServicePhone.Text = customerServicePhone;
                        txb_ContactCustomerServiceEmail.Text = customerServiceEmail;
                        txb_ContactAccountsReceivableFirstName.Text = accountsReceivableFirstName;
                        txb_ContactAccountsReceivableLastName.Text = accountsReceivableLastName;
                        txb_ContactAccountsReceivablePhone.Text = accountsReceivablePhone;
                        txb_ContactAccountsReceivableEmail.Text = accountsReceivableEmail;
                        txb_ContactInventoryFirstName.Text = inventoryFirstName;
                        txb_ContactInventoryLastName.Text = inventoryLastName;
                        txb_ContactInventoryPhone.Text = inventoryPhone;
                        txb_ContactInventoryEmail.Text = inventoryEmail;
                        txb_ContactReturnsFirstName.Text = returnsFirstName;
                        txb_ContactReturnsLastName.Text = returnsLastName;
                        txb_ContactReturnsPhone.Text = returnsPhone;
                        txb_ContactReturnsEmail.Text = returnsEmail;

                        if (supplierName == displayName)
                        {
                            chb_LegalEntity.Checked = false;
                            lbl_CompanyName.CssClass = "disabledLabel";
                            txb_CompanyName.Enabled = false;
                            require_CompanyName.Enabled = false;
                        }
                        else
                        {
                            chb_LegalEntity.Checked = true;
                            lbl_CompanyName.CssClass = String.Empty;
                            txb_CompanyName.Enabled = true;
                            require_CompanyName.Enabled = true;
                        }

                        if (mainAddress == returnsAddress
                            && mainSuite == returnsSuite
                            && mainCity == returnsCity
                            && mainState == returnsState
                            && mainZip == returnsZip)
                        {
                            chb_SameReturnsAddress.Checked = true;
                            lbl_ReturnsAddress.CssClass = "disabledLabel";
                            lbl_ReturnsSuite.CssClass = "disabledLabel";
                            lbl_ReturnsCity.CssClass = "disabledLabel";
                            lbl_ReturnsState.CssClass = "disabledLabel";
                            lbl_ReturnsZip.CssClass = "disabledLabel";
                            txb_ReturnsAddress.Enabled = false;
                            txb_ReturnsSuite.Enabled = false;
                            txb_ReturnsCity.Enabled = false;
                            ddl_ReturnsState.Enabled = false;
                            txb_ReturnsZip.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "CompanySettings.LoadControlValues", ex.Message, ex.StackTrace, "WebPart", "Error");
            }
        }

        protected XElement SearchForElement(IEnumerable<XElement> parent, String child, String value)
        {
            try
            {
                var found = (
                    from element in parent
                    where element.Element(child).Value == value
                    select element).FirstOrDefault();
                return found;
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "CompanySettings.SearchForElement", ex.Message, ex.StackTrace, "WebPart", "Error");
                return null;
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

        protected void ToggleCompanyName(object sender, EventArgs e)
        {
            try
            {
                if (chb_LegalEntity.Checked)
                {
                    txb_CompanyName.Text = String.Empty;
                    lbl_CompanyName.CssClass = String.Empty;
                    txb_CompanyName.Enabled = true;
                    require_CompanyName.Enabled = true;
                }
                else
                {
                    txb_CompanyName.Text = txb_legalEntity.Text;
                    lbl_CompanyName.CssClass = "disabledLabel";
                    txb_CompanyName.Enabled = false;
                    require_CompanyName.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "CompanySettings.ToggleCompanyName", ex.Message, ex.StackTrace, "WebPart", "Error");
            }
        }

        protected Boolean CompanyNameIsTaken(String companyName)
        {
            Boolean result = false;

            try
            {
                var webUrl = WebRequestHelper.BuildGetCompanyByName(companyName);
                var xdoc = WebRequestHelper.GetSupplier(webUrl);
                var supplierId = xdoc != null ? WebRequestHelper.GetSupplierId(xdoc) : String.Empty;
                result = (!String.IsNullOrEmpty(supplierId));
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "CompanySettings.CompanyNameIsTaken", ex.Message, ex.StackTrace, "WebPart", "Error");
            }

            return result;
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

        protected void ToggleReturnsAddress(object sender, EventArgs e)
        {
            try
            {
                txb_ReturnsAddress.Text = (chb_SameReturnsAddress.Checked) ? txb_Address.Text : String.Empty;
                txb_ReturnsSuite.Text = (chb_SameReturnsAddress.Checked) ? txb_Suite.Text : String.Empty;
                txb_ReturnsCity.Text = (chb_SameReturnsAddress.Checked) ? txb_City.Text : String.Empty;
                ddl_ReturnsState.SelectedValue = (chb_SameReturnsAddress.Checked) ? ddl_State.SelectedValue : ddl_ReturnsState.Items[0].Value;
                txb_ReturnsZip.Text = (chb_SameReturnsAddress.Checked) ? txb_Zip.Text : String.Empty;
                lbl_ReturnsAddress.CssClass = (chb_SameReturnsAddress.Checked) ? "disabledLabel" : String.Empty;
                lbl_ReturnsSuite.CssClass = (chb_SameReturnsAddress.Checked) ? "disabledLabel" : String.Empty;
                lbl_ReturnsCity.CssClass = (chb_SameReturnsAddress.Checked) ? "disabledLabel" : String.Empty;
                lbl_ReturnsState.CssClass = (chb_SameReturnsAddress.Checked) ? "disabledLabel" : String.Empty;
                lbl_ReturnsZip.CssClass = (chb_SameReturnsAddress.Checked) ? "disabledLabel" : String.Empty;
                txb_ReturnsAddress.Enabled = (!chb_SameReturnsAddress.Checked);
                txb_ReturnsSuite.Enabled = (!chb_SameReturnsAddress.Checked);
                txb_ReturnsCity.Enabled = (!chb_SameReturnsAddress.Checked);
                ddl_ReturnsState.Enabled = (!chb_SameReturnsAddress.Checked);
                txb_ReturnsZip.Enabled = (!chb_SameReturnsAddress.Checked);
                require_ReturnsAddress.Enabled = (!chb_SameReturnsAddress.Checked);
                require_ReturnsCity.Enabled = (!chb_SameReturnsAddress.Checked);
                require_ReturnsZip.Enabled = (!chb_SameReturnsAddress.Checked);
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "CompanySettings.ToggleReturnsAddress", ex.Message, ex.StackTrace, "WebPart", "Error");
            }
        }

        protected void EditSettings_Click(object sender, EventArgs e)
        {
            try
            {
                section_DisplayCompany.Visible = false;
                section_EditCompany.Visible = true;
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "CompanySettings.EditSettings_Click", ex.Message, ex.StackTrace, "WebPart", "Error");
            }
        }

        protected void CancelChanges_Click(object sender, EventArgs e)
        {
            try
            {
                section_DisplayCompany.Visible = true;
                section_EditCompany.Visible = false;
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "CompanySettings.CancelChanges_Click", ex.Message, ex.StackTrace, "WebPart", "Error");
            }
        }

        protected void SubmitChanges_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    var supplierId = GetCurrentUserSupplierId();
                    var getWebUrl = WebRequestHelper.BuildGetCompanySettingsUrl(supplierId);
                    var xdoc = WebRequestHelper.GetSupplier(getWebUrl);
                    var postWebUrl = WebRequestHelper.BuildPostCompanySettingsUrl(supplierId);
                    WebRequestHelper.PutToBus(postWebUrl, EditPostXml(xdoc));

                    LoadSettings();
                    section_DisplayCompany.Visible = true;
                    section_EditCompany.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "CompanySettings.SubmitChanges_Click", ex.Message, ex.StackTrace, "WebPart", "Error");
            }
        }

        public String EditPostXml(XDocument xdoc)
        {
            var xml = String.Empty;

            try
            {
                if (xdoc != null)
                {                    
                    var supplier = xdoc.Element("supplier");

                    if (supplier != null)
                    {
                        var supplierAddresses = supplier.Elements("supplierAddresses").Elements("supplierAddress");
                        var mainAddressElement = SearchForElement(supplierAddresses, "addressType", "PRIMARY");
                        var returnsAddressElement = SearchForElement(supplierAddresses, "addressType", "RETURNS");

                        var carrierAccounts = supplier.Elements("carrierAccounts").Elements("carrierAccount");
                        var smallParcelAccount = SearchForElement(carrierAccounts, "carrierType", "SMALLPARCEL");
                        // var ltlAccount = SearchForElement(carrierAccounts, "carrierType", "LTL");
                        // var thirdPartyBillingAccount = SearchForElement(carrierAccounts, "carrierType", "3PB");

                        var participate3Pb = supplier.ReadElement("thirdPartyBilling");

                        supplier.SetConditionalElementString("supplierName", txb_legalEntity.Text, "after:id");
                        supplier.SetConditionalElementString("displayName", chb_LegalEntity.Checked ? txb_CompanyName.Text : txb_legalEntity.Text, "after:supplierName");

                        mainAddressElement.SetConditionalElementString("address", "lineOne", txb_Address.Text, "after:addressee");
                        mainAddressElement.SetConditionalElementString("address", "lineTwo", txb_Suite.Text, "after:lineOne");
                        mainAddressElement.SetConditionalElementString("address", "city", txb_City.Text, "after:lineTwo");
                        mainAddressElement.SetConditionalElementString("address", "state", ddl_State.SelectedValue, "after:city");
                        mainAddressElement.SetConditionalElementString("address", "postalCode", txb_Zip.Text, "after:state");

                        returnsAddressElement.SetConditionalElementString("address", "lineOne", chb_SameReturnsAddress.Checked ? txb_Address.Text : txb_ReturnsAddress.Text, "after:addressee");
                        returnsAddressElement.SetConditionalElementString("address", "lineTwo", chb_SameReturnsAddress.Checked ? txb_Suite.Text : txb_ReturnsSuite.Text, "after:lineOne");
                        returnsAddressElement.SetConditionalElementString("address", "city", chb_SameReturnsAddress.Checked ? txb_City.Text : txb_ReturnsCity.Text, "after:lineTwo");
                        returnsAddressElement.SetConditionalElementString("address", "state", chb_SameReturnsAddress.Checked ? ddl_State.Text : ddl_ReturnsState.SelectedValue, "after:city");
                        returnsAddressElement.SetConditionalElementString("address", "postalCode", chb_SameReturnsAddress.Checked ? txb_Zip.Text : txb_ReturnsZip.Text, "after:state");

                        EditContact(xdoc, "PRIMARY", txb_ContactPrimaryBusinessFirstName.Text,
                            txb_ContactPrimaryBusinessLastName.Text,
                            txb_ContactPrimaryBusinessEmail.Text,
                            txb_ContactPrimaryBusinessPhone.Text);

                        EditContact(xdoc, "CUSTOMER_SERVICE", txb_ContactCustomerServiceFirstName.Text,
                            txb_ContactCustomerServiceLastName.Text,
                            txb_ContactCustomerServiceEmail.Text,
                            txb_ContactCustomerServicePhone.Text);

                        EditContact(xdoc, "ACCOUNTS_RECEIVABLE", txb_ContactAccountsReceivableFirstName.Text,
                            txb_ContactAccountsReceivableLastName.Text,
                            txb_ContactAccountsReceivableEmail.Text,
                            txb_ContactAccountsReceivablePhone.Text);

                        EditContact(xdoc, "INVENTORY", txb_ContactInventoryFirstName.Text,
                            txb_ContactInventoryLastName.Text,
                            txb_ContactInventoryEmail.Text,
                            txb_ContactInventoryPhone.Text);

                        EditContact(xdoc, "RETURNS", txb_ContactReturnsFirstName.Text,
                            txb_ContactReturnsLastName.Text,
                            txb_ContactReturnsEmail.Text,
                            txb_ContactReturnsPhone.Text);

                        if (participate3Pb != "true")
                        {
                            if (smallParcelAccount != null)
                            {
                                smallParcelAccount.SetConditionalElementString("carrierCode", ddl_SmallParcelCarrier.SelectedValue, "after:warehouseId");
                                smallParcelAccount.SetConditionalElementString("defaultServiceLevel", ddl_SmallParcelServiceLevel.SelectedValue, "after:id");
                                smallParcelAccount.SetConditionalElementString("thirdPartyBilling", (rbl_SmallParcelBillingType.SelectedValue == "3rd Party") ? "true" : "false", "before:defaultAccount");
                                smallParcelAccount.SetConditionalElementString("shipperOfRecordAcctNumber", txb_SmallParcelShipperAcctNumber.Text, "after:billingAccountNumber");
                                smallParcelAccount.SetConditionalElementString("billingAccountNumber", txb_SmallParcel3pbShipperAcctNumber.Text, "after:carrierType");
                                smallParcelAccount.SetConditionalElementString("carrierBillToAddress", "addressee", txb_SmallParcelBillToName.Text, "after:id");
                                smallParcelAccount.SetConditionalElementString("carrierBillToAddress", "lineOne", txb_SmallParcelBillToAddress.Text, "after:addressee");
                                smallParcelAccount.SetConditionalElementString("carrierBillToAddress", "lineTwo", txb_SmallParcelBillToSuite.Text, "after:lineOne");
                                smallParcelAccount.SetConditionalElementString("carrierBillToAddress", "city", txb_SmallParcelBillToCity.Text, "after:lineTwo");
                                smallParcelAccount.SetConditionalElementString("carrierBillToAddress", "state", ddl_SmallParcelBillToState.SelectedValue, "after:city");
                                smallParcelAccount.SetConditionalElementString("carrierBillToAddress", "countryCode", "US", "after:state");
                                smallParcelAccount.SetConditionalElementString("carrierBillToAddress", "postalCode", txb_SmallParcelBillToZip.Text, "after:countryCode");
                                smallParcelAccount.SetConditionalElementString("carrierBillToAddress", "phoneNumber", txb_SmallParcelPhone.Text, "after:postalCode");
                            }
                            else
                            {
                                LogHelper.LogMessage(SPContext.Current.Site, "CompanySettings.EditPostXml", "Unable to Identify SmallParcelAccount in Supplier XML", "", "Web Part", "Error");                                
                            }

                            /*ltlAccount.SetConditionalElementString("carrierCode", ddl_LtlCarrier.SelectedValue);
                            ltlAccount.SetConditionalElementString("defaultServiceLevel", ddl_LtlServiceLevel.SelectedValue);
                            ltlAccount.SetConditionalElementString("carrierBillToAddress", "addressee", txb_LtlBillToName.Text);
                            ltlAccount.SetConditionalElementString("carrierBillToAddress", "lineOne", txb_LtlBillToAddress.Text);
                            ltlAccount.SetConditionalElementString("carrierBillToAddress", "lineTwo", txb_LtlBillToSuite.Text);
                            ltlAccount.SetConditionalElementString("carrierBillToAddress", "city", txb_LtlBillToCity.Text);
                            ltlAccount.SetConditionalElementString("carrierBillToAddress", "state", ddl_LtlBillToState.SelectedValue);
                            ltlAccount.SetConditionalElementString("carrierBillToAddress", "countryCode", "US");
                            ltlAccount.SetConditionalElementString("carrierBillToAddress", "postalCode", txb_LtlBillToZip.Text);
                            ltlAccount.SetConditionalElementString("carrierBillToAddress", "phoneNumber", txb_LtlPhone.Text);
                            ltlAccount.SetConditionalElementString("specialInstructions", txb_LtlShippingInstructions.Text);*/
                        }
                    }
                }

                xml = xdoc.ToString();
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "CompanySettings.EditPostXml", ex.Message, ex.StackTrace, "Web Part", "Error");
            }

            return xml;
        }

        public void EditContact(XDocument xdoc, String contactType, String firstName, String lastName, String emailAddress, String phoneNumber)
        {
            try
            {
                var supplier = xdoc.Element("supplier");
                var contacts = supplier.Elements("contacts").Elements("contact");
                var contactElement = SearchForElement(contacts, "contactType", contactType);

                if (contactElement != null)
                {
                    contactElement.SetConditionalElementString("firstName", firstName, "after:id");
                    contactElement.SetConditionalElementString("lastName", lastName, "after:firstName");
                    contactElement.SetConditionalElementString("emailAddress", emailAddress, "after:lastName");
                    contactElement.SetConditionalElementString("phoneNumber", phoneNumber, "after:emailAddress");
                }
                else
                {
                    var newContact = BuildContact(firstName, lastName, emailAddress, phoneNumber, contactType);
                    if (newContact != null)
                        supplier.Element("contacts").Add(newContact);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "CompanySettings.EditContact", ex.Message, ex.StackTrace, "Web Part", "Error");
            }
        }

        public XElement BuildContact(String firstName, String lastName, String emailAddress, String phoneNumber, String contactType)
        {
            try
            {
                var element = new XElement("contact", new XElement("firstName", firstName),
                    new XElement("lastName", lastName),
                    new XElement("emailAddress", emailAddress),
                    new XElement("phoneNumber", phoneNumber),
                    new XElement("contactType", contactType));

                return element;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
