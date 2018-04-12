<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WarehouseSettings.ascx.cs" Inherits="Px.SelfService.WebParts.WarehouseSettings.WarehouseSettings" %>

<!-- LAYOUT -->
<link href="/_layouts/15/Px.SelfService/CSS/pxSettings.css" rel="stylesheet" type="text/css" />


<!-- SHARED NAVIGATION -->
<asp:Literal ID="ltr_SharedNav" runat="server"></asp:Literal>
<asp:Literal ID="ltr_Username" runat="server"></asp:Literal>

<!-- TITLE -->
<div style="margin-left: 20px; margin-top: 10px; margin-bottom: 10px;">
    <span class="pxSettingsTitle">Supplier Oasis Settings</span>
</div>


<!-- Tabs -->
<div class="tabs">
    <ul>
        <li><a href="/pages/CompanySettings.aspx">Company Settings</a></li>
        <li><a href="/pages/PersonalSettings.aspx">Personal Settings</a></li>
        <li><a href="#" class="activeTab">Warehouse Settings</a></li>
    </ul>
</div>
<div class="clear"></div>


<!-- FORM -->
<div class="pxWrap">

    <span class="sectionHeading">Warehouse Settings</span>

    <!-- PROOF OF CONCEPT WITH TABLE ELEMENT -->
    <asp:Literal ID="ltr_table" runat="server"></asp:Literal>

    <asp:Button ID="btnAddWarehouse" runat="server" Text="Add Supplier Warehouse" onclick="btnAddWarehouseClick" CssClass="settingsButton" />
</div>
