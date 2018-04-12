using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Px.Common;

namespace Px.SelfService.WebParts.ContactRepresentative
{
    [ToolboxItemAttribute(false)]
    public partial class ContactRepresentative : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public ContactRepresentative()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ChromeType = PartChromeType.None;
            }
            catch (Exception ex)
            {
                LogHelper.LogMessage(SPContext.Current.Site, "ContactRepresentative", ex.Message, ex.StackTrace, "WebPart", "Error");
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //...
        }

    }
}
