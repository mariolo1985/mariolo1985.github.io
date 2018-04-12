using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Office.InfoPath;
using System.Xml;
using System.Xml.XPath;
using TRCore.HelperClass;


namespace TrainingRequestXSN
{
    public partial class SubmitHelper
    {

        public static Boolean ExecuteSubmit(XmlFormHostItem Form)
        {
            Boolean SubmitSuccessful = true;
            XPathNavigator root = Form.MainDataSource.CreateNavigator(), FormNameNode = default(XPathNavigator);
            XmlNamespaceManager NS = Form.NamespaceManager;

            try
            {
                string FormName = string.Empty, SaveLocation = string.Empty;

                // Get form name
                FormNameNode = root.SelectSingleNode("/my:myFields/my:SystemInternal/my:FormName", NS);
                if (string.IsNullOrEmpty(FormNameNode.Value))
                {// FormName is not generated so build it
                    FormName = GenerateFormName(root, NS);
                    if (!string.IsNullOrEmpty(FormName))
                    {
                        // Set formname node
                        FormNameNode.SetValue(FormName);
                    }
                }
                else
                {
                    // Form name has been set previously
                    FormName = FormNameNode.Value;
                }

                // Get Form library to save to
                SaveLocation = FormVariable.FormLibURL;
                if (string.IsNullOrEmpty(SaveLocation))
                {
                    // than fall back to savelocation node
                    XPathNavigator SaveLocationNode = root.SelectSingleNode("/my:myFields/my:SystemInternal/my:SaveLocation", NS);
                    SaveLocation = SaveLocationNode.Value;
                }

                FileSubmitConnection TRSubmit = (FileSubmitConnection)Form.DataConnections["SubmitConnection"];
                TRSubmit.FolderUrl = SaveLocation;
                TRSubmit.Filename.SetStringValue(FormName);
                TRSubmit.Execute();

                UserMessage.SetUserMessage(Form, "Your request was submitted successfully.");
            }
            catch
            {
                UserMessage.SetUserMessage(Form, "Error occurred during a submit/save.  Please submit a ticket for the Enterprise team.");
                SubmitSuccessful = false;
            }

            return SubmitSuccessful;
        }// End ExecuteSubmit

        /// <summary>
        /// This will take the request title and request timestamp to create a filename 
        /// </summary>
        /// <param name="root">The form's xpathnavigator</param>
        /// <param name="NS">The form's namespacemanager</param>
        /// <returns>A filename from the form metadata</returns>
        private static string GenerateFormName(XPathNavigator root, XmlNamespaceManager NS)
        {
            string Filename = string.Empty;

            XPathNavigator RequestTitleNode = root.SelectSingleNode("/my:myFields/my:Core/my:RequestTitle", NS);

            try
            {
                if (!string.IsNullOrEmpty(RequestTitleNode.Value))
                {
                    string RequestTitle = RequestTitleNode.Value;

                    DateTime CurrentStamp = DateTime.Now;
                    string RequestStamp = string.Format("_{0}_{1}", CurrentStamp.ToString("MMddyyyy"), CurrentStamp.ToString("Hmmss"));

                    Filename = string.Concat(RequestTitle, RequestStamp);

                }
            }
            catch { }

            return Filename;

        }// end GenerateFormName
    }
}
