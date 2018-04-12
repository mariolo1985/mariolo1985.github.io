using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Px.Common;

namespace Px.SelfService.WebParts.AccountCreated
{
    [ToolboxItemAttribute(false)]
    public partial class AccountCreated : WebPart
    {
        public AccountCreated()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //var user = (!String.IsNullOrEmpty(this.Page.Request.QueryString["user"])) ? this.Page.Request.QueryString["user"] : String.Empty;
            var error = (!String.IsNullOrEmpty(this.Page.Request.QueryString["error"])) ? this.Page.Request.QueryString["error"] : String.Empty;

            ChromeType = PartChromeType.None;

            try
            {
                if (!String.IsNullOrEmpty(error))
                {
                    ltr_html.Text +=
                        "<div class='pxSubTitle'>Error</div>" +
                        "<span>We were unable to create a new supplier profile. Please contact 1-800-XXX-XXXX to resolve this issue.</span>";
                }
                else
                {
                    ltr_html.Text +=
                        "<div class='pxSubTitle'>Your Account has been Created</div>" +
                        "<span>Thank you for joining Supplier Oasis Fulfillment Services. Please check your email inbox for the welcome material.</span>";
                    // ltr_html.Text += "<p><a href='/_layouts/15/Authenticate.aspx?Source=%2F'>Sign In</a></p>";
                }

            }
            catch (Exception ex)
            {
                ltr_html.Text += "<p>Error: " + ex.Message + "</p>";
            }
        }
    }
}
