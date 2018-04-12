using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SharePoint;


namespace TrainingRequestXSN
{
    public partial class FormVariable
    {
        private static string _WebURL;
        private static string _FormLibURL;

        public static string TRWebURL
        {
            get { return _WebURL; }
            set { _WebURL = value; }
        }// end TRWebURL

        public static string FormLibURL
        {
            get { return _FormLibURL; }
            set { _FormLibURL = value; }
        }// end FormLibUrl


        public static void SetFormVariable()
        {
            string WebURL = string.Empty, LibURL = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(SPContext.Current.Web.Url.ToString()))
                {
                    WebURL = SPContext.Current.Web.Url.ToString();
                }

                if (!string.IsNullOrEmpty(WebURL))
                {
                    LibURL = WebURL + "/trainingrequestform";
                }

            }
            catch { }

            TRWebURL = WebURL;
            FormLibURL = LibURL;

        }// end FormVariable
    }
}
