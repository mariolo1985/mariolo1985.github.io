using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Publishing;
using Microsoft.SharePoint.Publishing.Fields;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using Oasis.Common;

namespace Partner.MiscWebParts.Announcements
{
    [ToolboxItemAttribute(false)]
    public class Announcements : WebPart
    {
        protected override void OnLoad(EventArgs e)
        {
            // this.ChromeType = PartChromeType.None;
            String cssHref = SPUtility.MakeBrowserCacheSafeLayoutsUrl("Partner.MiscWebParts/CSS/o-announcements.css", false);
            this.Page.Header.Controls.Add(new LiteralControl(String.Format("<link rel='stylesheet' type='text/css' href='{0}'/>", cssHref)));
            // String jsSrc = SPUtility.MakeBrowserCacheSafeLayoutsUrl("Partner.MiscWebParts/Scripts/Announcements.js", false);
            // this.Page.Header.Controls.Add(new LiteralControl(String.Format("<script type='text/javascript' src='{0}'></script>", jsSrc)));
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            try
            {
                using (SPSite site = new SPSite(Page.Request.Url.ToString()))
                {
                    UInt32 count = 6;
                    writer.WriteLine("<div class='o-announcements'>");

                    SPList list = site.RootWeb.Lists["Announcements"];
                    SPQuery query = new SPQuery();
                    query.Query = String.Format(
                        "<Where><And><Eq><FieldRef Name='ContentType' /><Value Type='Text'>Partner Announcement</Value></Eq><Or><IsNull><FieldRef Name='Expires' /></IsNull><Geq><FieldRef Name='Expires' /><Value IncludeTimeValue='TRUE' Type='DateTime'>{0}</Value></Geq></Or></And></Where><OrderBy><FieldRef Ascending='FALSE' Name='{1}' /></OrderBy>",
                        SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Today),
                        Constants.Column_Created);
                    query.RowLimit = count;
                    SPListItemCollection collection = list.GetItems(query);
                    
                    if (collection.Count > 0)
                    {
                        foreach (SPListItem item in collection)
                        {
                            writer.WriteLine("<div class='o-anc-post'><div class='o-anc-thumb'>{0}</div><div class='o-anc-text'>{1}<p>{2}</p></div></div>",
                                GetThumbnail(item),
                                GetHeading(item),
                                HtmlToSummaryText(GetBody(item), 80));
                        }
                    }

                    writer.WriteLine("</div>");               
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError("Partner.MiscWebParts.Announcements.RenderContents Error", ex, 1234);
            }
        }

        private String GetHref(SPListItem item)
        {
            String href = String.Empty;
            try
            {
                href = DisplayDialog(item);
            }
            catch (Exception ex)
            {
                LogHelper.LogError("Partner.MiscWebParts.Announcements.GetHref Error", ex, 1234);
            }
            return href;
        }

        private String GetThumbnail(SPListItem item)
        {
            String href = GetHref(item);
            String thumbnail = String.Format("<a href='{0}'><img src='/_layouts/Partner.MiscWebParts/Images/default.png' height='24' width='24' alt='' /></a>", href);
            try
            {
                String imageUrl = String.Format("/_layouts/images/helveticons/60/24x24/{0}-24x24.png", item["Announcement Thumbnail"].ToString());
                thumbnail = String.Format("<a href='{0}'><img src='{1}' height='24' width='24' alt='' /></a>", GetHref(item), imageUrl);
            }
            catch (Exception)
            {
                thumbnail = String.Format("<a href='{0}'><img src='/_layouts/Partner.MiscWebParts/Images/default.png' height='24' width='24' alt='' /></a>", href);
            }
            return thumbnail;
        }

        private String GetHeading(SPListItem item)
        {
            String heading = String.Empty;
            try
            {
                heading = String.Format("<a href='{0}'><h3>{1}</h3></a>", GetHref(item), item.Title);
            }
            catch (Exception ex)
            {
                LogHelper.LogError("Partner.MiscWebParts.Announcements.GetHeading Error", ex, 1234);
            }
            return heading;
        }

        private String GetBody(SPListItem item)
        {
            String body = String.Empty;
            try
            {
                body = String.Format("<p>{0}</p>", item.GetFormattedValue("Body"));
            }
            catch (Exception ex)
            {
                LogHelper.LogError("Partner.MiscWebParts.Announcements.GetBody Error", ex, 1234);
            }
            return body;
        }

        private String DisplayDialog(SPListItem item)
        {
            return String.Format(@"javascript:OpenPopUpPage(""{0}/{1}/DispForm.aspx?IsDlg=1&ID={2}"")",
                item.ParentList.ParentWeb.Url,
                (item.ParentList is SPDocumentLibrary) ? item.ParentList.RootFolder.Url + "/Forms" : item.ParentList.RootFolder.Url,
                item.ID);
        }

        private String HtmlToSummaryText(String html, Int32 max)
        {
            String result = String.Empty;
            try
            {
                Boolean tag = false;
                for (int i = 0; i < html.Length; i++)
                {
                    if (result.Length < max)
                    {
                        if (html.Substring(i, 1) == "<")
                        {
                            tag = true;
                        }
                        else if (html.Substring(i, 1) == ">")
                        {
                            tag = false;
                        }
                        else
                        {
                            if (!tag)
                            {
                                result = result + html.Substring(i, 1);
                            }
                        }
                    }
                    else
                    {
                        result = result + "...";
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError("Partner.MiscWebParts.Announcements.HtmlToSummaryText Error", ex, 1234);
            }
            return result;
        }
    
    }
}
