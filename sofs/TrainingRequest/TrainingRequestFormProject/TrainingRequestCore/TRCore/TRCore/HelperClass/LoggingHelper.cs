using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

using Microsoft.Office.InfoPath;
using System.Xml;
using System.Xml.XPath;
using SPLoggingWriter;

/***************************************************************
 * 
 *  THIS CLASS WILL HANDLE ALL LOGGING FUNCTIONS
 * 
 * *************************************************************/
namespace TRCore.HelperClass
{
    public static class LoggingHelper
    {
        // Class variable
        private static string _LogDirectory = @"C:\Debug\Logs";
        private static string _LogFilename = @"TrainingRequest";


        /// <summary>
        /// This will write out to a text file per line in [DateTime - Message] format
        /// </summary>
        /// <param name="directoryPath">The path to the text file's directory without ending "\"</param>
        /// <param name="filename">Name of the text file wihtout extension</param>
        /// <param name="message">string message</param>
        public static void DoLogFile(string message, XmlFormHostItem Form)
        {// 

            // ---------------------------
            // For Debugging purposes only
            // ---------------------------
            XPathNavigator DebugNode = Form.MainDataSource.CreateNavigator().SelectSingleNode("/my:myFields/my:Debug/my:DebugMessage", Form.NamespaceManager);

            try
            {

                //DebugNode.AppendChild(message + "\n\n");

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
            catch (Exception err)
            {
                DebugNode.AppendChild("*Error: DoLogFile - " + err.Message.ToString());
            }

        }// end fnDoLog

        /// <summary>
        /// Logs custom message and exception to the SP log
        /// </summary>
        /// <param name="message">Custom message</param>
        /// <param name="err">Exception</param>
        public static void LogExceptionToSP(string message, Exception err)
        {
            SPLogWriter.LogErrorMessage("TRCore", message, err);
        }// end logexceptiontosp
    }
}
