<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccountCreated.ascx.cs" Inherits="Px.SelfService.WebParts.AccountCreated.AccountCreated" %>

<style type="text/css">
    #s4-workspace { -moz-border-radius: 3px; -webkit-border-radius: 3px; border-radius: 3px; height: auto !important; left: 50%; margin-left: -225px !important; margin-top: -100px !important; position: absolute; width: 405px !important;  top: 50%; }
    #AnonymousHeader { background: #FFFFFF; margin: -15px 0 0 0; padding: 7px; }
    .pxContent { padding: 10px; }
    .pxContent span { color: #454545; display: block; font: 13px "Open Sans", Helvetica, Arial, sans-serif;  margin-top: 6px;  }
</style>

<div id="AnonymousHeader">
    <div>
        <img class="logo" src="/_layouts/15/Px.Branding/Images/Logo-195x50.png" alt=""></div>
    <div style="clear: both"></div>
</div>

<div class="pxContent">
    <asp:Literal ID="ltr_html" runat="server"></asp:Literal>
</div>
