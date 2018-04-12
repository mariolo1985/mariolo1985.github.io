<%@ Assembly Name="TrainingRequest.Forms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=cf26b8946d48cfd0" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="trainingrequest" TagName="ScriptAndStyle" Src="~/_controltemplates/15/TrainingRequest.Forms/ImportStyleAndScript.ascx" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="TrainingRequest.Forms.Layouts.TrainingRequest.Forms.Pages.NewRequest" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div id="div_request">
        <div class="requestHeader">
            <h2>New Training Material Request</h2>
        </div>

        <div class="requestField">
            <label class="requestLabel" for="txb_requestTitle">Request Title: <span class="requiredAsterisk">*</span></label>
            <asp:TextBox ID="txb_requestTitle" runat="server" CssClass="requestTextbox" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="clear"></div>

        <div class="requestField">
            <label class="requestLabel" for="txb_requestDescription">Request Description: <span class="requiredAsterisk">*</span></label>
            <asp:TextBox ID="txb_requestDescription" runat="server" CssClass="requestTextbox" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="clear"></div>

        <div class="requestField">
            <label class="requestLabel" for="ddl_requestType">Request Type: <span class="requiredAsterisk">*</span></label>
            <asp:DropDownList ID="ddl_requestType" runat="server" CssClass="requestDropdownlist"></asp:DropDownList>
        </div>
        <div class="clear"></div>

        <div id="div_plannedTraining" class="requestField" style="display: none;">
            <label class="requestLabel" for="chb_plannedTraining">Planned Training: </label>
            <asp:RadioButton ID="rad_plannedTrainingY" runat="server" GroupName="plannedTrainingGroup" Text="Yes" />
            <asp:RadioButton ID="rad_plannedTrainingN" runat="server" GroupName="plannedTrainingGroup" Text="No" Checked="true" />
        </div>
        <div class="clear"></div>

        <div id="div_dept">
            <label class="requestLabel" for="ddl_department">Department: <span class="requiredAsterisk">*</span></label>
            <div>
                <div id="div_addDeptBox">
                    <asp:ListBox ID="lsb_SourceDept" runat="server" CssClass="requestListbox" ToolTip="Available Departments"></asp:ListBox>
                </div>
                <div id="div_addDeptBtn">
                    <%--                    <input type="button" id="btn_addDept" value="Select" class="addDeptBtn" name="ADD" />
                    <div class="clear"></div>
                    <input type="button" id="btn_removeDept" value="Remove" class="addDeptBtn" name="REMOVE" />--%>
                    <asp:Button ID="btn_addDept" runat="server" CssClass="addDeptBtn" Text="Select" OnCommand="btn_DeptClick" CommandName="ADD" />
                    <div class="clear"></div>
                    <asp:Button ID="btn_removeDept" runat="server" CssClass="addDeptBtn" Text="Remove" OnCommand="btn_DeptClick" CommandName="REMOVE" />
                </div>
                <div id="div_removeDeptBox">
                    <asp:ListBox ID="lsb_SelectedDept" runat="server" CssClass="requestListbox" ToolTip="Selected Departments"></asp:ListBox>

                </div>
            </div>
        </div>

        <div class="clear"></div>

        <div class="requestField">
            <asp:Panel ID="pn_supportingDocs" GroupingText="Supporting Documents" CssClass="panelSupportingDocuments" runat="server" BorderStyle="None">
                <asp:FileUpload ID="file_supportingDocs" runat="server" AllowMultiple="True" CssClass="fileUploader" />
                <asp:Button ID="btn_uploadAttachment" runat="server" OnClick="UploadAttachment" CssClass="uploadButton" Text="Upload" OnClientClick="showProgress()" />

                <div class="div_attachments" id="div_attachments" runat="server">
                    <asp:Literal ID="ltr_attachmentLinks" runat="server"></asp:Literal>
                </div>

            </asp:Panel>

        </div>


        <div class="clear"></div>
        <div id="div_buttons">
            <asp:Button ID="btn_submit" runat="server" CssClass="requestButton" Text="Request" OnClick="submitRequest" OnClientClick="showProgress()" />
            <button id="btn_close" class="requestButton" onclick="closeThis()">Close</button>
        </div>
    </div>

    <div id="div_confirm" style="display: none;">
        <div id="div_success" class="confirmMsg">Your request was submitted successfully.</div>
        <div class="clear"></div>
        <div id="div_error" class="confirmMsg">Your request could not be submitted at this time.</div>
    </div>


    <div id="pnlProgressFull" class="div_spinner" style="display: none;" runat="server">
        <div id="divProgressLoader" class="loader">
            <span class="inno-comments-title">
                <img src="../Images/ajax-loader-white.gif" alt="Loading.." />
                Please Wait... </span>
        </div>
    </div>


    <asp:HiddenField ID="hf_rootGuid" runat="server" />
    <trainingrequest:ScriptAndStyle ID="trainingrequest_style" runat="server" />
    <script>
        var requestTypeDdlId = '<%=ddl_requestType.ClientID%>';
        var availableLsbId = '<%=lsb_SourceDept.ClientID%>';
        var selectedLsbId = '<%=lsb_SelectedDept.ClientID%>';
        var supportDocsPnId = '<%=div_attachments.ClientID%>';
        var progressId = '<%=pnlProgressFull.ClientID%>';

        addListeners();
    </script>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
</asp:Content>
