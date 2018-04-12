using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.SharePoint;
using Microsoft.Office.InfoPath;

using System.Xml;
using System.Xml.XPath;

using System.IO;



namespace TRCore
{
    class CommonHelper
    {
        // ************************************************** Class Variables **************************************************

        private static string _LogDirectory = @"C:\Debug\Logs";
        private static string _LogFilename = @"TrainingRequest";

        // ************************************************** Class Methods **************************************************
        /******************************************************************************************************
         *
         *                                      SP Helpers         
         * 
         ******************************************************************************************************/

        /// <summary>
        /// Gets an SPList object from the web via listname
        /// </summary>
        /// <param name="ListRelURL">Enter the URL instance of the List. IE: WorkingDocumentsLibrary</param>
        /// <param name="ListWeb">Web object that can connect to the list.  Elevated Web is optional and used if list has restrictive permissions.</param>
        /// <returns></returns>
        public static SPList fnGetSPList(string ListRelURL, SPWeb ElevatedListWeb)
        {
            SPList ReturnedList = default(SPList);

            try
            {
                ReturnedList = ElevatedListWeb.GetList(ElevatedListWeb.Url.ToString() + "/lists/" + ListRelURL);
            }
            catch (Exception Err)
            {
                fnDoLogFile("fnGetSpList: " + Err.Message.ToString() + string.Format("[Site: {0}", ListRelURL));
            }


            return ReturnedList;

        }// end fnGetSPList

        /// <summary>
        /// Gets an SPList object from the web via listname
        /// </summary>
        /// <param name="ListRelURL">Enter the URL instance of the List. IE: WorkingDocumentsLibrary</param>
        /// <param name="ListWeb">Web object that can connect to the list.  Elevated Web is optional and used if list has restrictive permissions.</param>
        /// <returns></returns>
        public static SPList fnGetSPLibrary(string LibraryRelURL, SPWeb ElevatedListWeb)
        {
            SPList ReturnedList = default(SPList);

            try
            {
                ReturnedList = ElevatedListWeb.GetList(ElevatedListWeb.Url.ToString() + "/" + LibraryRelURL);
            }
            catch (Exception Err)
            {
                fnDoLogFile("fnGetSPLibrary: " + Err.Message.ToString() + string.Format("[Site: {0}", LibraryRelURL));
            }


            return ReturnedList;

        }// end fnGetSPList

        /// <summary>
        /// Returns an SPUser
        /// </summary>
        /// <param name="MyWeb"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static SPUser GetSPUser(SPWeb MyWeb, string UserId)
        {
            SPUser MyUser = default(SPUser);

            try
            {
                MyUser = MyWeb.EnsureUser(UserId);
            }
            catch
            {
                // eat it
            }

            return MyUser;
        }

        /******************************************************************************************************
         *
         *                                      QUERY Helpers         
         * 
         ******************************************************************************************************/


        /// <summary>
        /// Contains query
        /// </summary>
        public static string fnGetContainsCAMLStr(string fieldName, string fieldType, string queryVal)
        {
            string queryStr = string.Empty;

            queryStr = String.Format("<Where><Contains><FieldRef Name='{0}'/><Value Type='{1}'>{2}</Value></Contains></Where>", fieldName, fieldType, queryVal);

            return queryStr;
        }// end fnGetCAMLStr

        /// <summary>
        /// Equal-to query
        /// </summary>
        public static string fnGetEqCAMLStr(string fieldName, string fieldType, string queryVal)
        {
            string queryStr = string.Empty;

            queryStr = String.Format("<Where><Eq><FieldRef Name='{0}'/><Value Type='{1}'>{2}</Value></Eq></Where>", fieldName, fieldType, queryVal);

            return queryStr;
        }// end fnGetCAMStrL

        public static string fnGetAttachmentCAMLStr(string FormSourceReference, string AttachmentSeq)
        {

            string queryStr = string.Empty;


            queryStr = string.Format("<Where><And><Eq><FieldRef Name='FormSourceReference' /><Value Type='Text'>{0}</Value></Eq><Eq><FieldRef Name='AttachmentSeq' /><Value Type='Text'>{1}</Value></Eq></And></Where>",
                FormSourceReference, AttachmentSeq);


            return queryStr;

        }// End fnGetAttachmetnCAMLStr


        /******************************************************************************************************
       *
       *                                      Debug Helpers         
       * 
       ******************************************************************************************************/

        /// <summary>
        /// This will write out to a text file per line in [DateTime - Message] format
        /// </summary>
        /// <param name="directoryPath">The path to the text file's directory without ending "\"</param>
        /// <param name="filename">Name of the text file wihtout extension</param>
        /// <param name="message">string message</param>
        public static void fnDoLogFile(string message)
        {// This should log to the SP server on C:\NextDocs\Logs

            // ---------------------------
            // For Debugging purposes only
            // ---------------------------

            try
            {

                if (!Directory.Exists(_LogDirectory))
                {
                    //if log is not created
                    Directory.CreateDirectory(_LogDirectory);

                }

                string filepath = string.Format(@"{0}\{1}.txt", _LogDirectory, _LogFilename);
                if (!File.Exists(filepath))
                {
                    Stream NewFile = File.Create(filepath);
                    NewFile.Close();
                }

                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(DateTime.Now.ToString() + " - " + message.ToString());
                    sw.Flush();
                }
            }
            catch { }

        }// end fnDoLog

        /******************************************************************************************************
         *
         *                                      Form Helpers         
         * 
         ******************************************************************************************************/

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
            catch { }
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
        public static void SetRepeatingSectionChilds(string ParentXpath, string RepeaterNodeName, List<RepeaterNodeHandler> ListOfChildNodes, XmlFormHostItem Form)
        {
            try
            {
                // Make sure we have what we need to build our section
                if ((!string.IsNullOrEmpty(ParentXpath)) && (!string.IsNullOrEmpty(RepeaterNodeName)) && (ListOfChildNodes.Count > 0))
                {

                    XPathNavigator Root = Form.CreateNavigator(), ParentNode = default(XPathNavigator);
                    XmlNamespaceManager NS = Form.NamespaceManager;

                    ParentNode = Root.SelectSingleNode(ParentXpath, NS);
                    if (ParentNode == null)
                    {
                        fnDoLogFile("ParentNode == null");
                    }

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
            catch (Exception err)
            {
                fnDoLogFile(string.Format("SetRepeatingSectionChilds: {0}", err.Message.ToString()));
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
        } // End DeleteNIl
    }
}
