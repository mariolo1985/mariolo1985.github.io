<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewUser.aspx.cs" Inherits="Partner.MiscWebParts.Layouts.Partner.MiscWebParts.Pages.NewUser" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <style type="text/css">
        .txtBox { width: 200px; }
    </style>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <table cellpadding="5" border="0">  
        <tr>
            <td></td>
            <td>
                <h2 style="margin-bottom:0px;">New User/Partner</h2>
            </td>
        </tr>      
        <!-- username -->
        <tr>
            <td class='ms-vb' align='right'><asp:Label ID="lblUsername" runat="server" Text="Username:"></asp:Label></td>
            <td class='ms-vb'><asp:TextBox ID="tbUsername" runat="server" CssClass="txtBox"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td><td class='ms-vb'><asp:Label ID="lblUsernameValidation" ForeColor="Red" runat="server" Text=""></asp:Label></td>
        </tr>
        <!-- display name -->
        <tr>
            <td class='ms-vb' align='right'><asp:Label ID="lblDisplayName" runat="server" Text="Display Name:"></asp:Label></td>
            <td class='ms-vb'><asp:TextBox ID="tbDisplayName" runat="server" CssClass="txtBox"></asp:TextBox></td>
        </tr>
        <!-- email address -->
        <tr>
            <td class='ms-vb' align='right'><asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label></td>
            <td class='ms-vb'><asp:TextBox ID="tbEmail" runat="server" CssClass="txtBox"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td><td class='ms-vb'><asp:Label ID="lblEmailValidation" ForeColor="Red" runat="server" Text=""></asp:Label></td>
        </tr>
        <!-- partner id -->
        <tr>
            <td class='ms-vb' align='right'><asp:Label ID="lblPartnerId" runat="server" Text="Partner ID:"></asp:Label></td>
            <td class='ms-vb'><asp:TextBox ID="tbPartnerId" runat="server" CssClass="txtBox"></asp:TextBox></td>
        </tr>
        <!-- partner name -->
        <tr>
            <td class='ms-vb' align='right'><asp:Label ID="lblPartnerName" runat="server" Text="Partner Name:"></asp:Label></td>
            <td class='ms-vb'><asp:TextBox ID="tbPartnerName" runat="server" CssClass="txtBox"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td><td class='ms-vb'><asp:Label ID="lblPartnerValidation" ForeColor="Red" runat="server" Text=""></asp:Label></td>
        </tr>
        <!-- submit -->
        <tr>
            <td class='ms-vb'></td>
            <td class='ms-vb'><asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" Text="Submit" runat="server"></asp:Button></td>
        </tr>
        <tr>
            <td></td><td class='ms-vb'><asp:Label ID="lblStatus" runat="server" Text=""></asp:Label></td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
New User
</asp:Content>
