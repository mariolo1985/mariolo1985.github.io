using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Office.InfoPath;
using System.Xml;
using System.Xml.XPath;

/********************************************************
 * 
 *      This will handle setting the user message view
 *      
 * *****************************************************/

namespace TRCore.HelperClass
{
    public static class UserMessage
    {
        //private static string MessageXPath = "/my:myFields/my:SystemInternal/my:UserMessage";
        //private static string PreviousViewXpath = "/my:myFields/my:SystemInternal/my:PreviousView";

        /// <summary>
        /// This will populate the User Message box with [Message] and append if text already exist
        /// </summary>
        /// <param name="Form">An infopath form obj.  Usually "This"</param>
        /// <param name="Message">A string message to pass into message box</param>
        /// <param name="ClearMessage">Flag to clear the message box</param>
        public static void SetUserMessage(XmlFormHostItem Form, string Message)
        {
            XPathNavigator root = Form.MainDataSource.CreateNavigator(), MessageNode = default(XPathNavigator);
            XmlNamespaceManager NS = Form.NamespaceManager;

            try
            {
                MessageNode = root.SelectSingleNode(Constants.MessageXPath, NS);           
            }
            catch { }

            if (MessageNode != null)
            {

                // check if messagenode is empty ? set message : append message
                if (string.IsNullOrEmpty(MessageNode.Value))
                {
                    MessageNode.SetValue(Message);
                }
                else
                {
                    MessageNode.AppendChild("\n\n" + Message);
                }
            }

        }// End SetUserMessage

        /// <summary>
        /// Empties the message box
        /// </summary>
        /// <param name="Form"></param>
        public static void ClearUserMessage(XmlFormHostItem Form)
        {
            XPathNavigator root = Form.MainDataSource.CreateNavigator(), MessageNode = default(XPathNavigator);
            XmlNamespaceManager NS = Form.NamespaceManager;

            MessageNode = root.SelectSingleNode(Constants.MessageXPath, NS);
            MessageNode.SetValue(string.Empty);
        }// end ClearUserMessage

    }
}
