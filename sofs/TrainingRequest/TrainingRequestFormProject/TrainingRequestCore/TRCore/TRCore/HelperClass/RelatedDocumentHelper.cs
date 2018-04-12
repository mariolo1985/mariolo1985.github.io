using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Office.InfoPath;
using System.Xml;
using System.Xml.XPath;


/********************************************************************
 * 
 *  This will handle adding and removing related document paths
 *  
 * ******************************************************************/

namespace TRCore.HelperClass
{
    public static class RelatedDocumentHelper
    {
        /// <summary>
        /// This will take the supplied Document/Network Path and append it to the section
        /// </summary>
        /// <param name="Form"></param>
        public static void SetRelatedDocumentPath(XmlFormHostItem Form)
        {
            XPathNavigator root = Form.MainDataSource.CreateNavigator(), PathNode = default(XPathNavigator);
            XmlNamespaceManager NS = Form.NamespaceManager;


            try
            {
                PathNode = root.SelectSingleNode(Constants.AddRDPath, NS);

                if (!string.IsNullOrEmpty(PathNode.Value))
                {
                    string PathValue = PathNode.Value;

                    List<RepeaterNodeHandler> ChildHolder = new List<RepeaterNodeHandler>();

                    RepeaterNodeHandler RDPathChild = new RepeaterNodeHandler(Constants.RDDisplayNodeName, PathValue);
                    ChildHolder.Add(RDPathChild);

                    // append "file:" header to path if relative path is supplied
                    PathValue = PathValue.Replace(@"\", @"/");
                    PathValue = string.Format(@"file:///{0}", PathValue);



                    RepeaterNodeHandler RDLinkChild = new RepeaterNodeHandler(Constants.RDHyperlinkNodeName, PathValue);
                    ChildHolder.Add(RDLinkChild);

                    RepeaterNodeHandler RDRemoveChild = new RepeaterNodeHandler(Constants.RDRemoveNodeName, "FALSE");
                    ChildHolder.Add(RDRemoveChild);

                    FormHelpers.PopulateRepeatingSectionChilds(Constants.RDParentXPath, "RelatedDocumentGroup", ChildHolder, Form);

                    // Clear the Pathnode after adding
                    PathNode.SetValue(string.Empty);
                }
            }
            catch (Exception Err)
            {
                //LoggingHelper.DoLogFile(string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "SetRelatedDocumentPath", Err.Message.ToString(), Err.StackTrace.ToString()), Form);
                string CustomMsg = string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "SetRelatedDocumentPath", Err.Message.ToString(), Err.StackTrace.ToString());
                LoggingHelper.LogExceptionToSP(CustomMsg, Err);
            }
        }// end SetRelatedDocumentPath

        public static void RemoveSelectedDocPath(XmlFormHostItem Form)
        {

            XPathNavigator root = Form.MainDataSource.CreateNavigator(), RemovePathNode = default(XPathNavigator);
            XmlNamespaceManager NS = Form.NamespaceManager;
            XPathNodeIterator RemovePathGroupNodes = root.Select(Constants.RDGroupXPath, NS);


            try
            {
                if (RemovePathGroupNodes.Count < 1)
                {
                    return;
                }
                else
                {
                    for (int x = RemovePathGroupNodes.Count; x > 0; x--)
                    {
                        RemovePathNode = root.SelectSingleNode(string.Format("{0}[{1}]/my:{2}", Constants.RDGroupXPath, x.ToString(), Constants.RDRemoveNodeName), NS);
                        if (RemovePathNode.Value.Contains("TRUE"))
                        {
                            root.SelectSingleNode(string.Format("{0}[{1}]", Constants.RDGroupXPath, x.ToString()), NS).DeleteSelf();
                        }

                    }
                }
            }
            catch (Exception Err)
            {
                //LoggingHelper.DoLogFile(string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "RemoveSelectedDocPath", Err.Message.ToString(), Err.StackTrace.ToString()), Form);
                string CustomMsg = string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "RemoveSelectedDocPath", Err.Message.ToString(), Err.StackTrace.ToString());
                LoggingHelper.LogExceptionToSP(CustomMsg, Err);
            }

        }// end RemoveSelectedDocPath

    }// end class
}
