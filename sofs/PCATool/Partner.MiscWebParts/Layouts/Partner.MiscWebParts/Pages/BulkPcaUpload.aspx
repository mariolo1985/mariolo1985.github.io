<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BulkPcaUpload.aspx.cs" Inherits="Partner.MiscWebParts.Layouts.Partner.MiscWebParts.Pages.BulkPcaUpload" DynamicMasterPageFile="~masterurl/default.master" %>

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
                <h2 style="margin-bottom:0px;">Bulk PCA Assignment</h2>
            </td>
        </tr>
        <tr>
            <td class='ms-vb' align='right'> File to Upload:</td>
            <td bgcolor="#FFFFFF">
                <asp:FileUpload ID="fileUpload" runat="server" /><br />
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" /><br />
                <asp:label ID="lblStatus" runat="server"></asp:label>                
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Bulk PCA Upload
</asp:Content>
