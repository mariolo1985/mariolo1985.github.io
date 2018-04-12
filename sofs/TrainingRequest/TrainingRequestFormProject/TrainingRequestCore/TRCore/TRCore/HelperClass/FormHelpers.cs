using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Xml.XPath;
using System.Collections;
using Microsoft.Office.InfoPath;

/******************************************************************************
 * 
 *      This will handle functions that reads and changes the form
 *      
 * ****************************************************************************/

namespace TRCore.HelperClass
{
    public static class FormHelpers
    {

        #region Manipulate form XML
        /// <summary>
        /// The allows removing repeating nodes by range of [ALL] || [SECOND:LAST].
        /// </summary>
        /// <param name="sourceXpath">XPath to the repeater</param>
        /// <param name="Form"></param>
        /// <param name="RemoveFirstNode">True = remove all repeater nodes</param>
        public static void sbClearSource(string sourceXpath, XmlFormHostItem Form, bool RemoveFirstNode)
        {
            XPathNavigator root = Form.MainDataSource.CreateNavigator(), sourceFirstNode = default(XPathNavigator),
            sourceSecondNode = default(XPathNavigator), sourceLastNode = default(XPathNavigator);
            XmlNamespaceManager NamespaceManager = Form.NamespaceManager;

            try
            {
                sourceFirstNode = root.SelectSingleNode(sourceXpath + "[1]", NamespaceManager);
                sourceSecondNode = root.SelectSingleNode(sourceXpath + "[2]", NamespaceManager);
                sourceLastNode = root.SelectSingleNode(sourceXpath + "[last()]", NamespaceManager);


                if (RemoveFirstNode)
                {
                    if ((sourceFirstNode != null) && (sourceLastNode != null))
                    {
                        sourceFirstNode.DeleteRange(sourceLastNode);
                    }
                }
                else
                {
                    if ((sourceSecondNode != null) && (sourceLastNode != null))
                    {
                        //sourceFirstNode.SetValue(string.Empty);
                        sourceSecondNode.DeleteRange(sourceLastNode);
                    }
                }
            }
            catch (Exception Err)
            {
                //LoggingHelper.DoLogFile(string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "sbClearSource", Err.Message.ToString(), Err.StackTrace.ToString()), Form);
                string CustomMsg = string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "sbClearSource", Err.Message.ToString(), Err.StackTrace.ToString());
                LoggingHelper.LogExceptionToSP(CustomMsg, Err);
            }
        }// end sbClearSOurce

        /// <summary>
        /// Empties the first child and delete the rest
        /// </summary>
        /// <param name="sourceXPath"></param>
        public static void sbClearSource(string sourceXPath, string node1name, XmlFormHostItem Form)
        {
            XPathNavigator root = Form.MainDataSource.CreateNavigator(), rowNode = default(XPathNavigator);
            XmlNamespaceManager NamespaceManager = Form.NamespaceManager;
            XPathNodeIterator sourceNodes = root.Select(sourceXPath, NamespaceManager);
            int rowCount = 0;

            rowCount = sourceNodes.Count;

            // Empty first row
            // Remove the rest
            for (int x = rowCount; x > 0; x--)
            {
                rowNode = root.SelectSingleNode(sourceXPath + "[" + x + "]", NamespaceManager);

                if (x == 1)
                {
                    rowNode.SelectSingleNode("./my:" + node1name, NamespaceManager).SetValue(string.Empty);

                }
                else
                {
                    rowNode.DeleteSelf();
                }
            }

        }// End sbClearSource

        /// <summary>
        /// This will build child nodes under a repeating group in our maindatasource.  This builds 1 row of child(s) nodes.
        /// </summary>
        /// <param name="ParentXpath">Full xpath to the parent group</param>
        /// <param name="RepeaterNodeName">The name of repeating group</param>
        /// <param name="ListOfChildNodes">A list of ND_RepeaterNodeHandler to parse for our child nodes</param>
        public static void PopulateRepeatingSectionChilds(string ParentXpath, string RepeaterNodeName, List<RepeaterNodeHandler> ListOfChildNodes, XmlFormHostItem Form)
        {
            try
            {
                // Make sure we have what we need to build our section
                if ((!string.IsNullOrEmpty(ParentXpath)) && (!string.IsNullOrEmpty(RepeaterNodeName)) && (ListOfChildNodes.Count > 0))
                {

                    XPathNavigator Root = Form.CreateNavigator(), ParentNode = default(XPathNavigator);
                    XmlNamespaceManager NS = Form.NamespaceManager;

                    ParentNode = Root.SelectSingleNode(ParentXpath, NS);
                    if (ParentNode != null)
                    {



                        XmlDocument BuilderXML = new XmlDocument();
                        XmlNode RepeaterNode = default(XmlNode), TempNode = default(XmlNode);
                        RepeaterNode = BuilderXML.CreateElement(string.Format("my:{0}", RepeaterNodeName), NS.LookupNamespace("my"));

                        foreach (RepeaterNodeHandler ChildNodeValues in ListOfChildNodes)
                        {
                            string ChildNodeName = string.Empty, ChildNodeVal = string.Empty;
                            ChildNodeName = ChildNodeValues.NodeName;
                            ChildNodeVal = ChildNodeValues.NodeVal;


                            if ((!string.IsNullOrEmpty(ChildNodeName)) && (!string.IsNullOrEmpty(ChildNodeVal)))
                            {
                                TempNode = BuilderXML.CreateElement(string.Format("my:{0}", ChildNodeName), NS.LookupNamespace("my"));

                                TempNode.InnerText = ChildNodeVal;
                                RepeaterNode.AppendChild(TempNode);
                            }
                        }// End foreach

                        // Add Repeater Group to our XMLDoc
                        BuilderXML.AppendChild(RepeaterNode);
                        ParentNode.AppendChild(BuilderXML.DocumentElement.CreateNavigator());
                    }
                }
            }
            catch (Exception err)
            {
                //LoggingHelper.DoLogFile(string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "PopulateRepeatingSectionChilds", err.Message.ToString(), err.StackTrace.ToString()), Form);
                string CustomMsg = string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "PopulateRepeatingSectionChilds", err.Message.ToString(), err.StackTrace.ToString());
                LoggingHelper.LogExceptionToSP(CustomMsg, err);

            }
        }// end SetRepeatingSectionChilds

        /// <summary>
        /// Removes nil attribute
        /// </summary>
        /// <param name="node"></param>
        public static void DeleteNil(XPathNavigator node)
        {
            if (node.MoveToAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance"))
                node.DeleteSelf();
        } // End DeleteNil
        #endregion

        #region Handle Form Validations
        /// <summary>
        /// This will check to see if declared required fields are all populated. If not, than throw a message and return false to direct to user message view.
        /// </summary>
        /// <param name="Form"></param>
        /// <returns>True: All required fields are populated.  False: All or some required fields are empty.</returns>
        public static Boolean ValidateRequiredFields(XmlFormHostItem Form)
        {
            Hashtable RequiredFieldsTbl = new Hashtable();
            RequiredFieldsTbl.Add(Constants.RequestTitleXPath, "Request Title");
            RequiredFieldsTbl.Add(Constants.RequestTypeXPath, "Request Type");
            RequiredFieldsTbl.Add(Constants.DeptXPath, "Department");

            Boolean ValidReqFields = true;

            try
            {

                XPathNavigator root = Form.MainDataSource.CreateNavigator(), RequiredFieldNode = default(XPathNavigator);
                XmlNamespaceManager NS = Form.NamespaceManager;

                foreach (string ReqKey in RequiredFieldsTbl.Keys)
                {
                    // Check each key[XPath] for empty value.  If empty than throw key value to user message.
                    RequiredFieldNode = default(XPathNavigator); // Make sure this is 'refresh' on each check
                    RequiredFieldNode = root.SelectSingleNode(ReqKey, NS); // The required field

                    if (string.IsNullOrEmpty(RequiredFieldNode.Value))
                    {
                        // only throw required message if it is empty and set ValidReqField Flag back to the form
                        ValidReqFields = false;
                        UserMessage.SetUserMessage(Form, string.Concat(RequiredFieldsTbl[ReqKey].ToString(), " is not populated."));
                    }
                }
            }
            catch (Exception Err)
            {
                //LoggingHelper.DoLogFile(string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "ValidateRequiredFields", Err.Message.ToString(), Err.StackTrace.ToString()), Form);
                string CustomMsg = string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "ValidateRequiredFields", Err.Message.ToString(), Err.StackTrace.ToString());
                LoggingHelper.LogExceptionToSP(CustomMsg, Err);
            }


            return ValidReqFields;

        }// End ValidateRequiredFields
        #endregion

        #region Form field Getters and Setters
        public static void SetPreviousViewName(XmlFormHostItem Form, string PreviousViewName)
        {
            XPathNavigator root = Form.MainDataSource.CreateNavigator(), PreviousViewNode = default(XPathNavigator);
            XmlNamespaceManager NS = Form.NamespaceManager;

            try
            {
                PreviousViewNode = root.SelectSingleNode(Constants.PreviousViewXPath, NS);
                PreviousViewNode.SetValue(PreviousViewName);

            }
            catch { }

        }// end setpreviousviewnode 

        public static string GetPreviousViewName(XmlFormHostItem Form)
        {
            string PreviousViewName = string.Empty;
            XPathNavigator root = Form.MainDataSource.CreateNavigator(), PreviousViewNode = default(XPathNavigator);
            XmlNamespaceManager NS = Form.NamespaceManager;

            try
            {
                PreviousViewNode = root.SelectSingleNode(Constants.PreviousViewXPath, NS);
                if (!string.IsNullOrEmpty(PreviousViewNode.Value))
                {
                    PreviousViewName = PreviousViewNode.Value;
                }

            }
            catch { }

            return PreviousViewName;

        }// end setpreviousviewnode 

        #endregion

    }
}
