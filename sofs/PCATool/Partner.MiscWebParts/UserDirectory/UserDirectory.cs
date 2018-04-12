using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using Oasis.Common;
using Partner.Administration;
using Partner.Infrastructure;

namespace Partner.MiscWebParts.UserDirectory
{
    [ToolboxItemAttribute(false)]
    public class UserDirectory : WebPart
    {
        protected DropDownList field_DropDownList;
        protected DropDownList condition_DropDownList;
        protected TextBox value_TextBox;

        protected Button applyFilter_Button;
        protected Button clearFilter_Button;

        private String filterField = String.Empty;
        private String filterCondition = String.Empty;
        private String filterValue = String.Empty;

        protected override void OnLoad(EventArgs e)
        {
            // this.ChromeType = PartChromeType.None;
            String cssHref = SPUtility.MakeBrowserCacheSafeLayoutsUrl("Partner.MiscWebParts/CSS/o-userdirectory.css", false);
            this.Page.Header.Controls.Add(new LiteralControl(String.Format("<link rel='stylesheet' type='text/css' href='{0}'/>", cssHref)));
            String jsSrc = SPUtility.MakeBrowserCacheSafeLayoutsUrl("Partner.MiscWebParts/Scripts/UserDirectory.js", false);
            this.Page.Header.Controls.Add(new LiteralControl(String.Format("<script type='text/javascript' src='{0}'></script>", jsSrc)));
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            field_DropDownList = new DropDownList();
            field_DropDownList.ID = "fieldDropDownList";
            field_DropDownList.Items.Add("Login");
            field_DropDownList.Items.Add("Partner");
            field_DropDownList.Items.Add("ID");
            field_DropDownList.Height = 20;
            Controls.Add(field_DropDownList);

            condition_DropDownList = new DropDownList();
            condition_DropDownList.ID = "conditionDropDownList";
            condition_DropDownList.Items.Add("Begins");
            condition_DropDownList.Items.Add("Contains");
            condition_DropDownList.Items.Add("Equals");
            condition_DropDownList.Height = 20;
            Controls.Add(condition_DropDownList);

            value_TextBox = new TextBox();
            Controls.Add(value_TextBox);

            applyFilter_Button = new Button();
            applyFilter_Button.Text = "Search";
            applyFilter_Button.Width = 80;
            applyFilter_Button.Click += new EventHandler(applyFilter_Click);
            Controls.Add(applyFilter_Button);

            clearFilter_Button = new Button();
            clearFilter_Button.Text = "Reset";
            clearFilter_Button.Width = 80;
            clearFilter_Button.Click += new EventHandler(clearFilter_Click);
            Controls.Add(clearFilter_Button);
        }

        protected override void OnPreRender(EventArgs e)
        {
            // querystring parameter handling...
            filterField = (!String.IsNullOrEmpty(this.Page.Request.QueryString["field"])) ? this.Page.Request.QueryString["field"] : String.Empty;
            filterCondition = (!String.IsNullOrEmpty(this.Page.Request.QueryString["condition"])) ? this.Page.Request.QueryString["condition"] : String.Empty;
            filterValue = (!String.IsNullOrEmpty(this.Page.Request.QueryString["value"])) ? this.Page.Request.QueryString["value"] : String.Empty;
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            try
            {
                this.EnsureChildControls();

                if (PartnerHelper.CurrentUserIsAdmin())
                {

                    writer.WriteLine("<div class='o-ud-filters'>");
                    writer.WriteLine("<span>");
                    writer.WriteLine("<span style='margin-left:20px;'>Search:</span>");
                    writer.WriteLine("<span>");
                    if (!String.IsNullOrEmpty(filterField)) field_DropDownList.SelectedValue = filterField;
                    field_DropDownList.RenderControl(writer);
                    if (!String.IsNullOrEmpty(filterCondition)) condition_DropDownList.SelectedValue = filterCondition;
                    condition_DropDownList.RenderControl(writer);
                    if (!String.IsNullOrEmpty(filterValue)) value_TextBox.Text = filterValue;
                    value_TextBox.RenderControl(writer);
                    writer.WriteLine("</span>");
                    writer.WriteLine("<span>");
                    applyFilter_Button.RenderControl(writer);
                    writer.WriteLine("</span>");
                    writer.WriteLine("<span>");
                    clearFilter_Button.RenderControl(writer);
                    writer.WriteLine("</span>");
                    writer.WriteLine("<span>");
                    writer.WriteLine("<img class='o-ud-thumbnail' src='/_layouts/images/helveticons/20/16x16/message-16x16.png' alt='' />Change Email");
                    writer.WriteLine("</span>");
                    writer.WriteLine("<span>");
                    writer.WriteLine("<img class='o-ud-thumbnail' src='/_layouts/images/helveticons/20/16x16/z-axis-rotation-16x16.png' alt='' />Change Password");
                    writer.WriteLine("</span>");
                    writer.WriteLine("<span>");
                    writer.WriteLine("<img class='o-ud-thumbnail' src='/_layouts/images/helveticons/20/16x16/lock-16x16.png' alt='' />Unlock");
                    writer.WriteLine("</span>");
                    writer.WriteLine("<span>");
                    writer.WriteLine("<img class='o-ud-thumbnail' src='/_layouts/images/helveticons/20/16x16/checkbox-16x16.png' alt='' />Enable/Disable");
                    writer.WriteLine("</span>");
                    writer.WriteLine("</div>");

                    using (SPSite site = new SPSite(Page.Request.Url.ToString()))
                    {
                        if (site.RootWeb.ListExists(PartnerConstants.List_PartnerMaster))
                        {
                            if (String.IsNullOrEmpty(filterValue))
                            {
                                writer.WriteLine("Please provide search parameters.</br>");
                            }
                            else
                            {
                                try
                                {
                                    SPList userLookup = site.RootWeb.Lists[PartnerConstants.List_UserLookup];
                                    SPQuery query = new SPQuery();
                                    query.Query = BuildCaml();
                                    query.RowLimit = 1000;
                                    SPListItemCollection userCollection = userLookup.GetItems(query);

                                    RenderUsers(writer, userCollection);
                                }
                                catch (Exception ex)
                                {
                                    writer.WriteLine("Error: " + ex.Message);
                                }
                            }
                        }
                    }
                }
                else
                {
                    writer.WriteLine("<div>Admin Access Required</div>");
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError("Partner.MiscWebParts.UserDirectory.RenderContents Error", ex, 1234);
            }
        }

        internal void RenderUsers(HtmlTextWriter writer, SPListItemCollection userCollection)
        {
            if (userCollection.Count == 0)
            {
                if (String.IsNullOrEmpty(value_TextBox.Text))
                {
                    writer.WriteLine("Please provide search parameters.</br>");
                }
                else
                {
                    writer.WriteLine("No Partners Found.</br>");
                }
            }
            else
            {
                writer.WriteLine("<div class='o-ud-users'>");
                writer.WriteLine("<table cellpadding='3' cellspacing='0' style='width:100%;'>");
                writer.WriteLine("<tr class='o-ud-heading'>");
                if (SPContext.Current.Web.CurrentUser.IsSiteAdmin)
                {
                    writer.WriteLine("<td></td>");
                    writer.WriteLine("<td>Claim</td>");
                }
                writer.WriteLine("<td>User Login</td>");
                writer.WriteLine("<td>Email</td>");
                writer.WriteLine("<td>Partner</td>");
                writer.WriteLine("<td>Partner ID</td>");
                writer.WriteLine("<td>Last Login</td>");
                writer.WriteLine("<td>Password Change</td>");
                writer.WriteLine("<td>Action</td>");
                writer.WriteLine("</tr>");

                PrincipalContext context = new PrincipalContext(ContextType.Domain, SPContext.Current.Site.WebApplication.Farm.GetProperty(PropertyBags.PartnerImport_UserDomain));
                Boolean alternate = true;

                foreach (SPListItem item in userCollection)
                {
                    StringBuilder html = new StringBuilder();
                    try
                    {
                        SPSecurity.RunWithElevatedPrivileges(delegate()
                        {
                            SPUser user = item.ReadUserField(PartnerConstants.Field_UserLogin);
                            UserPrincipal userPrincipal = null;
                            try
                            {                            
                                userPrincipal = UserPrincipal.FindByIdentity(context, PartnerHelper.RemoveClaimEncoding(user.LoginName));
                            }
                            catch (Exception ex)
                            {
                                LogHelper.LogError("Partner.MiscWebParts.UserDirectory.BuildDataRow Unable to get user principle.\n", ex, 1234);
                            }

                            String rowCss = alternate ? " class='o-ud-alternate'" : String.Empty;

                            html.AppendFormat("<tr{0}>", rowCss);                                    

                            // site admin list item edit & claim...
                            if (SPContext.Current.Web.CurrentUser.IsSiteAdmin)
                            {
                                html.AppendFormat("<td class='o-ud-thumbnail'><a href='{0}'><img src='/_layouts/images/helveticons/20/16x16/zoom-in-16x16.png' alt='' /></a></td>", DisplayForm(item));
                                html.AppendFormat("<td>{0}</td>", item.GetFormattedValue(PartnerConstants.Field_UserLogin));
                            }

                            // user login, email, partner title...
                            html.AppendFormat("<td>{0}</td>", PartnerHelper.RemoveClaimEncoding(user.LoginName));
                            html.AppendFormat("<td>{0}</td>", user.Email);
                            html.AppendFormat("<td>{0}</td>", item.GetFormattedValue(PartnerConstants.Field_PartnerTitle));
                            html.AppendFormat("<td>{0}</td>", item.GetFormattedValue(PartnerConstants.Field_PartnerID));

                            // last login...
                            try
                            {
                                String value = (userPrincipal == null) ? String.Empty : userPrincipal.LastLogon.Value.ToString("MM/dd/yyyy HH:mm");
                                html.AppendFormat("<td class='o-ud-datetime'>{0}</td>", value);
                            }
                            catch
                            {
                                html.Append("<td></td>");
                            }

                            // password change...
                            try
                            {
                                String value = (userPrincipal == null) ? String.Empty : userPrincipal.LastPasswordSet.Value.ToString("MM/dd/yyyy HH:mm");
                                html.AppendFormat("<td class='o-ud-datetime'>{0}</td>", value);
                            }
                            catch
                            {
                                html.Append("<td></td>");
                            }

                            // actions...
                            try
                            {
                                String value = (userPrincipal == null) ? String.Empty : GetActionHtml(userPrincipal);
                                html.AppendFormat("<td>{0}</td>", value);
                            }
                            catch
                            {
                                html.Append("<td>{0}</td>");
                            }

                            html.AppendFormat("</tr>");

                            alternate = !alternate;                                                                                        
                        });
                    }
                    catch (Exception ex)
                    {
                        writer.WriteLine(ex.Message);
                        html = new StringBuilder();
                    }
                    finally
                    {
                        writer.WriteLine(html.ToString());
                    }
                }

                writer.WriteLine("</table>");
                writer.WriteLine("</div>");
            }
        }

        internal String BuildCaml()
        {
            String caml = String.Empty;

            String fieldCaml = String.Empty;
            String conditionCaml = String.Empty;
            String valueCaml = String.Empty;

            switch (filterCondition.ToLower())
            {
                case "begins":
                    conditionCaml = "BeginsWith";
                    break;
                case "contains":
                    conditionCaml = "Contains";
                    break;
                case "equals":
                    conditionCaml = "Eq";
                    break;
                default:
                    break;
            }

            switch (filterField.ToLower())
            {
                case "login":
                    if (conditionCaml != "Contains")
                    {
                        conditionCaml = "Contains";
                        fieldCaml = "<FieldRef Name='User_x0020_Login' />";
                        valueCaml = (String.IsNullOrEmpty(filterValue)) ? String.Empty : String.Format("<Value Type='User'>|{0}|{1}</Value>", SPContext.Current.Site.WebApplication.Farm.GetProperty(PropertyBags.PartnerImport_ClaimsProvider), filterValue);
                    }
                    else 
                    {
                        fieldCaml = "<FieldRef Name='User_x0020_Login' />";
                        valueCaml = String.Format("<Value Type='User'>{0}</Value>", filterValue);                        
                    }
                    break;
                case "partner":
                    fieldCaml = "<FieldRef Name='Partner_x0020_Title' />";
                    valueCaml = String.Format("<Value Type='Text'>{0}</Value>", filterValue);
                    break;
                case "id":
                    fieldCaml = "<FieldRef Name='Partner_x0020_ID' />";
                    valueCaml = String.Format("<Value Type='Text'>{0}</Value>", filterValue);
                    break;
                default:
                    break;
            }

            if (value_TextBox.Text == "*")
            {
                caml = "<OrderBy><FieldRef Name='User_x0020_Login' Ascending='True' /></OrderBy>";
            }
            else
            {
                caml = String.Format("<Where><{0}>{1}{2}</{0}></Where><OrderBy><FieldRef Name='User_x0020_Login' Ascending='True' /></OrderBy>", conditionCaml, fieldCaml, valueCaml);
            }

            return caml;
        }

        internal String GetActionHtml(UserPrincipal userPrincipal)
        {
            String html = String.Empty;

            String urlChangeEmail = String.Format("javascript:OpenPopUpPage(&#39;/_layouts/Partner.MiscWebParts/Pages/ChangeEmail.aspx?identity={0}&#39;, UserDirectoryDialogCallback)", userPrincipal.Sid);
            String urlChangePassword = String.Format("javascript:OpenPopUpPage(&#39;/_layouts/Partner.MiscWebParts/Pages/ChangePassword.aspx?identity={0}&#39;, UserDirectoryDialogCallback)", userPrincipal.Sid);
            String urlToggleLock = String.Format("javascript:OpenPopUpPage(&#39;/_layouts/Partner.MiscWebParts/Pages/ToggleLock.aspx?identity={0}&#39;, UserDirectoryDialogCallback)", userPrincipal.Sid);
            String urlToggleEnable = String.Format("javascript:OpenPopUpPage(&#39;/_layouts/Partner.MiscWebParts/Pages/ToggleEnable.aspx?identity={0}&#39;, UserDirectoryDialogCallback)", userPrincipal.Sid);

            html += "<div class='o-ud-action'>";
            html += String.Format("<a href='{0}'><img class='o-ud-thumbnail' src='/_layouts/images/helveticons/20/16x16/message-16x16.png' alt='' /></a>", urlChangeEmail);
            html += String.Format("<a href='{0}'><img class='o-ud-thumbnail' src='/_layouts/images/helveticons/20/16x16/z-axis-rotation-16x16.png' alt='' /></a>", urlChangePassword);
            html += (userPrincipal.IsAccountLockedOut()) ?
                String.Format("<a href='{0}'><img class='o-ud-thumbnail' src='/_layouts/images/helveticons/20/16x16/lock-16x16.png' alt='' /></a>", urlToggleLock) :
                String.Format("<img class='o-ud-thumbnail' src='/_layouts/images/helveticons/60/16x16/unlock-16x16.png' alt='' />");
            html += String.Format("<a href='{0}'><img class='o-ud-thumbnail' src='/_layouts/images/helveticons/20/16x16/{1}-16x16.png' alt='' /></a>",  urlToggleEnable, ((Boolean)userPrincipal.Enabled) ? "checkbox" : "checkbox-empty");
            html += "</div>";

            return html;
        }

        private void applyFilter_Click(Object sender, EventArgs e)
        {
            // apply querystring and refresh page...
            String querystring = String.Format("?field={0}&condition={1}&value={2}", field_DropDownList.SelectedValue, condition_DropDownList.SelectedValue, value_TextBox.Text);
            this.Page.Response.Redirect(this.Page.Request.Url.GetLeftPart(UriPartial.Path) + querystring);
        }

        private void clearFilter_Click(Object sender, EventArgs e)
        {
            // clear querystring and refresh page...
            this.Page.Response.Redirect(this.Page.Request.Url.GetLeftPart(UriPartial.Path));
        }

        internal static String DisplayForm(SPListItem item)
        {
            String displayForm = String.Empty;

            try
            {
                String url = SPContext.Current + "/" + item["FileDirRef"].ToString().Substring(item["FileDirRef"].ToString().IndexOf("#") + 1);
                String rootFolder = (item.ParentList is SPDocumentLibrary) ? item.ParentList.RootFolder.Url + "/Forms" : item.ParentList.RootFolder.Url;
                displayForm = String.Format("javascript:OpenPopUpPage(&#39;{0}/{1}/DispForm.aspx?ID={2}&IsDlg=1&#39;, UserDirectoryDialogCallback)", item.ParentList.ParentWeb.Url, rootFolder, item.ID.ToString());
            }
            catch (Exception ex)
            {
                LogHelper.LogError("Partner.MiscWebParts.PartnerDirectory.DisplayForm Error", ex, 1234);
            }

            return displayForm;
        }

    }
}
