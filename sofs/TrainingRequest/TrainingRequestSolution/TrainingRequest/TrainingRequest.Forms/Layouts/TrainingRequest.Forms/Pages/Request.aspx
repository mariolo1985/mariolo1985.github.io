<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="trainingrequest" TagName="ScriptAndStyle" Src="~/_controltemplates/15/TrainingRequest.Forms/ImportStyleAndScript.ascx" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Request.aspx.cs" Inherits="TrainingRequest.Forms.Layouts.TrainingRequest.Forms.Pages.Request" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">

</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div id="div_request">
        <table class="requestMenu">
            <tr>
                <td class="menuItem">
                    <button class="menuButton">Core Information</button></td>
                <td class="menuItem">
                    <button class="menuButton">Related Documents</button></td>
                <td class="menuItem">
                    <button class="menuButton">Related Notes</button></td>
                <td class="menuItem">
                    <button class="menuButton">Upload Materials</button></td>
            </tr>
        </table>

        <div id="div_menu">
            <ul>
                <li><a href="#div_menu1">Core Information</a></li>
                <li><a href="#div_menu2">Related Documents</a></li>
                <li><a href="#div_menu3">Related Notes</a></li>
                <li><a href="#div_menu4">Upload Materials</a></li>
            </ul>

            <div id="div_menu1">menu1</div>
            <div id="div_menu2">menu2</div>
            <div id="div_menu3">menu3</div>
            <div id="div_menu4">menu4</div>
        </div>
        <div class="requestHeader">
            <h2>Training Material Request</h2>
        </div>

        <%-- Core Information Section --%>
        <div class="sectionHeader">
            <h3>Core Information</h3>
        </div>
        <div class="requestField">
            <label class="requestLabel">Request Title</label>
            <label id="lbl_requestTitle" runat="server" class="requestData"></label>
        </div>
        <div class="clear"></div>

        <div class="requestField">
            <label class="requestLabel">Request Type</label>
            <label id="lbl_requestType" runat="server" class="requestData"></label>
        </div>

        <div id="div_plannedTraining" runat="server" class="requestField">
            <label class="requestLabel">Planned Training</label>
            <label id="lbl_plannedTraining" runat="server" class="requestData"></label>
        </div>

        <div class="requestField">
            <label class="requestLabel">Department</label>
            <label id="lbl_dept" runat="server" class="requestData"></label>
        </div>
        <div class="clear"></div>

        <%-- Related Documents Section --%>
        <div class="sectionHeader">
            <h3>Related Documents</h3>
        </div>
        <div>
            <label class="requestLabel">Enter document path:</label><asp:TextBox ID="txb_documentPath" runat="server"></asp:TextBox>
            <asp:Button ID="btn_addDocumentPath" runat="server" Text="Add" />
            <asp:GridView ID="gv_relatedDocuments" runat="server">
            </asp:GridView>
        </div>
        <%-- Related Notes Section --%>
        <div class="sectionHeader">
            <h3>Related Notes</h3>
        </div>
        <div>
            <label class="requestLabel">Notes:</label><asp:TextBox ID="txb_notes" runat="server" TextMode="MultiLine" CssClass="requestNotes"></asp:TextBox>
            <div class="clear"></div>
            <label class="requestLabel">History</label><asp:Literal ID="ltr_notesHistory" runat="server"></asp:Literal>
        </div>

        <%-- Upload Material Section --%>
        <div class="sectionHeader">
            <h3>Materials</h3>
        </div>
        <div>
            <asp:FileUpload ID="fu_Material" runat="server" /><asp:Button ID="btn_uploadMaterial" runat="server" Text="Upload Material" />
        </div>
        <div>
            <asp:GridView ID="gv_materials" runat="server"></asp:GridView>
        </div>
    </div>
    <trainingrequest:ScriptAndStyle ID="trainingrequest_style" runat="server" />
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    Request
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Request
</asp:Content>
