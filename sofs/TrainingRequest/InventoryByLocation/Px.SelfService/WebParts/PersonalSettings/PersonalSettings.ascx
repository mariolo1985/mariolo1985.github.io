<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PersonalSettings.ascx.cs" Inherits="Px.SelfService.WebParts.PersonalSettings.PersonalSettings" %>

<script type="text/javascript">
    
    $(document).ready(function () {
        new User();
    });
    
    function Dialog(text) {
        var html = "<div class='modal fade'><div class='modal-dialog'><div class='modal-content'><div class='modal-body'><p>" + text + "</p></div><div class='modal-footer'><button type='button' class='btn btn-default' data-dismiss='modal'>Close</button></div></div></div></div>";
        $(html).modal();
    }

    function OnUpdateValidators() {
        for (var i = 0; i < Page_Validators.length; i++) {
            var validator = Page_Validators[i];
            var control = document.getElementById(validator.controltovalidate);
            if (control != null && control.style != null) {
                control.style.border = CheckValidatorsForControl(control) ? "1px solid #ABABAB" : "2px solid #F3A94C";
                //if (!val.isvalid) {
                //ctrl.style.border = "2px solid #F3A94C";
                //} else {
                //ctrl.style.border = "1px solid #ABABAB";
                //}
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
    div.welcome { padding-top: 0 !important; }
    .welcome-content {margin-top:0 !important;}    

    #PxBg { background-color: #E9E9E9 !important; }

    .activeTab {  }
    .inactiveTab { background-color: #E9E9E9; margin-left: 20px; padding: 5px; float: left; border-left: 1px solid #C1C1C1; border-top: 1px solid #C1C1C1; border-right: 1px solid #C1C1C1 }
    .inactiveTab a { color: #454545; }
    .inactiveTab a:hover { color: #454545; }
    .inactiveTab a:visited { color: #454545; }
    .tabs ul {list-style: none; margin: 0px; padding: 0px; padding-left: 15px; background-color: #E9E9E9; }
    .tabs li {border-width: 1px 1px 0px; border-style: solid; border-color: #CCCCCC; margin: 0px;  margin-right: 7px; border-top-left-radius: 3px; border-top-right-radius: 3px; float: left;}
    .tabs a {background: #F3F3F3; height: 25px;  padding: 5px 8px 0 8px; color: #454545; font: 12px Open Sans bold; text-decoration: none; display: block; margin-bottom: -1px}
    .tabs a:hover { }

    .tabs .activeTab {background-color: white; height: 26px; margin-bottom: -2px; position: relative;}
    .tabs .activeTab a { }
    #content {padding: 0px 1em; border-top: 1px solid #cccccc; clear: both;}

    .pxWrapper { border-top: 1px solid #cccccc; border-bottom: 1px solid #cccccc; background-color: white; padding-top: 24px; min-height: 269px; }
    .pxDisplaySetting { background-color: #FFFFFF; padding-bottom:8px; padding-left: 16px; }
    .pxDisplaySetting a, .pxDisplaySetting a:visited { color: #2B5272; text-decoration: underline; }
    .pxChangeSetting { background-color: #F3F3F3; padding: 13px 0 16px 16px; border-top: 1px solid #cccccc; border-bottom: 1px solid #cccccc; }
    .pxHeading { color: #454545; font-family:Open Sans Bold; font-size: 11px; text-transform: uppercase; }
    .pxLabel { font-weight: bold; }
    .pxButtons { margin-top: 10px; }
    .pxErrorText { color:#F3A94C }    
    .pxErrorTextBox {border-width: 2px; border-color: #F3A94C }    
    .clear { clear: both; }
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
            <li><a href="/pages/CompanySettings.aspx" >Company Settings</a></li>
            <li><a href="#" class="activeTab">Personal Settings</a></li>
            <li><a href="/pages/WarehouseSettings.aspx">Warehouse Settings</a></li>
        </ul>
    </div>
        <div class="clear"></div>

    <!-- Form -->
    <div>
        
        <div style="display: none;">
            <asp:RequiredFieldValidator ID="dummyValidator" runat="server" CssClass="hidden" ControlToValidate="dummyTextBox" ValidationGroup="dummy"></asp:RequiredFieldValidator>
            <asp:TextBox runat="server" ID="dummyTextBox" CssClass="hidden"></asp:TextBox>            
        </div>       
        
        <asp:UpdatePanel ID="UpdatePanelOne" runat="server">
            <ContentTemplate>
                <div class="pxWrapper">
                    <asp:Literal ID="ltr_Debug" runat="server"></asp:Literal>

                    <!-- Display Name -->
                    <div id="section_DisplayName" class="pxDisplaySetting" runat="server" Visible="True">
                        <asp:Label ID="lbl_DisplayName" CssClass="pxLabel" runat="server" Text="Name"></asp:Label>
                        <div class="clear"></div>
                        <asp:Literal ID="ltr_DisplayName" runat="server"></asp:Literal>
                        <asp:LinkButton ID="lnk_ChangeName" runat="server" OnClick="ShowChangeName" CausesValidation="False">Change Name</asp:LinkButton>
                    </div>

                    <!-- Change Name -->
                    <div id="section_ChangeName" class="pxChangeSetting" runat="server" Visible="False">
                        <asp:Label ID="lbl_ChangeName" CssClass="pxHeading" runat="server" Text="Change Name"></asp:Label>
                        <div class="clear"></div>
                        <div class="pxFields">
                            <asp:Label ID="lbl_FirstName" runat="server" Text="First Name"></asp:Label>
                            <asp:TextBox ID="txb_FirstName" runat="server" Width="125px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="require_FirstName" ValidationGroup="ChangeName" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_FirstName" ErrorMessage="First Name is required." Enabled="False"></asp:RequiredFieldValidator>        
                        </div>
                        <div class="pxFields">
                            <asp:Label ID="lbl_LastName" runat="server" Text="Last Name"></asp:Label>
                            <asp:TextBox ID="txb_LastName" runat="server" Width="125px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="require_LastName" ValidationGroup="ChangeName" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_LastName" ErrorMessage="Last Name is required." Enabled="False"></asp:RequiredFieldValidator>        
                        </div>
                        <div class="clear"></div>
                        <div class="pxButtons">
                            <asp:Button ID="btn_ChangeName" ValidationGroup="ChangeName" CssClass="pxBtn" runat="server" Text="Change Name" OnClick="ChangeName_Click" />
                            <asp:Button ID="btn_CancelChangeName" CssClass="pxBtnCancel" runat="server" Text="Cancel" OnClick="HideChangeName" CausesValidation="False" />
                        </div>
                    </div>

                    <!-- Display Password -->
                    <div id="section_DisplayPassword" class="pxDisplaySetting" runat="server" Visible="True">
                        <asp:Label ID="lbl_Password" CssClass="pxLabel" runat="server" Text="Password"></asp:Label>
                        <asp:LinkButton ID="lnk_ChangePassword" runat="server" OnClick="ShowChangePassword" CausesValidation="False">Change Password</asp:LinkButton>
                    </div>

                    <!-- Change Password -->
                    <div id="section_ChangePassword" class="pxChangeSetting" runat="server" Visible="False">
                        <asp:Label ID="lbl_ChangePassword" CssClass="pxHeading" runat="server" Text="Change Password"></asp:Label>
                        <div class="clear"></div>
                        <div class="pxFields">
                            <asp:Label ID="lbl_CurrentPassword" runat="server" Text="Current Password"></asp:Label>
                            <asp:TextBox ID="txb_CurrentPassword" runat="server" TextMode="Password" Width="125px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="require_CurrentPassword" ValidationGroup="ChangePassword" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_CurrentPassword" ErrorMessage="Password is required." Enabled="False"></asp:RequiredFieldValidator>        
                        </div>
                        <div class="clear"></div>
                        <div class="pxFields">
                            <asp:Label ID="lbl_NewPassword" runat="server" Text="New Password"></asp:Label>
                            <asp:TextBox ID="txb_NewPassword" runat="server" TextMode="Password" Width="125px" AutoPostBack="True" OnTextChanged="ValidatePassword"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="require_NewPassword" ValidationGroup="ChangePassword" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_NewPassword" ErrorMessage="Password is required." Enabled="False"></asp:RequiredFieldValidator>        
                            <asp:CustomValidator ID="custom_Password" ValidationGroup="ChangePassword" runat="server" Display="Dynamic" CssClass="pxErrorText" ErrorMessage="Password does not <br /> meet requirements."></asp:CustomValidator>
                        </div>
                        <div class="pxFields">
                            <asp:Label ID="lbl_ConfirmNewPassword" runat="server" Text="Confirm New Password"></asp:Label>
                            <asp:TextBox ID="txb_ConfirmNewPassword" runat="server" TextMode="Password" Width="125px"></asp:TextBox>                    
                            <asp:CompareValidator ID="compare_ConfirmPassword" ValidationGroup="ChangePassword" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_ConfirmNewPassword" ControlToCompare="txb_NewPassword" ErrorMessage="Passwords must match."></asp:CompareValidator>   
                        </div>
                        <div class="clear"></div>
                        <div class="pxButtons">
                            <asp:Button ID="btn_ChangePassword" ValidationGroup="ChangePassword" CssClass="pxBtn" runat="server" Text="Change Password" OnClick="ChangePassword_Click" />
                            <asp:Button ID="btn_CancelChangePassword" CssClass="pxBtnCancel" runat="server" Text="Cancel" OnClick="HideChangePassword" CausesValidation="False" />
                        </div>
                    </div>

                    <!-- Display Phone Number -->
                    <div id="section_DisplayPhoneNumber" class="pxDisplaySetting" runat="server" Visible="True">
                        <asp:Label ID="lbl_DisplayPhoneNumber" CssClass="pxLabel" runat="server" Text="Phone Number"></asp:Label>
                        <div class="clear"></div>
                        <asp:Literal ID="ltr_DisplayPhoneNumber" runat="server"></asp:Literal>
                        <asp:LinkButton ID="lnk_ChangePhoneNumber" runat="server" OnClick="ShowChangePhoneNumber" CausesValidation="False">Change Phone Number</asp:LinkButton>
                    </div>
                
                    <!-- Change Phone Number -->
                    <div id="section_ChangePhoneNumber" class="pxChangeSetting" runat="server" Visible="False">
                        <asp:Label ID="lbl_ChangePhoneNumber" CssClass="pxHeading" runat="server" Text="Change Phone Number"></asp:Label>
                        <div class="clear"></div>
                        <div class="pxFields">
                            <asp:Label ID="lbl_NewPhoneNumber" runat="server" Text="New Phone Number"></asp:Label>
                            <asp:TextBox ID="txb_NewPhoneNumber" runat="server" Width="125px"></asp:TextBox>                    
                            <asp:RequiredFieldValidator ID="require_PhoneNumber" ValidationGroup="ChangePhoneNumber" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_NewPhoneNumber" ErrorMessage="Phone Number is required." Enabled="False"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="regex_PhoneNumber" ValidationGroup="ChangePhoneNumber" runat="server" Display="Dynamic" CssClass="pxErrorText" ControlToValidate="txb_NewPhoneNumber" ValidationExpression="^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}( (|x|ext|ext.)\d{1,5})?$" ErrorMessage="Invalid phone number."></asp:RegularExpressionValidator>
                        </div>
                        <div class="clear"></div>
                        <div class="pxButtons">
                            <asp:Button ID="btn_ChangePhoneNumber" ValidationGroup="ChangePhoneNumber" CssClass="pxBtn" runat="server" Text="Change Phone Number" OnClick="ChangePhoneNumber_Click" />
                            <asp:Button ID="btn_CancelChangePhoneNumber" CssClass="pxBtnCancel" runat="server" Text="Cancel" OnClick="HideChangePhoneNumber" CausesValidation="False" />
                        </div>
                    </div>

                </div>
                                
	        </ContentTemplate>
        </asp:UpdatePanel>
                
    </div>
