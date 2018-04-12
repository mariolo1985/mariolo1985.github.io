<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecoverPassword.ascx.cs" Inherits="Px.SelfService.WebParts.RecoverPassword.RecoverPassword" %>

    <style type="text/css">
        #s4-workspace { border-radius: 3px; width:405px !important; height: auto !important; position:absolute; left:50%; top:50%; margin-left: -225px !important; margin-top: -100px !important; }
        #s4-bodyContainer { padding-bottom: 0 !important; }
        #AnonymousHeader { background: #FFFFFF; margin: -15px 0 0 0; padding: 7px; }
        a, input[type='text'], input[type='password'] { display:block; padding-right: 10px; margin-top: 2px; }
        label { display:inline }
        .terms-input { padding: 4px 6px; border-radius: 4px; height: 20px; color: rgb(85, 85, 85); line-height: 20px; font-size: 14px; vertical-align: middle; display: inline-block; -webkit-border-radius: 4px; -moz-border-radius: 4px; }
        .terms-input { border: 1px solid rgb(204, 204, 204); transition:border 0.2s linear, box-shadow 0.2s linear; box-shadow: inset 0px 1px 1px rgba(0,0,0,0.075); background-color: rgb(255, 255, 255); -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); -moz-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); -webkit-transition: border linear .2s, box-shadow linear .2s; -moz-transition: border linear .2s, box-shadow linear .2s; -o-transition: border linear .2s, box-shadow linear .2s; }
        .terms-input { overflow: hidden; overflow-y: scroll; width: 810px; height: 164px; }
        .terms-input:focus { border-color: rgba(82, 168, 236, 0.8); outline: dotted thin; box-shadow: inset 0px 1px 1px rgba(0,0,0,0.075), 0px 0px 8px rgba(82,168,236,0.6); -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075), 0 0 8px rgba(82, 168, 236, 0.6); -moz-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075), 0 0 8px rgba(82, 168, 236, 0.6); }
        .alerts { background: #EF2710; color: white; height: 30px; line-height: 30px; text-align: center; }                
        .pxContent { padding: 10px 10px 0 10px; }
        input[type='password'] { padding-right: 3px; }
        div.article, div.welcome { padding-bottom: 0px !important; }
        .ms-table .ms-fullWidth { display:none; }
    </style>

    <div id="AnonymousHeader">
        <div><img class="logo" src="/_layouts/15/Px.Branding/Images/Logo-195x50.png" alt=""></div>
        <div style="clear:both"></div>
    </div>

    <div>
        <asp:Panel ID="pnl_Notifications" runat="server" Visible="False" CssClass="alerts">
            <asp:Literal ID="ltr_Notification" runat="server"></asp:Literal>
        </asp:Panel>
    </div>

    <div class="pxContent">
    
        <div><asp:Label ID="lbl_Instruction" runat="server" Text="Please create your new password:"></asp:Label></div>
    
        <div style="margin-top: 5px; float: left">
            <div class="pxFields" >
                <div><asp:Label ID="lbl_Prompt" runat="server" Text="New Password"></asp:Label></div>
                <div><asp:TextBox ID="txb_Prompt" runat="server" TextMode="Password" Width="147"></asp:TextBox></div>
            </div>
            <div class="clear"></div>
            <div class="pxFields" >
                <div><asp:Label ID="lbl_Confirm" runat="server" Text="Confirm New Password"></asp:Label></div>
                <div><asp:TextBox ID="txb_Confirm" runat="server" TextMode="Password" Width="147"></asp:TextBox></div>
            </div>
            <div class="clear" style="height: 10px;"></div>
        </div>
    
        <div style="color:#454545; font:12px 'Open Sans'; line-height:1; width: 215px; margin-left: 3px; margin-top: 30px; float: left">
            NOTE: Password must be at least eight characters long and contain at least one alpha character [a-z], 
            one numeric character [0-9], and one special character [` ! @ $ % ^ & * ( ) - _ = + [ ] ; : ‘ “ , < . > / ?]
        </div>
        <div class="clear"></div>

        <div>
            <asp:Button ID="btn_Change" runat="server" Text="Change Password" OnClick="btn_Change_Click" Enabled="True" CssClass="pxBtn" Height="28px"/>
            <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" OnClick="btn_Cancel_Click" CssClass="pxBtnCancel" Height="28px"/>
        </div>
        
        <div>
            <asp:RequiredFieldValidator 
                ID="RequiredFieldValidator" 
                runat="server" 
                Display="Dynamic"                
                CssClass="text-error"
                ControlToValidate="txb_Prompt"                
                ErrorMessage="Password is required.">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator 
                ID="regex_NewPassword" 
                ValidationGroup="ChangePassword" 
                runat="server" 
                Display="Dynamic" 
                CssClass="pxErrorText" 
                ControlToValidate="txb_Prompt" 
                ValidationExpression="((?=.*\d)(?=.*[A-z])(?=.*[&#33;&#34;&#35;&#36;&#37;&#38;&#39;&#40;&#41;&#42;&#43;&#44;&#45;&#46;&#47;&#58;&#59;&#60;&#61;&#62;&#63;&#64;&#91;&#92;&#93;&#94;&#95;&#96;&#123;&#124;&#125;&#126;]).{8,32})" 
                ErrorMessage="Password does not <br /> meet requirements.">                            
            </asp:RegularExpressionValidator>
            <asp:CompareValidator 
                ID="CompareValidator" 
                runat="server" 
                Display="Dynamic"                
                CssClass="text-error"
                ControlToValidate="txb_Prompt" 
                ControlToCompare="txb_Confirm" 
                ErrorMessage="Passwords do not match.">                
            </asp:CompareValidator>
        </div>
    
    </div>
