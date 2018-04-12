using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Web;

using Microsoft.SharePoint.Utilities;

namespace TrainingRequest.Forms.CONTROLTEMPLATES.TrainingRequest.Forms
{
    public partial class ImportStyleAndScript : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string currPath = HttpContext.Current.Request.Url.AbsolutePath;
            currPath = VirtualPathUtility.GetFileName(currPath);

            if (!string.IsNullOrEmpty(currPath))
            {
                string cssPath = string.Empty, jsPath = string.Empty,
                    cssTag = "<link rel='stylesheet' type='text/css' href='{0}'/>", jsTag = "<script type='text/javascript' src='{0}'></script>";

                currPath = currPath.ToLower();
                if (currPath.Contains("newrequest"))
                {
                    cssPath = SPUtility.MakeBrowserCacheSafeLayoutsUrl(Constants.path_relStylePath + Constants.path_NewRequestStyle, false);
                    this.Page.Header.Controls.Add(new LiteralControl(string.Format(cssTag, cssPath)));

                    jsPath = SPUtility.MakeBrowserCacheSafeLayoutsUrl(Constants.path_relScriptPath + Constants.path_NewRequestScript, false);
                    this.Page.Header.Controls.Add(new LiteralControl(string.Format(jsTag, jsPath)));

                }
                else if (currPath.Contains("displayrequest"))
                {
                    cssPath = SPUtility.MakeBrowserCacheSafeLayoutsUrl(Constants.path_relStylePath + Constants.path_DisplayRequestStyle, false);
                    this.Page.Header.Controls.Add(new LiteralControl(string.Format(cssTag, cssPath)));

                    jsPath = SPUtility.MakeBrowserCacheSafeLayoutsUrl(Constants.path_relScriptPath + Constants.path_DisplayRequestScript, false);
                    this.Page.Header.Controls.Add(new LiteralControl(string.Format(jsTag, jsPath)));
                }
                else if (currPath.Contains("request"))
                {
                    cssPath = SPUtility.MakeBrowserCacheSafeLayoutsUrl(Constants.path_relStylePath + Constants.path_RequestStyle, false);
                    this.Page.Header.Controls.Add(new LiteralControl(string.Format(cssTag, cssPath)));

                    jsPath = SPUtility.MakeBrowserCacheSafeLayoutsUrl(Constants.path_relScriptPath + Constants.path_RequestScript, false);
                    this.Page.Header.Controls.Add(new LiteralControl(string.Format(jsTag, jsPath)));
                }
                else if (currPath.Contains("assigntaskowners"))
                {
                    cssPath = SPUtility.MakeBrowserCacheSafeLayoutsUrl(Constants.path_relStylePath + Constants.path_AssignOwnerStyle, false);
                    this.Page.Header.Controls.Add(new LiteralControl(string.Format(cssTag, cssPath)));

                    jsPath = SPUtility.MakeBrowserCacheSafeLayoutsUrl(Constants.path_relScriptPath + Constants.path_AssignOwnerScript, false);
                    this.Page.Header.Controls.Add(new LiteralControl(string.Format(jsTag, jsPath)));
                }
                else if (currPath.Contains("assign"))
                {
                    cssPath = SPUtility.MakeBrowserCacheSafeLayoutsUrl(Constants.path_relStylePath + Constants.path_AssignStyle, false);
                    this.Page.Header.Controls.Add(new LiteralControl(string.Format(cssTag, cssPath)));

                    jsPath = SPUtility.MakeBrowserCacheSafeLayoutsUrl(Constants.path_relScriptPath + Constants.path_AssignScript, false);
                    this.Page.Header.Controls.Add(new LiteralControl(string.Format(jsTag, jsPath)));
                }
            }
        }// end pageload
    }
}
