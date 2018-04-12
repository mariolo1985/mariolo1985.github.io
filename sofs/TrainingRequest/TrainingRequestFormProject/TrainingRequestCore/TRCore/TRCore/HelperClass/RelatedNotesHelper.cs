using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.SharePoint;
using TRCore.HelperClass;

using System.Xml;
using System.Xml.XPath;
using Microsoft.Office.InfoPath;

/***************************************************************************
 *
 *      This is handles setting and clearing notes (history) field
 *      
 * *************************************************************************/

namespace TRCore.HelperClass
{
    public static class RelatedNotesHelper
    {
        public static void SetNotesHistory(XmlFormHostItem Form)
        {
            XPathNavigator root = Form.MainDataSource.CreateNavigator(), NotesNode = default(XPathNavigator), NotesHistoryNode = default(XPathNavigator);
            XmlNamespaceManager NS = Form.NamespaceManager;

            try
            {
                NotesNode = root.SelectSingleNode(Constants.NotesXPath, NS);
                if (!string.IsNullOrEmpty(NotesNode.Value))
                {
                    string timeStamp = string.Empty, CurrentUser = string.Empty;
                    NotesHistoryNode = root.SelectSingleNode(Constants.NotesHistoryXPath, NS);

                    CurrentUser = SPContext.Current.Web.CurrentUser.Name.ToString();
                    timeStamp = string.Format("{0} - {1}: \n\n", DateTime.Now.ToString("MM/dd/yyyy - hh:mm:ss tt"), CurrentUser);

                    if (string.IsNullOrEmpty(NotesHistoryNode.Value))
                    {
                        NotesHistoryNode.AppendChild(string.Format("{0}{1}", timeStamp, NotesNode.Value));
                    }
                    else
                    {
                        NotesHistoryNode.AppendChild(string.Format("\n\n{0}{1}", timeStamp, NotesNode.Value));
                    }
                    ClearNotesField(Form);
                }
            }
            catch (Exception Err)
            {
                //LoggingHelper.DoLogFile(string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "SetNotesHistory", Err.Message.ToString(), Err.StackTrace.ToString()), Form);
                string CustomMsg = string.Format("*Error: Method [{0}] - Message [{1}] - Stack [{2}]", "SetNotesHistory", Err.Message.ToString(), Err.StackTrace.ToString());
                LoggingHelper.LogExceptionToSP(CustomMsg, Err);
            }
        }// SetNotesHistory


        public static void ClearNotesField(XmlFormHostItem Form)
        {
            XPathNavigator root = Form.MainDataSource.CreateNavigator(), NotesNode = default(XPathNavigator);
            XmlNamespaceManager NS = Form.NamespaceManager;

            try
            {
                NotesNode = root.SelectSingleNode(Constants.NotesXPath, NS);
                NotesNode.SetValue(string.Empty);
            }
            catch { }

        }// Clear Notes Field

    }// end Class
}
