<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="trainingrequest" TagName="ScriptAndStyle" Src="~/_controltemplates/15/TrainingRequest.Forms/ImportStyleAndScript.ascx" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayRequest.aspx.cs" Inherits="TrainingRequest.Forms.Layouts.TrainingRequest.Forms.Pages.DisplayRequest" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div id="div_request">

        <div class="requestHeader">
            <h2>Training Material Request</h2>
        </div>

        <div id="div_menu">
            <ul>
                <li><a href="#div_coreinformation">Core Information</a></li>
                <li><a href="#div_relatedDocs">Support Documents</a></li>
                <li><a href="#div_relatedNotes">Related Notes</a></li>
                <li><a href="#div_uploadMaterials">Materials</a></li>
            </ul>

            <div id="div_coreinformation" class="div_tab">
                <%-- Core Information Section --%>
                <div class="sectionHeader">
                    <h3>Core Information</h3>
                </div>

                <div id="div_displayCore">
                    <table id="tbl_displayCore">
                        <tr>
                            <td>
                                <label class="requestLabel">Requestor</label></td>
                            <td>
                                <label id="lbl_requestor" runat="server" class="requestData"></label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="requestLabel">Request Title</label></td>
                            <td>
                                <label id="lbl_requestTitle" runat="server" class="requestData"></label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="requestLabel">Request Type</label></td>
                            <td>
                                <label id="lbl_requestType" runat="server" class="requestData"></label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="requestLabel">Planned Training</label></td>
                            <td>
                                <label id="lbl_plannedTraining" runat="server" class="requestData"></label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="requestLabel">Department</label></td>
                            <td>
                                <label id="lbl_dept" runat="server" class="requestData"></label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="requestLabel">Description</label></td>
                            <td>
                                <label id="lbl_description" runat="server" class="requestData"></label>
                            </td>
                        </tr>

                    </table>
                </div>

            </div>
            <div id="div_relatedDocs" class="div_tab">
                <%-- Support Documents Section --%>
                <div class="sectionHeader">
                    <h3>Support Documents</h3>
                </div>
                <div id="div_rootSupportDocs">
                    <label class="requestLabel">Root Support Documents</label>
                    <asp:Panel ID="pn_supportDoc" runat="server" GroupingText="Root Request Documents">
                        <asp:Literal ID="ltr_rootDocuments" runat="server" ></asp:Literal>
                    </asp:Panel>
                </div>
                <div>
                    <label class="requestLabel">Enter document path:</label><asp:TextBox ID="txb_documentPath" runat="server"></asp:TextBox>
                    <asp:Button ID="btn_addDocumentPath" runat="server" Text="Add" />

                    <div id="div_requestDocLinks">
                        <asp:GridView ID="gv_relatedDocuments" runat="server">
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div id="div_relatedNotes" class="div_tab">
                <%-- Related Notes Section --%>
                <div class="sectionHeader">
                    <h3>Related Notes</h3>
                </div>
                <div>
                    <label class="requestLabel">Notes:</label>
                    <div class="clear"></div>
                    <asp:TextBox ID="txb_notes" runat="server" TextMode="MultiLine" CssClass="requestNotes"></asp:TextBox>
                    <div class="clear"></div>
                    <label class="requestLabel">History</label>
                    <div class="clear"></div>
                    <div id="div_notesHistory">
                        <asp:Literal ID="ltr_notesHistory" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
            <div id="div_uploadMaterials" class="div_tab">
                <%-- Upload Material Section --%>
                <div class="sectionHeader">
                    <h3>Materials</h3>
                </div>
                <div>
                    <asp:FileUpload ID="fu_Material" runat="server" /><asp:Button ID="btn_uploadMaterial" runat="server" Text="Upload Material" />
                </div>
                <div>
                    <asp:Literal ID="ltr_Materials" runat="server"></asp:Literal>
                </div>
            </div>
        </div>

        <div class="clear"></div>
        <div id="div_buttons">
            <asp:Button ID="btn_save" runat="server" CssClass="requestButton" Text="Save" OnClick="save" />
            <button id="btn_close" class="requestButton" onclick="closeThis()">Close</button>
        </div>

    </div>


    <div id="pnlProgressFull" class="div_spinner" style="display: none;" runat="server">
        <div id="divProgressLoader" class="loader">
            <span class="inno-comments-title">
                <img src="../Images/ajax-loader-white.gif" alt="Loading.." />
                Please Wait... </span>
        </div>
    </div>


    <asp:HiddenField ID="hf_thisGuid" runat="server" />
    <asp:HiddenField ID="hf_parentGuid" runat="server" />
    <trainingrequest:ScriptAndStyle ID="trainingrequest_style" runat="server" />

    <script>
        var progressId = '<%=pnlProgressFull.ClientID%>';
            
    </script>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
</asp:Content>
