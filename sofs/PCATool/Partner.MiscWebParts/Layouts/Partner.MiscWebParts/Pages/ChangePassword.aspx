<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="Partner.MiscWebParts.Layouts.Partner.MiscWebParts.Pages.ChangePassword" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <style type="text/css">
        .txtBox { width: 140px; }
    </style>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <table cellpadding="5" border="0">
        <tr>
            <td class='ms-vb'><asp:Label ID="Label1" runat="server" Text="Enter new password:"></asp:Label></td>
            <td class='ms-vb'><asp:TextBox ID="TextBox1" TextMode="Password" runat="server" CssClass="txtBox"></asp:TextBox></td>
        </tr>
        <tr>
            <td class='ms-vb'><asp:Label ID="Label2" runat="server" Text="Confirm new password:"></asp:Label></td>
            <td class='ms-vb'><asp:TextBox ID="TextBox2" TextMode="Password" runat="server" CssClass="txtBox"></asp:TextBox></td>
        </tr>
        <tr>
            <td class='ms-vb'></td>
            <td class='ms-vb'><asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" Text="Submit" runat="server"></asp:Button></td>
        </tr>
        <tr>
            <td class='ms-vb'><asp:Label ID="lblOutput" runat="server" Text=""></asp:Label></td>
        </tr>
    </table>
    <div>
        <ul style="padding-left: 14px;">
            <li>Password cannot match the 4 previous passwords</li>
            <li>Password cannot contain the partner's account name or parts of partner's full name that exceed two consecutive characters</li>
            <li>Password must be at least 8 characters long</li>
            <li>Password must contain 3 out of 4 of the following: Uppercase, Lowercase, Numbers, Special Characters</li>
        </ul>
   </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Change Password
</asp:Content>

