using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.SharePoint;
using System.Web.UI.WebControls;
using TrainingRequest.Common.Helpers;

namespace TrainingRequest.Forms.Helpers
{
    public static class ListLookupHelper
    {
        private static string lookupListUrl;
        private static string lookupColumnInternalName;

        public enum newRequestLookups
        {
            RequestType = 1,
            Department
        }

        public static void  SetRequestLookupConfig(newRequestLookups lookup)
        {

            switch (lookup)
            {
                case newRequestLookups.RequestType:
                    lookupListUrl = Constants.newrequest_RequestTypeListUrl;
                    lookupColumnInternalName = Constants.newrequest_RequestTypeColumnName;
                    break;

                case newRequestLookups.Department:
                    lookupListUrl = Constants.newrequest_DeptListUrl;
                    lookupColumnInternalName = Constants.newrequest_DeptColumnName;
                    break;
            }

        }// end setLookupConfiguration

        public static ListItemCollection GetNewRequestLookup(newRequestLookups lookup)
        {
            ListItemCollection lookupCollection = new ListItemCollection();
            SetRequestLookupConfig(lookup);

            try
            {
                string webUrl = SPContext.Current.Web.Url;
                if (!string.IsNullOrEmpty(webUrl))
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        using (SPSite TRsite = new SPSite(webUrl))
                        using (SPWeb TRweb = TRsite.OpenWeb())
                        {
                            SPList lookupList = default(SPList);
                            try
                            {
                                lookupList = TRweb.GetList(lookupListUrl);
                            }
                            catch { }

                            if ((lookupList != null) && (lookupList.Items.Count > 0))
                            {
                                SPQuery sortQuery = new SPQuery();
                                sortQuery.Query = string.Format("<OrderBy><FieldRef Name='{0}' Ascending='True'/></OrderBy>", lookupColumnInternalName);
                                sortQuery.RowLimit = 1000;

                                SPListItemCollection sortedResult = default(SPListItemCollection);
                                do
                                {
                                    sortedResult = lookupList.GetItems(sortQuery);

                                    foreach (SPListItem item in sortedResult)
                                    {
                                        string value = string.Empty;
                                        if (!string.IsNullOrEmpty(item[lookupColumnInternalName].ToString()))
                                        {
                                            value = item[lookupColumnInternalName].ToString();
                                            lookupCollection.Add(new ListItem(value, value));
                                        }
                                    }

                                    sortQuery.ListItemCollectionPosition = sortedResult.ListItemCollectionPosition;
                                } while (sortQuery.ListItemCollectionPosition != null);

                            }
                        }// end using
                    });
                }
            }
            catch (Exception err)
            {
                LogHelper.LogErrorMessage("ListLookupHelper.GetNewRequestLookup()", err.Message, err);
            }

            return lookupCollection;
        }// end GetNewLookup
    }// end class
}
