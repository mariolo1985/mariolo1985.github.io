<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccountRecovery.ascx.cs" Inherits="Px.SelfService.WebParts.AccountRecovery.AccountRecovery" %>

<script type="text/javascript">
    
    function Dialog(text) {
        var html = "<div class='modal fade'><div class='modal-dialog'><div class='modal-content'><div class='modal-body'><p>" + text + "</p></div><div class='modal-footer'><button type='button' class='btn btn-default' data-dismiss='modal'>Close</button></div></div></div></div>";
        $(html).modal();
    }

    function PasswordSelected() {
        document.getElementById('<%= ((RadioButton)FindControl("rbn_Username")).ClientID %>').checked = false;
        document.getElementById('<%= ((Label)FindControl("lbl_Instruction")).ClientID %>').innerHTML = 'Please enter your username to reset your password:';
        document.getElementById('<%= ((Label)FindControl("lbl_Prompt")).ClientID %>').innerHTML = 'Username';
        document.getElementById('<%= ((Button)FindControl("btn_Recover")).ClientID %>').value = 'Reset Password';
        document.getElementById('<%= ((RequiredFieldValidator)FindControl("RequiredFieldValidator")).ClientID %>').style.display = 'none';
        document.getElementById('<%= ((RequiredFieldValidator)FindControl("RequiredFieldValidator")).ClientID %>').innerHTML = "Please provide your username.";
        ValidatorEnable(document.getElementById('<%=RegularExpressionValidator.ClientID%>'), false);
    }

    function UsernameSelected() {        
        document.getElementById('<%= ((RadioButton)FindControl("rbn_Password")).ClientID %>').checked = false;
        document.getElementById('<%= ((Label)FindControl("lbl_Instruction")).ClientID %>').innerHTML = 'Please enter your email address to request your username:';
        document.getElementById('<%= ((Label)FindControl("lbl_Prompt")).ClientID %>').innerHTML = 'Email Address';
        document.getElementById('<%= ((Button)FindControl("btn_Recover")).ClientID %>').value = 'Request Username';
        document.getElementById('<%= ((RequiredFieldValidator)FindControl("RequiredFieldValidator")).ClientID %>').style.display = 'none';
        document.getElementById('<%= ((RequiredFieldValidator)FindControl("RequiredFieldValidator")).ClientID %>').innerHTML = "Please provide an email address.";
        ValidatorEnable(document.getElementById('<%=RegularExpressionValidator.ClientID%>'), true);
    }

</script>

<style type="text/css">
    #s4-workspace { border-radius: 4px; width:450px !important; height: auto !important; position:absolute; left:50%; top:50%; margin-left: -225px !important; margin-top: -100px !important; }
    #s4-bodyContainer {padding-bottom: 0 !important; }
    #AnonymousHeader { background: #FFFFFF; margin: -15px 0 0 0; padding: 7px; }           
    a, input[type='text'], input[type='password'] { display:block; padding-right: 10px; margin-top: 2px; }
    label { display:inline }
    .terms-input { padding: 4px 6px; border-radius: 4px; height: 20px; color: rgb(85, 85, 85); line-height: 20px; font-size: 14px; vertical-align: middle; display: inline-block; -webkit-border-radius: 4px; -moz-border-radius: 4px; }
    .terms-input { border: 1px solid rgb(204, 204, 204); transition:border 0.2s linear, box-shadow 0.2s linear; box-shadow: inset 0px 1px 1px rgba(0,0,0,0.075); background-color: rgb(255, 255, 255); -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); -moz-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075); -webkit-transition: border linear .2s, box-shadow linear .2s; -moz-transition: border linear .2s, box-shadow linear .2s; -o-transition: border linear .2s, box-shadow linear .2s; }
    .terms-input { overflow: hidden; overflow-y: scroll; width: 810px; height: 164px; }
    .terms-input:focus { border-color: rgba(82, 168, 236, 0.8); outline: dotted thin; box-shadow: inset 0px 1px 1px rgba(0,0,0,0.075), 0px 0px 8px rgba(82,168,236,0.6); -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075), 0 0 8px rgba(82, 168, 236, 0.6); -moz-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075), 0 0 8px rgba(82, 168, 236, 0.6); }
    .rbnf { float:left; margin-right: 10px; }
    .pxContent { padding: 10px 10px 10px 15px; font-family: Open Sans; color: #454545; }
</style>

<div id="AnonymousHeader">
    <div><img class="logo" src="/_layouts/15/Px.Branding/Images/Logo-195x50.png" alt=""></div>
    <div style="clear:both"></div>
</div>

<div class="pxContent">
   
    <div style="margin-bottom: 5px; margin-top: 8px; ">
        <div><asp:RadioButton ID="rbn_Password" runat="server" Text="Reset Password" Checked="true" CssClass="rbnf" /></div>
        <div><asp:RadioButton ID="rbn_Username" runat="server" Text="Retrieve Username" /></div>
        <div class="clear"></div>
    </div>
    
    <div style="margin-bottom: 5px;">
        <div><asp:Label ID="lbl_Instruction" runat="server" Text="Please Enter your username to reset your password:"></asp:Label></div>
    </div>

    <div class="pxFields">
        <asp:Label ID="lbl_Prompt" runat="server" Text="Username"></asp:Label>
        <asp:TextBox ID="txb_Prompt" runat="server" Width="150px"></asp:TextBox>        
    </div>

    <div class="pxFields" style="margin-top: 27px;">
        <asp:Button ID="btn_Recover" runat="server" Text="Reset Password" OnClick="btn_Recover_Click" CssClass="pxBtn" Height="28px"/>  <!--#286793-->
        <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" OnClick="btn_Cancel_Click" CssClass="pxBtnCancel" Height="28px" CausesValidation="False"/>
    </div>
    <div class="clear"></div>

    <div>
        <asp:RequiredFieldValidator 
            ID="RequiredFieldValidator" 
            runat="server" 
            Display="Dynamic"                
            CssClass="text-error"
            ControlToValidate="txb_Prompt"                
            ErrorMessage="Please provide your username.">
        </asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator 
            ID="RegularExpressionValidator" 
            runat="server" 
            Enabled="False"
            Display="Dynamic"                
            CssClass="text-error" 
            ControlToValidate="txb_Prompt"          
            ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$"      
            ErrorMessage="Invalid email address.">
        </asp:RegularExpressionValidator>
    </div>
    
    <asp:Literal ID="ltr_Debug" runat="server"></asp:Literal>
    
</div>