<%@ Assembly Name="TrainingRequestWFProj, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6bc354b2646b988d" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainingTask.aspx.cs" Inherits="TrainingRequestWFProj.Layouts.TrainingRequestWF.Pages.TrainingTask" DynamicMasterPageFile="~masterurl/default.master" %>



<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <%--<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8"/>--%>
    <link rel="Stylesheet" type="text/css" href="<%=Microsoft.SharePoint.Utilities.SPUtility.MakeBrowserCacheSafeLayoutsUrl("Workflow.TrainingRequest/CSS/TaskItemStyle.css", false)%>" />
    <script type="text/javascript" src="<%=Microsoft.SharePoint.Utilities.SPUtility.MakeBrowserCacheSafeLayoutsUrl("Workflow.TrainingRequest/Script/TrainingTaskScript.js", false)%>"></script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <%--GENERAL TASK INFO--%>
    <div id="divTaskInfo" class="divParentHide" runat="server">
        <div id="divHeader" runat="server" class="Header">
        </div>
        <div id="divGeneral" runat="server">
        </div>
        <div id="divAssignTask" runat="server">
            <div id="divAssignTaskBody" runat="server">
            </div>

            <label for="peAssignTo">*Group only</label>
            <SharePoint:PeopleEditor ID="peAssignTo" runat="server" AutoPostBack="false" MultiSelect="false" ValidatorEnabled="true" SelectionSet="SPGroup" PlaceButtonsUnderEntityEditor="true" />
        </div>
        <div id="divReviewTask" runat="server">
        </div>
        <div id="divTrainer" runat="server">
        </div>
        <div id="divTrainingMgr" runat="server">
            <div id="divTMgrBody" runat="server">
            </div>

            <label for="peAssignTrainer">*User only</label>
            <SharePoint:PeopleEditor ID="peAssignTrainer" runat="server" AutoPostBack="false" MultiSelect="false" ValidatorEnabled="true" SelectionSet="User" PlaceButtonsUnderEntityEditor="true" />
        </div>


        <%--BUTTONS--%>
        <div id="divAssignButtons" class="divParentHide" runat="server">
            <asp:Button ID="btn_Assign" runat="server" OnClick="btn_Assign_Click" Text="Assign" CssClass="buttonAssign" BorderStyle="None" />
            <asp:Button ID="btn_Cancel1" runat="server" OnClick="btn_Cancel_Click" Text="Cancel" CssClass="buttonCancel" BorderStyle="None" />
        </div>

        <div id="divReviewButtons" class="divParentHide" runat="server">
            <asp:Button ID="btn_Reviewed" runat="server" OnClick="btn_Reviewed_Click" Text="Review" CssClass="buttonAssign" BorderStyle="None" />
            <asp:Button ID="btn_Cancel2" runat="server" OnClick="btn_Cancel_Click" Text="Cancel" CssClass="buttonCancel" BorderStyle="None" />
        </div>

        <%--ERROR SECTION--%>
        <div id="divErrorSection" class="divParentHide" runat="server">
            <div id="divMessage" runat="server" class="ErrorMessage">
            </div>
        </div>
        <div id="divCloseBtn" runat="server" class="divParentHide">
            <asp:Button ID="btn_Close" runat="server" OnClick="btn_Cancel_Click" Text="Close" CssClass="buttonCancel" BorderStyle="None" />
        </div>
    </div>
    <div id="divInProgress" runat="server" class="divParentHide">
        Your action is being processed...
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    <asp:Literal ID="litPageTitle" runat="server"></asp:Literal>
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    PageTitleInTitleArea
</asp:Content>
