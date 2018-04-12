<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompanySettings.ascx.cs" Inherits="Px.SelfService.WebParts.CompanySettings.CompanySettings" %>

<script type="text/javascript">
    
    $(document).ready(function () {
        new User();
    });

    //function EditCompanySettings() {
        //document.getElementById("section_DisplayCompany").style.display = "none";
        //document.getElementById("section_EditCompany").style.display = "block";
    //}

    //function CancelEdit() {
        //document.getElementById("section_DisplayCompany").style.display = "block";
        //document.getElementById("section_EditCompany").style.display = "none";
    //}
    
</script>

<style>
    div.welcome { padding-top: 0 !important; }
    .welcome-content {margin-top:0 !important;}    

    #PxBg { background-color: #E9E9E9 !important; }

    .pxWrap { background-color: #FFFFFF; padding: 20px 20px; border-top: 1px solid #cccccc; border-bottom: 1px solid #cccccc;}
    .activeTab {  }
    .inactiveTab { background-color: #E9E9E9; margin-left: 20px; padding: 5px; float: left; border-left: 1px solid #C1C1C1; border-top: 1px solid #C1C1C1; border-right: 1px solid #C1C1C1 }
    .inactiveTab a { color: #454545; }
    .inactiveTab a:hover { color: #454545; }
    .inactiveTab a:visited { color: #454545; }
    .pxDisplaySetting { background-color: #FFFFFF; padding: 20px 15px; }
    .pxChangeSetting { background-color: #E9E9E9; padding: 20px 15px; display: none; }
    .pxHeading { font-weight: bold; text-transform: uppercase; }
    .pxSection { border-bottom: 1px solid #E5E5E5; margin-bottom: 15px; padding-bottom: 15px;}
            
    .pxContactInformation { border-spacing:0; border-collapse:collapse; margin-top: 8px; font: 13px Open Sans; color: #454545; }
    .pxContactInformation th { border: 1px solid #C1C1C1; padding: 6px; font: 12px Open Sans bold; background-color: #EBEBEB; text-align: left; }
    .pxContactInformation th:first-child {border: 1px solid #cccccc; border-top-left-radius: 3px;}
    .pxContactInformation th:last-child { border: 1px solid #cccccc; border-top-right-radius: 3px; }
    .pxContactInformation td {background-color: #FFFFFF; border: 1px solid #C1C1C1; padding: 9px 6px 9px 6px; }
    .pxContactInformation td input { margin: 0; }
    .pxContactInformation tr:last-child td:first-child { border: 1px solid #cccccc; border-bottom-left-radius: 3px;}
    .pxContactInformation tr:last-child td:last-child{ border: 1px solid #cccccc; border-bottom-right-radius: 3px;}

    .pxSectionHeading { color: #454545; font: 13px Open Sans; margin-bottom: 10px; }
    .pxLabel { font-weight: bold; }

    .disabledLabel { color: #C1C1C1 }
    .pxButtons { margin-top: 10px; }
    .pxErrorText { color:#F3A94C }    
    .pxErrorTextBox {border-width: 2px; border-color: #F3A94C }    
    .clear { clear: both; }

    .tabs ul {list-style: none; margin: 0px; padding: 0px; padding-left: 15px; background-color: #E9E9E9; }
    .tabs li {border-width: 1px 1px 0px; border-style: solid; border-color: #CCCCCC; margin: 0px;  margin-right: 7px; border-top-left-radius: 3px; border-top-right-radius: 3px; float: left;}
    .tabs a {background: #F3F3F3; height: 25px;  padding: 5px 8px 0 8px; color: #454545; font: 12px Open Sans bold; text-decoration: none; display: block; margin-bottom: -1px}
    .tabs a:hover { }

    .tabs .activeTab {background-color: white; height: 26px; margin-bottom: -2px; position: relative;}
        .tabs .activeTab a {
        }
    #content {padding: 0px 1em; border-top: 1px solid #cccccc; clear: both;}
</style>

    <!-- Shared Navigation -->
    <asp:Literal ID="ltr_SharedNav" runat="server"></asp:Literal>
    <asp:Literal ID="ltr_Username" runat="server"></asp:Literal>

    <!-- Title -->
    <div style="margin-left: 20px; margin-top: 10px; margin-bottom: 10px;">
        <span class="pxSettingsTitle">Supplier Oasis Settings</span>
    </div>

    <!-- Tabs -->
    <div class="tabs">
        <ul>
            <li><a href="#" class="activeTab">Company Settings</a></li>
            <li><a href="/pages/PersonalSettings.aspx">Personal Settings</a></li>
            <li><a href="/pages/WarehouseSettings.aspx">Warehouse Settings</a></li>
        </ul>
    </div>
    <div class="clear"></div>

    <!-- Form -->
    <div class="pxWrap">
        
        <div style="display: none;">
            <asp:RequiredFieldValidator ID="dummyValidator" runat="server" CssClass="hidden" ControlToValidate="dummyTextBox" ValidationGroup="dummy"></asp:RequiredFieldValidator>
            <asp:TextBox runat="server" ID="dummyTextBox" CssClass="hidden"></asp:TextBox>            
        </div>
        
        <asp:UpdatePanel ID="UpdatePanelOne" runat="server">
            <ContentTemplate>
                       
                <!-- Display Company -->
                <div id="section_DisplayCompany" runat="server" Visible="True">
                    
                    <asp:Literal ID="ltr_Debug" runat="server"></asp:Literal>

                    <div class="pxSection">
                        <div class="pxFields">
                            <asp:Label ID="lbl_DisplayLegalEntityName" runat="server" Text="Legal Entity Name" CssClass="pxLabel"></asp:Label>
                            <asp:Literal ID="ltr_LegalEntityName" runat="server"></asp:Literal>
						</div>
                        <div class="clear"></div>
                        <div class="pxFields">
                            <asp:Label ID="lbl_DisplayCompanyName" runat="server" Text="Company Name/DBA" CssClass="pxLabel"></asp:Label>
                            <asp:Literal ID="ltr_CompanyName" runat="server"></asp:Literal>
						</div>
                        <div class="clear"></div>
                    </div>
                    
                    <!-- Display Small Parcel -->
                    <div id="divDisplaySmallParcel" class="pxSection" runat="server">
						<span class="pxSectionHeading">Default shipping billing information - Small Parcel</span>
                        <div class="clear"></div>
						<div class="pxFields" style="width: 149px; margin-right: 24px;">
							<asp:Label ID="lbl_DisplaySmallParcelCarrier" runat="server" Text="Small Parcel Carrier Name" CssClass="pxLabel"></asp:Label>
							<div class="clear"></div>
							<asp:Literal ID="ltr_SmallParcelCarrier" runat="server"></asp:Literal>
						</div>
						<div class="pxFields" style="width: 68px; margin-right: 24px;">
							<asp:Label ID="lbl_DisplayBillingType" runat="server" Text="Billing Type" CssClass="pxLabel"></asp:Label>
							<div class="clear"></div>
							<asp:Literal ID="ltr_SmallParcelBillingType" runat="server"></asp:Literal>
						</div>
						<div class="pxFields" style="width: 106px; margin-right: 24px;">
							<asp:Label ID="lbl_DisplaySmallParcelShippingAccountNumber" runat="server" Text="Shipping Account #" CssClass="pxLabel"></asp:Label>
							<div class="clear"></div>
							<asp:Literal ID="ltr_SmallParcelShippingAccountNumber" runat="server"></asp:Literal>
						</div>
						<div class="pxFields" style="width: 112px;">
							<asp:Label ID="lbl_DisplaySmallParcelBillTo" runat="server" Text="Bill To" CssClass="pxLabel"></asp:Label>
							<div class="clear"></div>
							<asp:Literal ID="ltr_SmallParcelBillTo" runat="server"></asp:Literal>
						</div>
                        <div class="clear"></div>
                    </div>
                    
                    <!-- Display LTL ... currently disabled -->
                    <div class="pxSection" runat="server" Visible="False">
						<span class="pxSectionHeading">Default shipping billing information - LTL</span>
                        <div class="clear"></div>
						<div class="pxFields" style="width: 109px; margin-right: 24px;">
							<asp:Label ID="lbl_DisplayLtlCarrier" runat="server" Text="LTL Carrier Name" CssClass="pxLabel"></asp:Label>
							<div class="clear"></div>
							<asp:Literal ID="ltr_LtlCarrier" runat="server"></asp:Literal>
						</div>
						<div class="pxFields" style="width: 106px; margin-right: 24px;">
							<asp:Label ID="lbl_DisplayLtlShippingAccountNumber" runat="server" Text="Shipping Account #" CssClass="pxLabel"></asp:Label>
							<div class="clear"></div>
							<asp:Literal ID="ltr_LtlShippingAccountNumber" runat="server"></asp:Literal>
						</div>
						<div class="pxFields"  style="width: 112px;">
							<asp:Label ID="lbl_DisplayLtlBillTo" runat="server" Text="Bill To" CssClass="pxLabel"></asp:Label>
							<div class="clear"></div>
							<asp:Literal ID="ltr_LtlBillTo" runat="server"></asp:Literal>
						</div>						
                        <div class="clear"></div>
                    </div>
                    
                    <!-- Display Main & Return Addresses -->
                    <div class="pxSection">
						<div class="pxFields" style="width: 112px; margin-right: 24px;">
							<asp:Label ID="lbl_DisplayMainAddress" runat="server" Text="Main Address" CssClass="pxLabel"></asp:Label>
							<div class="clear"></div>
							<asp:Literal ID="ltr_MainAddress" runat="server"></asp:Literal>
						</div>
						<div class="pxFields" style="width: 112px;">
							<asp:Label ID="lbl_DisplayReturnsAddress" runat="server" Text="Returns Address" CssClass="pxLabel"></asp:Label>
							<div class="clear"></div>
							<asp:Literal ID="ltr_ReturnsAddress" runat="server"></asp:Literal>
						</div>						
                        <div class="clear"></div>
                    </div>
                    
                    <!-- Display Contacts -->
                    <div class="pxSection">
                        <span class="pxSectionHeading">Contact Information:</span>
                        <table class="pxContactInformation">
                            <tr>
                                <th style="min-width: 133px">Contact Type</th>
                                <th style="min-width: 158px">First Name</th>
                                <th style="min-width: 158px">Last Name</th>
                                <th style="min-width: 158px">Phone</th>
                                <th style="min-width: 240px">Email Address</th>
                            </tr>
                            <tr>
                                <td>Primary Business</td>
                                <td><asp:Literal ID="ltr_PrimaryBusinessFirstName" runat="server"></asp:Literal></td>                        
                                <td><asp:Literal ID="ltr_PrimaryBusinessLastName" runat="server"></asp:Literal></td>                        
                                <td><asp:Literal ID="ltr_PrimaryBusinessPhone" runat="server"></asp:Literal></td>                        
                                <td><asp:Literal ID="ltr_PrimaryBusinessEmail" runat="server"></asp:Literal></td>                        
                            </tr>
                            <tr>
                                <td>Customer Service</td>
                                <td><asp:Literal ID="ltr_CustomerServiceFirstName" runat="server"></asp:Literal></td>                        
                                <td><asp:Literal ID="ltr_CustomerServiceLastName" runat="server"></asp:Literal></td>                        
                                <td><asp:Literal ID="ltr_CustomerServicePhone" runat="server"></asp:Literal></td>                        
                                <td><asp:Literal ID="ltr_CustomerServiceEmail" runat="server"></asp:Literal></td>                        
                            </tr>
                            <tr>
                                <td>Accounts Receivable</td>
                                <td><asp:Literal ID="ltr_AccountsReceivableFirstName" runat="server"></asp:Literal></td>                        
                                <td><asp:Literal ID="ltr_AccountsReceivableLastName" runat="server"></asp:Literal></td>                        
                                <td><asp:Literal ID="ltr_AccountsReceivablePhone" runat="server"></asp:Literal></td>                        
                                <td><asp:Literal ID="ltr_AccountsReceivableEmail" runat="server"></asp:Literal></td>                        
                            </tr>
                            <tr>
                                <td>Inventory</td>
                                <td><asp:Literal ID="ltr_InventoryFirstName" runat="server"></asp:Literal></td>                        
                                <td><asp:Literal ID="ltr_InventoryLastName" runat="server"></asp:Literal></td>                        
                                <td><asp:Literal ID="ltr_InventoryPhone" runat="server"></asp:Literal></td>                        
                                <td><asp:Literal ID="ltr_InventoryEmail" runat="server"></asp:Literal></td>                        
                            </tr>
                            <tr>
                                <td>Returns</td>
                                <td><asp:Literal ID="ltr_ReturnsFirstName" runat="server"></asp:Literal></td>                        
                                <td><asp:Literal ID="ltr_ReturnsLastName" runat="server"></asp:Literal></td>                        
                                <td><asp:Literal ID="ltr_ReturnsPhone" runat="server"></asp:Literal></td>                        
                                <td><asp:Literal ID="ltr_ReturnsEmail" runat="server"></asp:Literal></td>                        
                            </tr>
                        </table>					
                    </div>

                    <asp:Button ID="btn_Edit" CssClass="pxBtnCancel" runat="server" Text="Edit Company Settings" OnClick="EditSettings_Click" />
                
                </div>
                
                <!-- Edit Company -->
                <div id="section_EditCompany" runat="server" Visible="False">                    
                    
                        <div class="pxSection">
                                    
                            <!-- Legal Entity -->
                            <div class="pxFields">
                                <asp:Label ID="lbl_legalEntity" runat="server" Text="Legal Entity"></asp:Label>
                                <asp:TextBox ID="txb_legalEntity" runat="server" Width="325px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="require_legalEntity" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_legalEntity" ErrorMessage="Legal Entity is required."></asp:RequiredFieldValidator>        
                            </div>
                            <div class="clear"></div>

                            <!-- Company Name -->
                            <div class="pxFields" >
                                <div class="pxSubFields" >
                                    <asp:CheckBox ID="chb_LegalEntity" runat="server" Text="The name of my company is different." AutoPostBack="True" OnCheckedChanged="ToggleCompanyName" />
                                </div>
                                <div class="clear"></div>
                                <asp:Label ID="lbl_CompanyName" runat="server" Text="Company Name/DBA"></asp:Label>
                                <asp:TextBox ID="txb_CompanyName" runat="server" Width="325px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="require_CompanyName" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_CompanyName" ErrorMessage="Company Name is required."></asp:RequiredFieldValidator>        
                            </div>
                            <div class="clear"></div>

                        </div>
                    
                        <!-- Small Parcel Shipments -->            
                        <div id="divEditSmallParcel" class="pxSection" runat="server">                
                            <span class="pxSectionHeading">Small Parcel Shipments</span>
                            <div class="clear"></div>                                
                    
                            <!-- Default Small Parcel Carrier -->
                            <div class="pxFields" >
                                <asp:Label ID="Label3" runat="server" Text="Default Small Parcel Carrier"></asp:Label>
                                <div class="clear"></div>
                                <asp:DropDownList ID="ddl_SmallParcelCarrier" runat="server" CssClass="pxDropDown" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="SetSmallParcelServiceLevels"></asp:DropDownList>
                            </div>
                    
                            <!-- Default Service Level -->
                            <div class="pxFields" >
                                <asp:Label ID="lbl_SmallParcelServiceLevel" runat="server" Text="Default Service Level"></asp:Label>
                                <div class="clear"></div>
                                <asp:DropDownList ID="ddl_SmallParcelServiceLevel" runat="server" CssClass="pxDropDown" Width="150px"></asp:DropDownList>
                            </div>
                    
                            <!-- Billing Type -->
                            <div class="pxFields">
                                Billing Type
                                <asp:RadioButtonList ID="rbl_SmallParcelBillingType" runat="server" TextAlign="Right" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="ToggleThirdPartyBilling" >
                                    <asp:ListItem Value="Prepaid" Text="Prepaid" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="3rd Party" Text="3rd Party"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="clear"></div>    
                    
                            <!-- My Shipper Acct. Number -->
                            <div class="pxFields" >
                                <asp:Label ID="lbl_SmallParcelShipperAcctNumber" runat="server" Text="My Shipper Acct. Number"></asp:Label>
                                <asp:TextBox ID="txb_SmallParcelShipperAcctNumber" runat="server" Width="120px"></asp:TextBox>
                            </div>            

                            <!-- 3rd Party Shipping Acct. # -->
                            <div class="pxFields" >
                                <asp:Label ID="lbl_SmallParcel3pbShipperAcctNumber" runat="server" Text="3rd Party Shipping Acct. #"></asp:Label>
                                <asp:TextBox ID="txb_SmallParcel3pbShipperAcctNumber" runat="server" Width="120px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="require_3pbShipperAcctNumber" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_SmallParcel3pbShipperAcctNumber" ErrorMessage="Acct. # is required."></asp:RequiredFieldValidator>
                            </div>            
                            <div class="clear"></div>

                            <!-- Bill-to Name -->
                            <div class="pxFields" >
                                <asp:Label ID="lbl_SmallParcelBillToName" runat="server" Text="Company Name"></asp:Label>
                                <asp:TextBox ID="txb_SmallParcelBillToName" runat="server" Width="265px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="require_SmallParcelBillToName" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_SmallParcelBillToName" ErrorMessage="Name is required."></asp:RequiredFieldValidator>
                            </div>            

                            <!-- Phone Number -->
                            <div class="pxFields" >
                                <asp:Label ID="lbl_SmallParcelPhone" runat="server" Text="Phone Number"></asp:Label>
                                <asp:TextBox ID="txb_SmallParcelPhone" runat="server" Width="120px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="require_SmallParcelPhone" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_SmallParcelPhone" ErrorMessage="Phone Number is required."></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator 
                                    ID="ValidPhone01" 
                                    runat="server" 
                                    Display="Dynamic" 
                                    CssClass="pxErrorText" 
                                    ControlToValidate="txb_SmallParcelPhone" 
                                    ValidationExpression="^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}( (|x|ext|ext.)\d{1,5})?$" 
                                    ErrorMessage="Invalid phone number.">                                        
                                </asp:RegularExpressionValidator>
                            </div>            
                            <div class="clear"></div>
                    
                            <!-- Address -->
                            <div class="pxFields" >
                                <asp:Label ID="lbl_SmallParcelBillToAddress" runat="server" Text="Address"></asp:Label>
                                <asp:TextBox ID="txb_SmallParcelBillToAddress" runat="server" Width="120px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="require_SmallParcelBillToAddress" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_SmallParcelBillToAddress" ErrorMessage="Address is required."></asp:RequiredFieldValidator>
                            </div>
            
                            <!-- Suite -->
                            <div class="pxFields" >
                                <asp:Label ID="lbl_SmallParcelBillToSuite" runat="server" Text="Suite, Building, etc."></asp:Label>
                                <asp:TextBox ID="txb_SmallParcelBillToSuite" runat="server" Width="120px"></asp:TextBox>
                            </div>
            
                            <!-- City -->
                            <div class="pxFields" >
                                <asp:Label ID="lbl_SmallParcelBillToCity" runat="server" Text="City"></asp:Label>
                                <asp:TextBox ID="txb_SmallParcelBillToCity" runat="server" Width="120px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="require_SmallParcelBillToCity" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_SmallParcelBillToCity" ErrorMessage="City is required."></asp:RequiredFieldValidator>
                            </div>
            
                            <!-- State -->
                            <div class="pxFields" >
                                <asp:Label ID="lbl_SmallParcelBillToState" runat="server" Text="State"></asp:Label>
                                <div class="clear"></div>
                                <asp:DropDownList ID="ddl_SmallParcelBillToState" runat="server" CssClass="pxDropDown" Width="80px"></asp:DropDownList>
                            </div>
            
                            <!-- Zip -->
                            <div class="pxFields" >
                                <asp:Label ID="lbl_SmallParcelBillToZip" runat="server" Text="Zip"></asp:Label>
                                <asp:TextBox ID="txb_SmallParcelBillToZip" runat="server" Width="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="require_SmallParcelBillToZip" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_SmallParcelBillToZip" ErrorMessage="Zip is required."></asp:RequiredFieldValidator>
				                <asp:RegularExpressionValidator 
					                ID="ValidZipCode01" 
					                runat="server" 
					                Display="Dynamic" 
					                CssClass="pxErrorText" 
					                ControlToValidate="txb_SmallParcelBillToZip" 
					                ValidationExpression="^\d{5}(?:[-\s]\d{4})?$" 
					                ErrorMessage="Invalid zip code.">                                        
				                </asp:RegularExpressionValidator>
                            </div>            
                            <div class="clear"></div>

                        </div>                
                
                        <!-- LTL Shipments ... currently disabled -->            
                        <div class="pxSection" runat="server" Visible="False">                
                            <span class="pxSectionHeading">LTL Shipments</span>
                            <div class="clear"></div>                                
                    
                            <!-- Default LTL Carrier -->
                            <div class="pxFields" >
                                <asp:Label ID="Label4" runat="server" Text="Default LTL Carrier"></asp:Label>
                                <div class="clear"></div>
                                <asp:DropDownList ID="ddl_LtlCarrier" runat="server" CssClass="pxDropDown" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="SetLtlServiceLevels"></asp:DropDownList>
                            </div>
                    
                            <!-- Default Service Level -->
                            <div class="pxFields" >
                                <asp:Label ID="lbl_LtlServiceLevel" runat="server" Text="Default Service Level"></asp:Label>
                                <div class="clear"></div>
                                <asp:DropDownList ID="ddl_LtlServiceLevel" runat="server" CssClass="pxDropDown" Width="150px"></asp:DropDownList>
                            </div>
                            <div class="clear"></div>    
                    
                            <!-- My Shipper Acct. Number -->
                            <div class="pxFields" >
                                <asp:Label ID="lbl_LtlShipperAcctNumber" runat="server" Text="My Shipper Acct. Number"></asp:Label>
                                <asp:TextBox ID="txb_LtlShipperAcctNumber" runat="server" Width="120px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="required_LtlShipperAcctNumber" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_LtlShipperAcctNumber" ErrorMessage="Shipper Acct. Number is required." Enabled="False"></asp:RequiredFieldValidator>
                            </div>            
                            <div class="clear"></div>
                    
                            <!-- Bill-to Name -->
                            <div class="pxFields" >
                                <asp:Label ID="lbl_LtlBillToName" runat="server" Text="Company Name"></asp:Label>
                                <asp:TextBox ID="txb_LtlBillToName" runat="server" Width="265px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="required_LtlBillToName" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_LtlBillToName" ErrorMessage="Name is required." Enabled="False"></asp:RequiredFieldValidator>
                            </div>            

                            <!-- Phone Number -->
                            <div class="pxFields" >
                                <asp:Label ID="lbl_LtlPhone" runat="server" Text="Phone Number"></asp:Label>
                                <asp:TextBox ID="txb_LtlPhone" runat="server" Width="120px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="required_LtlPhone" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_LtlPhone" ErrorMessage="Phone is required." Enabled="False"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator 
                                    ID="ValidPhone02"
                                    Enabled="False" 
                                    runat="server" 
                                    Display="Dynamic" 
                                    CssClass="pxErrorText" 
                                    ControlToValidate="txb_LtlPhone" 
                                    ValidationExpression="^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}( (|x|ext|ext.)\d{1,5})?$" 
                                    ErrorMessage="Invalid phone number.">                                        
                                </asp:RegularExpressionValidator>
                            </div>            
                            <div class="clear"></div>

                            <!-- Address -->
                            <div class="pxFields" >
                                <asp:Label ID="lbl_LtlBillToAddress" runat="server" Text="Address"></asp:Label>
                                <asp:TextBox ID="txb_LtlBillToAddress" runat="server" Width="120px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="required_LtlBillToAddress" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_LtlBillToAddress" ErrorMessage="Address is required." Enabled="False"></asp:RequiredFieldValidator>
                            </div>
            
                            <!-- Suite -->
                            <div class="pxFields" >
                                <asp:Label ID="lbl_LtlBillToSuite" runat="server" Text="Suite, Building, etc."></asp:Label>
                                <asp:TextBox ID="txb_LtlBillToSuite" runat="server" Width="120px"></asp:TextBox>
                            </div>
            
                            <!-- City -->
                            <div class="pxFields" >
                                <asp:Label ID="lbl_LtlBillToCity" runat="server" Text="City"></asp:Label>
                                <asp:TextBox ID="txb_LtlBillToCity" runat="server" Width="120px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="required_LtlBillToCity" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_LtlBillToCity" ErrorMessage="City is required." Enabled="False"></asp:RequiredFieldValidator>
                            </div>
            
                            <!-- State -->
                            <div class="pxFields" >
                                <asp:Label ID="lbl_LtlBillToState" runat="server" Text="State"></asp:Label>
                                <div class="clear"></div>
                                <asp:DropDownList ID="ddl_LtlBillToState" runat="server" CssClass="pxDropDown" Width="80px"></asp:DropDownList>
                            </div>
            
                            <!-- Zip -->
                            <div class="pxFields" >
                                <asp:Label ID="lbl_LtlBillToZip" runat="server" Text="Zip"></asp:Label>
                                <asp:TextBox ID="txb_LtlBillToZip" runat="server" Width="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="required_LtlBillToZip" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_LtlBillToZip" ErrorMessage="Zip is required." Enabled="False"></asp:RequiredFieldValidator>
				                <asp:RegularExpressionValidator 
					                ID="ValidZipCode02" 
                                    Enabled="False"
					                runat="server" 
					                Display="Dynamic" 
					                CssClass="pxErrorText" 
					                ControlToValidate="txb_LtlBillToZip" 
					                ValidationExpression="^\d{5}(?:[-\s]\d{4})?$" 
					                ErrorMessage="Invalid zip code.">                                        
				                </asp:RegularExpressionValidator>
                            </div>            
                            <div class="clear"></div>   
                    
                            <!-- Special Shipping Instructions -->
                            <div class="pxFields" >
                                <asp:Label ID="lbl_LtlShippingInstructions" runat="server" Text="Special Shipping Instructions"></asp:Label>
                                <div class="clear"></div>  
                                <asp:TextBox ID="txb_LtlShippingInstructions" runat="server" Width="415px" TextMode="MultiLine" Rows="4"></asp:TextBox>
                            </div>                                             
                            <div class="clear"></div>   

                        </div>                                    
                    
                        <!-- Main Company Address -->
                        <div class="pxSection">
                            <span class="pxSectionHeading">Main Company Address</span>
                            <div class="clear"></div>

                            <!-- Address -->
                            <div class="pxFields" >
                                <asp:Label ID="lbl_Address" runat="server" Text="Address"></asp:Label>
                                <asp:TextBox ID="txb_Address" runat="server" Width="120px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="require_Address" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_Address" ErrorMessage="Address is required."></asp:RequiredFieldValidator>
                            </div>
            
                            <!-- Suite -->
                            <div class="pxFields" >
                                <asp:Label ID="lbl_Suite" runat="server" Text="Suite"></asp:Label>
                                <asp:TextBox ID="txb_Suite" runat="server" Width="120px"></asp:TextBox>
                            </div>
            
                            <!-- City -->
                            <div class="pxFields" >
                                <asp:Label ID="lbl_City" runat="server" Text="City"></asp:Label>
                                <asp:TextBox ID="txb_City" runat="server" Width="120px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="require_City" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_City" ErrorMessage=" is required."></asp:RequiredFieldValidator>
                            </div>
            
                            <!-- State -->
                            <div class="pxFields" >
                                <asp:Label ID="lbl_State" runat="server" Text="State"></asp:Label>
                                <div class="clear"></div>
                                <asp:DropDownList ID="ddl_State" runat="server" CssClass="pxDropDown" Width="80px"></asp:DropDownList>
                            </div>
            
                            <!-- Zip -->
                            <div class="pxFields" >
                                <asp:Label ID="lbl_Zip" runat="server" Text="Zip"></asp:Label>
                                <asp:TextBox ID="txb_Zip" runat="server" Width="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="require_Zip" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_Zip" ErrorMessage="Zip is required."></asp:RequiredFieldValidator>
				                <asp:RegularExpressionValidator 
					                ID="ValidZipCode03" 
					                runat="server" 
					                Display="Dynamic" 
					                CssClass="pxErrorText" 
					                ControlToValidate="txb_Zip" 
					                ValidationExpression="^\d{5}(?:[-\s]\d{4})?$" 
					                ErrorMessage="Invalid zip code.">                                        
				                </asp:RegularExpressionValidator>
                            </div>            
                            <div class="clear"></div>
                        </div>

                        <!-- Product Returns Address -->
                        <div class="pxSection">
                            <span class="pxSectionHeading">Returns address: (Must not be a P.O. Box)</span>
                            <div class="clear"></div>                

                            <!-- Same Returns Address -->
                            <div class="pxFields" >
                                <asp:CheckBox ID="chb_SameReturnsAddress" runat="server" Text="Returns address is the same as the main address." AutoPostBack="True" OnCheckedChanged="ToggleReturnsAddress" />
                            </div>
                            <div class="clear"></div>                

                            <!-- Returns Address -->
                            <div class="pxFields" >
                                <asp:Label ID="lbl_ReturnsAddress" runat="server" Text="Address"></asp:Label>
                                <asp:TextBox ID="txb_ReturnsAddress" runat="server" Width="120px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="require_ReturnsAddress" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_ReturnsAddress" ErrorMessage="Returns Address is required."></asp:RequiredFieldValidator>
                            </div>
            
                            <!-- Returns Suite -->
                            <div class="pxFields" >
                                <asp:Label ID="lbl_ReturnsSuite" runat="server" Text="Suite"></asp:Label>
                                <asp:TextBox ID="txb_ReturnsSuite" runat="server" Width="120px"></asp:TextBox>
                            </div>
            
                            <!-- Returns City -->
                            <div class="pxFields" >
                                <asp:Label ID="lbl_ReturnsCity" runat="server" Text="City"></asp:Label>
                                <asp:TextBox ID="txb_ReturnsCity" runat="server" Width="120px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="require_ReturnsCity" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_ReturnsCity" ErrorMessage="Returns City is required."></asp:RequiredFieldValidator>
                            </div>
            
                            <!-- Returns State -->
                            <div class="pxFields" >
                                <asp:Label ID="lbl_ReturnsState" runat="server" Text="State"></asp:Label>
                                <div class="clear"></div>
                                <asp:DropDownList ID="ddl_ReturnsState" runat="server" CssClass="pxDropDown" Width="80px"></asp:DropDownList>
                            </div>
            
                            <!-- Returns Zip -->
                            <div class="pxFields" >
                                <asp:Label ID="lbl_ReturnsZip" runat="server" Text="Zip"></asp:Label>
                                <asp:TextBox ID="txb_ReturnsZip" runat="server" Width="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="require_ReturnsZip" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_ReturnsZip" ErrorMessage="Returns Zip is required."></asp:RequiredFieldValidator>
				                <asp:RegularExpressionValidator 
					                ID="ValidZipCode04" 
					                runat="server" 
					                Display="Dynamic" 
					                CssClass="pxErrorText" 
					                ControlToValidate="txb_ReturnsZip" 
					                ValidationExpression="^\d{5}(?:[-\s]\d{4})?$" 
					                ErrorMessage="Invalid zip code.">                                        
				                </asp:RegularExpressionValidator>
                            </div>            
                            <div class="clear"></div>
                        </div>
            
                        <div class="pxSection">
                            <span class="pxSectionHeading">Contact Information</span>
                            <div class="clear"></div>                               
                            <table class="pxContactInformation">
                                <tr>
                                    <th>Contact Type</th>
                                    <th>First Name</th>
                                    <th>Last Name</th>
                                    <th>Phone</th>
                                    <th>Email Address</th>
                                </tr>
                                <tr>
                                    <td>Primary Business</td>
                                    <td>
                                        <asp:TextBox ID="txb_ContactPrimaryBusinessFirstName" runat="server" Width="150px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="require_ContactFirstName01" ControlToValidate="txb_ContactPrimaryBusinessFirstName" ErrorMessage="First name is required." runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
                                    </td>                        
                                    <td>
                                        <asp:TextBox ID="txb_ContactPrimaryBusinessLastName" runat="server" Width="150px"></asp:TextBox>
										<asp:RequiredFieldValidator ID="require_ContactLastName01" ControlToValidate="txb_ContactPrimaryBusinessLastName" ErrorMessage="Last name is required." runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
                                    </td>                        
                                    <td>
                                        <asp:TextBox ID="txb_ContactPrimaryBusinessPhone" runat="server" Width="150px"></asp:TextBox>
										<asp:RequiredFieldValidator ID="require_ContactPhone01" ControlToValidate="txb_ContactPrimaryBusinessPhone" ErrorMessage="Phone is required." runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator 
                                            ID="ValidPhone03" 
                                            runat="server" 
                                            Display="Dynamic" 
                                            CssClass="pxErrorText" 
                                            ControlToValidate="txb_ContactPrimaryBusinessPhone" 
                                            ValidationExpression="^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}( (|x|ext|ext.)\d{1,5})?$" 
                                            ErrorMessage="Invalid phone number.">                                        
                                        </asp:RegularExpressionValidator>
                                    </td>                        
                                    <td>
                                        <asp:TextBox ID="txb_ContactPrimaryBusinessEmail" runat="server" Width="200px"></asp:TextBox>
										<asp:RequiredFieldValidator ID="require_ContactEmail01" ControlToValidate="txb_ContactPrimaryBusinessEmail" ErrorMessage="Email is required." runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
								        <asp:RegularExpressionValidator 
									        ID="ValidEmail01" 
									        runat="server" 
									        Display="Dynamic" 
									        CssClass="pxErrorText" 
									        ControlToValidate="txb_ContactPrimaryBusinessEmail" 
									        ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$" 
									        ErrorMessage="Invalid email address.">                                        
								        </asp:RegularExpressionValidator>
                                    </td>                        
                                </tr>
                                <tr>
                                    <td>Customer Service</td>
                                    <td>
                                        <asp:TextBox ID="txb_ContactCustomerServiceFirstName" runat="server" Width="150px"></asp:TextBox>
										<asp:RequiredFieldValidator ID="require_ContactFirstName02" ControlToValidate="txb_ContactCustomerServiceFirstName" ErrorMessage="First name is required." runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
                                    </td>                        
                                    <td>
                                        <asp:TextBox ID="txb_ContactCustomerServiceLastName" runat="server" Width="150px"></asp:TextBox>
										<asp:RequiredFieldValidator ID="require_ContactLastName02" ControlToValidate="txb_ContactCustomerServiceLastName" ErrorMessage="Last name is required." runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
                                    </td>                        
                                    <td>
                                        <asp:TextBox ID="txb_ContactCustomerServicePhone" runat="server" Width="150px"></asp:TextBox>
										<asp:RequiredFieldValidator ID="require_ContactPhone02" ControlToValidate="txb_ContactCustomerServicePhone" ErrorMessage="Phone is required." runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator 
                                            ID="ValidPhone04" 
                                            runat="server" 
                                            Display="Dynamic" 
                                            CssClass="pxErrorText" 
                                            ControlToValidate="txb_ContactCustomerServicePhone" 
                                            ValidationExpression="^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}( (|x|ext|ext.)\d{1,5})?$" 
                                            ErrorMessage="Invalid phone number.">                                        
                                        </asp:RegularExpressionValidator>
                                    </td>                        
                                    <td>
                                        <asp:TextBox ID="txb_ContactCustomerServiceEmail" runat="server" Width="200px"></asp:TextBox>
										<asp:RequiredFieldValidator ID="require_ContactEmail02" ControlToValidate="txb_ContactCustomerServiceEmail" ErrorMessage="Email is required." runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
										<asp:RegularExpressionValidator 
											ID="ValidEmail02" 
											runat="server" 
											Display="Dynamic" 
											CssClass="pxErrorText" 
											ControlToValidate="txb_ContactCustomerServiceEmail" 
											ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$" 
											ErrorMessage="Invalid email address.">                                        
										</asp:RegularExpressionValidator>
                                    </td>                        
                                </tr>
                                <tr>
                                    <td>Accounts Receivable</td>
                                    <td>
                                        <asp:TextBox ID="txb_ContactAccountsReceivableFirstName" runat="server" Width="150px"></asp:TextBox>
										<asp:RequiredFieldValidator ID="require_ContactFirstName03" ControlToValidate="txb_ContactAccountsReceivableFirstName" ErrorMessage="First name is required." runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
                                    </td>                        
                                    <td>
                                        <asp:TextBox ID="txb_ContactAccountsReceivableLastName" runat="server" Width="150px"></asp:TextBox>
										<asp:RequiredFieldValidator ID="require_ContactLastName03" ControlToValidate="txb_ContactAccountsReceivableLastName" ErrorMessage="Last name is required." runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
                                    </td>                        
                                    <td>
                                        <asp:TextBox ID="txb_ContactAccountsReceivablePhone" runat="server" Width="150px"></asp:TextBox>
										<asp:RequiredFieldValidator ID="require_ContactPhone03" ControlToValidate="txb_ContactAccountsReceivablePhone" ErrorMessage="Phone is required." runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator 
                                            ID="ValidPhone05" 
                                            runat="server" 
                                            Display="Dynamic" 
                                            CssClass="pxErrorText" 
                                            ControlToValidate="txb_ContactAccountsReceivablePhone" 
                                            ValidationExpression="^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}( (|x|ext|ext.)\d{1,5})?$" 
                                            ErrorMessage="Invalid phone number.">                                        
                                        </asp:RegularExpressionValidator>
                                    </td>                        
                                    <td>
                                        <asp:TextBox ID="txb_ContactAccountsReceivableEmail" runat="server" Width="200px"></asp:TextBox>
										<asp:RequiredFieldValidator ID="require_ContactEmail03" ControlToValidate="txb_ContactAccountsReceivableEmail" ErrorMessage="Email is required." runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
										<asp:RegularExpressionValidator 
											ID="ValidEmail03" 
											runat="server" 
											Display="Dynamic" 
											CssClass="pxErrorText" 
											ControlToValidate="txb_ContactAccountsReceivableEmail" 
											ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$" 
											ErrorMessage="Invalid email address.">                                        
										</asp:RegularExpressionValidator>
                                    </td>                        
                                </tr>
                                <tr>
                                    <td>Inventory</td>
                                    <td>
                                        <asp:TextBox ID="txb_ContactInventoryFirstName" runat="server" Width="150px"></asp:TextBox>
										<asp:RequiredFieldValidator ID="require_ContactFirstName04" ControlToValidate="txb_ContactInventoryFirstName" ErrorMessage="First name is required." runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
                                    </td>                        
                                    <td>
                                        <asp:TextBox ID="txb_ContactInventoryLastName" runat="server" Width="150px"></asp:TextBox>
										<asp:RequiredFieldValidator ID="require_ContactLastName04" ControlToValidate="txb_ContactInventoryLastName" ErrorMessage="Last name is required." runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
                                    </td>                        
                                    <td>
                                        <asp:TextBox ID="txb_ContactInventoryPhone" runat="server" Width="150px"></asp:TextBox>
										<asp:RequiredFieldValidator ID="require_ContactPhone04" ControlToValidate="txb_ContactInventoryPhone" ErrorMessage="Phone is required." runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator 
                                            ID="ValidPhone06" 
                                            runat="server" 
                                            Display="Dynamic" 
                                            CssClass="pxErrorText" 
                                            ControlToValidate="txb_ContactInventoryPhone" 
                                            ValidationExpression="^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}( (|x|ext|ext.)\d{1,5})?$" 
                                            ErrorMessage="Invalid phone number.">                                        
                                        </asp:RegularExpressionValidator>
                                    </td>                        
                                    <td>
                                        <asp:TextBox ID="txb_ContactInventoryEmail" runat="server" Width="200px"></asp:TextBox>
										<asp:RequiredFieldValidator ID="require_ContactEmail04" ControlToValidate="txb_ContactInventoryEmail" ErrorMessage="Email is required." runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
										<asp:RegularExpressionValidator 
											ID="ValidEmail04" 
											runat="server" 
											Display="Dynamic" 
											CssClass="pxErrorText" 
											ControlToValidate="txb_ContactInventoryEmail" 
											ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$" 
											ErrorMessage="Invalid email address.">                                        
										</asp:RegularExpressionValidator>
                                    </td>                        
                                </tr>
                                <tr>
                                    <td>Returns</td>
                                    <td>
                                        <asp:TextBox ID="txb_ContactReturnsFirstName" runat="server" Width="150px"></asp:TextBox>
										<asp:RequiredFieldValidator ID="require_ContactFirstName05" ControlToValidate="txb_ContactReturnsFirstName" ErrorMessage="First name is required." runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
                                    </td>                        
                                    <td>
                                        <asp:TextBox ID="txb_ContactReturnsLastName" runat="server" Width="150px"></asp:TextBox>
										<asp:RequiredFieldValidator ID="require_ContactLastName05" ControlToValidate="txb_ContactReturnsLastName" ErrorMessage="Last name is required." runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
                                    </td>                        
                                    <td>
                                        <asp:TextBox ID="txb_ContactReturnsPhone" runat="server" Width="150px"></asp:TextBox>                        
										<asp:RequiredFieldValidator ID="require_ContactPhone05" ControlToValidate="txb_ContactReturnsPhone" ErrorMessage="Phone is required." runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator 
                                            ID="ValidPhone07" 
                                            runat="server" 
                                            Display="Dynamic" 
                                            CssClass="pxErrorText" 
                                            ControlToValidate="txb_ContactReturnsPhone" 
                                            ValidationExpression="^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}( (|x|ext|ext.)\d{1,5})?$" 
                                            ErrorMessage="Invalid phone number.">                                        
                                        </asp:RegularExpressionValidator>
                                    </td>                        
                                    <td>
                                        <asp:TextBox ID="txb_ContactReturnsEmail" runat="server" Width="200px"></asp:TextBox>
										<asp:RequiredFieldValidator ID="require_ContactEmail05" ControlToValidate="txb_ContactReturnsEmail" ErrorMessage="Email is required." runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
										<asp:RegularExpressionValidator 
											ID="ValidEmail05" 
											runat="server" 
											Display="Dynamic" 
											CssClass="pxErrorText" 
											ControlToValidate="txb_ContactReturnsEmail" 
											ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$" 
											ErrorMessage="Invalid email address.">                                        
										</asp:RegularExpressionValidator>
                                    </td>                        
                                </tr>
                            </table>
                        </div>

                    <asp:Button ID="btn_SubmitChanges" CssClass="pxBtn" runat="server" Text="Submit Changes" OnClick="SubmitChanges_Click" />
                    <asp:Button ID="btn_Cancel" CssClass="pxBtnCancel" runat="server" Text="Cancel" OnClick="CancelChanges_Click" CausesValidation="False" />
                
                </div>
                
	        </ContentTemplate>
        </asp:UpdatePanel>
                
    </div>
