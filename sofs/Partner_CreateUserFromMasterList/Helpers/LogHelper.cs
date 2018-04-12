using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using Microsoft.SharePoint.Administration;

namespace Partner_CreateUserFromMasterList.Helpers
{
    public static class LogHelper
    {
        private const string _LogDirectory = @"C:\PartnerMasterProvision";
        private const string _LogFilename = @"UserCreation";

        /// <summary>
        /// Log error message out to ULS/SP logs
        /// </summary>
        public static void LogMessage(String AppName, String message, Exception ex)
        {
            try
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory(AppName, TraceSeverity.High, EventSeverity.Error), TraceSeverity.High, message + " - " + ex.Message, ex.StackTrace);
            }
            catch { }
        }// end logerrormessage


        /// <summary>
        /// Log verbose message out to ULS/SP logs
        /// </summary>
        public static void LogMessage(String AppName, String message)
        {
            try
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory(AppName, TraceSeverity.Verbose, EventSeverity.Verbose), TraceSeverity.Verbose, message, null);
            }
            catch { }
        }// end logerrormessage

        public static void LogToTextfile(string message)
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
                if (!string.IsNullOrEmpty(message))
                {
                    sw.WriteLine(DateTime.Now.ToString() + " - " + message.ToString());
                }
                else
                {
                    sw.WriteLine(string.Empty);
                }
                sw.Flush();
            }
        }// end logtotextfile
    }
}
