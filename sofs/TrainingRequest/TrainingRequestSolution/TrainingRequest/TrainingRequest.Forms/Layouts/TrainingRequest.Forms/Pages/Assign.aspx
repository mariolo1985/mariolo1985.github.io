<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="trainingrequest" TagName="ScriptAndStyle" Src="~/_controltemplates/15/TrainingRequest.Forms/ImportStyleAndScript.ascx" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Assign.aspx.cs" Inherits="TrainingRequest.Forms.Layouts.TrainingRequest.Forms.Pages.Assign" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">


</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div id="div_request">
        <div class="requestHeader">
            <h2>Assign Trainer</h2>
        </div>

        <div class="requestField">
            <label class="requestLabel" for="txb_requestTitle">Request Title:</label>
            <asp:TextBox ID="txb_requestTitle" runat="server" CssClass="requestTextbox" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
        </div>
        <div class="clear"></div>

        <div class="requestField">
            <label class="requestLabel" for="txb_requestDescription">Request Description:</label>
            <asp:TextBox ID="txb_requestDescription" runat="server" CssClass="requestTextbox" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
        </div>
        <div class="clear"></div>

        <div class="requestField">
            <label class="requestLabel" for="ddl_requestType">Request Type:</label>
            <asp:TextBox ID="txb_requestType" runat="server" CssClass="requestTextbox" ReadOnly="true"></asp:TextBox>
        </div>
        <div class="clear"></div>

        <div id="div_plannedTraining" class="requestField">
            <label class="requestLabel" for="chb_plannedTraining">Planned Training: </label>
            <asp:TextBox ID="txb_plannedTraining" runat="server" CssClass="requestTextbox" ReadOnly="true"></asp:TextBox>
        </div>
        <div class="clear"></div>

        <div id="div_dept" class="requestField">
            <label class="requestLabel" for="ddl_department">Department:</label>
            <asp:TextBox ID="txb_requestDept" runat="server" CssClass="requestTextbox" ReadOnly="true"></asp:TextBox>
        </div>

        <div class="clear"></div>

        <div class="requestField">
            <label class="requestLabel" for="pe_NewTaskUser" style="color: red;">Please assign a trainer below: </label>
            <SharePoint:PeopleEditor ID="pe_NewTaskUser" runat="server" CssClass="peNewTaskUser" MultiSelect="False" PlaceButtonsUnderEntityEditor="true" />
        </div>


        <div class="clear"></div>

        <div id="div_buttons">
            <asp:Button ID="btn_submit" runat="server" CssClass="requestButton" Text="Assign" />
            <button id="btn_close" class="requestButton" onclick="closeThis()">Close</button>
        </div>
    </div>

    <trainingrequest:ScriptAndStyle ID="trainingrequest_style" runat="server" />
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
</asp:Content>
