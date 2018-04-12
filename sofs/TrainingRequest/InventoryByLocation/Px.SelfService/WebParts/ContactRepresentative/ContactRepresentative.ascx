<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactRepresentative.ascx.cs" Inherits="Px.SelfService.WebParts.ContactRepresentative.ContactRepresentative" %>

<style type="text/css">
    input[type=button], input[type=reset], input[type=submit], button { 
    margin-left: 0 !important;
    }
    textarea {
        display: block;
    }

</style>

<div>
    Please complete the following to speak with a representative:
    <div class="clear8"></div>
    <div class="pxFields" >
        <asp:Label ID="lbl_CompanyName" runat="server" Text="Company Name"></asp:Label>
        <asp:TextBox ID="txb_CompanyName" runat="server" Width="325px"></asp:TextBox>
    </div>
    <div class="clear"></div>

    <div class="pxFields" >
        <asp:Label ID="lbl_FirstName" runat="server" Text="First Name"></asp:Label>
        <asp:TextBox ID="txb_FirstName" CssClass="input-medium" runat="server"></asp:TextBox>
    </div>
    <div class="pxFields" >
        <asp:Label ID="lbl_LastName" runat="server" Text="Last Name"></asp:Label>
        <asp:TextBox ID="txb_LastName" CssClass="input-medium" runat="server" ></asp:TextBox>
    </div>
    <div class="clear"></div>

    <div class="pxFields" >
        <asp:Label ID="lbl_EmailAddress" runat="server" Text="Email Address"></asp:Label>
        <asp:TextBox ID="txb_EmailAddress" CssClass="input-medium" runat="server"></asp:TextBox>
    </div>
    <div class="pxFields" >
        <asp:Label ID="lbl_PhoneNumber" runat="server" Text="Phone Number"></asp:Label>
        <asp:TextBox ID="txb_PhoneNumber" CssClass="input-medium" runat="server"></asp:TextBox>
    </div>
    <div class="clear"></div>

    <div class="pxFields">
        <asp:Label ID="lbl_Comments" runat="server" Text="Questions & Comments"></asp:Label>
        <asp:TextBox ID="txb_Comments" runat="server" TextMode="MultiLine" Height="164px" Width="660px"></asp:TextBox>
    </div>
    <div class="clear"></div>

    <asp:Button Text="Request Contact" ID="Button1" runat="server" CssClass="" OnClick="Button1_Click" />
</div>

    
