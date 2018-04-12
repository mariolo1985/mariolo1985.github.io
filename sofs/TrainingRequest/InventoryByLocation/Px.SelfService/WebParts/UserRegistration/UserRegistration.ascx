<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserRegistration.ascx.cs" Inherits="Px.SelfService.WebParts.UserRegistration.UserRegistration" %>

    <script type="text/javascript">

        function CustomerServiceSameAsPrimary() {
            var checkbox = document.getElementById("<%= chb_ContactCustomerServiceSame.ClientID %>");
            document.getElementById("<%= txb_ContactCustomerServiceFirstName.ClientID %>").value = (checkbox.checked == true) ?
                document.getElementById("<%= txb_ContactPrimaryBusinessFirstName.ClientID %>").value : "";
            document.getElementById("<%= txb_ContactCustomerServiceLastName.ClientID %>").value = (checkbox.checked == true) ?
                document.getElementById("<%= txb_ContactPrimaryBusinessLastName.ClientID %>").value : "";
            document.getElementById("<%= txb_ContactCustomerServicePhone.ClientID %>").value = (checkbox.checked == true) ?
                document.getElementById("<%= txb_ContactPrimaryBusinessPhone.ClientID %>").value : "";
            document.getElementById("<%= txb_ContactCustomerServiceEmail.ClientID %>").value = (checkbox.checked == true) ?
                document.getElementById("<%= txb_ContactPrimaryBusinessEmail.ClientID %>").value : "";
        }

        function AccountsReceivableSameAsPrimary() {
            var checkbox = document.getElementById("<%= chb_ContactAccountsReceivableSame.ClientID %>");
            document.getElementById("<%= txb_ContactAccountsReceivableFirstName.ClientID %>").value = (checkbox.checked == true) ?
                document.getElementById("<%= txb_ContactPrimaryBusinessFirstName.ClientID %>").value : "";
            document.getElementById("<%= txb_ContactAccountsReceivableLastName.ClientID %>").value = (checkbox.checked == true) ?
                document.getElementById("<%= txb_ContactPrimaryBusinessLastName.ClientID %>").value : "";
            document.getElementById("<%= txb_ContactAccountsReceivablePhone.ClientID %>").value = (checkbox.checked == true) ?
                document.getElementById("<%= txb_ContactPrimaryBusinessPhone.ClientID %>").value : "";
            document.getElementById("<%= txb_ContactAccountsReceivableEmail.ClientID %>").value = (checkbox.checked == true) ?
                document.getElementById("<%= txb_ContactPrimaryBusinessEmail.ClientID %>").value : "";
        }

        function InventorySameAsPrimary() {
            var checkbox = document.getElementById("<%= chb_ContactInventorySame.ClientID %>");
            document.getElementById("<%= txb_ContactInventoryFirstName.ClientID %>").value = (checkbox.checked == true) ?
                document.getElementById("<%= txb_ContactPrimaryBusinessFirstName.ClientID %>").value : "";
            document.getElementById("<%= txb_ContactInventoryLastName.ClientID %>").value = (checkbox.checked == true) ?
                document.getElementById("<%= txb_ContactPrimaryBusinessLastName.ClientID %>").value : "";
            document.getElementById("<%= txb_ContactInventoryPhone.ClientID %>").value = (checkbox.checked == true) ?
                document.getElementById("<%= txb_ContactPrimaryBusinessPhone.ClientID %>").value : "";
            document.getElementById("<%= txb_ContactInventoryEmail.ClientID %>").value = (checkbox.checked == true) ?
                        document.getElementById("<%= txb_ContactPrimaryBusinessEmail.ClientID %>").value : "";
        }

        function ReturnsSameAsPrimary() {
            var checkbox = document.getElementById("<%= chb_ContactReturnsSame.ClientID %>");
            document.getElementById("<%= txb_ContactReturnsFirstName.ClientID %>").value = (checkbox.checked == true) ?
                document.getElementById("<%= txb_ContactPrimaryBusinessFirstName.ClientID %>").value : "";
            document.getElementById("<%= txb_ContactReturnsLastName.ClientID %>").value = (checkbox.checked == true) ?
                document.getElementById("<%= txb_ContactPrimaryBusinessLastName.ClientID %>").value : "";
            document.getElementById("<%= txb_ContactReturnsPhone.ClientID %>").value = (checkbox.checked == true) ?
                document.getElementById("<%= txb_ContactPrimaryBusinessPhone.ClientID %>").value : "";
            document.getElementById("<%= txb_ContactReturnsEmail.ClientID %>").value = (checkbox.checked == true) ?
                document.getElementById("<%= txb_ContactPrimaryBusinessEmail.ClientID %>").value : "";
        }

        function ToggleLTL(item) {
            if (item.value == "Yes") {
                ValidatorEnable(document.getElementById("<%= required_LtlShipperAcctNumber.ClientID %>"), true);
                ValidatorEnable(document.getElementById("<%= required_LtlBillToName.ClientID %>"), true);
                ValidatorEnable(document.getElementById("<%= required_LtlPhone.ClientID %>"), true);
                ValidatorEnable(document.getElementById("<%= required_LtlBillToAddress.ClientID %>"), true);
                ValidatorEnable(document.getElementById("<%= required_LtlBillToCity.ClientID %>"), true);
                ValidatorEnable(document.getElementById("<%= required_LtlBillToZip.ClientID %>"), true);
            }
            else if (item.value == "No") {
                ValidatorEnable(document.getElementById("<%= required_LtlShipperAcctNumber.ClientID %>"), false);
                ValidatorEnable(document.getElementById("<%= required_LtlBillToName.ClientID %>"), false);
                ValidatorEnable(document.getElementById("<%= required_LtlPhone.ClientID %>"), false);
                ValidatorEnable(document.getElementById("<%= required_LtlBillToAddress.ClientID %>"), false);
                ValidatorEnable(document.getElementById("<%= required_LtlBillToCity.ClientID %>"), false);
                ValidatorEnable(document.getElementById("<%= required_LtlBillToZip.ClientID %>"), false);                
            }
        }
        
        function LearnMore() {
            var html = "<div class='modal fade'><div class='modal-dialog'><div class='modal-content'><div class='modal-body'><p>Our third-party billing program allows you to access discounted shipping rates by shipping your orders using one of our designated shipping accounts. Contact your Partner Care Associate to learn more about this program.</p></div><div class='modal-footer'><button type='button' class='btn btn-default' data-dismiss='modal'>Close</button></div></div></div></div>";
            $(html).modal();
        }

        function ContactRepresentative()
        {
            var options = {
                title: 'Contact a Representative.',
                url: '/Pages/ContactRepresentative.aspx',
                autoSize: true,
                showClose: true,
            };
            SP.UI.ModalDialog.showModalDialog(options);
        }

        function OnUpdateValidators() {
            for (var i = 0; i < Page_Validators.length; i++) {
                var validator = Page_Validators[i];
                var control = document.getElementById(validator.controltovalidate);
                if (control != null && control.style != null) {
                    control.style.border = CheckValidatorsForControl(control) ? "1px solid #ABABAB" : "1px solid #F6AA4F";
                }
            }
        }

        function CheckValidatorsForControl(control) {
            for (var i = 0; i < control.Validators.length; i++) {
                if (!control.Validators[i].isvalid) {
                    return false;
                }
            }
            return true;
        }

    </script>

    <style>

        .welcome-content {margin-top:0 !important;}  

        #s4-workspace { width:950px !important; height:auto !important; border-radius: 4px; font-family:"Open Sans", Helvetica, Arial, sans-serif; margin-top: 20px !important; padding-bottom: 0 !important;}
        #s4-bodyContainer { padding-bottom: 0 !important;  }
        #AnonymousHeader { background: #FFFFFF; padding: 7px; margin-top: -15px }
        .btnStep2 { height:28px; width:132px !important; margin-top: 20px !important; background-image: url('/_layouts/15/Px.Branding/Images/step2.png'); background-color: transparent !important; margin-left: 0 !important;}
        .btnStep2:hover { background-image: url('/_layouts/15/Px.Branding/Images/Step2Hover.png');}
        .btnStep3 { height:28px; width:132px !important; margin-top: 20px !important; background-image: url('/_layouts/15/Px.Branding/Images/step3.png'); background-color: transparent !important; margin-left: 0 !important;}
        .btnStep3:hover { background-image: url('/_layouts/15/Px.Branding/Images/Step3Hover.png');}
        .btnStepFinal { height:28px; width:151px !important; margin-top: 20px !important; background-image: url('/_layouts/15/Px.Branding/Images/stepFinal.png'); background-color: transparent !important; margin-left: 0 !important;}
        .btnStepFinal:hover { background-image: url('/_layouts/15/Px.Branding/Images/StepFinalHover.png');}
        .btnStepBack { height:28px; width:52px !important; min-width: 52px !important; margin-top: 20px !important; margin-right: 10px !important; background-image: url('/_layouts/15/Px.Branding/Images/Back.gif'); background-color: transparent !important; margin-left: 0 !important;}
        
        .absolute {position: absolute;}
        .activeStep { display: block; }
        .inactiveStep { display: none; }
        .active3PB { display: block; }
        .inactive3PB { display: none; }
        .pxSignUpSection, .pxSignUpSection2 { border-bottom: 1px solid #C1C1C1; margin-bottom: 5px; padding-bottom: 15px;}
        .pxSignUpSection2 { margin-bottom: 0; margin-top: 16px; }
        div.pxSignUpSection:last-of-type { border-bottom: none; }
        .pxSignUpBreadcrumb li { display: inline; }
        .pxSectionHeading { color: #454545; font-weight: normal; font-family: Open Sans Light; font-size: 18px; text-transform: uppercase; margin-bottom: 10px; }
        .pxDropDown { height: 30px; padding: 0 0 0 4px; line-height: 25px; margin-top: -1px !important; }
        .pxContactInformation { border-spacing:0; border-collapse:collapse; margin-top: 8px; font: 13px Open Sans; color: #454545; }
        .pxContactInformation th { border: 1px solid #C1C1C1; padding: 6px; font: 12px Open Sans bold; background-color: #EBEBEB; text-align: left; }
        .pxContactInformation th:first-child {border: 1px solid #cccccc; border-top-left-radius: 3px;}
        .pxContactInformation th:last-child { border: 1px solid #cccccc; border-top-right-radius: 3px; }
        .pxContactInformation td {background-color: #FFFFFF; border: 1px solid #C1C1C1; padding: 9px 6px 9px 6px; }
        .pxContactInformation td input { margin: 0; }
        .pxContactInformation tr:last-child td:first-child { border: 1px solid #cccccc; border-bottom-left-radius: 3px;}
        .pxContactInformation tr:last-child td:last-child{ border: 1px solid #cccccc; border-bottom-right-radius: 3px;}

        .disabledLabel { color: #C1C1C1 }
        .pxBackButton { float: left; }
        .pxForwardButton { float: left; background-color: #3399FF !important; }    
        .pxErrorText { font-weight: normal; color: #d6760e; }    
        .pxContent { padding: 30px; padding-bottom: 0; }

        .progressBreadcrumbs {width:100%;height:35px !important;margin:0;overflow:hidden;color:#666666;font-size:16px;font-weight:300; margin-bottom: 8px;}
        .progressBreadcrumbs div {width:33.3%; height:35px !important;display:inline-block;float:left;overflow:hidden;background:url('/_layouts/15/Px.Branding/Images/breadXLine.png') repeat-x;}
        .progressBreadcrumbs div span.start, .progressBreadcrumbs div span.end {display:inline-block;width:16px;height:35px;}
        .progressBreadcrumbs div.active {background-color:#FFFFFF;}
        .progressBreadcrumbs div span.start {float:left;background:url('/_layouts/15/Px.Branding/Images/breadArrowStartInactive.png') top right no-repeat;}
        .progressBreadcrumbs div span.end {float:right;background:url('/_layouts/15/Px.Branding/Images/breadArrowEndInactive.png') top right no-repeat;}
        .progressBreadcrumbs .active span.start {background:url('/_layouts/15/Px.Branding/Images/breadArrowStartActive.png') top right no-repeat;}
        .progressBreadcrumbs .active span.end {background:url('/_layouts/15/Px.Branding/Images/breadArrowEndActive.png') top right no-repeat;}
        .progressBreadcrumbs .first span.start {background:url('/_layouts/15/Px.Branding/Images/breadYLine.png') top right no-repeat;width:1px;}
        .progressBreadcrumbs .last span.end {background:url('/_layouts/15/Px.Branding/Images/breadYLine.png') top right no-repeat;width:1px;}
        .progressBreadcrumbs div span.breadText {line-height:35px;padding-left:10px;}
        .legal { font-size: 12px; }
    </style>

    <!-- Header -->
    <div id="AnonymousHeader">
        <div><img class="logo" src="/_layouts/15/Px.Branding/Images/Logo-195x50.png" alt=""></div>
    </div>

    <div class="pxContent">
        
        <div style="display: none;">
            <asp:RequiredFieldValidator ID="dummyValidator" runat="server" CssClass="hidden" ControlToValidate="dummyTextBox" ValidationGroup="dummy"></asp:RequiredFieldValidator>
            <asp:TextBox runat="server" ID="dummyTextBox" CssClass="hidden"></asp:TextBox>            
        </div>        

        <!-- Title -->
        <div style="margin-bottom:10px;">
            <span class="pxTitle">Sign Up</span>
            <div style="float:right;">
                Already have an account? <a href="/_layouts/15/Authenticate.aspx" style="display: inline;">Sign In</a>
            </div>
        </div>

        <!-- Breadcrumb -->
        <div class="progressBreadcrumbs">
	        <div id="menu_StepOne" runat="server" class="active first">
		        <span class="start"></span>
			        <span class="breadText">Step 1: User Details</span>
		        <span class="end"></span>
	        </div>
	        <div id="menu_StepTwo" runat="server" >
		        <span class="start"></span>
			        <span class="breadText">Step 2: Company Details</span>
		        <span class="end"></span>
	        </div>
	        <div id="menu_StepThree" runat="server" class="last">
		        <span class="start"></span>
			        <span class="breadText">Step 3: Shipping Details</span>
		        <span class="end"></span>
	        </div>
        </div>

        <!-- Form -->
        <div>
        
            <!-- Step One -->
            <div id="panel_StepOne" class="activeStep" runat="server">
            
                <asp:UpdatePanel ID="UpdatePanelOne" runat="server">
                    <ContentTemplate>
           
                        <div style="float:left;">                
                            <div class="pxSignUpSection">
                    
                                <!-- Legal Entity -->
                                <div class="pxFields" >
                                    <asp:Label ID="lbl_legalEntity" runat="server" Text="Legal Entity"></asp:Label>
                                    <asp:TextBox ID="txb_legalEntity" runat="server" Width="325px" AutoPostBack="True" OnTextChanged="ValidateLegalEntity" ></asp:TextBox>
                                    <asp:CustomValidator ID="custom_LegalEntity" ValidationGroup="StepOne" runat="server" Display="Dynamic" CssClass="pxErrorText" ErrorMessage="This name is not available."></asp:CustomValidator>
                                    <asp:RequiredFieldValidator ID="required_LegalEntity" ValidationGroup="StepOne" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_legalEntity" ErrorMessage="Legal Entity is required."></asp:RequiredFieldValidator>
                                </div>
                                <div class="clear"></div>
                
                                <!-- Company Name -->
                                <div class="pxSubFields legal" >
                                    <asp:CheckBox ID="chb_LegalEntity" runat="server" Text="The name of my company is different." AutoPostBack="True" OnCheckedChanged="ToggleLegalEntity" />
                                </div>
                                <div class="clear"></div>
                                <div class="pxFields" id="legalEntity" runat="server" Visible="False">
                                    <asp:Label ID="lbl_CompanyName" runat="server" Text="Company Name"></asp:Label>
                                    <asp:TextBox ID="txb_CompanyName" CssClass="input-medium" runat="server" Width="325px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="require_CompanyName" ValidationGroup="StepOne" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_CompanyName" ErrorMessage="Company Name is required." Enabled="False"></asp:RequiredFieldValidator>        
                                </div>
                                <div class="clear"></div>
                            </div>
                            <div class="pxSignUpSection">
                    
                                <!-- Username -->
                                <div class="pxFields" >
                                    <asp:Label ID="lbl_Username" runat="server" Text="Username"></asp:Label>
                                    <asp:TextBox ID="txb_Username" CssClass="input-medium" runat="server" AutoPostBack="True" OnTextChanged="ValidateUsername"></asp:TextBox>
                                    <asp:CustomValidator ID="custom_UserName" ValidationGroup="StepOne" runat="server" Display="Dynamic" CssClass="pxErrorText" ErrorMessage="This user name is not available."></asp:CustomValidator>
                                    <asp:RequiredFieldValidator ID="require_Username" ValidationGroup="StepOne" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_Username" ErrorMessage="Username is required."></asp:RequiredFieldValidator>        
                                </div>
                                <div class="clear"></div>
                
                                <!-- Password -->
                                <div class="pxFields" >
                                    <asp:Label ID="lbl_Password" runat="server" Text="Password"></asp:Label>
                                    <asp:TextBox ID="txb_Password" CssClass="input-medium warning" runat="server" TextMode="Password" AutoPostBack="True" OnTextChanged="ValidatePassword"></asp:TextBox>
                                    <asp:CustomValidator ID="custom_Password" ValidationGroup="StepOne" runat="server" Display="Dynamic" CssClass="pxErrorText" ErrorMessage="Password does not <br /> meet requirements."></asp:CustomValidator>
                                    <asp:RequiredFieldValidator ID="require_Password" ValidationGroup="StepOne" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_Password" ErrorMessage="Password is required."></asp:RequiredFieldValidator>        
                                </div>
                
                                <!-- Confirm Password -->
                                <div class="pxFields" >
                                    <asp:Label ID="lbl_ConfirmPassword" runat="server" Text="Confirm Password"></asp:Label>
                                    <asp:TextBox ID="txb_ConfirmPassword" CssClass="input-medium error" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="require_ConfirmPassword" ValidationGroup="StepOne" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_ConfirmPassword" ErrorMessage="Confirm password is required."></asp:RequiredFieldValidator>        
                                    <asp:CompareValidator ID="compare_ConfirmPassword" ValidationGroup="StepOne" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_ConfirmPassword" ControlToCompare="txb_Password" ErrorMessage="Passwords must match."></asp:CompareValidator>   
                                </div>
                                <div class="clear"></div>
                            </div>                
                            <div class="pxSignUpSection" style="border-bottom: none;">
                    
                                <!-- First Name -->
                                <div class="pxFields" >
                                    <asp:Label ID="lbl_FirstName" runat="server" Text="First Name"></asp:Label>
                                    <asp:TextBox ID="txb_FirstName" CssClass="input-medium" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="require_FirstName" ValidationGroup="StepOne" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_FirstName" ErrorMessage="First Name is required."></asp:RequiredFieldValidator>
                                </div>
                
                                <!-- Last Name -->
                                <div class="pxFields" >
                                    <asp:Label ID="Label2" runat="server" Text="Last Name"></asp:Label>
                                    <asp:TextBox ID="txb_LastName" CssClass="input-medium" runat="server" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="require_LastName" ValidationGroup="StepOne" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_LastName" ErrorMessage="Last Name is required."></asp:RequiredFieldValidator>
                                </div>
                                <div class="clear"></div>
                
                                <!-- Email Address -->
                                <div class="pxFields" >
                                    <asp:Label ID="lbl_EmailAddress" runat="server" Text="Email Address"></asp:Label>
                                    <asp:TextBox ID="txb_EmailAddress" CssClass="input-medium" runat="server" AutoPostBack="True" OnTextChanged="EmailAddressIsUnique"></asp:TextBox>
                                    <asp:CustomValidator ID="custom_EmailAdress" ValidationGroup="StepOne" runat="server" Display="Dynamic" CssClass="pxErrorText" ErrorMessage="This email address is not available."></asp:CustomValidator>
                                    <asp:RequiredFieldValidator ID="require_EmailAddress" ValidationGroup="StepOne" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_EmailAddress" ErrorMessage="Email Address is required."></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator 
                                        ID="regex_EmailAddress" 
                                        ValidationGroup="StepOne" 
                                        runat="server" 
                                        Display="Dynamic" 
                                        CssClass="pxErrorText" 
                                        ControlToValidate="txb_EmailAddress" 
                                        ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$" 
                                        ErrorMessage="Invalid email address.">                                        
                                    </asp:RegularExpressionValidator>
                                </div>
                
                                <!-- Confirm Email Address -->
                                <div class="pxFields" >
                                    <asp:Label ID="lbl_ConfirmEmail" runat="server" Text="Confirm Email Address"></asp:Label>
                                    <asp:TextBox ID="txb_ConfirmEmail" CssClass="input-medium" runat="server"></asp:TextBox>
                                    <asp:CompareValidator ID="compare_ConfirmEmail" ValidationGroup="StepOne" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_ConfirmEmail" ControlToCompare="txb_EmailAddress" ErrorMessage="Email Addresses must match."></asp:CompareValidator>
                                </div>
                                <div class="clear"></div>
                
                                <!-- Phone Number -->
                                <div class="pxFields" >
                                    <asp:Label ID="lbl_PhoneNumber" runat="server" Text="Phone Number"></asp:Label><span style="color: #999999;"> + Extension</span>
                                    <asp:TextBox ID="txb_PhoneNumber" CssClass="input-medium" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="require_PhoneNumber" ValidationGroup="StepOne" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_PhoneNumber" ErrorMessage="Phone Number is required."></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator 
                                        ID="regex_PhoneNumber" 
                                        ValidationGroup="StepOne" 
                                        runat="server" 
                                        Display="Dynamic" 
                                        CssClass="pxErrorText" 
                                        ControlToValidate="txb_PhoneNumber" 
                                        ValidationExpression="^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}( (|x|ext|ext.)\d{1,5})?$" 
                                        ErrorMessage="Invalid phone number.">                                        
                                    </asp:RegularExpressionValidator>
                                </div>
                                <div class="clear"></div>
                            </div>
                        </div>
                        <div style="float:left;">
                            <div style="margin-left: 120px;  margin-top: 50px;">
                                <img alt="" src="/_layouts/15/Px.Branding/Images/truck-palet.png" />
                            </div>
                        </div>

                        <div class="clear" style="border-bottom: 1px solid #C1C1C1"></div>                
                    
				    </ContentTemplate>
                </asp:UpdatePanel>
                
                <div style="float: left;">
                    <asp:Button ID="btn_ProceedToStepTwo" ValidationGroup="StepOne" runat="server" Text="" CssClass="btnStep2" OnClick="StepForward" CommandArgument="ProceedToStepTwo" CommandName="ProceedToStepTwo" BorderStyle="None" />            
                </div>
                <div style="float: right; margin-top: 20px;">Questions? Please contact your PCA.</div>
                <div class="clear"></div>
                
            </div>
        
            <!-- Step Two -->
            <div id="panel_StepTwo" class="inactiveStep" runat="server">
            
                <!-- Main Company Address -->
                <div class="pxSignUpSection2">
                    <span class="pxSectionHeading">Main Company Address</span>
                    <div class="clear"></div>

                    <!-- Address -->
                    <div class="pxFields" >
                        <asp:Label ID="lbl_Address" runat="server" Text="Address"></asp:Label>
                        <asp:TextBox ID="txb_Address" runat="server" Width="120px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="require_Address" ValidationGroup="StepTwo" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_Address" ErrorMessage="Address is required."></asp:RequiredFieldValidator>
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
                        <asp:RequiredFieldValidator ID="require_City" ValidationGroup="StepTwo" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_City" ErrorMessage=" is required."></asp:RequiredFieldValidator>
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
                        <asp:RequiredFieldValidator ID="require_Zip" ValidationGroup="StepTwo" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_Zip" ErrorMessage="Zip is required."></asp:RequiredFieldValidator>
				        <asp:RegularExpressionValidator 
					        ID="ValidZipCode01" 
					        ValidationGroup="StepTwo" 
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
                <div class="pxSignUpSection2">
                    <span class="pxSectionHeading">Product Returns Address</span>
                    <div class="clear"></div>                

                    <!-- Same Returns Address -->
                    <div class="pxFields" style="margin-top: 0; margin-left: -2px;" >
                        <asp:CheckBox ID="chb_SameReturnsAddress" runat="server" Text="My product returns address is the same as my main company address." Checked="True" AutoPostBack="True" OnCheckedChanged="ToggleReturnsAddress" />
                    </div>
                    <div class="clear"></div>                

                    <!-- Returns Address -->
                    <div class="pxFields" style="margin-top: 0;" >
                        <asp:Label ID="lbl_ReturnsAddress" runat="server" Text="Address" CssClass="disabledLabel"></asp:Label>
                        <asp:TextBox ID="txb_ReturnsAddress" runat="server" Width="120px" Enabled="False"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="require_ReturnsAddress" ValidationGroup="StepTwo" runat="server" Display="Dynamic" CssClass="pxErrorText absolute" ControlToValidate="txb_ReturnsAddress" ErrorMessage="Returns Address is required." Enabled="False"></asp:RequiredFieldValidator>
                    </div>
            
                    <!-- Returns Suite -->
                    <div class="pxFields" style="margin-top: 0;" >
                        <asp:Label ID="lbl_ReturnsSuite" runat="server" Text="Suite" CssClass="disabledLabel"></asp:Label>
                        <asp:TextBox ID="txb_ReturnsSuite" runat="server" Width="120px" Enabled="False"></asp:TextBox>
                    </div>
            
                    <!-- Returns City -->
                    <div class="pxFields" style="margin-top: 0;" >
                        <asp:Label ID="lbl_ReturnsCity" runat="server" Text="City" CssClass="disabledLabel"></asp:Label>
                        <asp:TextBox ID="txb_ReturnsCity" runat="server" Width="120px" Enabled="False"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="require_ReturnsCity" ValidationGroup="StepTwo" runat="server" Display="Dynamic" CssClass="pxErrorText absolute" ControlToValidate="txb_ReturnsCity" ErrorMessage="Returns City is required." Enabled="False"></asp:RequiredFieldValidator>
                    </div>
            
                    <!-- Returns State -->
                    <div class="pxFields" style="margin-top: 0;" >
                        <asp:Label ID="lbl_ReturnsState" runat="server" Text="State" CssClass="disabledLabel"></asp:Label>
                        <div class="clear"></div>
                        <asp:DropDownList ID="ddl_ReturnsState" runat="server" CssClass="pxDropDown" Width="80px" Enabled="False"></asp:DropDownList>
                    </div>
            
                    <!-- Returns Zip -->
                    <div class="pxFields" style="margin-top: 0;" >
                        <asp:Label ID="lbl_ReturnsZip" runat="server" Text="Zip" CssClass="disabledLabel"></asp:Label>
                        <asp:TextBox ID="txb_ReturnsZip" runat="server" Width="50px" Enabled="False"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="require_ReturnsZip" ValidationGroup="StepTwo" runat="server" Display="Dynamic" CssClass="pxErrorText absolute" ControlToValidate="txb_ReturnsZip" ErrorMessage="Returns Zip is required." Enabled="False"></asp:RequiredFieldValidator>
				        <asp:RegularExpressionValidator 
					        ID="ValidZipCode02" 
					        ValidationGroup="StepTwo" 
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
            
                <div class="pxSignUpSection2">
                    <span class="pxSectionHeading">Contact Information</span>
                    <div class="clear"></div>                
                
                    <table class="pxContactInformation">
                        <tr>
                            <th>Contact Type</th>
                            <th>First Name</th>
                            <th>Last Name</th>
                            <th>Phone</th>
                            <th>Email Address</th>
                            <th>Same?</th>
                        </tr>
                        <tr>
                            <td>Primary Business</td>
                            <td>
                                <asp:TextBox ID="txb_ContactPrimaryBusinessFirstName" runat="server" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="require_ContactFirstName01" ControlToValidate="txb_ContactPrimaryBusinessFirstName" ErrorMessage="First name is required." ValidationGroup="StepTwo" runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
                            </td>                        
                            <td>
                                <asp:TextBox ID="txb_ContactPrimaryBusinessLastName" runat="server" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="require_ContactLastName01" ControlToValidate="txb_ContactPrimaryBusinessLastName" ErrorMessage="Last name is required." ValidationGroup="StepTwo" runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
                            </td>                        
                            <td>
                                <asp:TextBox ID="txb_ContactPrimaryBusinessPhone" runat="server" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="require_ContactPhone01" ControlToValidate="txb_ContactPrimaryBusinessPhone" ErrorMessage="Phone is required." ValidationGroup="StepTwo" runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator 
                                    ID="ValidPhone01" 
                                    ValidationGroup="StepTwo" 
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
                                <asp:RequiredFieldValidator ID="require_ContactEmail01" ControlToValidate="txb_ContactPrimaryBusinessEmail" ErrorMessage="Email is required." ValidationGroup="StepTwo" runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
				                <asp:RegularExpressionValidator 
					                ID="ValidEmail01" 
                                    ValidationGroup="StepTwo" 
					                runat="server" 
					                Display="Dynamic" 
					                CssClass="pxErrorText" 
					                ControlToValidate="txb_ContactPrimaryBusinessEmail" 
					                ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$" 
					                ErrorMessage="Invalid email address.">                                        
				                </asp:RegularExpressionValidator>
                            </td>                        
                            <td></td>
                        </tr>
                        <tr>
                            <td>Customer Service</td>
                            <td>
                                <asp:TextBox ID="txb_ContactCustomerServiceFirstName" runat="server" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="require_ContactFirstName02" ControlToValidate="txb_ContactCustomerServiceFirstName" ErrorMessage="First name is required." ValidationGroup="StepTwo" runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
                            </td>                        
                            <td>
                                <asp:TextBox ID="txb_ContactCustomerServiceLastName" runat="server" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="require_ContactLastName02" ControlToValidate="txb_ContactCustomerServiceLastName" ErrorMessage="Last name is required." ValidationGroup="StepTwo" runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
                            </td>                        
                            <td>
                                <asp:TextBox ID="txb_ContactCustomerServicePhone" runat="server" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="require_ContactPhone02" ControlToValidate="txb_ContactCustomerServicePhone" ErrorMessage="Phone is required." ValidationGroup="StepTwo" runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator 
                                    ID="ValidPhone02" 
                                    ValidationGroup="StepTwo" 
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
                                <asp:RequiredFieldValidator ID="require_ContactEmail02" ControlToValidate="txb_ContactCustomerServiceEmail" ErrorMessage="Email is required." ValidationGroup="StepTwo" runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
								<asp:RegularExpressionValidator 
									ID="ValidEmail02" 
									ValidationGroup="StepTwo" 
									runat="server" 
									Display="Dynamic" 
									CssClass="pxErrorText" 
									ControlToValidate="txb_ContactCustomerServiceEmail" 
									ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$" 
									ErrorMessage="Invalid email address.">                                        
								</asp:RegularExpressionValidator>
                            </td>                        
                            <td style="text-align: center"><asp:CheckBox ID="chb_ContactCustomerServiceSame" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Accounts Receivable</td>
                            <td>
                                <asp:TextBox ID="txb_ContactAccountsReceivableFirstName" runat="server" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="require_ContactFirstName03" ControlToValidate="txb_ContactAccountsReceivableFirstName" ErrorMessage="First name is required." ValidationGroup="StepTwo" runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
                            </td>                        
                            <td>
                                <asp:TextBox ID="txb_ContactAccountsReceivableLastName" runat="server" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="require_ContactLastName03" ControlToValidate="txb_ContactAccountsReceivableLastName" ErrorMessage="Last name is required." ValidationGroup="StepTwo" runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
                            </td>                        
                            <td>
                                <asp:TextBox ID="txb_ContactAccountsReceivablePhone" runat="server" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="require_ContactPhone03" ControlToValidate="txb_ContactAccountsReceivablePhone" ErrorMessage="Phone is required." ValidationGroup="StepTwo" runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator 
                                    ID="ValidPhone03" 
                                    ValidationGroup="StepTwo" 
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
                                <asp:RequiredFieldValidator ID="require_ContactEmail03" ControlToValidate="txb_ContactAccountsReceivableEmail" ErrorMessage="Email is required." ValidationGroup="StepTwo" runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
								<asp:RegularExpressionValidator 
									ID="ValidEmail03" 
									ValidationGroup="StepTwo" 
									runat="server" 
									Display="Dynamic" 
									CssClass="pxErrorText" 
									ControlToValidate="txb_ContactAccountsReceivableEmail" 
									ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$" 
									ErrorMessage="Invalid email address.">                                        
								</asp:RegularExpressionValidator>
                            </td>                        
                            <td style="text-align: center"><asp:CheckBox ID="chb_ContactAccountsReceivableSame" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Inventory</td>
                            <td>
                                <asp:TextBox ID="txb_ContactInventoryFirstName" runat="server" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="require_ContactFirstName04" ControlToValidate="txb_ContactInventoryFirstName" ErrorMessage="First name is required." ValidationGroup="StepTwo" runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
                            </td>                        
                            <td>
                                <asp:TextBox ID="txb_ContactInventoryLastName" runat="server" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="require_ContactLastName04" ControlToValidate="txb_ContactInventoryLastName" ErrorMessage="Last name is required." ValidationGroup="StepTwo" runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
                            </td>                        
                            <td>
                                <asp:TextBox ID="txb_ContactInventoryPhone" runat="server" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="require_ContactPhone04" ControlToValidate="txb_ContactInventoryPhone" ErrorMessage="Phone is required." ValidationGroup="StepTwo" runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator 
                                    ID="ValidPhone04" 
                                    ValidationGroup="StepTwo" 
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
                                <asp:RequiredFieldValidator ID="require_ContactEmail04" ControlToValidate="txb_ContactInventoryEmail" ErrorMessage="Email is required." ValidationGroup="StepTwo" runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
								<asp:RegularExpressionValidator 
									ID="ValidEmail04" 
									ValidationGroup="StepTwo" 
									runat="server" 
									Display="Dynamic" 
									CssClass="pxErrorText" 
									ControlToValidate="txb_ContactInventoryEmail" 
									ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$" 
									ErrorMessage="Invalid email address.">                                        
								</asp:RegularExpressionValidator>
                            </td>                        
                            <td style="text-align: center"><asp:CheckBox ID="chb_ContactInventorySame" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Returns</td>
                            <td>
                                <asp:TextBox ID="txb_ContactReturnsFirstName" runat="server" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="require_ContactFirstName05" ControlToValidate="txb_ContactReturnsFirstName" ErrorMessage="First name is required." ValidationGroup="StepTwo" runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
                            </td>                        
                            <td>
                                <asp:TextBox ID="txb_ContactReturnsLastName" runat="server" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="require_ContactLastName05" ControlToValidate="txb_ContactReturnsLastName" ErrorMessage="Last name is required." ValidationGroup="StepTwo" runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
                            </td>                        
                            <td>
                                <asp:TextBox ID="txb_ContactReturnsPhone" runat="server" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="require_ContactPhone05" ControlToValidate="txb_ContactReturnsPhone" ErrorMessage="Phone is required." ValidationGroup="StepTwo" runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator 
                                    ID="ValidPhone05" 
                                    ValidationGroup="StepTwo" 
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
                                <asp:RequiredFieldValidator ID="require_ContactEmail05" ControlToValidate="txb_ContactReturnsEmail" ErrorMessage="Email is required." ValidationGroup="StepTwo" runat="server" Display="Dynamic" CssClass="pxErrorText"></asp:RequiredFieldValidator>
								<asp:RegularExpressionValidator 
									ID="ValidEmail05" 
									ValidationGroup="StepTwo" 
									runat="server" 
									Display="Dynamic" 
									CssClass="pxErrorText" 
									ControlToValidate="txb_ContactReturnsEmail" 
									ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$" 
									ErrorMessage="Invalid email address.">                                        
								</asp:RegularExpressionValidator>
                            </td>                        
                            <td style="text-align: center"><asp:CheckBox ID="chb_ContactReturnsSame" runat="server" /></td>
                        </tr>
                    </table>

                </div>
                                    
                <div style="float: left;">                
                    <asp:Button ID="btn_ReturnToStepOne" runat="server" Text="" CssClass="btnStepBack" BorderStyle="None" OnClick="StepBackward" CommandArgument="ReturnToStepOne" CommandName="ReturnToStepOne" />
                    <asp:Button ID="btn_ProceedToStepThree" ValidationGroup="StepTwo" runat="server" Text="" CssClass="btnStep3" BorderStyle="None" OnClick="StepForward" CommandArgument="ProceedToStepThree" CommandName="ProceedToStepThree" />
                </div>
                <div style="float: right; margin-top: 20px;">Questions? Please contact your PCA.</div>
                <div class="clear"></div>
                 
            </div>
        
            <!-- Step Three -->
            <div id="panel_StepThree" class="inactiveStep" runat="server">
            
                <asp:UpdatePanel ID="UpdatePanelThree" runat="server">
                    <ContentTemplate>
                                
                        <!-- Shipment Third-Party Billing -->
                        <div class="pxSignUpSection" style="margin-top: 16px;">
                            <span class="pxSectionHeading">Shipment Third-Party Billing (3PB)</span>
                            <div class="clear8"></div>                
                            <span>By joining our third-party billing program, you can take advantage of our group shipping rates. By being a part of this program, your shipping costs will be combined with your final invoice.</span>
                            <div class="clear"></div> 
                
                            <!-- 3PB -->
                            <div class="pxFields" style="width: 312px;">
                                Would you like to participate in the Supplier Oasis Shipment Third-party Billing Program?
                            </div>
                            <div class="clear"></div>

                            <div class="pxFields">
                                <asp:RadioButtonList ID="rbl_ThirdPartyOption" runat="server" TextAlign="Right" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="ThirdPartyOption" BackColor="Transparent">
                                    <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>                            
                            <div class="pxFields" style="padding-top: 8px;">                            
                                <a href="javascript:LearnMore()">Info / Learn More</a>
                            </div>
                            <div class="clear"></div>   
                            <asp:Literal ID="ltr_RequireThirdPartyOption" runat="server" Visible="False"></asp:Literal>

                
                            <!-- 3PB Yes -->
                            <div id="panel_3pbYes" runat="server" Visible="False">

                                <!-- UPS Shipper Account # -->
                                <div class="pxFields" >
                                    <asp:Label ID="lbl_UpsAccountNumber" runat="server" Text="UPS Shipper Account #"></asp:Label>
                                    <asp:TextBox ID="txb_UpsAccountNumber" runat="server" Width="150px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="require_UpsAccountNumber" ValidationGroup="StepThree" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_UpsAccountNumber" ErrorMessage="UPS Shipper Account # is required."></asp:RequiredFieldValidator>
                                </div>
                                <div class="clear"></div>            
                            </div>

                        </div>
    
                        <!-- 3PB No -->                                            
                        <div id="panel_3pbNo" runat="server" Visible="False">
                
                            <!-- Small Parcel Shipments -->            
                            <div class="pxSignUpSection">                
                                <span class="pxSectionHeading">Small Parcel Shipments</span>
                                <div class="clear"></div>                                
                    
                                <!-- Default Small Parcel Carrier -->
                                <div class="pxFields" >
                                    <asp:Label ID="lbl_SmallParcelCarrier" runat="server" Text="Default Small Parcel Carrier"></asp:Label>
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
                                    <asp:RadioButtonList ID="rbl_SmallParcelBillingType" runat="server" TextAlign="Right" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="ToggleThirdPartyBilling" BackColor="Transparent">
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
                                    <asp:Label ID="lbl_SmallParcel3pbShipperAcctNumber" CssClass="disabledLabel" runat="server" Text="3rd Party Shipping Acct. #"></asp:Label>
                                    <asp:TextBox ID="txb_SmallParcel3pbShipperAcctNumber" runat="server" Width="120px" Enabled="False"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="require_3pbShipperAcctNumber" ValidationGroup="StepThree" Enabled="False" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_SmallParcel3pbShipperAcctNumber" ErrorMessage="Acct. # is required."></asp:RequiredFieldValidator>
                                </div>            
                                <div class="clear"></div>
                    
                                <!-- Bill-to Name -->
                                <div class="pxFields" >
                                    <asp:Label ID="lbl_SmallParcelBillToName" runat="server" Text="Company Name"></asp:Label>
                                    <asp:TextBox ID="txb_SmallParcelBillToName" runat="server" Width="265px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="require_SmallParcelBillToName" ValidationGroup="StepThree" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_SmallParcelBillToName" ErrorMessage="Name is required."></asp:RequiredFieldValidator>
                                </div>            

                                <!-- Phone Number -->
                                <div class="pxFields" >
                                    <asp:Label ID="lbl_SmallParcelPhone" runat="server" Text="Phone Number"></asp:Label>
                                    <asp:TextBox ID="txb_SmallParcelPhone" runat="server" Width="120px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="require_SmallParcelPhone" ValidationGroup="StepThree" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_SmallParcelPhone" ErrorMessage="Phone Number is required."></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator 
                                        ID="ValidPhone06" 
                                        ValidationGroup="StepThree" 
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
                                    <asp:RequiredFieldValidator ID="require_SmallParcelBillToAddress" ValidationGroup="StepThree" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_SmallParcelBillToAddress" ErrorMessage="Address is required."></asp:RequiredFieldValidator>
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
                                    <asp:RequiredFieldValidator ID="require_SmallParcelBillToCity" ValidationGroup="StepThree" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_SmallParcelBillToCity" ErrorMessage="City is required."></asp:RequiredFieldValidator>
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
                                    <asp:RequiredFieldValidator ID="require_SmallParcelBillToZip" ValidationGroup="StepThree" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_SmallParcelBillToZip" ErrorMessage="Zip is required."></asp:RequiredFieldValidator>
				                    <asp:RegularExpressionValidator 
					                    ID="ValidZipCode03" 
					                    ValidationGroup="StepThree" 
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
                
                            <!-- LTL Shipments -->            
                            <div class="pxSignUpSection" runat="server" Visible="False">                
                                <span class="pxSectionHeading">LTL Shipments</span>
                                <div class="clear"></div>                                

                                <!-- Participate in LTL? -->
                                <div class="pxFields">
                                    Do you participate in LTL Shipping?
                                    <asp:RadioButtonList ID="rbl_ParticipateLtl" runat="server" TextAlign="Right" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="Yes" Text="Yes" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="clear"></div>    
                    
                                <!-- Default LTL Carrier -->
                                <div class="pxFields" >
                                    <asp:Label ID="lbl_LtlCarrier" runat="server" Text="Default LTL Carrier"></asp:Label>
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
                                    <asp:RequiredFieldValidator ID="required_LtlShipperAcctNumber" ValidationGroup="StepThree" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_LtlShipperAcctNumber" ErrorMessage="Shipper Acct. Number is required." Enabled="False"></asp:RequiredFieldValidator>
                                </div>            
                                <div class="clear"></div>
                    
                                <!-- Bill-to Name -->
                                <div class="pxFields" >
                                    <asp:Label ID="lbl_LtlBillToName" runat="server" Text="Company Name"></asp:Label>
                                    <asp:TextBox ID="txb_LtlBillToName" runat="server" Width="265px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="required_LtlBillToName" ValidationGroup="StepThree" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_LtlBillToName" ErrorMessage="Name is required." Enabled="False"></asp:RequiredFieldValidator>
                                </div>            

                                <!-- Phone Number -->
                                <div class="pxFields" >
                                    <asp:Label ID="lbl_LtlPhone" runat="server" Text="Phone Number"></asp:Label>
                                    <asp:TextBox ID="txb_LtlPhone" runat="server" Width="120px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="required_LtlPhone" ValidationGroup="StepThree" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_LtlPhone" ErrorMessage="Phone is required." Enabled="False"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator 
                                        ID="ValidPhone07" 
                                        Enabled="False"
                                        ValidationGroup="StepThree" 
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
                                    <asp:RequiredFieldValidator ID="required_LtlBillToAddress" ValidationGroup="StepThree" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_LtlBillToAddress" ErrorMessage="Address is required." Enabled="False"></asp:RequiredFieldValidator>
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
                                    <asp:RequiredFieldValidator ID="required_LtlBillToCity" ValidationGroup="StepThree" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_LtlBillToCity" ErrorMessage="City is required." Enabled="False"></asp:RequiredFieldValidator>
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
                                    <asp:RequiredFieldValidator ID="required_LtlBillToZip" ValidationGroup="StepThree" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_LtlBillToZip" ErrorMessage="Zip is required." Enabled="False"></asp:RequiredFieldValidator>
				                    <asp:RegularExpressionValidator 
					                    ID="ValidZipCode04" 
					                    ValidationGroup="StepThree" 
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
                        </div>            
                        <div class="clear"></div>
                    
				    </ContentTemplate>
                </asp:UpdatePanel>
                
                <div style="float: left;">
                    <asp:Button ID="btn_ReturnToStepTwo" runat="server" Text="" CssClass="btnStepBack" BorderStyle="None" OnClick="StepBackward" CommandArgument="ReturnToStepTwo" CommandName="ReturnToStepTwo" />
                    <asp:Button ID="btn_CreateAccount" ValidationGroup="StepThree" runat="server" Text="" CssClass="btnStepFinal" BorderStyle="None" OnClick="btn_CreateAccount_Click" />
                </div>
                <div style="float: right; margin-top: 20px;">Questions? Please contact your PCA.</div>
                <div class="clear"></div>           

            </div>
                        
        </div>
        
    </div>