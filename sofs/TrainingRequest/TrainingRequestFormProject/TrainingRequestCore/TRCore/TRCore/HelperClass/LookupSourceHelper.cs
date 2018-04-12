using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Xml.XPath;
using Microsoft.Office.InfoPath;
using Microsoft.SharePoint;


/*************************************************************************
 * 
 *      THIS WILL HANDLE GETTING AND CLEARING DROP DOWN LIST SOURCES
 *
 * ***********************************************************************/

namespace TRCore.HelperClass
{
    public static class LookupSourceHelper
    {
        //// Request Type handler
        //private static string RequestTypeListURL = "/lists/RequestTypes";
        //private static string RequestTypeSourceParentXPath = "/my:myFields/my:FieldSouce/my:RequestTypeSection";
        //private static string RequestTypeGroupNodeName = "RequestTypeGroup";
        //private static string RequestTypeValueNodeName = "RequestTypeValue";
        //private static string RequestTypeValueColumnName = "Title";


        //// Department Type handler
        //private static string DepartmentListURL = "/lists/Departments";
        //private static string DepartmentTypeSourceParentXPath = "/my:myFields/my:FieldSouce/my:DepartmentSection";
        //private static string DepartmentGroupNodeName = "DepartmentGroup";
        //private static string DepartmentValueNodeName = "DepartmentValue";
        //private static string DepartmentValueColumnName = "Title";


        public enum LookupTypes
        {
            RequestType = 1,
            DepartmentType
        }


        /// <summary>
        /// Trigger to start building lookup source
        /// </summary>
        public static void StartBuildingLookupSource(XmlFormHostItem Form, string WebURL)
        {
            ClearLookupSource(Form);
            foreach (LookupTypes LookupType in Enum.GetValues(typeof(LookupTypes)))
            {
                SetLookupSource(LookupType, Form, WebURL);
            }
        }// end StartBuildingLookupSource

        /// <summary>
        /// This will build the lookup passed in the parameter
        /// </summary>
        /// <param name="LookupToBuild"></param>
        public static void SetLookupSource(LookupTypes LookupToBuild, XmlFormHostItem Form, string WebURL)
        {
            try
            {

                using (SPSite TRSite = new SPSite(WebURL))
                {
                    using (SPWeb TRWeb = TRSite.OpenWeb())
                    {

                        try
                        {

                            // Set lookup helper values
                            string LookupListURL = string.Empty, LookupParentXPath = string.Empty, LookupGroupPathName = string.Empty, LookupXNodeName = string.Empty, LookupListColumnInternalName = string.Empty;

                            switch (LookupToBuild)
                            {
                                case LookupTypes.RequestType:
                                    LookupListURL = Constants.RequestTypeListURL;
                                    LookupParentXPath = Constants.RequestTypeSourceParentXPath;
                                    LookupGroupPathName = Constants.RequestTypeGroupNodeName;
                                    LookupXNodeName = Constants.RequestTypeValueNodeName;
                                    LookupListColumnInternalName = Constants.RequestTypeValueColumnName;
                                    break;

                                case LookupTypes.DepartmentType:
                                    LookupListURL = Constants.DepartmentListURL;
                                    LookupParentXPath = Constants.DepartmentTypeSourceParentXPath;
                                    LookupGroupPathName = Constants.DepartmentGroupNodeName;
                                    LookupXNodeName = Constants.DepartmentValueNodeName;
                                    LookupListColumnInternalName = Constants.DepartmentValueColumnName;
                                    break;
                            }

                            // Get lookup list
                            SPList LookupList = default(SPList);
                            try
                            {
                                LookupList = TRWeb.GetList(WebURL + LookupListURL);
                            }
                            catch { }

                            if (LookupList != null)
                            {
                                // Query items
                                SPQuery LookupItemQuery = new SPQuery();
                                LookupItemQuery.Query = string.Format("<OrderBy><FieldRef Name='{0}' Ascending='True'/></OrderBy>", LookupListColumnInternalName);
                                LookupItemQuery.RowLimit = 500;
                                LookupItemQuery.ViewAttributes = "Scope='RecusiveAll'";

                                SPListItemCollection LookupResults = default(SPListItemCollection);
                                do
                                {
                                    LookupResults = LookupList.GetItems(LookupItemQuery);

                                    foreach (SPListItem LookupItem in LookupResults)
                                    {
                                        // Get column value
                                        if (!string.IsNullOrEmpty(LookupItem[LookupListColumnInternalName].ToString()))
                                        {
                                            string LookupValue = LookupItem[LookupListColumnInternalName].ToString();


                                            // Set value in drop down source
                                            List<RepeaterNodeHandler> ChildHolder = new List<RepeaterNodeHandler>();

                                            RepeaterNodeHandler LookupValueNode = new RepeaterNodeHandler(LookupXNodeName, LookupValue);
                                            ChildHolder.Add(LookupValueNode);

                                            FormHelpers.PopulateRepeatingSectionChilds(LookupParentXPath, LookupGroupPathName, ChildHolder, Form);

                                        }
                                    }

                                    LookupItemQuery.ListItemCollectionPosition = LookupResults.ListItemCollectionPosition;
                                } while (LookupItemQuery.ListItemCollectionPosition != null);
                            }

                        }
                        catch (Exception err)
                        {
                            //LoggingHelper.DoLogFile(string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "SetLookupSource", err.Message.ToString(), err.StackTrace.ToString()), Form);
                            string CustomMsg = string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "SetLookupSource", err.Message.ToString(), err.StackTrace.ToString());
                            LoggingHelper.LogExceptionToSP(CustomMsg, err);
                        }


                    }// dispose TRWeb
                }// dispose TRSite

            }
            catch { }
        }// End SetLookupSource


        public static void ClearLookupSource(XmlFormHostItem Form)
        {

            try
            {
                // Loop avaiable lookup
                foreach (LookupTypes LookupType in Enum.GetValues(typeof(LookupTypes)))
                {
                    // Set lookup helper values
                    string LookupListURL = string.Empty, LookupParentXPath = string.Empty, LookupGroupPathName = string.Empty, LookupXNodeName = string.Empty;

                    switch (LookupType)
                    {
                        case LookupTypes.RequestType:
                            LookupParentXPath = Constants.RequestTypeSourceParentXPath;
                            LookupGroupPathName = Constants.RequestTypeGroupNodeName;
                            LookupXNodeName = Constants.RequestTypeValueNodeName;

                            break;

                        case LookupTypes.DepartmentType:
                            LookupParentXPath = Constants.DepartmentTypeSourceParentXPath;
                            LookupGroupPathName = Constants.DepartmentGroupNodeName;
                            LookupXNodeName = Constants.DepartmentValueNodeName;

                            break;
                    }

                    // Clear repeating node
                    FormHelpers.sbClearSource(string.Format("{0}/my:{1}", LookupParentXPath, LookupGroupPathName), Form, true);

                }
            }
            catch (Exception err)
            {
                string CustomMsg = string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "ClearLookupSource", err.Message.ToString(), err.StackTrace.ToString());
                LoggingHelper.LogExceptionToSP(CustomMsg, err);
            }


        }// end clearlookupsource

    }// end class
}
